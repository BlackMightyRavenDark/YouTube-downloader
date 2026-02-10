using System;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using YouTubeApiLib;
using MultiThreadedDownloaderLib;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
	public class ExternalServerClient : IYouTubeClient
	{
		public string DisplayName => "External server client";
		public string ServerAddress { get; }
		public ushort ServerPort { get; }
		public int ConnectionTimeout { get; }
		public YouTubeVideoWebPage WebPage { get; }
		public FileDownloader Downloader { get; set; }

		public ExternalServerClient(string serverAddress, ushort serverPort,
			int connectionTimeout,
			YouTubeVideoWebPage videoWebPage = null)
		{
			ServerAddress = serverAddress;
			ServerPort = serverPort;
			ConnectionTimeout = connectionTimeout;
			WebPage = videoWebPage;
		}

		public JObject GenerateRequestBody(string videoId, YouTubeConfig youTubeConfig = null)
		{
			throw new NotImplementedException();
		}

		public WebHeaderCollection GenerateRequestHeaders(string videoId, YouTubeConfig youTubeConfig = null)
		{
			throw new NotImplementedException();
		}

		public YouTubeRawVideoInfoResult GetRawVideoInfo(YouTubeVideoId videoId, out string errorMessage)
		{
			int errorCode = GetRawVideoInfo(videoId.Id, out YouTubeRawVideoInfo rawVideoInfo, out errorMessage);
			return new YouTubeRawVideoInfoResult(errorCode == 200 ? rawVideoInfo : null, errorCode);
		}

		public int GetRawVideoInfo(string videoId, out YouTubeRawVideoInfo rawVideoInfo, out string errorMessage)
		{
			if (WebPage != null)
			{
				string url = $"{ServerAddress}:{ServerPort}/api/streamingdata";
				string player_url = WebPage.ExtractYouTubeConfig()?.PlayerUrl;
				YouTubeStreamingDataResult streamingDataResult = ExtractStreamingDataFromVideoWebPage(WebPage);
				if (streamingDataResult.ErrorCode == 200)
				{
					JObject j = new JObject();
					j["playerUrl"] = player_url;
					j["streamingData"] = streamingDataResult.Data.RawData;
					WebHeaderCollection headers = new WebHeaderCollection()
					{
						{ "Content-Type", "application/json" }
					};
					HttpRequestResult requestResult = HttpRequestSender.Send("POST", url, j.ToString(), Encoding.UTF8, headers, ConnectionTimeout);
					if (requestResult.ErrorCode == 200)
					{
						if (requestResult.WebContent.ContentToString(out string rawStreamingData) == 200)
						{
							JObject rawData = new JObject()
							{
								["streamingData"] = rawStreamingData
							};

							rawVideoInfo = new YouTubeRawVideoInfo(rawData.ToString(), this, null, DateTime.UtcNow);
							errorMessage = null;
							return 200;
						}
					}
				}
			}
			else
			{
				try
				{
					string url = $"{ServerAddress}:{ServerPort}/api/videoinfo?video_id={videoId}";
					using (HttpRequestResult requestResult = HttpRequestSender.Send(url, ConnectionTimeout))
					{
						if (requestResult.ErrorCode == 200)
						{
							int errorCode = requestResult.WebContent.ContentToString(out string rawInfo);
							if (errorCode == 200)
							{
								rawVideoInfo = new YouTubeRawVideoInfo(rawInfo, this, null, DateTime.UtcNow);
								errorMessage = null;
								return 200;
							}
							else
							{
								rawVideoInfo = null;
								errorMessage = rawInfo;
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
				} catch (Exception e)
				{
					System.Diagnostics.Debug.WriteLine(e.Message);
					rawVideoInfo = null;
					errorMessage = e.Message;
					return e.HResult;
				}
			}

			rawVideoInfo = null;
			errorMessage = null;
			return 400;
		}

		public void SetWebPage(YouTubeVideoWebPage webPage)
		{
			throw new NotImplementedException();
		}
	}
}
