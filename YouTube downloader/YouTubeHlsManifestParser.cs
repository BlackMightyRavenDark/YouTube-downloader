using System.Collections.Generic;

namespace YouTube_downloader
{
    public sealed class YouTubeStreamInfo
    {
        public int formatId;
        public int width;
        public int height;
        public int bandwidth;
        public string codecs;
        public int fps;
        public string url;
    }
    
    public sealed class YouTubeHlsManifestParser
    {
        private List<YouTubeStreamInfo> streamInfos = new List<YouTubeStreamInfo>();

        public int Count => streamInfos.Count;

        public YouTubeHlsManifestParser(string manifest)
        {
            string[] strings = manifest.Split('\n');
            int startIndex;

            for (startIndex = 0; startIndex < strings.Length; startIndex++)
            {
                if (strings[startIndex].StartsWith("#EXT-X-STREAM-INF"))
                {
                    break;
                }
            }

            for (; startIndex < strings.Length - 1; startIndex += 2)
            {
                YouTubeStreamInfo streamInfo = new YouTubeStreamInfo();

                string t = GetParameter(strings[startIndex], "RESOLUTION");
                string[] widthHeight = t.Split('x');
                streamInfo.width = int.Parse(widthHeight[0]);
                streamInfo.height = int.Parse(widthHeight[1]);
                streamInfo.bandwidth = int.Parse(GetParameter(strings[startIndex], "BANDWIDTH"));
                streamInfo.codecs = GetParameter(strings[startIndex], "CODECS");
                streamInfo.fps = int.Parse(GetParameter(strings[startIndex], "FRAME-RATE"));
                streamInfo.url = strings[startIndex + 1];
                streamInfo.formatId = ExtractFormatId(streamInfo.url);
                streamInfos.Add(streamInfo);
            }
        }

        public YouTubeStreamInfo Get(int index)
        {
            return streamInfos[index];
        }

        public string GetParameter(string info, string parameterName)
        {
            if (parameterName.Equals("CODECS"))
            {
                int j = info.IndexOf(parameterName) + 8;
                string s = info.Substring(j);
                string[] s2 = s.Split('"');
                return s2[0];
            }
            int n = info.IndexOf(parameterName);
            string t = info.Substring(n);
            n = t.IndexOf("=");
            t = t.Substring(n + 1);
            string[] t2 = t.Split(',');
            return t2[0];
        }

        private int ExtractFormatId(string url)
        {
            int n = url.IndexOf("/itag/");
            string t = url.Substring(n + 6);
            n = t.IndexOf("/");
            t = t.Substring(0, n);
            return int.Parse(t);
        }
    }
}
