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
        public const string YOUTUBE_ACCEPT_STRING = "application/json";
        public const string YOUTUBE_VIDEOS_URL_BASE = "https://www.googleapis.com/youtube/v3/videos";
        public const string YOUTUBE_SEARCH_BASE_URL = "https://www.googleapis.com/youtube/v3/search";
        public const string YOUTUBE_VIDEO_URL_BASE = "https://www.youtube.com/watch?v=";
        public const string YOUTUBE_CHANNEL_URL_TEMPLATE = "https://www.youtube.com/channel/{0}/videos";
        public const string YOUTUBE_GET_VIDEO_INFO_URL = "https://www.youtube.com/get_video_info?video_id=";
        public const string YOUTUBEI_API_URL_TEMPLATE = "https://www.youtube.com/youtubei/v1/player?key={0}";

        public const string FILENAME_FORMAT_DEFAULT = "[<year>-<month>-<day>] <video_title> (id_<video_id>)";
        public static List<YouTubeChannel> channels = new List<YouTubeChannel>();
        public static List<YouTubeVideo> videos = new List<YouTubeVideo>();
        public static List<FrameYouTubeChannel> framesChannel = new List<FrameYouTubeChannel>();
        public static List<FrameYouTubeVideo> framesVideo = new List<FrameYouTubeVideo>();

        public static TreeListView treeFavorites = null;
        public static FavoriteItem favoritesRootNode = null;
        public static MyConfiguration config = new MyConfiguration("config_ytdl.json");

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

        public static int DownloadImage(string url, out Image image)
        {
            MemoryStream memoryStream = new MemoryStream();
            int errorCode = DownloadData(url, memoryStream);
            if (errorCode == 200)
            {
                memoryStream.Position = 0L;
                image = Image.FromStream(memoryStream);
            }
            else
            {
                image = null;
            }
            memoryStream.Dispose();
            return errorCode;
        }

        public static int DownloadData(string url, Stream stream)
        {
            FileDownloader d = new FileDownloader();
            d.Url = url;
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

        public static int GetYouTubeVideoWebPage(string videoId, out string resultPage)
        {
            string videoUrl = YOUTUBE_VIDEO_URL_BASE + videoId;
            int res = DownloadString(videoUrl, out resultPage);
            return res;
        }

        public static int GetYouTubeVideoInfoViaApi(string videoId,
            YouTubeApiRequestType requestType, out string resInfo)
        {
            JObject body = requestType == YouTubeApiRequestType.EncryptedUrls ?
                GenerateVideoInfoEncryptedRequestBody(videoId) : GenerateVideoInfoDecryptedRequestBody(videoId);
            string url = string.Format(YOUTUBEI_API_URL_TEMPLATE, "AIzaSyAO_FJ2SlqU8Q4STEHLGCilw_Y9_11qcW8");
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

        public static int SearchSingleVideo(string videoId, out string resList)
        {
            int errorCode = GetYouTubeVideoInfoEx(videoId, out string response, config.UseHiddenApiForGettingInfo);
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

        private static FavoriteItem FindFavoriteItem(FavoriteItem item, FavoriteItem root)
        {
            if (root.ID != null && root.ID.Equals(item.ID))
            {
                return root;
            }

            for (int i = 0; i < root.Children.Count; i++)
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
            for (int i = 0; i < root.Children.Count; i++)
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

        public static string LeadZero(int n)
        {
            return n < 10 ? ("0" + n.ToString()) : n.ToString();
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
            {
                algo = algo.Remove(0, 1);
            }
            if (algo.EndsWith("]"))
            {
                algo = algo.Remove(algo.Length - 1, 1);
            }
            string[] ints = algo.Split(',');
            string res = string.Empty;
            for (int i = 0; i < ints.Length; i++)
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
                System.Diagnostics.Debug.WriteLine(cmdArgs);
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

    public sealed class MyConfiguration
    {
        public string FilePath { get; private set; }
        public string SelfDirPath { get; set; }
        public string HomeDirPath { get; set; }
        public string DownloadingDirPath { get; set; }
        public string TempDirPath { get; set; }
        public string ChunksMergingDirPath { get; set; }
        public string FavoritesFilePath { get; set; }
        public string OutputFileNameFormat { get; set; }
        public int MaxSearch { get; set; }
        public bool MergeToContainer { get; set; }
        public bool DeleteSourceFiles { get; set; }
        public string CipherDecryptionAlgo { get; set; }
        public string YouTubeApiKey { get; set; }
        public string BrowserExeFilePath { get; set; }
        public string FfmpegExeFilePath { get; set; }
        public bool SaveImagePreview { get; set; }
        public bool UseHiddenApiForGettingInfo { get; set; }
        public int VideoTitleFontSize { get; set; }
        public int MenusFontSize { get; set; }
        public int FavoritesListFontSize { get; set; }
        public int ThreadCountVideo { get; set; }
        public int ThreadCountAudio { get; set; }
        public int GlobalThreadsMaximum { get; set; }

        public MyConfiguration(string fileName)
        {
            SelfDirPath = Path.GetDirectoryName(Application.ExecutablePath);
            bool useAppData = false;
            string[] args = Environment.GetCommandLineArgs();
            foreach (string arg in args)
            {
                if (!string.IsNullOrEmpty(arg) && arg.Equals("/standalone", StringComparison.OrdinalIgnoreCase))
                {
                    useAppData = true;
                    break;
                }
            }
            HomeDirPath = useAppData ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                "\\YouTube downloader\\" : SelfDirPath + "\\";
            FavoritesFilePath = HomeDirPath + "fav.json";
            FilePath = HomeDirPath + fileName;

            LoadDefault();
        }

        public void Save()
        {
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            JObject json = new JObject();
            json["downloadingDirPath"] = DownloadingDirPath;
            json["tempDirPath"] = TempDirPath;
            json["chunksMergingDirPath"] = ChunksMergingDirPath;
            json["cipherDecryptionAlgo"] = CipherDecryptionAlgo;
            json["youTubeApiKey"] = YouTubeApiKey;
            json["browserExeFilePath"] = BrowserExeFilePath;
            json["ffmpegExeFilePath"] = FfmpegExeFilePath;
            json["outputFileNameFormat"] = OutputFileNameFormat;
            json["maxSearch"] = MaxSearch;
            json["saveImagePreview"] = SaveImagePreview;
            json["useHiddenApiForGettingInfo"] = UseHiddenApiForGettingInfo;
            json["videoTitleFontSize"] = VideoTitleFontSize;
            json["menusFontSize"] = MenusFontSize;
            json["favoritesListFontSize"] = FavoritesListFontSize;
            json["threadsVideo"] = ThreadCountVideo;
            json["threadsAudio"] = ThreadCountAudio;
            json["globalThreadsMaximum"] = GlobalThreadsMaximum;
            File.WriteAllText(FilePath, json.ToString());
        }

        public void LoadDefault()
        {
            DownloadingDirPath = null;
            TempDirPath = null;
            ChunksMergingDirPath = null;
            MergeToContainer = true;
            DeleteSourceFiles = true;
            CipherDecryptionAlgo = null;
            YouTubeApiKey = null;
            BrowserExeFilePath = null;
            FfmpegExeFilePath = "FFMPEG.EXE";
            OutputFileNameFormat = Utils.FILENAME_FORMAT_DEFAULT;
            MaxSearch = 50;
            SaveImagePreview = true;
            UseHiddenApiForGettingInfo = true;
            VideoTitleFontSize = 8;
            MenusFontSize = 9;
            FavoritesListFontSize = 8;
            ThreadCountVideo = 8;
            ThreadCountAudio = 4;
            GlobalThreadsMaximum = 300;
        }

        public void Load()
        {
            if (File.Exists(FilePath))
            {
                JObject json = JObject.Parse(File.ReadAllText(FilePath));
                if (json != null)
                {
                    JToken jt = json.Value<JToken>("downloadingDirPath");
                    if (jt != null)
                    {
                        DownloadingDirPath = jt.Value<string>();
                    }
                    jt = json.Value<JToken>("tempDirPath");
                    if (jt != null)
                    {
                        TempDirPath = jt.Value<string>();
                    }
                    jt = json.Value<JToken>("chunksMergingDirPath");
                    if (jt != null)
                    {
                        ChunksMergingDirPath = jt.Value<string>();
                    }
                    jt = json.Value<JToken>("cipherDecryptionAlgo");
                    if (jt != null)
                    {
                        CipherDecryptionAlgo = jt.Value<string>();
                    }
                    jt = json.Value<JToken>("youTubeApiKey");
                    if (jt != null)
                    {
                        YouTubeApiKey = jt.Value<string>();
                    }
                    jt = json.Value<JToken>("browserExeFilePath");
                    if (jt != null)
                    {
                        BrowserExeFilePath = jt.Value<string>();
                    }
                    jt = json.Value<JToken>("ffmpegExeFilePath");
                    if (jt != null)
                    {
                        FfmpegExeFilePath = jt.Value<string>();
                    }
                    jt = json.Value<JToken>("outputFileNameFormat");
                    if (jt != null)
                    {
                        OutputFileNameFormat = jt.Value<string>();
                    }
                    jt = json.Value<JToken>("maxSearch");
                    if (jt != null)
                    {
                        MaxSearch = jt.Value<int>();
                        if (MaxSearch < 1)
                        {
                            MaxSearch = 1;
                        }
                        else if (MaxSearch > 500)
                        {
                            MaxSearch = 500;
                        }
                    }
                    jt = json.Value<JToken>("saveImagePreview");
                    SaveImagePreview = jt == null ? true : jt.Value<bool>();
                    jt = json.Value<JToken>("useApiForGettingInfo");
                    UseHiddenApiForGettingInfo = jt == null ? true : jt.Value<bool>();
                    jt = json.Value<JToken>("menusFontSize");
                    if (jt != null)
                    {
                        MenusFontSize = jt.Value<int>();
                        if (MenusFontSize < 9)
                        {
                            MenusFontSize = 9;
                        }
                        else if (MenusFontSize > 16)
                        {
                            MenusFontSize = 16;
                        }
                    }
                    jt = json.Value<JToken>("favoritesListFontSize");
                    if (jt != null)
                    {
                        FavoritesListFontSize = jt.Value<int>();
                        if (FavoritesListFontSize < 8)
                        {
                            FavoritesListFontSize = 8;
                        }
                        else if (FavoritesListFontSize > 16)
                        {
                            FavoritesListFontSize = 16;
                        }
                    }
                    jt = json.Value<JToken>("videoTitleFontSize");
                    if (jt != null)
                    {
                        VideoTitleFontSize = jt.Value<int>();
                        if (VideoTitleFontSize < 8)
                        {
                            VideoTitleFontSize = 8;
                        }
                        else if (VideoTitleFontSize > 16)
                        {
                            VideoTitleFontSize = 16;
                        }
                    }
                    jt = json.Value<JToken>("threadsVideo");
                    if (jt != null)
                    {
                        ThreadCountVideo = jt.Value<int>();
                    }
                    jt = json.Value<JToken>("threadsAudio");
                    if (jt != null)
                    {
                        ThreadCountAudio = jt.Value<int>();
                    }
                    jt = json.Value<JToken>("globalThreadsMaximum");
                    if (jt != null)
                    {
                        GlobalThreadsMaximum = jt.Value<int>();
                    }
                }
            }
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

    public sealed class YouTubeVideo
    {
        public string Title { get; set; }
        public string Id { get; set; }
        public TimeSpan Length { get; set; } = new TimeSpan(0L);
        public DateTime DateUploaded { get; set; }
        public DateTime DatePublished { get; set; }
        public List<string> ImageUrls { get; set; } = new List<string>();
        public Stream ImageData { get; set; } = null;
        public Image Image { get; set; } = null;
        public YouTubeChannel ChannelOwned { get; set; } = null;
        public bool Ciphered { get; set; } = false;
        public bool Dashed { get; set; } = false;
        public bool Hlsed { get; set; } = false;
        public bool IsFamilySafe { get; set; } = true;
        public bool IsUnlisted { get; set; } = false;
        public bool IsAvailable { get; set; } = true;
    }
}
