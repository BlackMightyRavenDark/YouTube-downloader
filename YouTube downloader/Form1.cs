using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using YouTube_downloader.Properties;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Resize;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPageSearch;

            tvFavorites.ChildrenGetter = obj => { return ((FavoriteItem)obj).Children; };
            tvFavorites.ParentGetter = obj => { return ((FavoriteItem)obj).Parent; };
            tvFavorites.CanExpandGetter = obj => { return ((FavoriteItem)obj).Children.Count > 0; };
            tvFavorites.Roots = new List<FavoriteItem>() { new FavoriteItem("Избранное") };
            favoritesRootNode = tvFavorites.Roots.Cast<FavoriteItem>().ToArray()[0];
            treeFavorites = tvFavorites;

            if (File.Exists(config.favoritesFileName))
            {
                LoadFavorites(config.favoritesFileName);
                tvFavorites.Expand(favoritesRootNode);
            }

            config.Load();
            editDownloadingPath.Text = config.downloadingPath;
            editTempPath.Text = config.tempPath;
            editMergingPath.Text = config.chunksMergingPath;
            editCipherDecryptionAlgo.Text = config.cipherDecryptionAlgo;
            editYouTubeApiKey.Text = config.youTubeApiKey;
            editBrowser.Text = config.browserExe;
            editOutputFileNameFormat.Text = config.outputFileNameFormat;
            numericUpDownSearchResult.Value = config.maxSearch;
            editFfmpeg.Text = config.ffmpegExe;
            chkMergeAdaptive.Checked = config.mergeToContainer;
            chkDeleteSourceFiles.Checked = config.deleteSourceFiles;
            chkSaveImage.Checked = config.saveImagePreview;
            chkUseApiForGettingInfo.Checked = config.useApiForGettingInfo;
            numericUpDownThreadsVideo.Value = config.threadsVideo;
            numericUpDownThreadsAudio.Value = config.threadsAudio;
            numericUpDownGlobalThreadsMaximum.Value = config.globalThreadsMaximum;

            ServicePointManager.DefaultConnectionLimit = config.globalThreadsMaximum;

            dateTimePickerAfter.Value = DateTime.Now - TimeSpan.FromDays(30);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            config.Save();
            ClearChannels();
            ClearFramesChannel();
            ClearVideos();
            ClearFramesVideo();
            SaveFavorites("fav.json");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            StackFrames();
        }

        private void SaveNode(FavoriteItem root, ref JArray jsonArr)
        {
            JObject json = new JObject();
            json["displayName"] = root.DisplayName;
            if (root.Children.Count > 0) //directory
            {
                JArray ja = new JArray();
                for (int i = 0; i < root.Children.Count; i++)
                {
                    SaveNode(root.Children[i], ref ja);
                }
                json["type"] = "directory";
                json["subItems"] = ja;
            }
            else
            {
                json["title"] = root.Title;
                json["type"] = root.DataType == DATATYPE.DT_VIDEO ? "video" : "channel";
                json["id"] = root.ID;
                if (root.DataType == DATATYPE.DT_VIDEO)
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
            for (int i = 0; i < favoritesRootNode.Children.Count; i++)
            {
                SaveNode(favoritesRootNode.Children[i], ref ja);
            }
            JObject json = new JObject();
            json["items"] = ja;
            File.WriteAllText(fileName, json.ToString());
        }

        private void ParseDataItem(JObject item, ref FavoriteItem root)
        {
            string displayName = item.Value<string>("displayName");
            JToken jt = item.Value<JToken>("title");
            string title = jt != null ? jt.Value<string>() : displayName;
            FavoriteItem favoriteItem = new FavoriteItem(title, displayName, null, null, null, root);
            JArray ja = item.Value<JArray>("subItems");
            if (ja != null)
            {
                favoriteItem.DataType = DATATYPE.DT_DIRECTORY;
                for (int i = 0; i < ja.Count; i++)
                {
                    JObject j = JObject.Parse(ja[i].Value<JObject>().ToString());
                    ParseDataItem(j, ref favoriteItem);
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
                    favoriteItem.DataType = DATATYPE.DT_VIDEO;
                }
                else if (t.Equals("channel"))
                {
                    favoriteItem.DataType = DATATYPE.DT_CHANNEL;
                }
                else
                {
                    throw new ArgumentException("Недопустимое значение DataType: " + t);
                }
                favoriteItem.ID = item.Value<string>("id");
                if (favoriteItem.DataType == DATATYPE.DT_VIDEO)
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
            for (int i = 0; i < jItems.Count; i++)
            {
                JObject j = JObject.Parse(jItems[i].Value<JObject>().ToString());
                ParseDataItem(j, ref favoritesRootNode);
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
                string req = $"{YOUTUBE_SEARCH_BASE_URL}?part=snippet&key={config.youTubeApiKey}" +
                    $"&channelId={channelId}&maxResults=50&type=video&order=date";
                if (chkPublishedAfter.Checked)
                {
                    string dateAfter = dateTimePickerAfter.Value.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
                    req += $"&publishedAfter={dateAfter}";
                }

                if (chkPublishedBefore.Checked)
                {
                    string dateBefore = dateTimePickerBefore.Value.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
                    req += $"&publishedBefore={dateBefore}";
                }

                if (!string.IsNullOrEmpty(pageToken))
                {
                    req += $"&pageToken={pageToken}";
                }

                errorCode = DownloadString(req, out string buf);
                if (errorCode == 200)
                {
                    JObject json = JObject.Parse(buf);
                    JToken jt = json.Value<JToken>("nextPageToken");
                    pageToken = jt == null ? null : jt.Value<string>();
                    JArray jsonArr = json.Value<JArray>("items");
                    for (int i = 0; i < jsonArr.Count; i++)
                    {
                        JObject jObject = JObject.Parse(jsonArr[i].Value<JObject>().ToString());
                        string videoId = jObject.Value<JObject>("id").Value<string>("videoId");
                        if (GetYouTubeVideoInfoEx(videoId, out string info) == 200)
                        {
                            JObject j = JObject.Parse(info);
                            jaVideos.Add(j);
                        }
                        if (sum++ + 1 >= maxVideos)
                        {
                            break;
                        }
                        Application.DoEvents();
                    }
                }

                if (sum >= maxVideos)
                {
                    break;
                }
                Application.DoEvents();
            } while (errorCode == 200 && sum < maxVideos && !string.IsNullOrEmpty(pageToken));

            JObject jsonRes = new JObject();
            jsonRes.Add(new JProperty("videos", jaVideos));
            resJsonList = jsonRes.ToString();
            return jaVideos.Count;
        }

        public int SearchYouTube(string searchString, int maxResults, out string resList)
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

            string resultType = "type=video,channel";
            if (!chkSearchChannels.Checked)
            {
                resultType = resultType.Replace(",channel", string.Empty);
            }
            if (!chkSearchVideos.Checked)
            {
                resultType = resultType.Replace("video,", string.Empty);
            }

            string req = $"{YOUTUBE_SEARCH_BASE_URL}?part=snippet&key={config.youTubeApiKey}" +
                $"&q={searchString}&maxResults={maxResults}&{resultType}&order=date";

            if (chkPublishedAfter.Checked)
            {
                string dateAfter = dateTimePickerAfter.Value.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
                req += $"&publishedAfter={dateAfter}";
            }

            if (chkPublishedBefore.Checked)
            {
                string dateBefore = dateTimePickerBefore.Value.ToString("yyyy-MM-dd\"T\"HH:mm:ss\"Z\"");
                req += $"&publishedBefore={dateBefore}";
            }

            int errorCode = DownloadString(req, out string buf);
            if (errorCode == 200)
            {
                JObject json = JObject.Parse(buf);

                JArray jsonArr = json.Value<JArray>("items");

                for (int i = 0; i < jsonArr.Count(); i++)
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
                            if (GetYouTubeVideoInfoEx(id, out buf) == 200)
                            {
                                JObject j2 = JObject.Parse(buf);
                                jaVideos.Add(j2);
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
                //TODO: Implement error checking
                JArray ja = jt.Value<JArray>();
                for (int i = 0; i < ja.Count(); i++)
                {
                    YouTubeChannel youTubeChannel = new YouTubeChannel();
                    JObject jSnippet = ja[i].Value<JObject>("snippet");
                    youTubeChannel.title = jSnippet.Value<string>("title");
                    youTubeChannel.id = jSnippet.Value<string>("channelId");
                    youTubeChannel.imageUrl =
                        jSnippet.Value<JObject>("thumbnails").Value<JObject>("high").Value<string>("url");
                    youTubeChannel.imageStream = new MemoryStream();
                    DownloadData(youTubeChannel.imageUrl, youTubeChannel.imageStream);

                    FrameYouTubeChannel frame = new FrameYouTubeChannel();
                    frame.Parent = panelSearchResults;
                    frame.SetChannelInfo(ref youTubeChannel);
                    framesChannel.Add(frame);
                }
            }

            jt = json.Value<JToken>("videos");
            if (jt != null)
            {
                //TODO: Implement error checking
                JArray jaVideos = jt.Value<JArray>();
                for (int i = 0; i < jaVideos.Count; i++)
                {
                    JObject jVideo = jaVideos[i].Value<JObject>();
                    JObject jVideoDetails = jVideo.Value<JObject>("videoDetails");
                    YouTubeVideo video = new YouTubeVideo();
                    video.title = jVideoDetails.Value<string>("title");
                    video.id = jVideoDetails.Value<string>("videoId");
                    jt = jVideoDetails.Value<JToken>("lengthSeconds");
                    video.length = jt == null ? DateTime.MinValue :
                        (DateTime.MinValue + TimeSpan.FromSeconds(int.Parse(jt.Value<string>())));
                    video.channelOwned = new YouTubeChannel();
                    video.channelOwned.id = jVideoDetails.Value<string>("channelId");
                    video.channelOwned.title = jVideoDetails.Value<string>("author");
                    jt = jVideo.Value<JToken>("streamingData");
                    if (jt != null)
                    {
                        JObject jStreamingData = jt.Value<JObject>();
                        JToken jData = jStreamingData.Value<JToken>("adaptiveFormats");
                        if (jData == null)
                        {
                            jData = jStreamingData.Value<JToken>("formats");
                        }

                        JArray jArray = jData.Value<JArray>();
                        if (jArray.Count > 0)
                        {
                            video.ciphered = jArray[0].Value<JToken>("signatureCipher") != null;
                        }
                        video.dashed = jStreamingData.Value<JToken>("dashManifestUrl") != null;
                        video.hlsed = jStreamingData.Value<JToken>("hlsManifestUrl") != null;
                    }
                    JObject jMicroformatRenderer = jVideo.Value<JObject>("microformat").Value<JObject>("playerMicroformatRenderer");
                    StringToDateTime(jMicroformatRenderer.Value<string>("uploadDate"), out video.dateUploaded);
                    StringToDateTime(jMicroformatRenderer.Value<string>("publishDate"), out video.datePublished);
                    video.isFamilySafe = jMicroformatRenderer.Value<bool>("isFamilySafe");
                    video.isUnlisted = jMicroformatRenderer.Value<bool>("isUnlisted");
                    JArray jaThumbnails = jMicroformatRenderer.Value<JObject>("thumbnail").Value<JArray>("thumbnails");
                    for (int i2 = 0; i2 < jaThumbnails.Count; i2++)
                    {
                        string t = jaThumbnails[i2].Value<JObject>().Value<string>("url");
                        if (t.Contains("?"))
                        {
                            t = t.Substring(0, t.IndexOf("?"));
                        }
                        if (t.Contains("vi_webp"))
                        {
                            t = t.Replace("vi_webp", "vi").Replace(".webp", ".jpg");
                        }
                        video.imageUrls.Add(t);
                    }
                    video.imageStream = new MemoryStream();
                    if (DownloadData(video.imageUrls[video.imageUrls.Count - 1], video.imageStream))
                    {
                        video.imageStream.Position = 0;
                    }
                    videos.Add(video);

                    FrameYouTubeVideo frameVideo = new FrameYouTubeVideo();
                    frameVideo.Parent = panelSearchResults;
                    frameVideo.SetVideoInfo(ref video);
                    frameVideo.FavoriteChannelChanged += (s, id, newState) =>
                    {
                        for (int j = 0; j < framesVideo.Count; j++)
                        {
                            if (framesVideo[j].VideoInfo.channelOwned.id.Equals(id))
                            {
                                framesVideo[j].FavoriteChannel = newState;
                            }
                        }
                    };
                    frameVideo.Activated += event_FrameActivated;
                    frameVideo.OpenChannel += event_OpenChannel;
                    framesVideo.Add(frameVideo);
                }
            }
            return framesChannel.Count + framesVideo.Count;
        }

        private void ClearChannels()
        {
            foreach (YouTubeChannel channel in channels)
            {
                channel.imageStream?.Dispose();
            }
            channels.Clear();
        }

        private void ClearVideos()
        {
            foreach (YouTubeVideo video in videos)
            {
                video.channelOwned?.imageStream?.Dispose();
                video.imageStream?.Dispose();
            }
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
            for (int i = 0; i < framesChannel.Count(); i++)
            {
                framesChannel[i].Left = 0;
                framesChannel[i].Top = h - scrollBarSearchResults.Value;
                h += framesChannel[i].Height;
            }
            for (int i = 0; i < framesVideo.Count(); i++)
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
            if (string.IsNullOrEmpty(config.youTubeApiKey))
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
                tabControl1.SelectedTab = tabPageSearchResults;
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

            string videoId = ExtractVideoIdFromUrl(url);
            if (string.IsNullOrEmpty(videoId) || string.IsNullOrWhiteSpace(videoId))
            {
                MessageBox.Show("Не удалось распознать ID видео!", "Ошибатор ошибок",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                EnableControls();
                return;
            }

            if (videoId.Length != 11)
            {
                MessageBox.Show("Введённый вами или автоматически определённый ID видео " +
                    $"имеет длину {videoId.Length} символов. Такого не может быть!", "Ошибатор ошибок",
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

            int errorCode = SearchSingleVideo(videoId, out string resList);
            if (errorCode == 200)
            {
                int count = ParseList(resList);
                if (count > 0)
                {
                    StackFrames();
                }
                tabPageSearchResults.Text = $"Результаты поиска: {count}";
                tabControl1.SelectedTab = tabPageSearchResults;
                editSearchUrl.Text = null;
            }
            else
            {
                MessageBox.Show($"Ошибка {errorCode}\n{resList}", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            EnableControls();
        }

        private void btnSearchByWebPage_Click(object sender, EventArgs e)
        {
            DisableControls();

            string webPage = richTextBoxWebPage.Text;
            if (string.IsNullOrEmpty(webPage) || string.IsNullOrWhiteSpace(webPage))
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

            string videoInfo = ExtractVideoInfoFromWebPage(webPage);
            if (!string.IsNullOrEmpty(videoInfo))
            {
                JObject j = JObject.Parse(videoInfo);
                if (j != null)
                {
                    JArray jaVideos = new JArray();
                    jaVideos.Add(j);
                    JObject json = new JObject();
                    json.Add(new JProperty("videos", jaVideos));
                    int count = ParseList(json.ToString());
                    if (count > 0)
                    {
                        framesVideo[0].webPage = webPage;
                        StackFrames();
                    }
                    tabPageSearchResults.Text = $"Результаты поиска: {count}";
                    tabControl1.SelectedTab = tabPageSearchResults;
                }
                else
                {
                    MessageBox.Show("Ошибка парсинга!", "Ошибка!",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Ошибка парсинга!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            EnableControls();
        }

        private void btnWhy_Click(object sender, EventArgs e)
        {
            DisableControls();

            string msg = "Это позволит скачать скрытое, заблокированное или 18+ видео, " +
                "если у вас есть к нему доступ из браузера.";
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
            for (int iItem = items.Count - 1; iItem >= 0; iItem--)
            {
                for (int iChild = items[iItem].Children.Count - 1; iChild >= 0; iChild--)
                {
                    tvFavorites.RemoveObject(items[iItem].Children[iChild]);
                    items[iItem].Children.RemoveAt(iChild);
                }
                items.RemoveAt(iItem);
            }
        }

        private void tvFavorites_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
        {
            menuFavorites.Show(Cursor.Position);
        }

        private void tvFavorites_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && tvFavorites.SelectedObject != null)
            {
                FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
                if (item.DataType != DATATYPE.DT_DIRECTORY)
                {
                    DisableControls();

                    if (item.DataType == DATATYPE.DT_VIDEO)
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

                            int errorCode = SearchSingleVideo(item.ID, out string list);
                            if (errorCode == 200)
                            {
                                int count = ParseList(list);
                                if (count > 0)
                                {
                                    StackFrames();
                                    tabControl1.SelectedTab = tabPageSearchResults;
                                }
                                else
                                {
                                    scrollBarSearchResults.Enabled = false;
                                }
                                tabPageSearchResults.Text = $"Результаты поиска: {count}";
                            }
                            else
                            {
                                MessageBox.Show($"Ошибка {errorCode}\n{list}", "Ошибка!",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (item.DataType == DATATYPE.DT_CHANNEL)
                    {
                        if (MessageBox.Show($"Перейти на канал {item.DisplayName}?", "Переход на канал",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (string.IsNullOrEmpty(config.youTubeApiKey))
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
                tabControl1.SelectedTab = tabPageSearchResults;
            }
            else
            {
                MessageBox.Show("Ничего не найдено!", "Ошибка!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBrowseDowloadingPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Выберите папку для скачивания";
            folderBrowserDialog.SelectedPath =
                (!string.IsNullOrEmpty(config.downloadingPath) && Directory.Exists(config.downloadingPath)) ?
                config.downloadingPath : config.selfPath;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                config.downloadingPath =
                    folderBrowserDialog.SelectedPath.EndsWith("\\")
                    ? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
                editDownloadingPath.Text = config.downloadingPath;
            }
            folderBrowserDialog.Dispose();
        }

        private void btnBrowseTempPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Выберите папку для временных файлов";
            folderBrowserDialog.SelectedPath =
                (!string.IsNullOrEmpty(config.tempPath) && Directory.Exists(config.tempPath)) ?
                config.tempPath : config.selfPath;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                config.tempPath =
                    folderBrowserDialog.SelectedPath.EndsWith("\\")
                    ? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
                editTempPath.Text = config.tempPath;
            }
            folderBrowserDialog.Dispose();
        }

        private void btnSelectMergingPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.Description = "Выберите папку для объединения чанков";
            folderBrowserDialog.SelectedPath =
                (!string.IsNullOrEmpty(config.chunksMergingPath) && Directory.Exists(config.chunksMergingPath)) ?
                config.chunksMergingPath : config.selfPath;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                config.chunksMergingPath =
                    folderBrowserDialog.SelectedPath.EndsWith("\\")
                    ? folderBrowserDialog.SelectedPath : folderBrowserDialog.SelectedPath + "\\";
                editMergingPath.Text = config.chunksMergingPath;
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
                config.browserExe = ofd.FileName;
                editBrowser.Text = config.browserExe;
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
                config.ffmpegExe = ofd.FileName;
                editFfmpeg.Text = config.ffmpegExe;
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
                if (string.IsNullOrEmpty(config.youTubeApiKey))
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
            config.cipherDecryptionAlgo = editCipherDecryptionAlgo.Text;
        }

        private void chkMergeAdaptive_CheckedChanged(object sender, EventArgs e)
        {
            config.mergeToContainer = chkMergeAdaptive.Checked;
            chkDeleteSourceFiles.Enabled = config.mergeToContainer;
        }

        private void editDownloadingPath_Leave(object sender, EventArgs e)
        {
            config.downloadingPath = editDownloadingPath.Text;
        }

        private void editTempPath_Leave(object sender, EventArgs e)
        {
            config.tempPath = editTempPath.Text;
        }

        private void editYouTubeApiKey_Leave(object sender, EventArgs e)
        {
            config.youTubeApiKey = editYouTubeApiKey.Text;
        }

        private void rbSearchRessultsMax_Click(object sender, EventArgs e)
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
                OpenBrowser(YOUTUBE_VIDEO_URL_BASE + item.ID);
            }
        }

        private void openChannelInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FavoriteItem item = (FavoriteItem)tvFavorites.SelectedObject;
            if (item != null)
            {
                OpenBrowser(string.Format(YOUTUBE_CHANNEL_URL_TEMPLATE, item.ID));
            }
        }

        private void chkSaveImage_CheckedChanged(object sender, EventArgs e)
        {
            config.saveImagePreview = chkSaveImage.Checked;
        }

        private void editOutputFileNameFormat_Leave(object sender, EventArgs e)
        {
            config.outputFileNameFormat = editOutputFileNameFormat.Text;
        }

        private void btnResetFileNameFormat_Click(object sender, EventArgs e)
        {
            config.outputFileNameFormat = FILENAME_FORMAT_DEFAULT;
            editOutputFileNameFormat.Text = FILENAME_FORMAT_DEFAULT;
        }

        private void numericUpDownThreadsAudio_ValueChanged(object sender, EventArgs e)
        {
            config.threadsAudio = (int)numericUpDownThreadsAudio.Value;
            if (config.threadsAudio > 20)
            {
                if (config.threadsAudio > 50)
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
            config.threadsVideo = (int)numericUpDownThreadsVideo.Value;
            if (config.threadsVideo > 20)
            {
                if (config.threadsVideo > 50)
                {
                    toolTip1.SetToolTip(panelWarningVideoThreads, "Опасно! Перегрузка!");
                    if (config.threadsVideo <= 70)
                    {
                        panelWarningVideoThreads.BackgroundImage = Resources.fire;
                    }
                    else if (config.threadsVideo <= 80)
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
            config.globalThreadsMaximum = (int)numericUpDownGlobalThreadsMaximum.Value;
            ServicePointManager.DefaultConnectionLimit = config.globalThreadsMaximum;
        }

        private void chkUseApiForGettingInfo_Click(object sender, EventArgs e)
        {
            config.useApiForGettingInfo = chkUseApiForGettingInfo.Checked;
        }

        private void editFfmpeg_Leave(object sender, EventArgs e)
        {
            config.ffmpegExe = editFfmpeg.Text;
        }

        private void editMergingPath_Leave(object sender, EventArgs e)
        {
            config.chunksMergingPath = editMergingPath.Text;
        }

        private void chkDeleteSourceFiles_CheckedChanged(object sender, EventArgs e)
        {
            config.deleteSourceFiles = chkDeleteSourceFiles.Checked;
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
            config.maxSearch = (int)numericUpDownSearchResult.Value;
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
                switch (item.DataType)
                {
                    case DATATYPE.DT_VIDEO:
                        openVideoInBrowserToolStripMenuItem.Visible = true;
                        openChannelInBrowserToolStripMenuItem.Visible = false;
                        copyDisplayNameWithIdToolStripMenuItem.Visible = true;
                        break;

                    case DATATYPE.DT_CHANNEL:
                        openVideoInBrowserToolStripMenuItem.Visible = false;
                        openChannelInBrowserToolStripMenuItem.Visible = true;
                        copyDisplayNameWithIdToolStripMenuItem.Visible = true;
                        break;

                    case DATATYPE.DT_DIRECTORY:
                        openVideoInBrowserToolStripMenuItem.Visible = false;
                        openChannelInBrowserToolStripMenuItem.Visible = false;
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
            if (item != null)
            {
                if (item.DataType != DATATYPE.DT_DIRECTORY)
                {
                    SetClipboardText($"{item.DisplayName} [{item.ID}]");
                }
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
        }

        private void EnableControls()
        {
            btnSearchByWebPage.Enabled = true;
            btnSearchByUrl.Enabled = true;
            btnSearchByQuery.Enabled = true;
            editQuery.Enabled = true;
            editSearchUrl.Enabled = true;
            btnWhy.Enabled = true;
        }
    }
}
