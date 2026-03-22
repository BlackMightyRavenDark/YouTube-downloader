using System;
using System.Drawing;
using System.IO;
using YouTubeApiLib;
using MultiThreadedDownloaderLib;

namespace YouTube_downloader
{
	public class VideoThumbnailWrapper : IDisposable
	{
		public YouTubeVideoThumbnail Thumbnail { get; }
		public Stream ImageData { get; private set; }
		public Image Image { get; private set; }
		public bool IsOk => ImageData != null && ImageData.Length > 0L;

		public VideoThumbnailWrapper(YouTubeVideoThumbnail thumbnail)
		{
			Thumbnail = thumbnail;
		}

		public void Dispose()
		{
			if (ImageData != null)
			{
				ImageData.Dispose();
				ImageData = null;
			}

			if (Image != null)
			{
				Image.Dispose();
				Image = null;
			}
		}

		public int DownloadThumbnail(FileDownloader downloader = null)
		{
			FileDownloader d = downloader ?? new FileDownloader();
			d.Url = Thumbnail.Url;
			ImageData = new MemoryStream();
			int errorCode = d.Download(ImageData);
			Image = errorCode == 200 ? Utils.TryGetImageFromStream(ImageData, out _, out _) : null;
			return errorCode;
		}
	}
}
