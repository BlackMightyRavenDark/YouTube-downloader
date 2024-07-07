using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using MultiThreadedDownloaderLib;
using YouTubeApiLib;
using System.Threading;

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

		public static List<YouTubeChannel> channels = new List<YouTubeChannel>();
		public static List<YouTubeVideo> videos = new List<YouTubeVideo>();
		public static List<FrameYouTubeChannel> framesChannel = new List<FrameYouTubeChannel>();
		public static List<FrameYouTubeVideo> framesVideo = new List<FrameYouTubeVideo>();

		public static TreeListView treeFavorites = null;
		public static FavoriteItem favoritesRootNode = null;
		public static MainConfiguration config;

		public static readonly bool Is64BitProcess = Environment.Is64BitProcess;

		public const int ERROR_CIPHER_DECRYPTION = -100;
		public const int ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM = -101;

		public static int DownloadString(string url, out string responseString)
		{
			FileDownloader d = new FileDownloader() { Url = url };
			return d.DownloadString(out responseString);
		}

		public static int DownloadData(string url, Stream stream)
		{
			FileDownloader d = new FileDownloader() { Url = url };
			return d.Download(stream);
		}

		public static string GetYouTubeChannelVideosRequestUrl(string channelId, int maxVideos)
		{
			NameValueCollection query = System.Web.HttpUtility.ParseQueryString(string.Empty);
			query.Add("part", "snippet");
			query.Add("type", "video");
			query.Add("order", "date");
			query.Add("key", config.YouTubeApiV3Key);
			query.Add("channelId", channelId);
			query.Add("maxResults", maxVideos.ToString());

			string url = $"{YOUTUBE_SEARCH_BASE_URL}?{query}";
			return url;
		}

		public static string GetYouTubeSearchQueryRequestUrl(
			string searchingPhrase, string resultTypes, int maxResults)
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

			string url = $"{YOUTUBE_SEARCH_BASE_URL}?{query}";
			return url;
		}

		public static string ExtractPlayerUrlFromWebPage(string webPage)
		{
			int n = webPage.IndexOf("\"jsUrl\":\"");
			if (n > 0)
			{
				string t = webPage.Substring(n + 9);
				string res = t.Substring(0, t.IndexOf("\""));
				if (!string.IsNullOrEmpty(res) && !string.IsNullOrWhiteSpace(res))
				{
					return "https://www.youtube.com" + res;
				}
			}
			return null;
		}

		public static YouTubeVideo GetSingleVideo(VideoId videoId)
		{
			YouTubeApi.getMediaTracksInfoImmediately = false;
			YouTubeApi.decryptMediaTrackUrlsAutomaticallyIfPossible = false;
			YouTubeApi api = new YouTubeApi();
			if (config.UseHiddenApiForGettingInfo)
			{
				YouTubeVideo video = api.GetVideo(videoId);
				return video;
			}
			else
			{
				YouTubeVideoWebPageResult youTubeVideoWebPageResult = YouTubeVideoWebPage.Get(videoId);
				if (youTubeVideoWebPageResult.ErrorCode == 200)
				{
					return api.GetVideo(youTubeVideoWebPageResult.VideoWebPage);
				}
			}
			return null;
		}

		public static List<YouTubeMediaTrackVideo> FilterVideoTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			List<YouTubeMediaTrackVideo> list = new List<YouTubeMediaTrackVideo>();
			foreach (YouTubeMediaTrack track in mediaTracks)
			{
				if (track is YouTubeMediaTrackVideo)
				{
					list.Add(track as YouTubeMediaTrackVideo);
				}
			}
			return list;
		}

		public static List<YouTubeMediaTrackContainer> FilterContainerTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			List<YouTubeMediaTrackContainer> list = new List<YouTubeMediaTrackContainer>();
			foreach (YouTubeMediaTrack track in mediaTracks)
			{
				if (track is YouTubeMediaTrackContainer)
				{
					list.Add(track as YouTubeMediaTrackContainer);
				}
			}
			return list;
		}

		public static List<YouTubeMediaTrackAudio> FilterAudioTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
		{
			List<YouTubeMediaTrackAudio> list = new List<YouTubeMediaTrackAudio>();
			foreach (YouTubeMediaTrack track in mediaTracks)
			{
				if (track is YouTubeMediaTrackAudio)
				{
					list.Add(track as YouTubeMediaTrackAudio);
				}
			}
			return list;
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
			YouTubeApi api = new YouTubeApi();
			YouTubeVideo video = api.GetVideo(new VideoId(videoId));
			return video != null ? video.DatePublished : DateTime.MaxValue;
		}

		internal static StreamingData ExtractStreamingDataFromWebPageCode(string webPageCode)
		{
			YouTubeVideoWebPageResult webPageResult = YouTubeVideoWebPage.FromCode(webPageCode);
			if (webPageResult.ErrorCode == 200)
			{
				RawVideoInfoResult rawVideoInfoResult = YouTubeApiLib.Utils.ExtractRawVideoInfoFromWebPage(webPageResult.VideoWebPage);
				if (rawVideoInfoResult.ErrorCode == 200)
				{
					return rawVideoInfoResult.RawVideoInfo.StreamingData;
				}
			}

			return null;
		}

		private static FavoriteItem FindFavoriteItem(FavoriteItem item, FavoriteItem root)
		{
			if (root.ID != null && root.ID.Equals(item.ID))
			{
				return root;
			}

			for (int i = 0; i < root.Children.Count; ++i)
			{
				FavoriteItem favoriteItem = FindFavoriteItem(item, root.Children[i]);
				if (favoriteItem != null)
				{
					return favoriteItem;
				}
			}
			return null;
		}

		public static FavoriteItem FindInFavorites(FavoriteItem find, FavoriteItem root)
		{
			for (int i = 0; i < root.Children.Count; ++i)
			{
				FavoriteItem item = FindFavoriteItem(find, root.Children[i]);
				if (item != null)
				{
					return item;
				}
			}
			return null;
		}

		public static FavoriteItem FindInFavorites(string itemId)
		{
			FavoriteItem item = new FavoriteItem(null, null, itemId, null, null, null);
			return FindInFavorites(item, favoritesRootNode);
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

	public sealed class YouTubeChannel
	{
		public string Title { get; set; }
		public string Id { get; set; }
		public string ImageUrl { get; set; }
		public List<string> ImageUrls { get; set; } = new List<string>();
		public Stream ImageData { get; set; } = null;
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
