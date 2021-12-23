
namespace YouTube_downloader
{
    public sealed class YouTubeVideoFile : YouTubeMediaFile
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Fps { get; private set; }

        public YouTubeVideoFile(int width, int height, int frameRate)
        {
            Width = width;
            Height = height;
            Fps = frameRate;
        }

        public override string ToString()
        {
            string res = isHlsManifest ? "HLS: " : isDashManifest ? "DASH Video: " : "Video: ";
            if (formatId != 0)
                res += $"ID {formatId}, ";
            res += $"{Width}x{Height}";
            if (Fps != 0)
                res += $", {Fps} fps";
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
            string res = $"{Width}x{Height}";
            if (Fps != 0)
                res += $", {Fps} fps";
            if (averageBitrate != 0)
                res += $", ~{averageBitrate / 1024} kbps";

            return res;
        }
    }
}
