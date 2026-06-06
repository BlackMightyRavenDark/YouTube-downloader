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
using MultiThreadedDownloaderLib;
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
			SetupListView();
			dateTimePickerSearchAfter.Value = DateTime.UtcNow - TimeSpan.FromDays(30);

			config.Saving += (s, json) => json["maximumSearchResults"] = config.SearchResultCountLimit;
			config.Loading += (s, json) =>
			{
				JToken jt = json.Value<JToken>("maximumSearchResults");
				if (jt != null)
				{
					config.SearchResultCountLimit = ClampValue(jt.Value<int>(), 1, 500);
				}
			};
			config.Loaded += s =>
			{
				numericUpDownSearchResultCountLimit.Value = config.SearchResultCountLimit;
				MultiThreadedDownloaderLib.Utils.ConnectionLimit = config.GlobalThreadCountLimit;
			};
			config.MenusFontSizeChanged += (s, sz) => SetMenusFontSize(sz);
			config.FavoritesListFontSizeChanged += (s, sz) => objectTreeViewFavorites.Font = new Font(objectTreeViewFavorites.Font.FontFamily, sz);
			config.Load();

			try
			{
				if (File.Exists(config.FavoritesFilePath))
				{
					LoadFavorites(config.FavoritesFilePath);
				}
				objectTreeViewFavorites.Expand(favoritesRootNode);
				isFavoritesLoaded = true;
			}
			catch (Exception ex)
			{
#if DEBUG
				System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
				objectTreeViewFavorites.Enabled = false;
				isFavoritesLoaded = false;
				string msg = "Ошибка загрузки избранного! " +
					$"Список избранного недоступен до перезапуска программы!{Environment.NewLine}{ex.Message}";
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

		private void SetupListView()
		{
			if (favoritesRootNode == null)
			{
				objectTreeViewFavorites.ChildrenGetter = obj => (obj as FavoriteItem).Children;
				objectTreeViewFavorites.ParentGetter = obj => (obj as FavoriteItem).Parent;
				objectTreeViewFavorites.CanExpandGetter = obj => (obj as FavoriteItem).Children?.Count > 0;
				favoritesRootNode = new FavoriteItem("Избранное");
				objectTreeViewFavorites.Roots = new List<FavoriteItem>() { favoritesRootNode };
				treeFavorites = objectTreeViewFavorites;
			}
		}

		private void panelSearchResults_Resize(object sender, EventArgs e)
		{
			StackFrames();
		}

		private void objectTreeViewFavorites_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
		{
			if (objectTreeViewFavorites.SelectedObject is FavoriteItem item && item.Parent != null)
			{
				contextMenuFavorites.Show(Cursor.Position);
			}
		}

		private async void objectTreeViewFavorites_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && objectTreeViewFavorites.SelectedObject != null)
			{
				FavoriteItem item = objectTreeViewFavorites.SelectedObject as FavoriteItem;
				if (item?.Parent == null) { return; } // Root item or an empty space was clicked.

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

							await FindVideoById(new YouTubeVideoId(item.Id));
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

							string id = !string.IsNullOrEmpty(item.ChannelId) && !string.IsNullOrWhiteSpace(item.ChannelId) ? item.ChannelId : item.Id;
							YouTubeChannel channel = new YouTubeChannel(id, item.Title);
							OpenChannel(channel);
						}
					}

					EnableControls(true);
				}
			}
		}

		private void btnWtfWebPageCode_Click(object sender, EventArgs e)
		{
			string msg = "Это позволит скачать скрытое, заблокированное или 18+ видео, " +
				$"если у вас есть доступ к нему из браузера.{Environment.NewLine}Но это не точно!";
			MessageBox.Show(msg, "Зачематор зачемок", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void btnWtfSearchApiV3_Click(object sender, EventArgs e)
		{
			string msg = "Обратите внимание, что поиск через YouTube API V3 глючный! Иногда он пропускает некоторые видео!";
			MessageBox.Show(msg, "Обращатор на себя внимания", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

			try
			{
				ushort resultCountLimit = (ushort)(radioButtonSearchResultCountLimitUserDefinedNumber.Checked ? numericUpDownSearchResultCountLimit.Value : 500);
				string searchQuery = UrlEncode(textBoxSearchQuery.Text);
				List<string> strings = new List<string>();
				if (checkBoxSearchVideos.Checked) { strings.Add("video"); }
				if (checkBoxSearchChannels.Checked) { strings.Add("channel"); }
				string types = string.Join(",", strings);
				DateTime after = checkBoxSearchRangePublishedAfter.Checked ? dateTimePickerSearchAfter.Value.ToUniversalTime() : DateTime.MaxValue;
				DateTime before = checkBoxSearchRangePublishedBefore.Checked ? dateTimePickerSearchBefore.Value.ToUniversalTime() : DateTime.MaxValue;
				FileDownloader d = new FileDownloader();
				d.Headers.Add("User-Agent", config.UserAgent);
				YouTubeQuerySearcherV3 searcher = new YouTubeQuerySearcherV3(config.YouTubeApiV3Key, searchQuery,
					after, before, types, null, resultCountLimit, null, d);
				int totalFound = await Task.Run(() => (int)searcher.Search());
				tabPageSearchResults.Text = $"Результаты поиска: {totalFound}";
				if (totalFound > 0)
				{
					foreach (YouTubeSearcherV3ResultVideo v3Video in searcher.FoundVideos)
					{
						CreateAndAddNewFrame(v3Video.ToVideo(), null, false, false);
					}

					foreach (YouTubeSearcherV3ResultChannel v3Channel in searcher.FoundChannels)
					{
						YouTubeChannelInfo channelInfo = new YouTubeChannelInfo()
						{
							Id = v3Channel.Id,
							Title = v3Channel.Title,
							ImageUrl = v3Channel.Thumbnails[0].Url,
							ImageData = new MemoryStream()
						};
						await Task.Run(() => DownloadData(channelInfo.ImageUrl, channelInfo.ImageData));
						CreateAndAddNewFrame(channelInfo);
					}

					StackFrames();
					tabControlMain.SelectedTab = tabPageSearchResults;

					if (framesVideo.Count > 0)
					{
						int remaining = framesVideo.Count;
						while (remaining > 0)
						{
							List<FrameYouTubeVideo> group = framesVideo.GetRange(framesVideo.Count - remaining,
								remaining > config.SimultaneousLoadThumbnailGroupSize ? config.SimultaneousLoadThumbnailGroupSize : remaining);
							var tasks = group.Select(item => Task.Run(() => item.DownloadAndSetVideoThumbnail()));
							await Task.Run(async () =>
							{
								await Task.WhenAll(tasks);
								remaining -= group.Count;
								if (remaining > 0 && config.IntervalBetweenThumbnailGroupsLoadMilliseconds > 0)
								{
									await Task.Delay(config.IntervalBetweenThumbnailGroupsLoadMilliseconds);
								}
							});
						}
					}
				}
				else
				{
					MessageBox.Show("Ничего не найдено!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибатор ошибок", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

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
					CreateAndAddNewFrame(video, webPageResult.VideoWebPage, true, true);
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
			config.SearchResultCountLimit = (int)numericUpDownSearchResultCountLimit.Value;
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
			if (objectTreeViewFavorites.SelectedObject is FavoriteItem item)
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
			if (objectTreeViewFavorites.SelectedObject is FavoriteItem item)
			{
				SetClipboardText(item.DisplayName);
			}
		}

		private void miCopyDisplayNameWithIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (objectTreeViewFavorites.SelectedObject is FavoriteItem item &&
				item.ItemType != FavoriteItemType.Directory)
			{
				SetClipboardText($"{item.DisplayName} [{item.Id}]");
			}
		}

		private void miCopyVideoUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (objectTreeViewFavorites.SelectedObject is FavoriteItem item &&
				item.ItemType == FavoriteItemType.Video)
			{
				string url = GetYouTubeVideoUrl(item.Id);
				SetClipboardText(url);
			}
		}

		private void miCopyChannelUrlToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (objectTreeViewFavorites.SelectedObject is FavoriteItem item &&
				item.ItemType != FavoriteItemType.Directory)
			{
				string id = item.ItemType == FavoriteItemType.Video ? item.ChannelId : item.Id;
				string url = string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, id);
				SetClipboardText(url);
			}
		}

		private void miCopyVideoIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (objectTreeViewFavorites.SelectedObject is FavoriteItem item &&
				item.ItemType == FavoriteItemType.Video)
			{
				SetClipboardText(item.Id);
			}
		}

		private void miCopyChannelIdToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (objectTreeViewFavorites.SelectedObject is FavoriteItem item &&
				item.ItemType != FavoriteItemType.Directory)
			{
				string id = item.ItemType == FavoriteItemType.Video ? item.Id : item.ChannelId;
				SetClipboardText(id);
			}
		}

		private void miOpenVideoInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (objectTreeViewFavorites.SelectedObject is FavoriteItem item)
			{
				string url = GetYouTubeVideoUrl(item.Id);
				OpenUrlInBrowser(url, out _);
			}
		}

		private void miOpenChannelInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (objectTreeViewFavorites.SelectedObject is FavoriteItem item)
			{
				string url = string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, item.Id);
				OpenUrlInBrowser(url, out _);
			}
		}

		private void miCutTextToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(richTextBoxWebPageCode.SelectedText))
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

			if (root.Children.Count > 0) // The item is directory (folder).
			{
				JArray jaSubItems = new JArray();
				for (int i = 0; i < root.Children.Count; ++i)
				{
					SaveNode(root.Children[i], jaSubItems);
				}
				json["type"] = "directory";
				json["subItems"] = jaSubItems;
			}
			else // The item is video or channel.
			{
				json["title"] = root.Title;
				json["type"] = root.ItemType == FavoriteItemType.Video ? "video" : "channel";
				json["id"] = root.Id;

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
			JArray jsonArr = new JArray();
			for (int i = 0; i < favoritesRootNode.Children.Count; ++i)
			{
				SaveNode(favoritesRootNode.Children[i], jsonArr);
			}
			JObject json = new JObject()
			{
				["items"] = jsonArr
			};
			File.WriteAllText(fileName, json.ToString());
		}

		private void ParseFavoritesItem(JObject item, FavoriteItem root)
		{
			string displayName = item.Value<string>("displayName");
			JToken jt = item.Value<JToken>("title");
			string title = jt != null ? jt.Value<string>() : displayName;
			FavoriteItem favoriteItem = new FavoriteItem(title, displayName, root);
			JArray jaSubItems = item.Value<JArray>("subItems");
			if (jaSubItems != null)
			{
				favoriteItem.ItemType = FavoriteItemType.Directory;
				if (jaSubItems.Count > 0)
				{
					foreach (JObject j in jaSubItems.Cast<JObject>())
					{
						ParseFavoritesItem(j, favoriteItem);
					}
				}
			}
			else
			{
				jt = item.Value<JToken>("type") ?? throw new ArgumentNullException("Favorite item type is undefined!");
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
					throw new ArgumentException($"Invalid DataType value: {t}");
				}

				favoriteItem.Id = item.Value<string>("id");

				jt = item.Value<JToken>("channelTitle");
				if (jt != null)
				{
					favoriteItem.ChannelTitle = jt.Value<string>();
				}
				jt = item.Value<JToken>("channelId");
				if (jt != null)
				{
					favoriteItem.ChannelId = jt.Value<string>();
					if (string.IsNullOrEmpty(favoriteItem.ChannelId) || string.IsNullOrWhiteSpace(favoriteItem.ChannelId))
					{
						favoriteItem.ChannelId = favoriteItem.Id;
					}
				}
			}

			root.Children.Add(favoriteItem);
		}

		private void LoadFavorites(string fileName)
		{
			JObject json = JObject.Parse(File.ReadAllText(fileName));
			JArray jaItems = json.Value<JArray>("items");
			foreach (JObject j in jaItems.Cast<JObject>())
			{
				ParseFavoritesItem(j, favoritesRootNode);
			}
		}

		private async Task<bool> FindVideoById(YouTubeVideoId videoId)
		{
			YouTubeVideoWebPage webPage = null;
			string errorMessage = null;
			YouTubeVideo video = await Task.Run(() => GetSingleVideo(videoId, out webPage, out errorMessage));
			if (video != null)
			{
				if (!(video is YtdlVideo) && !video.IsSucceed())
				{
					string msg = "Ошибка поиска видео!" + Environment.NewLine +
						(!string.IsNullOrEmpty(errorMessage) ? errorMessage : "Что-то где-то пошло не так или не пошло вообще!");
					MessageBox.Show(msg, "Ошибатор ошибок", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				CreateAndAddNewFrame(video, webPage, true, true);
				StackFrames();

				tabPageSearchResults.Text = $"Результаты поиска: {framesVideo.Count + framesChannel.Count}";
				tabControlMain.SelectedTab = tabPageSearchResults;
				textBoxVideoUrlOrId.Text = null;

				return true;
			}
			else
			{
				string msg = "Ошибка поиска видео!";
				if (!string.IsNullOrEmpty(errorMessage) && !string.IsNullOrWhiteSpace(errorMessage))
				{
					msg += Environment.NewLine + errorMessage;
				}
				MessageBox.Show(msg, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return false;
		}

		private void CreateAndAddNewFrame(YouTubeVideo video, YouTubeVideoWebPage webPage,
			bool isVideoInfoFoundByWebPage, bool updateThumbnail)
		{
			FrameYouTubeVideo frame = new FrameYouTubeVideo(video, webPage, isVideoInfoFoundByWebPage,
				updateThumbnail, panelSearchResults);
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

		private void CreateAndAddNewFrame(YouTubeChannelInfo channel)
		{
			FrameYouTubeChannel frame = new FrameYouTubeChannel(panelSearchResults);
			frame.SetChannelInfo(channel);
			frame.Activated += event_FrameActivated;
			frame.OpenChannel += event_OpenChannel;
			framesChannel.Add(frame);
		}

		private void ClearChannelInfos()
		{
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

		/// <summary>
		/// Найти и отобразить видео с указанного канала, используя текущие параметры поиска.
		/// </summary>
		private async void OpenChannel(YouTubeChannel channel)
		{
			try
			{
				ClearChannelInfos();
				ClearFramesChannel();
				ClearVideos();
				ClearFramesVideo();

				tabPageSearchResults.Text = "Результаты поиска";
				scrollBarSearchResults.Value = 0;

				ushort resultCountLimit = (ushort)(radioButtonSearchResultCountLimitUserDefinedNumber.Checked ? numericUpDownSearchResultCountLimit.Value : 500);
				DateTime after = checkBoxSearchRangePublishedAfter.Checked ? dateTimePickerSearchAfter.Value.ToUniversalTime() : DateTime.MaxValue;
				DateTime before = checkBoxSearchRangePublishedBefore.Checked ? dateTimePickerSearchBefore.Value.ToUniversalTime() : DateTime.MaxValue;
				FileDownloader d = new FileDownloader();
				d.Headers.Add("User-Agent", config.UserAgent);
				YouTubeChannelSearcherV3 searcher = new YouTubeChannelSearcherV3(
					config.YouTubeApiV3Key, channel.Id, after, before, resultCountLimit, null, d);
				int totalFound = await Task.Run(() => (int)searcher.Search());
				tabPageSearchResults.Text = $"Результаты поиска: {totalFound}";
				if (totalFound > 0)
				{
					foreach (YouTubeSearcherV3ResultVideo v3Video in searcher.FoundVideos)
					{
						CreateAndAddNewFrame(v3Video.ToVideo(), null, false, false);
					}

					StackFrames();
					tabControlMain.SelectedTab = tabPageSearchResults;

					int remaining = framesVideo.Count;
					while (remaining > 0)
					{
						List<FrameYouTubeVideo> group = framesVideo.GetRange(framesVideo.Count - remaining,
							remaining > config.SimultaneousLoadThumbnailGroupSize ? config.SimultaneousLoadThumbnailGroupSize : remaining);
						var tasks = group.Select(item => Task.Run(() => item.DownloadAndSetVideoThumbnail()));
						await Task.Run(async () =>
						{
							await Task.WhenAll(tasks);
							remaining -= group.Count;
							if (remaining > 0 && config.IntervalBetweenThumbnailGroupsLoadMilliseconds > 0)
							{
								await Task.Delay(config.IntervalBetweenThumbnailGroupsLoadMilliseconds);
							}
						});
					}
				}
				else
				{
					MessageBox.Show("Ничего не найдено!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибатор ошибок",
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
				if (string.IsNullOrEmpty(config.YouTubeApiV3Key) || string.IsNullOrWhiteSpace(config.YouTubeApiV3Key))
				{
					MessageBox.Show("Для использования этой функции, необходимо ввести ключ от YouTube API V3! Так просто работать не будет!",
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
			btnWtfSearchApiV3.Enabled =
			btnSearchByWebPage.Enabled =
			btnSearchByVideoUrlOrId.Enabled =
			btnSearchByQuery.Enabled =
			textBoxSearchQuery.Enabled =
			textBoxVideoUrlOrId.Enabled =
			btnWtfWebPageCode.Enabled = enabled;
		}
	}
}
