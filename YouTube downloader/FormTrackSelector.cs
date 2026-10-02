using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
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
			SelectedTracks = new();

			SetupListView();

			List<TrackSelectorItem> root = new();
			foreach (YouTubeMediaTrack mediaTrack in mediaTracks)
			{
				if (mediaTrack.GetType() == typeof(YouTubeMediaTrackVideo))
				{
					YouTubeMediaTrackVideo videoFile = mediaTrack as YouTubeMediaTrackVideo;
					string resolution = $"{videoFile.VideoWidth}x{videoFile.VideoHeight}";
					int chunkCount = videoFile.DashUrls != null ? videoFile.DashUrls.Count : -1;
					TrackSelectorItem trackItem = new("Видео", resolution, videoFile.FrameRate,
						videoFile.Bitrate, videoFile.AverageBitrate, videoFile.FileExtension,
						videoFile.ContentLength, chunkCount, mediaTrack);
					root.Add(trackItem);
				}
			}

			YouTubeMediaTrackAudio[] filteredAudioTracks = (Utils.config.ShowOnlyOriginalAudioTracks ?
				mediaTracks.FilterOriginalAudioTracks() : mediaTracks.Where(track => track is YouTubeMediaTrackAudio).Cast<YouTubeMediaTrackAudio>()).ToArray();
			if (filteredAudioTracks.Length > 0)
			{
				foreach (YouTubeMediaTrack mediaTrack in filteredAudioTracks)
				{
					YouTubeMediaTrackAudio audioTrack = mediaTrack as YouTubeMediaTrackAudio;
					int chunkCount = audioTrack.DashUrls != null ? audioTrack.DashUrls.Count : -1;
					TrackSelectorItem trackItem = new("Аудио", null, -1,
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
			olvColumnVideoResolution.AspectToStringConverter = obj =>
			{
				if (obj is YouTubeMediaTrackAudio trackAudio)
				{
					if (trackAudio.Language == null) { return null; }

					string name = trackAudio.Language.DisplayName;
					return trackAudio.Language.IsOriginal ? $"*{name}" : name;
				}

				YouTubeMediaTrackVideo v = obj as YouTubeMediaTrackVideo;
				return $"{v.VideoWidth}x{v.VideoHeight}";
			};
			olvColumnVideoFrameRate.AspectToStringConverter = obj =>
			{
				if (obj is YouTubeMediaTrackAudio)
				{
					return (obj as YouTubeMediaTrackAudio).FormatExtraInformation();
				}

				int n = (obj as YouTubeMediaTrackVideo).FrameRate;
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

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}
	}
}
