using System;
using System.Collections.Concurrent;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
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
		private ConcurrentDictionary<int, DownloadableTask> _contentChunks;
		public YouTubeVideo VideoInfo { get; private set; }
		private Stream _videoImageData = null;
		private Image _videoImage = null;
		private bool _ciphered;
		private bool _isFavoriteVideo = false;
		private bool _isFavoriteChannel = false;
		private bool _isVideo = true;
		private bool _isContainer = false;
		private YouTubeMediaTrack _mediaTrack;
		public YouTubeVideoWebPage WebPage { get; }
		public bool IsFavoriteVideo { get => _isFavoriteVideo; set { SetFavoriteVideo(value); } }
		public bool IsFavoriteChannel { get => _isFavoriteChannel; set { SetFavoriteChannel(value); } }

		public bool IsDownloadInProgress { get; private set; }

		private List<YouTubeMediaTrackAudio> audioFormats = new List<YouTubeMediaTrackAudio>();
		private List<YouTubeMediaTrackVideo> videoFormats = new List<YouTubeMediaTrackVideo>();
		private List<YouTubeMediaTrackHlsStream> hlsFormats = new List<YouTubeMediaTrackHlsStream>();
		private List<YouTubeMediaTrackContainer> containerFormats = new List<YouTubeMediaTrackContainer>();

		public delegate void BtnDownloadClickedDelegate(object sender, EventArgs e);
		public delegate void FavoriteChannelChangedDelegate(object sender, string channelId, bool newState);
		public delegate void ActivatedDelegate(object sender);
		public delegate void OpenChannelDelegate(object sender, string channelName, string channelId);
		public BtnDownloadClickedDelegate BtnDownloadClicked;
		public FavoriteChannelChangedDelegate FavoriteChannelChanged;
		public ActivatedDelegate Activated;
		public OpenChannelDelegate OpenChannel;

		private bool _cancelRequired;

		public FrameYouTubeVideo(YouTubeVideo videoInfo, YouTubeVideoWebPage webPage, Control parent)
		{
			InitializeComponent();

			if (parent != null) { Parent = parent; }
			WebPage = webPage;

			SetVideoTitleFontSize(config.VideoTitleFontSize);
			SetVideoInfo(videoInfo);
		}

		public FrameYouTubeVideo(YouTubeVideo videoInfo, Control parent)
			: this(videoInfo, null, parent) { }

		private void FrameYouTubeVideo_Load(object sender, EventArgs e)
		{
			lblStatus.Text = null;
			lblProgress.Text = null;
		}

		private async void SetVideoInfo(YouTubeVideo videoInfo)
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
			UpdateVideoDateTimeIndicator();
			FavoriteItem favoriteItem = new FavoriteItem(
				videoInfo.Title, videoInfo.Title, videoInfo.Id,
				videoInfo.OwnerChannelTitle, videoInfo.OwnerChannelId, null);
			_isFavoriteVideo = FindInFavorites(favoriteItem, favoritesRootNode) != null;

			favoriteItem.DisplayName = VideoInfo.Title;
			favoriteItem.ID = VideoInfo.OwnerChannelId;
			_isFavoriteChannel = FindInFavorites(favoriteItem, favoritesRootNode) != null;
			_ciphered = VideoInfo.IsCiphered();
			_videoImageData = await Task.Run(() => DownloadVideoThumbnail());
			try
			{
				_videoImage = _videoImageData != null ? Image.FromStream(_videoImageData) : GenerateVideoThumbnailFailed(imagePreview.Width, imagePreview.Height);
				imagePreview.Refresh();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				_videoImage = GenerateVideoThumbnailFailed(imagePreview.Width, imagePreview.Height);
				_videoImageData = null;
				imagePreview.Refresh();
			}
		}

		private Stream DownloadVideoThumbnail()
		{
			FileDownloader d = new FileDownloader() { ConnectionTimeout = config.ConnectionTimeout };
			d.WorkError += (s, errorCode, errorMessage, bytesTransferred, contentLength, tryNumber, tryCountLimit) =>
			{
				System.Diagnostics.Debug.WriteLine($"Video thumbnail loading error: {errorCode} | {tryNumber} / {tryCountLimit} | {errorMessage}");
			};

			const int maxTries = 5;
			for (int i = 0; i < maxTries; ++i)
			{
				Invoke(new MethodInvoker(() =>
				{
					_videoImage = GenerateVideoThumbnailLoadingIndicator(imagePreview.Width, imagePreview.Height, i + 1, maxTries);
					_videoImageData = null;
					imagePreview.Refresh();
				}));

				Stream stream = VideoInfo.DownloadPreviewImage(d);
				if (stream != null && stream.Length > 0L)
				{
					stream.Position = 0L;
					return stream;
				}
			}
			return null;
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

					isFavoritesChanged = true;
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

					isFavoritesChanged = true;
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

					isFavoritesChanged = true;
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

					isFavoritesChanged = true;
				}
			}
			imageFavoriteChannel.Invalidate();
		}

		public void UpdateVideoDateTimeIndicator()
		{
			string datePublishedString = VideoInfo != null && IsVideoDateAvailable(VideoInfo) ?
				FormatDateTime(VideoInfo.DatePublished) : "Недоступно";
			lblDatePublished.Text = $"Дата публикации: {datePublishedString}";
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

			lblStatus.Text = "Состояние: Поиск доступных форматов...";

			videoFormats.Clear();
			hlsFormats.Clear();
			containerFormats.Clear();
			audioFormats.Clear();
			contextMenuDownloads.Items.Clear();

			LinkedList<YouTubeMediaTrack> mediaTracks = null;
			bool isExternalVideoInfoServerNeeded = config.UseExternalRestApiServerToGetDownloadUrls ||
				(config.UseExternalRestApiServerToGetAdultVideos && !VideoInfo.IsFamilySafe) ||
				VideoInfo.IsPrivate;
			string externalVideoInfoServerUrl = config.ExternalRestApiServerUrl;
			ushort externalVideoInfoServerPort = config.ExternalRestApiServerPort;
			int serverTimeout = config.ConnectionTimeoutExternalRestApiServer;
			await Task.Run(() =>
			{
				IYouTubeClient client = isExternalVideoInfoServerNeeded ?
					new YouTubeClientRestApi(
						externalVideoInfoServerUrl, externalVideoInfoServerPort,
						serverTimeout, true, WebPage) :
					(IYouTubeClient)new YouTubeClientAndroidVr();
				YouTubeStreamingDataResult streamingDataResult = YouTubeStreamingData.Get(VideoInfo.Id, client);
				if (streamingDataResult.ErrorCode == 200)
				{
					mediaTracks = new LinkedList<YouTubeMediaTrack>();
					foreach (YouTubeMediaTrack track in streamingDataResult.Data.Parse().Tracks)
					{
						mediaTracks.AddLast(track);
					}
				}
			});

			if (mediaTracks == null || mediaTracks.Count == 0)
			{
				string t = "Ссылки для скачивания не найдены!";
				lblStatus.Text = $"Состояние: Ошибка! {t}";
				if (!VideoInfo.IsFamilySafe)
				{
					t += "\nДля этого видео установлено ограничение по возрасту. " +
						"Чтобы его скачать, вам необходимо запустить " +
						"специальный веб-сервер на Python и включить его использование в настройках.\n" +
						"Скачать код сервера можно здесь:\nhttps://github.com/BlackMightyRavenDark/youtube_video_info_server_python\n" +
						"Если это не помогло, можно попробовать воспользоваться поиском по коду веб-страницы с видео.";
				}
				MessageBox.Show(t, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnDownload.Enabled = true;
				return;
			}

			videoFormats = FilterVideoTracks(mediaTracks).ToList();
			audioFormats = FilterAudioTracks(mediaTracks).ToList();
			if (VideoInfo.IsDashed)
			{
				if (config.SortDashFormatsByBitrate)
				{
					int SorterFunc(YouTubeMediaTrack x, YouTubeMediaTrack y)
					{
						if (x.AverageBitrate <= 0 || y.AverageBitrate <= 0) { return 0; }
						return x.AverageBitrate < y.AverageBitrate ? 1 : -1;
					}

					videoFormats.Sort(SorterFunc);
					audioFormats.Sort(SorterFunc);
				}
			}
			else if (config.SortFormatsByFileSize)
			{
				int SorterFunc(YouTubeMediaTrack x, YouTubeMediaTrack y)
				{
					if (x.ContentLength <= 0L || y.ContentLength <= 0L ||
						x.ContentLength == y.ContentLength)
					{
						return 0;
					}
					return x.ContentLength < y.ContentLength ? 1 : -1;
				}

				videoFormats.Sort(SorterFunc);
				audioFormats.Sort(SorterFunc);
			}

			hlsFormats = FilterHlsTracks(mediaTracks).ToList();
			if (hlsFormats.Count > 0)
			{
				hlsFormats.Sort((x, y) =>
				{
					if (x.VideoHeight <= 0 || y.VideoHeight <= 0 ||
						(x.AverageBitrate == 0 && y.AverageBitrate == 0))
					{
						return 0;
					}

					if (x.VideoHeight == y.VideoHeight)
					{
						return x.AverageBitrate < y.AverageBitrate ? 1 : -1;
					}

					return x.VideoHeight < y.VideoHeight ? 1 : -1;
				});
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

			bool showHls = hlsFormats.Count > 0 &&
				((!config.ShowHlsTracksOnlyForStreams ||
				(config.ShowHlsTracksOnlyForStreams && VideoInfo.IsLiveNow)) ||
				(videoFormats.Count + containerFormats.Count == 0));
			if (showHls)
			{
				foreach (YouTubeMediaTrackHlsStream trackHls in hlsFormats)
				{
					tableRows.Add(trackHls.ToTableRow());
				}
			}

			foreach (YouTubeMediaTrackVideo trackVideo in videoFormats)
			{
				tableRows.Add(trackVideo.ToTableRow());
			}

			containerFormats = FilterContainerTracks(mediaTracks).ToList();
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
			const string columnSeparator = " | ";

			if (table.Rows.Count > 0)
			{
				Type previousObjectType = table.Rows[0].Tag.GetType();

				foreach (TableRow row in table.Rows)
				{
					Type objectType = row.Tag.GetType();
					if (objectType != previousObjectType)
					{
						contextMenuDownloads.Items.Add("-");
						previousObjectType = objectType;
					}

					string title = row.Join(columnSeparator);
					ToolStripMenuItem mi = new ToolStripMenuItem(title) { Tag = row.Tag };
					mi.Click += MenuItemDownloadClick;
					contextMenuDownloads.Items.Add(mi);
				}

				if (contextMenuDownloads.Items.Count > 0)
				{
					contextMenuDownloads.Items.Add("-");
					ToolStripMenuItem mi = new ToolStripMenuItem("Выбрать форматы...") { Tag = null };
					mi.Click += MenuItemDownloadClick;
					contextMenuDownloads.Items.Add(mi);
				}

				lblStatus.Text = null;

				Point pt = PointToScreen(new Point(btnDownload.Left + btnDownload.Width, btnDownload.Top));
				contextMenuDownloads.Show(pt.X, pt.Y);
			}
			else
			{
				const string msg = "Ошибка построения списка форматов для скачивания!";
				lblStatus.Text = $"Состояние: {msg}";
				MessageBox.Show(msg, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			btnDownload.Enabled = true;
		}

		/// <summary>
		/// Downloads a DASH video. Warning! This method must be run in a separate thread!
		/// </summary>
		private DownloadResult DownloadDash(YouTubeMediaTrack mediaTrack, string formattedFileName, bool audioOnly)
		{
			YouTubeDashUrlList dashUrlList = config.AlwaysDownloadAsDash ?
				mediaTrack.MakeDashUrlList(config.DashManualFragmentationChunkSize) : mediaTrack.DashUrls;
			if (dashUrlList == null || dashUrlList.Count == 0)
			{
				return new DownloadResult(404, "Ссылки DASH не найдены!", null);
			}

			string mediaType = null;
			Invoke(new MethodInvoker(() =>
			{
				progressBarDownload.ClearItems();
				mediaType = mediaTrack is YouTubeMediaTrackAudio ? "аудио" : "видео";
				lblStatus.Text = $"Скачивание чанков {mediaType}:";
				lblProgress.Left = lblStatus.Left + lblStatus.Width;
				lblProgress.Text = $"0 / {dashUrlList.Count} (0.00%), {GetTrackShortInfo(mediaTrack)}";
			}));

			bool canMerge = !audioOnly && config.MergeToContainer && IsFfmpegAvailable();
			string fnDash = fnDash = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(
				(canMerge ? config.TempDirPath : config.DownloadingDirPath) +
				$"{formattedFileName}_{mediaTrack.FormatId}.{mediaTrack.FileExtension}");
			string fnDashFinal = fnDash;
			string fnDashTmp = fnDash + ".tmp";

			_cancelRequired = false;

			if (File.Exists(fnDashTmp))
			{
				File.Delete(fnDashTmp);
			}

			Stream fileStream = File.OpenWrite(fnDashTmp);
			FileDownloader _singleThreadedDownloader = new FileDownloader();
			WebHeaderCollection headers = new WebHeaderCollection()
			{
				{ "Accept", "*/*" },
				{ "User-Agent", config.UserAgent }
			};
			_singleThreadedDownloader.Headers = headers;
			int retryCountMax;
			lock (config) { retryCountMax = config.DashDownloadRetryCountMax; }
			int errorCode = 400;
			for (int i = 0; i < dashUrlList.Count; ++i)
			{
				int tryNumber = 0;
				do
				{
					Stream memChunk = new MemoryStream();
					_singleThreadedDownloader.Url = dashUrlList[i];
					if (i == 0)
					{
						_singleThreadedDownloader.Connected += (object s, string url, long contentLength,
							WebHeaderCollection _headers, int _tryNumber, int tryCountLimit, int errCode) =>
						{
							Invoke(new MethodInvoker(() =>
							{
								int chunkNumber = i + 1;
								progressBarDownload.SetItem(0, dashUrlList.Count, chunkNumber);
								double percent = 100.0 / dashUrlList.Count * chunkNumber;
								lblStatus.Text = $"[{tryNumber + 1}/{retryCountMax}] Скачивание чанков {mediaType}:";
								lblProgress.Left = lblStatus.Left + lblStatus.Width;
								lblProgress.Text = $"{chunkNumber} / {dashUrlList.Count}" +
									$" ({string.Format("{0:F2}", percent)}%), {GetTrackShortInfo(mediaTrack)}";
							}));

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
					}

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
				} while (errorCode != 200 && ++tryNumber < retryCountMax && !_cancelRequired);

				if (_cancelRequired)
				{
					errorCode = FileDownloader.DOWNLOAD_ERROR_CANCELED;
				}

				if (errorCode != 200)
				{
					break;
				}
			}
			fileStream.Dispose();

			if (errorCode == 200)
			{
				fnDashFinal = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(fnDash);
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
			if (config.AlwaysDownloadAsDash || mediaTrack.IsDashManifestPresent)
			{
				return DownloadDash(mediaTrack, formattedFileName, audioOnly);
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

				string fileUrl = mediaTrack.FileUrl.Url;

				#region Расшифровка Cipher
				//TODO: Вынести это в отдельный метод.
				if (mediaTrack.IsCiphered && !config.UseExternalRestApiServerToGetDownloadUrls)
				{
					if (string.IsNullOrEmpty(config.CipherDecryptionAlgo) || string.IsNullOrWhiteSpace(config.CipherDecryptionAlgo))
					{
						return new DownloadResult(ERROR_NO_CIPHER_DECRYPTION_ALGORITHM, null, null);
					}

					#region Внимание! Непротестированный код!
					IYouTubeMediaTrackUrlDecryptor decryptor = new TrackUrlDecryptor(config.CipherDecryptionAlgo);
					decryptor.Decrypt(mediaTrack.FileUrl);
					if (audioFormats.Count > 0)
					{
						foreach (YouTubeMediaTrack track in audioFormats)
						{
							decryptor.Decrypt(audioFormats[0].FileUrl);
						}
					}

					int timeout = config.ConnectionTimeout;
					bool allOk = await Task.Run(() =>
					{
						bool ok = MultiThreadedDownloaderLib.Utils.GetUrlResponseHttpHeaders(
							"HEAD", mediaTrack.FileUrl.Url, null, null, null, timeout, out _, out _) == 200;
						if (ok && audioFormats.Count > 0)
						{
							int[] errorCodes = GetTrackAccessibilityHttpStatusCodes(audioFormats, timeout);
							ok &= errorCodes.All(code => code == 200);
						}

						return ok;
					});

					if (!allOk)
					{
						return new DownloadResult(ERROR_CIPHER_DECRYPTION, null, null);
					}
					#endregion
				}
				#endregion

				bool useRamToStoreTemporaryFiles = config.UseRamToStoreTemporaryFiles;

				try
				{
					WebHeaderCollection requestHeaders = new WebHeaderCollection()
					{
						{ "Accept", "*/*" },
						{ "User-Agent", config.UserAgent }
					};
					_multiThreadedDownloader = new MultiThreadedDownloader();
					_multiThreadedDownloader.Headers = requestHeaders;
					_multiThreadedDownloader.ThreadCount = isVideo ? config.ThreadCountVideo : config.ThreadCountAudio;
					_multiThreadedDownloader.TryCountLimitPerThread = config.ChunkDownloadRetryCountMax;
					_multiThreadedDownloader.TryCountLimitInsideThread = config.ChunkDownloadErrorCountMax;
					_multiThreadedDownloader.Url = fileUrl;
					if (!useRamToStoreTemporaryFiles)
					{
						_multiThreadedDownloader.TempDirectory = config.TempDirPath;
					}
					_multiThreadedDownloader.UseRamForTempFiles = useRamToStoreTemporaryFiles;
					string outputFilePath = Path.Combine(isContainer || audioOnly ? config.DownloadingDirPath : config.ChunksMergingDirPath,
						isContainer ? $"{formattedFileName}.{mediaTrack.FileExtension}" :
						$"{formattedFileName}_{mediaTrack.FormatId}.{mediaTrack.FileExtension}");
					_multiThreadedDownloader.OutputFileName = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(outputFilePath);
					_multiThreadedDownloader.Preparing += (s) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							progressBarDownload.ClearItems();
							lblStatus.Text = "Состояние: Подготовка к скачиванию...";
							lblProgress.Text = null;
						}));
					};
					_multiThreadedDownloader.Connecting += (s, url, tryNumber, tryCountLimit) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							string shortInfo = isVideo || isContainer ? GetTrackShortInfo(videoTrack) : GetTrackShortInfo(mediaTrack as YouTubeMediaTrackAudio);
							lblStatus.Text = $"Состояние: Подключение... {shortInfo}";
							lblProgress.Text = null;
						}));
					};
					_multiThreadedDownloader.Connected += (object s, string url, long contentLength,
						WebHeaderCollection headers, int tryNumber, int tryCountLimit, CustomError customError) =>
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
									}

									if (mtd.UseRamForTempFiles && MemoryWatcher.Update() &&
										MemoryWatcher.RamFree < (ulong)minimumFreeSpaceRequired)
									{
										customError.ErrorMessage = "Недостаточно памяти!";
										customError.ErrorCode = MultiThreadedDownloader.DOWNLOAD_ERROR_CUSTOM;
									}
								}
							}
							else
							{
								lblStatus.Text = $"Состояние: Ошибка! Код: {customError.ErrorCode}";
							}
						}));
					};
					_multiThreadedDownloader.DownloadStarted += (s, size) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							progressBarDownload.ClearItems();
							lblStatus.Text = $"Скачивание {mediaTypeString}:";
							string shortInfo = isVideo || isContainer ? GetTrackShortInfo(videoTrack) : GetTrackShortInfo(mediaTrack as YouTubeMediaTrackAudio);
							lblProgress.Text = $"0 / {FormatSize(size)} (0.00%), {shortInfo}";
							lblProgress.Left = lblStatus.Left + lblStatus.Width;
						}));
					};
					_multiThreadedDownloader.DownloadProgress += (object s, ConcurrentDictionary<int, DownloadableTask> contentChunks) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							_contentChunks = contentChunks;
							_isVideo = isVideo;
							_isContainer = isContainer;
							_mediaTrack = mediaTrack;

							UpdateDownloadProgress();
						}));
					};
					_multiThreadedDownloader.ChunkMergingStarted += (s, chunkCount) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							progressBarDownload.ClearItems();

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

							MultipleProgressBarItem[] items = GenerateChunkMergingProgressVisualizationItems(chunkCount, chunkId, percent);
							progressBarDownload.SetItems(items);
						}));
					};
					int res = await Task.Run(() => _multiThreadedDownloader.Download(config.AccurateMultithreading));
					if (useRamToStoreTemporaryFiles)
					{
						GC.Collect();
					}

					DownloadResult downloadResult = new DownloadResult(res,
						_multiThreadedDownloader.LastErrorMessage,
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

		private void UpdateDownloadProgress()
		{
			long fileSize = _multiThreadedDownloader.ContentLength != 0L ?
				_multiThreadedDownloader.ContentLength : (_mediaTrack as YouTubeMediaTrackVideo).ContentLength;
			if (fileSize <= 0L)
			{
				//Don't do it!
				fileSize = _contentChunks.Where(chunk => chunk.Value.ChunkFileSize > 0L).Sum(chunk => chunk.Value.ChunkFileSize);
			}

			long downloadedBytes = _contentChunks.Where(chunk => chunk.Value.ProcessedBytes > 0L).Sum(chunk => chunk.Value.ProcessedBytes);
			double percent = 100.0 / fileSize * downloadedBytes;
			string percentString = string.Format("{0:F2}", percent);
			string shortInfo = _isVideo || _isContainer ?
				GetTrackShortInfo(_mediaTrack as YouTubeMediaTrackVideo) :
				GetTrackShortInfo(_mediaTrack as YouTubeMediaTrackAudio);
			lblProgress.Text = $"{FormatSize(downloadedBytes)} / {FormatSize(fileSize)}" +
				$" ({percentString}%), {shortInfo}";

			if (miMultipleToolStripMenuItem.Checked)
			{
				IEnumerable<MultipleProgressBarItem> progressBarItems = ContentChunksToMultipleProgressBarItems(_contentChunks);
				progressBarDownload.SetItems(progressBarItems);
			}
			else if (miSingleToolStripMenuItem.Checked)
			{
				progressBarDownload.SetItem((int)percent, $"{percentString}%");
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
			_cancelRequired = false;

			progressBarDownload.ClearItems();
			lblStatus.Text = null;
			lblProgress.Text = null;

			string formattedFileName = FixFileName(FormatFileName(
				IsVideoDateAvailable(VideoInfo) ?
				config.OutputFileNameFormatWithDate :
				config.OutputFileNameFormatWithoutDate, VideoInfo));

			List<YouTubeMediaTrack> tracksToDownload = new List<YouTubeMediaTrack>();

			ToolStripMenuItem mi = sender as ToolStripMenuItem;
			Type typeOfTag = mi.Tag?.GetType();
			if (typeOfTag == null)
			{
				lblStatus.Text = "Состояние: Выбор форматов...";
				List<YouTubeMediaTrack> formats = new List<YouTubeMediaTrack>();
				formats.AddRange(videoFormats);
				formats.AddRange(audioFormats);
				FormTrackSelector trackSelector = new FormTrackSelector(formats);
				DialogResult dialogResult = trackSelector.ShowDialog();
				lblStatus.Text = null;
				if (dialogResult == DialogResult.OK)
				{
					foreach (YouTubeMediaTrack mediaTrack in trackSelector.SelectedTracks)
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
			else if (typeOfTag == typeof(YouTubeMediaTrackHlsStream))
			{
				if (IsFfmpegAvailable())
				{
					lblStatus.Text = "Состояние: Запуск FFMPEG...";
					lblStatus.Refresh();

					string filePath = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(
						Path.Combine(config.DownloadingDirPath, formattedFileName + ".ts"));
					YouTubeMediaTrackHlsStream stream = mi.Tag as YouTubeMediaTrackHlsStream;
					GrabHls(stream.FileUrl.Url, filePath);
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
			else if (typeOfTag == typeof(YouTubeMediaTrackVideo))
			{
				if (config.DownloadAllAdaptiveVideoTracks)
				{
					foreach (YouTubeMediaTrack videoFormat in videoFormats)
					{
						if (videoFormat.GetType() == typeof(YouTubeMediaTrackVideo))
						{
							tracksToDownload.Add(videoFormat);
						}
					}
				}
				else
				{
					tracksToDownload.Add(mi.Tag as YouTubeMediaTrackVideo);
				}

				if (audioFormats.Count > 0)
				{
					if (config.DownloadAllAudioTracks)
					{
						foreach (YouTubeMediaTrack audioFormat in audioFormats)
						{
							if (audioFormat.GetType() == typeof(YouTubeMediaTrackAudio))
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
			else if (typeOfTag == typeof(YouTubeMediaTrackContainer))
			{
				tracksToDownload.Add(mi.Tag as YouTubeMediaTrackContainer);
			}
			else if (typeOfTag == typeof(YouTubeMediaTrackAudio))
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

			if (config.CheckUrlsAccessibilityBeforeDownloading)
			{
				lblStatus.Text = "Состояние: Проверка доступности ссылок...";
				int[] statusCodes = await Task.Run(() => GetTrackAccessibilityHttpStatusCodes(tracksToDownload, config.ConnectionTimeout));
				bool isAllUrlsAccessible = statusCodes.All(value => value == 200);
				if (!isAllUrlsAccessible)
				{
					string msg = "Ошибка! Одна или несколько ссылок для скачивания недоступны!";
					lblStatus.Text = $"Состояние: {msg}";
					MessageBox.Show($"{msg}\nПовторите попытку позже (но не слишком, а то реал можно не успеть).", "Проверятор доступности ссылок",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					btnDownload.Text = "Скачать";
					btnDownload.Enabled = true;
					IsDownloadInProgress = false;
					return;
				}
			}

			if (_cancelRequired)
			{
				string msg = "Скачивание отменено!";
				lblStatus.Text = $"Состояние: {msg}";
				MessageBox.Show(msg, "Отменятор отменения отмены",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnDownload.Text = "Скачать";
				btnDownload.Enabled = true;
				IsDownloadInProgress = false;
				return;
			}

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

					string containerFilePath = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(
						Path.Combine(config.DownloadingDirPath, $"{formattedFileName}.{ext}"));
					await MergeYouTubeMediaTracks(downloadResults, containerFilePath, config.ExtraDelayAfterContainerWasBuilt);

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
					case FileDownloader.DOWNLOAD_ERROR_CANCELED:
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

					case ERROR_NO_CIPHER_DECRYPTION_ALGORITHM:
						{
							lblStatus.Text = "Состояние: Ошибка! Не указан алгоритм для расшифровки Cipher!";
							string t = "Ссылка на это видео, зачем-то, зашифрована алгоритмом \"Cipher\", " +
								"для расшифровки которого вам требуется ввести специальную последовательность чисел, " +
								"известную одному лишь дьяволу.";
							MessageBox.Show($"{VideoInfo.Title}\nОшибка ERROR_NO_CIPHER_DECRYPTION_ALGORITHM!\n{t}", "Ошибка!",
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

					case MultiThreadedDownloader.DOWNLOAD_ERROR_CUSTOM:
						lblStatus.Text = $"Состояние: Ошибка! {downloadResult.ErrorMessage}";
						MessageBox.Show($"{VideoInfo.Title}\nСкачивание прервано!\n{downloadResult.ErrorMessage}", "Ошибатор ошибок",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					case MultiThreadedDownloader.DOWNLOAD_ERROR_CHUNK_SEQUENCE:
						lblStatus.Text = "Состояние: Ошибка последовательности чанков!";
						MessageBox.Show($"{VideoInfo.Title}\nСкачивание прервано!\nНеправильная последовательность чанков!", "Ошибатор ошибок",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						break;

					case FileDownloader.DOWNLOAD_ERROR_OUT_OF_TRIES_LEFT:
						lblStatus.Text = "Состояние: Скачивание прервано! Закончились попытки!";
						MessageBox.Show($"{VideoInfo.Title}\nСкачивание прервано!\nЗакончились попытки!", "Ошибатор ошибок",
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

			_contentChunks = null;

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
				if (result.ErrorCode != 200) { return result; }
				results.Add(result);
			}

			return result ?? new DownloadResult(MultiThreadedDownloader.DOWNLOAD_ERROR_CUSTOM,
				"Список ссылок для скачивания пуст!", null);
		}

		public void StopDownload()
		{
			_cancelRequired = true;
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
				if (VideoInfo.Duration > TimeSpan.Zero)
				{
					TimeSpan hour = TimeSpan.FromHours(1);
					string videoLength = VideoInfo.Duration.ToString(VideoInfo.Duration >= hour ? "h':'mm':'ss" : "m':'ss");
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

				bool isAdultVideo = VideoInfo.Status != null ? VideoInfo.Status.IsAdult : !VideoInfo.IsFamilySafe;
				if (isAdultVideo)
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

		private void imageFavoriteVideo_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
			if (isFavoritesLoaded && e.Button == MouseButtons.Left && VideoInfo.IsInfoAvailable)
			{
				SetFavoriteVideo(!IsFavoriteVideo);
			}
		}

		private void imageFavoriteChannel_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
			if (isFavoritesLoaded && e.Button == MouseButtons.Left && VideoInfo.IsInfoAvailable)
			{
				if (FavoriteChannelChanged != null)
				{
					FavoriteChannelChanged.Invoke(this, VideoInfo.OwnerChannelId, !IsFavoriteChannel);
				}
				else
				{
					IsFavoriteChannel = !IsFavoriteChannel;
				}

				isFavoritesChanged = true;
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

		private void progressBarDownload_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				contextMenuProgressBar.Show(Cursor.Position);
			}
		}

		public void SetVideoTitleFontSize(int fontSize)
		{
			lblVideoTitle.Font = new Font(lblVideoTitle.Font.FontFamily, fontSize);
		}

		public void SetMenusFontSize(int fontSize)
		{
			contextMenuFrameActions.SetFontSize(fontSize);
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

		private void miCopyPlayerUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (WebPage == null ||
				string.IsNullOrEmpty(WebPage.WebPageCode) ||
				string.IsNullOrWhiteSpace(WebPage.WebPageCode))
			{
				MessageBox.Show("Ошибка!\nПолучить ссылку на плеер можно только если видео было найдено через поиск по веб-странице!",
					"Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			string playerUrl = WebPage.ExtractYouTubeConfig()?.PlayerUrl;
			if (string.IsNullOrEmpty(playerUrl) || string.IsNullOrWhiteSpace(playerUrl))
			{
				MessageBox.Show("Ссылка на плеер не найдена!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			SetClipboardText(playerUrl);
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

		private void lblBtnOpenFrameContextMenu_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
			{
				Point pt = PointToScreen(new Point(
					lblBtnOpenFrameContextMenu.Left + lblBtnOpenFrameContextMenu.Width,
					lblBtnOpenFrameContextMenu.Top));
				contextMenuFrameActions.Show(pt);
			}
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

		private void miCopyFormattedFileNameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string formattedFileName = FixFileName(FormatFileName(
				IsVideoDateAvailable(VideoInfo) ?
				config.OutputFileNameFormatWithDate :
				config.OutputFileNameFormatWithoutDate, VideoInfo));
			SetClipboardText(formattedFileName);
		}

		private void miOpenImageInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (VideoInfo.Thumbnails?.Count > 0)
			{
				string url = VideoInfo.Thumbnails[0].Url;
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
			if (VideoInfo.Thumbnails?.Count == 0 ||
				string.IsNullOrEmpty(VideoInfo.Thumbnails[0].Url) || string.IsNullOrWhiteSpace(VideoInfo.Thumbnails[0].Url))
			{
				MessageBox.Show("Ссылка на изображение отсутствует!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			SetClipboardText(VideoInfo.Thumbnails[0].Url);
		}

		private void miCopyVideoIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (VideoInfo != null && !string.IsNullOrEmpty(VideoInfo.Id))
			{
				SetClipboardText(VideoInfo.Id);
			}
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
				VideoInfo.Duration, VideoInfo.DateUploaded, dateTime,
				VideoInfo.OwnerChannelTitle, VideoInfo.OwnerChannelId,
				VideoInfo.Description, VideoInfo.ViewCount, VideoInfo.Category, VideoInfo.IsShortFormat,
				VideoInfo.IsPrivate, VideoInfo.IsUnlisted, VideoInfo.IsFamilySafe,
				VideoInfo.IsLiveContent, VideoInfo.Thumbnails,
				VideoInfo.InitialSimplifiedInfo, VideoInfo.Status);

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

		private void miSingleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			miSingleToolStripMenuItem.Checked = true;
			miMultipleToolStripMenuItem.Checked = false;
			if (_contentChunks != null && _mediaTrack != null)
			{
				UpdateDownloadProgress();
			}
		}

		private void miMultipleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			miSingleToolStripMenuItem.Checked = false;
			miMultipleToolStripMenuItem.Checked = true;
			if (_contentChunks != null && _mediaTrack != null)
			{
				UpdateDownloadProgress();
			}
		}

		private async void miGetVideoWebPageCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				miGetVideoWebPageCodeToolStripMenuItem.Enabled = true;
				return;
			}

			if (WebPage != null && !string.IsNullOrEmpty(WebPage.WebPageCode))
			{
				SetClipboardText(WebPage.WebPageCode);
				MessageBox.Show("Скопировано в буфер обмена", "Код веб-страницы",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
				miGetVideoWebPageCodeToolStripMenuItem.Enabled = true;
				return;
			}

			YouTubeVideoWebPageResult webPageResult = await Task.Run(() => YouTubeVideoWebPage.Get(new YouTubeVideoId(VideoInfo.Id)));
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

			miGetVideoWebPageCodeToolStripMenuItem.Enabled = true;
		}

		private async void miGetVideoInfoToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			miGetVideoInfoToolStripMenuItem.Enabled = false;

			{
				if (WebPage != null && !string.IsNullOrEmpty(WebPage.WebPageCode))
				{
					YouTubeVideoWebPageResult videoWebPageResult = YouTubeVideoWebPage.FromCode(WebPage.WebPageCode);
					YouTubeRawVideoInfoResult rawVideoInfoResult =
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

					miGetVideoInfoToolStripMenuItem.Enabled = true;
					return;
				}
			}
			{
				YouTubeRawVideoInfoResult rawVideoInfoResult = await Task.Run(() => YouTubeRawVideoInfo.Get(VideoInfo.Id));
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

			miGetVideoInfoToolStripMenuItem.Enabled = true;
		}

		private async void miGetDownloadUrlsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			miGetDownloadUrlsToolStripMenuItem.Enabled = false;

			IYouTubeClient client = new YouTubeClientAndroidVr();
			client.SetWebPage(WebPage);
			YouTubeRawVideoInfoResult rawVideoInfoResult = await Task.Run(() => client.GetRawVideoInfo(new YouTubeVideoId(VideoInfo.Id), out _));
			if (rawVideoInfoResult.ErrorCode == 200)
			{
				YouTubeStreamingDataResult streamingDataResult = rawVideoInfoResult.RawVideoInfo.StreamingData;
				if (streamingDataResult.ErrorCode == 200)
				{
					SetClipboardText(streamingDataResult.Data.RawData);
					MessageBox.Show("Скопировано в буфер обмена", "Ссылки для скачивания",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
					miGetDownloadUrlsToolStripMenuItem.Enabled = true;
					return;
				}
			}

			MessageBox.Show("Не удалось получить ссылки для скачивания!", "Ошибка!",
				MessageBoxButtons.OK, MessageBoxIcon.Error);
			miGetDownloadUrlsToolStripMenuItem.Enabled = true;
		}

		private async void miGetDashManifestToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			miGetDashManifestToolStripMenuItem.Enabled = false;

			IYouTubeClient client = new YouTubeClientAndroidVr();
			client.SetWebPage(WebPage);
			YouTubeStreamingDataResult streamingDataResult = await Task.Run(() => YouTubeStreamingData.Get(VideoInfo.Id, client));
			if (streamingDataResult.ErrorCode == 200)
			{
				string url = streamingDataResult.Data.GetDashManifestUrl();
				if (!string.IsNullOrEmpty(url))
				{
					FileDownloader d = new FileDownloader() { Url = url };
					if (d.DownloadString(out string manifest) == 200)
					{
						SetClipboardText(manifest);
						MessageBox.Show("Скопировано в буфер обмена", "DASH manifest",
							MessageBoxButtons.OK, MessageBoxIcon.Information);
						miGetDashManifestToolStripMenuItem.Enabled = true;
						return;
					}
				}
			}

			MessageBox.Show("DASH manifest не найден!", "Ошибатор ошибок",
				MessageBoxButtons.OK, MessageBoxIcon.Error);
			miGetDashManifestToolStripMenuItem.Enabled = true;
		}

		private async void miGetHlsManifestToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			miGetHlsManifestToolStripMenuItem.Enabled = false;

			IYouTubeClient client = new YouTubeClientAndroidVr();
			client.SetWebPage(WebPage);
			YouTubeStreamingDataResult streamingDataResult = await Task.Run(() => YouTubeStreamingData.Get(VideoInfo.Id, client));
			if (streamingDataResult.ErrorCode == 200)
			{
				string url = streamingDataResult.Data.GetHlsManifestUrl();
				if (!string.IsNullOrEmpty(url))
				{
					FileDownloader d = new FileDownloader() { Url = url };
					if (d.DownloadString(out string manifest) == 200)
					{
						SetClipboardText(manifest);
						MessageBox.Show("Скопировано в буфер обмена", "HLS manifest",
							MessageBoxButtons.OK, MessageBoxIcon.Information);
						miGetHlsManifestToolStripMenuItem.Enabled = true;
						return;
					}
				}
			}

			MessageBox.Show("HLS manifest не найден!", "Ошибатор ошибок",
				MessageBoxButtons.OK, MessageBoxIcon.Error);
			miGetHlsManifestToolStripMenuItem.Enabled = false;
		}

		private async void miGetPlayerCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!VideoInfo.IsInfoAvailable)
			{
				MessageBox.Show("Видео недоступно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			miGetPlayerCodeToolStripMenuItem.Enabled = false;
			string page = WebPage?.WebPageCode;
			if (string.IsNullOrEmpty(page) || string.IsNullOrWhiteSpace(page))
			{
				YouTubeVideoId youTubeVideoId = new YouTubeVideoId(VideoInfo.Id);
				YouTubeVideoWebPageResult webPageResult = await Task.Run(() => YouTubeVideoWebPage.Get(youTubeVideoId));
				if (webPageResult.ErrorCode != 200)
				{
					MessageBox.Show("Ошибка скачивания плеера!", "Ошибатор ошибок",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					miGetPlayerCodeToolStripMenuItem.Enabled = true;
					return;
				}

				page = webPageResult.VideoWebPage.WebPageCode;
			}

			string url = ExtractPlayerUrlFromWebPageCode(page);
			if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
			{
				MessageBox.Show("Ошибка скачивания плеера!", "Ошибатор ошибок!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				miGetPlayerCodeToolStripMenuItem.Enabled = true;
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
				miGetPlayerCodeToolStripMenuItem.Enabled = true;
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
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			miGetPlayerCodeToolStripMenuItem.Enabled = true;
		}
	}
}
