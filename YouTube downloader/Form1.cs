using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using MultiThreadedDownloaderLib;
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

			tvFavorites.ChildrenGetter = obj => { return ((FavoriteItem)obj).Children; };
			tvFavorites.ParentGetter = obj => { return ((FavoriteItem)obj).Parent; };
			tvFavorites.CanExpandGetter = obj => { return ((FavoriteItem)obj).Children.Count > 0; };
			tvFavorites.Roots = new List<FavoriteItem>() { new FavoriteItem("Избранное") };
			favoritesRootNode = tvFavorites.Roots.Cast<FavoriteItem>().ToArray()[0];
			treeFavorites = tvFavorites;

			config = new MainConfiguration("config_ytdl.json");
			config.Saving += (s, json) =>
			{
				json["downloadingDirPath"] = config.DownloadingDirPath;
				json["tempDirPath"] = config.TempDirPath;
				json["chunksMergingDirPath"] = config.ChunksMergingDirPath;
				json["downloadRetryCount"] = config.DownloadRetryCount;
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
				json["alwaysDownloadAsDash"] = config.AlwaysDownloadAsDash;
				json["dashManualFragmentationChunkSize"] = config.DashManualFragmentationChunkSize;
				json["dashDownloadRetryCountMax"] = config.DashDownloadRetryCountMax;
				json["alwaysUseMkvContainer"] = config.AlwaysUseMkvContainer;
				json["extraDelayAfterContainerWasBuilt"] = config.ExtraDelayAfterContainerWasBuilt;
				json["useRamToStoreTemporaryFiles"] = config.UseRamToStoreTemporaryFiles;
				json["savePreviewImage"] = config.SavePreviewImage;
				json["useHiddenApiForGettingInfo"] = config.UseHiddenApiForGettingInfo;
				json["useVideoInfoServerForAdultVideos"] = config.UseVideoInfoServerForAdultVideos;
				json["alwaysUseVideoInfoServer"] = config.AlwaysUseVideoInfoServer;
				json["videoInfoServerUrl"] = config.VideoInfoServerUrl;
				json["videoInfoServerPort"] = config.VideoInfoServerPort;
				json["videoTitleFontSize"] = config.VideoTitleFontSize;
				json["menusFontSize"] = config.MenusFontSize;
				json["favoritesListFontSize"] = config.FavoritesListFontSize;
				json["threadCountVideo"] = config.ThreadCountVideo;
				json["threadCountAudio"] = config.ThreadCountAudio;
				json["globalThreadCountMaximum"] = config.GlobalThreadCountMaximum;
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
					JToken jt = json.Value<JToken>("downloadRetryCount");
					if (jt != null)
					{
						config.DownloadRetryCount = jt.Value<int>();
						if (config.DownloadRetryCount < 1)
						{
							config.DownloadRetryCount = 1;
						}
						else if (config.DownloadRetryCount > (int)numericUpDownDownloadRetryCount.Maximum)
						{
							config.DownloadRetryCount = (int)numericUpDownDownloadRetryCount.Maximum;
						}
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
						config.MaxSearch = jt.Value<int>();
						if (config.MaxSearch < 1)
						{
							config.MaxSearch = 1;
						}
						else if (config.MaxSearch > 500)
						{
							config.MaxSearch = 500;
						}
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
					JToken jt = json.Value<JToken>("useApiForGettingInfo");
					if (jt != null)
					{
						config.UseHiddenApiForGettingInfo = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("useVideoInfoServerForAdultVideos");
					if (jt != null)
					{
						config.UseVideoInfoServerForAdultVideos = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("alwaysUseVideoInfoServer");
					if (jt != null)
					{
						config.AlwaysUseVideoInfoServer = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("videoInfoServerUrl");
					if (jt != null)
					{
						config.VideoInfoServerUrl = jt.Value<string>();
					}
				}
				{
					JToken jt = json.Value<JToken>("videoInfoServerPort");
					if (jt != null)
					{
						config.VideoInfoServerPort = jt.Value<int>();
					}
				}
				{
					JToken jt = json.Value<JToken>("menusFontSize");
					if (jt != null)
					{
						config.MenusFontSize = jt.Value<int>();
						if (config.MenusFontSize < 9)
						{
							config.MenusFontSize = 9;
						}
						else if (config.MenusFontSize > 16)
						{
							config.MenusFontSize = 16;
						}
					}
				}
				{
					JToken jt = json.Value<JToken>("favoritesListFontSize");
					if (jt != null)
					{
						config.FavoritesListFontSize = jt.Value<int>();
						if (config.FavoritesListFontSize < 8)
						{
							config.FavoritesListFontSize = 8;
						}
						else if (config.FavoritesListFontSize > 16)
						{
							config.FavoritesListFontSize = 16;
						}
					}
				}
				{
					JToken jt = json.Value<JToken>("videoTitleFontSize");
					if (jt != null)
					{
						config.VideoTitleFontSize = jt.Value<int>();
						if (config.VideoTitleFontSize < 8)
						{
							config.VideoTitleFontSize = 8;
						}
						else if (config.VideoTitleFontSize > 16)
						{
							config.VideoTitleFontSize = 16;
						}
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
						config.ExtraDelayAfterContainerWasBuilt = jt.Value<int>();
						if (config.ExtraDelayAfterContainerWasBuilt < 0)
						{
							config.ExtraDelayAfterContainerWasBuilt = 0;
						} else if (config.ExtraDelayAfterContainerWasBuilt > 1000)
						{
							config.ExtraDelayAfterContainerWasBuilt = 1000;
						}
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
			};
			config.Loaded += (s) =>
			{
				editDownloadingDirPath.Text = config.DownloadingDirPath;
				editTempDirPath.Text = config.TempDirPath;
				editMergingDirPath.Text = config.ChunksMergingDirPath;
				editCipherDecryptionAlgo.Text = config.CipherDecryptionAlgo;
				editYouTubeApiKey.Text = config.YouTubeApiV3Key;
				editBrowser.Text = config.BrowserExeFilePath;
				editOutputFileNameFormatWithDate.Text = config.OutputFileNameFormatWithDate;
				editOutputFileNameFormatWithoutDate.Text = config.OutputFileNameFormatWithoutDate;
				numericUpDownSearchResult.Value = config.MaxSearch;
				numericUpDownDownloadRetryCount.Value = config.DownloadRetryCount;
				editFfmpeg.Text = config.FfmpegExeFilePath;
				chkMergeAdaptive.Checked = config.MergeToContainer;
				chkDeleteSourceFiles.Checked = config.DeleteSourceFiles;
				if (config.AlwaysUseMkvContainer)
				{
					radioButtonContainerTypeMkv.Checked = true;
				}
				else
				{
					radioButtonContainerTypeMp4.Checked = true;
				}
				numericUpDownDelayAfterContainerCreated.Value = config.ExtraDelayAfterContainerWasBuilt;
				chkSaveImage.Checked = config.SavePreviewImage;
				chkUseHiddenApiForGettingInfo.Checked = config.UseHiddenApiForGettingInfo;
				checkBoxUseVideoInfoServerForAdultVideos.Checked = config.UseVideoInfoServerForAdultVideos;
				checkBoxAlwaysUseVideoInfoServer.Checked = config.AlwaysUseVideoInfoServer;
				textBoxVideoInfoServerUrl.Text = config.VideoInfoServerUrl;
				numericUpDownVideoInfoServerPort.Value = config.VideoInfoServerPort;
				numericUpDownVideoTitleFontSize.Value = config.VideoTitleFontSize;
				numericUpDownMenusFontSize.Value = config.MenusFontSize;
				numericUpDownFavoritesListFontSize.Value = config.FavoritesListFontSize;
				chkSortFormatsByFileSize.Checked = config.SortFormatsByFileSize;
				chkSortDashFormatsByBitrate.Checked = config.SortDashFormatsByBitrate;
				chkMoveAudioId140First.Checked = config.MoveAudioId140First;
				chkDownloadFirstAudioTrack.Checked = config.DownloadFirstAudioTrack;
				chkDownloadSecondAudioTrack.Checked = config.DownloadSecondAudioTrack;
				chkIfOnlyBiggerFileSize.Checked = config.IfOnlySecondAudioTrackIsBetter;
				chkDownloadAllAudioTracks.Checked = config.DownloadAllAudioTracks;
				numericUpDownThreadsVideo.Value = config.ThreadCountVideo;
				numericUpDownThreadsAudio.Value = config.ThreadCountAudio;
				numericUpDownGlobalThreadsMaximum.Value = config.GlobalThreadCountMaximum;
				checkBoxAlwaysDownloadAsDash.Checked = config.AlwaysDownloadAsDash;
				numericUpDownDashChunkDownloadRetriesCountMax.Value = config.DashDownloadRetryCountMax;
				numericUpDownDashChunkSize.Value = config.DashManualFragmentationChunkSize;
				if (Is64BitProcess)
				{
					chkUseRamForTempFiles.Checked = config.UseRamToStoreTemporaryFiles;
					btnUseRamWhy.Left = panelRAM.Left;
				}
				else
				{
					config.UseRamToStoreTemporaryFiles = false;
					chkUseRamForTempFiles.Enabled = false;
					panelRAM.Visible = true;
				}

				if (!config.DownloadAllAudioTracks)
				{
					chkDownloadFirstAudioTrack_CheckedChanged(null, null);
					chkIfOnlyBiggerFileSize.Enabled = config.DownloadFirstAudioTrack && config.DownloadSecondAudioTrack;
				}

				if (config.AlwaysUseVideoInfoServer)
				{
					chkUseHiddenApiForGettingInfo.Enabled = false;
					editCipherDecryptionAlgo.Enabled = false;
					editYouTubeApiKey.Enabled = false;
					checkBoxUseVideoInfoServerForAdultVideos.Enabled = false;
				}

				checkBoxUseGmtTime.Checked = config.UseGmtTime;

				MultiThreadedDownloader.SetMaximumConnectionsLimit(config.GlobalThreadCountMaximum);
			};
			config.Load();

			if (File.Exists(config.FavoritesFilePath))
			{
				LoadFavorites(config.FavoritesFilePath);
				tvFavorites.Expand(favoritesRootNode);
			}

			dateTimePickerAfter.Value = DateTime.Now - TimeSpan.FromDays(30);
		}

		private void Form1_FormClosed(object sender, FormClosedEventArgs e)
		{
			ClearChannels();
			ClearFramesChannel();
			ClearVideos();
			ClearFramesVideo();

			if (!Directory.Exists(config.HomeDirPath))
			{
				Directory.CreateDirectory(config.HomeDirPath);
			}
			if (Directory.Exists(config.HomeDirPath))
			{
				config.Save();
				SaveFavorites(config.FavoritesFilePath);
			}
		}

		private void panelSearchResults_Resize(object sender, EventArgs e)
		{
			StackFrames();
		}

		private void chkSortFormatsByFileSize_CheckedChanged(object sender, EventArgs e)
		{
			config.SortFormatsByFileSize = chkSortFormatsByFileSize.Checked;
		}

		private void chkMoveAudioId140First_CheckedChanged(object sender, EventArgs e)
		{
			config.MoveAudioId140First = chkMoveAudioId140First.Checked;
		}

		private void chkDownloadFirstAudioTrack_CheckedChanged(object sender, EventArgs e)
		{
			if (chkDownloadFirstAudioTrack.Checked)
			{
				config.DownloadFirstAudioTrack = true;
				chkDownloadSecondAudioTrack.Enabled = true;
				chkIfOnlyBiggerFileSize.Enabled = chkDownloadSecondAudioTrack.Checked;
			}
			else
			{
				config.DownloadFirstAudioTrack = false;
				chkDownloadSecondAudioTrack.Enabled = false;
				chkIfOnlyBiggerFileSize.Enabled = false;
			}
		}

		private void chkDownloadSecondAudioTrack_CheckedChanged(object sender, EventArgs e)
		{
			config.DownloadSecondAudioTrack = chkDownloadSecondAudioTrack.Checked;
			chkIfOnlyBiggerFileSize.Enabled = config.DownloadSecondAudioTrack;
		}

		private void chkIfOnlyBiggerFileSize_CheckedChanged(object sender, EventArgs e)
		{
			config.IfOnlySecondAudioTrackIsBetter = chkIfOnlyBiggerFileSize.Checked;
		}

		private void chkDownloadAllAudioTracks_CheckedChanged(object sender, EventArgs e)
		{
			if (chkDownloadAllAudioTracks.Checked)
			{
				config.DownloadAllAudioTracks = true;
				chkDownloadFirstAudioTrack.Enabled = false;
				chkDownloadSecondAudioTrack.Enabled = false;
				chkIfOnlyBiggerFileSize.Enabled = false;
			}
			else
			{
				config.DownloadAllAudioTracks = false;
				chkDownloadFirstAudioTrack.Enabled = true;
				chkDownloadSecondAudioTrack.Enabled = true;
				chkIfOnlyBiggerFileSize.Enabled = chkDownloadSecondAudioTrack.Checked;
			}
		}

		private void chkDownloadAllAdaptiveVideoTracks_CheckedChanged(object sender, EventArgs e)
		{
			config.DownloadAllAdaptiveVideoTracks = chkDownloadAllAdaptiveVideoTracks.Checked;
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
						JObject j = JObject.Parse(ja[i].Value<JObject>().ToString());
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

		private void LoadFavorites(string fileName)
		{
			JObject json = JObject.Parse(File.ReadAllText(fileName));
			JArray jItems = json.Value<JArray>("items");
			for (int i = 0; i < jItems.Count; ++i)
			{
				JObject j = JObject.Parse(jItems[i].Value<JObject>().ToString());
				ParseDataItem(j, favoritesRootNode);
			}
		}

		private int GetChannelVideosList(string channelId, int maxVideos, out string resJsonList)
		{
			if (maxVideos <= 0)
			{
				maxVideos = 50;
			}
			else if (maxVideos > 500)
			{
				maxVideos = 500;
			}

			int sum = 0;
			JArray jaVideos = new JArray();
			string pageToken = null;
			int errorCode;
			do
			{
				string url = GetYouTubeChannelVideosRequestUrl(channelId, maxVideos);
				if (chkPublishedAfter.Checked)
				{
					string dateAfter = dateTimePickerAfter.Value.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
					url += $"&publishedAfter={dateAfter}";
				}

				if (chkPublishedBefore.Checked)
				{
					string dateBefore = dateTimePickerBefore.Value.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
					url += $"&publishedBefore={dateBefore}";
				}

				if (!string.IsNullOrEmpty(pageToken))
				{
					url += $"&pageToken={pageToken}";
				}

				errorCode = Utils.DownloadString(url, out string buf);
				if (errorCode == 200)
				{
					JObject json = JObject.Parse(buf);
					JToken jt = json.Value<JToken>("nextPageToken");
					pageToken = jt == null ? null : jt.Value<string>();
					JArray jsonArr = json.Value<JArray>("items");
					if (jsonArr != null && jsonArr.Count > 0)
					{
						for (int i = 0; i < jsonArr.Count; ++i)
						{
							JObject jObject = JObject.Parse(jsonArr[i].Value<JObject>().ToString());
							string videoId = jObject.Value<JObject>("id")?.Value<string>("videoId");
							if (!string.IsNullOrEmpty(videoId) && !string.IsNullOrWhiteSpace(videoId))
							{
								YouTubeRawVideoInfoResult rawVideoInfoResult = YouTubeRawVideoInfo.Get(videoId);
								if (rawVideoInfoResult.ErrorCode == 200)
								{
									jaVideos.Add(rawVideoInfoResult.RawVideoInfo.RawData);
									if (sum++ + 1 >= maxVideos)
									{
										break;
									}
								}
							}

							Application.DoEvents();
						}
					}
				}

				if (sum >= maxVideos)
				{
					break;
				}
				Application.DoEvents();
			} while (errorCode == 200 && sum < maxVideos && !string.IsNullOrEmpty(pageToken));

			JObject jsonRes = new JObject();
			jsonRes["videos"] = jaVideos;
			resJsonList = jsonRes.ToString();
			return jaVideos.Count;
		}

		public int SearchYouTube(string searchingPhrase, int maxResults, out string resList)
		{
			if (chkPublishedAfter.Checked && chkPublishedBefore.Checked &&
				dateTimePickerAfter.Value > dateTimePickerBefore.Value)
			{
				MessageBox.Show("Ошибка диапазона дат!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				resList = null;
				return 0;
			}

			if (maxResults <= 0 || maxResults > 50)
			{
				maxResults = 50;
			}

			JArray jaChannels = new JArray();
			JArray jaVideos = new JArray();

			string resultTypes = "video,channel";
			if (!chkSearchChannels.Checked)
			{
				resultTypes = resultTypes.Replace(",channel", string.Empty);
			}
			if (!chkSearchVideos.Checked)
			{
				resultTypes = resultTypes.Replace("video,", string.Empty);
			}

			string url = GetYouTubeSearchQueryRequestUrl(searchingPhrase, resultTypes, maxResults);
			if (chkPublishedAfter.Checked)
			{
				string dateAfter = dateTimePickerAfter.Value.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
				url += $"&publishedAfter={dateAfter}";
			}

			if (chkPublishedBefore.Checked)
			{
				string dateBefore = dateTimePickerBefore.Value.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
				url += $"&publishedBefore={dateBefore}";
			}

			int errorCode = Utils.DownloadString(url, out string buf);
			if (errorCode == 200)
			{
				JObject json = JObject.Parse(buf);

				JArray jsonArr = json.Value<JArray>("items");

				for (int i = 0; i < jsonArr.Count(); ++i)
				{
					JObject j = JObject.Parse(jsonArr[i].ToString());
					string kind = j.Value<JObject>("id").Value<string>("kind");

					if (kind.Equals("youtube#channel"))
					{
						jaChannels.Add(j);
					}
					else
					{
						if (kind.Equals("youtube#video"))
						{
							string id = j.Value<JObject>("id").Value<string>("videoId");
							YouTubeVideoInfoGettingMethod method = config.UseHiddenApiForGettingInfo ?
								YouTubeVideoInfoGettingMethod.HiddenApiDecryptedUrls :
								YouTubeVideoInfoGettingMethod.WebPage;
							YouTubeRawVideoInfoResult rawVideoInfoResult = YouTubeRawVideoInfo.Get(id, method);
							if (rawVideoInfoResult.ErrorCode == 200)
							{
								jaVideos.Add(rawVideoInfoResult.RawVideoInfo.RawData);
							}
						}
					}
				}
			}

			JObject j3 = new JObject();
			j3.Add(new JProperty("channels", jaChannels));
			j3.Add(new JProperty("videos", jaVideos));
			resList = j3.ToString();
			int count = jaChannels.Count() + jaVideos.Count();
			return count;
		}

		private int ParseList(string jsonString)
		{
			JObject json = JObject.Parse(jsonString);
			JToken jt = json.Value<JToken>("channels");
			if (jt != null)
			{
				JArray ja = jt.Value<JArray>();
				if (ja != null)
				{
					for (int i = 0; i < ja.Count(); ++i)
					{
						YouTubeChannel youTubeChannel = new YouTubeChannel();
						JObject jSnippet = ja[i].Value<JObject>("snippet");
						youTubeChannel.Title = jSnippet.Value<string>("title");
						youTubeChannel.Id = jSnippet.Value<string>("channelId");
						youTubeChannel.ImageUrl =
							jSnippet.Value<JObject>("thumbnails")?.Value<JObject>("high")?.Value<string>("url");
						if (!string.IsNullOrEmpty(youTubeChannel.ImageUrl) && !string.IsNullOrWhiteSpace(youTubeChannel.ImageUrl))
						{
							youTubeChannel.ImageData = new MemoryStream();
							DownloadData(youTubeChannel.ImageUrl, youTubeChannel.ImageData);
						}

						FrameYouTubeChannel frame = new FrameYouTubeChannel();
						frame.Parent = panelSearchResults;
						frame.SetChannelInfo(youTubeChannel);
						framesChannel.Add(frame);
					}
				}
			}

			JArray jaVideos = json.Value<JArray>("videos");
			if (jaVideos != null)
			{
				for (int i = 0; i < jaVideos.Count; ++i)
				{
					//TODO: Fix this shit!
					YouTubeVideoInfoGettingMethod method = config.UseHiddenApiForGettingInfo ?
						YouTubeVideoInfoGettingMethod.HiddenApiDecryptedUrls :
						YouTubeVideoInfoGettingMethod.WebPage;
					string rawInfo = jaVideos[i].Value<string>();
					YouTubeRawVideoInfo rawVideoInfo = new YouTubeRawVideoInfo(rawInfo, method);
					YouTubeVideo video = MakeYouTubeVideo(rawVideoInfo);
					if (video != null)
					{
						videos.Add(video);

						FrameYouTubeVideo frameVideo = new FrameYouTubeVideo(video, panelSearchResults);
						frameVideo.SetMenusFontSize(config.MenusFontSize);
						frameVideo.FavoriteChannelChanged += (s, id, newState) =>
						{
							for (int j = 0; j < framesVideo.Count; ++j)
							{
								if (framesVideo[j].VideoInfo.OwnerChannelId == id)
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
			return framesChannel.Count + framesVideo.Count;
		}

		private void ClearChannels()
		{
			foreach (YouTubeChannel channel in channels)
			{
				channel.ImageData?.Dispose();
			}
			channels.Clear();
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
				framesVideo[i].Width = panelSearchResults.Width + FrameYouTubeVideo.EXTRA_WIDTH;
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

		private void BtnSearchByQuery_Click(object sender, EventArgs e)
		{
			DisableControls();

			if (string.IsNullOrEmpty(editQuery.Text) || string.IsNullOrWhiteSpace(editQuery.Text))
			{
				MessageBox.Show("Введите поисковый запрос!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls();
				editQuery.Focus();
				return;
			}
			if (string.IsNullOrEmpty(config.YouTubeApiV3Key))
			{
				MessageBox.Show("Для использования этой функции, необходимо ввести ключ от API ютуба!",
					"Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
				EnableControls();
				return;
			}
			if (chkPublishedAfter.Checked && chkPublishedBefore.Checked &&
				dateTimePickerAfter.Value >= dateTimePickerBefore.Value)
			{
				MessageBox.Show("Указан неверный диапазон дат!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls();
				return;
			}

			ClearChannels();
			ClearFramesChannel();
			ClearVideos();
			ClearFramesVideo();

			tabPageSearchResults.Text = "Результаты поиска";
			Application.DoEvents();
			scrollBarSearchResults.Value = 0;

			int maxResultsCount = rbSearchResultsUserDefined.Checked ? (int)numericUpDownSearchResult.Value : 500;

			int count = SearchYouTube(Uri.EscapeDataString(editQuery.Text), maxResultsCount, out string list);
			if (count > 0)
			{
				if (ParseList(list) > 0)
				{
					StackFrames();
				}
				tabControlMain.SelectedTab = tabPageSearchResults;
			}
			tabPageSearchResults.Text = $"Результаты поиска: {count}";

			EnableControls();
		}

		private void btnSearchByUrl_Click(object sender, EventArgs e)
		{
			DisableControls();

			string url = editSearchUrl.Text;
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

			ClearChannels();
			ClearFramesChannel();
			ClearVideos();
			ClearFramesVideo();

			tabPageSearchResults.Text = "Результаты поиска";
			Application.DoEvents();
			scrollBarSearchResults.Value = 0;

			try
			{
				YouTubeVideo video = GetSingleVideo(videoId);
				if (video != null)
				{
					FrameYouTubeVideo frame = new FrameYouTubeVideo(video, panelSearchResults);
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
					StackFrames();

					tabPageSearchResults.Text = "Результаты поиска: 1";
					tabControlMain.SelectedTab = tabPageSearchResults;
					editSearchUrl.Text = null;
				}
				else
				{
					MessageBox.Show("Ошибка поиска видео!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			EnableControls();
		}

		private void btnSearchByWebPage_Click(object sender, EventArgs e)
		{
			DisableControls();

			string webPageCode = richTextBoxWebPage.Text;
			if (string.IsNullOrEmpty(webPageCode) || string.IsNullOrWhiteSpace(webPageCode))
			{
				MessageBox.Show("Вставьте код веб-страницы с видео!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls();
				return;
			}

			ClearFramesChannel();
			ClearFramesVideo();
			ClearChannels();
			ClearVideos();

			tabPageSearchResults.Text = "Результаты поиска";
			Application.DoEvents();
			scrollBarSearchResults.Value = 0;

			try
			{
				YouTubeVideo video = YouTubeVideo.GetByWebPage(webPageCode);
				if (video != null)
				{
					if (!YouTubeApi.getMediaTracksInfoImmediately)
					{
						video.UpdateMediaFormats(video.RawInfo);
					}
					FrameYouTubeVideo frame = new FrameYouTubeVideo(video, panelSearchResults);
					frame._webPage = webPageCode;
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
					StackFrames();

					tabPageSearchResults.Text = "Результаты поиска: 1";
					tabControlMain.SelectedTab = tabPageSearchResults;
					editSearchUrl.Text = null;
					richTextBoxWebPage.Text = null;
				}
				else
				{
					MessageBox.Show("Ошибка поиска видео!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			EnableControls();
		}

		private void btnWhy_Click(object sender, EventArgs e)
		{
			DisableControls();

			string msg = "Это позволит скачать скрытое, заблокированное или 18+ видео, " +
				"если у вас есть к нему доступ из браузера.\nНо это не точно!";
			MessageBox.Show(msg, "Зачематор зачемок", MessageBoxButtons.OK, MessageBoxIcon.Information);

			EnableControls();
		}

		private void btnQ_Click(object sender, EventArgs e)
		{
			string msg = "Для достижения максимальной производительности и уменьшения нагрузки на накопители, " +
				"\"Папка для временных файлов\" и \"Папка для объединения чанков\" должны находиться " +
				"на разных физических дисках. А файл назначения не должен находиться на одном физическом диске с \"Папкой для объединения чанков\".\n" +
				"Если оставить это поле пустым, то \"Папка для объединения чанков\" будет равна \"Папке для временных файлов\".";
			MessageBox.Show(msg, "Зачематор зачемок", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void tvFavorites_ItemsRemoving(object sender, BrightIdeasSoftware.ItemsRemovingEventArgs e)
		{
			List<FavoriteItem> items = e.ObjectsToRemove.Cast<FavoriteItem>().ToList();
			for (int iItem = items.Count - 1; iItem >= 0; --iItem)
			{
				for (int iChild = items[iItem].Children.Count - 1; iChild >= 0; --iChild)
				{
					tvFavorites.RemoveObject(items[iItem].Children[iChild]);
					items[iItem].Children.RemoveAt(iChild);
				}
				items.RemoveAt(iItem);
			}
		}

		private void tvFavorites_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
		{
			FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
			if (item != null && item.Parent != null)
			{
				menuFavorites.Show(Cursor.Position);
			}
		}

		private void tvFavorites_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && tvFavorites.SelectedObject != null)
			{
				FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
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
							ClearChannels();
							ClearFramesChannel();
							ClearVideos();
							ClearFramesVideo();

							tabPageSearchResults.Text = "Результаты поиска";
							Application.DoEvents();
							scrollBarSearchResults.Value = 0;

							try
							{
								YouTubeVideoId youTubeVideoId = new YouTubeVideoId(item.ID);
								YouTubeVideo video = GetSingleVideo(youTubeVideoId);
								if (video != null)
								{
									FrameYouTubeVideo frame = new FrameYouTubeVideo(video, panelSearchResults);
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
									StackFrames();

									tabPageSearchResults.Text = "Результаты поиска: 1";
									tabControlMain.SelectedTab = tabPageSearchResults;
									editSearchUrl.Text = null;
								}
								else
								{
									MessageBox.Show("Ошибка поиска видео!", "Ошибка!",
										MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
							}
							catch (Exception ex)
							{
								System.Diagnostics.Debug.WriteLine(ex.Message);
								MessageBox.Show(ex.Message, "Ошибатор ошибок",
									MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
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

							if (chkPublishedAfter.Checked && chkPublishedBefore.Checked &&
								dateTimePickerAfter.Value >= dateTimePickerBefore.Value)
							{
								MessageBox.Show("Указан неверный диапазон дат!", "Ошибка!",
									MessageBoxButtons.OK, MessageBoxIcon.Error);
								EnableControls();
								return;
							}

							OpenChannel(item.ID);
						}
					}

					EnableControls();
				}
			}
		}

		private void OpenChannel(string channelId)
		{
			ClearChannels();
			ClearFramesChannel();
			ClearVideos();
			ClearFramesVideo();

			tabPageSearchResults.Text = "Результаты поиска";
			Application.DoEvents();
			scrollBarSearchResults.Value = 0;

			int maxResultsCount = rbSearchResultsUserDefined.Checked ? (int)numericUpDownSearchResult.Value : 500;
			int count = GetChannelVideosList(channelId, maxResultsCount, out string list);
			tabPageSearchResults.Text = $"Результаты поиска: {count}";
			if (count > 0)
			{
				if (ParseList(list) > 0)
				{
					StackFrames();
				}
				tabControlMain.SelectedTab = tabPageSearchResults;
			}
			else
			{
				MessageBox.Show("Ничего не найдено!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnBrowseDownloadingDirPath_Click(object sender, EventArgs e)
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
				editDownloadingDirPath.Text = config.DownloadingDirPath;
			}
			folderBrowserDialog.Dispose();
		}

		private void btnBrowseTempDirPath_Click(object sender, EventArgs e)
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
				editTempDirPath.Text = config.TempDirPath;
			}
			folderBrowserDialog.Dispose();
		}

		private void btnSelectMergingDirPath_Click(object sender, EventArgs e)
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
				editMergingDirPath.Text = config.ChunksMergingDirPath;
			}
			folderBrowserDialog.Dispose();
		}

		private void btnSelectBrowser_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Выберите EXE-файл браузера";
			ofd.Filter = "EXE-файлы|*.exe";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				config.BrowserExeFilePath = ofd.FileName;
				editBrowser.Text = config.BrowserExeFilePath;
			}
			ofd.Dispose();
		}

		private void btnBrowseFfmpeg_Click(object sender, EventArgs e)
		{
			OpenFileDialog ofd = new OpenFileDialog();
			ofd.Title = "Выберите EXE-файл FFMPEG";
			ofd.Filter = "EXE-файлы|*.exe";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				config.FfmpegExeFilePath = ofd.FileName;
				editFfmpeg.Text = config.FfmpegExeFilePath;
			}
			ofd.Dispose();
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
				if (chkPublishedAfter.Checked && chkPublishedBefore.Checked &&
					dateTimePickerAfter.Value >= dateTimePickerBefore.Value)
				{
					MessageBox.Show("Указан неверный диапазон дат!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				OpenChannel(channelId);
			}
		}

		private void editCipherDecryptionAlgo_Leave(object sender, EventArgs e)
		{
			config.CipherDecryptionAlgo = editCipherDecryptionAlgo.Text;
		}

		private void chkMergeAdaptive_CheckedChanged(object sender, EventArgs e)
		{
			config.MergeToContainer = chkMergeAdaptive.Checked;
			chkDeleteSourceFiles.Enabled = config.MergeToContainer;
		}

		private void editDownloadingPath_Leave(object sender, EventArgs e)
		{
			config.DownloadingDirPath = editDownloadingDirPath.Text;
		}

		private void editTempPath_Leave(object sender, EventArgs e)
		{
			config.TempDirPath = editTempDirPath.Text;
		}

		private void editYouTubeApiKey_Leave(object sender, EventArgs e)
		{
			config.YouTubeApiV3Key = editYouTubeApiKey.Text;
		}

		private void rbSearchResultsMax_Click(object sender, EventArgs e)
		{
			rbSearchResultsUserDefined.Checked = false;
			rbSearchResultsMax.Checked = true;
		}

		private void rbSearchResultsUserDefined_Click(object sender, EventArgs e)
		{
			rbSearchResultsMax.Checked = false;
			rbSearchResultsUserDefined.Checked = true;
		}

		private void openVideoInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
			if (item != null)
			{
				string url = $"{YOUTUBE_WATCH_URL_BASE}?v={item.ID}";
				OpenUrlInBrowser(url);
			}
		}

		private void openChannelInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
			if (item != null)
			{
				OpenUrlInBrowser(string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, item.ID));
			}
		}

		private void chkSaveImage_CheckedChanged(object sender, EventArgs e)
		{
			config.SavePreviewImage = chkSaveImage.Checked;
		}

		private void editOutputFileNameFormatWithDate_TextChanged(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithDate = editOutputFileNameFormatWithDate.Text;
		}

		private void btnResetFileNameFormatWithDate_Click(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithDate = FILENAME_FORMAT_DEFAULT_WITH_DATE;
			editOutputFileNameFormatWithDate.Text = FILENAME_FORMAT_DEFAULT_WITH_DATE;
		}

		private void editOutputFileNameFormatWithoutDate_TextChanged(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithoutDate = editOutputFileNameFormatWithoutDate.Text;
		}

		private void btnResetFileNameFormatWithoutDate_Click(object sender, EventArgs e)
		{
			config.OutputFileNameFormatWithoutDate = FILENAME_FORMAT_DEFAULT_WITHOUT_DATE;
			editOutputFileNameFormatWithoutDate.Text = FILENAME_FORMAT_DEFAULT_WITHOUT_DATE;
		}

		private void numericUpDownThreadsAudio_ValueChanged(object sender, EventArgs e)
		{
			config.ThreadCountAudio = (int)numericUpDownThreadsAudio.Value;
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

		private void numericUpDownThreadsVideo_ValueChanged(object sender, EventArgs e)
		{
			config.ThreadCountVideo = (int)numericUpDownThreadsVideo.Value;
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

		private void numericUpDownGlobalThreadsMaximum_ValueChanged(object sender, EventArgs e)
		{
			config.GlobalThreadCountMaximum = (int)numericUpDownGlobalThreadsMaximum.Value;
			MultiThreadedDownloader.SetMaximumConnectionsLimit(config.GlobalThreadCountMaximum);
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
			tvFavorites.Font = new Font(tvFavorites.Font.FontFamily, config.FavoritesListFontSize);
		}

		public void SetMenusFontSize(int fontSize)
		{
			menuFavorites.SetFontSize(fontSize);
			menuCopyPaste.SetFontSize(fontSize);
		}

		private void chkUseHiddenApiForGettingInfo_CheckedChanged(object sender, EventArgs e)
		{
			config.UseHiddenApiForGettingInfo = chkUseHiddenApiForGettingInfo.Checked;
		}

		private void editFfmpeg_Leave(object sender, EventArgs e)
		{
			config.FfmpegExeFilePath = editFfmpeg.Text;
		}

		private void editMergingDirPath_Leave(object sender, EventArgs e)
		{
			config.ChunksMergingDirPath = editMergingDirPath.Text;
		}

		private void chkDeleteSourceFiles_CheckedChanged(object sender, EventArgs e)
		{
			config.DeleteSourceFiles = chkDeleteSourceFiles.Checked;
		}

		private void radioButtonContainerTypeMp4_CheckedChanged(object sender, EventArgs e)
		{
			config.AlwaysUseMkvContainer = !radioButtonContainerTypeMp4.Checked;
		}

		private void radioButtonContainerTypeMkv_CheckedChanged(object sender, EventArgs e)
		{
			config.AlwaysUseMkvContainer = radioButtonContainerTypeMkv.Checked;
		}

		private void numericUpDownDelayAfterContainerCreated_ValueChanged(object sender, EventArgs e)
		{
			config.ExtraDelayAfterContainerWasBuilt = (int)numericUpDownDelayAfterContainerCreated.Value;
		}
		
		private void chkSearchVideos_CheckedChanged(object sender, EventArgs e)
		{
			if (!chkSearchVideos.Checked && !chkSearchChannels.Checked)
			{
				chkSearchChannels.Checked = true;
			}
		}

		private void chkSearchChannels_CheckedChanged(object sender, EventArgs e)
		{
			if (!chkSearchChannels.Checked && !chkSearchVideos.Checked)
			{
				chkSearchVideos.Checked = true;
			}
		}

		private void numericUpDownSearchResult_ValueChanged(object sender, EventArgs e)
		{
			config.MaxSearch = (int)numericUpDownSearchResult.Value;
		}

		private void menuCopyPaste_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (richTextBoxWebPage.TextLength > 0)
			{
				cutTextToolStripMenuItem.Enabled = !string.IsNullOrEmpty(richTextBoxWebPage.SelectedText);
				copyTextToolStripMenuItem.Enabled = true;
				selectAllTextToolStripMenuItem.Enabled = true;
			}
			else
			{
				cutTextToolStripMenuItem.Enabled = false;
				copyTextToolStripMenuItem.Enabled = false;
				selectAllTextToolStripMenuItem.Enabled = false;
			}
		}

		private void cutTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string t = richTextBoxWebPage.SelectedText;
			if (!string.IsNullOrEmpty(t))
			{
				richTextBoxWebPage.Cut();
			}
		}

		private void copyTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richTextBoxWebPage.Copy();
		}

		private void pasteTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Clipboard.ContainsText())
			{
				richTextBoxWebPage.Paste();
			}
		}

		private void selectAllTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			richTextBoxWebPage.SelectAll();
		}

		private void menuFavorites_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
			if (item != null)
			{
				switch (item.ItemType)
				{
					case FavoriteItemType.Video:
						openVideoInBrowserToolStripMenuItem.Visible = true;
						miCopyVideoUrlToolStripMenuItem.Visible = true;
						openChannelInBrowserToolStripMenuItem.Visible = false;
						miCopyChannelUrlToolStripMenuItem.Visible = true;
						miCopyChannelIdToolStripMenuItem.Visible = true;
						miCopyVideoIdToolStripMenuItem.Visible = true;
						copyDisplayNameWithIdToolStripMenuItem.Visible = true;
						break;

					case FavoriteItemType.Channel:
						openVideoInBrowserToolStripMenuItem.Visible = false;
						openChannelInBrowserToolStripMenuItem.Visible = true;
						miCopyVideoUrlToolStripMenuItem.Visible = false;
						miCopyChannelUrlToolStripMenuItem.Visible = true;
						miCopyChannelIdToolStripMenuItem.Visible = true;
						miCopyVideoIdToolStripMenuItem.Visible = false;
						copyDisplayNameWithIdToolStripMenuItem.Visible = true;
						break;

					case FavoriteItemType.Directory:
						openVideoInBrowserToolStripMenuItem.Visible = false;
						miCopyVideoUrlToolStripMenuItem.Visible = false;
						openChannelInBrowserToolStripMenuItem.Visible = false;
						miCopyChannelUrlToolStripMenuItem.Visible = false;
						miCopyChannelIdToolStripMenuItem.Visible = false;
						miCopyVideoIdToolStripMenuItem.Visible = false;
						copyDisplayNameWithIdToolStripMenuItem.Visible = false;
						break;
				}
			}
		}

		private void copyDisplayNameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
			if (item != null)
			{
				SetClipboardText(item.DisplayName);
			}
		}

		private void copyDisplayNameWithIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
			if (item != null && item.ItemType != FavoriteItemType.Directory)
			{
				SetClipboardText($"{item.DisplayName} [{item.ID}]");
			}
		}

		private void miCopyVideoUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
			if (item != null && item.ItemType == FavoriteItemType.Video)
			{
				string url = $"{YOUTUBE_WATCH_URL_BASE}?v={item.ID}";
				SetClipboardText(url);
			}
		}

		private void miCopyChannelUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
			if (item != null && item.ItemType != FavoriteItemType.Directory)
			{
				string id = item.ItemType == FavoriteItemType.Video ? item.ChannelId : item.ID;
				string url = string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, id);
				SetClipboardText(url);
			}
		}

		private void miCopyVideoIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
			if (item != null && item.ItemType == FavoriteItemType.Video)
			{
				SetClipboardText(item.ID);
			}
		}

		private void miCopyChannelIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
			if (item != null && item.ItemType != FavoriteItemType.Directory)
			{
				string id = item.ItemType == FavoriteItemType.Video ? item.ChannelId : item.ID;
				SetClipboardText(id);
			}
		}

		private void scrollBarSearchResults_Scroll(object sender, ScrollEventArgs e)
		{
			scrollBarSearchResults.Focus();
			StackFrames();
		}

		private void DisableControls()
		{
			btnSearchByWebPage.Enabled = false;
			btnSearchByUrl.Enabled = false;
			btnSearchByQuery.Enabled = false;
			editQuery.Enabled = false;
			editSearchUrl.Enabled = false;
			btnWhy.Enabled = false;
			btnApiWtf.Enabled = true;
		}

		private void EnableControls()
		{
			btnSearchByWebPage.Enabled = true;
			btnSearchByUrl.Enabled = true;
			btnSearchByQuery.Enabled = true;
			editQuery.Enabled = true;
			editSearchUrl.Enabled = true;
			btnWhy.Enabled = true;
			btnApiWtf.Enabled = true;
		}

		private void btnApiWtf_Click(object sender, EventArgs e)
		{
			btnApiWtf.Enabled = false;
			string msg = "Снимает ограничение ютуба на скорость скачивания и позволяет немного оттянуть " +
				"неизбежный момент возникновения ошибки \"HTTP 429: Too many requests\".\n" +
				"Внимание! Это не повлияет на скорость скачивания видео с доступом только по ссылке!\n" +
				"Если эта галочка включена, то не нужно вводить алгоритм для расшифровки \"Cipher\".";
			MessageBox.Show(msg, "Подсказатор подсказок",
				MessageBoxButtons.OK, MessageBoxIcon.Information);
			btnApiWtf.Enabled = true;
		}

		private void btnUseRamWhy_Click(object sender, EventArgs e)
		{
			btnUseRamWhy.Enabled = false;
			string msg = "Это позволяет ускорить скачивание, сократив количество обращений к накопителю.";
			MessageBox.Show(msg, "Зачематор зачемок", MessageBoxButtons.OK, MessageBoxIcon.Information);
			btnUseRamWhy.Enabled = true;
		}

		private void btnDownloadAllAdaptiveVideoTracksWtf_Click(object sender, EventArgs e)
		{
			btnDownloadAllAdaptiveVideoTracksWtf.Enabled = false;
			string msg = "Будут скачаны все адаптивные форматы видео, " +
				"не зависимо от того, какой формат был выбран.\n" +
				"Данная опция игнорируется при выборе форматов из окна выбора.\n" +
				"Данная опция не сохраняется в файле конфигурации " +
				"и автоматически отключается при каждом перезапуске программы.";
			MessageBox.Show(msg, "Подсказатор подсказок",
				MessageBoxButtons.OK, MessageBoxIcon.Information);
			btnDownloadAllAdaptiveVideoTracksWtf.Enabled = true;
		}

		private void chkUseRamForTempFiles_CheckedChanged(object sender, EventArgs e)
		{
			if (!Is64BitProcess && chkUseRamForTempFiles.Enabled)
			{
				chkUseRamForTempFiles.Enabled = false;
				chkUseRamForTempFiles.Checked = false;
				config.UseRamToStoreTemporaryFiles = false;
				MessageBox.Show("Это должно быть доступно только в 64-битной версии программы!",
					"Низя так делать, Вася ты чо!",
					MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
				return;
			}
			config.UseRamToStoreTemporaryFiles = chkUseRamForTempFiles.Checked;
		}

		private void chkSortDashFormatsByBitrate_CheckedChanged(object sender, EventArgs e)
		{
			config.SortDashFormatsByBitrate = chkSortDashFormatsByBitrate.Checked;
		}

		private void checkBoxUseGmtTime_CheckedChanged(object sender, EventArgs e)
		{
			config.UseGmtTime = checkBoxUseGmtTime.Checked;
		}

		private void numericUpDownDownloadRetryCount_ValueChanged(object sender, EventArgs e)
		{
			config.DownloadRetryCount = (int)numericUpDownDownloadRetryCount.Value;
		}

		private void checkBoxAlwaysDownloadAsDash_CheckedChanged(object sender, EventArgs e)
		{
			config.AlwaysDownloadAsDash = checkBoxAlwaysDownloadAsDash.Checked;
		}

		private void numericUpDownDashChunkDownloadRetriesCountMax_ValueChanged(object sender, EventArgs e)
		{
			config.DashDownloadRetryCountMax = (int)numericUpDownDashChunkDownloadRetriesCountMax.Value;
		}

		private void numericUpDownDashChunkSize_ValueChanged(object sender, EventArgs e)
		{
			config.DashManualFragmentationChunkSize = (long)numericUpDownDashChunkSize.Value;
			lblActualDashChunkSize.Text = FormatSize(config.DashManualFragmentationChunkSize);
		}

		private void btnWhyDash_Click(object sender, EventArgs e)
		{
			btnWhyDash.Enabled = false;
			string msg = "Бывают ситуации, когда обычным способом видео " +
				"не качается из-за постоянных разрывов соединения с сервером. " +
				"Эта опция может помочь обойти данную проблему. " +
				"Однако, скорость скачивания может стать намного ниже обычного :'(";
			MessageBox.Show(msg, "Зачематор зачемок",
				MessageBoxButtons.OK, MessageBoxIcon.Information);
			btnWhyDash.Enabled = true;
		}

		private void checkBoxUseVideoInfoServer_CheckedChanged(object sender, EventArgs e)
		{
			config.UseVideoInfoServerForAdultVideos = checkBoxUseVideoInfoServerForAdultVideos.Checked;
		}

		private void checkBoxAlwaysUseVideoInfoServer_CheckedChanged(object sender, EventArgs e)
		{
			bool flag = checkBoxAlwaysUseVideoInfoServer.Checked;
			config.AlwaysUseVideoInfoServer = flag;
			chkUseHiddenApiForGettingInfo.Enabled = !flag;
			editCipherDecryptionAlgo.Enabled = !flag;
			editYouTubeApiKey.Enabled = !flag;
			checkBoxUseVideoInfoServerForAdultVideos.Enabled = !flag;
		}

		private void btnVideoInfoServerWhy_Click(object sender, EventArgs e)
		{
			btnVideoInfoServerWhy.Enabled = false;
			string msg = "Этот сервер можно использовать в особых случаях. " +
				"Например, для скачивания видео 18+ или когда другое API не работает.";
			MessageBox.Show(msg, "Зачематор зачемок",
				MessageBoxButtons.OK, MessageBoxIcon.Information);
			btnVideoInfoServerWhy.Enabled = true;
		}

		private void numericUpDownVideoInfoServerPort_ValueChanged(object sender, EventArgs e)
		{
			config.VideoInfoServerPort = (int)numericUpDownVideoInfoServerPort.Value;
		}
	}
}
