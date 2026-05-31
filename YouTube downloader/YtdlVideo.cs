using System;
using System.Collections.Generic;
using System.Linq;
using YouTubeApiLib;

namespace YouTube_downloader
{
	internal class YtdlVideo : YouTubeVideo
	{
		internal List<YouTubeMediaTrack> TrackList { get; }
		internal DateTime ReceivedDate { get; private set; }
		internal DateTime UrlsValidUntil { get; private set; }
		internal bool IsLiveInProgress { get; private set; }
		internal bool IsUrlsExpired => DateTime.UtcNow >= UrlsValidUntil;

		internal YtdlVideo(
			string title,
			string id,
			TimeSpan duration,
			DateTime datePublished,
			string ownerChannelTitle,
			string ownerChannelId,
			string description,
			long viewCount,
			string category,
			bool isPrivate,
			bool isFamilySafe,
			bool isLiveContent,
			bool isLiveNow,
			IEnumerable<YouTubeVideoThumbnail> thumbnails,
			IEnumerable<YouTubeMediaTrack> tracks,
			YouTubeVideoPlayabilityStatus status,
			DateTime receivedDate) : base(title, id, duration, DateTime.MaxValue, datePublished,
				ownerChannelTitle, ownerChannelId, description, viewCount, category, false, isPrivate, false,
				isFamilySafe, isLiveContent, thumbnails.ToList(), null, status)
		{
			TrackList = tracks.ToList();
			ReceivedDate = receivedDate;
			UrlsValidUntil = receivedDate.AddHours(6.0);
			IsLiveInProgress = isLiveNow;
		}

		internal bool UpdateTrackList()
		{
			YouTubeClientYtdl client = new YouTubeClientYtdl(Utils.config.YtdlExeFilePath, Utils.config.YtdlParameters, Utils.config.ShowYtdlConsoleWindow);
			YouTubeRawVideoInfoResult raw = client.GetRawVideoInfo(new YouTubeVideoId(Id), out _);
			if (raw.ErrorCode == 200)
			{
				TrackList.Clear();
				foreach (YouTubeMediaTrack track in client.Video.TrackList)
				{
					TrackList.Add(track);
				}

				ReceivedDate = client.Video.ReceivedDate;
				UrlsValidUntil = client.Video.UrlsValidUntil;
				IsLiveInProgress = client.Video.IsLiveInProgress;
			}

			return raw.ErrorCode == 200;
		}
	}
}
