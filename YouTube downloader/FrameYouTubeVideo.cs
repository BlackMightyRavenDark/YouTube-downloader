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
		public YouTubeVideo VideoInfo { get; private set; }
		public List<ThumbnailWrapper> Thumbnails { get; private set; }
		public ThumbnailWrapper ActiveThumbnail { get; private set; }
		public bool IsThumbnailLoading { get; private set; }
		public YouTubeVideoWebPage WebPage { get; }
		public YouTubeStreamingData LastReceivedStreamingData { get; private set; }
		public DateTime StreamingDataExpirationDate { get; private set; } = DateTime.MinValue;
		public bool IsFavoriteVideo { get => _isFavoriteVideo; set { SetFavoriteVideo(value); } }
		public bool IsFavoriteChannel { get => _isFavoriteChannel; set { SetFavoriteChannel(value); } }
		public bool IsDownloadInProgress { get; private set; }
		public bool IsVideoInfoFoundBySearch { get; private set; }
		public bool IsVideoInfoFoundByWebPage { get; }

		private MultiThreadedDownloader _multiThreadedDownloader;
		private ConcurrentDictionary<int, DownloadableTask> _contentChunks;
		private bool _isCiphered;
		private bool _isFavoriteVideo = false;
		private bool _isFavoriteChannel = false;
		private bool _isVideo = true;
		private bool _isContainer = false;
		private YouTubeMediaTrack _mediaTrack;
		private Image _thumbnail;
		private string _playerUrl;
		private List<YouTubeMediaTrackAudio> _audioFormats = new List<YouTubeMediaTrackAudio>();
		private List<YouTubeMediaTrackVideo> _videoFormats = new List<YouTubeMediaTrackVideo>();
		private List<YouTubeMediaTrackHlsStream> _hlsFormats = new List<YouTubeMediaTrackHlsStream>();
		private List<YouTubeMediaTrackContainer> _containerFormats = new List<YouTubeMediaTrackContainer>();

		public delegate void DownloadButtonClickedDelegate(object sender, EventArgs e);
		public delegate void FavoriteChannelChangedDelegate(object sender, string channelId, bool newState);
		public delegate void ActivatedDelegate(object sender);
		public delegate void OpenChannelDelegate(object sender, string channelName, string channelId);
		public DownloadButtonClickedDelegate DownloadButtonClicked;
		public FavoriteChannelChangedDelegate FavoriteChannelChanged;
		public ActivatedDelegate Activated;
		public OpenChannelDelegate OpenChannel;

		private bool _isCancelRequired;

		public FrameYouTubeVideo(YouTubeVideo videoInfo, YouTubeVideoWebPage webPage,
			bool isVideoInfoFoundByWebPage,
			bool automaticallyDownloadThumbnail, Control parent)
		{
			InitializeComponent();
			contextMenuDownloadFormats.Renderer = new FormatListContextMenuRenderer();

			if (parent != null) { Parent = parent; }
			WebPage = webPage;
			_playerUrl = webPage?.ExtractYouTubeConfig()?.PlayerUrl;
			IsVideoInfoFoundByWebPage = false;

			SetVideoTitleFontSize(config.VideoTitleFontSize);
			SetVideoInfo(videoInfo, automaticallyDownloadThumbnail);
		}

		public FrameYouTubeVideo(YouTubeVideo videoInfo, Control parent)
			: this(videoInfo, null, false, true, parent) { }

		private void FrameYouTubeVideo_Load(object sender, EventArgs e)
		{
			lblStatus.Text = null;
			lblDowndloadProgress.Text = null;
		}

		private void pictureBoxVideoThumbnail_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
			if (e.Button == MouseButtons.Right && (IsVideoInfoFoundBySearch || VideoInfo.IsInfoAvailable))
			{
				contextMenuThumnailImage.Show(Cursor.Position);
			}
		}

		private void pictureBoxVideoThumbnail_Paint(object sender, PaintEventArgs e)
		{
			try
			{
				Image image = ActiveThumbnail != null && ActiveThumbnail.IsImageOk ? ActiveThumbnail.Image : _thumbnail;
				if (image != null)
				{
					Rectangle imageRect = new Rectangle(0, 0, image.Width, image.Height);
					Rectangle resizedRect = imageRect.ResizeTo(pictureBoxVideoThumbnail.Size).CenterIn(pictureBoxVideoThumbnail.ClientRectangle);
					e.Graphics.DrawImage(image, resizedRect);
				}

				using (Font font = new Font("Lucida Console", 10.0f))
				{
					if (VideoInfo.Duration > TimeSpan.Zero)
					{
						TimeSpan hour = TimeSpan.FromHours(1);
						string videoLength = VideoInfo.Duration.ToString(VideoInfo.Duration >= hour ? "h':'mm':'ss" : "m':'ss");
						SizeF sz = e.Graphics.MeasureString(videoLength, font);
						float x = pictureBoxVideoThumbnail.Width - sz.Width;
						float y = pictureBoxVideoThumbnail.Height - sz.Height;
						e.Graphics.FillRectangle(Brushes.Black, x, y, sz.Width, sz.Height);
						e.Graphics.DrawString(videoLength, font, Brushes.White, x, y);
					}

					if (_isCiphered || VideoInfo.IsDashed)
					{
						string t = VideoInfo.IsDashed ? "dash" : "cipher";
						SizeF sz = e.Graphics.MeasureString(t, font);
						RectangleF rect = new RectangleF(0f, pictureBoxVideoThumbnail.Height - sz.Height, sz.Width, sz.Height);
						e.Graphics.FillRectangle(Brushes.Black, rect);
						e.Graphics.DrawString(t, font, Brushes.White, rect.X, rect.Y);
					}
					if (VideoInfo.IsLiveNow)
					{
						string t = "hls";
						SizeF sz = e.Graphics.MeasureString(t, font);
						float y = (_isCiphered || VideoInfo.IsDashed) ?
							(pictureBoxVideoThumbnail.Height - sz.Height * 2f) : (pictureBoxVideoThumbnail.Height - sz.Height);
						RectangleF rect = new RectangleF(0f, y, sz.Width, sz.Height);
						e.Graphics.FillRectangle(Brushes.Black, rect);
						e.Graphics.DrawString(t, font, Brushes.White, new PointF(rect.X, rect.Y));
					}
				}

				bool isAdultVideo = VideoInfo.Status != null ? VideoInfo.Status.IsAdult : !VideoInfo.IsFamilySafe;
				if (isAdultVideo)
				{
					e.Graphics.DrawImage(Resources.age18plus, pictureBoxVideoThumbnail.Width - 40, 0, 40, 40);
				}
				if (VideoInfo.IsUnlisted)
				{
					e.Graphics.DrawImage(Resources.unlisted, 0, 0, 40, 40);
				}
			}
#if DEBUG
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
		}

		private void pictureBoxFavoriteVideo_Paint(object sender, PaintEventArgs e)
		{
			double x = pictureBoxFavoriteVideo.Width / 2.0;
			double y = pictureBoxFavoriteVideo.Height / 2.0;
			e.Graphics.DrawStar(x, y, x, 3.0, 0.0, IsFavoriteVideo, Color.LimeGreen);
		}

		private void pictureBoxFavoriteChannel_Paint(object sender, PaintEventArgs e)
		{
			double x = pictureBoxFavoriteChannel.Width / 2.0;
			double y = pictureBoxFavoriteChannel.Height / 2.0;
			e.Graphics.DrawStar(x, y, x, 3.0, 0.0, IsFavoriteChannel, Color.LimeGreen);
		}

		private void pictureBoxFavoriteVideo_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
			if (isFavoritesLoaded && e.Button == MouseButtons.Left && (IsVideoInfoFoundBySearch || VideoInfo.IsInfoAvailable))
			{
				SetFavoriteVideo(!IsFavoriteVideo);
			}
		}

		private void pictureBoxFavoriteChannel_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
			if (isFavoritesLoaded && e.Button == MouseButtons.Left && (IsVideoInfoFoundBySearch || VideoInfo.IsInfoAvailable))
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

		private void btnDownload_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
		}

		private void lblChannelTitle_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			OpenChannel?.Invoke(this, VideoInfo.OwnerChannelTitle, VideoInfo.OwnerChannelId);
		}

		private void lblBtnOpenFrameContextMenu_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
			{
				Point point = PointToScreen(new Point(
					lblBtnOpenFrameContextMenu.Left + lblBtnOpenFrameContextMenu.Width,
					lblBtnOpenFrameContextMenu.Top));
				contextMenuFrameActions.Show(point);
			}
		}

		private void lblVideoTitle_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
			if (e.Button == MouseButtons.Right && (IsVideoInfoFoundBySearch || VideoInfo.IsInfoAvailable))
			{
				contextMenuVideoTitle.Show(Cursor.Position);
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

		private void lblDatePublished_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
			if (e.Button == MouseButtons.Right && (IsVideoInfoFoundBySearch || VideoInfo.IsInfoAvailable))
			{
				contextMenuDate.Show(Cursor.Position);
			}
		}

		private void miOpenVideoInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string url = YouTubeApiLib.Utils.GetYouTubeVideoUrl(VideoInfo.Id);
			OpenUrlInBrowser(url);
		}

		private void miCopyVideoUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string url = YouTubeApiLib.Utils.GetYouTubeVideoUrl(VideoInfo.Id);
			SetClipboardText(url);
		}

		private void miCopyPlayerUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(_playerUrl) || string.IsNullOrWhiteSpace(_playerUrl))
			{
				MessageBox.Show("Ссылка на плеер не найдена!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			SetClipboardText(_playerUrl);
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

		private void miSaveThumbnailImageAssToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				bool ok = false;
				if (ActiveThumbnail != null && ActiveThumbnail.IsImageDataOk)
				{
					using (SaveFileDialog sfd = new SaveFileDialog())
					{
						string fileNameSuffix = ActiveThumbnail.GetThumbnailFileNameSuffix();
						string fileExtension = Path.GetExtension(fileNameSuffix);

						sfd.Title = "Сохранить изображение";
						sfd.Filter = $"{fileExtension.Substring(1)}|*{fileExtension}";
						sfd.DefaultExt = fileExtension;
						sfd.AddExtension = true;
						sfd.InitialDirectory = string.IsNullOrEmpty(config.DownloadDirectory) ? config.SelfDirectory : config.DownloadDirectory;
						sfd.FileName = FixFileName(FormatFileName(
							config.OutputFileNameFormatWithDate, VideoInfo)) + fileNameSuffix;
						if (sfd.ShowDialog() == DialogResult.OK)
						{
							ok = ActiveThumbnail.ImageData.SaveToFile(sfd.FileName);
						}
					}
				}

				if (!ok)
				{
					MessageBox.Show("Невозможно сохранить изображение!", "Ошибатор ошибок",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
			}
		}

		private async void miReloadActiveThumbnailToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (ActiveThumbnail != null)
			{
				if (MessageBox.Show("Перезагрузить текущий эскиз?", "Перезагружатор текущих эскизов",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					await Task.Run(async () =>
					{
						ActiveThumbnail.Reset();
						await DownloadAndSetVideoThumbnail();
					});
				}
			}
			else
			{
				MessageBox.Show("Невозможно перезагрузить эскиз!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		private void miOpenThumbnailImageInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string url = ActiveThumbnail?.Thumbnail?.Url;
			if (!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
			{
				OpenUrlInBrowser(url);
			}
			else
			{
				MessageBox.Show("Ссылка на изображение недоступна!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void miCopyChannelUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string url = string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, VideoInfo.OwnerChannelId);
			SetClipboardText(url);
		}

		private void miCopyThumbnailUrlToolStripMenuItem_Click(object sender, EventArgs e)
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

			DateTime date = config.UseUniversalTime ? dateTime : dateTime.ToLocalTime();
			string datePublishedString = $"Дата публикации: {date:yyyy.MM.dd, HH:mm:ss}";
			if (config.UseUniversalTime) { datePublishedString += " GMT"; }

			lblDatePublished.Text = datePublishedString;
		}

		private void miCopyVideoPublishedDateToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DateTime date = config.UseUniversalTime ? VideoInfo.DatePublished : VideoInfo.DatePublished.ToLocalTime();
			string datePublishedString = date.ToString("yyyy-MM-dd HH-mm-ss");
			if (config.UseUniversalTime) { datePublishedString += " GMT"; }

			SetClipboardText(datePublishedString);
		}

		private async void miGetVideoWebPageCodeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!IsVideoInfoFoundBySearch && !VideoInfo.IsInfoAvailable)
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
			if (!IsVideoInfoFoundBySearch && !VideoInfo.IsInfoAvailable)
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
			if (!IsVideoInfoFoundBySearch && !VideoInfo.IsInfoAvailable)
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
			if (!IsVideoInfoFoundBySearch && !VideoInfo.IsInfoAvailable)
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
			if (!IsVideoInfoFoundBySearch && !VideoInfo.IsInfoAvailable)
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
			if (!IsVideoInfoFoundBySearch && !VideoInfo.IsInfoAvailable)
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
					sfd.InitialDirectory = config.DownloadDirectory;
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
#if DEBUG
				System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			miGetPlayerCodeToolStripMenuItem.Enabled = true;
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

		private async void SetVideoInfo(YouTubeVideo videoInfo, bool updateThumbnail = true)
		{
			VideoInfo = videoInfo;
			IsVideoInfoFoundBySearch = videoInfo is YouTubeVideoWrapper;

			if (!IsVideoInfoFoundBySearch && !videoInfo.IsInfoAvailable)
			{
				lblChannelTitle.Text = "Имя канала: Недоступно";
				lblDatePublished.Text = "Дата публикации: Недоступно";
				lblVideoTitle.Text = $"{videoInfo.Status.Status}, {videoInfo.Status.Reason}";
				Thumbnails = null;
				ActiveThumbnail = null;
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
			_isCiphered = VideoInfo.IsCiphered();
			RecreateThumbnailsContextMenu();
			if (updateThumbnail)
			{
				await Task.Run(() => DownloadAndSetVideoThumbnail());
			}
		}

		private void RecreateThumbnailsContextMenu()
		{
			miThumbnailsToolStripMenuItem.DropDownItems.Clear();
			Thumbnails = new List<ThumbnailWrapper>();
			ActiveThumbnail = null;
			if (VideoInfo.Thumbnails != null && VideoInfo.Thumbnails.Count > 0)
			{
				Thumbnails.Capacity = VideoInfo.Thumbnails.Count;
				int maxFileNameLength = GetMaximalThumbnailFileNameLength(VideoInfo.Thumbnails);
				foreach (YouTubeVideoThumbnail thumbnail in VideoInfo.Thumbnails)
				{
					ThumbnailWrapper thumbnailWrapper = new ThumbnailWrapper(thumbnail);
					Thumbnails.Add(thumbnailWrapper);
					string fn = (!string.IsNullOrEmpty(thumbnail.FileName) ? thumbnail.FileName : "unnamed.jpg").PadRight(maxFileNameLength);
					string menuItemTitle = $"{fn} | {thumbnail.Width}x{thumbnail.Height}";
					ToolStripMenuItem menuItem = new ToolStripMenuItem(menuItemTitle) { Tag = thumbnailWrapper };
					menuItem.Click += async (s, e) =>
					{
						if (!IsThumbnailLoading)
						{
							foreach (ToolStripMenuItem item in miThumbnailsToolStripMenuItem.DropDownItems)
							{
								item.Checked = false;
							}
							ToolStripMenuItem mi = s as ToolStripMenuItem;
							mi.Checked = true;
							ActiveThumbnail = mi.Tag as ThumbnailWrapper;
							await Task.Run(() => DownloadAndSetVideoThumbnail());
						}
					};
					miThumbnailsToolStripMenuItem.DropDownItems.Add(menuItem);
				}

				ActiveThumbnail = Thumbnails[0];
				(miThumbnailsToolStripMenuItem.DropDownItems[0] as ToolStripMenuItem).Checked = true;
			}
		}

		public async Task<bool> DownloadAndSetVideoThumbnail(ThumbnailWrapper thumbnail, int tryCountLimit)
		{
			if (!IsThumbnailLoading)
			{
				int pictureBoxVideoThumbnailWidth = 0;
				int pictureBoxVideoThumbnailHeight = 0;
				pre();
				FileDownloader d = new FileDownloader() { ConnectionTimeout = config.ConnectionTimeout };
				d.Headers.Add("User-Agent", config.UserAgent);
#if DEBUG
				d.WorkError += (s, errorCode, errorMessage, bytesTransferred, contentLength, tryNumber, _tryCountLimit) =>
				{
					System.Diagnostics.Debug.WriteLine($"Video thumbnail loading error: {errorCode} | {tryNumber} / {_tryCountLimit} | {errorMessage}");
				};
#endif
				bool ok = false;
				for (int i = 0; i < tryCountLimit; ++i)
				{
					generateAndShowLoadingIndicator(i);
					if (!thumbnail.IsImageDataOk) { thumbnail.DownloadThumbnail(d); }
					if (thumbnail.IsImageDataOk)
					{
						ok = thumbnail.IsImageOk;
						break;
					}
					if (i < tryCountLimit - 1) { await Task.Delay(1000); }
				}

				if (!ok)
				{
					_thumbnail = GenerateVideoThumbnailFailed(pictureBoxVideoThumbnailWidth, pictureBoxVideoThumbnailHeight, thumbnail);
				}

				post();
				return ok;

				#region Helper functions
				void pre()
				{
					if (InvokeRequired)
					{
						Invoke(new MethodInvoker(() => pre()));
					}
					else
					{
						IsThumbnailLoading = true;
						miReloadActiveThumbnailToolStripMenuItem.Enabled =
						miThumbnailsToolStripMenuItem.Enabled = false;
						pictureBoxVideoThumbnailWidth = pictureBoxVideoThumbnail.Width;
						pictureBoxVideoThumbnailHeight = pictureBoxVideoThumbnail.Height;
					}
				}

				void post()
				{
					if (InvokeRequired)
					{
						Invoke(new MethodInvoker(() => post()));
					}
					else
					{
						pictureBoxVideoThumbnail.Refresh();
						miReloadActiveThumbnailToolStripMenuItem.Enabled =
						miThumbnailsToolStripMenuItem.Enabled = true;
						IsThumbnailLoading = false;
					}
				}

				void generateAndShowLoadingIndicator(int tryNumber)
				{
					if (InvokeRequired)
					{
						Invoke(new MethodInvoker(() => generateAndShowLoadingIndicator(tryNumber)));
					}
					else
					{
						_thumbnail = GenerateVideoThumbnailLoadingIndicator(
							pictureBoxVideoThumbnailWidth, pictureBoxVideoThumbnailHeight, tryNumber + 1, tryCountLimit);
						pictureBoxVideoThumbnail.Refresh();
					}
				}
				#endregion
			}

			return false;
		}

		public async Task<bool> DownloadAndSetVideoThumbnail()
		{
			return await DownloadAndSetVideoThumbnail(ActiveThumbnail, 5);
		}

		public void SetFavoriteVideo(bool fav)
		{
			_isFavoriteVideo = fav;
			if (_isFavoriteVideo)
			{
				FavoriteItem favoriteItem = new FavoriteItem(
					VideoInfo.Title, VideoInfo.Title, VideoInfo.Id,
					VideoInfo.OwnerChannelTitle, VideoInfo.OwnerChannelId, favoritesRootNode)
				{
					ItemType = FavoriteItemType.Video
				};
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
			pictureBoxFavoriteVideo.Invalidate();
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
						null, null, favoritesRootNode)
					{
						ItemType = FavoriteItemType.Channel
					};
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
			pictureBoxFavoriteChannel.Invalidate();
		}

		public void UpdateVideoDateTimeIndicator()
		{
			string datePublishedString = VideoInfo != null && IsVideoDateAvailable(VideoInfo) ?
				FormatDateTime(VideoInfo.DatePublished) : "Недоступно";
			lblDatePublished.Text = $"Дата публикации: {datePublishedString}";
		}

		private async void btnDownload_Click(object sender, EventArgs e)
		{
			if (!IsVideoInfoFoundBySearch && !VideoInfo.IsInfoAvailable)
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
			DownloadButtonClicked?.Invoke(this, e);

			lblStatus.Text = "Состояние: Поиск доступных форматов...";

			_videoFormats.Clear();
			_hlsFormats.Clear();
			_containerFormats.Clear();
			_audioFormats.Clear();
			contextMenuDownloadFormats.Items.Clear();
			if (IsVideoInfoFoundBySearch)
			{
				miActionsToolStripMenuItem.Enabled = false;
			}

			LinkedList<YouTubeMediaTrack> mediaTracks = null;
			if (!miOptimizeFormatListReceivingToolStripMenuItem.Checked ||
				LastReceivedStreamingData == null || (LastReceivedStreamingData != null &&
				StreamingDataExpirationDate < DateTime.UtcNow))
			{
				bool isExternalRestApiServerNeeded = config.UseExternalRestApiServerToGetDownloadUrls ||
					(config.UseExternalRestApiServerToGetAdultVideos && !VideoInfo.IsFamilySafe) ||
					VideoInfo.IsPrivate;
				string externalRestApiServerUrl = config.ExternalRestApiServerUrl;
				ushort externalRestApiServerPort = config.ExternalRestApiServerPort;
				int timeout = config.ConnectionTimeoutExternalRestApiServer;
				await Task.Run(() =>
				{
					IYouTubeClient client = isExternalRestApiServerNeeded ?
						new YouTubeClientRestApi(
							externalRestApiServerUrl, externalRestApiServerPort,
							timeout, true, WebPage) :
						(IYouTubeClient)new YouTubeClientAndroidVr();
					YouTubeRawVideoInfoResult rawVideoInfoResult = client.GetRawVideoInfo(new YouTubeVideoId(VideoInfo.Id), out _);
					if (rawVideoInfoResult.ErrorCode == 200)
					{
						if (IsVideoInfoFoundBySearch)
						{
							Invoke(new MethodInvoker(() =>
							{
								SetVideoInfo(client.WebPage.GetVideo());
								_playerUrl = client.WebPage.ExtractYouTubeConfig()?.PlayerUrl;
							}));
						}

						YouTubeStreamingDataResult streamingDataResult = rawVideoInfoResult.RawVideoInfo.StreamingData;
						if (streamingDataResult.ErrorCode == 200)
						{
							LastReceivedStreamingData = streamingDataResult.Data;
							StreamingDataExpirationDate = streamingDataResult.Data.DateReceived.AddSeconds(streamingDataResult.Data.GetLifeTimeSeconds());

							mediaTracks = new LinkedList<YouTubeMediaTrack>();
							foreach (YouTubeMediaTrack track in streamingDataResult.Data.Parse().Tracks)
							{
								mediaTracks.AddLast(track);
							}
						}
					}
				});
			}
			else if (LastReceivedStreamingData != null)
			{
				mediaTracks = new LinkedList<YouTubeMediaTrack>();
				foreach (YouTubeMediaTrack track in LastReceivedStreamingData.Parse().Tracks)
				{
					mediaTracks.AddLast(track);
				}
			}
			else
			{
				LastReceivedStreamingData = null;
				StreamingDataExpirationDate = DateTime.MinValue;

				lblStatus.Text = "Состояние: Ошибка поиска доступных форматов!";
				MessageBox.Show("Не удалось получить список форматов! Попытайтесь ещё раз!\n" +
					"Если это не помогает, попробуйте отключить галочку \"Оптимизировать получение списка форматов\" в меню \"Меню\".",
					"Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnDownload.Enabled = true;
				miThumbnailsToolStripMenuItem.Enabled = miThumbnailsToolStripMenuItem.DropDownItems.Count > 0;
				miActionsToolStripMenuItem.Enabled = true;
				return;
			}

			if (mediaTracks == null || mediaTracks.Count == 0)
			{
				string t = "Ссылки для скачивания не найдены!";
				lblStatus.Text = $"Состояние: Ошибка! {t}";
				if (!VideoInfo.IsFamilySafe)
				{
					t += "\nДля этого видео установлено ограничение по возрасту. " +
						"Чтобы его скачать, вам необходимо запустить " +
						"специальный локальный веб-сервер на JavaScript и включить его использование в настройках.\n" +
						"Скачать код сервера можно здесь:\n" +
						"https://github.com/BlackMightyRavenDark/youtube_rest_api_server_node_js\n" +
						"Если это не помогло, можно попробовать воспользоваться поиском по коду веб-страницы с видео.";
				}
				MessageBox.Show(t, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnDownload.Enabled = true;
				miThumbnailsToolStripMenuItem.Enabled = miThumbnailsToolStripMenuItem.DropDownItems.Count > 0;
				miActionsToolStripMenuItem.Enabled = true;
				return;
			}

			_videoFormats = FilterVideoTracks(mediaTracks).ToList();
			_audioFormats = FilterAudioTracks(mediaTracks).ToList();
			if (VideoInfo.IsDashed)
			{
				if (config.SortDashFormatsByBitrate)
				{
					int SorterFunc(YouTubeMediaTrack x, YouTubeMediaTrack y)
					{
						if (x.AverageBitrate <= 0 || y.AverageBitrate <= 0) { return 0; }
						return x.AverageBitrate < y.AverageBitrate ? 1 : -1;
					}

					_videoFormats.Sort(SorterFunc);
					_audioFormats.Sort(SorterFunc);
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

				_videoFormats.Sort(SorterFunc);
				_audioFormats.Sort(SorterFunc);
			}

			_hlsFormats = FilterHlsTracks(mediaTracks).ToList();
			if (_hlsFormats.Count > 1)
			{
				_hlsFormats.Sort((x, y) =>
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

			if (config.AlwaysMoveAudioId140ToTopOfList && _audioFormats.Count > 1)
			{
				for (int i = 0; i < _audioFormats.Count; ++i)
				{
					if (_audioFormats[i].FormatId == 140)
					{
						if (i != 0)
						{
							(_audioFormats[i], _audioFormats[0]) = (_audioFormats[0], _audioFormats[i]);
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

			bool showHls = _hlsFormats.Count > 0 &&
				((!config.ShowHlsTracksOnlyForStreams ||
				(config.ShowHlsTracksOnlyForStreams && VideoInfo.IsLiveNow)) ||
				(_videoFormats.Count + _containerFormats.Count == 0));
			if (showHls)
			{
				foreach (YouTubeMediaTrackHlsStream trackHls in _hlsFormats)
				{
					tableRows.Add(trackHls.ToTableRow());
				}
			}

			foreach (YouTubeMediaTrackVideo trackVideo in _videoFormats)
			{
				tableRows.Add(trackVideo.ToTableRow());
			}

			_containerFormats = FilterContainerTracks(mediaTracks).ToList();
			foreach (YouTubeMediaTrackContainer trackContainer in _containerFormats)
			{
				tableRows.Add(trackContainer.ToTableRow());
			}

			foreach (YouTubeMediaTrackAudio trackAudio in _audioFormats)
			{
				tableRows.Add(trackAudio.ToTableRow());
			}

			Table table = new Table(tableRows, tableColumns);
			table.Format();
			const string columnSeparator = " | ";

			if (table.Rows.Count > 0)
			{
				foreach (TableRow row in table.Rows)
				{
					string title = row.Join(columnSeparator);
					ToolStripMenuItem mi = new ToolStripMenuItem(title)
					{
						Padding = new Padding(0, 4, 0, 4),
						Tag = row.Tag
					};
					mi.Click += MenuItemDownloadClick;
					contextMenuDownloadFormats.Items.Add(mi);
				}

				if (contextMenuDownloadFormats.Items.Count > 0)
				{
					ToolStripMenuItem mi = new ToolStripMenuItem("Выбрать форматы...") { Tag = null };
					mi.Click += MenuItemDownloadClick;
					contextMenuDownloadFormats.Items.Add(mi);
				}

				lblStatus.Text = null;

				Point point = PointToScreen(new Point(btnDownload.Left + btnDownload.Width, btnDownload.Top));
				contextMenuDownloadFormats.Show(point);
			}
			else
			{
				const string msg = "Ошибка построения списка форматов для скачивания!";
				lblStatus.Text = $"Состояние: {msg}";
				MessageBox.Show(msg, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			btnDownload.Enabled = true;
			miThumbnailsToolStripMenuItem.Enabled = miThumbnailsToolStripMenuItem.DropDownItems.Count > 0;
			miActionsToolStripMenuItem.Enabled = true;
		}

		/// <summary>
		/// Downloads a DASH video. Warning! This method must be run in a separate thread!
		/// </summary>
		private DownloadResult DownloadDash(YouTubeMediaTrack mediaTrack, string formattedFileName, bool audioOnly)
		{
			_isCancelRequired = false;

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
				lblDowndloadProgress.Left = lblStatus.Left + lblStatus.Width;
				lblDowndloadProgress.Text = $"0 / {dashUrlList.Count} (0.00%), {GetTrackShortInfo(mediaTrack)}";
			}));

			try
			{
				bool canMerge = !audioOnly && config.AutomaticallyMergeToContainer && IsFfmpegAvailable();
				string fnDash = fnDash = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(
					(canMerge ? config.TemporaryDirectory : config.DownloadDirectory) +
					$"{formattedFileName}_{mediaTrack.FormatId}.{mediaTrack.FileExtension}");
				string fnDashFinal = fnDash;
				string fnDashTmp = fnDash + ".tmp";

				if (File.Exists(fnDashTmp)) { File.Delete(fnDashTmp); }
				if (File.Exists(fnDashTmp))
				{
					return new DownloadResult(
						FileDownloader.DOWNLOAD_ERROR_OUTPUT_STREAM_NOT_ASSIGNED,
						"Unable to delete existed temporary file", null);
				}

				int errorCode = MultiThreadedDownloader.DOWNLOAD_ERROR_UNDEFINED;
				string errorMessage = null;
				using (Stream outputStream = File.OpenWrite(fnDashTmp))
				{
					using (FileDownloader downloader = new FileDownloader()
					{
						Headers = new WebHeaderCollection()
						{
							{ "Accept", "*/*" },
							{ "User-Agent", config.UserAgent }
						}
					})
					{
						int tryCountLimit;
						lock (config) { tryCountLimit = config.DashChunkDownloadTryCountLimit; }
						for (int i = 0; i < dashUrlList.Count; ++i)
						{
							int tryNumber = 0;
							do
							{
								Stream memChunk = new MemoryStream();
								downloader.Url = dashUrlList[i];
								if (i == 0)
								{
									downloader.Connected += (object s, string url, long contentLength,
										WebHeaderCollection _headers, int _tryNumber, int _tryCountLimit, int errCode) =>
									{
										Invoke(new MethodInvoker(() =>
										{
											int chunkNumber = i + 1;
											progressBarDownload.SetItem(0, dashUrlList.Count, chunkNumber);
											double percent = 100.0 / dashUrlList.Count * chunkNumber;
											lblStatus.Text = $"[{tryNumber + 1}/{tryCountLimit}] Скачивание чанков {mediaType}:";
											lblDowndloadProgress.Left = lblStatus.Left + lblStatus.Width;
											lblDowndloadProgress.Text = $"{chunkNumber} / {dashUrlList.Count}" +
												$" ({string.Format("{0:F2}", percent)}%), {GetTrackShortInfo(mediaTrack)}";
										}));

										if (errCode == 200 || errCode == 206)
										{
											if (contentLength > 0L)
											{
												try
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
#if DEBUG
												catch (Exception ex)
												{
													System.Diagnostics.Debug.WriteLine(ex.Message);
#else
												catch
												{
#endif
													return FileDownloader.DOWNLOAD_ERROR_DRIVE_NOT_READY;
												}
											}
										}

										return errCode;
									};
								}

								errorCode = downloader.Download(memChunk);
								if (errorCode != 200)
								{
									memChunk.Dispose();
									continue;
								}
								memChunk.Position = 0L;
								bool appended = StreamAppender.Append(memChunk, outputStream);
								memChunk.Dispose();
								if (!appended)
								{
									return new DownloadResult(MultiThreadedDownloader.DOWNLOAD_ERROR_MERGING_CHUNKS, null, fnDashTmp);
								}
							}
							while (errorCode != 200 && ++tryNumber < tryCountLimit && !_isCancelRequired);

							if (_isCancelRequired)
							{
								errorCode = FileDownloader.DOWNLOAD_ERROR_CANCELED;
							}

							if (errorCode != 200) { break; }
						}
						errorMessage = downloader.LastErrorMessage;
					}
				}

				if (errorCode == 200)
				{
					fnDashFinal = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(fnDash);
					File.Move(fnDashTmp, fnDashFinal);
				}
				return new DownloadResult(errorCode, errorMessage, fnDashFinal);
			}
			catch (Exception ex)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
				return new DownloadResult(ex.HResult, ex.Message, null);
			}
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
				try
				{
					YouTubeMediaTrackVideo videoTrack = mediaTrack as YouTubeMediaTrackVideo;
					bool isVideo = videoTrack != null;
					if (isVideo) { audioOnly = false; }
					bool isContainer = mediaTrack is YouTubeMediaTrackContainer;
					string mediaTypeString = isVideo || isContainer ? "видео" : "аудио";

					string fileUrl = mediaTrack.FileUrl.Url;

					#region Расшифровка Cipher
					//TODO: Вынести это в отдельный метод.
					if (mediaTrack.IsCiphered && !config.UseExternalRestApiServerToGetDownloadUrls)
					{
						if (string.IsNullOrEmpty(config.CipherDecryptionAlgorythm) || string.IsNullOrWhiteSpace(config.CipherDecryptionAlgorythm))
						{
							return new DownloadResult(ERROR_NO_CIPHER_DECRYPTION_ALGORITHM, null, null);
						}

						#region Внимание! Непротестированный код!
						IYouTubeMediaTrackUrlDecryptor decryptor = new TrackUrlDecryptor(config.CipherDecryptionAlgorythm);
						decryptor.Decrypt(mediaTrack.FileUrl);
						if (_audioFormats.Count > 0)
						{
							foreach (YouTubeMediaTrack track in _audioFormats)
							{
								decryptor.Decrypt(_audioFormats[0].FileUrl);
							}
						}

						int timeout = config.ConnectionTimeout;
						bool allOk = await Task.Run(() =>
						{
							bool ok = MultiThreadedDownloaderLib.Utils.GetUrlResponseHttpHeaders(
								"HEAD", mediaTrack.FileUrl.Url, null, null, null, timeout, out _, out _) == 200;
							if (ok && _audioFormats.Count > 0)
							{
								int[] errorCodes = GetTrackAccessibilityHttpStatusCodes(_audioFormats, timeout);
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
					WebHeaderCollection requestHeaders = new WebHeaderCollection()
					{
						{ "Accept", "*/*" },
						{ "User-Agent", config.UserAgent }
					};

					string outputFilePath = Path.Combine(isContainer || audioOnly ? config.DownloadDirectory : config.ChunkMergerDirectory,
						isContainer ? $"{formattedFileName}.{mediaTrack.FileExtension}" :
						$"{formattedFileName}_{mediaTrack.FormatId}.{mediaTrack.FileExtension}");
					_multiThreadedDownloader = new MultiThreadedDownloader()
					{
						Url = fileUrl,
						Headers = requestHeaders,
						ThreadCount = isVideo ? config.ThreadCountVideo : config.ThreadCountAudio,
						TryCountLimitPerThread = config.ChunkDownloadTryCountLimit,
						TryCountLimitInsideThread = config.ChunkDownloadInnerErrorCountLimit,
						UseRamForTempFiles = useRamToStoreTemporaryFiles,
						OutputFileName = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(outputFilePath)
					};
					if (!useRamToStoreTemporaryFiles)
					{
						_multiThreadedDownloader.TempDirectory = config.TemporaryDirectory;
					}
					_multiThreadedDownloader.Preparing += s =>
					{
						Invoke(new MethodInvoker(() =>
						{
							progressBarDownload.ClearItems();
							lblStatus.Text = "Состояние: Подготовка к скачиванию...";
							lblDowndloadProgress.Text = null;
						}));
					};
					_multiThreadedDownloader.Connecting += (s, url, tryNumber, tryCountLimit) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							string shortInfo = isVideo || isContainer ? GetTrackShortInfo(videoTrack) : GetTrackShortInfo(mediaTrack as YouTubeMediaTrackAudio);
							lblStatus.Text = $"Состояние: Подключение... {shortInfo}";
							lblDowndloadProgress.Text = null;
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
							lblDowndloadProgress.Text = $"0 / {FormatSize(size)} (0.00%), {shortInfo}";
							lblDowndloadProgress.Left = lblStatus.Left + lblStatus.Width;
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
							lblDowndloadProgress.Text = $"0 / {chunkCount}";
							lblDowndloadProgress.Left = lblStatus.Left + lblStatus.Width;
						}));
					};
					_multiThreadedDownloader.ChunkMergingProgress += (s, chunkId, chunkCount, chunkPosition, chunkSize) =>
					{
						Invoke(new MethodInvoker(() =>
						{
							double percent = 100.0 / chunkSize * chunkPosition;
							string percentString = string.Format("{0:F2}", percent);
							lblDowndloadProgress.Text = $"{chunkId + 1} / {_multiThreadedDownloader.ThreadCount}: " +
								$"{FormatSize(chunkPosition)} / {FormatSize(chunkSize)} ({percentString}%)";

							MultipleProgressBarItem[] items = GenerateChunkMergingProgressVisualizationItems(chunkCount, chunkId, percent);
							progressBarDownload.SetItems(items);
						}));
					};

					int res = await Task.Run(() => _multiThreadedDownloader.Download(config.UseAccurateMultithreading));
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
				}
				catch (Exception ex)
				{
#if DEBUG
					System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
					DownloadResult downloadResult = new DownloadResult(
						ex.HResult, ex.Message, _multiThreadedDownloader?.OutputFileName);
					if (_multiThreadedDownloader != null)
					{
						_multiThreadedDownloader.Dispose();
						_multiThreadedDownloader = null;
					}
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
				// Don't do it!
				fileSize = _contentChunks.Where(chunk => chunk.Value.ChunkFileSize > 0L).Sum(chunk => chunk.Value.ChunkFileSize);
			}

			long downloadedBytes = _contentChunks.Where(chunk => chunk.Value.ProcessedBytes > 0L).Sum(chunk => chunk.Value.ProcessedBytes);
			double percent = 100.0 / fileSize * downloadedBytes;
			string percentString = string.Format("{0:F2}", percent);
			string shortInfo = _isVideo || _isContainer ?
				GetTrackShortInfo(_mediaTrack as YouTubeMediaTrackVideo) :
				GetTrackShortInfo(_mediaTrack as YouTubeMediaTrackAudio);
			lblDowndloadProgress.Text = $"{FormatSize(downloadedBytes)} / {FormatSize(fileSize)}" +
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

			if (string.IsNullOrEmpty(config.DownloadDirectory) || string.IsNullOrWhiteSpace(config.DownloadDirectory))
			{
				MessageBox.Show("Не указана папка для скачивания!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnDownload.Enabled = true;
				return;
			}
			if (!Directory.Exists(config.DownloadDirectory))
			{
				MessageBox.Show("Папка для скачивания не найдена!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnDownload.Enabled = true;
				return;
			}
			if (!config.UseRamToStoreTemporaryFiles)
			{
				if (string.IsNullOrEmpty(config.TemporaryDirectory) || string.IsNullOrWhiteSpace(config.TemporaryDirectory))
				{
					MessageBox.Show("Не указана папка для временных файлов!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					btnDownload.Enabled = true;
					return;
				}
				if (!Directory.Exists(config.TemporaryDirectory))
				{
					MessageBox.Show("Папка для временных файлов не найдена!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					btnDownload.Enabled = true;
					return;
				}
			}
			if (!string.IsNullOrEmpty(config.ChunkMergerDirectory) &&
				!string.IsNullOrWhiteSpace(config.ChunkMergerDirectory) &&
				!Directory.Exists(config.ChunkMergerDirectory))
			{
				MessageBox.Show("Папка для объединения чанков не найдена!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnDownload.Enabled = true;
				return;
			}

			IsDownloadInProgress = true;
			_isCancelRequired = false;

			progressBarDownload.ClearItems();
			lblStatus.Text = null;
			lblDowndloadProgress.Text = null;

			List<YouTubeMediaTrack> tracksToDownload = new List<YouTubeMediaTrack>();

			ToolStripMenuItem mi = sender as ToolStripMenuItem;
			Type typeOfTag = mi.Tag?.GetType();
			if (typeOfTag == null)
			{
				lblStatus.Text = "Состояние: Выбор форматов...";
				List<YouTubeMediaTrack> formats = new List<YouTubeMediaTrack>();
				formats.AddRange(_videoFormats);
				formats.AddRange(_audioFormats);
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

					string fn = GetNumberedFixedOutputFileNameWithotExtension(VideoInfo, null);
					string filePath = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(
						Path.Combine(config.DownloadDirectory, fn + ".ts"));
					YouTubeMediaTrackHlsStream stream = mi.Tag as YouTubeMediaTrackHlsStream;
					GrabHls(stream.FileUrl.Url, filePath);
					lblStatus.Text = null;
				}
				else
				{
					string ffmpegMsg = "Не удалось запустить FFMPEG!";
					lblStatus.Text = $"Состояние: {ffmpegMsg}";
					MessageBox.Show(ffmpegMsg, "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				IsDownloadInProgress = false;
				btnDownload.Enabled = true;
				return;
			}
			else if (typeOfTag == typeof(YouTubeMediaTrackVideo))
			{
				if (config.AutomaticallyDownloadAllAdaptiveVideoTracks)
				{
					foreach (YouTubeMediaTrack videoFormat in _videoFormats)
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

				if (_audioFormats.Count > 0)
				{
					if (config.AutomaticallyDownloadAllAdaptiveAudioTracks)
					{
						foreach (YouTubeMediaTrack audioFormat in _audioFormats)
						{
							if (audioFormat.GetType() == typeof(YouTubeMediaTrackAudio))
							{
								tracksToDownload.Add(audioFormat);
							}
						}
					}
					else
					{
						if (config.AutomaticallyDownloadFirstAudioTrack)
						{
							tracksToDownload.Add(_audioFormats[0]);
						}
						if (config.AutomaticallyDownloadSecondAudioTrack && _audioFormats.Count > 1)
						{
							if (config.AutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger)
							{
								if (_audioFormats[1].ContentLength > _audioFormats[0].ContentLength)
								{
									tracksToDownload.Add(_audioFormats[1]);
								}
								else if (!config.AutomaticallyDownloadFirstAudioTrack)
								{
									tracksToDownload.Add(_audioFormats[0]);
								}
							}
							else
							{
								tracksToDownload.Add(_audioFormats[1]);
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

			long summaryFileSize = tracksToDownload.Sum(track => track.ContentLength);
			long minimumFreeSpaceRequired = summaryFileSize > 0L ? (long)(summaryFileSize * 1.1) : 0L;
			if (minimumFreeSpaceRequired > 0L)
			{
				MultiThreadedDownloader tempDownloader = new MultiThreadedDownloader()
				{
					OutputFileName = Path.Combine(config.DownloadDirectory, "temp.tmp"),
					UseRamForTempFiles = config.UseRamToStoreTemporaryFiles
				};
				if (config.UseRamToStoreTemporaryFiles)
				{
					tempDownloader.TempDirectory = config.TemporaryDirectory;
				}

				List<char> driveLetters = tempDownloader.GetUsedDriveLetters();
				tempDownloader.Dispose();

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
					!AdvancedFreeDiskSpaceCheckPassed(summaryFileSize))
				{
					lblStatus.Text = "Состояние: Ошибка: Недостаточно места на диске!";
					MessageBox.Show("Недостаточно места на диске или произошла ошибка при обращении к одному из указанных дисков!", "Ошибка!",
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
				if (config.AutomaticallyMergeToContainer && !IsFfmpegAvailable())
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

			if (config.CheckUrlsAccessibilityBeforeDownloadStarted)
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

			if (_isCancelRequired)
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
			string containerFileExtension = GetContainerFileExtension(tracksToDownload);
			string formattedFileName = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(
				GetNumberedFixedOutputFileNameWithotExtension(VideoInfo, containerFileExtension, ActiveThumbnail));
			DownloadResult downloadResult = await Task.Run(() =>
				DownloadTracks(tracksToDownload, formattedFileName, audioOnly, downloadResults));

			lblDowndloadProgress.Text = null;
			lblDowndloadProgress.Refresh();

			if (downloadResult.ErrorCode == 200)
			{
				if (config.AutomaticallyMergeToContainer && needToMerge && !audioOnly && IsFfmpegAvailable())
				{
					btnDownload.Enabled = false;
					if (minimumFreeSpaceRequired > 0L)
					{
						try
						{
							DriveInfo driveInfo = new DriveInfo(config.DownloadDirectory[0].ToString());
							if (driveInfo.AvailableFreeSpace < minimumFreeSpaceRequired)
							{
								lblStatus.Text = "Состояние: Ошибка: Недостаточно места на диске!";
								string msg = "Недостаточно места на диске для сборки контейнера! " +
									$"Оригинальные файлы сохранены в папку\n{config.ChunkMergerDirectory}";
								MessageBox.Show(msg, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);

								IsDownloadInProgress = false;
								btnDownload.Text = "Скачать";
								btnDownload.Enabled = true;
								return;
							}
						}
#if DEBUG
						catch (Exception ex)
						{
							System.Diagnostics.Debug.WriteLine(ex.Message);
#else
						catch
						{
#endif
							string msg = "Внимание (ахтунг)! Произошла ошибка при проверке свободного места! " +
								$"Будьте бдительны! Сейчас места на диске {char.ToUpper(config.DownloadDirectory[0])}: может не хватить!";
							MessageBox.Show(msg, "Обращатор на себя внимания",
								MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}

					lblStatus.Text = $"Состояние: Создание контейнера {containerFileExtension.Substring(1).ToUpper()}...";
					lblStatus.Refresh();

					string containerFilePath = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(
						Path.Combine(config.DownloadDirectory, formattedFileName + containerFileExtension));
					await Task.Run(() => MergeYouTubeMediaTracks(downloadResults, containerFilePath, config.ExtraDelayAfterContainerWasBuilt));

					if (config.DeleteSourceFilesWhenMerged)
					{
						foreach (DownloadResult dr in downloadResults)
						{
							try
							{
								if (File.Exists(dr.OutputFilePath))
								{
									File.Delete(dr.OutputFilePath);
								}
							}
#if DEBUG
							catch (Exception ex)
							{
								System.Diagnostics.Debug.WriteLine(ex.Message);
							}
#else
							catch { }
#endif
						}
					}
				}

				// Сохранение эскиза видео.
				if (config.AutomaticallySaveVideoThumbnailImage && ActiveThumbnail != null && ActiveThumbnail.IsImageDataOk)
				{
					if (!SaveThumbnailToFile(ActiveThumbnail, formattedFileName))
					{
						MessageBox.Show("Внимание! Не удалось сохранить картинку от видео!", "Обращатор на себя внимания",
							MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
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
			_isCancelRequired = true;
			_multiThreadedDownloader?.Stop();
		}

		public void SetVideoTitleFontSize(int fontSize)
		{
			lblVideoTitle.Font = new Font(lblVideoTitle.Font.FontFamily, fontSize);
		}

		public void SetMenusFontSize(int fontSize)
		{
			contextMenuFrameActions.SetFontSize(fontSize);
			contextMenuThumnailImage.SetFontSize(fontSize);
			contextMenuVideoTitle.SetFontSize(fontSize);
			contextMenuChannelTitle.SetFontSize(fontSize);
			contextMenuDate.SetFontSize(fontSize);
			contextMenuDownloadFormats.SetFontSize(fontSize);
		}
	}
}
