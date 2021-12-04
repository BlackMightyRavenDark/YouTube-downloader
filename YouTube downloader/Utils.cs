using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.Globalization;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Newtonsoft.Json.Linq;

namespace YouTube_downloader
{
    public static class Utils
    {
        public sealed class MyConfiguration
        {
            public string fileName;
            public string selfPath;
            public string downloadingPath;
            public string tempPath;
            public string favoritesFileName;
            public string outputFileNameFormat;
            public int maxSearch;
            public bool mergeToContainer;
            public bool deleteSourceFiles;
            public string cipherDecryptionAlgo;
            public string youTubeApiKey;
            public string browserExe;
            public string ffmpegExe;
            public bool saveImagePreview;
            public bool useApiForGettingInfo;
            public int threadsVideo;
            public int threadsAudio;
            public int globalThreadsMaximum;

            public MyConfiguration(string fileName)
            {
                this.fileName = fileName;
                selfPath = Path.GetDirectoryName(Application.ExecutablePath);
                LoadDefault();
            }

            public void Save()
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                JObject json = new JObject();
                json["downloadingPath"] = downloadingPath;
                json["tempPath"] = tempPath;
                json["cipherDecryptionAlgo"] = cipherDecryptionAlgo;
                json["youTubeApiKey"] = youTubeApiKey;
                json["browserExe"] = browserExe;
                json["ffmpegExe"] = ffmpegExe;
                json["outputFileNameFormat"] = outputFileNameFormat;
                json["maxSearch"] = maxSearch;
                json["saveImagePreview"] = saveImagePreview;
                json["useApiForGettingInfo"] = useApiForGettingInfo;
                json["threadsVideo"] = threadsVideo;
                json["threadsAudio"] = threadsAudio;
                json["globalThreadsMaximum"] = globalThreadsMaximum;
                File.WriteAllText(fileName, json.ToString());
            }

            public void LoadDefault()
            {
                downloadingPath = null;
                tempPath = null;
                favoritesFileName = "fav.json";
                mergeToContainer = true;
                deleteSourceFiles = true;
                cipherDecryptionAlgo = null;
                youTubeApiKey = null;
                browserExe = null;
                ffmpegExe = "FFMPEG.EXE";
                outputFileNameFormat = FILENAME_FORMAT_DEFAULT;
                maxSearch = 50;
                saveImagePreview = true;
                useApiForGettingInfo = true;
                threadsVideo = 8;
                threadsAudio = 4;
                globalThreadsMaximum = 300;
            }

            public void Load()
            {
                if (File.Exists(fileName))
                {
                    JObject json = JObject.Parse(File.ReadAllText(fileName));
                    if (json != null)
                    {
                        JToken jt = json.Value<JToken>("downloadingPath");
                        if (jt != null)
                            downloadingPath = jt.Value<string>();
                        jt = json.Value<JToken>("tempPath");
                        if (jt != null)
                            tempPath = jt.Value<string>();
                        jt = json.Value<JToken>("cipherDecryptionAlgo");
                        if (jt != null)
                            cipherDecryptionAlgo = jt.Value<string>();
                        jt = json.Value<JToken>("youTubeApiKey");
                        if (jt != null)
                            youTubeApiKey = jt.Value<string>();
                        jt = json.Value<JToken>("browserExe");
                        if (jt != null)
                            browserExe = jt.Value<string>();
                        jt = json.Value<JToken>("ffmpegExe");
                        if (jt != null)
                            ffmpegExe = jt.Value<string>();
                        jt = json.Value<JToken>("outputFileNameFormat");
                        if (jt != null)
                            outputFileNameFormat = jt.Value<string>();
                        jt = json.Value<JToken>("maxSearch");
                        if (jt != null)
                        {
                            maxSearch = jt.Value<int>();
                            if (maxSearch < 1)
                            {
                                maxSearch = 1;
                            }
                            else if (maxSearch > 500)
                            {
                                maxSearch = 500;
                            }
                        }
                        jt = json.Value<JToken>("saveImagePreview");
                        saveImagePreview = jt == null ? true : jt.Value<bool>();
                        jt = json.Value<JToken>("useApiForGettingInfo");
                        useApiForGettingInfo = jt == null ? true : jt.Value<bool>();
                        jt = json.Value<JToken>("threadsVideo");
                        if (jt != null)
                        {
                            threadsVideo = jt.Value<int>();
                        }
                        jt = json.Value<JToken>("threadsAudio");
                        if (jt != null)
                        {
                            threadsAudio = jt.Value<int>();
                        }
                        jt = json.Value<JToken>("globalThreadsMaximum");
                        if (jt != null)
                        {
                            globalThreadsMaximum = jt.Value<int>();
                        }
                    }
                }
            }
        }

        public enum DATATYPE { DT_VIDEO, DT_CHANNEL, DT_DIRECTORY };

        public sealed class FavoriteItem
        {
            public List<FavoriteItem> Children { get; private set; } = new List<FavoriteItem>();
            public FavoriteItem Parent { get; private set; } = null;
            public string DisplayName { get; set; }
            public string Title { get; set; }
            public DATATYPE DataType { get; set; }
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
            public string FileName { get; private set; }

            public DownloadResult(int errorCode, string fileName)
            {
                ErrorCode = errorCode;
                FileName = fileName;
            }
        }

        public sealed class YouTubeChannel
        {
            public string title;
            public string id;
            public string imageUrl;
            public List<string> imageUrls = new List<string>();
            public Stream imageStream = null;
        }

        public sealed class YouTubeVideo
        {
            public string title;
            public string id;
            public DateTime length;
            public DateTime dateUploaded;
            public DateTime datePublished;
            public List<string> imageUrls = new List<string>();
            public Stream imageStream = null;
            public YouTubeChannel channelOwned = null;
            public bool ciphered = false;
            public bool dashed = false;
            public bool hlsed = false;
            public bool isFamilySafe = true;
            public bool isUnlisted = false;
        }

        public const string YOUTUBE_ACCEPT_STRING = "application/json";
        public const string YOUTUBE_VIDEOS_URL_BASE = "https://www.googleapis.com/youtube/v3/videos";
        public const string YOUTUBE_SEARCH_BASE_URL = "https://www.googleapis.com/youtube/v3/search";
        public const string YOUTUBE_VIDEO_URL_BASE = "https://www.youtube.com/watch?v=";
        public const string YOUTUBE_CHANNEL_URL_TEMPLATE = "https://www.youtube.com/channel/{0}/videos";
        public const string YOUTUBE_GET_VIDEO_INFO_URL = "https://www.youtube.com/get_video_info?video_id=";
        public const string YOUTUBEI_API_URL_TEMPLATE = "https://www.youtube.com/youtubei/v1/player?key={0}";
        public const string YOUTUBEI_API_POST_BODY_TEMPLATE =
            "{\"context\": {\"client\": {\"clientName\": \"WEB\", \"clientVersion\": \"2.20201021.03.00\"}}, \"videoId\": \"<video_id>\"}";

        public const string FILENAME_FORMAT_DEFAULT = "[<year>-<month>-<day>] <video_title> (id_<video_id>)";
        public static List<YouTubeChannel> channels = new List<YouTubeChannel>();
        public static List<YouTubeVideo> videos = new List<YouTubeVideo>();
        public static List<FrameYouTubeChannel> framesChannel = new List<FrameYouTubeChannel>();
        public static List<FrameYouTubeVideo> framesVideo = new List<FrameYouTubeVideo>();

        public static TreeListView treeFavorites = null;
        public static FavoriteItem favoritesRootNode = null;
        public static MyConfiguration config = new MyConfiguration($"{Application.StartupPath}\\config_ytdl.json");

        public const int ERROR_CIPHER_DECRYPTION = -100;
        public const int ERROR_NO_CIPHER_DECRYPTION_ALGORYTHM = -101;


        public static WebClient GetYouTubeWebClient()
        {
            WebClient wc = new WebClient();
            wc.Headers.Add("Accept", YOUTUBE_ACCEPT_STRING);
            wc.Encoding = Encoding.UTF8;
            return wc;
        }

        public static int DownloadString(string url, out string response)
        {
            WebClient client = GetYouTubeWebClient();
            int result = DownloadString(client, url, out response);
            client.Dispose();
            return result;
        }

        public static int DownloadString(WebClient webClient, string url, out string response)
        {
            int errorCode;
            try
            {
                response = webClient.DownloadString(url);
                errorCode = 200;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpWebResponse httpWebResponse = (HttpWebResponse)e.Response;
                    response = httpWebResponse.StatusDescription;
                    errorCode = (int)httpWebResponse.StatusCode;
                }
                else
                {
                    errorCode = 400;
                    response = "Client error";
                }
            }
            catch (Exception ex)
            {
                response = ex.Message;
                return 400;
            }
            return errorCode;
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


        public static bool DownloadImage(string url, out Image image)
        {
            WebClient webClient = new WebClient();
            try
            {
                Stream stream = webClient.OpenRead(url);
                try
                {
                    image = Image.FromStream(stream);
                    stream?.Dispose();
                    webClient.Dispose();
                    return true;
                }
                catch
                {
                    image = null;
                }
                stream?.Dispose();
            }
            catch
            {
                image = null;
            }
            webClient.Dispose();
            return false;
        }

        public static bool DownloadData(string url, Stream stream)
        {
            WebClient webClient = new WebClient();
            try
            {
                byte[] b = webClient.DownloadData(url);
                stream.Write(b, 0, b.Length);
                webClient.Dispose();
                return true;
            }
            catch
            {

            }
            webClient.Dispose();
            return false;
        }

        public static int GetYouTubeVideoWebPage(string videoId, out string resultPage)
        {
            string videoUrl = YOUTUBE_VIDEO_URL_BASE + videoId;
            int res = DownloadString(videoUrl, out resultPage);
            return res;
        }

        public static int GetYouTubeVideoInfoViaApi(string videoId, out string resInfo)
        {
            string url = string.Format(YOUTUBEI_API_URL_TEMPLATE, "AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8");
            string body = YOUTUBEI_API_POST_BODY_TEMPLATE.Replace("<video_id>", videoId);
            return HttpsPost(url, body, out resInfo);
        }

        public static string ExtractPlayerUrlFromWebPage(string webPage)
        {
            string res = null;
            int n = webPage.IndexOf("\"jsUrl\":\"");
            if (n > 0)
            {
                res = webPage.Substring(n + 9);
                res = res.Substring(0, res.IndexOf("\""));
            }
            return res;
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

                n2 = webPage.IndexOf(";</script><div");
                if (n2 > 0)
                {
                    return webPage.Substring(n + 30, n2 - n - 30);
                }
            }
            return null;
        }

        public static int GetYouTubeVideoInfoEx(string videoId, out string resInfo, bool ciphered = false)
        {
            resInfo = "Client error";
            int res = 400;
            if (!ciphered && config.useApiForGettingInfo)
                res = GetYouTubeVideoInfoViaApi(videoId, out resInfo);
            if (res == 200)
                return res;
            
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

        public static int SearchSingleVideo(string videoId, out string resList)
        {
            int errorCode = GetYouTubeVideoInfoEx(videoId, out string response);
            if (errorCode == 200)
            {
                JObject j = JObject.Parse(response);
                if (j == null)
                {
                    resList = null;
                    return 400;
                }
                JArray jaVideos = new JArray();
                jaVideos.Add(j);
                JObject json = new JObject();
                json.Add(new JProperty("videos", jaVideos));
                resList = json.ToString();
            }
            else
            {
                resList = response;
            }
            return errorCode;
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

        private static FavoriteItem FindData(FavoriteItem data, FavoriteItem root)
        {
            if (root.ID != null && root.ID.Equals(data.ID))
            {
                return root;
            }

            for (int i = 0; i < root.Children.Count; i++)
            {
                FavoriteItem d = FindData(data, root.Children[i]);
                if (d != null)
                {
                    return d;
                }
            }
            return null;
        }

        public static FavoriteItem FindInFavorites(FavoriteItem find, FavoriteItem root)
        {
            for (int i = 0; i < root.Children.Count; i++)
            {
                FavoriteItem item = FindData(find, root.Children[i]);
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
                return null;
            }
            Dictionary<string, string> dict = SplitUrlQueryToDictionary(uri.Query);
            if (dict == null || !dict.ContainsKey("v"))
            {
                return null;
            }
          
            return dict["v"];
        }

        public static string LeadZero(int n)
        {
            return n < 10 ? ("0" + n.ToString()) : n.ToString();
        }

        public static string FormatFileName(string fmt, YouTubeVideo aVideoInfo)
        {
            return fmt.Replace("<year>", LeadZero(aVideoInfo.datePublished.Year))
                .Replace("<month>", LeadZero(aVideoInfo.datePublished.Month))
                .Replace("<day>", LeadZero(aVideoInfo.datePublished.Day))
                .Replace("<video_title>", aVideoInfo.title)
                .Replace("<video_id>", aVideoInfo.id);
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
            for (int i = 0; i < keyValues.Length; i++)
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
                algo = algo.Remove(0, 1);
            if (algo.EndsWith("]"))
                algo = algo.Remove(algo.Length - 1, 1);
            string[] ints = algo.Split(',');
            string res = string.Empty;
            for (int i = 0; i < ints.Length; i++)
            {
                if (!int.TryParse(ints[i], out int index))
                    return null;
                if (index >= signatureEncrypted.Length)
                    return null;
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

        public static async Task<bool> MergeYouTubeMediaTracks(string fileNameVideo,
            string fileNameAudio, string destinationFileName, bool wait = true)
        {
            return await Task.Run(() =>
            {
                Process process = new Process();
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "cmd.exe";
                string t = Path.GetFileName(config.ffmpegExe);
                string ffmpegName = t.Contains(" ") ? $"\"{t}\"" : t;
                string ffmpegPath = Path.GetDirectoryName(config.ffmpegExe);
                if (!string.IsNullOrEmpty(config.ffmpegExe))
                {
                    process.StartInfo.WorkingDirectory = ffmpegPath;
                }
                string cmdArgs = $"/k {ffmpegName} -i \"{fileNameVideo}\" -i \"" +
                    $"{fileNameAudio}\" -c copy \"{destinationFileName}\"";

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
            string t = Path.GetFileName(config.ffmpegExe);
            string ffmpegName = t;
            string ffmpegPath = Path.GetDirectoryName(config.ffmpegExe);
            if (!string.IsNullOrEmpty(config.ffmpegExe))
            {
                process.StartInfo.WorkingDirectory = ffmpegPath;
            }
            string cmdArgs = $"/k " + ffmpegName + " -i \"" + hlsUrl + "\" -c copy \"" + destinationFileName + "\"";

            process.StartInfo.Arguments = cmdArgs;
            return process.Start();
        }

        public static void OpenBrowser(string url)
        {
            if (!string.IsNullOrEmpty(config.browserExe) && !string.IsNullOrWhiteSpace(config.browserExe) &&
                !string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
            {
                Process process = new Process();
                process.StartInfo.FileName = config.browserExe;
                process.StartInfo.Arguments = url;
                process.Start();
            }
        }

        public static void DrawStar(Graphics graphics, float x, float y, double radius, 
            double rotAngle, float depth, Color color, bool fill)
        {
            PointF[] points = new PointF[11];
            int a = 18;
            for (int i = 0; i < 11; i++)
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
}
