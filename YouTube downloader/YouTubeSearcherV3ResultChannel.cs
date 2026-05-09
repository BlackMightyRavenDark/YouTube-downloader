using System;
using System.Collections.Generic;
using YouTubeApiLib;

namespace YouTube_downloader
{
	internal class YouTubeSearcherV3ResultChannel : YouTubeSearcherV3Result
	{
		internal YouTubeSearcherV3ResultChannel(string id, string title, string description, DateTime publishDate,
			List<YouTubeVideoThumbnail> thumbnails, bool isLiveBroadcastContent)
		{
			Id = id;
			Title = title;
			Description = description;
			PublishDate = publishDate;
			Thumbnails = thumbnails;
			IsLiveBroadcastContent = isLiveBroadcastContent;
		}
	}
}
