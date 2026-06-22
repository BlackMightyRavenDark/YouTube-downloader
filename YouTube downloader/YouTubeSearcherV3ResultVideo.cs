using System;
using System.Collections.Generic;
using YouTubeApiLib;

namespace YouTube_downloader
{
	internal class YouTubeSearcherV3ResultVideo : YouTubeSearcherV3Result
	{
		internal string OwnerChannelId { get; }
		internal string OwnerChannelTitle { get; }

		internal YouTubeSearcherV3ResultVideo(string id, string title, string description, DateTime publishDate,
			string ownerChannelId, string ownerChannelTitle,
			List<YouTubeVideoThumbnail> thumbnails, bool isLiveBroadcastContent)
		{
			Id = id;
			Title = title;
			Description = description;
			PublishDate = publishDate;
			OwnerChannelId = ownerChannelId;
			OwnerChannelTitle = ownerChannelTitle;
			Thumbnails = thumbnails;
			IsLiveBroadcastContent = isLiveBroadcastContent;
		}

		internal YouTubeVideoWrapper ToVideo()
		{
			return new(this);
		}
	}
}
