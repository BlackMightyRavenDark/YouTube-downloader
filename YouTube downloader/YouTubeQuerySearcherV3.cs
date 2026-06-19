using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using MultiThreadedDownloaderLib;
using YouTubeApiLib;

namespace YouTube_downloader
{
	internal class YouTubeQuerySearcherV3 : IYouTubeSearcher
	{
		internal string ApiKey { get; }
		internal string SearchQuery { get; }
		internal DateTime PublishedAfter { get; }
		internal DateTime PublishedBefore { get; }
		internal string ResultTypes { get; }
		internal string ResultsOrder { get; }
		internal ushort ResultCountLimit { get; }
		internal string PageToken { get; }
		internal FileDownloader Downloader { get; }
		internal List<YouTubeSearcherV3ResultVideo> FoundVideos { get; }
		internal List<YouTubeSearcherV3ResultChannel> FoundChannels { get; }

		internal YouTubeQuerySearcherV3(string apiKey, string searchQuery, DateTime publishedAfter, DateTime publishedBefore,
			string resultTypes, string resultsOrder, ushort resultCountLimit, string pageToken, FileDownloader downloader = null)
		{
			if (string.IsNullOrEmpty(apiKey) || string.IsNullOrWhiteSpace(apiKey))
			{
				throw new ArgumentException("The API key is null or empty!");
			}

			if (string.IsNullOrEmpty(searchQuery) || string.IsNullOrWhiteSpace(searchQuery))
			{
				throw new ArgumentException("The search query is null or empty!");
			}

			if (publishedAfter < DateTime.MaxValue && publishedBefore < DateTime.MaxValue && publishedAfter >= publishedBefore)
			{
				throw new ArgumentException("Invalid range of dates!");
			}

			if (string.IsNullOrEmpty(resultTypes) || string.IsNullOrWhiteSpace(resultTypes))
			{
				throw new ArgumentException("The result types is null or empty!");
			}

			if (resultCountLimit == 0 || resultCountLimit > 500)
			{
				throw new ArgumentException("The result count limit must be in range [1..500]!");
			}

			ApiKey = apiKey;
			SearchQuery = searchQuery;
			PublishedAfter = publishedAfter;
			PublishedBefore = publishedBefore;
			ResultTypes = resultTypes;
			ResultsOrder = resultsOrder;
			ResultCountLimit = resultCountLimit;
			PageToken = pageToken;
			Downloader = downloader;
			FoundVideos = new List<YouTubeSearcherV3ResultVideo>();
			FoundChannels = new List<YouTubeSearcherV3ResultChannel>();
		}

		public object Search()
		{
			FoundVideos.Clear();
			FoundChannels.Clear();
			string nextPageToken = PageToken;
			int exceptionInRowCount = 0;
			do
			{
				Dictionary<string, string> requestQuery = new Dictionary<string, string>()
				{
					{ "key", ApiKey },
					{ "part", "snippet" },
					{ "q", SearchQuery },
					{ "type", ResultTypes },
					{ "maxResults", ResultCountLimit.ToString() }
				};
				if (!string.IsNullOrEmpty(ResultsOrder) && !string.IsNullOrWhiteSpace(ResultsOrder))
				{
					requestQuery["order"] = ResultsOrder;
				}
				if (PublishedAfter < DateTime.MaxValue)
				{
					requestQuery["publishedAfter"] = PublishedAfter.ToString("O");
				}
				if (PublishedBefore < DateTime.MaxValue)
				{
					requestQuery["publishedBefore"] = PublishedBefore.ToString("O");
				}
				if (!string.IsNullOrEmpty(nextPageToken) && !string.IsNullOrWhiteSpace(nextPageToken))
				{
					requestQuery["pageToken"] = nextPageToken;
				}
				requestQuery["safeSearch"] = "none";
				string query = string.Join("&", requestQuery.Select(item => $"{item.Key}={item.Value}"));

				FileDownloader d = Downloader ?? Utils.CreateConfiguredDownloader();
				d.Url = $"{Utils.YOUTUBE_ENDPOINT_SEARCH_URL}?{query}";
				d.SkipHeaderRequest = true;
				int errorCode = d.DownloadString(out string response);
				if (errorCode == 200)
				{
					JObject json = Utils.TryParseJson(response);
					if (json != null)
					{
						JArray jaItems = json.Value<JArray>("items");
						if (jaItems != null && jaItems.Count > 0)
						{
							nextPageToken = json.Value<string>("nextPageToken");
							foreach (JObject j in jaItems.Cast<JObject>())
							{
								try
								{
									JObject jId = j.Value<JObject>("id");
									if (jId != null)
									{
										string kind = jId.Value<string>("kind");
										bool isVideo = kind == "youtube#video";
										bool isChannel = kind == "youtube#channel";
										if (isVideo || isChannel)
										{
											JObject jSnippet = j.Value<JObject>("snippet");
											if (ParseSnippet(jSnippet, jId, isVideo, isChannel) &&
												FoundVideos.Count + FoundChannels.Count >= ResultCountLimit)
											{
												break;
											}
											exceptionInRowCount = 0;
										}
									}
								}
#if DEBUG
								catch (Exception ex)
								{
									System.Diagnostics.Debug.WriteLine(ex.Message);
#else
								catch
								{
#endif
									exceptionInRowCount++;
								}
							}
						}
						else
						{
#if DEBUG
							System.Diagnostics.Debug.WriteLine("No items found!");
#endif
							break;
						}
					}
					else
					{
#if DEBUG
						System.Diagnostics.Debug.WriteLine("Can't parse JSON!");
#endif
						break;
					}
				}
				else
				{
#if DEBUG
					System.Diagnostics.Debug.WriteLine($"Failed to search! Error code {errorCode}");
#endif
					break;
				}
			}
			while (!string.IsNullOrEmpty(nextPageToken) && !string.IsNullOrWhiteSpace(nextPageToken) &&
				FoundVideos.Count + FoundChannels.Count < ResultCountLimit && exceptionInRowCount < 3);

			if (FoundVideos.Count > 1)
			{
				FoundVideos.Sort((x, y) => x.PublishDate > y.PublishDate ? -1 : 1);
			}

			return FoundVideos.Count + FoundChannels.Count;
		}

		private bool ParseSnippet(JObject jSnippet, JObject jId, bool isVideo, bool isChannel)
		{
			string id = jId.Value<string>(isVideo ? "videoId" : "channelId");

			// Sometimes, YouTube API returns the same element multiple times.
			if (isVideo && FoundVideos.Any(item => item.Id == id)) { return false; }
			else if (isChannel && FoundChannels.Any(item => item.Id == id)) { return false; }

			string title = jSnippet.Value<string>("title");
			string description = jSnippet.Value<string>("description");
			DateTime publshedAt = jSnippet.Value<DateTime>("publishedAt");
			string channelId = jSnippet.Value<string>("channelId");
			string channelTitle = jSnippet.Value<string>("channelTitle");
			bool isLiveContent = jSnippet.Value<string>("liveBroadcastContent") == "live";

			List<YouTubeVideoThumbnail> thumbnails = new List<YouTubeVideoThumbnail>();
			JObject jThumbnails = jSnippet.Value<JObject>("thumbnails");
			var list = jThumbnails.Properties().Select(item => item.Value as JObject);
			if (list != null && list.Count() > 0)
			{
				foreach (JObject jThumbnail in list)
				{
					string url = jThumbnail.Value<string>("url");
					ushort width = (ushort)(isVideo ? jThumbnail.Value<ushort>("width") : 0);
					ushort height = (ushort)(isVideo ? jThumbnail.Value<ushort>("height") : 0);
					if (isChannel)
					{
						string t = Utils.FindRegExp(url, "s(\\d{1,4})-?");
						if (!string.IsNullOrEmpty(t) && !ushort.TryParse(t, out height)) { height = 0; }
					}
					string fileName = isVideo ? url.Substring(url.LastIndexOf("/") + 1) : $"{height}.jpg";
					thumbnails.Add(new YouTubeVideoThumbnail(width, height, fileName, url));
				}

				if (thumbnails.Count > 1)
				{
					thumbnails.Sort((x, y) => x.Height > y.Height ? -1 : 1);
				}
			}

			if (isVideo)
			{
				YouTubeSearcherV3ResultVideo v3Video = new YouTubeSearcherV3ResultVideo(
					id, title, description, publshedAt, channelId, channelTitle,
					thumbnails, isLiveContent);
				FoundVideos.Add(v3Video);
			}
			else
			{
				YouTubeSearcherV3ResultChannel v3Channel = new YouTubeSearcherV3ResultChannel(
					id, title, description, publshedAt, thumbnails, isLiveContent);
				FoundChannels.Add(v3Channel);
			}

			return true;
		}
	}
}
