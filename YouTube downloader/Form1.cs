using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using YouTubeApiLib;
using YouTube_downloader.Properties;
using static YouTubeApiLib.Utils;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			tabControlMain.SelectedTab = tabPageSearch;

			objectTreeViewFavorites.ChildrenGetter = obj => { return ((FavoriteItem)obj).Children; };
			objectTreeViewFavorites.ParentGetter = obj => { return ((FavoriteItem)obj).Parent; };
			objectTreeViewFavorites.CanExpandGetter = obj => { return ((FavoriteItem)obj).Children.Count > 0; };
			objectTreeViewFavorites.Roots = new List<FavoriteItem>() { new FavoriteItem("Избранное") };
			favoritesRootNode = objectTreeViewFavorites.Roots.Cast<FavoriteItem>().ToArray()[0];
			treeFavorites = objectTreeViewFavorites;

			dateTimePickerSearchAfter.Value = DateTime.Now - TimeSpan.FromDays(30);

			config = new Configurator("config_ytdl.json");
			config.Saving += (s, json) =>
			{
				json["downloadingDirPath"] = config.DownloadingDirPath;
				json["tempDirPath"] = config.TempDirPath;
				json["chunksMergingDirPath"] = config.ChunksMergingDirPath;
				json["userAgent"] = config.UserAgent;
				json["chunkDownloadTryCountMax"] = config.ChunkDownloadRetryCountMax;
				json["chunkDownloadErrorCountMax"] = config.ChunkDownloadErrorCountMax;
				json["cipherDecryptionAlgo"] = config.CipherDecryptionAlgo;
				json["youTubeApiV3Key"] = config.YouTubeApiV3Key;
				json["browserExeFilePath"] = config.BrowserExeFilePath;
				json["ffmpegExeFilePath"] = config.FfmpegExeFilePath;
				json["outputFileNameFormatWithDate"] = config.OutputFileNameFormatWithDate;
				json["outputFileNameFormatWithoutDate"] = config.OutputFileNameFormatWithoutDate;
				json["useGmtTime"] = config.UseGmtTime;
				json["maxSearch"] = config.MaxSearch;
				json["sortFormatsByFileSize"] = config.SortFormatsByFileSize;
				json["sortDashFormatsByBitrate"] = config.SortDashFormatsByBitrate;
				json["moveAudioId140First"] = config.MoveAudioId140First;
				json["downloadFirstAudioTrackAutomatically"] = config.DownloadFirstAudioTrack;
				json["downloadSecondAudioTrackAutomatically"] = config.DownloadSecondAudioTrack;
				json["ifOnlySecondAudioTrackIsBetter"] = config.IfOnlySecondAudioTrackIsBetter;
				json["downloadAllAudioTracksAutomatically"] = config.DownloadAllAudioTracks;
				json["showHlsTracksOnlyForStreams"] = config.ShowHlsTracksOnlyForStreams;
				json["checkUrlsAccessibilityBeforeDownloading"] = config.CheckUrlsAccessibilityBeforeDownloading;
				json["alwaysDownloadAsDash"] = config.AlwaysDownloadAsDash;
				json["dashManualFragmentationChunkSize"] = config.DashManualFragmentationChunkSize;
				json["dashDownloadRetryCountMax"] = config.DashDownloadRetryCountMax;
				json["alwaysUseMkvContainer"] = config.AlwaysUseMkvContainer;
				json["extraDelayAfterContainerWasBuilt"] = config.ExtraDelayAfterContainerWasBuilt;
				json["useRamToStoreTemporaryFiles"] = config.UseRamToStoreTemporaryFiles;
				json["savePreviewImage"] = config.SavePreviewImage;
				json["externalRestApiServerUrl"] = config.ExternalRestApiServerUrl;
				json["externalRestApiServerPort"] = config.ExternalRestApiServerPort;
				json["useExternalRestApiServerToGetBasicVideoInfo"] = config.UseExternalRestApiServerToGetBasicVideoInfo;
				json["useExternalRestApiServerToGetDownloadUrls"] = config.UseExternalRestApiServerToGetDownloadUrls;
				json["useExternalRestApiServerToGetAdultVideos"] = config.UseExternalRestApiServerToGetAdultVideos;
				json["videoTitleFontSize"] = config.VideoTitleFontSize;
				json["menusFontSize"] = config.MenusFontSize;
				json["favoritesListFontSize"] = config.FavoritesListFontSize;
				json["threadCountVideo"] = config.ThreadCountVideo;
				json["threadCountAudio"] = config.ThreadCountAudio;
				json["globalThreadCountMaximum"] = config.GlobalThreadCountMaximum;
				json["accurateMultithreading"] = config.AccurateMultithreading;
				json["connectionTimeout"] = config.ConnectionTimeout;
				json["connectionTimeoutExternalRestApiServer"] = config.ConnectionTimeoutExternalRestApiServer;
			};
			config.Loading += (s, json) =>
			{
				{
					JToken jt = json.Value<JToken>("downloadingDirPath");
					if (jt != null)
					{
						config.DownloadingDirPath = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("tempDirPath");
					if (jt != null)
					{
						config.TempDirPath = jt.Value<string>();
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
					JToken jt = json.Value<JToken>("chunksMergingDirPath");
					if (jt != null)
					{
						config.ChunksMergingDirPath = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("userAgent");
					config.UserAgent = jt != null ? jt.Value<string>()?.Trim() : Configurator.DEFAULT_USER_AGENT;
				}
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
					JToken jt = json.Value<JToken>("browserExeFilePath");
					if (jt != null)
					{
						config.BrowserExeFilePath = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("ffmpegExeFilePath");
					if (jt != null)
					{
						config.FfmpegExeFilePath = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("outputFileNameFormatWithDate");
					if (jt != null)
					{
						config.OutputFileNameFormatWithDate = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("outputFileNameFormatWithoutDate");
					if (jt != null)
					{
						config.OutputFileNameFormatWithoutDate = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("useGmtTime");
					if (jt != null)
					{
						config.UseGmtTime = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("maxSearch");
					if (jt != null)
					{
						config.MaxSearch = ClampValue(jt.Value<int>(), 1, 500);
					}
				}
				{
					JToken jt = json.Value<JToken>("savePreviewImage");
					if (jt != null)
					{
						config.SavePreviewImage = jt.Value<bool>();
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
					JToken jt = json.Value<JToken>("menusFontSize");
					if (jt != null)
					{
						config.MenusFontSize = ClampValue(jt.Value<int>(), 9, 16);
					}
				}
				{
					JToken jt = json.Value<JToken>("favoritesListFontSize");
					if (jt != null)
					{
						config.FavoritesListFontSize = ClampValue(jt.Value<int>(), 8, 16);
					}
				}
				{
					JToken jt = json.Value<JToken>("videoTitleFontSize");
					if (jt != null)
					{
						config.VideoTitleFontSize = ClampValue(jt.Value<int>(), 8, 16);
					}
				}
				{
					JToken jt = json.Value<JToken>("sortFormatsByFileSize");
					if (jt != null)
					{
						config.SortFormatsByFileSize = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("sortDashFormatsByBitrate");
					if (jt != null)
					{
						config.SortDashFormatsByBitrate = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("moveAudioId140First");
					if (jt != null)
					{
						config.MoveAudioId140First = jt.Value<bool>();
					}
				}
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
					JToken jt = json.Value<JToken>("showHlsTracksOnlyForStreams");
					if (jt != null)
					{
						config.ShowHlsTracksOnlyForStreams = jt.Value<bool>();
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
					JToken jt = json.Value<JToken>("useRamToStoreTemporaryFiles");
					if (jt != null)
					{
						config.UseRamToStoreTemporaryFiles = jt.Value<bool>();
					}
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
					JToken jt = json.Value<JToken>("connectionTimeoutExternalRestApiServer");
					if (jt != null)
					{
						int min = (int)numericUpDownConnectionTimeoutExternalRestApiServer.Minimum;
						int max = (int)numericUpDownConnectionTimeoutExternalRestApiServer.Maximum;
						config.ConnectionTimeoutExternalRestApiServer = ClampValue(jt.Value<int>(), min, max);
					}
				}
			};
			config.Loaded += (s) =>
			{
				textBoxDownloadDirectory.Text = config.DownloadingDirPath;
				textBoxTempDirectory.Text = config.TempDirPath;
				textBoxFilesMergerDirectory.Text = config.ChunksMergingDirPath;
				textBoxUserAgent.Text = config.UserAgent;
				textBoxCipherDecryptionAlgorythm.Text = config.CipherDecryptionAlgo;
				textBoxYouTubeApiV3Key.Text = config.YouTubeApiV3Key;
				textBoxWebBrowserFilePath.Text = config.BrowserExeFilePath;
				textBoxOutputFileNameFormatWithDate.Text = config.OutputFileNameFormatWithDate;
				textBoxOutputFileNameFormatWithoutDate.Text = config.OutputFileNameFormatWithoutDate;
				numericUpDownSearchResultCountLimit.Value = config.MaxSearch;
				numericUpDownChunkDownloadTryCountLimit.Value = config.ChunkDownloadRetryCountMax;
				numericUpDownChunkDownloadErrorCountLimit.Value = config.ChunkDownloadErrorCountMax;
				textBoxFfmpegFilePath.Text = config.FfmpegExeFilePath;
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
				textBoxExternalRestApiServerUrl.Text = config.ExternalRestApiServerUrl;
				numericUpDownExternalRestApiServerPort.Value = config.ExternalRestApiServerPort;
				checkBoxUseExternalRestApiServerToGetBasicVideoInfo.Checked = config.UseExternalRestApiServerToGetBasicVideoInfo;
				checkBoxUseExternalRestApiServerToGetDownloadUrls.Checked = config.UseExternalRestApiServerToGetDownloadUrls;
				checkBoxUseExternalRestApiServerToGetAdultVideos.Checked = config.UseExternalRestApiServerToGetAdultVideos;
				numericUpDownVideoTitleFontSize.Value = config.VideoTitleFontSize;
				numericUpDownMenusFontSize.Value = config.MenusFontSize;
				numericUpDownFavoritesListFontSize.Value = config.FavoritesListFontSize;
				checkBoxSortAdaptiveFormatsByFileSize.Checked = config.SortFormatsByFileSize;
				checkBoxSortDashFormatsByBitrate.Checked = config.SortDashFormatsByBitrate;
				checkBoxMoveAudioTrackId140ToTopOfList.Checked = config.MoveAudioId140First;
				checkBoxAutomaticallyDownloadFirstAudioTrack.Checked = config.DownloadFirstAudioTrack;
				checkBoxAutomaticallyDownloadSecondAudioTrack.Checked = config.DownloadSecondAudioTrack;
				checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Checked = config.IfOnlySecondAudioTrackIsBetter;
				checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.Checked = config.DownloadAllAudioTracks;
				checkBoxShowHlsTracksOnlyForStreams.Checked = config.ShowHlsTracksOnlyForStreams;
				checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Checked = config.CheckUrlsAccessibilityBeforeDownloading;
				numericUpDownThreadCountVideo.Value = config.ThreadCountVideo;
				numericUpDownThreadCountAudio.Value = config.ThreadCountAudio;
				numericUpDownGlobalThreadCountLimit.Value = config.GlobalThreadCountMaximum;
				numericUpDownConnectionTimeout.Value = config.ConnectionTimeout;
				numericUpDownConnectionTimeoutExternalRestApiServer.Value = config.ConnectionTimeoutExternalRestApiServer;
				checkBoxUseAccurateMultithreading.Checked = config.AccurateMultithreading;
				checkBoxAlwaysDownloadAsDash.Checked = config.AlwaysDownloadAsDash;
				numericUpDownDashChunkDownloadTryCountLimit.Value = config.DashDownloadRetryCountMax;
				numericUpDownDashChunkSize.Value = config.DashManualFragmentationChunkSize;
				if (Is64BitProcess)
				{
					checkBoxUseRamForTempFiles.Checked = config.UseRamToStoreTemporaryFiles;
					btnWtfUseRam.Left = panelRAM.Left;
				}
				else
				{
					config.UseRamToStoreTemporaryFiles = false;
					checkBoxUseRamForTempFiles.Enabled = false;
					panelRAM.Visible = true;
				}

				if (!config.DownloadAllAudioTracks)
				{
					checkBoxAutomaticallyDownloadFirstAudioTrack_CheckedChanged(null, null);
					checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Enabled = config.DownloadFirstAudioTrack && config.DownloadSecondAudioTrack;
				}

				if (config.UseExternalRestApiServerToGetDownloadUrls)
				{
					textBoxCipherDecryptionAlgorythm.Enabled = false;
					textBoxYouTubeApiV3Key.Enabled = false;
				}

				checkBoxUseUniversalTime.Checked = config.UseGmtTime;

				MultiThreadedDownloaderLib.Utils.ConnectionLimit = config.GlobalThreadCountMaximum;
			};
			config.Load();

			try
			{
				if (File.Exists(config.FavoritesFilePath))
				{
					isFavoritesLoaded = LoadFavorites(config.FavoritesFilePath);
					if (isFavoritesLoaded)
					{
						objectTreeViewFavorites.Expand(favoritesRootNode);
					}
				}
				else
				{
					isFavoritesLoaded = true;
				}
			} catch (Exception ex)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
				objectTreeViewFavorites.Enabled = false;
				string msg = $"Ошибка загрузки избранного! Список избранного недоступен до перезапуска программы!\n{ex.Message}";
				MessageBox.Show(msg, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			ClearChannelInfos();
			ClearFramesChannel();
			ClearVideos();
			ClearFramesVideo();

			if (e.CloseReason != CloseReason.ApplicationExitCall)
			{
				try
				{
					if (!Directory.Exists(config.HomeDirPath))
					{
						Directory.CreateDirectory(config.HomeDirPath);
					}
					if (Directory.Exists(config.HomeDirPath))
					{
						config.Save();
						if (isFavoritesLoaded && isFavoritesChanged)
						{
							SaveFavorites(config.FavoritesFilePath);
						}
					}
				}
#if DEBUG
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
				}
#else
				catch { }
#endif
			}
		}

		private void panelSearchResults_Resize(object sender, EventArgs e)
		{
			StackFrames();
		}

		private void objectTreeViewFavorites_ItemsRemoving(object sender, BrightIdeasSoftware.ItemsRemovingEventArgs e)
		{
			List<FavoriteItem> items = e.ObjectsToRemove.Cast<FavoriteItem>().ToList();
			for (int iItem = items.Count - 1; iItem >= 0; --iItem)
			{
				for (int iChild = items[iItem].Children.Count - 1; iChild >= 0; --iChild)
				{
					objectTreeViewFavorites.RemoveObject(items[iItem].Children[iChild]);
					items[iItem].Children.RemoveAt(iChild);
				}
				items.RemoveAt(iItem);
			}
		}

		private void objectTreeViewFavorites_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
		{
			FavoriteItem item = (FavoriteItem)objectTreeViewFavorites.SelectedObject;
			if (item != null && item.Parent != null)
			{
				contextMenuFavorites.Show(Cursor.Position);
			}
		}

		private async void objectTreeViewFavorites_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && objectTreeViewFavorites.SelectedObject != null)
			{
				FavoriteItem item = (FavoriteItem)objectTreeViewFavorites.SelectedObject;
				if (item == null || item.Parent == null)
				{
					return;
				}

				if (item.ItemType != FavoriteItemType.Directory)
				{
					DisableControls();

					if (item.ItemType == FavoriteItemType.Video)
					{
						if (MessageBox.Show($"Перейти к видео {item.DisplayName}?", "Переход к видео",
							MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							ClearChannelInfos();
							ClearFramesChannel();
							ClearVideos();
							ClearFramesVideo();

							tabPageSearchResults.Text = "Результаты поиска";
							scrollBarSearchResults.Value = 0;

							YouTubeVideoId youTubeVideo = new YouTubeVideoId(item.ID);
							await FindVideoById(youTubeVideo);
						}
					}
					else if (item.ItemType == FavoriteItemType.Channel)
					{
						if (MessageBox.Show($"Перейти на канал {item.DisplayName}?", "Переход на канал",
							MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							if (string.IsNullOrEmpty(config.YouTubeApiV3Key) || string.IsNullOrWhiteSpace(config.YouTubeApiV3Key))
							{
								MessageBox.Show("Для использования этой функции, необходимо ввести ключ от API ютуба!",
									"Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
								EnableControls();
								return;
							}

							if (checkBoxSearchRangePublishedAfter.Checked && checkBoxSearchRangePublishedBefore.Checked &&
								dateTimePickerSearchAfter.Value >= dateTimePickerSearchBefore.Value)
							{
								MessageBox.Show("Указан неверный диапазон дат!", "Ошибка!",
									MessageBoxButtons.OK, MessageBoxIcon.Error);
								EnableControls();
								return;
							}

							YouTubeChannel channel = new YouTubeChannel(item.ID, item.Title);
							OpenChannel(channel);
						}
					}

					EnableControls();
				}
			}
		}

		private void btnBrowseDownloadDirectory_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "Выберите папку для скачивания";
			folderBrowserDialog.SelectedPath =
				(!string.IsNullOrEmpty(config.DownloadingDirPath) && Directory.Exists(config.DownloadingDirPath)) ?
				config.DownloadingDirPath : config.SelfDirPath;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				config.DownloadingDirPath =
					folderBrowserDialog.SelectedPath.EndsWith("\\")
					? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
				textBoxDownloadDirectory.Text = config.DownloadingDirPath;
			}
			folderBrowserDialog.Dispose();
		}

		private void btnBrowseTempDirectory_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "Выберите папку для временных файлов";
			folderBrowserDialog.SelectedPath =
				(!string.IsNullOrEmpty(config.TempDirPath) && Directory.Exists(config.TempDirPath)) ?
				config.TempDirPath : config.SelfDirPath;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				config.TempDirPath =
					folderBrowserDialog.SelectedPath.EndsWith("\\")
					? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
				textBoxTempDirectory.Text = config.TempDirPath;
			}
			folderBrowserDialog.Dispose();
		}

		private void btnSelectMergerDirectory_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.Description = "Выберите папку для объединения чанков";
			folderBrowserDialog.SelectedPath =
				(!string.IsNullOrEmpty(config.ChunksMergingDirPath) && Directory.Exists(config.ChunksMergingDirPath)) ?
				config.ChunksMergingDirPath : config.SelfDirPath;
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				config.ChunksMergingDirPath =
					folderBrowserDialog.SelectedPath.EndsWith("\\")
					? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
				textBoxFilesMergerDirectory.Text = config.ChunksMergingDirPath;
			}
			folderBrowserDialog.Dispose();
		}

		private void btnSelectWebBrowserFilePath_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Выберите EXE-файл браузера";
			ofd.Filter = "EXE-файлы|*.exe";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				config.BrowserExeFilePath = ofd.FileName;
				textBoxWebBrowserFilePath.Text = config.BrowserExeFilePath;
			}
			ofd.Dispose();
		}

		private void btnBrowseFfmpegFilePath_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Выберите EXE-файл FFMPEG";
			ofd.Filter = "EXE-файлы|*.exe";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				config.FfmpegExeFilePath = ofd.FileName;
				textBoxFfmpegFilePath.Text = config.FfmpegExeFilePath;
			}
			ofd.Dispose();
		}

		private void btnResetFileNameFormatWithDate_Click(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithDate = FILENAME_FORMAT_DEFAULT_WITH_DATE;
			textBoxOutputFileNameFormatWithDate.Text = FILENAME_FORMAT_DEFAULT_WITH_DATE;
		}

		private void btnResetFileNameFormatWithoutDate_Click(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithoutDate = FILENAME_FORMAT_DEFAULT_WITHOUT_DATE;
			textBoxOutputFileNameFormatWithoutDate.Text = FILENAME_FORMAT_DEFAULT_WITHOUT_DATE;
		}

		private void btnRestoreDefaultUserAgent_Click(object sender, EventArgs e)
		{
			config.UserAgent = Configurator.DEFAULT_USER_AGENT;
			textBoxUserAgent.Text = Configurator.DEFAULT_USER_AGENT;
		}

		private void btnWtfWebPageCode_Click(object sender, EventArgs e)
		{
			DisableControls();

			string msg = "Это позволит скачать скрытое, заблокированное или 18+ видео, " +
				"если у вас есть к нему доступ из браузера.\nНо это не точно!";
			MessageBox.Show(msg, "Зачематор зачемок", MessageBoxButtons.OK, MessageBoxIcon.Information);

			EnableControls();
		}

		private void btnWtfMergerDirectory_Click(object sender, EventArgs e)
		{
			string msg = "Для достижения максимальной производительности и уменьшения нагрузки на накопители, " +
				"\"Папка для временных файлов\" и \"Папка для объединения чанков\" должны находиться " +
				"на разных физических дисках. А файл назначения не должен находиться на одном физическом диске с \"Папкой для объединения чанков\".\n" +
				"Если оставить это поле пустым, то \"Папка для объединения чанков\" будет равна \"Папке для временных файлов\".";
			MessageBox.Show(msg, "Зачематор зачемок", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void btnWtfUseRam_Click(object sender, EventArgs e)
		{
			btnWtfUseRam.Enabled = false;
			string msg = "Это позволяет ускорить скачивание, сократив количество обращений к накопителю.";
			MessageBox.Show(msg, "Зачематор зачемок", MessageBoxButtons.OK, MessageBoxIcon.Information);
			btnWtfUseRam.Enabled = true;
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

		private async void btnSearchByQuery_Click(object sender, EventArgs e)
		{
			DisableControls();

			if (string.IsNullOrEmpty(textBoxSearchQuery.Text) || string.IsNullOrWhiteSpace(textBoxSearchQuery.Text))
			{
				MessageBox.Show("Введите поисковый запрос!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls();
				textBoxSearchQuery.Focus();
				return;
			}
			if (string.IsNullOrEmpty(config.YouTubeApiV3Key))
			{
				MessageBox.Show("Для использования этой функции, необходимо ввести ключ от API ютуба!",
					"Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
				EnableControls();
				return;
			}
			if (checkBoxSearchRangePublishedAfter.Checked && checkBoxSearchRangePublishedBefore.Checked &&
				dateTimePickerSearchAfter.Value >= dateTimePickerSearchBefore.Value)
			{
				MessageBox.Show("Указан неверный диапазон дат!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls();
				return;
			}

			ClearChannelInfos();
			ClearFramesChannel();
			ClearVideos();
			ClearFramesVideo();

			tabPageSearchResults.Text = "Результаты поиска";
			scrollBarSearchResults.Value = 0;

			ushort maxResultsCount = (ushort)(radioButtonSearchResultCountLimitUserDefinedNumber.Checked ? numericUpDownSearchResultCountLimit.Value : 500);

			IYouTubeSearcher searcher = new YouTubeQuerySearcher(
				textBoxSearchQuery.Text, maxResultsCount, dateTimePickerSearchAfter.Value, dateTimePickerSearchBefore.Value,
				checkBoxSearchVideos.Checked, checkBoxSearchChannels.Checked, config.YouTubeApiV3Key);
			JObject json = await Task.Run(() => (JObject)searcher.Search());
			if (json == null)
			{
				tabPageSearchResults.Text = "Результаты поиска: 0";
				MessageBox.Show("Ничего не найдено!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			int count = await ParseList(json);
			if (count > 0) { StackFrames(); }
			tabControlMain.SelectedTab = tabPageSearchResults;
			tabPageSearchResults.Text = $"Результаты поиска: {count}";

			EnableControls();
		}

		private async void btnSearchByVideoUrlOrId_Click(object sender, EventArgs e)
		{
			DisableControls();

			string url = textBoxVideoUrlOrId.Text;
			if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
			{
				MessageBox.Show("Не введена ссылка!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls();
				return;
			}

			YouTubeVideoId videoId = ExtractVideoIdFromUrl(url);
			if (videoId == null || string.IsNullOrEmpty(videoId.Id) || string.IsNullOrWhiteSpace(videoId.Id))
			{
				MessageBox.Show("Не удалось распознать ID видео!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls();
				return;
			}

			if (videoId.Id.Length != 11)
			{
				MessageBox.Show("Введённый вами или автоматически определённый ID видео " +
					$"имеет длину {videoId.Id.Length} символов. Такого не может быть!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls();
				return;
			}

			ClearChannelInfos();
			ClearFramesChannel();
			ClearVideos();
			ClearFramesVideo();

			tabPageSearchResults.Text = "Результаты поиска";
			scrollBarSearchResults.Value = 0;

			await FindVideoById(videoId);

			EnableControls();
		}

		private void btnSearchByWebPage_Click(object sender, EventArgs e)
		{
			DisableControls();

			string webPageCode = richTextBoxWebPageCode.Text;
			if (string.IsNullOrEmpty(webPageCode) || string.IsNullOrWhiteSpace(webPageCode))
			{
				MessageBox.Show("Вставьте код веб-страницы с видео!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls();
				return;
			}

			ClearFramesChannel();
			ClearFramesVideo();
			ClearChannelInfos();
			ClearVideos();

			tabPageSearchResults.Text = "Результаты поиска";
			scrollBarSearchResults.Value = 0;

			try
			{
				YouTubeVideo video = YouTubeVideo.GetByWebPage(webPageCode);
				if (video != null)
				{
					YouTubeVideoWebPageResult webPageResult = YouTubeVideoWebPage.FromCode(webPageCode);
					CreateAndAddNewFrame(video, webPageResult.VideoWebPage);
					StackFrames();

					tabPageSearchResults.Text = "Результаты поиска: 1";
					tabControlMain.SelectedTab = tabPageSearchResults;
					textBoxVideoUrlOrId.Text = null;
					richTextBoxWebPageCode.Text = null;
				}
				else
				{
					MessageBox.Show("Ошибка поиска видео!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			EnableControls();
		}

		private void textBoxCipherDecryptionAlgorythm_Leave(object sender, EventArgs e)
		{
			config.CipherDecryptionAlgo = textBoxCipherDecryptionAlgorythm.Text;
		}

		private void textBoxDownloadDirectory_Leave(object sender, EventArgs e)
		{
			config.DownloadingDirPath = textBoxDownloadDirectory.Text;
		}

		private void textBoxTempDirectory_Leave(object sender, EventArgs e)
		{
			config.TempDirPath = textBoxTempDirectory.Text;
		}

		private void textBoxFilesMergerDirectory_Leave(object sender, EventArgs e)
		{
			config.ChunksMergingDirPath = textBoxFilesMergerDirectory.Text;
		}

		private void textBoxYouTubeApiV3Key_Leave(object sender, EventArgs e)
		{
			config.YouTubeApiV3Key = textBoxYouTubeApiV3Key.Text;
		}

		private void textBoxUserAgent_Leave(object sender, EventArgs e)
		{
			config.UserAgent = textBoxUserAgent.Text.Trim();
		}

		private void textBoxOutputFileNameFormatWithDate_TextChanged(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithDate = textBoxOutputFileNameFormatWithDate.Text;
		}

		private void textBoxOutputFileNameFormatWithoutDate_TextChanged(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithoutDate = textBoxOutputFileNameFormatWithoutDate.Text;
		}

		private void textBoxFfmpegFilePath_Leave(object sender, EventArgs e)
		{
			config.FfmpegExeFilePath = textBoxFfmpegFilePath.Text;
		}

		private void checkBoxSortAdaptiveFormatsByFileSize_CheckedChanged(object sender, EventArgs e)
		{
			config.SortFormatsByFileSize = checkBoxSortAdaptiveFormatsByFileSize.Checked;
		}

		private void checkBoxMoveAudioTrackId140ToTopOfList_CheckedChanged(object sender, EventArgs e)
		{
			config.MoveAudioId140First = checkBoxMoveAudioTrackId140ToTopOfList.Checked;
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

		private void checkBoxUseAccurateMultithreading_CheckedChanged(object sender, EventArgs e)
		{
			config.AccurateMultithreading = checkBoxUseAccurateMultithreading.Checked;
		}

		private void checkBoxShowHlsTracksOnlyForStreams_CheckedChanged(object sender, EventArgs e)
		{
			config.ShowHlsTracksOnlyForStreams = checkBoxShowHlsTracksOnlyForStreams.Checked;
		}

		private void checkBoxCheckUrlsAccessibilityBeforeDownloadStarted_CheckedChanged(object sender, EventArgs e)
		{
			config.CheckUrlsAccessibilityBeforeDownloading = checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Checked;
		}

		private void checkBoxSearchVideos_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkBoxSearchVideos.Checked && !checkBoxSearchChannels.Checked)
			{
				checkBoxSearchChannels.Checked = true;
			}
		}

		private void checkBoxSearchChannels_CheckedChanged(object sender, EventArgs e)
		{
			if (!checkBoxSearchChannels.Checked && !checkBoxSearchVideos.Checked)
			{
				checkBoxSearchVideos.Checked = true;
			}
		}

		private void checkBoxDeleteSourceFilesWhenMerged_CheckedChanged(object sender, EventArgs e)
		{
			config.DeleteSourceFiles = checkBoxDeleteSourceFilesWhenMerged.Checked;
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

		private void checkBoxSortDashFormatsByBitrate_CheckedChanged(object sender, EventArgs e)
		{
			config.SortDashFormatsByBitrate = checkBoxSortDashFormatsByBitrate.Checked;
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

		private void numericUpDownDelayAfterContainerCreated_ValueChanged(object sender, EventArgs e)
		{
			config.ExtraDelayAfterContainerWasBuilt = (int)numericUpDownDelayAfterContainerCreated.Value;
		}

		private void numericUpDownSearchResultCountLimit_ValueChanged(object sender, EventArgs e)
		{
			config.MaxSearch = (int)numericUpDownSearchResultCountLimit.Value;
		}

		private void numericUpDownChunkDownloadTryCountLimit_ValueChanged(object sender, EventArgs e)
		{
			config.ChunkDownloadRetryCountMax = (int)numericUpDownChunkDownloadTryCountLimit.Value;
		}

		private void numericUpDownThreadCountAudio_ValueChanged(object sender, EventArgs e)
		{
			config.ThreadCountAudio = (int)numericUpDownThreadCountAudio.Value;
			if (config.ThreadCountAudio > 20)
			{
				if (config.ThreadCountAudio > 50)
				{
					toolTip1.SetToolTip(panelWarningAudioThreads, "Опасно! Перегрузка!");
					panelWarningAudioThreads.BackgroundImage = Resources.fire;
				}
				else
				{
					toolTip1.SetToolTip(panelWarningAudioThreads, "Слишком много потоков! Могут возникнуть проблемы со скачиванием!");
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
					toolTip1.SetToolTip(panelWarningVideoThreads, "Опасно! Перегрузка!");
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
					toolTip1.SetToolTip(panelWarningVideoThreads, "Слишком много потоков! Могут возникнуть проблемы со скачиванием!");
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

		private void numericUpDownVideoTitleFontSize_ValueChanged(object sender, EventArgs e)
		{
			config.VideoTitleFontSize = (int)numericUpDownVideoTitleFontSize.Value;
			foreach (FrameYouTubeVideo frameYouTubeVideo in framesVideo)
			{
				frameYouTubeVideo.SetVideoTitleFontSize(config.VideoTitleFontSize);
			}
		}

		private void numericUpDownMenusFontSize_ValueChanged(object sender, EventArgs e)
		{
			config.MenusFontSize = (int)numericUpDownMenusFontSize.Value;
			SetMenusFontSize(config.MenusFontSize);
			foreach (FrameYouTubeVideo frameYouTubeVideo in framesVideo)
			{
				frameYouTubeVideo.SetMenusFontSize(config.MenusFontSize);
			}
		}

		private void numericUpDownFavoritesListFontSize_ValueChanged(object sender, EventArgs e)
		{
			config.FavoritesListFontSize = (int)numericUpDownFavoritesListFontSize.Value;
			objectTreeViewFavorites.Font = new Font(objectTreeViewFavorites.Font.FontFamily, config.FavoritesListFontSize);
		}

		private void numericUpDownChunkDownloadErrorCount_ValueChanged(object sender, EventArgs e)
		{
			config.ChunkDownloadErrorCountMax = (int)numericUpDownChunkDownloadErrorCountLimit.Value;
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

		private void radioButtonSearchResultCountLimitMaxPossible_Click(object sender, EventArgs e)
		{
			radioButtonSearchResultCountLimitUserDefinedNumber.Checked = false;
			radioButtonSearchResultCountLimitMaxPossible.Checked = true;
		}

		private void radioButtonSearchResultCountLimitUserDefinedNumber_Click(object sender, EventArgs e)
		{
			radioButtonSearchResultCountLimitMaxPossible.Checked = false;
			radioButtonSearchResultCountLimitUserDefinedNumber.Checked = true;
		}

		private void radioButtonContainerTypeMp4_CheckedChanged(object sender, EventArgs e)
		{
			config.AlwaysUseMkvContainer = !radioButtonContainerTypeMp4.Checked;
		}

		private void radioButtonContainerTypeMkv_CheckedChanged(object sender, EventArgs e)
		{
			config.AlwaysUseMkvContainer = radioButtonContainerTypeMkv.Checked;
		}

		private void contextMenuCopyPaste_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (richTextBoxWebPageCode.TextLength > 0)
			{
				miCutTextToolStripMenuItem.Enabled = !string.IsNullOrEmpty(richTextBoxWebPageCode.SelectedText);
				miCopyTextToolStripMenuItem.Enabled = true;
				miSelectAllTextToolStripMenuItem.Enabled = true;
			}
			else
			{
				miCutTextToolStripMenuItem.Enabled = false;
				miCopyTextToolStripMenuItem.Enabled = false;
				miSelectAllTextToolStripMenuItem.Enabled = false;
			}
		}

		private void contextMenuFavorites_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			FavoriteItem item = (FavoriteItem)objectTreeViewFavorites.SelectedObject;
			if (item != null)
			{
				switch (item.ItemType)
				{
					case FavoriteItemType.Video:
						miOpenVideoInBrowserToolStripMenuItem.Visible = true;
						miCopyVideoUrlToolStripMenuItem.Visible = true;
						miOpenChannelInBrowserToolStripMenuItem.Visible = false;
						miCopyChannelUrlToolStripMenuItem.Visible = true;
						miCopyChannelIdToolStripMenuItem.Visible = true;
						miCopyVideoIdToolStripMenuItem.Visible = true;
						miCopyDisplayNameWithIdToolStripMenuItem.Visible = true;
						break;

					case FavoriteItemType.Channel:
						miOpenVideoInBrowserToolStripMenuItem.Visible = false;
						miOpenChannelInBrowserToolStripMenuItem.Visible = true;
						miCopyVideoUrlToolStripMenuItem.Visible = false;
						miCopyChannelUrlToolStripMenuItem.Visible = true;
						miCopyChannelIdToolStripMenuItem.Visible = true;
						miCopyVideoIdToolStripMenuItem.Visible = false;
						miCopyDisplayNameWithIdToolStripMenuItem.Visible = true;
						break;

					case FavoriteItemType.Directory:
						miOpenVideoInBrowserToolStripMenuItem.Visible = false;
						miCopyVideoUrlToolStripMenuItem.Visible = false;
						miOpenChannelInBrowserToolStripMenuItem.Visible = false;
						miCopyChannelUrlToolStripMenuItem.Visible = false;
						miCopyChannelIdToolStripMenuItem.Visible = false;
						miCopyVideoIdToolStripMenuItem.Visible = false;
						miCopyDisplayNameWithIdToolStripMenuItem.Visible = false;
						break;
				}
			}
		}

		private void miCopyDisplayNameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)objectTreeViewFavorites.SelectedObject;
			if (item != null)
			{
				SetClipboardText(item.DisplayName);
			}
		}

		private void miCopyDisplayNameWithIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)objectTreeViewFavorites.SelectedObject;
			if (item != null && item.ItemType != FavoriteItemType.Directory)
			{
				SetClipboardText($"{item.DisplayName} [{item.ID}]");
			}
		}

		private void miCopyVideoUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)objectTreeViewFavorites.SelectedObject;
			if (item != null && item.ItemType == FavoriteItemType.Video)
			{
				string url = $"{YOUTUBE_WATCH_URL_BASE}?v={item.ID}";
				SetClipboardText(url);
			}
		}

		private void miCopyChannelUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)objectTreeViewFavorites.SelectedObject;
			if (item != null && item.ItemType != FavoriteItemType.Directory)
			{
				string id = item.ItemType == FavoriteItemType.Video ? item.ChannelId : item.ID;
				string url = string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, id);
				SetClipboardText(url);
			}
		}

		private void miCopyVideoIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)objectTreeViewFavorites.SelectedObject;
			if (item != null && item.ItemType == FavoriteItemType.Video)
			{
				SetClipboardText(item.ID);
			}
		}

		private void miCopyChannelIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)objectTreeViewFavorites.SelectedObject;
			if (item != null && item.ItemType != FavoriteItemType.Directory)
			{
				string id = item.ItemType == FavoriteItemType.Video ? item.ChannelId : item.ID;
				SetClipboardText(id);
			}
		}

		private void miOpenVideoInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)objectTreeViewFavorites.SelectedObject;
			if (item != null)
			{
				string url = $"{YOUTUBE_WATCH_URL_BASE}?v={item.ID}";
				OpenUrlInBrowser(url);
			}
		}

		private void miOpenChannelInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)objectTreeViewFavorites.SelectedObject;
			if (item != null)
			{
				OpenUrlInBrowser(string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, item.ID));
			}
		}

		private void miCutTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string t = richTextBoxWebPageCode.SelectedText;
			if (!string.IsNullOrEmpty(t))
			{
				richTextBoxWebPageCode.Cut();
			}
		}

		private void miCopyTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richTextBoxWebPageCode.Copy();
		}

		private void miPasteTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsText())
			{
				richTextBoxWebPageCode.Paste();
			}
		}

		private void miSelectAllTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richTextBoxWebPageCode.SelectAll();
		}

		private void SaveNode(FavoriteItem root, JArray jsonArr)
		{
			JObject json = new JObject();
			json["displayName"] = root.DisplayName;
			if (root.Children.Count > 0) //directory
			{
				JArray ja = new JArray();
				for (int i = 0; i < root.Children.Count; ++i)
				{
					SaveNode(root.Children[i], ja);
				}
				json["type"] = "directory";
				json["subItems"] = ja;
			}
			else
			{
				json["title"] = root.Title;
				json["type"] = root.ItemType == FavoriteItemType.Video ? "video" : "channel";
				json["id"] = root.ID;
				if (root.ItemType == FavoriteItemType.Video)
				{
					if (!string.IsNullOrEmpty(root.ChannelTitle))
					{
						json["channelTitle"] = root.ChannelTitle;
					}
					if (!string.IsNullOrEmpty(root.ChannelId))
					{
						json["channelId"] = root.ChannelId;
					}
				}
			}
			jsonArr.Add(json);
		}

		private void SaveFavorites(string fileName)
		{
			JArray ja = new JArray();
			for (int i = 0; i < favoritesRootNode.Children.Count; ++i)
			{
				SaveNode(favoritesRootNode.Children[i], ja);
			}
			JObject json = new JObject();
			json["items"] = ja;
			File.WriteAllText(fileName, json.ToString());
		}

		private void ParseDataItem(JObject item, FavoriteItem root)
		{
			string displayName = item.Value<string>("displayName");
			JToken jt = item.Value<JToken>("title");
			string title = jt != null ? jt.Value<string>() : displayName;
			FavoriteItem favoriteItem = new FavoriteItem(title, displayName, null, null, null, root);
			JArray ja = item.Value<JArray>("subItems");
			if (ja != null)
			{
				if (ja.Count > 0)
				{
					favoriteItem.ItemType = FavoriteItemType.Directory;
					for (int i = 0; i < ja.Count; ++i)
					{
						JObject j = TryParseJson(ja[i].Value<JObject>().ToString());
						ParseDataItem(j, favoriteItem);
					}
				}
			}
			else
			{
				jt = item.Value<JToken>("type");
				if (jt == null)
				{
					throw new ArgumentNullException("type = NULL");
				}
				string t = jt.Value<string>().ToLower();
				if (t.Equals("video"))
				{
					favoriteItem.ItemType = FavoriteItemType.Video;
				}
				else if (t.Equals("channel"))
				{
					favoriteItem.ItemType = FavoriteItemType.Channel;
				}
				else
				{
					throw new ArgumentException("Недопустимое значение DataType: " + t);
				}
				favoriteItem.ID = item.Value<string>("id");
				if (favoriteItem.ItemType == FavoriteItemType.Video)
				{
					jt = item.Value<JToken>("channelTitle");
					if (jt != null)
					{
						favoriteItem.ChannelTitle = jt.Value<string>();
					}
					jt = item.Value<JToken>("channelId");
					if (jt != null)
					{
						favoriteItem.ChannelId = jt.Value<string>();
					}
				}
			}
			root.Children.Add(favoriteItem);
		}

		private bool LoadFavorites(string fileName)
		{
			JObject json = JObject.Parse(File.ReadAllText(fileName));
			JArray jItems = json.Value<JArray>("items");
			for (int i = 0; i < jItems.Count; ++i)
			{
				JObject j = TryParseJson(jItems[i].Value<JObject>().ToString());
				if (j != null)
				{
					ParseDataItem(j, favoritesRootNode);
				}
				else { return false; }
			}

			return true;
		}

		private async Task<int> ParseList(JObject json)
		{
			JArray jaChannels = json.Value<JArray>("channels");
			if (jaChannels != null)
			{
				foreach (JObject jChannel in jaChannels.Cast<JObject>())
				{
					YouTubeChannelInfo channelInfo = new YouTubeChannelInfo();
					JObject jSnippet = jChannel.Value<JObject>("snippet");
					if (jSnippet != null)
					{
						channelInfo.Title = jSnippet.Value<string>("title");
						channelInfo.Id = jSnippet.Value<string>("channelId");
						channelInfo.ImageUrl =
							jSnippet.Value<JObject>("thumbnails")?.Value<JObject>("high")?.Value<string>("url");
						if (!string.IsNullOrEmpty(channelInfo.ImageUrl) && !string.IsNullOrWhiteSpace(channelInfo.ImageUrl))
						{
							channelInfo.ImageData = new MemoryStream();
							await Task.Run(() => DownloadData(channelInfo.ImageUrl, channelInfo.ImageData));
						}

						FrameYouTubeChannel frame = new FrameYouTubeChannel();
						frame.Parent = panelSearchResults;
						frame.SetChannelInfo(channelInfo);
						framesChannel.Add(frame);
					}
				}
			}

			IYouTubeClient client = YouTubeApi.GetYouTubeClient("video_info");
			if (client == null) { return framesChannel.Count; }

			JArray jaVideos = json.Value<JArray>("videos");
			if (jaVideos != null)
			{
				foreach (JObject jVideo in jaVideos.Cast<JObject>())
				{
					string id = jVideo.Value<JObject>("id")?.Value<string>("videoId");

					if (!string.IsNullOrEmpty(id))
					{
						YouTubeVideo video = await Task.Run(() =>
						{
							int errorCode = client.GetRawVideoInfo(id, out YouTubeRawVideoInfo rawVideoInfo, out _);
							return errorCode == 200 ? rawVideoInfo.ToVideo() : null;
						});

						if (video != null)
						{
							videos.Add(video);

							FrameYouTubeVideo frameVideo = new FrameYouTubeVideo(video, panelSearchResults);
							frameVideo.SetMenusFontSize(config.MenusFontSize);
							frameVideo.FavoriteChannelChanged += (s, vidId, newState) =>
							{
								for (int j = 0; j < framesVideo.Count; ++j)
								{
									if (framesVideo[j].VideoInfo.OwnerChannelId == vidId)
									{
										framesVideo[j].IsFavoriteChannel = newState;
									}
								}
							};
							frameVideo.Activated += event_FrameActivated;
							frameVideo.OpenChannel += event_OpenChannel;
							framesVideo.Add(frameVideo);
						}
					}
				}
			}

			return framesChannel.Count + framesVideo.Count;
		}

		private async Task<bool> FindVideoById(YouTubeVideoId videoId)
		{
			YouTubeVideo video = await Task.Run(() => GetSingleVideo(videoId, out _));
			if (video != null)
			{
				if (!video.IsSucceed())
				{
					MessageBox.Show("Ошибка поиска видео! Что-то где-то пошло не так!", "Ошибатор ошибок",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				CreateAndAddNewFrame(video);
				StackFrames();

				tabPageSearchResults.Text = $"Результаты поиска: {framesVideo.Count + framesChannel.Count}";
				tabControlMain.SelectedTab = tabPageSearchResults;
				textBoxVideoUrlOrId.Text = null;

				return true;
			}
			else
			{
				MessageBox.Show("Ошибка поиска видео!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return false;
		}

		private void CreateAndAddNewFrame(YouTubeVideo video, YouTubeVideoWebPage webPage = null)
		{
			FrameYouTubeVideo frame = new FrameYouTubeVideo(video, webPage, panelSearchResults);
			frame.SetMenusFontSize(config.MenusFontSize);
			frame.FavoriteChannelChanged += (s, id, newState) =>
			{
				for (int j = 0; j < framesVideo.Count; ++j)
				{
					if (framesVideo[j].VideoInfo.OwnerChannelId == id)
					{
						framesVideo[j].IsFavoriteChannel = newState;
					}
				}
			};
			frame.Activated += event_FrameActivated;
			frame.OpenChannel += event_OpenChannel;
			framesVideo.Add(frame);
		}

		private void ClearChannelInfos()
		{
			foreach (YouTubeChannelInfo channelInfo in channelInfos)
			{
				channelInfo.ImageData?.Dispose();
			}
			channelInfos.Clear();
		}

		private void ClearVideos()
		{
			videos.Clear();
		}

		private void ClearFramesChannel()
		{
			foreach (FrameYouTubeChannel frame in framesChannel)
			{
				frame.Dispose();
			}
			framesChannel.Clear();
		}

		private void ClearFramesVideo()
		{
			foreach (FrameYouTubeVideo frame in framesVideo)
			{
				frame.Dispose();
			}
			framesVideo.Clear();
		}

		private void StackFrames()
		{
			int h = 0;
			for (int i = 0; i < framesChannel.Count(); ++i)
			{
				framesChannel[i].Left = 0;
				framesChannel[i].Top = h - scrollBarSearchResults.Value;
				h += framesChannel[i].Height;
			}
			for (int i = 0; i < framesVideo.Count(); ++i)
			{
				framesVideo[i].Left = 0;
				framesVideo[i].Top = h - scrollBarSearchResults.Value;
				framesVideo[i].Width = panelSearchResults.Width;
				h += framesVideo[i].Height;
			}

			if (h > panelSearchResults.Height)
			{
				if (scrollBarSearchResults.Value >= h)
				{
					scrollBarSearchResults.Value = 0;
				}
				scrollBarSearchResults.Maximum = h;
				if (panelSearchResults.Height >= 0)
				{
					scrollBarSearchResults.LargeChange = panelSearchResults.Height;
				}
				scrollBarSearchResults.SmallChange = 10;
				scrollBarSearchResults.Enabled = true;
			}
			else
			{
				scrollBarSearchResults.Value = 0;
				scrollBarSearchResults.Enabled = false;
			}
		}

		private async void OpenChannel(YouTubeChannel channel)
		{
			ClearChannelInfos();
			ClearFramesChannel();
			ClearVideos();
			ClearFramesVideo();

			tabPageSearchResults.Text = "Результаты поиска";
			scrollBarSearchResults.Value = 0;

			ushort maxResultsCount = (ushort)(radioButtonSearchResultCountLimitUserDefinedNumber.Checked ? numericUpDownSearchResultCountLimit.Value : 500);
			IYouTubeSearcher searcher = new YouTubeChannelSearcher(channel, dateTimePickerSearchAfter.Value, dateTimePickerSearchBefore.Value,
				maxResultsCount, config.YouTubeApiV3Key);
			List<YouTubeVideo> list = await Task.Run(() => (List<YouTubeVideo>)searcher.Search());
			int count = list != null ? list.Count : 0;
			tabPageSearchResults.Text = $"Результаты поиска: {count}";
			if (count > 0)
			{
				foreach (YouTubeVideo video in list)
				{
					CreateAndAddNewFrame(video);
				}
				StackFrames();
				tabControlMain.SelectedTab = tabPageSearchResults;
			}
			else
			{
				MessageBox.Show("Ничего не найдено!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void event_FrameActivated(object sender)
		{
			foreach (FrameYouTubeChannel frame in framesChannel)
			{
				//TODO: Implement this
			}
			foreach (FrameYouTubeVideo frame in framesVideo)
			{
				frame.btnDownload.ForeColor = Color.Black;
			}

			if (sender is FrameYouTubeVideo)
			{
				(sender as FrameYouTubeVideo).btnDownload.ForeColor = Color.Red;
			}
			else if (sender is FrameYouTubeChannel)
			{
				//TODO: Implement this
			}
		}

		private void event_OpenChannel(object sender, string channelName, string channelId)
		{
			if (MessageBox.Show($"Перейти на канал {channelName}?", "Переход на канал",
				MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				if (string.IsNullOrEmpty(config.YouTubeApiV3Key))
				{
					MessageBox.Show("Для использования этой функции, необходимо ввести ключ от API ютуба!",
						"Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				if (checkBoxSearchRangePublishedAfter.Checked && checkBoxSearchRangePublishedBefore.Checked &&
					dateTimePickerSearchAfter.Value >= dateTimePickerSearchBefore.Value)
				{
					MessageBox.Show("Указан неверный диапазон дат!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				YouTubeChannel channel = new YouTubeChannel(channelId, channelName);
				OpenChannel(channel);
			}
		}

		public void SetMenusFontSize(int fontSize)
		{
			contextMenuFavorites.SetFontSize(fontSize);
			contextMenuCopyPaste.SetFontSize(fontSize);
		}

		private void scrollBarSearchResults_Scroll(object sender, ScrollEventArgs e)
		{
			scrollBarSearchResults.Focus();
			StackFrames();
		}

		private void DisableControls()
		{
			btnSearchByWebPage.Enabled = false;
			btnSearchByVideoUrlOrId.Enabled = false;
			btnSearchByQuery.Enabled = false;
			textBoxSearchQuery.Enabled = false;
			textBoxVideoUrlOrId.Enabled = false;
			btnWtfWebPageCode.Enabled = false;
		}

		private void EnableControls()
		{
			btnSearchByWebPage.Enabled = true;
			btnSearchByVideoUrlOrId.Enabled = true;
			btnSearchByQuery.Enabled = true;
			textBoxSearchQuery.Enabled = true;
			textBoxVideoUrlOrId.Enabled = true;
			btnWtfWebPageCode.Enabled = true;
		}

		private void groupBox13_Resize(object sender, EventArgs e)
		{
			btnWtfUseRam.Left = groupBox13.Width - btnWtfUseRam.Width - 4;
		}
    }
}
