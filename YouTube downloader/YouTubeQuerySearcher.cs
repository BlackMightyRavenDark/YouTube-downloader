using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using YouTubeApiLib;

namespace YouTube_downloader
{
	internal class YouTubeQuerySearcher : IYouTubeSearcher
	{
		public string SearchQuery { get; }
		public string SearchOrder { get; }
		public ushort MaxResults { get; }
		public DateTime PublishedAfter { get; }
		public DateTime PublishedBefore { get; }
		public bool SearchVideos { get; }
		public bool SearchChannels { get; }
		public string ApiV3Key { get; }

		public YouTubeQuerySearcher(string searchQuery, string searchOrder, ushort maxResults,
			DateTime publishedAfter, DateTime publishedBefore,
			bool searchVideos, bool searchChannels, string apiV3Key)
		{
			SearchQuery = searchQuery;
			SearchOrder = searchOrder;
			MaxResults = maxResults;
			PublishedAfter = publishedAfter;
			PublishedBefore = publishedBefore;
			SearchVideos = searchVideos;
			SearchChannels = searchChannels;
			ApiV3Key = apiV3Key;
		}

		public YouTubeQuerySearcher(string searchQuery, ushort maxResults,
			DateTime publishedAfter, DateTime publishedBefore,
			bool searchVideos, bool searchChannels, string apiV3Key)
			: this(searchQuery, "date", maxResults, publishedAfter, publishedBefore,
				searchVideos, searchChannels, apiV3Key) { }

		public object Search()
		{
			ConcurrentBag<JObject> channelJsonBag = new ConcurrentBag<JObject>();
			ConcurrentBag<JObject> videoJsonBag = new ConcurrentBag<JObject>();

			List<string> resultTypeList = new List<string>();
			if (SearchChannels) { resultTypeList.Add("channel"); }
			if (SearchVideos) { resultTypeList.Add("video"); }

			string resultTypesJoined = string.Join(",", resultTypeList);
			string url = Utils.GetYouTubeSearchQueryRequestUrl(
				SearchQuery, resultTypesJoined, MaxResults, PublishedAfter, PublishedBefore);

			int errorCode = Utils.DownloadString(url, out string response, true);
			if (errorCode == 200)
			{
				JObject json = Utils.TryParseJson(response);
				if (json == null) { return null; }

				JArray jsonArr = json.Value<JArray>("items");

				if (jsonArr.Count > 0)
				{
					var tasks = jsonArr.Select(item => Task.Run(() =>
					{
						string kind = item.Value<JObject>("id")?.Value<string>("kind");
						if (string.IsNullOrEmpty(kind) || string.IsNullOrWhiteSpace(kind)) { return; }

						if (kind.Equals("youtube#channel"))
						{
							channelJsonBag.Add(item as JObject);
						}
						else if (kind.Equals("youtube#video"))
						{
							videoJsonBag.Add(item as JObject);
						}
					}));
					Task.WhenAll(tasks).Wait();
				}
			}

			JArray jaChannels = new JArray();
			foreach (JObject item in channelJsonBag)
			{
				jaChannels.Add(item);
			}
			JArray jaVideos = new JArray();
			foreach (JObject item in videoJsonBag)
			{
				jaVideos.Add(item);
			}

			JObject jsonResult = new JObject()
			{
				["channels"] = jaChannels,
				["videos"] = jaVideos
			};

			return jsonResult;
		}
	}
}
