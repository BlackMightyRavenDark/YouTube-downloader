using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouTubeApiLib;
using MultiThreadedDownloaderLib;
using static YouTube_downloader.Utils;
using YouTube_downloader.Properties;

namespace YouTube_downloader
{
	public partial class FrameYouTubeVideo : UserControl
	{
		private MultiThreadedDownloader _multiThreadedDownloader;
		public YouTubeVideo VideoInfo { get; private set; }
		private Stream _videoImageData = null;
		private Image _videoImage = null;
		private bool _ciphered;
		private bool _isFavoriteVideo = false;
		private bool _isFavoriteChannel = false;
		public string _webPage = null;
		public bool IsFavoriteVideo { get { return _isFavoriteVideo; } set { SetFavoriteVideo(value); } }
		public bool IsFavoriteChannel { get { return _isFavoriteChannel; } set { SetFavoriteChannel(value); } }

		public bool IsDownloadInProgress { get; private set; }

		private List<YouTubeMediaTrackAudio> audioFormats = new List<YouTubeMediaTrackAudio>();
		private List<YouTubeMediaTrackVideo> videoFormats = new List<YouTubeMediaTrackVideo>();
		private List<YouTubeMediaTrackContainer> containerFormats = new List<YouTubeMediaTrackContainer>();

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
		public const int EXTRA_WIDTH = 260;
		private bool _dashCancelRequired;

		public FrameYouTubeVideo(YouTubeVideo videoInfo, Control parent)
		{
			InitializeComponent();

			if (parent != null)
			{
				Parent = parent;
			}

			SetVideoTitleFontSize(config.VideoTitleFontSize);
			imgScrollbar.SetDoubleBuffered(true);

			SetVideoInfo(videoInfo);
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
				groupBoxButtons.Left = parentControl.Width + offset;

				imgScrollbar.Left = 0;
				imgScrollbar.Width = parentControl.Width;
				imgScrollbar.Invalidate();
			}
		}

		private void SetVideoInfo(YouTubeVideo videoInfo)
		{
			VideoInfo = videoInfo;

			if (!videoInfo.IsInfoAvailable)
			{
				lblChannelTitle.Text = "Имя канала: Недоступно";
				lblDatePublished.Text = "Дата публикации: Недоступно";
				lblVideoTitle.Text = $"{videoInfo.Status.Status}, {videoInfo.Status.Reason}";
				_videoImageData = new MemoryStream();
				if (DownloadData(videoInfo.Status.ThumbnailUrl, _videoImageData) == 200)
				{
					_videoImage = Image.FromStream(_videoImageData);
				}
				else
				{
					_videoImageData.Dispose();
					_videoImageData = null;
					_videoImage = null;
				}
				return;
			}

			lblVideoTitle.Text = videoInfo.Title;
			if (string.IsNullOrEmpty(videoInfo.OwnerChannelId) || string.IsNullOrWhiteSpace(videoInfo.OwnerChannelId) ||
				string.IsNullOrEmpty(videoInfo.OwnerChannelTitle) || string.IsNullOrWhiteSpace(videoInfo.OwnerChannelTitle))
			{
				lblChannelTitle.Text = "Имя канала: Недоступно";
				lblDatePublished.Text = "Дата публикации: Недоступно";
			}

			lblChannelTitle.Text = videoInfo.OwnerChannelTitle;
			string datePublishedString = "Недоступно";
			if (IsVideoDateAvailable(videoInfo))
			{
				DateTime date = config.UseGmtTime ?
					videoInfo.DatePublished.ToGmt() : videoInfo.DatePublished;
				datePublishedString = date.ToString("yyyy.MM.dd, HH:mm:ss");
				if (config.UseGmtTime) { datePublishedString += " GMT"; }
			}
			lblDatePublished.Text = $"Дата публикации: {datePublishedString}";
			FavoriteItem favoriteItem = new FavoriteItem(
				videoInfo.Title, videoInfo.Title, videoInfo.Id,
				videoInfo.OwnerChannelTitle, videoInfo.OwnerChannelId, null);
			_isFavoriteVideo = FindInFavorites(favoriteItem, favoritesRootNode) != null;

			favoriteItem.DisplayName = VideoInfo.Title;
			favoriteItem.ID = VideoInfo.OwnerChannelId;
			_isFavoriteChannel = FindInFavorites(favoriteItem, favoritesRootNode) != null;
			_ciphered = VideoInfo.IsCiphered();
			_videoImageData = videoInfo.DownloadPreviewImage();
			_videoImage = _videoImageData != null && _videoImageData.Length > 0L ? Image.FromStream(_videoImageData) : null;
			imagePreview.Refresh();
		}

		public void SetFavoriteVideo(bool fav)
		{
			_isFavoriteVideo = fav;
			if (_isFavoriteVideo)
			{   
				FavoriteItem favoriteItem = new FavoriteItem(
					VideoInfo.Title, VideoInfo.Title, VideoInfo.Id,
					VideoInfo.OwnerChannelTitle, VideoInfo.OwnerChannelId, favoritesRootNode);
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
			_isFavoriteChannel = fav;
			if (_isFavoriteChannel)
			{
				if (FindInFavorites(VideoInfo.OwnerChannelId) == null)
				{
					FavoriteItem favoriteItem = new FavoriteItem(
						VideoInfo.OwnerChannelTitle, VideoInfo.OwnerChannelTitle, VideoInfo.OwnerChannelId,
						null, null, favoritesRootNode);
					favoriteItem.ItemType = FavoriteItemType.Channel;
					favoritesRootNode.Children.Add(favoriteItem);
					treeFavorites.RefreshObject(favoriteItem.Parent);
				}
			}
			else
			{
				FavoriteItem favoriteItem = FindInFavorites(VideoInfo.OwnerChannelId);
				if (favoriteItem != null)
				{
					treeFavorites.RemoveObject(favoriteItem);
					favoriteItem.Parent.Children.Remove(favoriteItem);
					treeFavorites.RefreshObject(favoriteItem.Parent);
				}
			}
			imageFavoriteChannel.Invalidate();
		}

		private async void btnDownload_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (IsDownloadInProgress)
			{
				btnDownload.Enabled = false;
				StopDownload();
				return;
			}

			btnDownload.Enabled = false;
			BtnDownloadClicked?.Invoke(this, e);

			if (progressBarDownload.Value < progressBarDownload.Maximum)
			{
				progressBarDownload.Value = 0;
			}

			lblStatus.Text = "Состояние: Определение доступных форматов...";

			videoFormats.Clear();
			containerFormats.Clear();
			audioFormats.Clear();
			contextMenuDownloads.Items.Clear();

			LinkedList<YouTubeMediaTrack> mediaTracks = null;
			if (VideoInfo.RawInfo.DataGettingMethod != YouTubeApiLib.Utils.VideoInfoGettingMethod.Manual)
			{
				bool useHiddenApi = config.UseHiddenApiForGettingInfo;
				await Task.Run(() =>
				{
					if (useHiddenApi)
					{
						YouTubeApi api = new YouTubeApi();
						RawVideoInfoResult rawVideoInfoResult =
							api.GetRawVideoInfo(new VideoId(VideoInfo.Id),
							YouTubeApiLib.Utils.VideoInfoGettingMethod.HiddenApiDecryptedUrls);
						if (rawVideoInfoResult.ErrorCode == 200)
						{
							mediaTracks = rawVideoInfoResult.RawVideoInfo.StreamingData?.Parse();
						}
					}
					else
					{
						YouTubeVideo video = GetSingleVideo(new VideoId(VideoInfo.Id));
						if (video != null)
						{
							if (!YouTubeApi.getMediaTracksInfoImmediately)
							{
								YouTubeApiLib.Utils.VideoInfoGettingMethod method = useHiddenApi ?
									YouTubeApiLib.Utils.VideoInfoGettingMethod.HiddenApiDecryptedUrls :
									YouTubeApiLib.Utils.VideoInfoGettingMethod.WebPage;
								video.UpdateMediaFormats(method);
							}
							mediaTracks = video.MediaTracks;
						}
					}
					//TODO: Исправить ошибку, которая возникает если текущее видео было найдено поиском.
					//VideoInfo.UpdateMediaFormats());
				});
			}
			else
			{
				mediaTracks = VideoInfo.MediaTracks;
			}

			if (mediaTracks == null || mediaTracks.Count == 0)
			{
				string t = "Ссылки для скачивания не найдены!";
				lblStatus.Text = $"Состояние: Ошибка! {t}";
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

			videoFormats = FilterVideoTracks(mediaTracks);
			audioFormats = FilterAudioTracks(mediaTracks);
			if (VideoInfo.IsDashed)
			{
				if (config.SortDashFormatsByBitrate)
				{
					int SorterFunc(YouTubeMediaTrack x, YouTubeMediaTrack y)
					{
						if (x == null || x.IsHlsManifest || x.AverageBitrate <= 0 ||
							y == null || y.IsHlsManifest || y.AverageBitrate <= 0)
						{
							return 0;
						}
						return x.AverageBitrate < y.AverageBitrate ? 1 : -1;
					}

					videoFormats.Sort(SorterFunc);
					audioFormats.Sort(SorterFunc);
				}
			}
			else
			{
				if (config.SortFormatsByFileSize)
				{
					int SorterFunc(YouTubeMediaTrack x, YouTubeMediaTrack y)
					{
						if (x == null || x.IsHlsManifest || x.ContentLength <= 0L ||
							y == null || y.IsHlsManifest || y.ContentLength <= 0L ||
							x.ContentLength == y.ContentLength)
						{
							return 0;
						}
						return x.ContentLength < y.ContentLength ? 1 : -1;
					}

					videoFormats.Sort(SorterFunc);
					audioFormats.Sort(SorterFunc);
				}
			}

			if (config.MoveAudioId140First)
			{
				for (int i = 0; i < audioFormats.Count; ++i)
				{
					if (audioFormats[i].FormatId == 140)
					{
						if (i != 0)
						{
							(audioFormats[i], audioFormats[0]) = (audioFormats[0], audioFormats[i]);
						}

						break;
					}
				}
			}

			List<TableRow> tableRows = new List<TableRow>();
			List<TableColumn> tableColumns = new List<TableColumn>()
			{
				new TableColumn(TableColumnAlignment.Left),
				new TableColumn(TableColumnAlignment.Left),
				new TableColumn(TableColumnAlignment.Right),
				new TableColumn(TableColumnAlignment.Right),
				new TableColumn(TableColumnAlignment.Right),
				new TableColumn(TableColumnAlignment.Left),
				new TableColumn(TableColumnAlignment.Left),
				new TableColumn(TableColumnAlignment.Right),
				new TableColumn(TableColumnAlignment.Right)
			};

			foreach (YouTubeMediaTrackVideo trackVideo in videoFormats)
			{
				tableRows.Add(trackVideo.ToTableRow());
			}

			containerFormats = FilterContainerTracks(mediaTracks);
			foreach (YouTubeMediaTrackContainer trackContainer in containerFormats)
			{
				tableRows.Add(trackContainer.ToTableRow());
			}

			foreach (YouTubeMediaTrackAudio trackAudio in audioFormats)
			{
				tableRows.Add(trackAudio.ToTableRow());
			}

			Table table = new Table(tableRows, tableColumns);
			table.Format();

			List<YouTubeMediaTrackVideo> videos = table.Rows.Where(
				o => o.Tag is YouTubeMediaTrackVideo).Select(o => o.Tag as YouTubeMediaTrackVideo).ToList();
			List<YouTubeMediaTrackContainer> containers = table.Rows.Where(
				o => o.Tag is YouTubeMediaTrackContainer).Select(o => o.Tag as YouTubeMediaTrackContainer).ToList();
			List<YouTubeMediaTrackAudio> audios = table.Rows.Where(
				o => o.Tag is YouTubeMediaTrackAudio).Select(o => o.Tag as YouTubeMediaTrackAudio).ToList();

			const string columnSeparator = " | ";
			int tableRowId = 0;
			foreach (YouTubeMediaTrackVideo trackVideo in videos)
			{
				string title = table.Rows[tableRowId].Join(columnSeparator);
				ToolStripMenuItem mi = new ToolStripMenuItem(title);
				mi.Tag = trackVideo;
				mi.Click += MenuItemDownloadClick;
				contextMenuDownloads.Items.Add(mi);
				tableRowId++;
			}
			if (containers.Count > 0)
			{
				contextMenuDownloads.Items.Add("-");
				foreach (YouTubeMediaTrackContainer trackContainer in containers)
				{
					string title = table.Rows[tableRowId].Join(columnSeparator);
					ToolStripMenuItem mi = new ToolStripMenuItem(title);
					mi.Tag = trackContainer;
					mi.Click += MenuItemDownloadClick;
					contextMenuDownloads.Items.Add(mi);
					tableRowId++;
				}
			}
			if (audios.Count > 0)
			{
				contextMenuDownloads.Items.Add("-");
				foreach (YouTubeMediaTrackAudio trackAudio in audios)
				{
					string title = table.Rows[tableRowId].Join(columnSeparator);
					ToolStripMenuItem mi = new ToolStripMenuItem(title);
					mi.Tag = trackAudio;
					mi.Click += MenuItemDownloadClick;
					contextMenuDownloads.Items.Add(mi);
					tableRowId++;
				}
			}

			if (videoFormats.Count + audioFormats.Count > 0)
			{
				contextMenuDownloads.Items.Add("-");
				ToolStripMenuItem mi = new ToolStripMenuItem("Выбрать форматы...");
				mi.Tag = null;
				mi.Click += MenuItemDownloadClick;
				contextMenuDownloads.Items.Add(mi);
			}

			lblStatus.Text = null;

			Point pt = PointToScreen(new Point(btnDownload.Left + btnDownload.Width, btnDownload.Top));
			contextMenuDownloads.Show(pt.X, pt.Y);

			btnDownload.Enabled = true;
		}

		/// <summary>
		/// Downloads a DASH video. Warning! This method must be run in a separate thread!
		/// </summary>
		private DownloadResult DownloadDash(YouTubeMediaTrack mediaTrack, string formattedFileName)
		{
			string mediaType = null;
			Invoke(new MethodInvoker(() =>
			{
				progressBarDownload.Value = 0;
				progressBarDownload.Maximum = mediaTrack.DashUrls.Count;
				mediaType = mediaTrack is YouTubeMediaTrackAudio ? "аудио" : "видео";
				lblStatus.Text = $"Скачивание чанков {mediaType}:";
				lblProgress.Left = lblStatus.Left + lblStatus.Width;
				lblProgress.Text = $"0 / {mediaTrack.DashUrls.Count} (0.00%), {GetTrackShortInfo(mediaTrack)}";
			}));

			bool canMerge = config.MergeToContainer && IsFfmpegAvailable();
			string fnDash = MultiThreadedDownloader.GetNumberedFileName(
				(canMerge ? config.TempDirPath : config.DownloadingDirPath) +
				$"{formattedFileName}_{mediaTrack.FormatId}.{mediaTrack.FileExtension}");
			string fnDashFinal = fnDash;
			string fnDashTmp = fnDash + ".tmp";

			_dashCancelRequired = false;

			if (File.Exists(fnDashTmp))
			{
				File.Delete(fnDashTmp);
			}

			Stream fileStream = File.OpenWrite(fnDashTmp);
			FileDownloader _singleThreadedDownloader = new FileDownloader();
			int errorCode = 400;
			for (int i = 0; i < mediaTrack.DashUrls.Count; ++i)
			{
				int errors = 0;
				do
				{
					Stream memChunk = new MemoryStream();
					_singleThreadedDownloader.Url = mediaTrack.DashUrls[i];
					_singleThreadedDownloader.Connected += (object s, string url, long contentLength, int errCode) =>
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
										return FileDownloader.DOWNLOAD_ERROR_DRIVE_NOT_READY;
									}
									long minimumFreeSpaceRequired = contentLength * 10;
									if (driveInfo.AvailableFreeSpace <= minimumFreeSpaceRequired)
									{
										return FileDownloader.DOWNLOAD_ERROR_INSUFFICIENT_DISK_SPACE;
									}
								}
							}
						}

						return errCode;
					};

					errorCode = _singleThreadedDownloader.Download(memChunk);
					if (errorCode != 200)
					{
						memChunk.Dispose();
						continue;
					}
					memChunk.Position = 0L;
					bool appended = StreamAppender.Append(memChunk, fileStream);
					memChunk.Dispose();
					if (!appended)
					{
						fileStream.Dispose();
						return new DownloadResult(MultiThreadedDownloader.DOWNLOAD_ERROR_MERGING_CHUNKS, null, null);
					}
				} while (errorCode != 200 && errors++ < 9 && !_dashCancelRequired);

				if (_dashCancelRequired)
				{
					errorCode = FileDownloader.DOWNLOAD_ERROR_CANCELED_BY_USER;
				}

				if (errorCode != 200)
				{
					break;
				}

				Invoke(new MethodInvoker(() =>
				{
					int chunkNumber = i + 1;
					progressBarDownload.Value = chunkNumber;
					double percent = 100.0 / progressBarDownload.Maximum * chunkNumber;
					lblStatus.Text = $"Скачивание чанков {mediaType}:";
					lblProgress.Left = lblStatus.Left + lblStatus.Width;
					lblProgress.Text = $"{chunkNumber} / {mediaTrack.DashUrls.Count}" +
						$" ({string.Format("{0:F2}", percent)}%), {GetTrackShortInfo(mediaTrack)}";
				}));
			}
			fileStream.Dispose();
			if (errorCode == 200)
			{
				fnDashFinal = MultiThreadedDownloader.GetNumberedFileName(fnDash);
				File.Move(fnDashTmp, fnDashFinal);
			}

			DownloadResult downloadResult =
				new DownloadResult(errorCode, _singleThreadedDownloader.LastErrorMessage, fnDashFinal);
			_singleThreadedDownloader.Dispose();
			_singleThreadedDownloader = null;
			return downloadResult;
		}

		private async Task<DownloadResult> DownloadYouTubeMediaTrack(
			YouTubeMediaTrack mediaTrack, string formattedFileName, bool audioOnly)
		{
			if (mediaTrack.IsDashManifest)
			{
				return DownloadDash(mediaTrack, formattedFileName);
			}
			else
			{
				YouTubeMediaTrackVideo videoTrack = mediaTrack as YouTubeMediaTrackVideo;
				bool isVideo = videoTrack != null;
				if (isVideo)
				{
					audioOnly = false;
				}
				bool isContainer = mediaTrack is YouTubeMediaTrackContainer;
				string mediaTypeString = isVideo || isContainer ? "видео" : "аудио";

				string fileUrl = mediaTrack.FileUrl;

				#region Расшифровка Cipher
				//TODO: Вынести это в отдельный метод.
				if (mediaTrack.IsCiphered)
				{
					if (string.IsNullOrEmpty(config.CipherDecryptionAlgo) || string.IsNullOrWhiteSpace(config.CipherDecryptionAlgo))
					{
						return new DownloadResult(ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM, null, null);
					}

					#region Расшифровка ссылки на видео-дорожку
					string cipherDecrypted = DecryptCipherSignature(mediaTrack.CipherSignatureEncrypted, config.CipherDecryptionAlgo);

					if (string.IsNullOrEmpty(cipherDecrypted))
					{
						return new DownloadResult(ERROR_CIPHER_DECRYPTION, null, null);
					}

					fileUrl = $"{mediaTrack.CipherEncryptedFileUrl}&sig={cipherDecrypted}";

					if (FileDownloader.GetUrlResponseHeaders(fileUrl, null, out _, out _) != 200)
					{
						return new DownloadResult(ERROR_CIPHER_DECRYPTION, null, null);
					}
					#endregion

					#region Расшифровка ссылки на аудио-дорожку
					if ((mediaTrack is YouTubeMediaTrackAudio) && audioFormats.Count > 0)
					{
						YouTubeMediaTrackAudio audioFile = audioFormats[0];
						string audioCipherDecrypted = DecryptCipherSignature(
							audioFile.CipherSignatureEncrypted, config.CipherDecryptionAlgo);
						string urlAudio = $"{audioFile.CipherEncryptedFileUrl}&sig={audioCipherDecrypted}";
						if (FileDownloader.GetUrlResponseHeaders(urlAudio, null, out _, out _) != 200)
						{
							return new DownloadResult(ERROR_CIPHER_DECRYPTION, null, null);
						}
					}
					#endregion
				}
				#endregion

				bool useRamToStoreTemporaryFiles = config.UseRamToStoreTemporaryFiles;

				try
				{
					_multiThreadedDownloader = new MultiThreadedDownloader();
					_multiThreadedDownloader.ThreadCount = isVideo ? config.ThreadCountVideo : config.ThreadCountAudio;
					_multiThreadedDownloader.Url = fileUrl;
					if (!useRamToStoreTemporaryFiles)
					{
						_multiThreadedDownloader.TempDirectory = config.TempDirPath;
					}
					_multiThreadedDownloader.UseRamForTempFiles = useRamToStoreTemporaryFiles;

					string destFilePath;
					if (mediaTrack is YouTubeMediaTrackContainer)
					{
						destFilePath = MultiThreadedDownloader.GetNumberedFileName(
							$"{config.DownloadingDirPath}{formattedFileName}.{mediaTrack.MimeExt}");
						_multiThreadedDownloader.KeepDownloadedFileInTempOrMergingDirectory = false;
					}
					else
					{
						if (!audioOnly && config.MergeToContainer && IsFfmpegAvailable())
						{
							_multiThreadedDownloader.MergingDirectory = DecideMergingDirectory();
							_multiThreadedDownloader.KeepDownloadedFileInTempOrMergingDirectory = true;
						}
						else
						{
							_multiThreadedDownloader.MergingDirectory = config.DownloadingDirPath;
						}
						destFilePath = MultiThreadedDownloader.GetNumberedFileName(config.DownloadingDirPath +
							$"{formattedFileName}_{mediaTrack.FormatId}.{mediaTrack.FileExtension}");
					}
					_multiThreadedDownloader.OutputFileName = destFilePath;

					_multiThreadedDownloader.Connecting += (s, url) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							string shortInfo = isVideo || isContainer ? GetTrackShortInfo(videoTrack) : GetTrackShortInfo(mediaTrack as YouTubeMediaTrackAudio);
							lblStatus.Text = $"Состояние: Подключение... {shortInfo}";
							lblProgress.Text = null;
						}));
					};
					_multiThreadedDownloader.Connected += (object s, string url, long contentLength, CustomError customError) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							if (customError.ErrorCode == 200 || customError.ErrorCode == 206)
							{
								string shortInfo = isVideo || isContainer ? GetTrackShortInfo(videoTrack) : GetTrackShortInfo(mediaTrack as YouTubeMediaTrackAudio);
								lblStatus.Text = $"Состояние: Подключено! {shortInfo}";

								if (contentLength > 0L)
								{
									long minimumFreeSpaceRequired = (long)(contentLength * 1.1);

									MultiThreadedDownloader mtd = s as MultiThreadedDownloader;

									List<char> driveLetters = mtd.GetUsedDriveLetters();
									if (driveLetters.Count > 0 && !IsEnoughDiskSpace(driveLetters, minimumFreeSpaceRequired))
									{
										customError.ErrorMessage = "Недостаточно места на диске!";
										customError.ErrorCode = MultiThreadedDownloader.DOWNLOAD_ERROR_CUSTOM;
										return;
									}

									if (mtd.UseRamForTempFiles && MemoryWatcher.Update() &&
										MemoryWatcher.RamFree < (ulong)minimumFreeSpaceRequired)
									{
										customError.ErrorMessage = "Недостаточно памяти!";
										customError.ErrorCode = MultiThreadedDownloader.DOWNLOAD_ERROR_CUSTOM;
										return;
									}
								}
							}
						}));
					};
					_multiThreadedDownloader.DownloadStarted += (s, size) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							progressBarDownload.Value = 0;
							progressBarDownload.Maximum = 100;

							lblStatus.Text = $"Скачивание {mediaTypeString}:";
							string shortInfo = isVideo || isContainer ? GetTrackShortInfo(videoTrack) : GetTrackShortInfo(mediaTrack as YouTubeMediaTrackAudio);
							lblProgress.Text = $"0 / {FormatSize(size)} (0.00%), {shortInfo}";
							lblProgress.Left = lblStatus.Left + lblStatus.Width;
						}));
					};
					_multiThreadedDownloader.DownloadProgress += (object s, long bytesTransferred) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							long fileSize = _multiThreadedDownloader.ContentLength != 0L ? _multiThreadedDownloader.ContentLength : videoTrack.ContentLength;
							double percent = 100.0 / fileSize * bytesTransferred;
							progressBarDownload.Value = (int)Math.Round(percent);

							string percentString = string.Format("{0:F2}", percent);
							string shortInfo = isVideo || isContainer ? GetTrackShortInfo(videoTrack) : GetTrackShortInfo(mediaTrack as YouTubeMediaTrackAudio);
							lblProgress.Text = $"{FormatSize(bytesTransferred)} / {FormatSize(fileSize)}" +
								$" ({percentString}%), {shortInfo}";
						}));
					};
					_multiThreadedDownloader.ChunkMergingStarted += (s, chunkCount) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							progressBarDownload.Value = 0;
							progressBarDownload.Maximum = chunkCount;

							lblStatus.Text = $"Объединение чанков {mediaTypeString}:";
							lblProgress.Text = $"0 / {chunkCount}";
							lblProgress.Left = lblStatus.Left + lblStatus.Width;
						}));
					};
					_multiThreadedDownloader.ChunkMergingProgress += (s, chunkId, chunkCount, chunkPosition, chunkSize) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							double percent = 100.0 / chunkSize * chunkPosition;
							string percentString = string.Format("{0:F2}", percent);
							lblProgress.Text = $"{chunkId + 1} / {_multiThreadedDownloader.ThreadCount}: " +
								$"{FormatSize(chunkPosition)} / {FormatSize(chunkSize)} ({percentString}%)";
							progressBarDownload.Value = chunkId + 1;
						}));
					};
					int res = await Task.Run(() => _multiThreadedDownloader.Download());
					if (useRamToStoreTemporaryFiles)
					{
						GC.Collect();
					}
					DownloadResult downloadResult =
						new DownloadResult(res, _multiThreadedDownloader.LastErrorMessage,
						_multiThreadedDownloader.OutputFileName);
					_multiThreadedDownloader.Dispose();
					_multiThreadedDownloader = null;
					return downloadResult;
				} catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
					DownloadResult downloadResult =
						new DownloadResult(ex.HResult, ex.Message,
						_multiThreadedDownloader.OutputFileName);
					_multiThreadedDownloader.Dispose();
					_multiThreadedDownloader = null;
					return downloadResult;
				}
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

			IsDownloadInProgress = true;

			progressBarDownload.Value = 0;
			lblStatus.Text = null;
			lblProgress.Text = null;

			string formattedFileName = FixFileName(FormatFileName(
				IsVideoDateAvailable(VideoInfo) ?
				config.OutputFileNameFormatWithDate :
				config.OutputFileNameFormatWithoutDate, VideoInfo));

			List<YouTubeMediaTrack> tracksToDownload = new List<YouTubeMediaTrack>();

			ToolStripMenuItem mi = sender as ToolStripMenuItem;
			if (mi.Tag == null)
			{
				lblStatus.Text = "Состояние: Выбор форматов...";
				List<YouTubeMediaTrack> formats = new List<YouTubeMediaTrack>();
				formats.AddRange(videoFormats);
				formats.AddRange(audioFormats);
				FormTracksSelector tracksSelector = new FormTracksSelector(formats);
				DialogResult dialogResult = tracksSelector.ShowDialog();
				lblStatus.Text = null;
				if (dialogResult == DialogResult.OK)
				{
					foreach (YouTubeMediaTrack mediaTrack in tracksSelector.SelectedTracks)
					{
						tracksToDownload.Add(mediaTrack);
					}
				}
				else
				{
					MessageBox.Show("Скачивание отменено!", "Отменный отменятор отменения отмены",
						MessageBoxButtons.OK, MessageBoxIcon.Stop);
					IsDownloadInProgress = false;
					btnDownload.Enabled = true;
					return;
				}
			}
			else if (mi.Tag is YouTubeMediaTrackVideo)
			{
				YouTubeMediaTrackVideo videoTrack = mi.Tag as YouTubeMediaTrackVideo;
				if (videoTrack.IsHlsManifest)
				{
					if (IsFfmpegAvailable())
					{
						lblStatus.Text = "Состояние: Запуск FFMPEG...";
						lblStatus.Refresh();

						string filePath = MultiThreadedDownloader.GetNumberedFileName(
							$"{config.DownloadingDirPath}{formattedFileName}.ts");
						GrabHls(videoTrack.FileUrl, filePath);
						lblStatus.Text = null;
					}
					else
					{
						lblStatus.Text = "Состояние: Не удалось запустить FFMPEG!";
						string ffmpegMsg = "Не удалось запустить FFMPEG!";
						MessageBox.Show(ffmpegMsg, "Ошибка!",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					IsDownloadInProgress = false;
					btnDownload.Enabled = true;
					return;
				}

				if (config.DownloadAllAdaptiveVideoTracks)
				{
					foreach (YouTubeMediaTrack videoFormat in videoFormats)
					{
						if (videoFormat is YouTubeMediaTrackVideo && !videoFormat.IsHlsManifest)
						{
							tracksToDownload.Add(videoFormat);
						}
					}
				}
				else
				{
					tracksToDownload.Add(videoTrack);
				}

				if (audioFormats.Count > 0)
				{
					if (config.DownloadAllAudioTracks)
					{
						foreach (YouTubeMediaTrack audioFormat in audioFormats)
						{
							if (audioFormat is YouTubeMediaTrackAudio)
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
								if (audioFormats[1].ContentLength > audioFormats[0].ContentLength)
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
			else if (mi.Tag is YouTubeMediaTrackContainer)
			{
				tracksToDownload.Add(mi.Tag as YouTubeMediaTrackContainer);
			}
			else if (mi.Tag is YouTubeMediaTrackAudio)
			{
				tracksToDownload.Add(mi.Tag as YouTubeMediaTrackAudio);
			}

			//Подсчёт общего размера файлов.
			long summaryFilesSize = 0L;
			foreach (YouTubeMediaTrack ytmt in tracksToDownload)
			{
				if (ytmt.ContentLength > 0L)
				{
					summaryFilesSize += ytmt.ContentLength;
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

					IsDownloadInProgress = false;
					btnDownload.Enabled = true;
					return;
				}

				if (tracksToDownload.Count > 1 && !(tracksToDownload[0] is YouTubeMediaTrackContainer) &&
					!AdvancedFreeSpaceCheck(summaryFilesSize))
				{
					lblStatus.Text = "Состояние: Ошибка: Недостаточно места на диске!";
					MessageBox.Show("Недостаточно места на диске!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);

					IsDownloadInProgress = false;
					btnDownload.Enabled = true;
					return;
				}
			}

			bool needToMerge = tracksToDownload.Count > 0 && !(tracksToDownload[0] is YouTubeMediaTrackContainer);
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
					IsDownloadInProgress = false;
					btnDownload.Enabled = true;
					return;
				}
			}

			btnDownload.Text = "Отмена";
			btnDownload.Enabled = true;

			lblStatus.Text = "Скачивание...";

			List<DownloadResult> downloadResults = new List<DownloadResult>();
			bool audioOnly = IsAudioOnly(tracksToDownload);
			DownloadResult downloadResult = await Task.Run(() =>
				DownloadTracks(tracksToDownload, formattedFileName, audioOnly, downloadResults));
			lblProgress.Text = null;
			lblProgress.Refresh();

			if (downloadResult.ErrorCode == 200)
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

							IsDownloadInProgress = false;
							btnDownload.Text = "Скачать";
							btnDownload.Enabled = true;
							return;
						}
					}

					string ext = "mkv";
					if (!config.AlwaysUseMkvContainer)
					{
						ext = "mp4";
						foreach (YouTubeMediaTrack mediaTrack in tracksToDownload)
						{
							if (mediaTrack.MimeExt != "mp4")
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
						foreach (DownloadResult dr in downloadResults)
						{
							if (File.Exists(dr.FileName))
							{
								File.Delete(dr.FileName);
							}
						}
					}
				}

				//сохранение картинки
				if (config.SavePreviewImage && _videoImageData != null)
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
				switch (downloadResult.ErrorCode)
				{
					case FileDownloader.DOWNLOAD_ERROR_CANCELED_BY_USER:
						lblStatus.Text = "Состояние: Скачивание отменено";
						MessageBox.Show($"{VideoInfo.Title}\nСкачивание успешно отменено!", "Ошибка!",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					case FileDownloader.DOWNLOAD_ERROR_INSUFFICIENT_DISK_SPACE:
						lblStatus.Text = "Состояние: Ошибка! Недостаточно места на диске!";
						MessageBox.Show($"{VideoInfo.Title}\nНедостаточно места на диске!", "Ошибка!",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					case ERROR_CIPHER_DECRYPTION:
						lblStatus.Text = "Состояние: Ошибка расшифровки Cipher!";
						MessageBox.Show($"{VideoInfo.Title}\n" +
							"Ошибка расшифровки ссылки! Попробуйте ещё раз.", "Ошибка!",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					case ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM:
						{
							lblStatus.Text = "Состояние: Ошибка! Не указан алгоритм для расшифровки Cipher!";
							string t = "Ссылка на это видео, зачем-то, зашифрована алгоритмом \"Cipher\", " +
								"для расшифровки которого вам требуется ввести специальную последовательность чисел, " +
								"известную одному лишь дьяволу.\n" +
								"Или включите галочку \"Использовать скрытое API для получения информации о видео\".";
							MessageBox.Show($"{VideoInfo.Title}\nОшибка ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM\n{t}", "Ошибка!",
							   MessageBoxButtons.OK, MessageBoxIcon.Error);
							break;
						}

					case MultiThreadedDownloader.DOWNLOAD_ERROR_MERGING_CHUNKS:
						lblStatus.Text = "Состояние: Ошибка объединения чанков!";
						MessageBox.Show($"{VideoInfo.Title}\nСкачивание прервано!\nОшибка объединения чанков!", "Ошибатор ошибок",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					case MultiThreadedDownloader.DOWNLOAD_ERROR_CREATE_FILE:
						lblStatus.Text = "Состояние: Ошибка создания файла!";
						MessageBox.Show($"{VideoInfo.Title}\nСкачивание прервано!\nОшибка создания файла!", "Ошибатор ошибок",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					case MultiThreadedDownloader.DOWNLOAD_ERROR_NO_URL_SPECIFIED:
						lblStatus.Text = "Состояние: Ошибка! Не указана ссылка на файл!";
						MessageBox.Show($"{VideoInfo.Title}\nСкачивание прервано!\nНе указана ссылка на файл!", "Ошибатор ошибок",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					case MultiThreadedDownloader.DOWNLOAD_ERROR_NO_FILE_NAME_SPECIFIED:
						lblStatus.Text = "Состояние: Ошибка! Не указано имя файла!";
						MessageBox.Show($"{VideoInfo.Title}\nСкачивание прервано!\nОшибка создания файла!", "Ошибатор ошибок",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					case MultiThreadedDownloader.DOWNLOAD_ERROR_TEMPORARY_DIR_NOT_EXISTS:
						lblStatus.Text = "Состояние: Ошибка! Папка для временных файлов не существует!";
						MessageBox.Show($"{VideoInfo.Title}\nСкачивание прервано!\n" +
							"Папка для временных файлов не существует!", "Ошибатор ошибок",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					case MultiThreadedDownloader.DOWNLOAD_ERROR_MERGING_DIR_NOT_EXISTS:
						lblStatus.Text = "Состояние: Ошибка! Папка для объединения чанков не существует!";
						MessageBox.Show($"{VideoInfo.Title}\nСкачивание прервано!\n" +
							"Папка для объединения чанков не существует!", "Ошибатор ошибок",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					case MultiThreadedDownloader.DOWNLOAD_ERROR_CUSTOM:
						lblStatus.Text = $"Состояние: Ошибка! {downloadResult.ErrorMessage}";
						MessageBox.Show($"{VideoInfo.Title}\nСкачивание прервано!\n{downloadResult.ErrorMessage}", "Ошибатор ошибок",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					default:
						{
							lblStatus.Text = $"Состояние: Ошибка {downloadResult.ErrorCode}";
							string t = $"{VideoInfo.Title}\nСкачивание прервано!\nКод ошибки: {downloadResult.ErrorCode}";
							if (!string.IsNullOrEmpty(downloadResult.ErrorMessage))
							{
								t += "\n" + downloadResult.ErrorMessage;
							}
							MessageBox.Show(t, "Ошибатор ошибок",
								MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						break;
				}
			}

			btnDownload.Text = "Скачать";
			btnDownload.Enabled = true;
			IsDownloadInProgress = false;
		}

		private async Task<DownloadResult> DownloadTracks(IEnumerable<YouTubeMediaTrack> tracks,
			string formattedFileName, bool audioOnly, List<DownloadResult> results)
		{
			DownloadResult result = null;
			foreach (YouTubeMediaTrack track in tracks)
			{
				result = await DownloadYouTubeMediaTrack(track, formattedFileName, audioOnly);
				if (result.ErrorCode != 200)
				{
					return result;
				}

				results.Add(result);
			}

			return result == null ? new DownloadResult(MultiThreadedDownloader.DOWNLOAD_ERROR_CUSTOM,
				"Список ссылок для скачивания пуст!", null) : result;
		}

		public void StopDownload()
		{
			_dashCancelRequired = true;
			_multiThreadedDownloader?.Stop();
		}

		private bool IsAudioOnly(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			foreach (YouTubeMediaTrack mediaTrack in mediaTracks)
			{
				if (!(mediaTrack is YouTubeMediaTrackAudio))
				{
					return false;
				}
			}
			return true;
		}

		private void SaveImageToFile(string formattedFileName)
		{
			if (!string.IsNullOrEmpty(formattedFileName) && !string.IsNullOrWhiteSpace(formattedFileName))
			{
				string imageFileName = _videoImage != null ?
					$"_image_{_videoImage.Width}x{_videoImage.Height}.jpg" : "_image.dat";
				string filePath = $"{config.DownloadingDirPath}{formattedFileName}{imageFileName}";
				_videoImageData.SaveToFile(filePath);
			}
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
			if (_videoImage != null)
			{
				Rectangle imageRect = new Rectangle(0, 0, _videoImage.Width, _videoImage.Height);
				Rectangle resizedRect = imageRect.ResizeTo(imagePreview.Size).CenterIn(imagePreview.ClientRectangle);
				e.Graphics.DrawImage(_videoImage, resizedRect);
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
			
				if (_ciphered || VideoInfo.IsDashed)
				{
					string t = VideoInfo.IsDashed ? "dash" : "cipher";
					SizeF sz = e.Graphics.MeasureString(t, fnt);
					RectangleF rect = new RectangleF(0, imagePreview.Height - sz.Height, sz.Width, sz.Height);
					e.Graphics.FillRectangle(Brushes.Black, rect);
					e.Graphics.DrawString(t, fnt, Brushes.White, new PointF(rect.X, rect.Y));
				}
				if (VideoInfo.IsLiveNow)
				{
					string t = "hls";
					SizeF sz = e.Graphics.MeasureString(t, fnt);
					float y = (_ciphered || VideoInfo.IsDashed) ? 
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
			double x = imageFavorite.Width / 2.0;
			double y = imageFavorite.Height / 2.0;
			e.Graphics.DrawStar(x, y, x, 3.0, 0.0, IsFavoriteVideo, Color.LimeGreen);
		}

		private void imgFavoriteChannel_Paint(object sender, PaintEventArgs e)
		{
			double x = imageFavorite.Width / 2.0;
			double y = imageFavorite.Height / 2.0;
			e.Graphics.DrawStar(x, y, x, 3.0, 0.0, IsFavoriteChannel, Color.LimeGreen);
		}

		private async void btnGetWebPage_Click(object sender, EventArgs e)
		{
			btnGetWebPage.Enabled = false;
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnGetWebPage.Enabled = true;
				return;
			}

			if (!string.IsNullOrEmpty(_webPage))
			{
				SetClipboardText(_webPage);
				MessageBox.Show("Скопировано в буфер обмена", "Код веб-страницы",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
				btnGetWebPage.Enabled = true;
				return;
			}

			YouTubeVideoWebPageResult webPageResult = await Task.Run(() => YouTubeVideoWebPage.Get(new VideoId(VideoInfo.Id)));
			if (webPageResult.ErrorCode == 200)
			{
				SetClipboardText(webPageResult.VideoWebPage.WebPageCode);
				MessageBox.Show("Скопировано в буфер обмена", "Код веб-страницы",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			else
			{
				MessageBox.Show("Ошибатор ошибок", $"Ошибка {webPageResult.VideoWebPage}",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			btnGetWebPage.Enabled = true;
		}

		private async void btnGetVideoInfo_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			{
				if (!string.IsNullOrEmpty(_webPage))
				{
					YouTubeVideoWebPageResult videoWebPageResult = YouTubeVideoWebPage.FromCode(_webPage);
					RawVideoInfoResult rawVideoInfoResult =
						YouTubeApiLib.Utils.ExtractRawVideoInfoFromWebPage(videoWebPageResult.VideoWebPage);
					if (rawVideoInfoResult.ErrorCode == 200)
					{
						string t = rawVideoInfoResult.RawVideoInfo.RawData.ToString();
						SetClipboardText(t);
						MessageBox.Show("Скопировано в буфер обмена", "Информация о видео",
							MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					else
					{
						MessageBox.Show("Не удалось получить информацию о видео!", "Ошибатор ошибок",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					return;
				}
			}
			{
				RawVideoInfoResult rawVideoInfoResult = null;
				await Task.Run(() =>
				{
					YouTubeApi api = new YouTubeApi();
					YouTubeApiLib.Utils.VideoInfoGettingMethod method = config.UseHiddenApiForGettingInfo ?
						YouTubeApiLib.Utils.VideoInfoGettingMethod.HiddenApiEncryptedUrls :
						YouTubeApiLib.Utils.VideoInfoGettingMethod.WebPage;
					rawVideoInfoResult = api.GetRawVideoInfo(
						new VideoId(VideoInfo.Id), method);
				});
				if (rawVideoInfoResult.ErrorCode == 200)
				{
					SetClipboardText(rawVideoInfoResult.RawVideoInfo.RawData.ToString());
					MessageBox.Show("Скопировано в буфер обмена", "Информация о видео",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else
				{
					MessageBox.Show("Ошибатор ошибок", $"Ошибка {rawVideoInfoResult.ErrorCode}",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private async void btnGetVideoUrls_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			StreamingData streamingData = await Task.Run(() =>
				!string.IsNullOrEmpty(_webPage) && !string.IsNullOrWhiteSpace(_webPage) ?
				ExtractStreamingDataFromWebPageCode(_webPage) :
				YouTubeApiLib.Utils.GetStreamingData(VideoInfo.Id, YouTubeApiLib.Utils.VideoInfoGettingMethod.HiddenApiDecryptedUrls));

			if (streamingData == null || streamingData.RawData == null)
			{
				MessageBox.Show("Не удалось получить ссылки для скачивания! ", "Ссылки для скачивания",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			string t = streamingData.RawData.ToString();
			SetClipboardText(t);
			MessageBox.Show("Скопировано в буфер обмена", "Ссылки для скачивания",
				MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private async void btnGetDashManifest_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			RawVideoInfoResult rawVideoInfoResult = null;
			await Task.Run(() =>
			{
				YouTubeApi api = new YouTubeApi();
				YouTubeApiLib.Utils.VideoInfoGettingMethod method = config.UseHiddenApiForGettingInfo ?
					YouTubeApiLib.Utils.VideoInfoGettingMethod.HiddenApiDecryptedUrls :
					YouTubeApiLib.Utils.VideoInfoGettingMethod.WebPage;
				rawVideoInfoResult = api.GetRawVideoInfo(new VideoId(VideoInfo.Id), method);
			});
			if (rawVideoInfoResult.ErrorCode == 200)
			{
				string dashManifestUrl = rawVideoInfoResult.RawVideoInfo.StreamingData?.RawData?.Value<string>("dashManifestUrl");
				FileDownloader d = new FileDownloader() { Url = dashManifestUrl };
				if (d.DownloadString(out string manifest) == 200)
				{
					SetClipboardText(manifest);
					MessageBox.Show("Скопировано в буфер обмена", "DASH manifest",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
			}

			MessageBox.Show("DASH manifest не найден!", "Ошибатор ошибок",
				MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private async void btnGetHlsManifest_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			RawVideoInfoResult rawVideoInfoResult = null;
			await Task.Run(() =>
			{
				YouTubeApi api = new YouTubeApi();
				YouTubeApiLib.Utils.VideoInfoGettingMethod method = config.UseHiddenApiForGettingInfo ?
					YouTubeApiLib.Utils.VideoInfoGettingMethod.HiddenApiDecryptedUrls :
					YouTubeApiLib.Utils.VideoInfoGettingMethod.WebPage;
				rawVideoInfoResult = api.GetRawVideoInfo(new VideoId(VideoInfo.Id), method);
			});
			if (rawVideoInfoResult.ErrorCode == 200)
			{
				string dashManifestUrl = rawVideoInfoResult.RawVideoInfo.StreamingData?.RawData?.Value<string>("hlsManifestUrl");
				FileDownloader d = new FileDownloader() { Url = dashManifestUrl };
				if (d.DownloadString(out string manifest) == 200)
				{
					SetClipboardText(manifest);
					MessageBox.Show("Скопировано в буфер обмена", "HLS manifest",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
			}

			MessageBox.Show("HLS manifest не найден!", "Ошибатор ошибок",
				MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private async void btnGetPlayerCode_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			string page = _webPage;
			if (string.IsNullOrEmpty(page) || string.IsNullOrWhiteSpace(page))
			{
				YouTubeVideoWebPageResult webPageResult = null;
				await Task.Run(() => webPageResult = YouTubeVideoWebPage.Get(new VideoId(VideoInfo.Id)));
				if (webPageResult.ErrorCode != 200)
				{
					MessageBox.Show("Ошибка скачивания плеера!", "Ошибатор ошибок",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				page = webPageResult.VideoWebPage.WebPageCode;
			}

			string url = ExtractPlayerUrlFromWebPage(page);
			if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
			{
				MessageBox.Show("Ошибка скачивания плеера!", "Ошибатор ошибок!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			string code = null;
			int errCode = await Task.Run(() =>
			{
				FileDownloader d = new FileDownloader() { Url = url };
				int errorCode = d.DownloadString(out code);
				d.Dispose();
				return errorCode;
			});
			if (errCode != 200)
			{
				MessageBox.Show("Ошибка скачивания плеера!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				using (SaveFileDialog sfd = new SaveFileDialog())
				{
					sfd.InitialDirectory = config.DownloadingDirPath;
					sfd.Title = "Сохранить код плеера как...";
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
				}
			} catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void imagePreview_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
			if (e.Button == MouseButtons.Right && VideoInfo.IsInfoAvailable)
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
			if (e.Button == MouseButtons.Right && VideoInfo.IsInfoAvailable)
			{
				contextMenuVideoTitle.Show(Cursor.Position);
			}
		}

		private void imageFavorite_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
			if (e.Button == MouseButtons.Left && VideoInfo.IsInfoAvailable)
			{
				SetFavoriteVideo(!IsFavoriteVideo);
			}
		}

		private void imageFavoriteChannel_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
			if (VideoInfo.IsInfoAvailable)
			{
				if (FavoriteChannelChanged != null)
				{
					FavoriteChannelChanged.Invoke(this, VideoInfo.OwnerChannelId, !IsFavoriteChannel);
				}
				else
				{
					IsFavoriteChannel = !IsFavoriteChannel;
				}
			}
		}

		private void lblDatePublished_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
			if (e.Button == MouseButtons.Right && VideoInfo.IsInfoAvailable)
			{
				contextMenuDate.Show(Cursor.Position);
			}
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
			contextMenuDate.SetFontSize(fontSize);
			contextMenuDownloads.SetFontSize(fontSize);
		}

		private void miOpenVideoInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenUrlInBrowser($"{YOUTUBE_WATCH_URL_BASE}?v={VideoInfo.Id}");
		}

		private void miCopyVideoUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetClipboardText($"{YOUTUBE_WATCH_URL_BASE}?v={VideoInfo.Id}");
		}

		private void miCopyChannelTitleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(VideoInfo.OwnerChannelTitle) && !string.IsNullOrWhiteSpace(VideoInfo.OwnerChannelTitle))
			{
				SetClipboardText(VideoInfo.OwnerChannelTitle);
			}
		}

		private void miCopyChannelIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (VideoInfo == null || string.IsNullOrEmpty(VideoInfo.OwnerChannelId) ||
				string.IsNullOrWhiteSpace(VideoInfo.OwnerChannelId))
			{
				string msg = "Произошла ошибация! По-этому, в настоящее время " +
					"данное действие совершить невозможно! Сорян, бро!";
				MessageBox.Show(msg, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			SetClipboardText(VideoInfo.OwnerChannelId);
		}

		private void miOpenChannelInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenUrlInBrowser(string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, VideoInfo.OwnerChannelId));
		}

		private void lblChannelTitle_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			OpenChannel?.Invoke(this, VideoInfo.OwnerChannelTitle, VideoInfo.OwnerChannelId);
		}

		private void miSaveImageAssToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_videoImageData != null)
			{
				try
				{
					using (Image img = Image.FromStream(_videoImageData))
					{
						using (SaveFileDialog sfd = new SaveFileDialog())
						{
							sfd.Title = "Сохранить изображение";
							sfd.Filter = "jpg|*.jpg";
							sfd.DefaultExt = ".jpg";
							sfd.AddExtension = true;
							sfd.InitialDirectory = string.IsNullOrEmpty(config.DownloadingDirPath) ? config.SelfDirPath : config.DownloadingDirPath;
							string fileNameSuffix = $"_image_{img.Width}x{img.Height}";
							string fileName = FixFileName(FormatFileName(
								config.OutputFileNameFormatWithDate, VideoInfo)) + fileNameSuffix;
							sfd.FileName = fileName;
							if (sfd.ShowDialog() == DialogResult.OK)
							{
								_videoImageData.SaveToFile(sfd.FileName);
							}
						}
					}
				} catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
					MessageBox.Show(ex.Message, "Ошибатор ошибок",
						MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
				}
			}
		}

		private void miCopyChannelNameWithIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SetClipboardText($"{VideoInfo.OwnerChannelTitle} [{VideoInfo.OwnerChannelId}]");
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
			if (VideoInfo.ThumbnailUrls.Count > 0)
			{
				string url = VideoInfo.ThumbnailUrls[0].Url;
				OpenUrlInBrowser(url);
			}
		}

		private void miCopyChannelUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string url = string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, VideoInfo.OwnerChannelId);
			SetClipboardText(url);
		}

		private void miCopyImageUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (VideoInfo.ThumbnailUrls == null || VideoInfo.ThumbnailUrls.Count == 0 ||
				string.IsNullOrEmpty(VideoInfo.ThumbnailUrls[0].Url) || string.IsNullOrWhiteSpace(VideoInfo.ThumbnailUrls[0].Url))
			{
				MessageBox.Show("Ссылка на изображение отсутствует!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			SetClipboardText(VideoInfo.ThumbnailUrls[0].Url);
		}

		private void miUpdateVideoPublishedDateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DateTime dateTime = GetVideoPublishedDate(VideoInfo.Id);
			if (dateTime == DateTime.MaxValue)
			{
				MessageBox.Show("Не получилось.", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

			VideoInfo = new YouTubeVideo(VideoInfo.Title, VideoInfo.Id,
				VideoInfo.Length, VideoInfo.DateUploaded, dateTime,
				VideoInfo.OwnerChannelTitle, VideoInfo.OwnerChannelId,
				VideoInfo.Description, VideoInfo.ViewCount, VideoInfo.Category,
				VideoInfo.IsPrivate, VideoInfo.IsUnlisted, VideoInfo.IsFamilySafe,
				VideoInfo.IsLiveContent, VideoInfo.ThumbnailUrls, VideoInfo.MediaTracks,
				VideoInfo.RawInfo, VideoInfo.SimplifiedInfo, VideoInfo.Status);

			DateTime date = config.UseGmtTime ? dateTime.ToGmt() : dateTime;
			string datePublishedString = $"Дата публикации: {date:yyyy.MM.dd, HH:mm:ss}";
			if (config.UseGmtTime) { datePublishedString += " GMT"; }

			lblDatePublished.Text = datePublishedString;
		}

		private void miCopyVideoPublishedDateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DateTime date = config.UseGmtTime ? VideoInfo.DatePublished.ToGmt() : VideoInfo.DatePublished;
			string datePublishedString = date.ToString("yyyy-MM-dd HH-mm-ss");
			if (config.UseGmtTime) { datePublishedString += " GMT"; }

			SetClipboardText(datePublishedString);
		}
	}
}
