using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Xml;
using Newtonsoft.Json.Linq;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
    public sealed class ThreadGetDownloadableFormats
    {
        public delegate void ThreadCompletedDelegate(object sender);
        public delegate void ThreadInfoSendDelegate(object sender, string infoMessage);
        public ThreadCompletedDelegate ThreadCompleted;
        public ThreadInfoSendDelegate Info;

        private bool SortFormatsByFileSize;
        private bool SortDashByBitrate;
        private bool MoveAudioId140First;

        private SynchronizationContext synchronizationContext;

        public int ErrorCode { get; private set; } = 400;
        public List<YouTubeVideoFile> videoFiles = new List<YouTubeVideoFile>();
        public List<YouTubeAudioFile> audioFiles = new List<YouTubeAudioFile>();

        private YouTubeApiRequestType YouTubeApiRequestType;

        public string WebPage { get; private set; }
        public string _videoId;
        public bool _ciphered;

        public ThreadGetDownloadableFormats(YouTubeApiRequestType requestType,
            bool sortFormatsByFileSize, bool sortDashByBitrate,
            bool moveAudioId140First, string webPageContent = null)
        {
            YouTubeApiRequestType = requestType;
            SortFormatsByFileSize = sortFormatsByFileSize;
            SortDashByBitrate = sortDashByBitrate;
            MoveAudioId140First = moveAudioId140First;
            WebPage = webPageContent;
        }

        public void Run(object synchronizationContext)
        {
            this.synchronizationContext = (SynchronizationContext)synchronizationContext;
            this.synchronizationContext?.Send(InfoSend, "Состояние: Определение доступных форматов...");
            try
            {
                string videoInfo = null;
                if (string.IsNullOrEmpty(WebPage) || string.IsNullOrWhiteSpace(WebPage))
                {
                    if (_ciphered && YouTubeApiRequestType != YouTubeApiRequestType.DecryptedUrls)
                    {
                        ErrorCode = GetYouTubeVideoWebPage(_videoId, out string page);
                        if (ErrorCode != 200)
                        {
                            this.synchronizationContext?.Send(Finished, this);
                            return;
                        }
                        if (string.IsNullOrEmpty(page) || string.IsNullOrWhiteSpace(page))
                        {
                            ErrorCode = 400;
                            this.synchronizationContext?.Send(Finished, this);
                            return;
                        }

                        WebPage = page;
                        videoInfo = ExtractVideoInfoFromWebPage(WebPage);
                        if (string.IsNullOrEmpty(videoInfo) || string.IsNullOrWhiteSpace(videoInfo))
                        {
                            ErrorCode = 400;
                            this.synchronizationContext?.Send(Finished, this);
                            return;
                        }
                    }
                    else
                    {
                        ErrorCode = GetYouTubeVideoInfoViaApi(_videoId, YouTubeApiRequestType, out videoInfo);
                    }

                    if (ErrorCode == 200)
                    {
                        ParseFormats(videoInfo);
                    }

                    this.synchronizationContext?.Send(Finished, this);
                    return;
                }

                videoInfo = ExtractVideoInfoFromWebPage(WebPage);
                if (string.IsNullOrEmpty(videoInfo) || string.IsNullOrWhiteSpace(videoInfo))
                {
                    ErrorCode = 404;
                    this.synchronizationContext?.Send(Finished, this);
                    return;
                }

                ParseFormats(videoInfo);
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                ErrorCode = ex.HResult;
            }

            this.synchronizationContext?.Send(Finished, this);
        }

        private void InfoSend(object obj)
        {
            Info?.Invoke(this, (string)obj);
        }

        private void Finished(object obj)
        {
            ThreadCompleted?.Invoke(obj);
        }



        private void ParseDashManifest(string dashManifestString)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(dashManifestString);
            XmlNode nodePeriod = xml.DocumentElement.SelectSingleNode("//*[local-name()='Period']");
            foreach (XmlNode nodeAdaptationSet in nodePeriod)
            {
                if (!nodeAdaptationSet.Name.Equals("AdaptationSet"))
                {
                    continue;
                }
                string mimeType = nodeAdaptationSet.Attributes["mimeType"].Value;
                string[] mimeTypeSplitted = mimeType.Split('/');
                if (mimeType.Contains("video"))
                {
                    foreach (XmlNode node in nodeAdaptationSet)
                    {
                        if (node.Name.Equals("Representation"))
                        {
                            if (!int.TryParse(node.Attributes["width"].Value, out int width))
                            {
                                width = 0;
                            }
                            if (!int.TryParse(node.Attributes["height"].Value, out int height))
                            {
                                height = 0;
                            }
                            if (!int.TryParse(node.Attributes["frameRate"].Value, out int frameRate))
                            {
                                frameRate = 0;
                            }
                            YouTubeVideoFile videoFile = new YouTubeVideoFile(width, height, frameRate);
                            videoFile.dashManifestUrls = new List<string>();
                            videoFile.formatId = int.Parse(node.Attributes["id"].Value);
                            videoFile.bitrate = int.Parse(node.Attributes["bandwidth"].Value);
                            videoFile.averageBitrate = videoFile.bitrate;
                            videoFile.mimeCodecs = node.Attributes["codecs"].Value;
                            videoFile.mimeType = mimeType + "; codecs=\"" + videoFile.mimeCodecs + "\"";
                            videoFile.mimeExt = mimeTypeSplitted[1];
                            videoFile.fileExtension = videoFile.mimeExt == "mp4" ? "m4v" : videoFile.mimeExt;
                            videoFile.isDashManifest = true;
                            videoFile.dashManifestUrls = new List<string>();

                            XmlNode nodeBaseUrl = node.SelectSingleNode("./*[local-name()='BaseURL']");
                            string baseUrl = nodeBaseUrl.InnerText;

                            XmlNode nodeSegmentList = node.SelectSingleNode("./*[local-name()='SegmentList']");
                            XmlNode nodeInitialization = nodeSegmentList.SelectSingleNode("./*[local-name()='Initialization']");
                            if (nodeInitialization != null)
                            {
                                string sourceUrl = nodeInitialization.Attributes["sourceURL"].Value; 
                                videoFile.dashManifestUrls.Add(baseUrl + sourceUrl);
                            }
                            foreach (XmlNode nodeSegment in nodeSegmentList)
                            {
                                if (nodeSegment.Name.Equals("SegmentURL"))
                                {
                                    string segmentUrl = baseUrl + nodeSegment.Attributes["media"].Value;
                                    videoFile.dashManifestUrls.Add(segmentUrl);
                                }
                            }

                            videoFiles.Add(videoFile);
                        }
                    }
                }
                else if (mimeType.Contains("audio"))
                {
                    foreach (XmlNode node in nodeAdaptationSet)
                    {
                        if (node.Name.Equals("Representation"))
                        {
                            YouTubeAudioFile audioFile = new YouTubeAudioFile();
                            audioFile.dashManifestUrls = new List<string>();
                            audioFile.formatId = int.Parse(node.Attributes["id"].Value);
                            audioFile.bitrate = int.Parse(node.Attributes["bandwidth"].Value);
                            audioFile.averageBitrate = audioFile.bitrate;
                            audioFile.mimeCodecs = node.Attributes["codecs"].Value;
                            audioFile.mimeType = mimeType + "; codecs=\"" + audioFile.mimeCodecs + "\"";
                            audioFile.mimeExt = mimeTypeSplitted[1];
                            audioFile.fileExtension = audioFile.mimeExt == "mp4" ? "m4a" : audioFile.mimeExt;
                            audioFile.isDashManifest = true;
                            audioFile.dashManifestUrls = new List<string>();

                            XmlNode nodeBaseUrl = node.SelectSingleNode("./*[local-name()='BaseURL']");
                            string baseUrl = nodeBaseUrl.InnerText;

                            XmlNode nodeSegmentList = node.SelectSingleNode("./*[local-name()='SegmentList']");
                            XmlNode nodeInitialization = nodeSegmentList.SelectSingleNode("./*[local-name()='Initialization']");
                            if (nodeInitialization != null)
                            {
                                string sourceUrl = nodeInitialization.Attributes["sourceURL"].Value;
                                audioFile.dashManifestUrls.Add(baseUrl + sourceUrl);
                            }
                            foreach (XmlNode nodeSegment in nodeSegmentList)
                            {
                                if (nodeSegment.Name.Equals("SegmentURL"))
                                {
                                    string segmentUrl = baseUrl + nodeSegment.Attributes["media"].Value;
                                    audioFile.dashManifestUrls.Add(segmentUrl);
                                }
                            }
                            
                            audioFiles.Add(audioFile);
                        }
                    }
                }
            }
        }

        private void ParseHlsManifest(string hlsManifestString)
        {
            YouTubeHlsManifestParser parser = new YouTubeHlsManifestParser(hlsManifestString);
            for (int i = 0; i < parser.Count; i++)
            {
                YouTubeStreamInfo youTubeStreamInfo = parser[i];
                if (youTubeStreamInfo != null)
                {
                    YouTubeVideoFile videoFile = new YouTubeVideoFile(
                        youTubeStreamInfo.Width, youTubeStreamInfo.Height, youTubeStreamInfo.Fps);
                    videoFile.isHlsManifest = true;
                    videoFile.formatId = youTubeStreamInfo.FormatId;
                    videoFile.bitrate = youTubeStreamInfo.Bandwidth;
                    videoFile.averageBitrate = videoFile.bitrate;
                    videoFile.mimeCodecs = youTubeStreamInfo.Codecs;
                    videoFile.url = youTubeStreamInfo.Url;

                    videoFiles.Add(videoFile);
                }
            }
        }

        private void ParseFormats(string jsonString)
        {
            JObject json = JObject.Parse(jsonString);
            JObject jStreamingData = json.Value<JObject>("streamingData");
            if (jStreamingData == null)
            {
                ErrorCode = 404;
                return;
            }

            JToken jtHls = jStreamingData.Value<JToken>("hlsManifestUrl");
            if (jtHls != null)
            {
                string hlsManifestUrl = jtHls.Value<string>();
                if (DownloadString(hlsManifestUrl, out string hlsManifest) == 200)
                {
                    ParseHlsManifest(hlsManifest);
                }
            }
            JToken jtDash = jStreamingData.Value<JToken>("dashManifestUrl");
            if (jtDash != null)
            {
                string manifestUrl = jtDash.Value<string>();
                if (DownloadString(manifestUrl, out string dashManifest) == 200)
                {
                    ParseDashManifest(dashManifest);
                }
            }

            if (jtDash == null && jtHls == null)
            {
                JArray jaAdaptiveFormats = jStreamingData.Value<JArray>("adaptiveFormats");
                if (jaAdaptiveFormats != null)
                {
                    for (int i = 0; i < jaAdaptiveFormats.Count; i++)
                    {
                        string mime = jaAdaptiveFormats[i].Value<string>("mimeType");
                        if (mime.Contains("video"))
                        {
                            int width = jaAdaptiveFormats[i].Value<int>("width");
                            int height = jaAdaptiveFormats[i].Value<int>("height");
                            int frameRate = jaAdaptiveFormats[i].Value<int>("fps");
                            YouTubeVideoFile videoFile = new YouTubeVideoFile(width, height, frameRate);
                            videoFile.formatId = jaAdaptiveFormats[i].Value<int>("itag");
                            videoFile.mimeType = mime;
                            videoFile.bitrate = jaAdaptiveFormats[i].Value<int>("bitrate");
                            videoFile.averageBitrate = jaAdaptiveFormats[i].Value<int>("averageBitrate");
                            videoFile.quality = jaAdaptiveFormats[i].Value<string>("quality");
                            videoFile.approxDurationMs = int.Parse(jaAdaptiveFormats[i].Value<string>("approxDurationMs"));
                            JToken jt = jaAdaptiveFormats[i].Value<JToken>("contentLength");
                            videoFile.contentLength = jt == null ? 0 : jt.Value<long>();
                            videoFile.isContainer = false;
                            string[] t = mime.Split(';', '/', '=');
                            videoFile.mimeCodecs = t[3] != null ? t[3].Replace("\"", string.Empty) : null;
                            videoFile.mimeExt = t[1];
                            videoFile.fileExtension = videoFile.mimeExt.ToLower() == "mp4" ? "m4v" : "webm";
                            jt = jaAdaptiveFormats[i].Value<JToken>("signatureCipher");
                            if (jt != null)
                            {
                                string t2 = jt.Value<string>();
                                Dictionary<string, string> dict = SplitStringToKeyValues(t2, '&', '=');
                                videoFile.cipherSignatureEncrypted = WebUtility.UrlDecode(dict["s"]);
                                videoFile.cipherUrl = WebUtility.UrlDecode(dict["url"]);
                                videoFile.isCiphered = true;
                            }
                            else
                            {
                                videoFile.url = jaAdaptiveFormats[i].Value<string>("url");
                            }
                            videoFiles.Add(videoFile);
                        }
                        else if (mime.Contains("audio"))
                        {
                            YouTubeAudioFile audioFile = new YouTubeAudioFile();
                            audioFile.formatId = jaAdaptiveFormats[i].Value<int>("itag");
                            audioFile.mimeType = mime;
                            audioFile.bitrate = jaAdaptiveFormats[i].Value<int>("bitrate");
                            audioFile.averageBitrate = jaAdaptiveFormats[i].Value<int>("averageBitrate");
                            audioFile.audioQuality = jaAdaptiveFormats[i].Value<string>("audioQuality");
                            audioFile.quality = jaAdaptiveFormats[i].Value<string>("quality");
                            audioFile.approxDurationMs = int.Parse(jaAdaptiveFormats[i].Value<string>("approxDurationMs"));
                            audioFile.audioSampleRate = int.Parse(jaAdaptiveFormats[i].Value<string>("audioSampleRate"));
                            audioFile.audioChannels = jaAdaptiveFormats[i].Value<int>("audioChannels");
                            audioFile.loudnessDb = jaAdaptiveFormats[i].Value<double>("loudnessDb");
                            JToken jt = jaAdaptiveFormats[i].Value<string>("projectionType");
                            audioFile.projectionType = jt == null ? null : jt.Value<string>();
                            jt = jaAdaptiveFormats[i].Value<JToken>("contentLength");
                            audioFile.contentLength = jt == null ? 0 : jt.Value<long>();
                            audioFile.isContainer = false;
                            string[] t = mime.Split(';', '/', '=');
                            audioFile.mimeCodecs = t[3] != null ? t[3].Replace("\"", string.Empty) : null;
                            audioFile.mimeExt = t[1];
                            audioFile.fileExtension = audioFile.mimeExt.ToLower() == "mp4" ? "m4a" : "weba";
                            jt = jaAdaptiveFormats[i].Value<JToken>("signatureCipher");
                            if (jt != null)
                            {
                                string t2 = jt.Value<string>();
                                Dictionary<string, string> dict = SplitStringToKeyValues(t2, '&', '=');
                                audioFile.cipherSignatureEncrypted = WebUtility.UrlDecode(dict["s"]);
                                audioFile.cipherUrl = WebUtility.UrlDecode(dict["url"]);
                                audioFile.isCiphered = true;
                            }
                            else
                            {
                                audioFile.url = jaAdaptiveFormats[i].Value<string>("url");
                            }
                            audioFiles.Add(audioFile);
                        }
                    }
                }

                JArray jaFormats = jStreamingData.Value<JArray>("formats");
                if (jaFormats != null)
                {
                    for (int i = 0; i < jaFormats.Count; i++)
                    {
                        int width = jaFormats[i].Value<int>("width");
                        int height = jaFormats[i].Value<int>("height");
                        int frameRate = jaFormats[i].Value<int>("fps");
                        YouTubeVideoFile videoFile = new YouTubeVideoFile(width, height, frameRate);
                        videoFile.formatId = jaFormats[i].Value<int>("itag");
                        videoFile.mimeType = jaFormats[i].Value<string>("mimeType");
                        videoFile.bitrate = jaFormats[i].Value<int>("bitrate");
                        videoFile.averageBitrate = jaFormats[i].Value<int>("averageBitrate");
                        videoFile.audioQuality = jaFormats[i].Value<string>("audioQuality");
                        videoFile.quality = jaFormats[i].Value<string>("quality");
                        videoFile.approxDurationMs = int.Parse(jaFormats[i].Value<string>("approxDurationMs"));
                        JToken jt = jaFormats[i].Value<JToken>("audioSampleRate");
                        videoFile.audioSampleRate = jt == null ? 0 : int.Parse(jt.Value<string>());
                        videoFile.audioChannels = jaFormats[i].Value<int>("audioChannels");
                        jt = jaFormats[i].Value<JToken>("contentLength");
                        videoFile.contentLength = jt == null ? 0 : jt.Value<long>();
                        videoFile.isContainer = true;
                        string[] t = videoFile.mimeType.Split(';', '/', '=');
                        videoFile.mimeCodecs = t[3] != null ? t[3].Replace("\"", string.Empty) : null;
                        videoFile.mimeExt = t[1];
                        videoFile.fileExtension = videoFile.mimeExt.ToLower();
                        jt = jaFormats[i].Value<JToken>("signatureCipher");
                        if (jt != null)
                        {
                            string t2 = jt.Value<string>();
                            Dictionary<string, string> dict = SplitStringToKeyValues(t2, '&', '=');
                            videoFile.cipherSignatureEncrypted = WebUtility.UrlDecode(dict["s"]);
                            videoFile.cipherUrl = WebUtility.UrlDecode(dict["url"]);
                            videoFile.isCiphered = true;
                        }
                        else
                        {
                            videoFile.url = jaFormats[i].Value<string>("url");
                        }
                        videoFiles.Add(videoFile);
                    }
                }
            }

            SortTracks();
        }

        private void SortTracks()
        {
            if (SortFormatsByFileSize)
            {
                videoFiles.Sort(new FormatListSorterFileSize());
                audioFiles.Sort(new FormatListSorterFileSize());
            }

            if (SortDashByBitrate)
            {
                //TODO: Fix this shit!
                // Неправильная сортировка видео-дорожек у некоторых не-DASH видео,
                // если флаг "SortDashByBitrate" включен.
                videoFiles.Sort(new FormatListSorterDashBitrate());

                audioFiles.Sort(new FormatListSorterDashBitrate());
            }

            if (MoveAudioId140First)
            {
                for (int i = 0; i < audioFiles.Count; i++)
                {
                    if (audioFiles[i].formatId == 140)
                    {
                        if (i > 0)
                        {
                            YouTubeAudioFile tmp = audioFiles[i];
                            audioFiles[i] = audioFiles[0];
                            audioFiles[0] = tmp;
                        }
                        break;
                    }
                }
            }
        }
    }
}
