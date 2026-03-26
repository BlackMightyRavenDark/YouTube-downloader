using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using static YouTube_downloader.Utils;
using YouTube_downloader.Properties;

namespace YouTube_downloader
{
	public partial class FrameSettingsSystemOptions : UserControl
	{
		public FrameSettingsSystemOptions()
		{
			InitializeComponent();

			config.Saving += (s, json) =>
			{
				json["cipherDecryptionAlgo"] = config.CipherDecryptionAlgo;
				json["youTubeApiV3Key"] = config.YouTubeApiV3Key;
				json["externalRestApiServerUrl"] = config.ExternalRestApiServerUrl;
				json["externalRestApiServerPort"] = config.ExternalRestApiServerPort;
				json["connectionTimeoutExternalRestApiServer"] = config.ConnectionTimeoutExternalRestApiServer;
				json["useExternalRestApiServerToGetBasicVideoInfo"] = config.UseExternalRestApiServerToGetBasicVideoInfo;
				json["useExternalRestApiServerToGetDownloadUrls"] = config.UseExternalRestApiServerToGetDownloadUrls;
				json["useExternalRestApiServerToGetAdultVideos"] = config.UseExternalRestApiServerToGetAdultVideos;
				json["userAgent"] = config.UserAgent;
				json["threadCountVideo"] = config.ThreadCountVideo;
				json["threadCountAudio"] = config.ThreadCountAudio;
				json["globalThreadCountMaximum"] = config.GlobalThreadCountMaximum;
				json["accurateMultithreading"] = config.AccurateMultithreading;
				json["connectionTimeout"] = config.ConnectionTimeout;
				json["useRamToStoreTemporaryFiles"] = config.UseRamToStoreTemporaryFiles;
				json["alwaysDownloadAsDash"] = config.AlwaysDownloadAsDash;
				json["dashManualFragmentationChunkSize"] = config.DashManualFragmentationChunkSize;
				json["dashDownloadRetryCountMax"] = config.DashDownloadRetryCountMax;
				json["useGmtTime"] = config.UseGmtTime;
			};

			config.Loading += (s, json) =>
			{
				{
					JToken jt = json.Value<JToken>("cipherDecryptionAlgo");
					if (jt != null)
					{
						config.CipherDecryptionAlgo = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("youTubeApiV3Key");
					if (jt != null)
					{
						config.YouTubeApiV3Key = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("externalRestApiServerUrl");
					if (jt != null)
					{
						config.ExternalRestApiServerUrl = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("externalRestApiServerPort");
					if (jt != null)
					{
						config.ExternalRestApiServerPort = jt.Value<ushort>();
					}
				}
				{
					JToken jt = json.Value<JToken>("connectionTimeoutExternalRestApiServer");
					if (jt != null)
					{
						int min = (int)numericUpDownConnectionTimeoutExternalRestApiServer.Minimum;
						int max = (int)numericUpDownConnectionTimeoutExternalRestApiServer.Maximum;
						config.ConnectionTimeoutExternalRestApiServer = ClampValue(jt.Value<int>(), min, max);
					}
				}
				{
					JToken jt = json.Value<JToken>("useExternalRestApiServerToGetBasicVideoInfo");
					if (jt != null)
					{
						config.UseExternalRestApiServerToGetBasicVideoInfo = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("useExternalRestApiServerToGetDownloadUrls");
					if (jt != null)
					{
						config.UseExternalRestApiServerToGetDownloadUrls = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("useExternalRestApiServerToGetAdultVideos");
					if (jt != null)
					{
						config.UseExternalRestApiServerToGetAdultVideos = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("userAgent");
					config.UserAgent = jt != null ? jt.Value<string>()?.Trim() : Configurator.DEFAULT_USER_AGENT;
				}
				{
					JToken jt = json.Value<JToken>("threadCountVideo");
					if (jt != null)
					{
						config.ThreadCountVideo = jt.Value<int>();
					}
				}
				{
					JToken jt = json.Value<JToken>("threadCountAudio");
					if (jt != null)
					{
						config.ThreadCountAudio = jt.Value<int>();
					}
				}
				{
					JToken jt = json.Value<JToken>("globalThreadCountMaximum");
					if (jt != null)
					{
						config.GlobalThreadCountMaximum = jt.Value<int>();
					}
				}
				{
					JToken jt = json.Value<JToken>("accurateMultithreading");
					if (jt != null)
					{
						config.AccurateMultithreading = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("connectionTimeout");
					if (jt != null)
					{
						int min = (int)numericUpDownConnectionTimeout.Minimum;
						int max = (int)numericUpDownConnectionTimeout.Maximum;
						config.ConnectionTimeout = ClampValue(jt.Value<int>(), min, max);
					}
				}
				{
					JToken jt = json.Value<JToken>("useRamToStoreTemporaryFiles");
					if (jt != null)
					{
						config.UseRamToStoreTemporaryFiles = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("alwaysDownloadAsDash");
					if (jt != null)
					{
						config.AlwaysDownloadAsDash = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("dashManualFragmentationChunkSize");
					if (jt != null)
					{
						config.DashManualFragmentationChunkSize = jt.Value<long>();
					}
				}
				{
					JToken jt = json.Value<JToken>("dashDownloadRetryCountMax");
					if (jt != null)
					{
						config.DashDownloadRetryCountMax = jt.Value<int>();
					}
				}
				{
					JToken jt = json.Value<JToken>("useGmtTime");
					if (jt != null)
					{
						config.UseGmtTime = jt.Value<bool>();
					}
				}
			};

			config.Loaded += s =>
			{
				textBoxCipherDecryptionAlgorythm.Text = config.CipherDecryptionAlgo;
				textBoxYouTubeApiV3Key.Text = config.YouTubeApiV3Key;
				textBoxExternalRestApiServerUrl.Text = config.ExternalRestApiServerUrl;
				numericUpDownExternalRestApiServerPort.Value = config.ExternalRestApiServerPort;
				numericUpDownConnectionTimeoutExternalRestApiServer.Value = config.ConnectionTimeoutExternalRestApiServer;
				checkBoxUseExternalRestApiServerToGetBasicVideoInfo.Checked = config.UseExternalRestApiServerToGetBasicVideoInfo;
				checkBoxUseExternalRestApiServerToGetDownloadUrls.Checked = config.UseExternalRestApiServerToGetDownloadUrls;
				checkBoxUseExternalRestApiServerToGetAdultVideos.Checked = config.UseExternalRestApiServerToGetAdultVideos;
				textBoxUserAgent.Text = config.UserAgent;
				numericUpDownThreadCountVideo.Value = config.ThreadCountVideo;
				numericUpDownThreadCountAudio.Value = config.ThreadCountAudio;
				numericUpDownGlobalThreadCountLimit.Value = config.GlobalThreadCountMaximum;
				checkBoxUseAccurateMultithreading.Checked = config.AccurateMultithreading;
				numericUpDownConnectionTimeout.Value = config.ConnectionTimeout;
				checkBoxAlwaysDownloadAsDash.Checked = config.AlwaysDownloadAsDash;
				numericUpDownDashChunkDownloadTryCountLimit.Value = config.DashDownloadRetryCountMax;
				numericUpDownDashChunkSize.Value = config.DashManualFragmentationChunkSize;
				if (Is64BitProcess)
				{
					checkBoxUseRamForTempFiles.Checked = config.UseRamToStoreTemporaryFiles;
				}
				else
				{
					config.UseRamToStoreTemporaryFiles = false;
					checkBoxUseRamForTempFiles.Enabled = false;
					panelImageRamIsUnusable.Visible = false;
				}
				if (config.UseExternalRestApiServerToGetDownloadUrls)
				{
					textBoxCipherDecryptionAlgorythm.Enabled = false;
					textBoxYouTubeApiV3Key.Enabled = false;
				}

				checkBoxUseUniversalTime.Checked = config.UseGmtTime;
			};
		}

		private void btnRestoreDefaultUserAgent_Click(object sender, EventArgs e)
		{
			config.UserAgent = Configurator.DEFAULT_USER_AGENT;
			textBoxUserAgent.Text = Configurator.DEFAULT_USER_AGENT;
		}

		private void btnWtfUseRam_Click(object sender, EventArgs e)
		{
			btnWtfUseRam.Enabled = false;
			string msg = "Это позволяет ускорить скачивание, сократив количество обращений к накопителю.";
			MessageBox.Show(msg, "Зачематор зачемок", MessageBoxButtons.OK, MessageBoxIcon.Information);
			btnWtfUseRam.Enabled = true;
		}

		private void btnWtfUserAgent_Click(object sender, EventArgs e)
		{
			const string msg = "\"User-Agent\" это специальный идентификатор, отправляемый серверам ютуба. " +
				"При пустом или неподходящем значении, скачивание может не работать или чаще выдавать ошибки.\n" +
				"Внимание! Этот параметр используется при скачивании видео / аудио! " +
				"При обращении к API ютуба (например, при поиске видео) значение этого параметра может быть другим!";
			MessageBox.Show(msg, "Зачематор зачемок",
				MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void btnWtfAlwaysDownloadAsDash_Click(object sender, EventArgs e)
		{
			btnWtfAlwaysDownloadAsDash.Enabled = false;
			string msg = "Бывают ситуации, когда обычным способом видео " +
				"не качается из-за постоянных разрывов соединения с сервером. " +
				"Эта опция может помочь обойти данную проблему. " +
				"Однако, скорость скачивания может стать намного ниже обычного :'(";
			MessageBox.Show(msg, "Зачематор зачемок",
				MessageBoxButtons.OK, MessageBoxIcon.Information);
			btnWtfAlwaysDownloadAsDash.Enabled = true;
		}

		private void btnWtfExternalRestApiServer_Click(object sender, EventArgs e)
		{
			btnWtfExternalRestApiServer.Enabled = false;

			const string serverSourceCodeUrl = "https://github.com/BlackMightyRavenDark/youtube_rest_api_server_node_js";
			string msg = "Этот сервер можно использовать в особых случаях. " +
				"Например, когда другое API не работает.\n" +
				$"Скачать исходный код сервера (на JavaScript) можно здесь: {serverSourceCodeUrl}\n" +
				"Открыть ссылку в браузере?";
			if (MessageBox.Show(msg, "Зачематор зачемок",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				OpenUrlInBrowser(serverSourceCodeUrl);
			}

			btnWtfExternalRestApiServer.Enabled = true;
		}

		private void textBoxYouTubeApiV3Key_Leave(object sender, EventArgs e)
		{
			config.YouTubeApiV3Key = textBoxYouTubeApiV3Key.Text;
		}

		private void textBoxCipherDecryptionAlgorythm_Leave(object sender, EventArgs e)
		{
			config.CipherDecryptionAlgo = textBoxCipherDecryptionAlgorythm.Text;
		}

		private void textBoxExternalRestApiServerUrl_Leave(object sender, EventArgs e)
		{
			config.ExternalRestApiServerUrl = textBoxExternalRestApiServerUrl.Text;
		}

		private void textBoxUserAgent_Leave(object sender, EventArgs e)
		{
			config.UserAgent = textBoxUserAgent.Text.Trim();
		}

		private void checkBoxUseRamForTempFiles_CheckedChanged(object sender, EventArgs e)
		{
			if (!Is64BitProcess && checkBoxUseRamForTempFiles.Enabled)
			{
				checkBoxUseRamForTempFiles.Enabled = false;
				checkBoxUseRamForTempFiles.Checked = false;
				config.UseRamToStoreTemporaryFiles = false;
				MessageBox.Show("Это должно быть доступно только в 64-битной версии программы!",
					"Низя так делать, Вася ты чо!",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
				return;
			}
			config.UseRamToStoreTemporaryFiles = checkBoxUseRamForTempFiles.Checked;
		}

		private void checkBoxUseUniversalTime_CheckedChanged(object sender, EventArgs e)
		{
			config.UseGmtTime = checkBoxUseUniversalTime.Checked;
			foreach (FrameYouTubeVideo frame in framesVideo)
			{
				frame.UpdateVideoDateTimeIndicator();
			}
		}

		private void checkBoxAlwaysDownloadAsDash_CheckedChanged(object sender, EventArgs e)
		{
			config.AlwaysDownloadAsDash = checkBoxAlwaysDownloadAsDash.Checked;
		}

		private void checkBoxUseExternalRestApiServerToGetBasicVideoInfo_CheckedChanged(object sender, EventArgs e)
		{
			config.UseExternalRestApiServerToGetBasicVideoInfo = checkBoxUseExternalRestApiServerToGetBasicVideoInfo.Checked;
		}

		private void checkBoxUseExternalRestApiServerToGetDownloadUrls_CheckedChanged(object sender, EventArgs e)
		{
			config.UseExternalRestApiServerToGetDownloadUrls = checkBoxUseExternalRestApiServerToGetDownloadUrls.Checked;
			textBoxYouTubeApiV3Key.Enabled = textBoxCipherDecryptionAlgorythm.Enabled = !config.UseExternalRestApiServerToGetDownloadUrls;
		}

		private void checkBoxUseExternalRestApiServerToGetAdultVideos_CheckedChanged(object sender, EventArgs e)
		{
			config.UseExternalRestApiServerToGetAdultVideos = checkBoxUseExternalRestApiServerToGetAdultVideos.Checked;
		}

		private void checkBoxUseAccurateMultithreading_CheckedChanged(object sender, EventArgs e)
		{
			config.AccurateMultithreading = checkBoxUseAccurateMultithreading.Checked;
		}

		private void numericUpDownThreadCountAudio_ValueChanged(object sender, EventArgs e)
		{
			config.ThreadCountAudio = (int)numericUpDownThreadCountAudio.Value;
			if (config.ThreadCountAudio > 20)
			{
				if (config.ThreadCountAudio > 50)
				{
					//toolTip1.SetToolTip(panelWarningAudioThreads, "Опасно! Перегрузка!");
					panelWarningAudioThreads.BackgroundImage = Resources.fire;
				}
				else
				{
					//toolTip1.SetToolTip(panelWarningAudioThreads, "Слишком много потоков! Могут возникнуть проблемы со скачиванием!");
					panelWarningAudioThreads.BackgroundImage = Resources.warning;
				}
				panelWarningAudioThreads.Visible = true;
			}
			else
			{
				panelWarningAudioThreads.Visible = false;
			}
		}

		private void numericUpDownThreadCountVideo_ValueChanged(object sender, EventArgs e)
		{
			config.ThreadCountVideo = (int)numericUpDownThreadCountVideo.Value;
			if (config.ThreadCountVideo > 20)
			{
				if (config.ThreadCountVideo > 50)
				{
					//toolTip1.SetToolTip(panelWarningVideoThreads, "Опасно! Перегрузка!");
					if (config.ThreadCountVideo <= 70)
					{
						panelWarningVideoThreads.BackgroundImage = Resources.fire;
					}
					else if (config.ThreadCountVideo <= 80)
					{
						panelWarningVideoThreads.BackgroundImage = Resources.fear;
					}
					else
					{
						panelWarningVideoThreads.BackgroundImage = Resources.skull;
					}
				}
				else
				{
					//toolTip1.SetToolTip(panelWarningVideoThreads, "Слишком много потоков! Могут возникнуть проблемы со скачиванием!");
					panelWarningVideoThreads.BackgroundImage = Resources.warning;
				}
				panelWarningVideoThreads.Visible = true;
			}
			else
			{
				panelWarningVideoThreads.Visible = false;
			}
		}

		private void numericUpDownGlobalThreadCountLimit_ValueChanged(object sender, EventArgs e)
		{
			config.GlobalThreadCountMaximum = (int)numericUpDownGlobalThreadCountLimit.Value;
			MultiThreadedDownloaderLib.Utils.ConnectionLimit = config.GlobalThreadCountMaximum;
		}

		private void numericUpDownDashChunkDownloadTryCountLimit_ValueChanged(object sender, EventArgs e)
		{
			config.DashDownloadRetryCountMax = (int)numericUpDownDashChunkDownloadTryCountLimit.Value;
		}

		private void numericUpDownDashChunkSize_ValueChanged(object sender, EventArgs e)
		{
			config.DashManualFragmentationChunkSize = (long)numericUpDownDashChunkSize.Value;
			lblActualDashChunkSize.Text = FormatSize(config.DashManualFragmentationChunkSize);
		}

		private void numericUpDownExternalRestApiServerPort_ValueChanged(object sender, EventArgs e)
		{
			config.ExternalRestApiServerPort = (ushort)numericUpDownExternalRestApiServerPort.Value;
		}

		private void numericUpDownConnectionTimeout_ValueChanged(object sender, EventArgs e)
		{
			config.ConnectionTimeout = (int)numericUpDownConnectionTimeout.Value;
		}

		private void numericUpDownConnectionTimeoutExternalRestApiServer_ValueChanged(object sender, EventArgs e)
		{
			config.ConnectionTimeoutExternalRestApiServer = (int)numericUpDownConnectionTimeoutExternalRestApiServer.Value;
		}
	}
}
