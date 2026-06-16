
namespace YouTube_downloader
{
	public sealed class YouTubeDownloadResult
	{
		public int ErrorCode { get; }
		public string ErrorMessage { get; }
		public string OutputFilePath { get; }

		public YouTubeDownloadResult(int errorCode, string errorMessage, string outputFilePath)
		{
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
			OutputFilePath = outputFilePath;
		}
	}
}
