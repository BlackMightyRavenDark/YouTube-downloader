using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YouTubeApiLib;

namespace YouTube_downloader
{
	public partial class FormTrackSelector : Form
	{
		public List<YouTubeMediaTrack> SelectedTracks { get; private set; } = new List<YouTubeMediaTrack>();

		public FormTrackSelector(List<YouTubeMediaTrack> mediaTracks)
		{
			InitializeComponent();

			SetupListView();

			List<TrackItem> root = new List<TrackItem>();
			foreach (YouTubeMediaTrack mediaTrack in mediaTracks)
			{
				if (mediaTrack.GetType() == typeof(YouTubeMediaTrackVideo))
				{
					YouTubeMediaTrackVideo videoFile = mediaTrack as YouTubeMediaTrackVideo;
					string resolution = $"{videoFile.VideoWidth}x{videoFile.VideoHeight}";
					int chunkCount = videoFile.DashUrls != null ? videoFile.DashUrls.Count : -1;
					TrackItem trackItem = new TrackItem("Видео", resolution, videoFile.FrameRate,
						videoFile.Bitrate, videoFile.AverageBitrate, videoFile.FileExtension,
						videoFile.ContentLength, chunkCount, mediaTrack);
					root.Add(trackItem);
				}
			}
			foreach (YouTubeMediaTrack mediaTrack in mediaTracks)
			{
				if (mediaTrack.GetType() == typeof(YouTubeMediaTrackAudio))
				{
					YouTubeMediaTrackAudio audioTrack = mediaTrack as YouTubeMediaTrackAudio;
					int chunkCount = audioTrack.DashUrls != null ? audioTrack.DashUrls.Count : -1;
					TrackItem trackItem = new TrackItem("Аудио", null, -1,
						audioTrack.Bitrate, audioTrack.AverageBitrate, audioTrack.FileExtension,
						audioTrack.ContentLength, chunkCount, mediaTrack);
					root.Add(trackItem);
				}
			}

			listViewTracksSelector.Objects = root.ToArray();
		}

		private void SetupListView()
		{
			olvColumnFrameRate.AspectToStringConverter = (obj) =>
			{
				int n = (int)obj;
				return n > 0 ? $"{n} fps" : null;
			};
			olvColumnFormalBitrate.AspectToStringConverter = (obj) =>
			{
				int n = (int)obj;
				return n > 0 ? $"~{n / 1024} kbps" : "Неизвестно";
			};
			olvColumnAverageBitrate.AspectToStringConverter = (obj) =>
			{
				int n = (int)obj;
				return n > 0 ? $"~{n / 1024} kbps" : "Неизвестно";
			};
			olvColumnFileExt.AspectToStringConverter = (obj) =>
			{
				if (obj == null)
				{
					return null;
				}
				return (obj as string).ToUpper();
			};
			olvColumnFileSize.AspectToStringConverter = (obj) =>
			{
				long n = (long)obj;
				return n > 0L ? Utils.FormatSize(n) : "Неизвестно";
			};
			olvColumnChunkCount.AspectToStringConverter = (obj) =>
			{
				int n = (int)obj;
				return n >= 0 ? n.ToString() : null;
			};
		}

		private void btnDownload_Click(object sender, System.EventArgs e)
		{
			var items = listViewTracksSelector.CheckedObjects;
			if (items == null || items.Count == 0)
			{
				MessageBox.Show("Ничего не выбрано!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			foreach (TrackItem item in items)
			{
				SelectedTracks.Add(item.Tag as YouTubeMediaTrack);
			}

			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}

	public sealed class TrackItem
	{
		public string TrackType { get; private set; }
		public string Resolution { get; private set; }
		public int FrameRate { get; private set; }
		public int FormalBitrate { get; private set; }
		public int AverageBitrate { get; private set; }
		public string FileExtension { get; private set; }
		public long FileSize { get; private set; }
		public int ChunkCount { get; private set; }
		public object Tag { get; private set; }

		public TrackItem(string trackType, string resolution, int frameRate,
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
