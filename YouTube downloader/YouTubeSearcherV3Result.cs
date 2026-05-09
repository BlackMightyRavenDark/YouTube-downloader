using System;
using System.Collections.Generic;
using YouTubeApiLib;

namespace YouTube_downloader
{
	internal abstract class YouTubeSearcherV3Result
	{
		public string Id { get; protected set; }
		public string Title { get; protected set; }
		public string Description { get; protected set; }
		public DateTime PublishDate { get; protected set; }
		public List<YouTubeVideoThumbnail> Thumbnails { get; protected set; }
		public bool IsLiveBroadcastContent { get; protected set; }
	}
}
