using System.Collections.Generic;

namespace YouTube_downloader
{
    public class YouTubeMediaFile
    {
        public int formatId;
        public string mimeType;
        public string mimeExt;
        public string mimeCodecs;
        public int bitrate;
        public string lastModified = string.Empty;
        public long contentLength = 0;
        public string quality;
        public string qualityLabel;
        public string audioQuality;
        public int audioSampleRate = 0;
        public int audioChannels = 0;

        public int averageBitrate = 0;
        public int approxDurationMs = 0;
        public string projectionType;
        public string url;
        public string cipherSignatureEncrypted;
        public string cipherUrl = null;

        public bool isContainer = false;
        public bool isDashManifest = false;
        public bool isHlsManifest = false;
        public bool isCiphered = false;
        public List<string> dashManifestUrls = null;
        public List<string> hlsManifestUrls = null;
        public string fileExtension;        

        public virtual string GetShortInfo()
        {
            return null;
        }

    }

}
