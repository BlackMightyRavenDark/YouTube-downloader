using System.Collections.Generic;

namespace YouTube_downloader
{
    public sealed class YouTubeHlsManifestParser
    {
        private readonly List<YouTubeStreamInfo> streamInfos = new List<YouTubeStreamInfo>();

        public int Count => streamInfos.Count;

        public YouTubeStreamInfo this[int number]
        {
            get
            {
                if (Count == 0 || number < 0 || number >= Count)
                {
                    return null;
                }
                return streamInfos[number];
            }
        }

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
                string t = GetParameter(strings[startIndex], "RESOLUTION");
                string[] widthHeight = t.Split('x');
                if (!int.TryParse(widthHeight[0], out int width))
                {
                    width = 0;
                }
                if (!int.TryParse(widthHeight[1], out int height))
                {
                    height = 0;
                }
                if (!int.TryParse(GetParameter(strings[startIndex], "BANDWIDTH"), out int bandwidth))
                {
                    bandwidth = 0;
                }
                string codecs = GetParameter(strings[startIndex], "CODECS");
                if (!int.TryParse(GetParameter(strings[startIndex], "FRAME-RATE"), out int frameRate))
                {
                    frameRate = 0;
                }
                string url = strings[startIndex + 1];
                int formatId = ExtractFormatId(url);

                streamInfos.Add(new YouTubeStreamInfo(formatId, width, height, bandwidth, codecs, frameRate, url));
            }
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

    public sealed class YouTubeStreamInfo
    {
        public int FormatId { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Bandwidth { get; private set; }
        public string Codecs { get; private set; }
        public int Fps { get; private set; }
        public string Url { get; private set; }

        public YouTubeStreamInfo(int formatId, int width, int height, int bandwidth,
            string codecs, int frameRate, string url)
        {
            FormatId = formatId;
            Width = width;
            Height = height;
            Bandwidth = bandwidth;
            Codecs = codecs;
            Fps = frameRate;
            Url = url;
        }
    }
}
