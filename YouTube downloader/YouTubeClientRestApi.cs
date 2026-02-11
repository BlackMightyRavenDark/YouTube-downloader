using System;
using System.Collections.Specialized;
using System.Net;
using Newtonsoft.Json.Linq;
using YouTubeApiLib;
using MultiThreadedDownloaderLib;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
	public class YouTubeClientRestApi : IYouTubeClient
	{
		public string DisplayName => "External REST API client";
		public string ServerAddress { get; }
		public ushort ServerPort { get; }
		public int ConnectionTimeout { get; }
		public YouTubeVideoWebPage WebPage { get; private set; }
		public FileDownloader Downloader { get; set; }

		private bool _getUrls;

		public YouTubeClientRestApi(string serverAddress, ushort serverPort,
			int connectionTimeout, bool getUrls,
			YouTubeVideoWebPage videoWebPage = null)
		{
			ServerAddress = serverAddress;
			ServerPort = serverPort;
			ConnectionTimeout = connectionTimeout;
			_getUrls = getUrls;
			WebPage = videoWebPage;
		}

		public JObject GenerateRequestBody(string videoId, YouTubeConfig youTubeConfig = null)
		{
			return null;
		}

		public WebHeaderCollection GenerateRequestHeaders(string videoId, YouTubeConfig youTubeConfig = null)
		{
			return null;
		}

		public YouTubeRawVideoInfoResult GetRawVideoInfo(YouTubeVideoId videoId, out string errorMessage)
		{
			int errorCode = GetRawVideoInfo(videoId.Id, out YouTubeRawVideoInfo rawVideoInfo, out errorMessage);
			return new YouTubeRawVideoInfoResult(errorCode == 200 ? rawVideoInfo : null, errorCode);
		}

		public int GetRawVideoInfo(string videoId, out YouTubeRawVideoInfo rawVideoInfo, out string errorMessage)
		{
			try
			{
				DateTime now = DateTime.UtcNow;

				NameValueCollection query = System.Web.HttpUtility.ParseQueryString(string.Empty);
				query["video_id"] = videoId;
				query["requested_data"] = _getUrls ? "raw_video_info,urls" : "raw_video_info";

				string url = $"{ServerAddress}:{ServerPort}/api/get_video_info?{query}";
				using (HttpRequestResult requestResult = HttpRequestSender.Send(url, ConnectionTimeout))
				{
					if (requestResult.ErrorCode == 200)
					{
						requestResult.GetContent(out _);
						int errorCode = requestResult.WebContent.ContentToString(out string response);
						if (errorCode == 200)
						{
							JObject j = TryParseJson(response, out string e);
							if (j != null)
							{
								JObject jRaw = j.Value<JObject>("raw_video_info");
								JArray ja = j.Value<JArray>("download_urls");
								JObject data = ja != null && ja.Count > 0 ? ja[0].Value<JObject>("streaming_data") : null;
								if (data != null)
								{
									jRaw["streamingData"] = data;
								}
								rawVideoInfo = new YouTubeRawVideoInfo(jRaw.ToString(), this, null, now);
								errorMessage = null;
								return 200;
							}
							else
							{
								System.Diagnostics.Debug.WriteLine(e);
								rawVideoInfo = null;
								errorMessage = e;
								return 404;
							}
						}
						else
						{
							rawVideoInfo = null;
							errorMessage = response;
							return errorCode;
						}
					}
					else
					{
						rawVideoInfo = null;
						errorMessage = requestResult.ErrorMessage;
						return requestResult.ErrorCode;
					}
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.Message);
				rawVideoInfo = null;
				errorMessage = e.Message;
				return e.HResult;
			}
		}

		public void SetWebPage(YouTubeVideoWebPage webPage)
		{
			WebPage = webPage;
		}
	}
}
