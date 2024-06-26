using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using MultiThreadedDownloaderLib;
using YouTubeApiLib;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
	public static class Helper
	{
		public static void SaveToFile(this Stream stream, string filePath)
		{
			try
			{
				using (Stream fileStream = File.OpenWrite(filePath))
				{
					stream.Position = 0L;
					StreamAppender.Append(stream, fileStream);
				}
			}
			catch (System.Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		public static bool IsCiphered(this YouTubeVideo video)
		{
			StreamingData streamingData = video.RawInfo?.StreamingData;
			if (streamingData != null)
			{
				JArray jaFormats = streamingData.RawData?.Value<JArray>("formats");
				if (jaFormats != null && jaFormats.Count > 0)
				{
					return jaFormats[0].Value<JToken>("signatureCipher") != null;
				}
				JArray jaAdaptiveFormats = streamingData.RawData?.Value<JArray>("adaptiveFormats");
				if (jaAdaptiveFormats != null && jaAdaptiveFormats.Count > 0)
				{
					return jaAdaptiveFormats[0].Value<JToken>("signatureCipher") != null;
				}
			}

			return false;
		}

		public static Stream DownloadPreviewImage(this YouTubeVideo video)
		{
			if (video.ThumbnailUrls != null)
			{
				FileDownloader d = new FileDownloader();
				foreach (YouTubeVideoThumbnail thumbnail in video.ThumbnailUrls)
				{
					Stream mem = new MemoryStream();
					d.Url = thumbnail.Url;
					if (d.Download(mem) == 200)
					{
						return mem;
					}
					mem.Dispose();
				}
			}

			return null;
		}

		public static Rectangle ResizeTo(this Rectangle source, Size newSize)
		{
			float aspectSource = source.Height / (float)source.Width;
			float aspectDest = newSize.Height / (float)newSize.Width;
			int w = newSize.Width;
			int h = newSize.Height;
			if (aspectSource > aspectDest)
			{
				w = (int)(newSize.Height / aspectSource);
			}
			else if (aspectSource < aspectDest)
			{
				h = (int)(newSize.Width * aspectSource);
			}
			return new Rectangle(0, 0, w, h);
		}

		public static Rectangle CenterIn(this Rectangle source, Rectangle dest)
		{
			int x = dest.Width / 2 - source.Width / 2;
			int y = dest.Height / 2 - source.Height / 2;
			return new Rectangle(x, y, source.Width, source.Height);
		}

		public static void SetDoubleBuffered(this Control control, bool enabled)
		{
			typeof(Control).InvokeMember("DoubleBuffered",
				BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
				null, control, new object[] { enabled });
		}

		public static void SetFontSize(this ContextMenuStrip contextMenu, int newSize)
		{
			contextMenu.Font = new Font(contextMenu.Font.FontFamily, newSize);
		}

		public static string GetTypeAsString(this YouTubeMediaTrack track)
		{
			if (track is YouTubeMediaTrackAudio)
			{
				return track.IsDashManifest ? "DASH Audio" : "Audio";
			}
			else if (track is YouTubeMediaTrackVideo)
			{
				return track.IsHlsManifest ? "HLS" :
					track.IsDashManifest ? "DASH Video" : "Video";
			}
			else if (track is YouTubeMediaTrackContainer) { return "Container"; }
			else { return "Unknown"; }
		}

		public static string GetFormattedResolution(this YouTubeMediaTrack track)
		{
			if (track is YouTubeMediaTrackVideo)
			{
				YouTubeMediaTrackVideo video = track as YouTubeMediaTrackVideo;
				return $"{video.VideoWidth}x{video.VideoHeight}";
			}
			else if (track is YouTubeMediaTrackContainer)
			{
				YouTubeMediaTrackContainer container = track as YouTubeMediaTrackContainer;
				return $"{container.VideoWidth}x{container.VideoHeight}";
			}
			else
			{
				return string.Empty;
			}
		}

		public static string GetFormattedFrameRate(this YouTubeMediaTrack track)
		{
			if (track is YouTubeMediaTrackVideo)
			{
				YouTubeMediaTrackVideo video = track as YouTubeMediaTrackVideo;
				return $"{video.FrameRate} fps";
			}
			else if (track is YouTubeMediaTrackContainer)
			{
				YouTubeMediaTrackContainer container = track as YouTubeMediaTrackContainer;
				return $"{container.VideoFrameRate} fps";
			}
			else
			{
				return string.Empty;
			}
		}

		public static string GetFormattedChunkCount(this YouTubeMediaTrack track)
		{
			if (track is YouTubeMediaTrackVideo)
			{
				YouTubeMediaTrackVideo video = track as YouTubeMediaTrackVideo;
				if (video.IsDashManifest)
				{
					return $"{(video.DashUrls != null ? video.DashUrls.Count : 0)} chunks";
				}
			}

			return string.Empty;
		}

		internal static TableRow ToTableRow(this YouTubeMediaTrack track)
		{
			string trackType = track.GetTypeAsString();
			string trackId = $"ID {track.FormatId,3}";
			string resolution = track.GetFormattedResolution();
			string fps = track.GetFormattedFrameRate();
			string bitrate = track.AverageBitrate > 0 ? $"~{track.AverageBitrate / 1024} kbps" : string.Empty;
			string formattedFileSize = track.ContentLength > 0 ? FormatSize(track.ContentLength) : string.Empty;
			string chunkCount = track.GetFormattedChunkCount();

			string[] data = new string[]
			{
				trackType, trackId, resolution, fps, bitrate,
				track.FileExtension, track.MimeCodecs, formattedFileSize, chunkCount
			};

			return new TableRow(data, track);
		}
		
		public static DateTime ToGmt(this DateTime dateTime)
		{
			TimeSpan offset = TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time").BaseUtcOffset;
			return dateTime.ToUniversalTime().AddHours(offset.Hours);
		}
	}
}
