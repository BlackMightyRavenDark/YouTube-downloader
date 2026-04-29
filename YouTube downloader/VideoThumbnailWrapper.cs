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
		public bool IsWebP
		{
			get
			{
				if (!_isFileExtensionDetermined) { GetIsWebpImage(); }
				return _isWebP;
			}
		}

		private bool _isWebP;
		public bool _isFileExtensionDetermined;

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

			_isWebP = _isFileExtensionDetermined = false;
		}

		public int DownloadThumbnail(FileDownloader downloader = null)
		{
			_isWebP = _isFileExtensionDetermined = false;
			FileDownloader d = downloader ?? new FileDownloader();
			d.Url = Thumbnail.Url;
			ImageData = new MemoryStream();
			int errorCode = d.Download(ImageData);
			Image = errorCode == 200 ? Utils.TryGetImageFromStream(ImageData, out _, out _) : null;
			return errorCode;
		}

		public string GetThumbnailFileNameSuffix()
		{
			string ext = GetFileExtension();
			string t = Image != null ? $"_{Image.Width}x{Image.Height}{ext}" : ext;
			return $"_image{t}";
		}

		private bool GetIsWebpImage()
		{
			if (!_isFileExtensionDetermined && IsOk)
			{
				ImageData.Position = 0L;
				byte[] bytes = new byte[32];
				int read = ImageData.Read(bytes, 0, bytes.Length);
				for (int i = 0; i < read - 4; ++i)
				{
					if (bytes[i] == 'W' && bytes[i + 1] == 'E' && bytes[i + 2] == 'B' && bytes[i + 3] == 'P')
					{
						_isWebP = true;
						break;
					}
				}
			}

			return _isWebP;
		}

		public string GetFileExtension()
		{
			return IsWebP ? ".webp" : ".jpg";
		}
	}
}
