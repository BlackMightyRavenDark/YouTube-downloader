using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using YouTubeApiLib;

namespace YouTube_downloader
{
	internal class YouTubeChannelSearcher : IYouTubeSearcher
	{
		public YouTubeChannel Channel { get; }
		public DateTime DateFrom { get; }
		public DateTime DateTo { get; }
		public ushort MaxVideos { get; }
		public string ApiV3Key { get; }

		public YouTubeChannelSearcher(YouTubeChannel channel,
			DateTime dateFrom, DateTime dateTo,
			ushort maxVideos, string apiV3Key)
		{
			Channel = channel;
			DateFrom = dateFrom;
			DateTo = dateTo;
			if (maxVideos == 0) { MaxVideos = 15; }
			else if (maxVideos > 500) { MaxVideos = 500; }
			else { MaxVideos = maxVideos; }
			ApiV3Key = apiV3Key;
		}

		public object Search()
		{
			string channelId = Channel?.Id;
			if (string.IsNullOrEmpty(channelId) || string.IsNullOrWhiteSpace(channelId)) { return null; }
			IYouTubeClient client = YouTubeApi.GetYouTubeClient("video_info");
			if (client == null) { return null; }

			int sum = 0;
			List<string> videoIdList = new List<string>();
			string pageToken = null;
			do
			{
				string url = Utils.GetYouTubeChannelVideosRequestUrl(channelId, MaxVideos, DateFrom, DateTo);

				if (!string.IsNullOrEmpty(pageToken) && !string.IsNullOrWhiteSpace(pageToken))
				{
					url += $"&pageToken={pageToken}";
				}

				int errorCode = Utils.DownloadString(url, out string response, true);
				if (errorCode == 200)
				{
					JObject json = Utils.TryParseJson(response);
					if (json == null) { break; }

					JToken jt = json.Value<JToken>("nextPageToken");
					pageToken = jt?.Value<string>();
					JArray jsonArr = json.Value<JArray>("items");
					if (jsonArr != null && jsonArr.Count > 0)
					{
						foreach (JObject j in jsonArr)
						{
							string videoId = j.Value<JObject>("id")?.Value<string>("videoId");
							if (!string.IsNullOrEmpty(videoId) && !string.IsNullOrWhiteSpace(videoId))
							{
								videoIdList.Add(videoId);
								if (sum++ + 1 >= MaxVideos) { break; }
							}
						}
					}
				}

				if (errorCode != 200 || sum >= MaxVideos) { break; }
			} while (sum < MaxVideos && !string.IsNullOrEmpty(pageToken));

			if (sum <= 0) { return null; }

			ConcurrentBag<YouTubeVideo> videoBag = new ConcurrentBag<YouTubeVideo>();
			const int MAX_SIMULANEOUS = 10;
			int i = 0;
			while (i < sum)
			{
				int remaining = sum - i;
				if (remaining <= 0) { break; }
				int simulaneous = remaining > MAX_SIMULANEOUS ? MAX_SIMULANEOUS : remaining;
				string[] chunk = GetStrings(videoIdList, i, i + simulaneous - 1);
				var tasks = chunk.Select(item => Task.Run(() =>
				{
					YouTubeVideo video = YouTubeVideo.GetById(item, client);
					if (video != null) { videoBag.Add(video); }
				}));
				Task.WhenAll(tasks).Wait();

				i += MAX_SIMULANEOUS;
			}

			List<YouTubeVideo> list = videoBag.ToList();
			if (list.Count > 0)
			{
				list.Sort((x, y) =>
				{
					if ((x.DatePublished == DateTime.MaxValue && y.DatePublished == DateTime.MaxValue) ||
						(x.DatePublished == y.DatePublished))
					{
						return 0;
					}

					return x.DatePublished < y.DatePublished ? 1 : -1;
				});
			}

			return list;
		}

		private static string[] GetStrings(List<string> list, int startId, int lastId)
		{
			string[] strings = new string[lastId - startId + 1];
			for (int i = startId; i <= lastId; ++i)
			{
				strings[i - startId] = list[i];
			}
			return strings;
		}
	}
}
