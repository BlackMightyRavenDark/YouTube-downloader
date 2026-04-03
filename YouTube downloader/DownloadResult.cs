
namespace YouTube_downloader
{
	public sealed class DownloadResult
	{
		public int ErrorCode { get; }
		public string ErrorMessage { get; }
		public string FileName { get; }

		public DownloadResult(int errorCode, string errorMessage, string fileName)
		{
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
			FileName = fileName;
		}
	}
}
