
namespace YouTube_downloader
{
    public sealed class YouTubeVideoFile : YouTubeMediaFile
    {
        public int width;
        public int height;
        public int fps;

        public override string ToString()
        {
            string res = isHlsManifest ? "HLS: " : isDashManifest ? "DASH Video: " : "Video: ";
            if (formatId != 0)
                res += $"ID {formatId}, ";
            res += $"{width}x{height}";
            if (fps != 0)
                res += $", {fps} fps";
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
            string res = $"{width}x{height}";
            if (fps != 0)
                res += $", {fps} fps";
            if (averageBitrate != 0)
                res += $", ~{averageBitrate / 1024} kbps";

            return res;
        }
    }
}
