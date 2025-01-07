using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
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
		public const string YOUTUBE_SEARCH_BASE_URL = "https://www.googleapis.com/youtube/v3/search";
		public const string YOUTUBE_CHANNEL_URL_TEMPLATE = "https://www.youtube.com/channel/{0}/videos";
		public const string YOUTUBE_WATCH_URL_BASE = "https://www.youtube.com/watch";

		public const string FILENAME_FORMAT_DEFAULT_WITH_DATE =
			"[<year>-<month>-<day> <hour>-<minute>-<second><GMT>] <video_title> (id_<video_id>)";
		public const string FILENAME_FORMAT_DEFAULT_WITHOUT_DATE = "<video_title> (id_<video_id>)";

		public static List<YouTubeChannelInfo> channelInfos = new List<YouTubeChannelInfo>();
		public static List<YouTubeVideo> videos = new List<YouTubeVideo>();
		public static List<FrameYouTubeChannel> framesChannel = new List<FrameYouTubeChannel>();
		public static List<FrameYouTubeVideo> framesVideo = new List<FrameYouTubeVideo>();

		public static TreeListView treeFavorites = null;
		public static FavoriteItem favoritesRootNode = null;
		public static Configurator config;

		public static readonly bool Is64BitProcess = Environment.Is64BitProcess;

		public const int ERROR_CIPHER_DECRYPTION = -100;
		public const int ERROR_NO_CIPHER_DECRYPTION_ALGORITHM = -101;

		internal static bool isFavoritesLoaded = false;
		internal static bool isFavoritesChanged = false;

		public static int DownloadString(string url, out string responseString, bool useRequestSender = false)
		{
			if (useRequestSender)
			{
				try
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
				} catch (Exception ex)
				{
					responseString = ex.Message;
					return ex.HResult;
				}
			}

			FileDownloader d = new FileDownloader() { Url = url };
			return d.DownloadString(out responseString);
		}

		public static int DownloadData(string url, Stream stream)
		{
			FileDownloader d = new FileDownloader() { Url = url };
			return d.Download(stream);
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

			string url = $"{YOUTUBE_SEARCH_BASE_URL}?{query}";
			return url;
		}

		public static string GetYouTubeChannelVideosRequestUrl(string channelId, int maxVideos)
		{
			DateTime maxDate = DateTime.MaxValue;
			return GetYouTubeChannelVideosRequestUrl(channelId, maxVideos, maxDate, maxDate);
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

			string url = $"{YOUTUBE_SEARCH_BASE_URL}?{query}";
			return url;
		}

		public static string GetYouTubeSearchQueryRequestUrl(
			string searchingPhrase, string resultTypes, int maxResults)
		{
			DateTime maxDate = DateTime.MaxValue;
			return GetYouTubeSearchQueryRequestUrl(searchingPhrase, resultTypes, maxResults, maxDate, maxDate);
		}

		public static YouTubeVideo GetSingleVideo(YouTubeVideoId videoId, out string errorMessage)
		{
			YouTubeApi.getMediaTracksInfoImmediately = false;
			YouTubeVideo video = YouTubeVideo.GetById(videoId, null);
			if (video == null)
			{
				const string clientId = "web_page";
				IYouTubeClient client = YouTubeApi.GetYouTubeClient(clientId);
				if (client != null)
				{
					YouTubeRawVideoInfoResult rawVideoInfoResult = client.GetRawVideoInfo(videoId, out errorMessage);
					if (rawVideoInfoResult.ErrorCode == 200)
					{
						video = rawVideoInfoResult.RawVideoInfo.ToVideo();
					}
				}
				else
				{
					errorMessage = $"The client with ID '{clientId}' is not found! The default client was also failed! Let's cry, baby :'(";
				}
			}
			else
			{
				errorMessage = null;
			}

			return video;
		}

		public static IEnumerable<YouTubeMediaTrackVideo> FilterVideoTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			Type typeOfTrack = typeof(YouTubeMediaTrackVideo);
			foreach (YouTubeMediaTrack track in mediaTracks)
			{
				if (track.GetType() == typeOfTrack)
				{
					yield return track as YouTubeMediaTrackVideo;
				}
			}
		}

		public static IEnumerable<YouTubeMediaTrackHlsStream> FilterHlsTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			Type typeOfTrack = typeof(YouTubeMediaTrackHlsStream);
			foreach (YouTubeMediaTrack track in mediaTracks)
			{
				if (track.GetType() == typeOfTrack)
				{
					yield return track as YouTubeMediaTrackHlsStream;
				}
			}
		}

		public static IEnumerable<YouTubeMediaTrackContainer> FilterContainerTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			Type typeOfTrack = typeof(YouTubeMediaTrackContainer);
			foreach (YouTubeMediaTrack track in mediaTracks)
			{
				if (track.GetType() == typeOfTrack)
				{
					yield return track as YouTubeMediaTrackContainer;
				}
			}
		}

		public static IEnumerable<YouTubeMediaTrackAudio> FilterAudioTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			Type typeOfTrack = typeof(YouTubeMediaTrackAudio);
			foreach (YouTubeMediaTrack track in mediaTracks)
			{
				if (track.GetType() == typeOfTrack)
				{
					yield return track as YouTubeMediaTrackAudio;
				}
			}
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
			YouTubeApi.getMediaTracksInfoImmediately = false;
			YouTubeVideo video = YouTubeVideo.GetById(videoId);
			return video != null ? video.DatePublished : DateTime.MaxValue;
		}

		internal static YouTubeStreamingDataResult ExtractStreamingDataFromVideoWebPage(YouTubeVideoWebPage webPage)
		{
			YouTubeRawVideoInfoResult rawVideoInfoResult = YouTubeApiLib.Utils.ExtractRawVideoInfoFromWebPage(webPage);
			return rawVideoInfoResult.ErrorCode == 200 ?
				rawVideoInfoResult.RawVideoInfo.StreamingData :
				new YouTubeStreamingDataResult(null, rawVideoInfoResult.ErrorCode);
		}

		internal static YouTubeStreamingDataResult ExtractStreamingDataFromVideoWebPage(string webPageCode)
		{
			YouTubeVideoWebPageResult webPageResult = YouTubeVideoWebPage.FromCode(webPageCode);
			return webPageResult.ErrorCode == 200 ?
				ExtractStreamingDataFromVideoWebPage(webPageResult.VideoWebPage) :
				new YouTubeStreamingDataResult(null, webPageResult.ErrorCode);
		}

		private static FavoriteItem FindItemWithId(string itemId, FavoriteItem root)
		{
			if (string.IsNullOrEmpty(itemId) || string.IsNullOrEmpty(itemId)) { return null; }

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

		public static string DecideMergingDirectory()
		{
			return !string.IsNullOrEmpty(config.ChunksMergingDirPath) &&
				!string.IsNullOrWhiteSpace(config.ChunksMergingDirPath) ?
				config.ChunksMergingDirPath : config.TempDirPath;
		}

		public static bool IsEnoughDiskSpace(IEnumerable<char> driveLetters, long contentLength)
		{
			foreach (char letter in driveLetters)
			{
				DriveInfo driveInfo = new DriveInfo(letter.ToString());
				if (driveInfo.AvailableFreeSpace < contentLength)
				{
					return false;
				}
			}
			return true;
		}

		public static bool AdvancedFreeSpaceCheck(long totalFilesSize)
		{
			//TODO: Double-check this code for mistakes

			long minimumFreeSpaceRequired = (long)(totalFilesSize * 1.1);

			char chunkMergingDirDriveLetter;
			if (!string.IsNullOrEmpty(config.ChunksMergingDirPath) && !string.IsNullOrWhiteSpace(config.ChunksMergingDirPath))
			{
				chunkMergingDirDriveLetter = config.ChunksMergingDirPath[0];
			}
			else if (!string.IsNullOrEmpty(config.TempDirPath) && !string.IsNullOrWhiteSpace(config.TempDirPath))
			{
				chunkMergingDirDriveLetter = config.TempDirPath[0];
			}
			else
			{
				chunkMergingDirDriveLetter = config.DownloadingDirPath[0];
			}

			char tempDirDriveLetter;
			if (!config.UseRamToStoreTemporaryFiles)
			{
				if (!string.IsNullOrEmpty(config.TempDirPath) && !string.IsNullOrWhiteSpace(config.TempDirPath))
				{
					tempDirDriveLetter = config.TempDirPath[0];
				}
				else
				{
					tempDirDriveLetter = config.DownloadingDirPath[0];
				}
			}
			else
			{
				tempDirDriveLetter = chunkMergingDirDriveLetter;
			}

			char downloadingDirDriveLetter = config.DownloadingDirPath[0];

			DriveInfo driveInfoTempDir = new DriveInfo(tempDirDriveLetter.ToString());
			if (tempDirDriveLetter == chunkMergingDirDriveLetter)
			{
				if (config.MergeToContainer && downloadingDirDriveLetter == chunkMergingDirDriveLetter)
				{
					minimumFreeSpaceRequired += totalFilesSize;
				}
				if (driveInfoTempDir.AvailableFreeSpace < minimumFreeSpaceRequired)
				{
					return false;
				}
			}
			else
			{
				if (!config.UseRamToStoreTemporaryFiles &&
					driveInfoTempDir.AvailableFreeSpace < minimumFreeSpaceRequired)
				{
					return false;
				}

				DriveInfo driveInfoMergingDir = new DriveInfo(chunkMergingDirDriveLetter.ToString());
				if (driveInfoMergingDir.AvailableFreeSpace < minimumFreeSpaceRequired)
				{
					return false;
				}

				if (downloadingDirDriveLetter == chunkMergingDirDriveLetter)
				{
					if (config.MergeToContainer)
					{
						minimumFreeSpaceRequired += totalFilesSize;
					}
					if (!config.UseRamToStoreTemporaryFiles && tempDirDriveLetter == downloadingDirDriveLetter)
					{
						minimumFreeSpaceRequired += totalFilesSize;
					}
					if (driveInfoMergingDir.AvailableFreeSpace < minimumFreeSpaceRequired)
					{
						return false;
					}
				}
				else
				{
					DriveInfo driveInfoDownloadingDir = new DriveInfo(downloadingDirDriveLetter.ToString());
					if (driveInfoDownloadingDir.AvailableFreeSpace < minimumFreeSpaceRequired)
					{
						return false;
					}
				}
			}

			return true;
		}

		public static string FormatFileName(string fmt, YouTubeVideo videoInfo)
		{
			DateTime date = videoInfo.DatePublished < DateTime.MaxValue && config.UseGmtTime ?
				videoInfo.DatePublished.ToGmt() : videoInfo.DatePublished;
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
			return video.SimplifiedInfo != null &&
				video.SimplifiedInfo.IsMicroformatInfoAvailable &&
				video.DatePublished < DateTime.MaxValue;
		}

		public static IEnumerable<MultipleProgressBarItem> ContentChunksToMultipleProgressBarItems(
			ConcurrentDictionary<int, DownloadableContentChunk> chunks)
		{
			foreach (var chunk in chunks)
			{
				double percent = 0.0;
				string itemText;
				switch (chunk.Value.State)
				{
					case DownloadableContentChunkState.Preparing:
						itemText = "Подготовка...";
						break;

					case DownloadableContentChunkState.Connecting:
						itemText = "Подключение...";
						break;

					default:
						{
							percent = chunk.Value.TotalBytes > 0L && chunk.Value.ProcessedBytes > 0L ?
								100.0 / chunk.Value.TotalBytes * chunk.Value.ProcessedBytes : 0L;
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
			NameValueCollection headers = new NameValueCollection()
			{
				{ "Accept", "*/*" },
				{ "User-Agent", config.UserAgent }
			};
			int[] results = new int[tracks.Count()];
			var testTasks = tracks.Select((track, taskId) => Task.Run(() =>
			{
				int errorCode = FileDownloader.GetUrlResponseHeaders(track.FileUrl.Url, headers, connectionTimeout, out _, out _);
				results[taskId] = errorCode;
			}));
			Task.WhenAll(testTasks).Wait();
			return results;
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

		public static IEnumerable<YouTubeMediaTrack> MediaTracksToEnumerable(Dictionary<string, YouTubeMediaFormatList> formatLists)
		{
			IEnumerable<YouTubeMediaFormatList> lists = formatLists.Values;
			foreach (YouTubeMediaFormatList list in lists)
			{
				foreach (YouTubeMediaTrack track in list.Tracks)
				{
					yield return track;
				}
			}
		}

		public static void SetClipboardText(string text)
		{
			bool res;
			do
			{
				try
				{
					Clipboard.SetText(text);
					res = true;
					return;
				}
				catch
				{
					res = false;
				}
			} while (!res);
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

		public static async Task<bool> MergeYouTubeMediaTracks(IEnumerable<DownloadResult> files,
			string destinationFileName, bool wait, int delayAfterCompletion)
		{
			bool success = await Task.Run(() =>
			{
				Process process = new Process();
				process.StartInfo.UseShellExecute = true;
				process.StartInfo.FileName = "cmd.exe";
				string t = Path.GetFileName(config.FfmpegExeFilePath);
				string ffmpegName = t.Contains(" ") ? $"\"{t}\"" : t;
				string ffmpegPath = Path.GetDirectoryName(config.FfmpegExeFilePath);
				if (!string.IsNullOrEmpty(config.FfmpegExeFilePath))
				{
					process.StartInfo.WorkingDirectory = ffmpegPath;
				}

				string cmdArgs = $"/k {ffmpegName} ";
				foreach (DownloadResult file in files)
				{
					cmdArgs += $"-i \"{file.FileName}\" ";
				}
				int iter = 0;
				foreach (DownloadResult file in files)
				{
					cmdArgs += $"-map {iter}:0 ";
					++iter;
				}

				cmdArgs += $"-c copy \"{destinationFileName}\"";
				process.StartInfo.Arguments = cmdArgs;
				bool res = process.Start();
				if (res && wait)
				{
					process.WaitForExit();
				}
				return res;
			});

			if (success && delayAfterCompletion > 0)
			{
				await Task.Run(() => Thread.Sleep(delayAfterCompletion));
			}

			return success;
		}

		public static async Task<bool> MergeYouTubeMediaTracks(IEnumerable<DownloadResult> files,
			string destinationFileName, int delayAfterCompletion)
		{
			return await MergeYouTubeMediaTracks(files, destinationFileName, true, delayAfterCompletion);
		}

		public static async Task<bool> MergeYouTubeMediaTracks(IEnumerable<DownloadResult> files,
			string destinationFileName)
		{
			return await MergeYouTubeMediaTracks(files, destinationFileName, true, 0);
		}

		public static bool GrabHls(string hlsUrl, string destinationFileName)
		{
			Process process = new Process();
			process.StartInfo.UseShellExecute = true;
			process.StartInfo.FileName = "cmd.exe";
			string t = Path.GetFileName(config.FfmpegExeFilePath);
			string ffmpegName = t;
			string ffmpegPath = Path.GetDirectoryName(config.FfmpegExeFilePath);
			if (!string.IsNullOrEmpty(config.FfmpegExeFilePath))
			{
				process.StartInfo.WorkingDirectory = ffmpegPath;
			}
			string cmdArgs = $"/k {ffmpegName} -i \"{hlsUrl}\" -c copy \"{destinationFileName}\"";

			process.StartInfo.Arguments = cmdArgs;
			return process.Start();
		}

		public static int ClampValue(int value, int min, int max)
		{
			if (value <= min) { return min; }
			return value >= max ? max : value;
		}

		public static void OpenUrlInBrowser(string url)
		{
			if (!string.IsNullOrEmpty(config.BrowserExeFilePath) &&
				!string.IsNullOrWhiteSpace(config.BrowserExeFilePath) &&
				!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
			{
				Process process = new Process();
				process.StartInfo.FileName = config.BrowserExeFilePath;
				process.StartInfo.Arguments = url;
				process.Start();
			}
		}

		public static bool IsFfmpegAvailable()
		{
			return !string.IsNullOrEmpty(config.FfmpegExeFilePath) &&
				!string.IsNullOrWhiteSpace(config.FfmpegExeFilePath) &&
				File.Exists(config.FfmpegExeFilePath);
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

	public enum FavoriteItemType { Video, Channel, Directory };

	public sealed class FavoriteItem
	{
		public List<FavoriteItem> Children { get; private set; } = new List<FavoriteItem>();
		public FavoriteItem Parent { get; private set; } = null;
		public string DisplayName { get; set; }
		public string Title { get; set; }
		public FavoriteItemType ItemType { get; set; }
		public string ID { get; set; } = null;
		public string ChannelTitle { get; set; } = null;
		public string ChannelId { get; set; } = null;

		public FavoriteItem(string displayName)
		{
			DisplayName = displayName;
			Title = displayName;
		}

		public FavoriteItem(string title, string displayName, string id,
			string channelTitle, string channelId, FavoriteItem parent)
		{
			Title = title;
			DisplayName = displayName;
			ID = id;
			ChannelTitle = channelTitle;
			ChannelId = channelId;
			Parent = parent;
		}
	}

	public sealed class DownloadResult
	{
		public int ErrorCode { get; private set; }
		public string ErrorMessage { get; private set; }
		public string FileName { get; private set; }

		public DownloadResult(int errorCode, string errorMessage, string fileName)
		{
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
			FileName = fileName;
		}
	}

	public static class MemoryWatcher
	{
		public static ulong RamTotal { get; private set; } = 0U;
		public static ulong RamUsed { get; private set; } = 0U;
		public static ulong RamFree { get; private set; } = 0U;
		private static Microsoft.VisualBasic.Devices.ComputerInfo computerInfo = null;

		public static bool Update()
		{
			try
			{
				if (computerInfo == null)
				{
					computerInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
				}
				RamTotal = computerInfo.TotalPhysicalMemory;
				RamFree = computerInfo.AvailablePhysicalMemory;
				RamUsed = RamTotal - RamFree;
				return true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				computerInfo = null;
				return false;
			}
		}
	}
}
