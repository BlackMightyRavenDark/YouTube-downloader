
namespace YouTube_downloader
{
	public sealed class TrackSelectorItem
	{
		public string TrackType { get; }
		public string Resolution { get; }
		public int FrameRate { get; }
		public int FormalBitrate { get; }
		public int AverageBitrate { get; }
		public string FileExtension { get; }
		public long FileSize { get; }
		public int ChunkCount { get; }
		public object Tag { get; }

		public TrackSelectorItem(string trackType, string resolution, int frameRate,
			int formalBitrate, int averageBitrate,
			string fileExtension, long fileSize, int chunkCount, object tag)
		{
			TrackType = trackType;
			Resolution = resolution;
			FrameRate = frameRate;
			FormalBitrate = formalBitrate;
			AverageBitrate = averageBitrate;
			FileExtension = fileExtension;
			FileSize = fileSize;
			ChunkCount = chunkCount;
			Tag = tag;
		}
	}
}
