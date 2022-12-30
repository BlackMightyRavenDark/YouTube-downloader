﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using MultiThreadedDownloaderLib;
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

        public FrameYouTubeVideo(Control parent)
        {
            InitializeComponent();
            if (parent != null)
            {
                Parent = parent;
            }

            SetVideoTitleFontSize(config.VideoTitleFontSize);
            imgScrollbar.SetDoubleBuffered(true);
            synchronizationContext = SynchronizationContext.Current;
        }

        private void FrameYouTubeVideo_Load(object sender, EventArgs e)
        {
            lblStatus.Text = null;
            lblProgress.Text = null;
        }

        private void FrameYouTubeVideo_Resize(object sender, EventArgs e)
        {
            Control parentControl = Parent;
            if (parentControl != null)
            {
                const int offset = 10;
                imageFavorite.Left = parentControl.Width - imageFavorite.Width - offset;
                btnDownload.Left = parentControl.Width - btnDownload.Width - offset;
                lblVideoTitle.Width = imageFavorite.Left - lblVideoTitle.Left - 4;
                progressBarDownload.Width = btnDownload.Left - progressBarDownload.Left - 4;
                btnGetVideoInfo.Left = parentControl.Width + offset;
                btnGetWebPage.Left = btnGetVideoInfo.Left;
                btnGetDashManifest.Left = btnGetVideoInfo.Left;
                btnGetHlsManifest.Left = btnGetVideoInfo.Left;
                btnGetPlayerCode.Left = btnGetVideoInfo.Left;

                imgScrollbar.Left = 0;
                imgScrollbar.Width = parentControl.Width;
                imgScrollbar.Invalidate();
            }
        }

        private void SetVideoInfo(YouTubeVideo videoInfo)
        {
            _youTubeVideo = videoInfo;
            lblVideoTitle.Text = videoInfo.Title;
            if (videoInfo.ChannelOwned == null)
            {
                lblChannelTitle.Text = "Имя канала: Не доступно";
                lblDatePublished.Text = "Дата публикации: Не доступно";
                return;
            }

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
            if (!VideoInfo.IsAvailable)
            {
                MessageBox.Show("Видео недоступно!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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

            YouTubeApiRequestType requestType = config.UseHiddenApiForGettingInfo && !VideoInfo.Dashed ?
                YouTubeApiRequestType.DecryptedUrls : YouTubeApiRequestType.EncryptedUrls;

            bool needToSortDash = VideoInfo.Dashed && config.SortDashFormatsByBitrate;
            ThreadGetDownloadableFormats thr = new ThreadGetDownloadableFormats(
                requestType, config.SortFormatsByFileSize, needToSortDash,
                config.MoveAudioId140First, webPage);
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
            contextMenuDownloads.Items.Clear();

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

            int adaptiveCount = 0;
            for (int i = 0; i < thr.videoFiles.Count; i++)
            {
                if (!thr.videoFiles[i].isContainer)
                {
                    ++adaptiveCount;
                }

                videoFormats.Add(thr.videoFiles[i]);
                ToolStripMenuItem mi = new ToolStripMenuItem(thr.videoFiles[i].ToString());
                mi.Tag = thr.videoFiles[i];
                mi.Click += MenuItemDownloadClick;
                contextMenuDownloads.Items.Add(mi);
            }
            if (thr.audioFiles.Count > 0)
            {
                contextMenuDownloads.Items.Add("-");
                for (int i = 0; i < thr.audioFiles.Count; i++)
                {
                    if (!thr.audioFiles[i].isContainer)
                    {
                        ++adaptiveCount;
                    }

                    audioFormats.Add(thr.audioFiles[i]);
                    ToolStripMenuItem mi = new ToolStripMenuItem(thr.audioFiles[i].ToString());
                    mi.Tag = thr.audioFiles[i];
                    mi.Click += MenuItemDownloadClick;
                    contextMenuDownloads.Items.Add(mi);
                }
            }
            if (adaptiveCount > 0)
            {
                contextMenuDownloads.Items.Add("-");
                ToolStripMenuItem mi = new ToolStripMenuItem("Выбрать форматы...");
                mi.Tag = null;
                mi.Click += MenuItemDownloadClick;
                contextMenuDownloads.Items.Add(mi);
            }

            btnDownload.Enabled = true;

            Point pt = PointToScreen(new Point(btnDownload.Left + btnDownload.Width, btnDownload.Top));                           
            contextMenuDownloads.Show(pt.X, pt.Y);
        }

        private async Task<DownloadResult> DownloadDash(YouTubeMediaFile mediaFile, string formattedFileName)
        {
            progressBarDownload.Value = 0;
            progressBarDownload.Maximum = mediaFile.dashManifestUrls.Count;
            string mediaType = mediaFile is YouTubeVideoFile ? "видео" : "аудио";
            lblStatus.Text = $"Скачивание чанков {mediaType}:";
            lblProgress.Left = lblStatus.Left + lblStatus.Width;
            lblProgress.Text = $"0 / {mediaFile.dashManifestUrls.Count} (0.00%), {mediaFile.GetShortInfo()}";

            bool canMerge = config.MergeToContainer && IsFfmpegAvailable();
            string fnDash = MultiThreadedDownloader.GetNumberedFileName(
                (canMerge ? config.TempDirPath : config.DownloadingDirPath) +
                $"{formattedFileName}_{mediaFile.formatId}.{mediaFile.fileExtension}");
            string fnDashFinal = fnDash;
            string fnDashTmp = fnDash + ".tmp";

            Progress<int> progressDash = new Progress<int>();
            progressDash.ProgressChanged += (s, n) =>
            {
                progressBarDownload.Value = n + 1;
                double percent = 100.0 / progressBarDownload.Maximum * (n + 1);
                lblStatus.Text = $"Скачивание чанков {mediaType}:";
                lblProgress.Left = lblStatus.Left + lblStatus.Width;
                lblProgress.Text = $"{n + 1} / {mediaFile.dashManifestUrls.Count}" +
                    $" ({string.Format("{0:F2}", percent)}%), {mediaFile.GetShortInfo()}";
            };

            return await Task.Run(() =>
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
                        d.Connected += (object s, string url, long contentLength, ref int errCode) =>
                        {
                            if (errCode == 200 || errCode == 206)
                            {
                                if (contentLength > 0L)
                                {
                                    char driveLetter = fnDashTmp[0];
                                    if (driveLetter != '\\')
                                    {
                                        DriveInfo driveInfo = new DriveInfo(driveLetter.ToString());
                                        if (!driveInfo.IsReady)
                                        {
                                            errCode = FileDownloader.DOWNLOAD_ERROR_DRIVE_NOT_READY;
                                            return;
                                        }
                                        long minimumFreeSpaceRequired = contentLength * 10;
                                        if (driveInfo.AvailableFreeSpace <= minimumFreeSpaceRequired)
                                        {
                                            errCode = FileDownloader.DOWNLOAD_ERROR_INSUFFICIENT_DISK_SPACE;
                                            return;
                                        }
                                    }
                                }
                            }
                        };
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
                            return new DownloadResult(MultiThreadedDownloader.DOWNLOAD_ERROR_MERGING_CHUNKS, null, null);
                        }
                    } while (errorCode != 200 && errors++ < 9 && !downloadCancelRequired);
                    if (downloadCancelRequired)
                    {
                        errorCode = FileDownloader.DOWNLOAD_ERROR_CANCELED_BY_USER;
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
                return new DownloadResult(errorCode, d.LastErrorMessage, fnDashFinal);
            }
            );
        }

        private async Task<DownloadResult> DownloadYouTubeMediaFile(
            YouTubeMediaFile mediaFile, string formattedFileName, bool audioOnly)
        {
            if (mediaFile.isDashManifest)
            {
                return await DownloadDash(mediaFile, formattedFileName);
            }
            else
            {
                YouTubeVideoFile videoFile = mediaFile as YouTubeVideoFile;
                bool isVideo = videoFile != null;
                if (isVideo)
                {
                    audioOnly = false;
                }
                string mediaTypeString = isVideo ? "видео" : "аудио";

                #region Расшифровка Cipher
                //TODO: Вынести это в отдельный метод.
                if (mediaFile.isCiphered)
                {
                    if (string.IsNullOrEmpty(config.CipherDecryptionAlgo) || string.IsNullOrWhiteSpace(config.CipherDecryptionAlgo))
                    {
                        return new DownloadResult(ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM, null, null);
                    }

                    #region Расшифровка ссылки на видео-дорожку
                    string cipherDecrypted = DecryptCipherSignature(mediaFile.cipherSignatureEncrypted, config.CipherDecryptionAlgo);

                    if (string.IsNullOrEmpty(cipherDecrypted))
                    {
                        return new DownloadResult(ERROR_CIPHER_DECRYPTION, null, null);
                    }

                    mediaFile.url = $"{mediaFile.cipherUrl}&sig={cipherDecrypted}";

                    if (FileDownloader.GetUrlContentLength(mediaFile.url, out _, out _) != 200)
                    {
                        return new DownloadResult(ERROR_CIPHER_DECRYPTION, null, null);
                    }
                    #endregion

                    #region Расшифровка ссылки на аудио-дорожку
                    if (!mediaFile.isContainer && !isVideo && audioFormats.Count > 0)
                    {
                        YouTubeAudioFile audioFile = audioFormats[0];
                        string audioCipherDecrypted = DecryptCipherSignature(
                            audioFile.cipherSignatureEncrypted, config.CipherDecryptionAlgo);
                        audioFile.url = $"{audioFile.cipherUrl}&sig={audioCipherDecrypted}";
                        if (FileDownloader.GetUrlContentLength(audioFile.url, out _, out _) != 200)
                        {
                            return new DownloadResult(ERROR_CIPHER_DECRYPTION, null, null);
                        }
                    }
                    #endregion
                }
                #endregion

                bool useRamToStoreTemporaryFiles = config.UseRamToStoreTemporaryFiles;
                MultiThreadedDownloader downloader = new MultiThreadedDownloader();
                downloader.ThreadCount = isVideo ? config.ThreadCountVideo : config.ThreadCountAudio;
                downloader.Url = mediaFile.url;
                if (!useRamToStoreTemporaryFiles)
                {
                    downloader.TempDirectory = config.TempDirPath;
                }
                downloader.UseRamForTempFiles = useRamToStoreTemporaryFiles;

                string destFilePath;
                if (isVideo && mediaFile.isContainer)
                {
                    destFilePath = MultiThreadedDownloader.GetNumberedFileName(
                        $"{config.DownloadingDirPath}{formattedFileName}.{mediaFile.mimeExt}");
                    downloader.KeepDownloadedFileInTempOrMergingDirectory = false;
                }
                else
                {
                    if (!audioOnly && config.MergeToContainer && IsFfmpegAvailable())
                    {
                        downloader.MergingDirectory = DecideMergingDirectory();
                        downloader.KeepDownloadedFileInTempOrMergingDirectory = true;
                    }
                    else
                    {
                        downloader.MergingDirectory = config.DownloadingDirPath;
                    }
                    destFilePath = MultiThreadedDownloader.GetNumberedFileName(config.DownloadingDirPath +
                        $"{formattedFileName}_{mediaFile.formatId}.{mediaFile.fileExtension}");
                }
                downloader.OutputFileName = destFilePath;

                downloader.Connecting += (s, url) =>
                {
                    string shortInfo = isVideo ? videoFile.GetShortInfo() : (mediaFile as YouTubeAudioFile).GetShortInfo();
                    lblStatus.Text = $"Состояние: Подключение... {shortInfo}";
                    lblProgress.Text = null;
                    Application.DoEvents();
                };
                downloader.Connected += (object s, string url, long contentLength, ref int errCode, ref string errorMessage) =>
                {
                    if (errCode == 200 || errCode == 206)
                    {
                        string shortInfo = isVideo ? videoFile.GetShortInfo() : (mediaFile as YouTubeAudioFile).GetShortInfo();
                        lblStatus.Text = $"Состояние: Подключено! {shortInfo}";
                        lblStatus.Refresh();

                        if (contentLength > 0L)
                        {
                            long minimumFreeSpaceRequired = (long)(contentLength * 1.1);

                            MultiThreadedDownloader mtd = s as MultiThreadedDownloader;

                            List<char> driveLetters = mtd.GetUsedDriveLetters();
                            if (driveLetters.Count > 0 && !IsEnoughDiskSpace(driveLetters, minimumFreeSpaceRequired))
                            {
                                errorMessage = "Недостаточно места на диске!";
                                errCode = MultiThreadedDownloader.DOWNLOAD_ERROR_CUSTOM;
                                return;
                            }

                            if (mtd.UseRamForTempFiles && MemoryWatcher.Update() &&
                                MemoryWatcher.RamFree < (ulong)minimumFreeSpaceRequired)
                            {
                                errorMessage = "Недостаточно памяти!";
                                errCode = MultiThreadedDownloader.DOWNLOAD_ERROR_CUSTOM;
                                return;
                            }
                        }
                    }
                };
                downloader.DownloadStarted += (s, size) =>
                {
                    progressBarDownload.Value = 0;
                    progressBarDownload.Maximum = 100;

                    lblStatus.Text = $"Скачивание {mediaTypeString}:";
                    string shortInfo = isVideo ? videoFile.GetShortInfo() : (mediaFile as YouTubeAudioFile).GetShortInfo();
                    lblProgress.Text = $"0 / {FormatSize(size)} (0.00%), {shortInfo}";
                    lblProgress.Left = lblStatus.Left + lblStatus.Width;
                };
                downloader.DownloadProgress += (object s, long bytesTransfered) =>
                {
                    long fileSize = downloader.ContentLength != 0L ? downloader.ContentLength : videoFile.contentLength;
                    double percent = 100.0 / fileSize * bytesTransfered;
                    progressBarDownload.Value = (int)Math.Round(percent);

                    string percentString = string.Format("{0:F2}", percent);
                    string shortInfo = isVideo ? videoFile.GetShortInfo() : (mediaFile as YouTubeAudioFile).GetShortInfo();
                    lblProgress.Text = $"{FormatSize(bytesTransfered)} / {FormatSize(fileSize)}" +
                        $" ({percentString}%), {shortInfo}";
                };
                downloader.CancelTest += (object s, ref bool cancel) =>
                {
                    cancel = downloadCancelRequired;
                };
                downloader.MergingStarted += (s, chunkCount) =>
                {
                    progressBarDownload.Value = 0;
                    progressBarDownload.Maximum = chunkCount;

                    lblStatus.Text = $"Объединение чанков {mediaTypeString}:";
                    lblProgress.Text = $"0 / {chunkCount}";
                    lblProgress.Left = lblStatus.Left + lblStatus.Width;
                };
                downloader.MergingProgress += (s, chunkId) =>
                {
                    lblProgress.Text = $"{chunkId + 1} / {downloader.ThreadCount}";
                    progressBarDownload.Value = chunkId + 1;
                };
                int res = await downloader.Download();
                if (useRamToStoreTemporaryFiles)
                {
                    GC.Collect();
                }
                return new DownloadResult(res, downloader.LastErrorMessage, downloader.OutputFileName);
            }
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
            if (!config.UseRamToStoreTemporaryFiles)
            {
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
            lblStatus.Text = null;
            lblProgress.Text = null;

            string formattedFileName = FixFileName(FormatFileName(config.OutputFileNameFormat, VideoInfo));

            List<YouTubeMediaFile> tracksToDownload = new List<YouTubeMediaFile>();

            ToolStripMenuItem mi = sender as ToolStripMenuItem;

            if (mi.Tag == null)
            {
                lblStatus.Text = "Состояние: Выбор форматов...";
                List<YouTubeMediaFile> formats = new List<YouTubeMediaFile>();
                formats.AddRange(videoFormats);
                formats.AddRange(audioFormats);
                FormTracksSelector tracksSelector = new FormTracksSelector(formats);
                DialogResult dialogResult = tracksSelector.ShowDialog();
                lblStatus.Text = null;
                if (dialogResult == DialogResult.OK)
                {
                    foreach (YouTubeMediaFile mediaFile in tracksSelector.SelectedTracks)
                    {
                        tracksToDownload.Add(mediaFile);
                    }
                }
                else
                {
                    MessageBox.Show("Скачивание отменено!", "Отменный отменятор отменения отмены",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    downloading = false;
                    btnDownload.Enabled = true;
                    return;
                }
            }
            else if (mi.Tag is YouTubeVideoFile)
            {
                YouTubeVideoFile videoFile = mi.Tag as YouTubeVideoFile;
                if (videoFile.isHlsManifest)
                {
                    if (IsFfmpegAvailable())
                    {
                        lblStatus.Text = "Состояние: Запуск FFMPEG...";
                        lblStatus.Refresh();

                        string filePath = MultiThreadedDownloader.GetNumberedFileName(
                            $"{config.DownloadingDirPath}{formattedFileName}.ts");
                        GrabHls(videoFile.url, filePath);
                        lblStatus.Text = null;
                    }
                    else
                    {
                        lblStatus.Text = "Состояние: Не удалось запустить FFMPEG!";
                        string ffmpegMsg = "Не удалось запустить FFMPEG!";
                        MessageBox.Show(ffmpegMsg, "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    downloading = false;
                    btnDownload.Enabled = true;
                    return;
                }

                if (config.DownloadAllAdaptiveVideoTracks)
                {
                    foreach (YouTubeMediaFile videoFormat in videoFormats)
                    {
                        if (videoFormat is YouTubeVideoFile && !videoFormat.isContainer && !videoFormat.isHlsManifest)
                        {
                            tracksToDownload.Add(videoFormat);
                        }
                    }
                }
                else
                {
                    tracksToDownload.Add(videoFile);
                }

                if (audioFormats.Count > 0)
                {
                    if (config.DownloadAllAudioTracks)
                    {
                        foreach (YouTubeMediaFile audioFormat in audioFormats)
                        {
                            if (audioFormat is YouTubeAudioFile)
                            {
                                tracksToDownload.Add(audioFormat);
                            }
                        }
                    }
                    else
                    {
                        if (config.DownloadFirstAudioTrack)
                        {
                            tracksToDownload.Add(audioFormats[0]);
                        }
                        if (config.DownloadSecondAudioTrack && audioFormats.Count > 1)
                        {
                            if (config.IfOnlySecondAudioTrackIsBetter)
                            {
                                if (audioFormats[1].contentLength > audioFormats[0].contentLength)
                                {
                                    tracksToDownload.Add(audioFormats[1]);
                                }
                                else if (!config.DownloadFirstAudioTrack)
                                {
                                    tracksToDownload.Add(audioFormats[0]);
                                }
                            }
                            else
                            {
                                tracksToDownload.Add(audioFormats[1]);
                            }
                        }
                    }
                }
            }
            else if (mi.Tag is YouTubeAudioFile)
            {
                tracksToDownload.Add(mi.Tag as YouTubeAudioFile);
            }

            //Подсчёт общего размера файлов.
            long summaryFilesSize = 0L;
            foreach (YouTubeMediaFile ytmf in tracksToDownload)
            {
                if (ytmf.contentLength > 0L)
                {
                    summaryFilesSize += ytmf.contentLength;
                }
            }

            long minimumFreeSpaceRequired = summaryFilesSize > 0L ? (long)(summaryFilesSize * 1.1) : 0L;
            if (minimumFreeSpaceRequired > 0L)
            {
                MultiThreadedDownloader tempDownloader = new MultiThreadedDownloader();
                tempDownloader.OutputFileName = Path.Combine(config.DownloadingDirPath, "temp.tmp");
                tempDownloader.TempDirectory = config.TempDirPath;
                tempDownloader.MergingDirectory = config.ChunksMergingDirPath;

                List<char> driveLetters = tempDownloader.GetUsedDriveLetters();
                if (driveLetters.Count > 0 && !IsEnoughDiskSpace(driveLetters, minimumFreeSpaceRequired))
                {
                    lblStatus.Text = "Состояние: Ошибка: Недостаточно места на диске!";
                    MessageBox.Show($"{VideoInfo.Title}\nНедостаточно места на диске!", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    downloading = false;
                    btnDownload.Enabled = true;
                    return;
                }

                if (tracksToDownload.Count > 1 && !tracksToDownload[0].isContainer &&
                    !AdvancedFreeSpaceCheck(summaryFilesSize))
                {
                    lblStatus.Text = "Состояние: Ошибка: Недостаточно места на диске!";
                    MessageBox.Show("Недостаточно места на диске!", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    downloading = false;
                    btnDownload.Enabled = true;
                    return;
                }
            }

            bool needToMerge = tracksToDownload.Count > 0 && !tracksToDownload[0].isContainer;
            if (needToMerge)
            {
                bool stop = false;
                if (config.MergeToContainer && !IsFfmpegAvailable())
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
            }

            btnDownload.Text = "Отмена";
            btnDownload.Enabled = true;

            lblStatus.Text = "Скачивание...";
            lblStatus.Refresh();

            List<DownloadResult> downloadResults = new List<DownloadResult>();
            bool audioOnly = IsAudioOnly(tracksToDownload);
            int errorCode;
            int counter = 0;
            do
            {
                DownloadResult downloadResult = await DownloadYouTubeMediaFile(
                    tracksToDownload[counter], formattedFileName, audioOnly);
                if (downloadResult == null)
                {
                    errorCode = 400;
                    break;
                }
                else
                {
                    errorCode = downloadResult.ErrorCode;
                    downloadResults.Add(downloadResult);
                }
                counter++;
            }
            while (errorCode == 200 && counter < tracksToDownload.Count);

            lblProgress.Text = null;
            lblProgress.Refresh();

            if (errorCode == 200)
            {
                if (config.MergeToContainer && needToMerge && !audioOnly && IsFfmpegAvailable())
                {
                    btnDownload.Enabled = false;
                    if (minimumFreeSpaceRequired > 0L)
                    {
                        DriveInfo driveInfo = new DriveInfo(config.DownloadingDirPath[0].ToString());
                        if (driveInfo.AvailableFreeSpace < minimumFreeSpaceRequired)
                        {
                            lblStatus.Text = "Состояние: Ошибка: Недостаточно места на диске!";
                            string dir = config.ChunksMergingDirPath;
                            string msg = "Недостаточно места на диске для сборки контейнера! " +
                                $"Оригинальные файлы сохранены в папку\n{dir}";
                            MessageBox.Show(msg, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            downloading = false;
                            btnDownload.Text = "Скачать";
                            btnDownload.Enabled = true;
                            return;
                        }
                    }

                    string ext = "mkv";
                    if (!config.AlwaysUseMkvContainer)
                    {
                        ext = "mp4";
                        foreach (YouTubeMediaFile mediaFile in tracksToDownload)
                        {
                            if (mediaFile.mimeExt != "mp4")
                            {
                                ext = "mkv";
                                break;
                            }
                        }
                    }

                    lblStatus.Text = $"Состояние: Создание контейнера {ext.ToUpper()}...";
                    lblStatus.Refresh();

                    string containerFilePath = MultiThreadedDownloader.GetNumberedFileName(
                        $"{config.DownloadingDirPath}{formattedFileName}.{ext}");
                    await MergeYouTubeMediaTracks(downloadResults, containerFilePath);

                    if (config.DeleteSourceFiles)
                    {
                        foreach (DownloadResult downloadResult in downloadResults)
                        {
                            if (File.Exists(downloadResult.FileName))
                            {
                                File.Delete(downloadResult.FileName);
                            }
                        }
                    }
                }

                //сохранение картинки
                if (config.SavePreviewImage && VideoInfo.ImageData != null)
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
                switch (errorCode)
                {
                    case FileDownloader.DOWNLOAD_ERROR_CANCELED_BY_USER:
                        lblStatus.Text = "Состояние: Скачивание отменено";
                        MessageBox.Show($"{VideoInfo.Title}\nСкачивание успешно отменено!", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case FileDownloader.DOWNLOAD_ERROR_INSUFFICIENT_DISK_SPACE:
                        lblStatus.Text = "Состояние: Ошибка: Недостаточно места на диске!";
                        MessageBox.Show($"{VideoInfo.Title}\n" +
                            "Недостаточно места на диске!", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case ERROR_CIPHER_DECRYPTION:
                        lblStatus.Text = "Состояние: Ошибка ERROR_CIPHER_DECRYPTION";
                        MessageBox.Show($"{VideoInfo.Title}\n" +
                            "Ошибка расшифровки ссылки! Попробуйте ещё раз.", "Ошибка!",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                    case ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM:
                        {
                            lblStatus.Text = "Состояние: Ошибка ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM";
                            string t = "Ссылка на это видео, зачем-то, зашифрована алгоритмом \"Cipher\", " +
                                "для расшифровки которого вам требуется ввести специальную последовательность чисел, " +
                                "известную одному лишь дьяволу.\n" +
                                "Или включите галочку \"Использовать скрытое API для получения информации о видео\".";
                            MessageBox.Show($"{VideoInfo.Title}\nОшибка ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM\n{t}", "Ошибка!",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                    default:
                        {
                            string errorMessage = $"{VideoInfo.Title}\nСкачивание прервано!";
                            DownloadResult dr = downloadResults[downloadResults.Count - 1];
                            if (!string.IsNullOrEmpty(dr.ErrorMessage))
                            {
                                errorMessage += $"\n{dr.ErrorMessage}";
                                lblStatus.Text = $"Состояние: Ошибка {dr.ErrorMessage}";
                            }
                            else
                            {
                                lblStatus.Text = $"Состояние: Ошибка {dr.ErrorCode}";
                            }
                            MessageBox.Show(errorMessage, "Ошибка!",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                }
            }

            downloading = false;
            btnDownload.Text = "Скачать";
            btnDownload.Enabled = true;
        }

        private bool IsAudioOnly(IEnumerable<YouTubeMediaFile> mediaFiles)
        {
            foreach (YouTubeMediaFile mediaFile in mediaFiles)
            {
                if (!(mediaFile is YouTubeAudioFile))
                {
                    return false;
                }
            }
            return true;
        }

        private void SaveImageToFile(string formattedFileName)
        {
            string imageFileName = VideoInfo.Image != null ?
                $"_image_{VideoInfo.Image.Width}x{VideoInfo.Image.Height}.jpg" : "_image.bin";
            string filePath = $"{config.DownloadingDirPath}{formattedFileName}{imageFileName}";
            VideoInfo.ImageData.SaveToFile(filePath);
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
            if (!VideoInfo.IsAvailable)
            {
                MessageBox.Show("Видео недоступно!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrEmpty(webPage))
            {
                string t = ExtractVideoInfoFromWebPage(webPage);
                SetClipboardText(t);
                MessageBox.Show("Скопировано в буфер обмена");
                return;
            }
            int errorCode = GetYouTubeVideoInfoEx(VideoInfo.Id, out string info, config.UseHiddenApiForGettingInfo);
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
            if (!VideoInfo.IsAvailable)
            {
                MessageBox.Show("Видео недоступно!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGetWebPage.Enabled = true;
                return;
            }

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
            if (!VideoInfo.IsAvailable)
            {
                MessageBox.Show("Видео недоступно!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int errorCode = GetYouTubeVideoInfoEx(VideoInfo.Id, out string info, config.UseHiddenApiForGettingInfo);
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
            if (!VideoInfo.IsAvailable)
            {
                MessageBox.Show("Видео недоступно!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int errorCode = GetYouTubeVideoInfoEx(VideoInfo.Id, out string info, config.UseHiddenApiForGettingInfo);
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

        private void btnGetPlayerCode_Click(object sender, EventArgs e)
        {
            if (!VideoInfo.IsAvailable)
            {
                MessageBox.Show("Видео недоступно!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string page = webPage;
            if (string.IsNullOrEmpty(page) || string.IsNullOrWhiteSpace(page))
            {
                if (GetYouTubeVideoWebPage(VideoInfo.Id, out page) != 200)
                {
                    MessageBox.Show("Ошибка скачивания плеера!", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            string url = ExtractPlayerUrlFromWebPage(page);
            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
            {
                MessageBox.Show("Ошибка скачивания плеера!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FileDownloader d = new FileDownloader() { Url = url };
            int errorCode = d.DownloadString(out string code);
            if (errorCode != 200)
            {
                MessageBox.Show("Ошибка скачивания плеера!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.InitialDirectory = config.DownloadingDirPath;
            sfd.Title = "Сохранить плеер как...";
            sfd.Filter = "JS-files|*.js";
            sfd.DefaultExt = ".js";
            sfd.AddExtension = true;
            sfd.FileName = $"Player for {VideoInfo.Id}";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    File.Delete(sfd.FileName);
                }
                File.WriteAllText(sfd.FileName, code);
            }
            sfd.Dispose();
        }

        private void imagePreview_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
            if (VideoInfo.IsAvailable && e.Button == MouseButtons.Right)
            {
                contextMenuImage.Show(Cursor.Position);
            }
        }

        private void lblChannelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
            if (e.Button == MouseButtons.Right)
            {
                contextMenuChannelTitle.Show(Cursor.Position);
            }
        }

        private void lblVideoTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
            if (VideoInfo.IsAvailable && e.Button == MouseButtons.Right)
            {
                contextMenuVideoTitle.Show(Cursor.Position);
            }
        }

        private void imageFavorite_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
            if (VideoInfo.IsAvailable && e.Button == MouseButtons.Left)
            {
                SetFavoriteVideo(!favoriteVideo);
            }
        }

        private void imageFavoriteChannel_MouseDown(object sender, MouseEventArgs e)
        {
            Activated?.Invoke(this);
            if (VideoInfo.IsAvailable)
            {
                if (FavoriteChannelChanged != null)
                {
                    FavoriteChannelChanged.Invoke(this, VideoInfo.ChannelOwned.Id, !FavoriteChannel);
                }
                else
                {
                    FavoriteChannel = !favoriteChannel;
                }
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
            if (e.Button == MouseButtons.Left)
            {
                oldX = e.X;
                canDrag = true;
            }
        }

        private void imgScrollbar_MouseUp(object sender, MouseEventArgs e)
        {
            canDrag = false;
        }

        private void imgScrollbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (canDrag)
            {
                int newX = Left + oldX - e.X;
                if (newX > 0)
                {
                    newX = 0;
                }
                else if (newX < -EXTRA_WIDTH)
                {
                    newX = -EXTRA_WIDTH;
                }
                Left = newX;
                oldX = e.X;
                imgScrollbar.Left = -Left;
                imgScrollbar.Invalidate();
            }
        }

        public void SetVideoTitleFontSize(int fontSize)
        {
            lblVideoTitle.Font = new Font(lblVideoTitle.Font.FontFamily, fontSize);
        }

        public void SetMenusFontSize(int fontSize)
        {
            contextMenuImage.SetFontSize(fontSize);
            contextMenuVideoTitle.SetFontSize(fontSize);
            contextMenuChannelTitle.SetFontSize(fontSize);
            contextMenuDownloads.SetFontSize(fontSize);
        }

        private void miCopyVideoUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetClipboardText(YOUTUBE_VIDEO_URL_BASE + VideoInfo.Id);
        }

        private void miCopyChannelTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetClipboardText(VideoInfo.ChannelOwned.Title);
        }

        private void miCopyChannelIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VideoInfo == null || VideoInfo.ChannelOwned == null)
            {
                string msg = "Произошла ошибация! По-этому, в настоящее время " +
                    "данное действие совершить невозможно! Сорян, бро!";
                MessageBox.Show(msg, "Ошибатор ошибок",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SetClipboardText(VideoInfo.ChannelOwned.Id);
        }

        private void miOpenChannelInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenUrlInBrowser(string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, VideoInfo.ChannelOwned.Id));
        }

        private void miOpenVideoInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenUrlInBrowser(YOUTUBE_VIDEO_URL_BASE + VideoInfo.Id);
        }

        private void lblChannelTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OpenChannel?.Invoke(this, VideoInfo.ChannelOwned.Title, VideoInfo.ChannelOwned.Id);
        }

        private void miSaveImageAssToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VideoInfo.ImageData != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить изображение";
                sfd.Filter = "jpg|*.jpg";
                sfd.DefaultExt = ".jpg";
                sfd.AddExtension = true;
                sfd.InitialDirectory = string.IsNullOrEmpty(config.DownloadingDirPath) ? config.SelfDirPath : config.DownloadingDirPath;
                string imageFileName = VideoInfo.Image != null ? $"_image_{VideoInfo.Image.Width}x{VideoInfo.Image.Height}" : "_image";
                string filePath = FixFileName(FormatFileName(config.OutputFileNameFormat, VideoInfo)) + imageFileName;
                sfd.FileName = filePath;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    VideoInfo.ImageData.SaveToFile(sfd.FileName);
                }
                sfd.Dispose();
            }
        }

        private void miCopyChannelNameWithIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetClipboardText($"{VideoInfo.ChannelOwned.Title} [{VideoInfo.ChannelOwned.Id}]");
        }

        private void miCopyTitleAsIsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetClipboardText(VideoInfo.Title);
        }

        private void miCopyFixedTitleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string t = FixFileName(VideoInfo.Title);
            SetClipboardText(t);
        }

        private void miOpenImageInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VideoInfo.ImageUrls.Count > 0)
            {
                string url = VideoInfo.ImageUrls[0];
                OpenUrlInBrowser(url);
            }
        }

        private void miCopyChannelUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string url = string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, VideoInfo.ChannelOwned.Id);
            SetClipboardText(url);
        }

        private void miCopyImageUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VideoInfo.ImageUrls == null || VideoInfo.ImageUrls.Count == 0 || string.IsNullOrEmpty(VideoInfo.ImageUrls[0]))
            {
                MessageBox.Show("Ссылка на изображение отсутствует!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SetClipboardText(VideoInfo.ImageUrls[0]);
        }
    }
}
