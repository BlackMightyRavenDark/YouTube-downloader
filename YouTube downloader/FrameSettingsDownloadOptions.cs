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
				json["automaticallyDownloadFirstAudioTrack"] = config.AutomaticallyDownloadFirstAudioTrack;
				json["automaticallyDownloadSecondAudioTrack"] = config.AutomaticallyDownloadSecondAudioTrack;
				json["automaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger"] = config.AutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger;
				json["automaticallyDownloadAllAdaptiveAudioTracks"] = config.AutomaticallyDownloadAllAdaptiveAudioTracks;
				json["alwaysUseMkvContainerIfPossible"] = config.AlwaysUseMkvContainerIfPossible;
				json["extraDelayAfterContainerWasBuilt"] = config.ExtraDelayAfterContainerWasBuilt;
				json["checkUrlsAccessibilityBeforeDownloadStarted"] = config.CheckUrlsAccessibilityBeforeDownloadStarted;
				json["chunkDownloadTryCountLimit"] = config.ChunkDownloadTryCountLimit;
				json["chunkDownloadInnerErrorCountLimit"] = config.ChunkDownloadInnerErrorCountLimit;
				json["automaticallySaveVideoThumbnailImage"] = config.AutomaticallySaveVideoThumbnailImage;
			};

			config.Loading += (s, json) =>
			{
				{
					JToken jt = json.Value<JToken>("automaticallyDownloadFirstAudioTrack");
					if (jt != null)
					{
						config.AutomaticallyDownloadFirstAudioTrack = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("automaticallyDownloadSecondAudioTrack");
					if (jt != null)
					{
						config.AutomaticallyDownloadSecondAudioTrack = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("automaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger");
					if (jt != null)
					{
						config.AutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("automaticallyDownloadAllAdaptiveAudioTracks");
					if (jt != null)
					{
						config.AutomaticallyDownloadAllAdaptiveAudioTracks = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("alwaysUseMkvContainerIfPossible");
					if (jt != null)
					{
						config.AlwaysUseMkvContainerIfPossible = jt.Value<bool>();
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
					JToken jt = json.Value<JToken>("checkUrlsAccessibilityBeforeDownloadStarted");
					if (jt != null)
					{
						config.CheckUrlsAccessibilityBeforeDownloadStarted = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("chunkDownloadTryCountLimit");
					if (jt != null)
					{
						int n = jt.Value<int>();
						config.ChunkDownloadTryCountLimit = ClampValue(n, 0, (int)numericUpDownChunkDownloadTryCountLimit.Maximum);
					}
				}
				{
					JToken jt = json.Value<JToken>("chunkDownloadInnerErrorCountLimit");
					if (jt != null)
					{
						config.ChunkDownloadInnerErrorCountLimit = jt.Value<int>();
					}
				}
				{
					JToken jt = json.Value<JToken>("automaticallySaveVideoThumbnailImage");
					if (jt != null)
					{
						config.AutomaticallySaveVideoThumbnailImage = jt.Value<bool>();
					}
				}
			};

			config.Loaded += (s) =>
			{
				numericUpDownChunkDownloadTryCountLimit.Value = config.ChunkDownloadTryCountLimit;
				numericUpDownChunkDownloadErrorCountLimit.Value = config.ChunkDownloadInnerErrorCountLimit;
				checkBoxAutomaticallyMergeAdaptiveTracks.Checked = config.AutomaticallyMergeToContainer;
				checkBoxDeleteSourceFilesWhenMerged.Checked = config.DeleteSourceFilesWhenMerged;
				if (config.AlwaysUseMkvContainerIfPossible)
				{
					radioButtonContainerTypeMkv.Checked = true;
				}
				else
				{
					radioButtonContainerTypeMp4.Checked = true;
				}
				numericUpDownDelayAfterContainerCreated.Value = config.ExtraDelayAfterContainerWasBuilt;
				checkBoxAutomaticallySaveVideoThumbnailImage.Checked = config.AutomaticallySaveVideoThumbnailImage;
				checkBoxAutomaticallyDownloadFirstAudioTrack.Checked = config.AutomaticallyDownloadFirstAudioTrack;
				checkBoxAutomaticallyDownloadSecondAudioTrack.Checked = config.AutomaticallyDownloadSecondAudioTrack;
				checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Checked = config.AutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger;
				checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.Checked = config.AutomaticallyDownloadAllAdaptiveAudioTracks;
				checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Checked = config.CheckUrlsAccessibilityBeforeDownloadStarted;
				if (!config.AutomaticallyDownloadAllAdaptiveAudioTracks)
				{
					checkBoxAutomaticallyDownloadFirstAudioTrack_CheckedChanged(null, null);
					checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled =
						config.AutomaticallyDownloadFirstAudioTrack && config.AutomaticallyDownloadSecondAudioTrack;
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
				config.AutomaticallyDownloadFirstAudioTrack = true;
				checkBoxAutomaticallyDownloadSecondAudioTrack.Enabled = true;
				checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = checkBoxAutomaticallyDownloadSecondAudioTrack.Checked;
			}
			else
			{
				config.AutomaticallyDownloadFirstAudioTrack = false;
				checkBoxAutomaticallyDownloadSecondAudioTrack.Enabled = false;
				checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = false;
			}
		}

		private void checkBoxAutomaticallyDownloadSecondAudioTrack_CheckedChanged(object sender, EventArgs e)
		{
			config.AutomaticallyDownloadSecondAudioTrack = checkBoxAutomaticallyDownloadSecondAudioTrack.Checked;
			checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = config.AutomaticallyDownloadSecondAudioTrack;
		}

		private void checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger_CheckedChanged(object sender, EventArgs e)
		{
			config.AutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger = checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Checked;
		}

		private void checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks_CheckedChanged(object sender, EventArgs e)
		{
			config.AutomaticallyDownloadAllAdaptiveVideoTracks = checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks.Checked;
		}

		private void checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.Checked)
			{
				config.AutomaticallyDownloadAllAdaptiveAudioTracks = true;
				checkBoxAutomaticallyDownloadFirstAudioTrack.Enabled = false;
				checkBoxAutomaticallyDownloadSecondAudioTrack.Enabled = false;
				checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = false;
			}
			else
			{
				config.AutomaticallyDownloadAllAdaptiveAudioTracks = false;
				checkBoxAutomaticallyDownloadFirstAudioTrack.Enabled = true;
				checkBoxAutomaticallyDownloadSecondAudioTrack.Enabled = true;
				checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = checkBoxAutomaticallyDownloadSecondAudioTrack.Checked;
			}
		}

		private void checkBoxAutomaticallyMergeAdaptiveTracks_CheckedChanged(object sender, EventArgs e)
		{
			config.AutomaticallyMergeToContainer = checkBoxAutomaticallyMergeAdaptiveTracks.Checked;
			checkBoxDeleteSourceFilesWhenMerged.Enabled = config.AutomaticallyMergeToContainer;
		}

		private void checkBoxAutomaticallySaveVideoThumbnailImage_CheckedChanged(object sender, EventArgs e)
		{
			config.AutomaticallySaveVideoThumbnailImage = checkBoxAutomaticallySaveVideoThumbnailImage.Checked;
		}

		private void checkBoxCheckUrlsAccessibilityBeforeDownloadStarted_CheckedChanged(object sender, EventArgs e)
		{
			config.CheckUrlsAccessibilityBeforeDownloadStarted = checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Checked;
		}

		private void checkBoxDeleteSourceFilesWhenMerged_CheckedChanged(object sender, EventArgs e)
		{
			config.DeleteSourceFilesWhenMerged = checkBoxDeleteSourceFilesWhenMerged.Checked;
		}

		private void numericUpDownDelayAfterContainerCreated_ValueChanged(object sender, EventArgs e)
		{
			config.ExtraDelayAfterContainerWasBuilt = (int)numericUpDownDelayAfterContainerCreated.Value;
		}
		private void numericUpDownChunkDownloadTryCountLimit_ValueChanged(object sender, EventArgs e)
		{
			config.ChunkDownloadTryCountLimit = (int)numericUpDownChunkDownloadTryCountLimit.Value;
		}

		private void numericUpDownChunkDownloadErrorCount_ValueChanged(object sender, EventArgs e)
		{
			config.ChunkDownloadInnerErrorCountLimit = (int)numericUpDownChunkDownloadErrorCountLimit.Value;
		}

		private void radioButtonContainerTypeMp4_CheckedChanged(object sender, EventArgs e)
		{
			config.AlwaysUseMkvContainerIfPossible = !radioButtonContainerTypeMp4.Checked;
		}

		private void radioButtonContainerTypeMkv_CheckedChanged(object sender, EventArgs e)
		{
			config.AlwaysUseMkvContainerIfPossible = radioButtonContainerTypeMkv.Checked;
		}
	}
}
