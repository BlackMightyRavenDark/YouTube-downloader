using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using BrightIdeasSoftware;
using MultiThreadedDownloaderLib;
using YouTubeApiLib;

namespace YouTube_downloader
{
	public static class Utils
	{
		public const string YOUTUBE_ENDPOINT_WATCH_URL = "https://www.youtube.com/watch";
		public const string YOUTUBE_ENDPOINT_SEARCH_URL = "https://www.googleapis.com/youtube/v3/search";
		public const string YOUTUBE_CHANNEL_URL_TEMPLATE = "https://www.youtube.com/channel/{0}/videos";

		public static List<YouTubeChannelInfo> channelInfos = new List<YouTubeChannelInfo>();
		public static List<YouTubeVideo> videos = new List<YouTubeVideo>();
		public static List<FrameYouTubeChannel> framesChannel = new List<FrameYouTubeChannel>();
		public static List<FrameYouTubeVideo> framesVideo = new List<FrameYouTubeVideo>();

		public static TreeListView treeFavorites = null;
		public static FavoriteItem favoritesRootNode = null;
		public static readonly Configurator config = new Configurator();

		public static readonly bool Is64BitProcess = Environment.Is64BitProcess;

		public const int ERROR_CIPHER_DECRYPTION = -100;
		public const int ERROR_NO_CIPHER_DECRYPTION_ALGORITHM = -101;

		internal static bool isFavoritesLoaded = false;
		internal static bool isFavoritesChanged = false;

		public static int DownloadString(string url, out string responseString, bool useRequestSender = false)
		{
			try
			{
				if (useRequestSender)
				{
					using (HttpRequestResult requestResult = HttpRequestSender.Send(url))
					{
						if (requestResult.ErrorCode == 200)
						{
							return requestResult.WebContent.ContentToString(out responseString);
						}

						responseString = requestResult.ErrorMessage;
						return requestResult.ErrorCode;
					}
				}
				else
				{
					FileDownloader d = new FileDownloader() { Url = url };
					return d.DownloadString(out responseString);
				}
			}
			catch (Exception ex)
			{
				responseString = ex.Message;
				return ex.HResult;
			}
		}

		public static int DownloadData(string url, Stream outputStream)
		{
			FileDownloader d = new FileDownloader() { Url = url };
			return d.Download(outputStream);
		}

		public static string GetYouTubeChannelVideosRequestUrl(string channelId, int maxVideos,
			DateTime publishedAfter, DateTime publishedBefore)
		{
			NameValueCollection query = System.Web.HttpUtility.ParseQueryString(string.Empty);
			query.Add("part", "snippet");
			query.Add("type", "video");
			query.Add("order", "date");
			query.Add("key", config.YouTubeApiV3Key);
			query.Add("channelId", channelId);
			query.Add("maxResults", maxVideos.ToString());
			if (publishedAfter < DateTime.MaxValue)
			{
				string dateAfter = publishedAfter.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
				query.Add("publishedAfter", dateAfter);
			}
			if (publishedBefore < DateTime.MaxValue)
			{
				string dateBefore = publishedBefore.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
				query.Add("publishedBefore", dateBefore);
			}

			string url = $"{YOUTUBE_ENDPOINT_SEARCH_URL}?{query}";
			return url;
		}

		public static string GetYouTubeSearchQueryRequestUrl(
			string searchingPhrase, string resultTypes, int maxResults,
			DateTime publishedAfter, DateTime publishedBefore)
		{
			NameValueCollection query = System.Web.HttpUtility.ParseQueryString(string.Empty);
			query.Add("part", "snippet");
			query.Add("order", "date");
			query.Add("key", config.YouTubeApiV3Key);
			query.Add("q", searchingPhrase);
			query.Add("maxResults", maxResults.ToString());
			if (!string.IsNullOrEmpty(resultTypes))
			{
				query.Add("type", resultTypes);
			}
			if (publishedAfter < DateTime.MaxValue)
			{
				string dateAfter = publishedAfter.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
				query.Add("publishedAfter", dateAfter);
			}
			if (publishedBefore < DateTime.MaxValue)
			{
				string dateBefore = publishedBefore.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
				query.Add("publishedBefore", dateBefore);
			}

			string url = $"{YOUTUBE_ENDPOINT_SEARCH_URL}?{query}";
			return url;
		}

		public static YouTubeVideo GetSingleVideo(YouTubeVideoId videoId, out string errorMessage)
		{
			const string clientId = "web_page";
			IYouTubeClient client = config.UseExternalRestApiServerToGetBasicVideoInfo ?
				new YouTubeClientRestApi(config.ExternalRestApiServerUrl, config.ExternalRestApiServerPort,
					config.ConnectionTimeoutExternalRestApiServer, false) :
				YouTubeApi.GetYouTubeClient(clientId);
			if (client != null)
			{
				YouTubeRawVideoInfoResult rawVideoInfoResult = client.GetRawVideoInfo(videoId, out errorMessage);
				if (rawVideoInfoResult.ErrorCode == 200)
				{
					return rawVideoInfoResult.RawVideoInfo.ToVideo();
				}
			}
			else
			{
				errorMessage = $"The client with ID '{clientId}' is not found! The default client was also failed! Let's cry, baby :'(";
			}

			return null;
		}

		public static IEnumerable<YouTubeMediaTrackVideo> FilterVideoTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			Type typeOfTrack = typeof(YouTubeMediaTrackVideo);
			return mediaTracks.Where(track => track.GetType() == typeOfTrack).Cast<YouTubeMediaTrackVideo>();
		}

		public static IEnumerable<YouTubeMediaTrackHlsStream> FilterHlsTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			Type typeOfTrack = typeof(YouTubeMediaTrackHlsStream);
			return mediaTracks.Where(track => track.GetType() == typeOfTrack).Cast<YouTubeMediaTrackHlsStream>();
		}

		public static IEnumerable<YouTubeMediaTrackContainer> FilterContainerTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			Type typeOfTrack = typeof(YouTubeMediaTrackContainer);
			return mediaTracks.Where(track => track.GetType() == typeOfTrack).Cast<YouTubeMediaTrackContainer>();
		}

		public static IEnumerable<YouTubeMediaTrackAudio> FilterAudioTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			Type typeOfTrack = typeof(YouTubeMediaTrackAudio);
			return mediaTracks.Where(track => track.GetType() == typeOfTrack).Cast<YouTubeMediaTrackAudio>();
		}

		public static string GetTrackShortInfo(YouTubeMediaTrack track)
		{
			string res = null;
			if (track is YouTubeMediaTrackAudio)
			{
				YouTubeMediaTrackAudio audio = track as YouTubeMediaTrackAudio;
				res = string.Empty;
				if (audio.AverageBitrate != 0)
				{
					string comma = !string.IsNullOrEmpty(res) ? "," : string.Empty;
					res += $"{comma} ~{audio.AverageBitrate / 1024} kbps";
				}
				if (!string.IsNullOrEmpty(audio.FileExtension))
				{
					string comma = !string.IsNullOrEmpty(res) ? "," : string.Empty;
					res += $"{comma} {audio.FileExtension}";
				}
				if (!string.IsNullOrEmpty(audio.MimeCodecs))
				{
					string comma = !string.IsNullOrEmpty(res) ? "," : string.Empty;
					res += $"{comma} {audio.MimeCodecs}";
				}
			}
			else if (track is YouTubeMediaTrackVideo)
			{
				YouTubeMediaTrackVideo video = track as YouTubeMediaTrackVideo;
				res = $"{video.VideoWidth}x{video.VideoHeight}";
				if (video.FrameRate != 0)
					res += $", {video.FrameRate} fps";
				if (video.AverageBitrate != 0)
					res += $", ~{video.AverageBitrate / 1024} kbps";
			}
			else if (track is YouTubeMediaTrackContainer)
			{
				YouTubeMediaTrackContainer container = track as YouTubeMediaTrackContainer;
				res = $"Video: {container.VideoWidth}x{container.VideoHeight}";
				if (container.VideoFrameRate != 0)
					res += $", {container.VideoFrameRate} fps";
				if (container.AverageBitrate != 0)
					res += $", ~{container.AverageBitrate / 1024} kbps";
			}
			return res;
		}

		internal static DateTime GetVideoPublishedDate(string videoId)
		{
			YouTubeVideo video = YouTubeVideo.GetById(videoId);
			return video != null ? video.DatePublished : DateTime.MaxValue;
		}

		private static FavoriteItem FindItemWithId(string itemId, FavoriteItem root)
		{
			if (string.IsNullOrEmpty(itemId) || string.IsNullOrWhiteSpace(itemId)) { return null; }
			if (string.CompareOrdinal(root.ID, itemId) == 0) { return root; }

			for (int i = 0; i < root.Children.Count; ++i)
			{
				FavoriteItem favoriteItem = FindItemWithId(itemId, root.Children[i]);
				if (favoriteItem != null) { return favoriteItem; }
			}

			return null;
		}

		public static FavoriteItem FindInFavorites(FavoriteItem itemToFind, FavoriteItem root)
		{
			if (root != null && !string.IsNullOrEmpty(itemToFind?.ID))
			{
				for (int i = 0; i < root.Children.Count; ++i)
				{
					FavoriteItem item = FindItemWithId(itemToFind.ID, root.Children[i]);
					if (item != null) { return item; }
				}
			}

			return null;
		}

		public static FavoriteItem FindInFavorites(string itemId)
		{
			return FindItemWithId(itemId, favoritesRootNode);
		}

		public static bool IsEnoughDiskSpace(IEnumerable<char> driveLetters, long contentLength)
		{
			try
			{
				foreach (char letter in driveLetters)
				{
					DriveInfo driveInfo = new DriveInfo(letter.ToString());
					if (driveInfo.AvailableFreeSpace < contentLength)
					{
						return false;
					}
				}
			}
#if DEBUG
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
			return true;
		}

		public static bool AdvancedFreeDiskSpaceCheckPassed(long totalFileSize)
		{
			//TODO: Find mistakes in this code
			try
			{
				long minimumFreeSpaceRequired = (long)(totalFileSize * 1.1);

				char chunkMergerDirectoryDriveLetter;
				if (!string.IsNullOrEmpty(config.ChunkMergerDirectory) && !string.IsNullOrWhiteSpace(config.ChunkMergerDirectory))
				{
					chunkMergerDirectoryDriveLetter = config.ChunkMergerDirectory[0];
				}
				else if (!string.IsNullOrEmpty(config.TemporaryDirectory) && !string.IsNullOrWhiteSpace(config.TemporaryDirectory))
				{
					chunkMergerDirectoryDriveLetter = config.TemporaryDirectory[0];
				}
				else
				{
					chunkMergerDirectoryDriveLetter = config.DownloadDirectory[0];
				}

				char tempDirectoryDriveLetter;
				if (!config.UseRamToStoreTemporaryFiles)
				{
					if (!string.IsNullOrEmpty(config.TemporaryDirectory) && !string.IsNullOrWhiteSpace(config.TemporaryDirectory))
					{
						tempDirectoryDriveLetter = config.TemporaryDirectory[0];
					}
					else
					{
						tempDirectoryDriveLetter = config.DownloadDirectory[0];
					}
				}
				else
				{
					tempDirectoryDriveLetter = chunkMergerDirectoryDriveLetter;
				}

				char downloadDirectoryDriveLetter = config.DownloadDirectory[0];
				DriveInfo driveInfoTempDirectory = new DriveInfo(tempDirectoryDriveLetter.ToString());
				if (tempDirectoryDriveLetter == chunkMergerDirectoryDriveLetter)
				{
					if (config.AutomaticallyMergeToContainer && downloadDirectoryDriveLetter == chunkMergerDirectoryDriveLetter)
					{
						minimumFreeSpaceRequired += totalFileSize;
					}
					if (driveInfoTempDirectory.AvailableFreeSpace < minimumFreeSpaceRequired)
					{
						return false;
					}
				}
				else
				{
					if (!config.UseRamToStoreTemporaryFiles &&
						driveInfoTempDirectory.AvailableFreeSpace < minimumFreeSpaceRequired)
					{
						return false;
					}

					DriveInfo driveInfoMergerDirectory = new DriveInfo(chunkMergerDirectoryDriveLetter.ToString());
					if (driveInfoMergerDirectory.AvailableFreeSpace < minimumFreeSpaceRequired)
					{
						return false;
					}

					if (downloadDirectoryDriveLetter == chunkMergerDirectoryDriveLetter)
					{
						if (config.AutomaticallyMergeToContainer)
						{
							minimumFreeSpaceRequired += totalFileSize;
						}
						if (!config.UseRamToStoreTemporaryFiles && tempDirectoryDriveLetter == downloadDirectoryDriveLetter)
						{
							minimumFreeSpaceRequired += totalFileSize;
						}
						if (driveInfoMergerDirectory.AvailableFreeSpace < minimumFreeSpaceRequired)
						{
							return false;
						}
					}
					else
					{
						DriveInfo driveInfoDownloadDirectory = new DriveInfo(downloadDirectoryDriveLetter.ToString());
						if (driveInfoDownloadDirectory.AvailableFreeSpace < minimumFreeSpaceRequired)
						{
							return false;
						}
					}
				}

				return true;
			}
#if DEBUG
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
			return false;
		}

		public static string FormatFileName(string fmt, YouTubeVideo videoInfo)
		{
			DateTime date = videoInfo.DatePublished < DateTime.MaxValue && config.UseUniversalTime ?
				videoInfo.DatePublished : videoInfo.DatePublished.ToLocalTime();
			return fmt.Replace("<year>", date.Year.ToString())
				.Replace("<month>", date.Month.ToString().PadLeft(2, '0'))
				.Replace("<day>", date.Day.ToString().PadLeft(2, '0'))
				.Replace("<hour>", date.Hour.ToString().PadLeft(2, '0'))
				.Replace("<minute>", date.Minute.ToString().PadLeft(2, '0'))
				.Replace("<second>", date.Second.ToString().PadLeft(2, '0'))
				.Replace("<GMT>", " GMT")
				.Replace("<video_title>", videoInfo.Title)
				.Replace("<video_id>", videoInfo.Id);
		}

		public static string FixFileName(string fn)
		{
			return fn.Replace("\\", "\u29F9").Replace("|", "\u2758").Replace("/", "\u2044")
				.Replace("?", "\u2753").Replace(":", "\uFE55").Replace("<", "\u227A").Replace(">", "\u227B")
				.Replace("\"", "\u201C").Replace("*", "\uFE61").Replace("^", "\u2303").Replace("\n", " ");
		}

		public static Bitmap GenerateVideoThumbnailLoadingIndicator(int width, int height, int tryNumber, int maxTries)
		{
			try
			{
				Bitmap bitmap = new Bitmap(width, height);
				Graphics g = Graphics.FromImage(bitmap);
				g.FillRectangle(Brushes.Black, 0, 0, width, height);
				Font font = new Font("Lucida Console", 15f);
				float xCenter = width / 2f;
				float yCenter = height / 2f;

				string t = "Загрузка...";
				SizeF sz = g.MeasureString(t, font);
				float y = yCenter - sz.Height;
				g.DrawString(t, font, Brushes.Lime, xCenter - sz.Width / 2f, y);

				t = $"Try №{tryNumber} / {maxTries}";
				y += sz.Height + 4f;
				sz = g.MeasureString(t, font);
				g.DrawString(t, font, Brushes.Lime, xCenter - sz.Width / 2f, y);

				return bitmap;
			}
#if DEBUG
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
			return null;
		}

		public static Bitmap GenerateVideoThumbnailFailed(int width, int height)
		{
			try
			{
				Bitmap bitmap = new Bitmap(width, height);
				Graphics g = Graphics.FromImage(bitmap);
				g.FillRectangle(Brushes.Black, 0, 0, width, height);
				Font font = new Font("Lucida Console", 15f);

				string t = $"Ошибка!";
				SizeF sz = g.MeasureString(t, font);
				PointF center = new PointF(width / 2f - sz.Width / 2f, height / 2f - sz.Height / 2f);
				g.DrawString(t, font, Brushes.Lime, center);

				return bitmap;
			}
#if DEBUG

			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
			return null;
		}

		public static Image TryGetImageFromStream(Stream stream, out int imageWidth, out int imageHeight)
		{
			try
			{
				Image image = Image.FromStream(stream);
				imageWidth = image.Width;
				imageHeight = image.Height;
				return image;
			}
#if DEBUG
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
			imageWidth = imageHeight = 0;
			return null;
		}

		public static int GetMaximalThumbnailFileNameLength(IEnumerable<YouTubeVideoThumbnail> thumbnails)
		{
			int result = 0;
			foreach (YouTubeVideoThumbnail thumbnail in thumbnails)
			{
				int length = string.IsNullOrEmpty(thumbnail.FileName) ? 0 : thumbnail.FileName.Length;
				if (length > result) { result = length; }
			}
			return result;
		}

		public static string DecryptCipherSignature(string signatureEncrypted, string algo)
		{
			if (algo.StartsWith("["))
			{
				algo = algo.Remove(0, 1);
			}
			if (algo.EndsWith("]"))
			{
				algo = algo.Remove(algo.Length - 1, 1);
			}
			string[] ints = algo.Split(',');
			string res = string.Empty;
			for (int i = 0; i < ints.Length; ++i)
			{
				if (!int.TryParse(ints[i], out int index))
				{
					return null;
				}
				if (index >= signatureEncrypted.Length)
				{
					return null;
				}
				res += signatureEncrypted[index];
			}
			return res;
		}

		public static bool IsVideoDateAvailable(YouTubeVideo video)
		{
			return video.InitialSimplifiedInfo != null &&
				video.InitialSimplifiedInfo.IsMicroformatInfoAvailable &&
				video.DatePublished < DateTime.MaxValue;
		}

		public static IEnumerable<MultipleProgressBarItem> ContentChunksToMultipleProgressBarItems(
			ConcurrentDictionary<int, DownloadableTask> chunks)
		{
			foreach (var chunk in chunks)
			{
				double percent = 0.0;
				string itemText;
				switch (chunk.Value.State)
				{
					case DownloadableTaskState.Preparing:
						itemText = "Подготовка...";
						break;

					case DownloadableTaskState.Connecting:
						itemText = "Подключение...";
						break;

					default:
						{
							percent = chunk.Value.ChunkFileSize > 0L && chunk.Value.ProcessedBytes > 0L ?
								100.0 / chunk.Value.ChunkFileSize * chunk.Value.ProcessedBytes : 0L;
							string percentFormatted = string.Format("{0:F2}", percent);
							itemText = $"{percentFormatted}%";
							break;
						}
				}

				MultipleProgressBarItem mpi = new MultipleProgressBarItem(
					0, 100, (int)percent, itemText, Color.Lime);
				yield return mpi;
			}
		}

		public static MultipleProgressBarItem[] GenerateChunkMergingProgressVisualizationItems(
			int chunkCount, int currentChunkId, double currentChunkProgressPercent)
		{
			MultipleProgressBarItem[] items = new MultipleProgressBarItem[chunkCount];
			for (int i = 0; i < chunkCount; ++i)
			{
				if (i < currentChunkId) { items[i] = new MultipleProgressBarItem(100, "100,00%"); }
				else if (i > currentChunkId) { items[i] = new MultipleProgressBarItem(0, "0,00%"); }
				else
				{
					string percentFormatted = string.Format("{0:F2}", currentChunkProgressPercent);
					items[i] = new MultipleProgressBarItem((int)currentChunkProgressPercent, $"{percentFormatted}%");
				}
			}
			return items;
		}

		public static int[] GetTrackAccessibilityHttpStatusCodes(IEnumerable<YouTubeMediaTrack> tracks, int connectionTimeout)
		{
			WebHeaderCollection headers = new WebHeaderCollection()
			{
				{ "Accept", "*/*" },
				{ "User-Agent", config.UserAgent }
			};
			int[] results = new int[tracks.Count()];
			var testTasks = tracks.Select((track, taskId) => Task.Run(() =>
			{
				string url = track.FileUrl?.Url;
				int errorCode = !string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url) ?
					MultiThreadedDownloaderLib.Utils.GetUrlResponseHttpHeaders("HEAD",
					url, headers, null, null, connectionTimeout, out _, out _) : 400;
				results[taskId] = errorCode;
			}));
			Task.WhenAll(testTasks).Wait();
			return results;
		}

		public static string GetNumberedFixedOutputFileNameWithotExtension(
			YouTubeVideo video, string containerFileExtension, VideoThumbnailWrapper thumbnailWrapper = null)
		{
			string fixedFileName = FixFileName(FormatFileName(IsVideoDateAvailable(video) ?
				config.OutputFileNameFormatWithDate : config.OutputFileNameFormatWithoutDate, video));
			if (thumbnailWrapper == null || string.IsNullOrEmpty(containerFileExtension)) { return fixedFileName; }
			string numberedVideoFilePath = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(Path.Combine(config.DownloadDirectory, fixedFileName + containerFileExtension));
			int int1 = ExtractNumberFromVideoFileName(numberedVideoFilePath);
			int int2 = GetThumbnailNextFreeFileNameNumber(thumbnailWrapper, Path.Combine(config.DownloadDirectory, fixedFileName), int1);
			int max = Math.Max(int1, int2);
			return max > 0 ? $"{fixedFileName}_{max}" : fixedFileName;
		}

		private static int ExtractNumberFromVideoFileName(string fileName)
		{
			string numbered = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(fileName);
			if (!string.IsNullOrEmpty(numbered))
			{
				string t = FindRegExp(numbered, @"_(\d+)(?:\.[\w\d]+)$");
				if (!string.IsNullOrEmpty(t) && int.TryParse(t, out int n)) { return n; }
			}

			return 0;
		}

		private static int GetThumbnailNextFreeFileNameNumber(VideoThumbnailWrapper thumbnailWrapper,
			string filePath, int startNumber)
		{
			try
			{
				if (startNumber < 0) { startNumber = 0; }
				string suffix = thumbnailWrapper.GetThumbnailFileNameSuffix();
				string fn = startNumber == 0 ? (filePath + suffix) : $"{filePath}_{startNumber}{suffix}";
				if (File.Exists(fn))
				{
					int i = startNumber == 0 ? 1 : startNumber;
					while (true)
					{
						string t = $"{filePath}_{++i}{suffix}";
						if (!File.Exists(t)) { return i; }
					}
				}
			}
#if DEBUG
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
			return startNumber;
		}

		public static string ExtractPlayerUrlFromWebPageCode(string webPageCode)
		{
			int n = webPageCode.IndexOf("\"jsUrl\":\"");
			if (n > 0)
			{
				string t = webPageCode.Substring(n + 9);
				string res = t.Substring(0, t.IndexOf("\""));
				if (!string.IsNullOrEmpty(res) && !string.IsNullOrWhiteSpace(res))
				{
					return YouTubeApiLib.Utils.YOUTUBE_URL + res;
				}
			}
			return null;
		}

		public static bool IsAudioOnly(IEnumerable<YouTubeMediaTrack> mediaTracks)
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

		internal static bool SaveThumbnailToFile(VideoThumbnailWrapper thumbnail, string formattedFileName)
		{
			try
			{
				if (!string.IsNullOrEmpty(formattedFileName) && !string.IsNullOrWhiteSpace(formattedFileName) && thumbnail.IsOk)
				{
					string outputFilePath = MultiThreadedDownloaderLib.Utils.GetNumberedFileName(
						Path.Combine(config.DownloadDirectory, formattedFileName + thumbnail.GetThumbnailFileNameSuffix()));
					thumbnail.ImageData.SaveToFile(outputFilePath);
					return true;
				}
			}
#if DEBUG
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
			return false;
		}

		public static bool SetClipboardText(string text)
		{
			for (int i = 0; i < 100; ++i)
			{
				try
				{
					Clipboard.SetText(text);
					return true;
				}
				catch { }
			}

			return false;
		}

		public static string FormatSize(long n)
		{
			const int KB = 1000;
			const int MB = 1000000;
			const int GB = 1000000000;
			const long TB = 1000000000000;
			long b = n % KB;
			long kb = (n % MB) / KB;
			long mb = (n % GB) / MB;
			long gb = (n % TB) / GB;

			if (n >= 0 && n < KB)
				return string.Format("{0} B", b);
			if (n >= KB && n < MB)
				return string.Format("{0},{1:D3} KB", kb, b);
			if (n >= MB && n < GB)
				return string.Format("{0},{1:D3} MB", mb, kb);
			if (n >= GB && n < TB)
				return string.Format("{0},{1:D3} GB", gb, mb);

			return string.Format("{0} {1:D3} {2:D3} {3:D3} bytes", gb, mb, kb, b);
		}

		public static string FormatDateTime(DateTime dateTime)
		{
			DateTime dt = config.UseUniversalTime ? dateTime : dateTime.ToLocalTime();
			string t = dt.ToString("yyyy.MM.dd, HH:mm:ss");
			return config.UseUniversalTime ? $"{t} GMT" : t;
		}

		public static string GetContainerFileExtension(IEnumerable<YouTubeMediaTrack> tracks)
		{
			if (config.AlwaysUseMkvContainer) { return ".mkv"; }

			bool hasWebmOrWeba = tracks.Any(track => track.MimeExt == "webm" || track.MimeExt == "weba");
			return hasWebmOrWeba ? ".mkv" : ".mp4";
		}

		public static async Task<bool> MergeYouTubeMediaTracks(IEnumerable<DownloadResult> files,
			string destinationFileName, bool wait, int delayAfterCompletion)
		{
			try
			{
				Process process = new Process();
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.FileName = "cmd.exe";
				string t = Path.GetFileName(config.FfmpegExeFilePath);
				string ffmpegFileName = t.Contains(" ") ? $"\"{t}\"" : t;
				string ffmpegDirectory = Path.GetDirectoryName(config.FfmpegExeFilePath);
				if (!string.IsNullOrEmpty(ffmpegDirectory))
				{
					process.StartInfo.WorkingDirectory = ffmpegDirectory;
				}

				string cmdArgs = $"/k {ffmpegFileName} ";
				foreach (DownloadResult file in files)
				{
					cmdArgs += $"-i \"{file.OutputFilePath}\" ";
				}

				int idx = 0;
				foreach (DownloadResult file in files)
				{
					cmdArgs += $"-map {idx}:0 ";
					++idx;
				}

				cmdArgs += $"-c copy \"{destinationFileName}\"";
				process.StartInfo.Arguments = cmdArgs;
				bool success = process.Start();
				if (success && wait)
				{
					process.WaitForExit();
				}

				if (success && wait && delayAfterCompletion > 0)
				{
					await Task.Delay(delayAfterCompletion);
				}

				return success;
			}
#if DEBUG
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
			return false;
		}

		public static async Task<bool> MergeYouTubeMediaTracks(IEnumerable<DownloadResult> files,
			string destinationFileName, int delayAfterCompletion)
		{
			return await MergeYouTubeMediaTracks(files, destinationFileName, true, delayAfterCompletion);
		}

		public static bool GrabHls(string hlsUrl, string outputFileName)
		{
			try
			{
				Process process = new Process();
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.FileName = "cmd.exe";
				string ffmpegFileName = Path.GetFileName(config.FfmpegExeFilePath);
				string ffmpegDirectory = Path.GetDirectoryName(config.FfmpegExeFilePath);
				if (!string.IsNullOrEmpty(config.FfmpegExeFilePath))
				{
					process.StartInfo.WorkingDirectory = ffmpegDirectory;
				}
				string cmdArgs = $"/k {ffmpegFileName} -i \"{hlsUrl}\" -c copy \"{outputFileName}\"";

				process.StartInfo.Arguments = cmdArgs;
				return process.Start();
			}
#if DEBUG
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
			return false;
		}

		public static int ClampValue(int value, int min, int max)
		{
			if (value <= min) { return min; }
			return value >= max ? max : value;
		}

		public static void OpenUrlInBrowser(string url)
		{
			try
			{
				if (!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
				{
					Process process = new Process();
					if (!string.IsNullOrEmpty(config.WebBrowserExeFilePath) &&
						!string.IsNullOrWhiteSpace(config.WebBrowserExeFilePath))
					{
						process.StartInfo.FileName = config.WebBrowserExeFilePath;
						process.StartInfo.Arguments = url;
					}
					else
					{
						process.StartInfo.FileName = url;
					}
					process.Start();
				}
			}
#if DEBUG
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
		}

		public static bool IsFfmpegAvailable()
		{
			return !string.IsNullOrEmpty(config.FfmpegExeFilePath) &&
				!string.IsNullOrWhiteSpace(config.FfmpegExeFilePath) &&
				File.Exists(config.FfmpegExeFilePath);
		}

		internal static string FindRegExp(string inputString, string pattern)
		{
			Regex regex = new Regex(pattern);
			MatchCollection matches = regex.Matches(inputString);
			return matches.Count > 0 && matches[0].Groups.Count > 1 ? matches[0].Groups[1].Value : null;
		}

		internal static JObject TryParseJson(string jsonString, out string errorText)
		{
			try
			{
				errorText = null;
				return JObject.Parse(jsonString);
			}
			catch (Exception ex)
			{
				errorText = ex.Message;
				return null;
			}
		}

		internal static JObject TryParseJson(string jsonString)
		{
			return TryParseJson(jsonString, out _);
		}
	}
}
