using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MultiThreadedDownloaderLib;
using YouTubeApiLib;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
	internal class DownloadProgressRenderer
	{
		private readonly MultipleProgressBar _multipleProgressBar;
		private readonly Label _labelProgress;
		private readonly ConcurrentDictionary<int, DownloadableTask> _fileChunks;
		private readonly YouTubeMediaTrack _track;
		private readonly string _shortTrackInfo;
		private readonly long _contentLength;

		internal DownloadProgressRenderer(MultipleProgressBar multipleProgressBar, Label labelProgress,
			ConcurrentDictionary<int, DownloadableTask> fileChunks, YouTubeMediaTrack track, long contentLength)
		{
			_multipleProgressBar = multipleProgressBar;
			_labelProgress = labelProgress;
			_fileChunks = fileChunks;
			_track = track;
			_shortTrackInfo = GetTrackShortInfo(track);
			_contentLength = contentLength;
		}

		internal void Render(bool showEachThreadProgresses, bool updateTextLabel)
		{
			if (_fileChunks != null)
			{
				DownloadableTask[] downloadableTasks = _fileChunks.Select(item => item.Value).ToArray();

				if (showEachThreadProgresses)
				{
					IEnumerable<MultipleProgressBarItem> progressBarItems = ContentChunksToMultipleProgressBarItems(downloadableTasks);
					_multipleProgressBar.SetItems(progressBarItems);
				}

				long fileSize = _contentLength > 0L ? _contentLength : _track.ContentLength;
				if (fileSize <= 0L)
				{
					// Don't do it!
					fileSize = downloadableTasks.Where(task => task.ChunkFileSize > 0L).Sum(task => task.ChunkFileSize);
				}

				if (updateTextLabel)
				{
					calculate(out long downloadedBytes, out double percent, out string percentFormatted);
					_labelProgress.Text = $"{FormatSize(downloadedBytes)} / {FormatSize(fileSize)}" +
						$" ({percentFormatted}%), {_shortTrackInfo}";
					if (!showEachThreadProgresses)
					{
						_multipleProgressBar.SetItem((int)percent, $"{percentFormatted}%");
					}
				}
				else if (!showEachThreadProgresses)
				{
					calculate(out _, out double percent, out string percentFormatted);
					_multipleProgressBar.SetItem((int)percent, $"{percentFormatted}%");
				}

				void calculate(out long downloadedBytes, out double percent, out string percentFormatted)
				{
					downloadedBytes = downloadableTasks.Where(task => task.ProcessedBytes > 0L).Sum(item => item.ProcessedBytes);
					percent = 100.0 / fileSize * downloadedBytes;
					percentFormatted = string.Format("{0:F2}", percent);
				}
			}
			else
			{
				Refresh();
			}
		}

		internal void Refresh()
		{
			_multipleProgressBar.Refresh();
		}

		internal void DisposeChunkStreams()
		{
			if (_fileChunks != null)
			{
				foreach (DownloadableTask task in _fileChunks.Values)
				{
					task.DownloadableChunk.OutputStream.Dispose();
				}
			}
		}

		private static IEnumerable<MultipleProgressBarItem> ContentChunksToMultipleProgressBarItems(
			IEnumerable<DownloadableTask> chunks)
		{
			foreach (DownloadableTask downloadableTask in chunks)
			{
				double percent = 0.0;
				string itemText;
				Color itemColor = Color.Lime;
				switch (downloadableTask.State)
				{
					case DownloadableTaskState.Preparing:
						itemText = "Подготовка...";
						break;

					case DownloadableTaskState.Connecting:
						itemText = "Подключение...";
						break;

					case DownloadableTaskState.Errored:
						{
							getPercentage(downloadableTask, out percent, out string percentFormatted);
							itemText = $"{percentFormatted}% | Error!";
							itemColor = Color.Orange;
							break;
						}

					default:
						{
							getPercentage(downloadableTask, out percent, out string percentFormatted);
							itemText = $"{percentFormatted}%";
							break;
						}
				}

				MultipleProgressBarItem mpi = new((int)percent, itemText, itemColor);
				yield return mpi;
			}

			void getPercentage(DownloadableTask task, out double percent, out string percentFormatted)
			{
				percent = task.ChunkFileSize > 0L && task.ProcessedBytes > 0L ?
					100.0 / task.ChunkFileSize * task.ProcessedBytes : 0L;
				percentFormatted = string.Format("{0:F2}", percent);
			}
		}

		internal static MultipleProgressBarItem[] GenerateChunkMergingProgressVisualizationItems(
			int chunkCount, int currentChunkId, double currentChunkProgressPercent)
		{
			MultipleProgressBarItem[] items = new MultipleProgressBarItem[chunkCount];
			for (int i = 0; i < chunkCount; ++i)
			{
				if (i < currentChunkId) { items[i] = new(100, "100,00%"); }
				else if (i > currentChunkId) { items[i] = new(0, "0,00%"); }
				else
				{
					string percentFormatted = string.Format("{0:F2}", currentChunkProgressPercent);
					items[i] = new((int)currentChunkProgressPercent, $"{percentFormatted}%");
				}
			}
			return items;
		}
	}
}
