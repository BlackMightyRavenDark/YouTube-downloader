using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using MultiThreadedDownloaderLib;
using YouTubeApiLib;

namespace YouTube_downloader
{
	internal class YouTubeClientYtdl : IYouTubeClient
	{
		public string DisplayName => "youtube-dl or youtube-dlp client";

		/// <summary>
		/// Not used. Always equals 'null'.
		/// </summary>
		public YouTubeVideoWebPage WebPage => null;

		/// <summary>
		/// Not used.
		/// </summary>
		public FileDownloader Downloader { get; set; }

		public string YtdlExeFilePath { get; }
		public string CommandLineArguments { get; }
		public bool ShowConsoleWindow { get; }
		public YtdlVideo Video { get; private set; }

		private readonly DateTime _minimalUnixDateTime;

		public YouTubeClientYtdl(string ytdlExeFilePath, string commandLineArguments, bool showConsoleWindow)
		{
			YtdlExeFilePath = ytdlExeFilePath;
			CommandLineArguments = commandLineArguments;
			ShowConsoleWindow = showConsoleWindow;
			_minimalUnixDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		}

		/// <summary>
		/// Not used. Always returns 'null'.
		/// </summary>
		public JObject GenerateRequestBody(string videoId, YouTubeConfig youTubeConfig = null)
		{
			return null;
		}

		/// <summary>
		/// Not used. Always returns 'null'.
		/// </summary>
		public WebHeaderCollection GenerateRequestHeaders(string videoId, YouTubeConfig youTubeConfig = null)
		{
			return null;
		}

		public YouTubeRawVideoInfoResult GetRawVideoInfo(YouTubeVideoId videoId, out string errorMessage)
		{
			int errorCode = GetRawVideoInfo(videoId.Id, out YouTubeRawVideoInfo rawVideoInfo, out errorMessage);
			return new YouTubeRawVideoInfoResult(rawVideoInfo, errorCode);
		}

		public int GetRawVideoInfo(string videoId, out YouTubeRawVideoInfo rawVideoInfo, out string errorMessage)
		{
			bool ok = CallYtdl(videoId, out string raw);
			rawVideoInfo = new YouTubeRawVideoInfo(raw, this, null, DateTime.UtcNow);
			if (ok)
			{
				Video = ParseYtdlJson(raw, out errorMessage);
				return Video != null ? 200 : 400;
			}
			else
			{
				Video = null;
				errorMessage = string.IsNullOrEmpty(raw) || string.IsNullOrWhiteSpace(raw) ? "youtube-dl returned an empty string!" : raw;
				return 400;
			}
		}

		/// <summary>
		/// Not used. Does nothing.
		/// </summary>
		public void SetWebPage(YouTubeVideoWebPage webPage) { }

		private bool CallYtdl(string videoId, out string response)
		{
			if (string.IsNullOrEmpty(CommandLineArguments) || string.IsNullOrWhiteSpace(CommandLineArguments))
			{
				response = "No command line arguments passed!";
				return false;
			}

			try
			{
				Process process = new Process();
				process.StartInfo.FileName = YtdlExeFilePath;
				process.StartInfo.Arguments = CommandLineArguments.Replace("<video_url>", YouTubeApiLib.Utils.GetYouTubeVideoUrl(videoId));
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.CreateNoWindow = !ShowConsoleWindow;
				if (process.Start())
				{
					response = process.StandardOutput.ReadToEnd();
					process.WaitForExit();
					return !string.IsNullOrEmpty(response) && response.Length > 0 && response[0] == '{';
				}
				else
				{
					response = "Can't start a youtube-dl or ytdlp process!";
					return false;
				}
			}
			catch (Exception ex)
			{
				response = ex.Message;
				return false;
			}
		}

		public YtdlVideo ParseYtdlJson(string jsonString, out string errorMessage)
		{
			try
			{
				JObject json = Utils.TryParseJson(jsonString, out errorMessage);
				if (json != null)
				{
					string mediaType = json.Value<string>("media_type");
					string id = json.Value<string>("id");
					string title = json.Value<string>("title");
					if (!int.TryParse(json.Value<string>("duration"), out int dur)) { dur = 0; }
					TimeSpan duration = TimeSpan.FromSeconds(dur > 0 ? dur : 0);
					string channelId = json.Value<string>("channel_id");
					string channelTitle = json.Value<string>("channel");
					int ageLimit = json.Value<int>("age_limit");
					bool isFamilySafe = ageLimit <= 0;
					bool isLiveContent = mediaType == "livestream";
					long timestamp = TryGetValue<long>(json, isLiveContent ? "release_timestamp" : "timestamp", -1L);
					DateTime publishDate = timestamp > 0L ? DateTimeFromUnixSeconds(timestamp) : DateTime.MaxValue;
					string description = json.Value<string>("description");
					long viewCount = json.Value<long>("view_count");
					string availability = json.Value<string>("availability");
					bool isPrivate = availability == "private";
					string liveStatus = json.Value<string>("live_status");
					bool isLiveNow = liveStatus == "live";
					JArray jaThumbnails = json.Value<JArray>("thumbnails");

					List<YouTubeVideoThumbnail> thumbnails = new List<YouTubeVideoThumbnail>();
					if (jaThumbnails != null && jaThumbnails.Count > 0)
					{
						thumbnails.Capacity = jaThumbnails.Count;
						foreach (JObject jThumbnail in jaThumbnails.Cast<JObject>())
						{
							ushort width = jThumbnail.Value<ushort>("width");
							ushort height = jThumbnail.Value<ushort>("height");
							string url = jThumbnail.Value<string>("url");
							string fileName = Utils.ExtractFileNameFromThumbnailUrl(url);
							thumbnails.Add(new YouTubeVideoThumbnail(width, height, fileName, url));
						}

						if (thumbnails.Count > 1)
						{
							thumbnails.Sort((x, y) => x.Height > y.Height ? -1 : 1);
						}
					}

					var tracks = ParseYtdlFormats(json.Value<JArray>("formats")).ToList();
					YouTubeVideoPlayabilityStatus status = new YouTubeVideoPlayabilityStatus("OK", null, null, true, true, isPrivate, false, false, false, false, null, null, null);

					return new YtdlVideo(title, id, duration, publishDate, channelTitle, channelId,
						description, viewCount, null, isPrivate, isFamilySafe, isLiveContent, isLiveNow,
						thumbnails, tracks, status, DateTime.UtcNow);
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}

			return null;
		}

		public static IEnumerable<YouTubeMediaTrack> ParseYtdlFormats(JArray jaFormats)
		{
			bool isMultilingualVideo = jaFormats.Where(x => x.Value<string>("acodec") != null).Any(item =>
				(item.Value<string>("format_note") ?? string.Empty).Contains("orig"));
			foreach (JObject jFormat in jaFormats.Cast<JObject>())
			{
				string formatNote = jFormat.Value<string>("format_note") ?? string.Empty;
				if (formatNote == "storyboard") { continue; }

				string vcodec = jFormat.Value<string>("vcodec");
				string acodec = jFormat.Value<string>("acodec");
				bool isVideo = acodec == "none";
				bool isAudio = vcodec == "none";
				bool isContainer = vcodec != "none" && acodec != "none";
				int bitrate = (int)(TryGetValue<float>(jFormat, "vbr") * 1000);
				int averageBitrate = (int)(TryGetValue<float>(jFormat, "abr") * 1000);
				if (bitrate < 0 && averageBitrate < 0)
				{
					bitrate = (int)(TryGetValue<float>(jFormat, "tbr") * 1000);
				}

				if (isVideo || isContainer)
				{
					int width = jFormat.Value<int>("width");
					int height = jFormat.Value<int>("height");
					int frameRate = (int)jFormat.Value<float>("fps");
					string ext = GetFileExtension(vcodec, isVideo, isContainer);
					long fileSize = TryGetValue<long>(jFormat, "filesize");
					string[] id = jFormat.Value<string>("format_id").Split('-');
					if (!int.TryParse(id[0], out int formatId)) { formatId = 0; }
					YouTubeMediaTrackUrl url = new YouTubeMediaTrackUrl(jFormat.Value<string>("url"), null);
					bool isHls = jFormat.ContainsKey("manifest_url");
					if (isHls)
					{
						string manifestUrl = jFormat.Value<string>("manifest_url");
						string raw = jFormat.ToString();
						YouTubeBroadcast broadcast = new YouTubeBroadcast(formatId, width, height, frameRate,
							averageBitrate > 0 ? averageBitrate : bitrate, $"{vcodec},{acodec}", url, raw);
						yield return new YouTubeMediaTrackHlsStream(broadcast, manifestUrl, raw);
					}
					else if (isVideo)
					{
						yield return new YouTubeMediaTrackVideo(formatId, width, height, frameRate, bitrate, averageBitrate,
							null, fileSize, null, null, -1, null, url, ext, ext, vcodec, ext, false, jFormat.ToString());
					}
					else // container
					{
						int audioSampleRate = TryGetValue<int>(jFormat, "asr");
						int audioChannelCount = TryGetValue<int>(jFormat, "audio_channels");
						yield return new YouTubeMediaTrackContainer(formatId, width, height, frameRate, bitrate, averageBitrate,
							null, fileSize, null, null, null, audioSampleRate, audioChannelCount, -1, null, url,
							ext, "mp4", $"{vcodec},{acodec}", "mp4", false, jFormat.ToString());
					}
				}
				else if (isAudio)
				{
					bool isDrc = formatNote.Contains("DRC");
					bool isOriginal = formatNote.Contains("orig");
					if ((!isMultilingualVideo && !isDrc) || (isMultilingualVideo && isOriginal && !isDrc))
					{
						string[] id = jFormat.Value<string>("format_id").Split('-');
						int formatId = int.Parse(id[0]);
						long fileSize = TryGetValue<long>(jFormat, "filesize");
						int audioSampleRate = TryGetValue<int>(jFormat, "asr");
						int audioChannelCount = TryGetValue<int>(jFormat, "audio_channels");
						string ext = GetFileExtension(acodec, false, false);
						YouTubeMediaTrackUrl url = new YouTubeMediaTrackUrl(jFormat.Value<string>("url"), null);
						yield return new YouTubeMediaTrackAudio(formatId, bitrate, averageBitrate, null, fileSize,
							null, null, null, audioSampleRate, audioChannelCount, false, false, null, -1.0,
							-1, url, ext, ext, acodec, ext, false, false, null, null, jFormat.ToString());
					}
				}
			}
		}

		private static string GetFileExtension(string mime, bool isVideo, bool isContainer)
		{
			if (isContainer) { return "mp4"; }
			bool isOpus = mime.Contains("opus") || mime.Contains("vp");
			return isVideo ? (isOpus ? "webm" : "m4v") : (isOpus ? "weba" : "m4a");
		}

		private static T TryGetValue<T>(JToken jt, string key, T defaultValue = default)
		{
			try
			{
				JValue jv = jt.Value<JValue>(key);
				return jv != null && jv.Type != JTokenType.Null ? jv.Value<T>() : defaultValue;
			}
			catch
			{
				return defaultValue;
			}
		}

		private DateTime DateTimeFromUnixSeconds(long seconds)
		{
			return _minimalUnixDateTime + TimeSpan.FromSeconds(seconds);
		}
	}
}
