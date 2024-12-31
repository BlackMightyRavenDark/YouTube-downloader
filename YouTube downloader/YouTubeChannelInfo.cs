using System.Collections.Generic;
using System.IO;

namespace YouTube_downloader
{
	public sealed class YouTubeChannelInfo
	{
		public string Title { get; set; }
		public string Id { get; set; }
		public string ImageUrl { get; set; }
		public List<string> ImageUrls { get; set; } = new List<string>();
		public Stream ImageData { get; set; } = null;
	}
}
