using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using static YouTube_downloader.Utils;
using YouTube_downloader.Properties;

namespace YouTube_downloader
{
    public partial class FrameYouTubeVideo : UserControl
    {
        private SynchronizationContext synchronizationContext;
        private YouTubeVideo _youTubeVideo = null;
        public YouTubeVideo VideoInfo { get { return _youTubeVideo; } set { SetVideoInfo(value); } }
        private bool favoriteVideo = false;
        private bool favoriteChannel = false;
        public string webPage = null;
        public bool FavoriteVideo
        {
            get
            {
                return favoriteVideo;
            }
            set
            {
                SetFavoriteVideo(value);
            }
        }
        public bool FavoriteChannel
        {
            get
            {
                return favoriteChannel;
            }
            set
            {
                SetFavoriteChannel(value);
            }
        }

        public bool IsDownloading => downloading;

        private readonly List<YouTubeVideoFile> videoFormats = new List<YouTubeVideoFile>();
        private readonly List<YouTubeAudioFile> audioFormats = new List<YouTubeAudioFile>();

        public delegate void BtnDownloadClickedDelegate(object sender, EventArgs e);
        public delegate void FavoriteChannelChangedDelegate(object sender, string channelId, bool newState);
        public delegate void ActivatedDelegate(object sender);
        public delegate void OpenChannelDelegate(object sender, string channelName, string channelId);
        public BtnDownloadClickedDelegate BtnDownloadClicked;
        public FavoriteChannelChangedDelegate FavoriteChannelChanged;
        public ActivatedDelegate Activated;
        public OpenChannelDelegate OpenChannel;

        private int oldX;
        private bool canDrag = false;
        private bool downloadCancelRequired = false;
        private bool downloading = false;
        public const int EXTRA_WIDTH = 140;


        public FrameYouTubeVideo()
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
        }

        private void FrameYouTubeVideo_Load(object sender, EventArgs e)
        {
            lblStatus.Text = null;
            lblProgress.Text = null;
        }

        private void FrameYouTubeVideo_Resize(object sender, EventArgs e)
        {
            int offset = 10;
            imageFavorite.Left = Parent.Width - imageFavorite.Width - offset;
            btnDownload.Left = Parent.Width - btnDownload.Width - offset;
            lblVideoTitle.Width = imageFavorite.Left - lblVideoTitle.Left - 4;
            progressBarDownload.Width = btnDownload.Left - progressBarDownload.Left - 4;
            btnGetVideoInfo.Left = Parent.Width + offset;
            btnGetWebPage.Left = btnGetVideoInfo.Left;
            btnGetDashManifest.Left = btnGetVideoInfo.Left;
            btnGetHlsManifest.Left = btnGetVideoInfo.Left;
            imgScrollbar.Left = 0;
            imgScrollbar.Width = Parent.Width;

            imgScrollbar.Invalidate();
        }

        private void SetVideoInfo(YouTubeVideo videoInfo)
        {
            _youTubeVideo = videoInfo;
            lblVideoTitle.Text = videoInfo.Title;
            lblChannelTitle.Text = videoInfo.ChannelOwned.Title;
            lblDatePublished.Text = $"Дата публикации: {videoInfo.DatePublished:yyyy.MM.dd}";
            FavoriteItem favoriteItem = new FavoriteItem(
                videoInfo.Title, videoInfo.Title, videoInfo.Id,
                videoInfo.ChannelOwned.Title, videoInfo.ChannelOwned.Id, null);
            favoriteVideo = FindInFavorites(favoriteItem, favoritesRootNode) != null;

            favoriteItem.DisplayName = VideoInfo.Title;
            favoriteItem.ID = VideoInfo.ChannelOwned.Id;
            favoriteChannel = FindInFavorites(favoriteItem, favoritesRootNode) != null;
        }

        public void SetFavoriteVideo(bool fav)
        {
            favoriteVideo = fav;
            if (favoriteVideo)
            {   
                FavoriteItem favoriteItem = new FavoriteItem(
                    VideoInfo.Title, VideoInfo.Title, VideoInfo.Id,
                    VideoInfo.ChannelOwned.Title, VideoInfo.ChannelOwned.Id, favoritesRootNode);
                favoriteItem.ItemType = FavoriteItemType.Video;
                if (FindInFavorites(favoriteItem, favoritesRootNode) == null)
                {
                    favoritesRootNode.Children.Add(favoriteItem);
                    treeFavorites.RefreshObject(favoritesRootNode);
                }
            }
            else
            {
                FavoriteItem favoriteItem = FindInFavorites(VideoInfo.Id);
                if (favoriteItem != null)
                {
                    treeFavorites.RemoveObject(favoriteItem);
                    favoriteItem.Parent.Children.Remove(favoriteItem);
                    treeFavorites.RefreshObject(favoriteItem.Parent);
                }
            }
            imageFavorite.Invalidate();
        }

        private void SetFavoriteChannel(bool fav)
        {
            favoriteChannel = fav;
            if (favoriteChannel)
            {
                if (FindInFavorites(VideoInfo.ChannelOwned.Id) == null)
                {
                    FavoriteItem favoriteItem = new FavoriteItem(
                        VideoInfo.ChannelOwned.Title, VideoInfo.ChannelOwned.Title, VideoInfo.ChannelOwned.Id, 
                        null, null, favoritesRootNode);
                    favoriteItem.ItemType = FavoriteItemType.Channel;
                    favoritesRootNode.Children.Add(favoriteItem);
                    treeFavorites.RefreshObject(favoriteItem.Parent);
                }
            }
            else
            {
                FavoriteItem favoriteItem = FindInFavorites(VideoInfo.ChannelOwned.Id);
                if (favoriteItem != null)
                {
                    treeFavorites.RemoveObject(favoriteItem);
                    favoriteItem.Parent.Children.Remove(favoriteItem);
                    treeFavorites.RefreshObject(favoriteItem.Parent);
                }
            }
            imageFavoriteChannel.Invalidate();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (downloading)
            {
                downloadCancelRequired = true;
                btnDownload.Enabled = false;
                return;
            }
            
            btnDownload.Enabled = false;
            BtnDownloadClicked?.Invoke(this, e);

            if (progressBarDownload.Value < progressBarDownload.Maximum)
            {
                progressBarDownload.Value = 0;
            }
            ThreadGetDownloadableFormats thr = new ThreadGetDownloadableFormats(webPage);
            thr.ThreadCompleted += EventThreadGetFormatsTerminate;
            thr.Info += (s, info) =>
            {      
                lblProgress.Text = null;
                lblStatus.Text = info;
            };

            thr._videoId = VideoInfo.Id;
            thr._ciphered = VideoInfo.Ciphered;
            Thread thread = new Thread(thr.Run);
            thread.Start(synchronizationContext);
        }

        private void EventThreadGetFormatsTerminate(object sender)
        {
            lblStatus.Text = null;
            audioFormats.Clear();
            videoFormats.Clear();
            menuDownloads.Items.Clear();

            ThreadGetDownloadableFormats thr = (ThreadGetDownloadableFormats)sender;
            if (thr.videoFiles.Count == 0 && thr.audioFiles.Count == 0)
            {
                string t = "Ссылки для скачивания не найдены!";
                if (!VideoInfo.IsFamilySafe)
                {
                    t += "\nДля этого видео установлено ограничение по возрасту. " +
                        "Чтобы его скачать, воспользуйтесь поиском по коду веб-страницы.\n" +
                        "Для этого вам понадобится браузер и аккаунт на ютубе.";
                }
                MessageBox.Show(t, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDownload.Enabled = true;
                return;
            }
         
            for (int i = 0; i < thr.videoFiles.Count; i++)
            {
                videoFormats.Add(thr.videoFiles[i]);
                ToolStripMenuItem mi = new ToolStripMenuItem(thr.videoFiles[i].ToString());
                mi.Tag = thr.videoFiles[i];
                mi.Click += MenuItemDownloadClick;
                menuDownloads.Items.Add(mi);
            }
            if (thr.audioFiles.Count > 0)
            {
                menuDownloads.Items.Add("-");
                for (int i = 0; i < thr.audioFiles.Count; i++)
                {
                    audioFormats.Add(thr.audioFiles[i]);
                    ToolStripMenuItem mi = new ToolStripMenuItem(thr.audioFiles[i].ToString());
                    mi.Tag = thr.audioFiles[i];
                    mi.Click += MenuItemDownloadClick;
                    menuDownloads.Items.Add(mi);
                }
            }

            btnDownload.Enabled = true;

            Point pt = PointToScreen(new Point(btnDownload.Left + btnDownload.Width, btnDownload.Top));                           
            menuDownloads.Show(pt.X, pt.Y);
        }

        private async Task<DownloadResult> DownloadDash(YouTubeMediaFile mediaFile, string formattedFileName)
        {
            progressBarDownload.Value = 0;
            progressBarDownload.Maximum = mediaFile.dashManifestUrls.Count;
            string mediaType = mediaFile is YouTubeVideoFile ? "видео" : "аудио";
            lblStatus.Text = $"Скачивание чанков {mediaType}:";
            lblProgress.Left = lblStatus.Left + lblStatus.Width;
            lblProgress.Text = $"0 / {mediaFile.dashManifestUrls.Count} (0.00%), {mediaFile.GetShortInfo()}";

            string fnDash = MultiThreadedDownloader.GetNumberedFileName(
                (config.MergeToContainer ? config.TempDirPath : config.DownloadingDirPath) +
                $"{formattedFileName}_{mediaFile.formatId}.{mediaFile.fileExtension}");
            string fnDashFinal = fnDash;
            string fnDashTmp = fnDash + ".tmp";

            Progress<int> progressDash = new Progress<int>();
            progressDash.ProgressChanged += (s, n) =>
            {
                progressBarDownload.Value = n + 1;
                double percent = 100.0 / progressBarDownload.Maximum * (n + 1);
                lblProgress.Text = $"{n + 1} / {mediaFile.dashManifestUrls.Count}" +
                    $" ({string.Format("{0:F2}", percent)}%), {mediaFile.GetShortInfo()}";
            };

            int res = await Task.Run(() =>
            {
                if (File.Exists(fnDashTmp))
                {
                    File.Delete(fnDashTmp);
                }
                IProgress<int> dashReporter = progressDash;
                Stream fileStream = File.OpenWrite(fnDashTmp);
                FileDownloader d = new FileDownloader();
                int errorCode = 400;
                for (int i = 0; i < mediaFile.dashManifestUrls.Count; i++)
                {
                    int errors = 0;
                    do
                    {
                        Stream memChunk = new MemoryStream();
                        d.Url = mediaFile.dashManifestUrls[i];
                        errorCode = d.Download(memChunk);
                        if (errorCode != 200)
                        {
                            memChunk.Dispose();
                            continue;
                        }
                        memChunk.Position = 0;
                        bool appended = MultiThreadedDownloader.AppendStream(memChunk, fileStream);
                        memChunk.Dispose();
                        if (!appended)
                        {
                            fileStream.Dispose();
                            return MultiThreadedDownloader.DOWNLOAD_ERROR_MERGING_CHUNKS;
                        }
                    } while (errorCode != 200 && errors++ < 9 && !downloadCancelRequired);
                    if (downloadCancelRequired)
                    {
                        errorCode = FileDownloader.DOWNLOAD_ERROR_ABORTED_BY_USER;
                    }
                    if (errorCode != 200)
                    {
                        break;
                    }
                    dashReporter.Report(i);
                }
                fileStream.Dispose();
                if (errorCode == 200)
                {
                    fnDashFinal = MultiThreadedDownloader.GetNumberedFileName(fnDash);
                    File.Move(fnDashTmp, fnDashFinal);
                }
                return errorCode;
            }
            );
            return new DownloadResult(res, fnDashFinal);
        }

        private async Task<DownloadResult> DownloadYouTubeMediaFile(
            YouTubeMediaFile mediaFile, string formattedFileName, bool audioOnly = false)
        {
            if (mediaFile is YouTubeVideoFile)
            {
                YouTubeVideoFile videoTrack = (YouTubeVideoFile)mediaFile;
                if (videoTrack.isDashManifest)
                {
                    #region Скачивание видео Даши
                    return await DownloadDash(videoTrack, formattedFileName);
                    #endregion
                }
                else //без Даши
                {
                    #region Скачивание видео-дорожки
                    #region Расшифровка Cipher
                    if (videoTrack.isCiphered)
                    {
                        if (string.IsNullOrEmpty(config.CipherDecryptionAlgo) || string.IsNullOrWhiteSpace(config.CipherDecryptionAlgo))
                        {
                            return new DownloadResult(ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM, null);
                        }

                        string cipherDecrypted = DecryptCipherSignature(videoTrack.cipherSignatureEncrypted, config.CipherDecryptionAlgo);

                        if (string.IsNullOrEmpty(cipherDecrypted))
                        {
                            return new DownloadResult(ERROR_CIPHER_DECRYPTION, null);
                        }

                        videoTrack.url = $"{videoTrack.cipherUrl}&sig={cipherDecrypted}";

                        if (MultiThreadedDownloader.GetUrlContentLength(videoTrack.url, out _) != 200)
                        {
                            return new DownloadResult(ERROR_CIPHER_DECRYPTION, null);
                        }

                        if (!videoTrack.isContainer)
                        {
                            //расшифровка аудио
                            string audioCipherDecrypted = DecryptCipherSignature(
                                audioFormats[0].cipherSignatureEncrypted, config.CipherDecryptionAlgo);
                            audioFormats[0].url = $"{audioFormats[0].cipherUrl}&sig={audioCipherDecrypted}";
                            if (MultiThreadedDownloader.GetUrlContentLength(audioFormats[0].url, out _) != 200)
                            {
                                return new DownloadResult(ERROR_CIPHER_DECRYPTION, null);
                            }
                        }
                    }
                    #endregion
                    MultiThreadedDownloader downloader = new MultiThreadedDownloader();
                    downloader.ThreadCount = config.ThreadCountVideo;
                    downloader.Url = videoTrack.url;
                    downloader.TempDirectory = config.TempDirPath;
                    downloader.MergingDirectory = config.ChunksMergingDirPath;

                    string fnVideo;
                    if (videoTrack.isContainer)
                    {
                        fnVideo = MultiThreadedDownloader.GetNumberedFileName(
                            $"{config.DownloadingDirPath}{formattedFileName}.{videoTrack.mimeExt}");
                        downloader.KeepDownloadedFileInMergingDirectory = false;
                    }
                    else
                    {
                        bool ffmpegExists = !string.IsNullOrEmpty(config.FfmpegExeFilePath) && 
                            !string.IsNullOrWhiteSpace(config.FfmpegExeFilePath) && 
                            File.Exists(config.FfmpegExeFilePath);
                        fnVideo = MultiThreadedDownloader.GetNumberedFileName(
                            (ffmpegExists ? config.TempDirPath : config.DownloadingDirPath) +
                            $"{formattedFileName}_{videoTrack.formatId}.{videoTrack.fileExtension}");
                        downloader.KeepDownloadedFileInMergingDirectory = true;
                    }
                    downloader.OutputFileName = fnVideo;
                    downloader.DownloadStarted += (s, size) =>
                    {
                        progressBarDownload.Value = 0;
                        progressBarDownload.Maximum = 100;

                        lblStatus.Text = "Скачивание видео:";
                        lblProgress.Text = $"0 / {FormatSize(size)} (0.00%), {videoTrack.GetShortInfo()}";
                        lblProgress.Left = lblStatus.Left + lblStatus.Width;
                    };
                    downloader.DownloadProgress += (object s, long bytesTransfered) =>
                    {
                        long fileSize = downloader.ContentLength != 0 ? downloader.ContentLength : videoTrack.contentLength;
                        double percent = 100.0 / fileSize * bytesTransfered;
                        progressBarDownload.Value = (int)Math.Round(percent);

                        lblProgress.Text = $"{FormatSize(bytesTransfered)} / {FormatSize(fileSize)}" +
                            $" ({string.Format("{0:F2}", percent)}%), {videoTrack.GetShortInfo()}";

                    };
                    downloader.CancelTest += (object s, ref bool cancel) =>
                    {
                        cancel = downloadCancelRequired;
                    };
                    downloader.MergingStarted += (s, chunkCount) =>
                    {
                        progressBarDownload.Value = 0;
                        progressBarDownload.Maximum = chunkCount;
                 
                        lblStatus.Text = "Объединение чанков видео:";
                        lblProgress.Text = $"0 / {chunkCount}";
                        lblProgress.Left = lblStatus.Left + lblStatus.Width;
                    };
                    downloader.MergingProgress += (s, chunkId) =>
                    {
                        lblProgress.Text = $"{chunkId + 1} / {downloader.ThreadCount}";
                        progressBarDownload.Value = chunkId + 1;
                    };
                    int res = await downloader.Download();
                    return new DownloadResult(res, downloader.OutputFileName);
                    #endregion
                }
            }
            else if (mediaFile is YouTubeAudioFile)
            {
                YouTubeAudioFile audioTrack = (YouTubeAudioFile)mediaFile;
                if (audioTrack.isDashManifest)
                {
                    #region Скачивание аудио Даши
                    return await DownloadDash(audioTrack, formattedFileName);
                    #endregion
                }
                else // без Даши
                {
                    #region Скачивание аудио-дорожки
                    #region Расшифровка Cipher
                    if (audioTrack.isCiphered)
                    {
                        if (MultiThreadedDownloader.GetUrlContentLength(audioTrack.url, out _) != 200)
                        {

                            if (string.IsNullOrEmpty(config.CipherDecryptionAlgo) || string.IsNullOrWhiteSpace(config.CipherDecryptionAlgo))
                            {
                                return new DownloadResult(ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM, null);
                            }

                            string cipherDecrypted = DecryptCipherSignature(audioTrack.cipherSignatureEncrypted, config.CipherDecryptionAlgo);

                            if (string.IsNullOrEmpty(cipherDecrypted))
                            {
                                return new DownloadResult(ERROR_CIPHER_DECRYPTION, null);
                            }

                            audioTrack.url = $"{audioTrack.cipherUrl}&sig={cipherDecrypted}";

                            if (MultiThreadedDownloader.GetUrlContentLength(audioTrack.url, out _) != 200)
                            {
                                return new DownloadResult(ERROR_CIPHER_DECRYPTION, null);
                            }
                        }
                    }
                    #endregion
                    
                    MultiThreadedDownloader downloader = new MultiThreadedDownloader();
                    downloader.ThreadCount = config.ThreadCountAudio;
                    downloader.Url = audioTrack.url;
                    downloader.TempDirectory = config.TempDirPath;
                    downloader.MergingDirectory = config.ChunksMergingDirPath;
                    downloader.KeepDownloadedFileInMergingDirectory = true;

                    bool ffmpegExists = !string.IsNullOrEmpty(config.FfmpegExeFilePath) && 
                        !string.IsNullOrWhiteSpace(config.FfmpegExeFilePath) && File.Exists(config.FfmpegExeFilePath);
                    string fnAudio = MultiThreadedDownloader.GetNumberedFileName(
                        (config.MergeToContainer && !audioOnly && ffmpegExists ? config.TempDirPath : config.DownloadingDirPath) +
                         $"{formattedFileName}_{audioTrack.formatId}.{audioTrack.fileExtension}");
                    downloader.OutputFileName = fnAudio;
                    downloader.DownloadStarted += (s, size) =>
                    {
                        progressBarDownload.Value = 0;
                        progressBarDownload.Maximum = 100;

                        lblStatus.Text = "Скачивание аудио:";
                        lblProgress.Text = $"0 / {FormatSize(size)} (0.00%), {audioTrack.GetShortInfo()}";
                        lblProgress.Left = lblStatus.Left + lblStatus.Width;
                    };
                    downloader.DownloadProgress += (object s, long bytesTransfered) =>
                    {
                        long fileSize = downloader.ContentLength != 0 ? downloader.ContentLength : audioTrack.contentLength;
                        double percent = 100 / (double)fileSize * bytesTransfered;
                        progressBarDownload.Value = (int)Math.Round(percent);

                        lblProgress.Text = $"{FormatSize(bytesTransfered)} / {FormatSize(fileSize)}" +
                            $" ({string.Format("{0:F2}", percent)}%), {audioTrack.GetShortInfo()}";
                    };
                    downloader.CancelTest += (object s, ref bool cancel) =>
                    {
                        cancel = downloadCancelRequired;
                    };
                    downloader.MergingStarted += (s, chunkCount) =>
                    {
                        progressBarDownload.Value = 0;
                        progressBarDownload.Maximum = chunkCount;

                        lblStatus.Text = "Объединение чанков аудио:";
                        lblProgress.Text = $"0 / {chunkCount}";
                        lblProgress.Left = lblStatus.Left + lblStatus.Width;
                    };
                    downloader.MergingProgress += (s, chunkId) =>
                    {
                        lblProgress.Text = $"{chunkId + 1} / {downloader.ThreadCount}";
                        progressBarDownload.Value = chunkId + 1;
                    };
                    int res = await downloader.Download();
                    return new DownloadResult(res, downloader.OutputFileName);
                    #endregion
                }
            }
            return new DownloadResult(400, string.Empty);
        }

        private async void MenuItemDownloadClick(object sender, EventArgs e)
        {
            btnDownload.Enabled = false;

            if (string.IsNullOrEmpty(config.DownloadingDirPath) || string.IsNullOrWhiteSpace(config.DownloadingDirPath))
            {
                MessageBox.Show("Не указана папка для скачивания!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDownload.Enabled = true;
                return;
            }
            if (!Directory.Exists(config.DownloadingDirPath))
            {
                MessageBox.Show("Папка для скачивания не найдена!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDownload.Enabled = true;
                return;
            }
            if (string.IsNullOrEmpty(config.TempDirPath) || string.IsNullOrWhiteSpace(config.TempDirPath))
            {
                MessageBox.Show("Не указана папка для временных файлов!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDownload.Enabled = true;
                return;
            }
            if (!Directory.Exists(config.TempDirPath))
            {
                MessageBox.Show("Папка для временных файлов не найдена!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDownload.Enabled = true;
                return;
            }
            if (!string.IsNullOrEmpty(config.ChunksMergingDirPath) &&
                !string.IsNullOrWhiteSpace(config.ChunksMergingDirPath) &&
                !Directory.Exists(config.ChunksMergingDirPath))
            {
                MessageBox.Show("Папка для объединения чанков не найдена!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnDownload.Enabled = true;
                return;
            }

            downloading = true;
            downloadCancelRequired = false;

            progressBarDownload.Value = 0;
            lblStatus.Text = "Скачивание...";
            lblProgress.Text = null;

            ToolStripMenuItem mi = sender as ToolStripMenuItem;
            string formattedFileName = FixFileName(FormatFileName(config.OutputFileNameFormat, VideoInfo));
            if (mi.Tag is YouTubeVideoFile)
            {
                YouTubeVideoFile videoFile = mi.Tag as YouTubeVideoFile;
                bool ffmpegExists = !string.IsNullOrEmpty(config.FfmpegExeFilePath) && 
                    !string.IsNullOrWhiteSpace(config.FfmpegExeFilePath) && 
                    File.Exists(config.FfmpegExeFilePath);
                if (videoFile.isHlsManifest)
                {
                    lblStatus.Text = null;
                    string fn = MultiThreadedDownloader.GetNumberedFileName($"{config.DownloadingDirPath}{formattedFileName}.ts");
                    GrabHls(videoFile.url, fn);

                    downloading = false;
                    btnDownload.Enabled = true;
                    return;
                }

                bool stop = false;
                if (config.MergeToContainer && !ffmpegExists)
                {
                    string msg = "Формат данного видео является адаптивным. " +
                        "Это значит, что дорожки видео и аудио хранятся по отдельности. " +
                        "Чтобы склеить их воедино, нужен FFMPEG.EXE. Но он не указан в настройках или не найден.\n" +
                        "Продолжить скачивание без склеивания?";
                    if (MessageBox.Show(msg, "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        stop = true;
                    }
                }
                if (stop)
                {
                    downloading = false;
                    btnDownload.Enabled = true;
                    return;
                }
                btnDownload.Text = "Отмена";
                btnDownload.Enabled = true;

                //TODO: Refactor this shit!
                DownloadResult resVideo = await DownloadYouTubeMediaFile(mi.Tag as YouTubeVideoFile, formattedFileName);
                if (resVideo.ErrorCode == 200)
                {
                    if (!videoFile.isContainer)
                    {
                        lblProgress.Text = null;
                        lblStatus.Text = "Состояние: Скачивание аудио...";
                        DownloadResult resAudio = await DownloadYouTubeMediaFile(audioFormats[0], formattedFileName);

                        btnDownload.Enabled = false;
                        lblProgress.Text = null;
                        lblStatus.Text = "Состояние: Подготовка...";
                        Application.DoEvents();
                        if (resAudio.ErrorCode == 200)
                        {
                            if (config.MergeToContainer)
                            {
                                if (ffmpegExists)
                                {
                                    lblStatus.Text = "Состояние: Объединение видео и аудио...";
                                    string ext = (videoFile.fileExtension == "m4v" &&
                                        audioFormats[0].fileExtension == "m4a") ? "mp4" : "mkv";
                                    await MergeYouTubeMediaTracks(resVideo.FileName, resAudio.FileName,
                                        MultiThreadedDownloader.GetNumberedFileName($"{config.DownloadingDirPath}{formattedFileName}.{ext}"));
                                    if (config.DeleteSourceFiles)
                                    {
                                        if (File.Exists(resVideo.FileName))
                                        {
                                            File.Delete(resVideo.FileName);
                                        }
                                        if (File.Exists(resAudio.FileName))
                                        {
                                            File.Delete(resAudio.FileName);
                                        }
                                    }
                                }
                            }

                            //сохранение картинки
                            if (config.SaveImagePreview && VideoInfo.ImageData != null)
                            {
                                SaveImageToFile(formattedFileName);
                            }
                            lblStatus.Text = "Состояние: Ожидание нажатия кнопки \"OK\"";
                            MessageBox.Show($"{VideoInfo.Title}\nСкачано!", "Успех!",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            lblStatus.Text = "Состояние: Скачано";
                        }
                        else
                        {
                            switch (resAudio.ErrorCode)
                            {
                                case ERROR_CIPHER_DECRYPTION:
                                    lblStatus.Text = "Состояние: Ошибка ERROR_CIPHER_DECRYPTION";
                                    MessageBox.Show($"{VideoInfo.Title}\n" +
                                        "Ошибка расшифровки ссылки! Попробуйте ещё раз.", "Ошибка!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;

                                case ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM:
                                    lblStatus.Text = "Состояние: Ошибка ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM";
                                    string t = "Ссылка на это видео, зачем-то, зашифрована алгоритмом \"Cipher\", " +
                                        "для расшифровки которого вам требуется ввести специальную последовательность чисел, " +
                                        "известную одному лишь дьяволу.";
                                    MessageBox.Show($"{VideoInfo.Title}\nОшибка ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM\n{t}", "Ошибка!",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;

                                case MultiThreadedDownloader.DOWNLOAD_ERROR_MERGING_CHUNKS:
                                    lblStatus.Text = "Состояние: Ошибка объединения чанков видео";
                                    MessageBox.Show($"{VideoInfo.Title}\nОшибка объединения чанков видео!\n" +
                                        "Повторите попытку скачивания.", "Ошибка!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;

                                case FileDownloader.DOWNLOAD_ERROR_ZERO_LENGTH_CONTENT:
                                    lblStatus.Text = "Состояние: Ошибка! Файл на сервере пуст!";
                                    MessageBox.Show($"{VideoInfo.Title}\nФайл на сервере пуст!", "Ошибка!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;

                                case FileDownloader.DOWNLOAD_ERROR_ABORTED_BY_USER:
                                    lblStatus.Text = "Состояние: Скачивание отменено";
                                    MessageBox.Show($"{VideoInfo.Title}\nСкачивание успешно отменено!", "Отменятор отменения отмены",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;

                                case FileDownloader.DOWNLOAD_ERROR_RANGE:
                                    lblStatus.Text = "Состояние: Ошибка DOWNLOAD_ERROR_RANGE";
                                    MessageBox.Show($"{VideoInfo.Title}\nЗадан неправильный диапазон!", "Ошибка!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;

                                case FileDownloader.DOWNLOAD_ERROR_UNKNOWN:
                                    lblStatus.Text = "Состояние: Неизвестная ошибка";
                                    MessageBox.Show($"{VideoInfo.Title}\nНеизвестная ошибка! Скачивание прервано!", "Ошибка!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;

                                default:
                                    lblStatus.Text = $"Состояние: Ошибка {resAudio.ErrorCode}";
                                    MessageBox.Show($"{VideoInfo.Title}\nОшибка {resAudio.ErrorCode}", "Ошибка!",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        lblStatus.Text = null;
                        lblProgress.Text = null;

                        //сохранение картинки
                        if (config.SaveImagePreview && VideoInfo.ImageData != null)
                        {
                            SaveImageToFile(formattedFileName);
                        }
                        lblStatus.Text = "Состояние: Ожидание нажатия кнопки \"OK\"";
                        MessageBox.Show($"{VideoInfo.Title}\nСкачано!", "Успех!",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lblStatus.Text = "Состояние: Скачано";
                    }
                }
                else
                {
                    lblProgress.Text = null;

                    switch (resVideo.ErrorCode)
                    {
                        case ERROR_CIPHER_DECRYPTION:
                            lblStatus.Text = "Состояние: Ошибка ERROR_CIPHER_DECRYPTION";
                            MessageBox.Show($"{VideoInfo.Title}\n" +
                                "Ошибка расшифровки ссылки! Попробуйте ещё раз.", "Ошибка!",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM:
                            lblStatus.Text = "Состояние: Ошибка ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM";
                            string t = "Ссылка на это видео, зачем-то, зашифрована алгоритмом \"Cipher\", " +
                                "для расшифровки которого вам требуется ввести специальную последовательность чисел, " +
                                "известную одному лишь дьяволу.";
                            MessageBox.Show($"{VideoInfo.Title}\nОшибка ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM\n{t}", "Ошибка!",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case FileDownloader.DOWNLOAD_ERROR_INCOMPLETE_DATA_READ:
                            lblStatus.Text = "Состояние: Ошибка INCOMPLETE_DATA_READ";
                            MessageBox.Show($"{VideoInfo.Title}\nФайл скачан не полностью!\n" +
                                "Повторите попытку скачивания.", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case MultiThreadedDownloader.DOWNLOAD_ERROR_MERGING_CHUNKS:
                            lblStatus.Text = "Состояние: Ошибка объединения чанков видео";
                            MessageBox.Show($"{VideoInfo.Title}\nОшибка объединения чанков видео!\n" +
                                "Повторите попытку скачивания.", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case FileDownloader.DOWNLOAD_ERROR_ZERO_LENGTH_CONTENT:
                            lblStatus.Text = "Состояние: Ошибка! Файл на сервере пуст!"; 
                            MessageBox.Show($"{VideoInfo.Title}\nФайл на сервере пуст!", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case FileDownloader.DOWNLOAD_ERROR_ABORTED_BY_USER:
                            lblStatus.Text = "Состояние: Скачивание отменено";
                            MessageBox.Show($"{VideoInfo.Title}\nСкачивание успешно отменено!", "Отменятор отменения отмены",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;

                        case FileDownloader.DOWNLOAD_ERROR_RANGE:
                            lblStatus.Text = "Состояние: Ошибка DOWNLOAD_ERROR_RANGE";
                            MessageBox.Show($"{VideoInfo.Title}\nЗадан неправильный диапазон!", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case FileDownloader.DOWNLOAD_ERROR_UNKNOWN:
                            lblStatus.Text = "Состояние: Неизвестная ошибка";
                            MessageBox.Show($"{VideoInfo.Title}\nНеизвестная ошибка! Скачивание прервано!", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            lblStatus.Text = $"Состояние: Ошибка {resVideo.ErrorCode}";
                            MessageBox.Show($"{VideoInfo.Title}\nОшибка {resVideo.ErrorCode}", "Ошибка!",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
            }
            else if (mi.Tag is YouTubeAudioFile)
            {
                btnDownload.Text = "Отмена";
                btnDownload.Enabled = true;

                #region Скачивание только аудио
                DownloadResult resAudio = await DownloadYouTubeMediaFile(mi.Tag as YouTubeAudioFile, formattedFileName, true);
                lblProgress.Text = null;
                if (resAudio.ErrorCode == 200)
                {
                    lblStatus.Text = "Состояние: Скачано";
                    MessageBox.Show($"{VideoInfo.Title}\nСкачано!", "Успех!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    switch (resAudio.ErrorCode)
                    {
                        case ERROR_CIPHER_DECRYPTION:
                            lblStatus.Text = "Состояние: Ошибка ERROR_CIPHER_DECRYPTION";
                            MessageBox.Show($"{VideoInfo.Title}\nОшибка ERROR_CIPHER_DECRYPTION\n" +
                                "Не удалось расшифровать сигнатуру Cipher!", "Ошибка!",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM:
                            lblStatus.Text = "Состояние: Ошибка ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM";
                            string t = "Ссылка на это аудио, зачем-то, зашифрована алгоритмом \"Cipher\", " +
                                "для расшифровки которого вам требуется ввести специальную последовательность чисел.";
                            MessageBox.Show($"{VideoInfo.Title}\nОшибка ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM\n{t}",
                                "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case MultiThreadedDownloader.DOWNLOAD_ERROR_MERGING_CHUNKS:
                            lblStatus.Text = "Состояние: Ошибка объединения чанков аудио";
                            MessageBox.Show($"{VideoInfo.Title}\nОшибка объединения чанков аудио!\n" +
                                "Повторите попытку скачивания.", "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case FileDownloader.DOWNLOAD_ERROR_ABORTED_BY_USER:
                            lblStatus.Text = "Состояние: Скачивание отменено";
                            MessageBox.Show($"{VideoInfo.Title}\nСкачивание успешно отменено!", "Отменятор отменения отмены",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;

                        default:
                            lblStatus.Text = $"Состояние: Ошибка {resAudio.ErrorCode}";
                            MessageBox.Show($"{VideoInfo.Title}\nОшибка {resAudio.ErrorCode}", "Ошибка!",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }

                #endregion
            }
            downloading = false;
            btnDownload.Text = "Скачать";
            btnDownload.Enabled = true;
        }

        private void SaveImageToFile(string formattedFileName)
        {
            string imageFileName = VideoInfo.Image != null ?
                $"_image_{VideoInfo.Image.Width}x{VideoInfo.Image.Height}.jpg" : "_image.bin";
            string fn = $"{config.DownloadingDirPath}{formattedFileName}{imageFileName}";
            VideoInfo.ImageData.SaveToFile(fn);
        }

        private void FrameYouTubeVideo_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
            if (e.Button == MouseButtons.Left)
            {
                oldX = e.X;
                canDrag = true;
            }
        }

        private void FrameYouTubeVideo_MouseUp(object sender, MouseEventArgs e)
        {
            canDrag = false;
        }

        private void FrameYouTubeVideo_MouseMove(object sender, MouseEventArgs e)
        {
            if (canDrag)
            {
                int newX = Left + e.X - oldX;
                if (newX > 0)
                {
                    newX = 0;
                }
                else if (newX < -EXTRA_WIDTH)
                {
                    newX = -EXTRA_WIDTH;
                }
                Left = newX;
                imgScrollbar.Left = -Left;
                imgScrollbar.Invalidate();
            }
        }

        private void imgScrollbar_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, e.ClipRectangle);
            int xLeft = (int)Math.Round(imgScrollbar.Width / (double)Width * -Left);
            int xRight = (int)Math.Round(imgScrollbar.Width / (double)Width * (-Left + Parent.Width));
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(xLeft, 0, xRight - xLeft, imgScrollbar.Height));
        }

        private void imagePreview_Paint(object sender, PaintEventArgs e)
        {
            if (VideoInfo.Image != null)
            {
                Rectangle imageRect = new Rectangle(0, 0, VideoInfo.Image.Width, VideoInfo.Image.Height);
                Rectangle resizedRect = imageRect.ResizeTo(imagePreview.Size).CenterIn(imagePreview.ClientRectangle);
                e.Graphics.DrawImage(VideoInfo.Image, resizedRect);
            }
            Font fnt = new Font("Lucida Console", 10.0f);
            if (fnt != null)
            {
                if (VideoInfo.Length.Ticks > 0L)
                {
                    TimeSpan hour = new TimeSpan(1, 0, 0);
                    string videoLength = VideoInfo.Length.ToString(VideoInfo.Length >= hour ? "h':'mm':'ss" : "m':'ss");
                    SizeF sz = e.Graphics.MeasureString(videoLength, fnt);
                    float x = imagePreview.Width - sz.Width;
                    float y = imagePreview.Height - sz.Height;
                    e.Graphics.FillRectangle(Brushes.Black, new RectangleF(x, y, sz.Width, sz.Height));
                    e.Graphics.DrawString(videoLength, fnt, Brushes.White, new PointF(x, y));
                }
            
                if (VideoInfo.Ciphered || VideoInfo.Dashed)
                {
                    string t = VideoInfo.Dashed ? "dash" : "cipher";
                    SizeF sz = e.Graphics.MeasureString(t, fnt);
                    RectangleF rect = new RectangleF(0, imagePreview.Height - sz.Height, sz.Width, sz.Height);
                    e.Graphics.FillRectangle(Brushes.Black, rect);
                    e.Graphics.DrawString(t, fnt, Brushes.White, new PointF(rect.X, rect.Y));
                }
                if (VideoInfo.Hlsed)
                {
                    string t = "hls";
                    SizeF sz = e.Graphics.MeasureString(t, fnt);
                    float y = (VideoInfo.Ciphered || VideoInfo.Dashed) ? 
                        imagePreview.Height - sz.Height * 2 : imagePreview.Height - sz.Height;
                    RectangleF rect = new RectangleF(0, y, sz.Width, sz.Height);
                    e.Graphics.FillRectangle(Brushes.Black, rect);
                    e.Graphics.DrawString(t, fnt, Brushes.White, new PointF(rect.X, rect.Y));
                }
                if (!VideoInfo.IsFamilySafe)
                {
                    e.Graphics.DrawImage(Resources.age18plus, imagePreview.Width - 40, 0, 40, 40);
                }
                if (VideoInfo.IsUnlisted)
                {
                    e.Graphics.DrawImage(Resources.unlisted, 0, 0, 40, 40);
                }
                fnt.Dispose();
            }
        }

        private void imageFavorite_Paint(object sender, PaintEventArgs e)
        {
            DrawStar(e.Graphics, imageFavorite.Width / 2f, imageFavorite.Height / 2f,
                imageFavorite.Width / 2f, 35f, 3f, Color.LimeGreen, favoriteVideo);
        }

        private void imgFavoriteChannel_Paint(object sender, PaintEventArgs e)
        {
            DrawStar(e.Graphics, imageFavoriteChannel.Width / 2f, imageFavoriteChannel.Height / 2f,
                imageFavoriteChannel.Width / 2f, 35f, 3f, Color.LimeGreen, FavoriteChannel);
        }

        private void btnGetVideoInfo_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(webPage))
            {
                string t = ExtractVideoInfoFromWebPage(webPage);
                SetClipboardText(t);
                MessageBox.Show("Скопировано в буфер обмена");
                return;
            }
            int errorCode = GetYouTubeVideoInfoEx(VideoInfo.Id, out string info);
            if (errorCode == 200)
            {
                SetClipboardText(info);
                MessageBox.Show("Скопировано в буфер обмена");
            }
            else
            {
                MessageBox.Show("Ошибатор ошибок", $"Ошибка {errorCode}",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetWebPage_Click(object sender, EventArgs e)
        {
            btnGetWebPage.Enabled = false;

            int errorCode = GetYouTubeVideoWebPage(VideoInfo.Id, out string page);
            if (errorCode == 200)
            {
                SetClipboardText(page);
                MessageBox.Show("Скопировано в буфер обмена");
            }
            else
            {
                MessageBox.Show("Ошибатор ошибок", $"Ошибка {errorCode}",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btnGetWebPage.Enabled = true;
        }

        private void btnGetDashManifest_Click(object sender, EventArgs e)
        {
            int errorCode = GetYouTubeVideoInfoEx(VideoInfo.Id, out string info, VideoInfo.Ciphered);
            if (errorCode == 200)
            {
                JObject json = JObject.Parse(info);
                if (json != null)
                {
                    JToken jt = json.Value<JToken>("streamingData");
                    if (jt != null)
                    {
                        JObject jData = jt.Value<JObject>();
                        jt = jData.Value<JToken>("dashManifestUrl");
                        if (jt != null)
                        {
                            FileDownloader d = new FileDownloader();
                            d.Url = jt.Value<string>();
                            if (d.DownloadString(out string manifest) == 200)
                            {
                                SetClipboardText(manifest);
                                MessageBox.Show("Скопировано в буфер обмена.");
                                return;
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Ошибка!", "Ошибатор ошибок", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnGetHlsManifest_Click(object sender, EventArgs e)
        {
            int errorCode = GetYouTubeVideoInfoEx(VideoInfo.Id, out string info, VideoInfo.Ciphered);
            if (errorCode == 200)
            {
                JObject json = JObject.Parse(info);
                if (json != null)
                {
                    JToken jt = json.Value<JToken>("streamingData");
                    if (jt != null)
                    {
                        JObject jData = jt.Value<JObject>();
                        jt = jData.Value<JToken>("hlsManifestUrl");
                        if (jt != null)
                        {
                            FileDownloader d = new FileDownloader();
                            d.Url = jt.Value<string>();
                            if (d.DownloadString(out string manifest) == 200)
                            {
                                SetClipboardText(manifest);
                                MessageBox.Show("Скопировано в буфер обмена.");
                                return;
                            }
                        }
                    }
                }
            }
            MessageBox.Show("Ошибка!", "Ошибатор ошибок", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void copyVideoTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetClipboardText(VideoInfo.Title);
        }

        private void copyChannelTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetClipboardText(VideoInfo.ChannelOwned.Title);
        }

        private void copyVideoUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetClipboardText(YOUTUBE_VIDEO_URL_BASE + VideoInfo.Id);
        }

        private void copyChannelTitleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetClipboardText(VideoInfo.ChannelOwned.Title);
        }

        private void openChannelInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenUrlInBrowser(string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, VideoInfo.ChannelOwned.Id));
        }

        private void imagePreview_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
            if (e.Button == MouseButtons.Right)
            {
                menuImage.Show(Cursor.Position);
            }
        }

        private void lblChannelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
            if (e.Button == MouseButtons.Right)
            {
                menuChannelTitle.Show(Cursor.Position);
            }
        }

        private void lblVideoTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
        }

        private void imageFavorite_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
            if (e.Button == MouseButtons.Left)
            {
                SetFavoriteVideo(!favoriteVideo);
            }
        }

        private void imageFavoriteChannel_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
            if (FavoriteChannelChanged != null)
            {
                FavoriteChannelChanged.Invoke(this, VideoInfo.ChannelOwned.Id, !FavoriteChannel);
            }
            else
            {
                FavoriteChannel = !favoriteChannel;
            }
        }

        private void lblDatePublished_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
        }

        private void btnGetVideoInfo_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
        }

        private void btnDownload_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
        }

        private void progressBarDownload_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
        }

        private void imgScrollbar_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
        }

        private void openVideoInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenUrlInBrowser(YOUTUBE_VIDEO_URL_BASE + VideoInfo.Id);
        }

        private void lblChannelTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenChannel?.Invoke(this, VideoInfo.ChannelOwned.Title, VideoInfo.ChannelOwned.Id);
        }

        private void saveImageAssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VideoInfo.ImageData != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить изображение";
                sfd.Filter = "jpg|*.jpg";
                sfd.DefaultExt = ".jpg";
                sfd.AddExtension = true;
                sfd.InitialDirectory = string.IsNullOrEmpty(config.DownloadingDirPath) ? config.SelfDirPath : config.DownloadingDirPath;
                string fn = FixFileName(FormatFileName(config.OutputFileNameFormat, VideoInfo)) +
                    $"_image_{imagePreview.Image.Width}x{imagePreview.Image.Height}";
                sfd.FileName = fn;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    VideoInfo.ImageData.Position = 0L;
                    VideoInfo.ImageData.SaveToFile(sfd.FileName);
                }
                sfd.Dispose();
            }
        }

        private void copyChannelNameWithIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetClipboardText($"{VideoInfo.ChannelOwned.Title} [{VideoInfo.ChannelOwned.Id}]");
        }
    }
}
