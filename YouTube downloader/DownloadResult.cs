
namespace YouTube_downloader
{
	public sealed class DownloadResult
	{
		public int ErrorCode { get; }
		public string ErrorMessage { get; }
		public string OutputFilePath { get; }

		public DownloadResult(int errorCode, string errorMessage, string outputFilePath)
		{
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
			OutputFilePath = outputFilePath;
		}
	}
}
