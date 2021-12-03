
namespace YouTube_downloader
{
    public sealed class YouTubeAudioFile : YouTubeMediaFile
    {
        public double loudnessDb = 0.0;

        public override string ToString()
        {
            string res = isDashManifest ? "DASH Audio: " : "Audio: ";
            if (formatId != 0)
                res += $"ID {formatId}";
            if (averageBitrate != 0)
                res += $", ~{averageBitrate / 1024} kbps";
            if (!string.IsNullOrEmpty(fileExtension))
                res += $", {fileExtension}";
            if (!string.IsNullOrEmpty(mimeCodecs))
                res += $", {mimeCodecs}";
            if (contentLength != 0)
                res += $", {Utils.FormatSize(contentLength)}";
            if (isDashManifest)
                res += $", {dashManifestUrls.Count} chunks";
            return res;
        }

        public override string GetShortInfo()
        {
            return ToString();
        }
    }
}
