using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using YouTubeApiLib;
using MultiThreadedDownloaderLib;

namespace YouTube_downloader
{
	internal class YouTubeChannelSearcherV3 : IYouTubeSearcher
	{
		internal string ApiKey { get; }
		internal string ChannelId { get; }
		internal DateTime PublishedAfter { get; }
		internal DateTime PublishedBefore { get; }
		internal ushort ResultCountLimit { get; }
		internal string PageToken { get; }
		internal FileDownloader Downloader { get; }
		internal List<YouTubeSearcherV3ResultVideo> FoundVideos { get; }

		internal YouTubeChannelSearcherV3(string apiKey, string channelId,
			DateTime publishedAfter, DateTime publishedBefore,
			ushort resultCountLimit, string pageToken, FileDownloader downloader = null)
		{
			if (string.IsNullOrEmpty(apiKey) || string.IsNullOrWhiteSpace(apiKey))
			{
				throw new ArgumentException("The API key is null or empty!");
			}

			if (string.IsNullOrEmpty(channelId) || string.IsNullOrWhiteSpace(channelId))
			{
				throw new ArgumentException("The channel ID is null or empty!");
			}

			if (channelId.Contains(" "))
			{
				throw new ArgumentException("The channel ID must not contain spaces!");
			}

			if (publishedAfter < DateTime.MaxValue && publishedBefore < DateTime.MaxValue && publishedAfter >= publishedBefore)
			{
				throw new ArgumentException("Invalid range of dates!");
			}

			if (resultCountLimit == 0 || resultCountLimit > 500)
			{
				throw new ArgumentException("The result count limit must be in range [1..500]!");
			}

			ApiKey = apiKey;
			ChannelId = channelId;
			PublishedAfter = publishedAfter;
			PublishedBefore = publishedBefore;
			ResultCountLimit = resultCountLimit;
			PageToken = pageToken;
			Downloader = downloader;
			FoundVideos = new List<YouTubeSearcherV3ResultVideo>();
		}

		public object Search()
		{
			FoundVideos.Clear();
			string nextPageToken = PageToken;
			int exceptionInRowCount = 0;
			do
			{
				Dictionary<string, string> requestQuery = new Dictionary<string, string>()
				{
					{ "key", ApiKey },
					{ "part", "snippet" },
					{ "channelId", ChannelId },
					{ "maxSearch", ResultCountLimit.ToString() }
				};
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

				FileDownloader d = Downloader ?? new FileDownloader();
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
										if (kind == "youtube#video")
										{
											JObject jSnippet = j.Value<JObject>("snippet");
											if (ParseSnippet(jSnippet, jId) && FoundVideos.Count >= ResultCountLimit) { break; }
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
				FoundVideos.Count < ResultCountLimit && exceptionInRowCount < 3);

			if (FoundVideos.Count > 1)
			{
				FoundVideos.Sort((x, y) => x.PublishDate > y.PublishDate ? -1 : 1);
			}

			return FoundVideos.Count;
		}

		private bool ParseSnippet(JObject jSnippet, JObject jId)
		{
			string id = jId.Value<string>("videoId");

			// Sometimes, YouTube API returns the same element multiple times.
			if (FoundVideos.Any(item => item.Id == id)) { return false; }

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
					ushort width = jThumbnail.Value<ushort>("width");
					ushort height = jThumbnail.Value<ushort>("height");
					string fileName = url.Substring(url.LastIndexOf("/") + 1);
					thumbnails.Add(new YouTubeVideoThumbnail(width, height, fileName, url));
				}

				if (thumbnails.Count > 1)
				{
					thumbnails.Sort((x, y) => x.Height > y.Height ? -1 : 1);
				}
			}

			YouTubeSearcherV3ResultVideo v3Video = new YouTubeSearcherV3ResultVideo(
				id, title, description, publshedAt, channelId, channelTitle,
				thumbnails, isLiveContent);
			FoundVideos.Add(v3Video);
			return true;
		}
	}
}
