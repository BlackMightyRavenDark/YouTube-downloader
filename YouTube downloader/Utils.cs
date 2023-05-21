using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Globalization;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Newtonsoft.Json.Linq;
using MultiThreadedDownloaderLib;
using YouTubeApiLib;

namespace YouTube_downloader
{
    public static class Utils
    {
        public const string YOUTUBE_ACCEPT_STRING = "application/json";
        public const string YOUTUBE_VIDEOS_URL_BASE = "https://www.googleapis.com/youtube/v3/videos";
        public const string YOUTUBE_SEARCH_BASE_URL = "https://www.googleapis.com/youtube/v3/search";
        public const string YOUTUBE_VIDEO_URL_BASE = "https://www.youtube.com/watch?v=";
        public const string YOUTUBE_CHANNEL_URL_TEMPLATE = "https://www.youtube.com/channel/{0}/videos";
        public const string YOUTUBE_GET_VIDEO_INFO_URL = "https://www.youtube.com/get_video_info?video_id=";
        public const string YOUTUBEI_API_URL_TEMPLATE = "https://www.youtube.com/youtubei/v1/player?key={0}";

        public const string FILENAME_FORMAT_DEFAULT = "[<year>-<month>-<day>] <video_title> (id_<video_id>)";
        public static List<YouTubeChannel> channels = new List<YouTubeChannel>();
        public static List<YouTubeApiLib.YouTubeVideo> videos = new List<YouTubeApiLib.YouTubeVideo>();
        public static List<FrameYouTubeChannel> framesChannel = new List<FrameYouTubeChannel>();
        public static List<FrameYouTubeVideo> framesVideo = new List<FrameYouTubeVideo>();

        public static TreeListView treeFavorites = null;
        public static FavoriteItem favoritesRootNode = null;
        public static MainConfiguration config;

        public static readonly bool Is64BitProcess = Environment.Is64BitProcess;

        public const int ERROR_CIPHER_DECRYPTION = -100;
        public const int ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM = -101;

        public static int DownloadString(string url, out string responseString)
        {
            FileDownloader d = new FileDownloader() { Url = url };
            return d.DownloadString(out responseString);
        }

        public static int HttpsPost(string aUrl, string body, out string responseString)
        {
            responseString = "Client error";
            int res = 400;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(aUrl);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentLength = body.Length;
            httpWebRequest.Host = "www.youtube.com";
            httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/72.0.3591.2 Safari/537.36";
            httpWebRequest.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.7");
            httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            httpWebRequest.Method = "POST";
            StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            try
            {
                streamWriter.Write(body);
                streamWriter.Close();
                streamWriter.Dispose();
            }
            catch
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
                return res;
            }
            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());
                try
                {
                    responseString = streamReader.ReadToEnd();
                    streamReader.Close();
                    streamReader.Dispose();
                    res = (int)httpResponse.StatusCode;
                }
                catch
                {
                    if (streamReader != null)
                    {
                        streamReader.Close();
                        streamReader.Dispose();
                    }
                    return 400;
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response;
                    responseString = ex.Message;
                    res = (int)httpWebResponse.StatusCode;
                }
            }
            return res;
        }

        public static int DownloadImage(string url, out Image image)
        {
            image = null;
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    int errorCode = DownloadData(url, memoryStream);
                    if (errorCode == 200)
                    {
                        memoryStream.Position = 0L;
                        image = Image.FromStream(memoryStream);
                    }
                    return errorCode;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                image = null;
                return ex.HResult;
            }
        }

        public static int DownloadData(string url, Stream stream)
        {
            FileDownloader d = new FileDownloader() { Url = url };
            return d.Download(stream);
        }

        /// <summary>
        /// Генерирует тело POST-запроса для получения информации о видео.
        /// Полученный JSON будет содержать всё необходимое, кроме ссылок для скачивания.
        /// Ссылки будут зашифрованы (Cipher, ограничение скорости и т.д.).
        /// </summary>
        /// <param name="videoId">ID видео</param>
        /// <returns>Тело запроса</returns>
        public static JObject GenerateVideoInfoEncryptedRequestBody(string videoId)
        {
            const string CLIENT_VERSION = "2.20201021.03.00";

            JObject jClient = new JObject();
            jClient["hl"] = "en";
            jClient["gl"] = "US";
            jClient["clientName"] = "WEB";
            jClient["clientVersion"] = CLIENT_VERSION;

            JObject jContext = new JObject();
            jContext.Add(new JProperty("client", jClient));

            JObject json = new JObject();
            json.Add(new JProperty("context", jContext));
            json["videoId"] = videoId;

            return json;
        }

        /// <summary>
        /// Генерирует тело POST-запроса для получения информации о видео.
        /// Ответ будет содержать уже расшифрованные ссылки для скачивания
        /// без ограничения скорости, но остальная информация будет не полной.
        /// Используйте этот запрос только для получения ссылок.
        /// Внимание! Этот запрос не работает для видео с доступом только по ссылке (unlisted)!
        /// </summary>
        /// <param name="videoId">ID видео</param>
        /// <returns>Тело запроса</returns>
        public static JObject GenerateVideoInfoDecryptedRequestBody(string videoId)
        {
            const string CLIENT_VERSION = "16.46.37";

            JObject jClient = new JObject();
            jClient["hl"] = "en";
            jClient["gl"] = "US";
            jClient["clientName"] = "ANDROID";
            jClient["clientVersion"] = CLIENT_VERSION;
            jClient["clientScreen"] = null;
            jClient["utcOffsetMinutes"] = 0;

            JObject jContext = new JObject();
            jContext.Add(new JProperty("client", jClient));

            JObject json = new JObject();
            json.Add(new JProperty("context", jContext));
            json["contentCheckOk"] = true;
            json["racyCheckOk"] = true;
            json["videoId"] = videoId;

            return json;
        }

        public static string GetYouTubeChannelVideosRequestUrl(string channelId, int maxVideos)
        {
            NameValueCollection query = System.Web.HttpUtility.ParseQueryString(string.Empty);
            query.Add("part", "snippet");
            query.Add("type", "video");
            query.Add("order", "date");
            query.Add("key", config.YouTubeApiV3Key);
            query.Add("channelId", channelId);
            query.Add("maxResults", maxVideos.ToString());

            string url = $"{YOUTUBE_SEARCH_BASE_URL}?{query}";
            return url;
        }

        public static string GetYouTubeSearchQueryRequestUrl(
            string searchingPhrase, string resultTypes, int maxResults)
        {
            NameValueCollection query = System.Web.HttpUtility.ParseQueryString(string.Empty);
            query.Add("part", "snippet");
            query.Add("order", "date");
            query.Add("key", config.YouTubeApiV3Key);
            query.Add("q", searchingPhrase);
            query.Add("maxResults", maxResults.ToString());
            if (!string.IsNullOrEmpty(resultTypes))
            {
                query.Add("type", resultTypes);
            }

            string url = $"{YOUTUBE_SEARCH_BASE_URL}?{query}";
            return url;
        }

        public static int GetYouTubeVideoWebPage(string videoId, out string resultPage)
        {
            string videoUrl = YOUTUBE_VIDEO_URL_BASE + videoId;
            int res = DownloadString(videoUrl, out resultPage);
            return res;
        }

        public static int GetYouTubeVideoInfoViaApi(string videoId,
            YouTubeApiRequestType requestType, out string resInfo)
        {
            const string apiV1Key = "AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8";
            JObject body = requestType == YouTubeApiRequestType.EncryptedUrls ?
                GenerateVideoInfoEncryptedRequestBody(videoId) : GenerateVideoInfoDecryptedRequestBody(videoId);
            string url = string.Format(YOUTUBEI_API_URL_TEMPLATE, apiV1Key);
            return HttpsPost(url, body.ToString(), out resInfo);
        }

        public static string ExtractPlayerUrlFromWebPage(string webPage)
        {
            int n = webPage.IndexOf("\"jsUrl\":\"");
            if (n > 0)
            {
                string t = webPage.Substring(n + 9);
                string res = t.Substring(0, t.IndexOf("\""));
                if (!string.IsNullOrEmpty(res) && !string.IsNullOrWhiteSpace(res))
                {
                    return "https://www.youtube.com" + res;
                }
            }
            return null;
        }

        public static string ExtractVideoInfoFromWebPage(string webPage)
        {
            int n = webPage.IndexOf("var ytInitialPlayerResponse");
            if (n > 0)
            {
                int n2 = webPage.IndexOf("}};var meta =");
                if (n2 > 0)
                {
                    return webPage.Substring(n + 30, n2 - n - 28);
                }

                n2 = webPage.IndexOf("};\nvar meta =");
                if (n2 > 0)
                {
                    return webPage.Substring(n + 29, n2 - n - 28);
                }

                n2 = webPage.IndexOf("}};var head =");
                if (n2 > 0)
                {
                    return webPage.Substring(n + 30, n2 - n - 28);
                }

                n2 = webPage.IndexOf("};\nvar head =");
                if (n2 > 0)
                {
                    return webPage.Substring(n + 29, n2 - n - 28);
                }

                n2 = webPage.IndexOf(";</script><div");
                if (n2 > 0)
                {
                    return webPage.Substring(n + 30, n2 - n - 30);
                }
            }
            return null;
        }

        public static int GetYouTubeVideoInfoEx(string videoId, out string resInfo, bool useHiddenApi)
        {
            resInfo = "Client error";
            int res = 400;
            if (useHiddenApi)
            {
                res = GetYouTubeVideoInfoViaApi(videoId, YouTubeApiRequestType.DecryptedUrls, out resInfo);
            }
            if (res == 200)
            {
                return res;
            }
            
            res = GetYouTubeVideoWebPage(videoId, out string page);
            if (res == 200)
            {
                resInfo = ExtractVideoInfoFromWebPage(page);
                return string.IsNullOrEmpty(resInfo) ? 404 : res;
            }
            else
            {
                resInfo = page;
            }
            return res;
        }

        public static YouTubeVideo GetSingleVideo(VideoId videoId)
        {
            YouTubeApi.getMediaTracksInfoImmediately = false;
            YouTubeApi.decryptMediaTrackUrlsAutomaticallyIfPossible = false;
            YouTubeApi api = new YouTubeApi();
            if (config.UseHiddenApiForGettingInfo)
            {
                YouTubeVideo video = api.GetVideo(videoId);
                return video;
            }
            else
            {
                YouTubeVideoWebPageResult youTubeVideoWebPageResult = YouTubeVideoWebPage.Get(videoId);
                if (youTubeVideoWebPageResult.ErrorCode == 200)
                {
                    return api.GetVideo(youTubeVideoWebPageResult.VideoWebPage);
                }
            }
            return null;
        }

        public static List<YouTubeMediaTrackVideo> FilterVideoTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
        {
            List<YouTubeMediaTrackVideo> list = new List<YouTubeMediaTrackVideo>();
            foreach (YouTubeMediaTrack track in mediaTracks)
            {
                if (track is YouTubeMediaTrackVideo)
                {
                    list.Add(track as YouTubeMediaTrackVideo);
                }
            }
            return list;
        }

        public static List<YouTubeMediaTrackContainer> FilterContainerTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
        {
            List<YouTubeMediaTrackContainer> list = new List<YouTubeMediaTrackContainer>();
            foreach (YouTubeMediaTrack track in mediaTracks)
            {
                if (track is YouTubeMediaTrackContainer)
                {
                    list.Add(track as YouTubeMediaTrackContainer);
                }
            }
            return list;
        }

        public static List<YouTubeMediaTrackAudio> FilterAudioTracks(IEnumerable<YouTubeMediaTrack> mediaTracks)
        {
            List<YouTubeMediaTrackAudio> list = new List<YouTubeMediaTrackAudio>();
            foreach (YouTubeMediaTrack track in mediaTracks)
            {
                if (track is YouTubeMediaTrackAudio)
                {
                    list.Add(track as YouTubeMediaTrackAudio);
                }
            }
            return list;
        }

        public static string MediaTrackToString(YouTubeMediaTrack track)
        {
            string res = null;
            if (track is YouTubeMediaTrackAudio)
            {
                YouTubeMediaTrackAudio audio = track as YouTubeMediaTrackAudio;
                res = audio.IsDashManifest ? "DASH Audio: " : "Audio: ";
                if (audio.FormatId != 0)
                    res += $"ID {audio.FormatId}";
                if (audio.AverageBitrate != 0)
                    res += $", ~{audio.AverageBitrate / 1024} kbps";
                if (!string.IsNullOrEmpty(audio.FileExtension))
                    res += $", {audio.FileExtension}";
                if (!string.IsNullOrEmpty(audio.MimeCodecs))
                    res += $", {audio.MimeCodecs}";
                if (audio.ContentLength > 0L)
                    res += $", {FormatSize(audio.ContentLength)}";
                if (audio.IsDashManifest && audio.DashUrls != null)
                    res += $", {audio.DashUrls.Count} chunks";
            }
            else if (track is YouTubeMediaTrackVideo)
            {
                YouTubeMediaTrackVideo video = track as YouTubeMediaTrackVideo;
                res = video.IsHlsManifest ? "HLS: " : video.IsDashManifest ? "DASH Video: " : "Video: ";
                if (video.FormatId != 0)
                    res += $"ID {video.FormatId}, ";
                res += $"{video.VideoWidth}x{video.VideoHeight}";
                if (video.FrameRate != 0)
                    res += $", {video.FrameRate} fps";
                if (video.AverageBitrate != 0)
                    res += $", ~{video.AverageBitrate / 1024} kbps";
                if (!string.IsNullOrEmpty(video.FileExtension))
                    res += $", {video.FileExtension}";
                if (!string.IsNullOrEmpty(video.MimeCodecs))
                    res += $", {video.MimeCodecs}";
                if (video.ContentLength > 0L)
                    res += $", {FormatSize(video.ContentLength)}";
                if (video.IsDashManifest && video.DashUrls != null)
                    res += $", {video.DashUrls.Count} chunks";
            }
            else if (track is YouTubeMediaTrackContainer)
            {
                YouTubeMediaTrackContainer container = track as YouTubeMediaTrackContainer;
                res = "Container: ";
                if (container.FormatId != 0)
                    res += $"ID {container.FormatId}, ";
                res += $"{container.VideoWidth}x{container.VideoHeight}";
                if (container.VideoFrameRate != 0)
                    res += $", {container.VideoFrameRate} fps";
                if (container.AverageBitrate != 0)
                    res += $", ~{container.AverageBitrate / 1024} kbps";
                if (!string.IsNullOrEmpty(container.FileExtension))
                    res += $", {container.FileExtension}";
                if (!string.IsNullOrEmpty(container.MimeCodecs))
                    res += $", {container.MimeCodecs}";
                if (container.ContentLength > 0L)
                    res += $", {FormatSize(container.ContentLength)}";
            }
            return res;
        }

        public static string GetTrackShortInfo(YouTubeMediaTrack track)
        {
            string res = null;
            if (track is YouTubeMediaTrackAudio)
            {
                YouTubeMediaTrackAudio audio = track as YouTubeMediaTrackAudio;
                res = string.Empty;
                if (audio.AverageBitrate != 0)
                    res += $", ~{audio.AverageBitrate / 1024} kbps";
                if (!string.IsNullOrEmpty(audio.FileExtension))
                    res += $", {audio.FileExtension}";
                if (!string.IsNullOrEmpty(audio.MimeCodecs))
                    res += $", {audio.MimeCodecs}";
            }
            else if (track is YouTubeMediaTrackVideo)
            {
                YouTubeMediaTrackVideo video = track as YouTubeMediaTrackVideo;
                res = $"{video.VideoWidth}x{video.VideoHeight}";
                if (video.FrameRate != 0)
                    res += $", {video.FrameRate} fps";
                if (video.AverageBitrate != 0)
                    res += $", ~{video.AverageBitrate / 1024} kbps";
            }
            else if (track is YouTubeMediaTrackContainer)
            {
                YouTubeMediaTrackContainer container = track as YouTubeMediaTrackContainer;
                res = $"Video: {container.VideoWidth}x{container.VideoHeight}";
                if (container.VideoFrameRate != 0)
                    res += $", {container.VideoFrameRate} fps";
                if (container.AverageBitrate != 0)
                    res += $", ~{container.AverageBitrate / 1024} kbps";
            }
            return res;
        }

        public static bool StringToDateTime(string inputString, out DateTime resDateTime, string format = "yyyy-MM-dd")
        {
            bool res = DateTime.TryParseExact(inputString, format,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out resDateTime);
            if (!res)
            {
                resDateTime = DateTime.MinValue;
            }
            return res;
        }

        private static FavoriteItem FindFavoriteItem(FavoriteItem item, FavoriteItem root)
        {
            if (root.ID != null && root.ID.Equals(item.ID))
            {
                return root;
            }

            for (int i = 0; i < root.Children.Count; ++i)
            {
                FavoriteItem favoriteItem = FindFavoriteItem(item, root.Children[i]);
                if (favoriteItem != null)
                {
                    return favoriteItem;
                }
            }
            return null;
        }

        public static FavoriteItem FindInFavorites(FavoriteItem find, FavoriteItem root)
        {
            for (int i = 0; i < root.Children.Count; ++i)
            {
                FavoriteItem item = FindFavoriteItem(find, root.Children[i]);
                if (item != null)
                {
                    return item;
                }
            }
            return null;
        }

        public static FavoriteItem FindInFavorites(string itemId)
        {
            FavoriteItem item = new FavoriteItem(null, null, itemId, null, null, null);
            return FindInFavorites(item, favoritesRootNode);
        }

        public static string ExtractVideoIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
            {
                return null;
            }

            Uri uri;
            try
            {
                uri = new Uri(url);
            }
            catch (Exception ex)
            {
                //подразумевается, что юзер ввёл ID видео, а не ссылку.
                Debug.WriteLine(ex.Message);
                return url;
            }

            if (string.IsNullOrEmpty(uri.Query))
            {
                if (!string.IsNullOrEmpty(uri.AbsolutePath) && !string.IsNullOrWhiteSpace(uri.AbsolutePath))
                {
                    if (uri.AbsolutePath.StartsWith("/shorts/", StringComparison.OrdinalIgnoreCase))
                    {
                        return uri.AbsolutePath.Substring(8);
                    }
                    else if (uri.AbsolutePath.StartsWith("/embed/", StringComparison.OrdinalIgnoreCase))
                    {
                        return uri.AbsolutePath.Substring(7);
                    }
                }
                return null;
            }
            Dictionary<string, string> dict = SplitUrlQueryToDictionary(uri.Query);
            if (dict == null || !dict.ContainsKey("v"))
            {
                return null;
            }
          
            return dict["v"];
        }

        public static string DecideMergingDirectory()
        {
            return !string.IsNullOrEmpty(config.ChunksMergingDirPath) &&
                !string.IsNullOrWhiteSpace(config.ChunksMergingDirPath) ?
                config.ChunksMergingDirPath : config.TempDirPath;
        }

        public static bool IsEnoughDiskSpace(IEnumerable<char> driveLetters, long contentLength)
        {
            foreach (char letter in driveLetters)
            {
                DriveInfo driveInfo = new DriveInfo(letter.ToString());
                if (driveInfo.AvailableFreeSpace < contentLength)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool AdvancedFreeSpaceCheck(long totalFilesSize)
        {
            //TODO: Double-check this code for mistakes

            long minimumFreeSpaceRequired = (long)(totalFilesSize * 1.1);

            char chunkMergingDirDriveLetter;
            if (!string.IsNullOrEmpty(config.ChunksMergingDirPath) && !string.IsNullOrWhiteSpace(config.ChunksMergingDirPath))
            {
                chunkMergingDirDriveLetter = config.ChunksMergingDirPath[0];
            }
            else if (!string.IsNullOrEmpty(config.TempDirPath) && !string.IsNullOrWhiteSpace(config.TempDirPath))
            {
                chunkMergingDirDriveLetter = config.TempDirPath[0];
            }
            else
            {
                chunkMergingDirDriveLetter = config.DownloadingDirPath[0];
            }

            char tempDirDriveLetter;
            if (!config.UseRamToStoreTemporaryFiles)
            {
                if (!string.IsNullOrEmpty(config.TempDirPath) && !string.IsNullOrWhiteSpace(config.TempDirPath))
                {
                    tempDirDriveLetter = config.TempDirPath[0];
                }
                else
                {
                    tempDirDriveLetter = config.DownloadingDirPath[0];
                }
            }
            else
            {
                tempDirDriveLetter = chunkMergingDirDriveLetter;
            }

            char downloadingDirDriveLetter = config.DownloadingDirPath[0];

            DriveInfo driveInfoTempDir = new DriveInfo(tempDirDriveLetter.ToString());
            if (tempDirDriveLetter == chunkMergingDirDriveLetter)
            {
                if (config.MergeToContainer && downloadingDirDriveLetter == chunkMergingDirDriveLetter)
                {
                    minimumFreeSpaceRequired += totalFilesSize;
                }
                if (driveInfoTempDir.AvailableFreeSpace < minimumFreeSpaceRequired)
                {
                    return false;
                }
            }
            else
            {
                if (!config.UseRamToStoreTemporaryFiles &&
                    driveInfoTempDir.AvailableFreeSpace < minimumFreeSpaceRequired)
                {
                    return false;
                }

                DriveInfo driveInfoMergingDir = new DriveInfo(chunkMergingDirDriveLetter.ToString());
                if (driveInfoMergingDir.AvailableFreeSpace < minimumFreeSpaceRequired)
                {
                    return false;
                }

                if (downloadingDirDriveLetter == chunkMergingDirDriveLetter)
                {
                    if (config.MergeToContainer)
                    {
                        minimumFreeSpaceRequired += totalFilesSize;
                    }
                    if (!config.UseRamToStoreTemporaryFiles && tempDirDriveLetter == downloadingDirDriveLetter)
                    {
                        minimumFreeSpaceRequired += totalFilesSize;
                    }
                    if (driveInfoMergingDir.AvailableFreeSpace < minimumFreeSpaceRequired)
                    {
                        return false;
                    }
                }
                else
                {
                    DriveInfo driveInfoDownloadingDir = new DriveInfo(downloadingDirDriveLetter.ToString());
                    if (driveInfoDownloadingDir.AvailableFreeSpace < minimumFreeSpaceRequired)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static string LeadZero(int n)
        {
            return n < 10 ? $"0{n}" : n.ToString();
        }

        public static string FormatFileName(string fmt, YouTubeVideo videoInfo)
        {
            return fmt.Replace("<year>", LeadZero(videoInfo.DatePublished.Year))
                .Replace("<month>", LeadZero(videoInfo.DatePublished.Month))
                .Replace("<day>", LeadZero(videoInfo.DatePublished.Day))
                .Replace("<video_title>", videoInfo.Title)
                .Replace("<video_id>", videoInfo.Id);
        }

        public static string FixFileName(string fn)
        {
            return fn.Replace("\\", "\u29F9").Replace("|", "\u2758").Replace("/", "\u2044")
                .Replace("?", "\u2753").Replace(":", "\uFE55").Replace("<", "\u227A").Replace(">", "\u227B")
                .Replace("\"", "\u201C").Replace("*", "\uFE61").Replace("^", "\u2303").Replace("\n", " ");
        }

        public static Dictionary<string,string> SplitStringToKeyValues(
            string inputString, char keySeparaor, char valueSeparator)
        {
            if (string.IsNullOrEmpty(inputString) || string.IsNullOrWhiteSpace(inputString))
            {
                return null;
            }
            string[] keyValues = inputString.Split(keySeparaor);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 0; i < keyValues.Length; ++i)
            {
                string[] t = keyValues[i].Split(valueSeparator);
                dict.Add(t[0], t[1]);
            }
            return dict;
        }

        public static Dictionary<string, string> SplitUrlQueryToDictionary(string urlQuery)
        {
            if (string.IsNullOrEmpty(urlQuery) || string.IsNullOrWhiteSpace(urlQuery))
            {
                return null;
            }
            if (urlQuery[0] == '?')
            {
                urlQuery = urlQuery.Remove(0, 1);
            }
            return SplitStringToKeyValues(urlQuery, '&', '=');
        }

        public static string DecryptCipherSignature(string signatureEncrypted, string algo)
        {
            if (algo.StartsWith("["))
            {
                algo = algo.Remove(0, 1);
            }
            if (algo.EndsWith("]"))
            {
                algo = algo.Remove(algo.Length - 1, 1);
            }
            string[] ints = algo.Split(',');
            string res = string.Empty;
            for (int i = 0; i < ints.Length; ++i)
            {
                if (!int.TryParse(ints[i], out int index))
                {
                    return null;
                }
                if (index >= signatureEncrypted.Length)
                {
                    return null;
                }
                res += signatureEncrypted[index];
            }
            return res;
        }

        public static void SetClipboardText(string text)
        {
            bool res;
            do
            {
                try
                {
                    Clipboard.SetText(text);
                    res = true;
                    return;
                }
                catch
                {
                    res = false;
                }
            } while (!res);
        }

        public static string FormatSize(long n)
        {
            const int KB = 1000;
            const int MB = 1000000;
            const int GB = 1000000000;
            const long TB = 1000000000000;
            long b = n % KB;
            long kb = (n % MB) / KB;
            long mb = (n % GB) / MB;
            long gb = (n % TB) / GB;

            if (n >= 0 && n < KB)
                return string.Format("{0} B", b);
            if (n >= KB && n < MB)
                return string.Format("{0},{1:D3} KB", kb, b);
            if (n >= MB && n < GB)
                return string.Format("{0},{1:D3} MB", mb, kb);
            if (n >= GB && n < TB)
                return string.Format("{0},{1:D3} GB", gb, mb);

            return string.Format("{0} {1:D3} {2:D3} {3:D3} bytes", gb, mb, kb, b);
        }

        public static async Task<bool> MergeYouTubeMediaTracks(IEnumerable<DownloadResult> files,
            string destinationFileName, bool wait = true)
        {
            return await Task.Run(() =>
            {
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "cmd.exe";
                string t = Path.GetFileName(config.FfmpegExeFilePath);
                string ffmpegName = t.Contains(" ") ? $"\"{t}\"" : t;
                string ffmpegPath = Path.GetDirectoryName(config.FfmpegExeFilePath);
                if (!string.IsNullOrEmpty(config.FfmpegExeFilePath))
                {
                    process.StartInfo.WorkingDirectory = ffmpegPath;
                }

                string cmdArgs = $"/k {ffmpegName} ";
                foreach (DownloadResult file in files)
                {
                    cmdArgs += $"-i \"{file.FileName}\" ";
                }
                int iter = 0;
                foreach (DownloadResult file in files)
                {
                    cmdArgs += $"-map {iter}:0 ";
                    ++iter;
                }

                cmdArgs += $"-c copy \"{destinationFileName}\"";
                process.StartInfo.Arguments = cmdArgs;
                bool res = process.Start();
                if (res && wait)
                {
                    process.WaitForExit();
                }
                return res;
            });
        }

        public static bool GrabHls(string hlsUrl, string destinationFileName)
        {
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "cmd.exe";
            string t = Path.GetFileName(config.FfmpegExeFilePath);
            string ffmpegName = t;
            string ffmpegPath = Path.GetDirectoryName(config.FfmpegExeFilePath);
            if (!string.IsNullOrEmpty(config.FfmpegExeFilePath))
            {
                process.StartInfo.WorkingDirectory = ffmpegPath;
            }
            string cmdArgs = $"/k {ffmpegName} -i \"{hlsUrl}\" -c copy \"{destinationFileName}\"";

            process.StartInfo.Arguments = cmdArgs;
            return process.Start();
        }

        public static void OpenUrlInBrowser(string url)
        {
            if (!string.IsNullOrEmpty(config.BrowserExeFilePath) &&
                !string.IsNullOrWhiteSpace(config.BrowserExeFilePath) &&
                !string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
            {
                Process process = new Process();
                process.StartInfo.FileName = config.BrowserExeFilePath;
                process.StartInfo.Arguments = url;
                process.Start();
            }
        }

        public static bool IsFfmpegAvailable()
        {
            return !string.IsNullOrEmpty(config.FfmpegExeFilePath) &&
                        !string.IsNullOrWhiteSpace(config.FfmpegExeFilePath) &&
                        File.Exists(config.FfmpegExeFilePath);
        }

        public static void DrawStar(Graphics graphics, float x, float y, double radius, 
            double rotAngle, float depth, Color color, bool fill)
        {
            PointF[] points = new PointF[11];
            int a = 18;
            for (int i = 0; i < 11; ++i)
            {
                if (i % 2 == 0)
                {
                    int xx = (int)Math.Round(x + Math.Cos((a + rotAngle) * 2 * Math.PI / 360.0) * (radius / depth));
                    int yy = (int)Math.Round(y - Math.Sin((a + rotAngle) * 2 * Math.PI / 360.0) * (radius / depth));
                    points[i] = new PointF(xx, yy);
                }
                else
                {
                    int xx = (int)Math.Round(x + Math.Cos((a + rotAngle) * 2 * Math.PI / 360.0) * radius);
                    int yy = (int)Math.Round(y - Math.Sin((a + rotAngle) * 2 * Math.PI / 360.0) * radius);
                    points[i] = new PointF(xx, yy);

                }
                a += 36;
            }

            if (fill)
            {
                Brush brush = new SolidBrush(color);
                graphics.FillPolygon(brush, points);
                brush.Dispose();
            }
            graphics.DrawPolygon(Pens.Black, points);
        }
    }

    public enum YouTubeApiRequestType { EncryptedUrls, DecryptedUrls }
    public enum FavoriteItemType { Video, Channel, Directory };

    public sealed class FavoriteItem
    {
        public List<FavoriteItem> Children { get; private set; } = new List<FavoriteItem>();
        public FavoriteItem Parent { get; private set; } = null;
        public string DisplayName { get; set; }
        public string Title { get; set; }
        public FavoriteItemType ItemType { get; set; }
        public string ID { get; set; } = null;
        public string ChannelTitle { get; set; } = null;
        public string ChannelId { get; set; } = null;

        public FavoriteItem(string displayName)
        {
            DisplayName = displayName;
            Title = displayName;
        }

        public FavoriteItem(string title, string displayName, string id,
            string channelTitle, string channelId, FavoriteItem parent)
        {
            Title = title;
            DisplayName = displayName;
            ID = id;
            ChannelTitle = channelTitle;
            ChannelId = channelId;
            Parent = parent;
        }
    }

    public sealed class DownloadResult
    {
        public int ErrorCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public string FileName { get; private set; }

        public DownloadResult(int errorCode, string errorMessage, string fileName)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            FileName = fileName;
        }
    }

    public sealed class YouTubeChannel
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public Stream ImageData { get; set; } = null;
    }

    public static class MemoryWatcher
    {
        public static ulong RamTotal { get; private set; } = 0U;
        public static ulong RamUsed { get; private set; } = 0U;
        public static ulong RamFree { get; private set; } = 0U;
        private static Microsoft.VisualBasic.Devices.ComputerInfo computerInfo = null;

        public static bool Update()
        {
            try
            {
                if (computerInfo == null)
                {
                    computerInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
                }
                RamTotal = computerInfo.TotalPhysicalMemory;
                RamFree = computerInfo.AvailablePhysicalMemory;
                RamUsed = RamTotal - RamFree;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                computerInfo = null;
                return false;
            }
        }
    }

    public class FormatListSorterFileSize : IComparer<YouTubeMediaTrack>
    {
        public int Compare(YouTubeMediaTrack x, YouTubeMediaTrack y)
        {
            if (x == null || x.IsDashManifest || x.IsHlsManifest || x.ContentLength <= 0L || y == null)
            {
                return 0;
            }
            return x.ContentLength < y.ContentLength ? 1 : -1;
        }
    }

    public class FormatListSorterDashBitrate : IComparer<YouTubeMediaTrack>
    {
        public int Compare(YouTubeMediaTrack x, YouTubeMediaTrack y)
        {
            if (x != null && y != null && x.IsDashManifest && y.IsDashManifest && !x.IsHlsManifest && !y.IsHlsManifest)
            {
                return x.AverageBitrate < y.AverageBitrate ? 1 : -1;
            }
            return 0;
        }
    }
}
