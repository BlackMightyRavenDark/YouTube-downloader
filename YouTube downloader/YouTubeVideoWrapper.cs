using System;
using YouTubeApiLib;

namespace YouTube_downloader
{
	internal class YouTubeVideoWrapper : YouTubeVideo
	{
		internal YouTubeVideoWrapper(YouTubeSearcherV3ResultVideo video) : base(
			video.Title, video.Id, TimeSpan.Zero, DateTime.MaxValue, video.PublishDate,
			video.OwnerChannelTitle, video.OwnerChannelId, video.Description, 0L,
			null, false, false, false, true, false, video.Thumbnails, null, new YouTubeVideoPlayabilityStatus(200))
			{ }
	}
}
