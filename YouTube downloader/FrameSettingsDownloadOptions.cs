using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
	public partial class FrameSettingsDownloadOptions : UserControl
	{
		public FrameSettingsDownloadOptions()
		{
			InitializeComponent();

			config.Saving += (s, json) =>
			{
				json["downloadFirstAudioTrackAutomatically"] = config.DownloadFirstAudioTrack;
				json["downloadSecondAudioTrackAutomatically"] = config.DownloadSecondAudioTrack;
				json["ifOnlySecondAudioTrackIsBetter"] = config.IfOnlySecondAudioTrackIsBetter;
				json["downloadAllAudioTracksAutomatically"] = config.DownloadAllAudioTracks;
				json["alwaysUseMkvContainer"] = config.AlwaysUseMkvContainer;
				json["extraDelayAfterContainerWasBuilt"] = config.ExtraDelayAfterContainerWasBuilt;
				json["checkUrlsAccessibilityBeforeDownloading"] = config.CheckUrlsAccessibilityBeforeDownloading;
				json["chunkDownloadTryCountMax"] = config.ChunkDownloadRetryCountMax;
				json["chunkDownloadErrorCountMax"] = config.ChunkDownloadErrorCountMax;
				json["savePreviewImage"] = config.SavePreviewImage;
			};

			config.Loading += (s, json) =>
			{
				{
					JToken jt = json.Value<JToken>("downloadFirstAudioTrackAutomatically");
					if (jt != null)
					{
						config.DownloadFirstAudioTrack = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("downloadSecondAudioTrackAutomatically");
					if (jt != null)
					{
						config.DownloadSecondAudioTrack = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("ifOnlySecondAudioTrackIsBetter");
					if (jt != null)
					{
						config.IfOnlySecondAudioTrackIsBetter = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("downloadAllAudioTracksAutomatically");
					if (jt != null)
					{
						config.DownloadAllAudioTracks = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("alwaysUseMkvContainer");
					if (jt != null)
					{
						config.AlwaysUseMkvContainer = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("extraDelayAfterContainerWasBuilt");
					if (jt != null)
					{
						config.ExtraDelayAfterContainerWasBuilt = ClampValue(jt.Value<int>(), 0, 1000);
					}
				}
				{
					JToken jt = json.Value<JToken>("checkUrlsAccessibilityBeforeDownloading");
					if (jt != null)
					{
						config.CheckUrlsAccessibilityBeforeDownloading = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("chunkDownloadTryCountMax");
					if (jt != null)
					{
						int n = jt.Value<int>();
						config.ChunkDownloadRetryCountMax = ClampValue(n, 0, (int)numericUpDownChunkDownloadTryCountLimit.Maximum);
					}
				}
				{
					JToken jt = json.Value<JToken>("chunkDownloadErrorCountMax");
					if (jt != null)
					{
						config.ChunkDownloadErrorCountMax = jt.Value<int>();
					}
				}
				{
					JToken jt = json.Value<JToken>("savePreviewImage");
					if (jt != null)
					{
						config.SavePreviewImage = jt.Value<bool>();
					}
				}
			};

			config.Loaded += (s) =>
			{
				numericUpDownChunkDownloadTryCountLimit.Value = config.ChunkDownloadRetryCountMax;
				numericUpDownChunkDownloadErrorCountLimit.Value = config.ChunkDownloadErrorCountMax;
				checkBoxAutomaticallyMergeAdaptiveTracks.Checked = config.MergeToContainer;
				checkBoxDeleteSourceFilesWhenMerged.Checked = config.DeleteSourceFiles;
				if (config.AlwaysUseMkvContainer)
				{
					radioButtonContainerTypeMkv.Checked = true;
				}
				else
				{
					radioButtonContainerTypeMp4.Checked = true;
				}
				numericUpDownDelayAfterContainerCreated.Value = config.ExtraDelayAfterContainerWasBuilt;
				checkBoxAutomaticallySaveVideoThumbnailImage.Checked = config.SavePreviewImage;
				checkBoxAutomaticallyDownloadFirstAudioTrack.Checked = config.DownloadFirstAudioTrack;
				checkBoxAutomaticallyDownloadSecondAudioTrack.Checked = config.DownloadSecondAudioTrack;
				checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Checked = config.IfOnlySecondAudioTrackIsBetter;
				checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.Checked = config.DownloadAllAudioTracks;
				checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Checked = config.CheckUrlsAccessibilityBeforeDownloading;
				if (!config.DownloadAllAudioTracks)
				{
					checkBoxAutomaticallyDownloadFirstAudioTrack_CheckedChanged(null, null);
					checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = config.DownloadFirstAudioTrack && config.DownloadSecondAudioTrack;
				}
			};
		}

		private void btnWtfDownloadAllAdaptiveVideoTracks_Click(object sender, EventArgs e)
		{
			btnWtfDownloadAllAdaptiveVideoTracks.Enabled = false;
			string msg = "Будут скачаны все адаптивные форматы видео, " +
				"не зависимо от того, какой формат был выбран.\n" +
				"Данная опция игнорируется при выборе форматов из окна выбора.\n" +
				"Данная опция не сохраняется в файле конфигурации " +
				"и автоматически отключается при каждом перезапуске программы.";
			MessageBox.Show(msg, "Подсказатор подсказок",
				MessageBoxButtons.OK, MessageBoxIcon.Information);
			btnWtfDownloadAllAdaptiveVideoTracks.Enabled = true;
		}

		private void checkBoxAutomaticallyDownloadFirstAudioTrack_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxAutomaticallyDownloadFirstAudioTrack.Checked)
			{
				config.DownloadFirstAudioTrack = true;
				checkBoxAutomaticallyDownloadSecondAudioTrack.Enabled = true;
				checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = checkBoxAutomaticallyDownloadSecondAudioTrack.Checked;
			}
			else
			{
				config.DownloadFirstAudioTrack = false;
				checkBoxAutomaticallyDownloadSecondAudioTrack.Enabled = false;
				checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = false;
			}
		}

		private void checkBoxAutomaticallyDownloadSecondAudioTrack_CheckedChanged(object sender, EventArgs e)
		{
			config.DownloadSecondAudioTrack = checkBoxAutomaticallyDownloadSecondAudioTrack.Checked;
			checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = config.DownloadSecondAudioTrack;
		}

		private void checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger_CheckedChanged(object sender, EventArgs e)
		{
			config.IfOnlySecondAudioTrackIsBetter = checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Checked;
		}

		private void checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks_CheckedChanged(object sender, EventArgs e)
		{
			config.DownloadAllAdaptiveVideoTracks = checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks.Checked;
		}

		private void checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.Checked)
			{
				config.DownloadAllAudioTracks = true;
				checkBoxAutomaticallyDownloadFirstAudioTrack.Enabled = false;
				checkBoxAutomaticallyDownloadSecondAudioTrack.Enabled = false;
				checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = false;
			}
			else
			{
				config.DownloadAllAudioTracks = false;
				checkBoxAutomaticallyDownloadFirstAudioTrack.Enabled = true;
				checkBoxAutomaticallyDownloadSecondAudioTrack.Enabled = true;
				checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = checkBoxAutomaticallyDownloadSecondAudioTrack.Checked;
			}
		}

		private void checkBoxAutomaticallyMergeAdaptiveTracks_CheckedChanged(object sender, EventArgs e)
		{
			config.MergeToContainer = checkBoxAutomaticallyMergeAdaptiveTracks.Checked;
			checkBoxDeleteSourceFilesWhenMerged.Enabled = config.MergeToContainer;
		}

		private void checkBoxAutomaticallySaveVideoThumbnailImage_CheckedChanged(object sender, EventArgs e)
		{
			config.SavePreviewImage = checkBoxAutomaticallySaveVideoThumbnailImage.Checked;
		}

		private void checkBoxCheckUrlsAccessibilityBeforeDownloadStarted_CheckedChanged(object sender, EventArgs e)
		{
			config.CheckUrlsAccessibilityBeforeDownloading = checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Checked;
		}

		private void checkBoxDeleteSourceFilesWhenMerged_CheckedChanged(object sender, EventArgs e)
		{
			config.DeleteSourceFiles = checkBoxDeleteSourceFilesWhenMerged.Checked;
		}

		private void numericUpDownDelayAfterContainerCreated_ValueChanged(object sender, EventArgs e)
		{
			config.ExtraDelayAfterContainerWasBuilt = (int)numericUpDownDelayAfterContainerCreated.Value;
		}
		private void numericUpDownChunkDownloadTryCountLimit_ValueChanged(object sender, EventArgs e)
		{
			config.ChunkDownloadRetryCountMax = (int)numericUpDownChunkDownloadTryCountLimit.Value;
		}

		private void numericUpDownChunkDownloadErrorCount_ValueChanged(object sender, EventArgs e)
		{
			config.ChunkDownloadErrorCountMax = (int)numericUpDownChunkDownloadErrorCountLimit.Value;
		}

		private void radioButtonContainerTypeMp4_CheckedChanged(object sender, EventArgs e)
		{
			config.AlwaysUseMkvContainer = !radioButtonContainerTypeMp4.Checked;
		}

		private void radioButtonContainerTypeMkv_CheckedChanged(object sender, EventArgs e)
		{
			config.AlwaysUseMkvContainerIfPossible = radioButtonContainerTypeMkv.Checked;
        }
    }
}
