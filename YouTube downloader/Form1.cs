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

			objectTreeViewFavorites.ChildrenGetter = obj => ((FavoriteItem)obj).Children;
			objectTreeViewFavorites.ParentGetter = obj => ((FavoriteItem)obj).Parent;
			objectTreeViewFavorites.CanExpandGetter = obj => ((FavoriteItem)obj).Children.Count > 0;
			objectTreeViewFavorites.Roots = new List<FavoriteItem>() { new FavoriteItem("Избранное") };
			favoritesRootNode = objectTreeViewFavorites.Roots.Cast<FavoriteItem>().ToArray()[0];
			treeFavorites = objectTreeViewFavorites;

			dateTimePickerSearchAfter.Value = DateTime.Now - TimeSpan.FromDays(30);

			config.Saving += (s, json) => json["maximumSearchResults"] = config.MaximumSearchResults;
			config.Loading += (s, json) =>
			{
				JToken jt = json.Value<JToken>("maximumSearchResults");
				if (jt != null)
				{
					config.MaximumSearchResults = ClampValue(jt.Value<int>(), 1, 500);
				}
			};
			config.Loaded += s =>
			{
				numericUpDownSearchResultCountLimit.Value = config.MaximumSearchResults;
				MultiThreadedDownloaderLib.Utils.ConnectionLimit = config.GlobalThreadCountMaximum;
			};
			config.MenusFontSizeChanged += (s, sz) => SetMenusFontSize(sz);
			config.FavoritesListFontSizeChanged += (s, sz) => objectTreeViewFavorites.Font = new Font(objectTreeViewFavorites.Font.FontFamily, sz);
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
					if (!Directory.Exists(config.HomeDirectory))
					{
						Directory.CreateDirectory(config.HomeDirectory);
					}
					if (Directory.Exists(config.HomeDirectory))
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
				if (item == null || item.Parent == null) { return; }

				if (item.ItemType != FavoriteItemType.Directory)
				{
					EnableControls(false);

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

							await FindVideoById(new YouTubeVideoId(item.ID));
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
								EnableControls(true);
								return;
							}

							if (checkBoxSearchRangePublishedAfter.Checked && checkBoxSearchRangePublishedBefore.Checked &&
								dateTimePickerSearchAfter.Value >= dateTimePickerSearchBefore.Value)
							{
								MessageBox.Show("Указан неверный диапазон дат!", "Ошибка!",
									MessageBoxButtons.OK, MessageBoxIcon.Error);
								EnableControls(true);
								return;
							}

							YouTubeChannel channel = new YouTubeChannel(item.ID, item.Title);
							OpenChannel(channel);
						}
					}

					EnableControls(true);
				}
			}
		}

		private void btnWtfWebPageCode_Click(object sender, EventArgs e)
		{
			EnableControls(false);

			string msg = "Это позволит скачать скрытое, заблокированное или 18+ видео, " +
				"если у вас есть к нему доступ из браузера.\nНо это не точно!";
			MessageBox.Show(msg, "Зачематор зачемок", MessageBoxButtons.OK, MessageBoxIcon.Information);

			EnableControls(true);
		}

		private async void btnSearchByQuery_Click(object sender, EventArgs e)
		{
			EnableControls(false);

			if (string.IsNullOrEmpty(textBoxSearchQuery.Text) || string.IsNullOrWhiteSpace(textBoxSearchQuery.Text))
			{
				MessageBox.Show("Введите поисковый запрос!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls(true);
				textBoxSearchQuery.Focus();
				return;
			}
			if (string.IsNullOrEmpty(config.YouTubeApiV3Key))
			{
				MessageBox.Show("Для использования этой функции, необходимо ввести ключ от API ютуба!",
					"Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Information);
				EnableControls(true);
				return;
			}
			if (checkBoxSearchRangePublishedAfter.Checked && checkBoxSearchRangePublishedBefore.Checked &&
				dateTimePickerSearchAfter.Value >= dateTimePickerSearchBefore.Value)
			{
				MessageBox.Show("Указан неверный диапазон дат!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls(true);
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

			EnableControls(true);
		}

		private async void btnSearchByVideoUrlOrId_Click(object sender, EventArgs e)
		{
			EnableControls(false);

			string url = textBoxVideoUrlOrId.Text;
			if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
			{
				MessageBox.Show("Не введена ссылка!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls(true);
				return;
			}

			YouTubeVideoId videoId = ExtractVideoIdFromUrl(url);
			if (videoId == null || string.IsNullOrEmpty(videoId.Id) || string.IsNullOrWhiteSpace(videoId.Id))
			{
				MessageBox.Show("Не удалось распознать ID видео!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls(true);
				return;
			}

			if (videoId.Id.Length != 11)
			{
				MessageBox.Show("Введённый вами или автоматически определённый ID видео " +
					$"имеет длину {videoId.Id.Length} символов. Такого не может быть!", "Ошибатор ошибок",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls(true);
				return;
			}

			ClearChannelInfos();
			ClearFramesChannel();
			ClearVideos();
			ClearFramesVideo();

			tabPageSearchResults.Text = "Результаты поиска";
			scrollBarSearchResults.Value = 0;

			await FindVideoById(videoId);

			EnableControls(true);
		}

		private void btnSearchByWebPage_Click(object sender, EventArgs e)
		{
			EnableControls(false);

			string webPageCode = richTextBoxWebPageCode.Text;
			if (string.IsNullOrEmpty(webPageCode) || string.IsNullOrWhiteSpace(webPageCode))
			{
				MessageBox.Show("Вставьте код веб-страницы с видео!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				EnableControls(true);
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
					CreateAndAddNewVideoFrame(video, webPageResult.VideoWebPage);
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

			EnableControls(true);
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

		private void numericUpDownSearchResultCountLimit_ValueChanged(object sender, EventArgs e)
		{
			config.MaximumSearchResults = (int)numericUpDownSearchResultCountLimit.Value;
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
						miOpenVideoInBrowserToolStripMenuItem.Visible =
						miCopyVideoUrlToolStripMenuItem.Visible = true;
						miOpenChannelInBrowserToolStripMenuItem.Visible = false;
						miCopyChannelUrlToolStripMenuItem.Visible =
						miCopyChannelIdToolStripMenuItem.Visible =
						miCopyVideoIdToolStripMenuItem.Visible =
						miCopyDisplayNameWithIdToolStripMenuItem.Visible = true;
						break;

					case FavoriteItemType.Channel:
						miOpenVideoInBrowserToolStripMenuItem.Visible = false;
						miOpenChannelInBrowserToolStripMenuItem.Visible = true;
						miCopyVideoUrlToolStripMenuItem.Visible = false;
						miCopyChannelUrlToolStripMenuItem.Visible =
						miCopyChannelIdToolStripMenuItem.Visible = true;
						miCopyVideoIdToolStripMenuItem.Visible = false;
						miCopyDisplayNameWithIdToolStripMenuItem.Visible = true;
						break;

					case FavoriteItemType.Directory:
						miOpenVideoInBrowserToolStripMenuItem.Visible =
						miCopyVideoUrlToolStripMenuItem.Visible =
						miOpenChannelInBrowserToolStripMenuItem.Visible =
						miCopyChannelUrlToolStripMenuItem.Visible =
						miCopyChannelIdToolStripMenuItem.Visible =
						miCopyVideoIdToolStripMenuItem.Visible =
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
				string url = GetYouTubeVideoUrl(item.ID);
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
				string url = GetYouTubeVideoUrl(item.ID);
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
			JObject json = new JObject()
			{
				["displayName"] = root.DisplayName
			};
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
			JObject json = new JObject()
			{
				["items"] = ja
			};
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
				jt = item.Value<JToken>("type") ?? throw new ArgumentNullException("type = NULL");
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
				else
				{
					return false;
				}
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

						FrameYouTubeChannel frame = new FrameYouTubeChannel()
						{
							Parent = panelSearchResults
						};
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
							CreateAndAddNewVideoFrame(video);
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

				CreateAndAddNewVideoFrame(video);
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

		private void CreateAndAddNewVideoFrame(YouTubeVideo video, YouTubeVideoWebPage webPage = null)
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
					CreateAndAddNewVideoFrame(video);
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

		private void EnableControls(bool enabled)
		{
			btnSearchByWebPage.Enabled =
			btnSearchByVideoUrlOrId.Enabled =
			btnSearchByQuery.Enabled =
			textBoxSearchQuery.Enabled =
			textBoxVideoUrlOrId.Enabled =
			btnWtfWebPageCode.Enabled = enabled;
		}
	}
}
