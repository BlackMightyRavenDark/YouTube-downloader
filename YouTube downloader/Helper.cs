using System;
using System.Collections.Generic;
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
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		public static bool IsSucceed(this YouTubeVideo video)
		{
			return video.Status != null &&
				((bool)video.Status.Status?.Equals("OK", StringComparison.OrdinalIgnoreCase) ||
				!string.IsNullOrEmpty(video.Status.Reason) ||
				!string.IsNullOrEmpty(video.Status.ReasonDetails));
		}

		public static bool IsCiphered(this YouTubeVideo video)
		{
			try
			{
				YouTubeStreamingDataResult streamingDataResult = video.RawInfo?.StreamingData;
				if (streamingDataResult.ErrorCode == 200)
				{
					JObject json = TryParseJson(streamingDataResult.Data.RawData);
					if (json != null)
					{
						JArray jaFormats = json.Value<JArray>("formats");
						if (jaFormats != null && jaFormats.Count > 0)
						{
							return jaFormats[0].Value<JToken>("signatureCipher") != null;
						}
						JArray jaAdaptiveFormats = json.Value<JArray>("adaptiveFormats");
						if (jaAdaptiveFormats != null && jaAdaptiveFormats.Count > 0)
						{
							return jaAdaptiveFormats[0].Value<JToken>("signatureCipher") != null;
						}
					}
				}
			} catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}

			return false;
		}

		public static Stream DownloadPreviewImage(this YouTubeVideo video)
		{
			if (video.Thumbnails != null)
			{
				FileDownloader d = new FileDownloader();
				foreach (YouTubeVideoThumbnail thumbnail in video.Thumbnails)
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
			Type trackType = track.GetType();
			if (trackType == typeof(YouTubeMediaTrackAudio))
			{
				return track.IsDashManifestPresent ? "DASH Audio" : "Audio";
			}
			else if (trackType == typeof(YouTubeMediaTrackVideo))
			{
				return track.IsDashManifestPresent ? "DASH Video" : "Video";
			}
			else if (trackType == typeof(YouTubeMediaTrackContainer)) { return "Container"; }
			else if (trackType == typeof(YouTubeMediaTrackHlsStream)) { return "HLS"; }
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

		public static string GetFormattedDashChunkCount(this YouTubeMediaTrack track)
		{
			if (track.GetType() == typeof(YouTubeMediaTrackVideo))
			{
				YouTubeMediaTrackVideo video = track as YouTubeMediaTrackVideo;
				if (video.IsDashManifestPresent)
				{
					return $"{(video.DashUrls != null ? video.DashUrls.Count : 0)} chunks";
				}
			}

			return string.Empty;
		}

		public static YouTubeDashUrlList MakeDashUrlList(this YouTubeMediaTrack track, long chunkSize)
		{
			List<string> list = new List<string>();
			for (long position = 0L; position < track.ContentLength; position += chunkSize)
			{
				long endPos = position + chunkSize - 1L;
				long n = endPos < track.ContentLength ? endPos : track.ContentLength - 1L;
				string rangeValue = $"{position}-{n}";
				list.Add($"&range={rangeValue}");
			}

			YouTubeDashUrlList dashUrlList = new YouTubeDashUrlList(track.FileUrl.Url, list);
			return dashUrlList;
		}

		internal static TableRow ToTableRow(this YouTubeMediaTrack track)
		{
			string trackType = track.GetTypeAsString();
			string trackId = $"ID {track.FormatId,3}";
			string resolution = track.GetFormattedResolution();
			string fps = track.GetFormattedFrameRate();
			string bitrate = track.AverageBitrate > 0 ? $"~{track.AverageBitrate / 1024} kbps" : string.Empty;
			string formattedFileSize = track.ContentLength > 0 ? FormatSize(track.ContentLength) : string.Empty;
			string chunkCount = track.GetFormattedDashChunkCount();

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

		public static void DrawStar(this Graphics graphics,
			double positionX, double positionY, double outerRadius,
			double spikeSizeDivider, double rotationAngle, bool fillArea, Color areaColor)
		{
			const int vertexCount = 10;
			double innerRadius = outerRadius / spikeSizeDivider;
			const double oneRadian = Math.PI / 180.0;
			PointF[] vertices = new PointF[vertexCount + 1];
			double step = 360.0 / vertexCount;
			double angle = rotationAngle;
			for (int i = 0; i < vertexCount; ++i)
			{
				double angleRadian = angle * oneRadian;
				double radius = i % 2 == 0 ? innerRadius : outerRadius;
				float x = (float)(positionX + Math.Sin(angleRadian) * radius);
				float y = (float)(positionY + Math.Cos(angleRadian) * radius);
				vertices[i] = new PointF(x, y);
				if (i == 0)
				{
					vertices[vertexCount] = new PointF(x, y);
				}
				angle += step;
			}

			if (fillArea)
			{
				Brush brush = new SolidBrush(areaColor);
				graphics.FillPolygon(brush, vertices);
				brush.Dispose();
			}
			graphics.DrawLines(Pens.Black, vertices);
		}

		public static Rectangle Deflate(this Rectangle rectangle, int width, int height)
		{
			return new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - width, rectangle.Height - height);
		}
	}
}
