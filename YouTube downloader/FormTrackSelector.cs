using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using YouTubeApiLib;

namespace YouTube_downloader
{
	public partial class FormTrackSelector : Form
	{
		public List<YouTubeMediaTrack> SelectedTracks { get; }

		public FormTrackSelector(List<YouTubeMediaTrack> mediaTracks)
		{
			InitializeComponent();
			SelectedTracks = new List<YouTubeMediaTrack>();

			SetupListView();

			List<TrackSelectorItem> root = new List<TrackSelectorItem>();
			foreach (YouTubeMediaTrack mediaTrack in mediaTracks)
			{
				if (mediaTrack.GetType() == typeof(YouTubeMediaTrackVideo))
				{
					YouTubeMediaTrackVideo videoFile = mediaTrack as YouTubeMediaTrackVideo;
					string resolution = $"{videoFile.VideoWidth}x{videoFile.VideoHeight}";
					int chunkCount = videoFile.DashUrls != null ? videoFile.DashUrls.Count : -1;
					TrackSelectorItem trackItem = new TrackSelectorItem("Видео", resolution, videoFile.FrameRate,
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
					TrackSelectorItem trackItem = new TrackSelectorItem("Аудио", null, -1,
						audioTrack.Bitrate, audioTrack.AverageBitrate, audioTrack.FileExtension,
						audioTrack.ContentLength, chunkCount, mediaTrack);
					root.Add(trackItem);
				}
			}

			listViewTrackSelector.Objects = root.ToArray();
		}

		private void listViewTrackSelector_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
		{
			TrackSelectorItem item = e.Model as TrackSelectorItem;
			if (item.TrackType == "Видео") { e.Item.BackColor = Color.Lime; }
			else if (item.TrackType == "Аудио") { e.Item.BackColor = Color.LightSkyBlue; }
		}

		private void SetupListView()
		{
			olvColumnVideoFrameRate.AspectToStringConverter = obj =>
			{
				int n = (int)obj;
				return n > 0 ? $"{n} fps" : null;
			};
			olvColumnFormalBitrate.AspectToStringConverter = obj =>
			{
				int n = (int)obj;
				return n > 0 ? $"~{n / 1024} kbps" : "Неизвестно";
			};
			olvColumnAverageBitrate.AspectToStringConverter = obj =>
			{
				int n = (int)obj;
				return n > 0 ? $"~{n / 1024} kbps" : "Неизвестно";
			};
			olvColumnFileExtension.AspectToStringConverter = obj =>
			{
				if (obj == null || !(obj is string)) { return null; }
				return (obj as string).ToUpper();
			};
			olvColumnFileSize.AspectToStringConverter = obj =>
			{
				long n = (long)obj;
				return n > 0L ? Utils.FormatSize(n) : "Неизвестно";
			};
			olvColumnChunkCount.AspectToStringConverter = obj =>
			{
				int n = (int)obj;
				return n >= 0 ? n.ToString() : null;
			};
		}

		private void btnDownload_Click(object sender, EventArgs e)
		{
			var items = listViewTrackSelector.CheckedObjects;
			if (items == null || items.Count == 0)
			{
				MessageBox.Show("Ничего не выбрано!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			SelectedTracks.Clear();
			foreach (TrackSelectorItem item in items)
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
}
