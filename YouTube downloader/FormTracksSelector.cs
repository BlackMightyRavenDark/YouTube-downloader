using System.Collections.Generic;
using System.Windows.Forms;

namespace YouTube_downloader
{
    public partial class FormTracksSelector : Form
    {
        private List<YouTubeMediaFile> Tracks;
        public List<YouTubeMediaFile> SelectedTracks { get; private set; } = new List<YouTubeMediaFile>();

        public FormTracksSelector(List<YouTubeMediaFile> mediaFiles)
        {
            InitializeComponent();

            SetupListView();

            Tracks = mediaFiles;
            List<TrackItem> root = new List<TrackItem>();
            foreach (YouTubeMediaFile mediaFile in mediaFiles)
            {
                if (!mediaFile.isContainer && mediaFile is YouTubeVideoFile)
                {
                    YouTubeVideoFile videoFile = mediaFile as YouTubeVideoFile;
                    string resolution = $"{videoFile.Width}x{videoFile.Height}";
                    int chunkCount = videoFile.dashManifestUrls != null ? videoFile.dashManifestUrls.Count : -1;
                    TrackItem trackItem = new TrackItem("Видео", resolution, videoFile.Fps,
                        videoFile.bitrate, videoFile.fileExtension, videoFile.contentLength, chunkCount, mediaFile);
                    root.Add(trackItem);
                }
            }
            foreach (YouTubeMediaFile mediaFile in mediaFiles)
            {
                if (mediaFile is YouTubeAudioFile)
                {
                    YouTubeAudioFile audioFile = mediaFile as YouTubeAudioFile;
                    int chunkCount = audioFile.dashManifestUrls != null ? audioFile.dashManifestUrls.Count : -1;
                    TrackItem trackItem = new TrackItem("Аудио", null, -1,
                        audioFile.bitrate, audioFile.fileExtension, audioFile.contentLength, chunkCount, mediaFile);
                    root.Add(trackItem);
                }
            }

            listViewTracksSelector.Objects = root.ToArray();
        }

        private void SetupListView()
        {
            olvColumnFrameRate.AspectToStringConverter = (obj) =>
            {
                int n = (int)obj;
                return n > 0 ? $"{n} fps" : null;
            };
            olvColumnBitrate.AspectToStringConverter = (obj) =>
            {
                int n = (int)obj;
                return n > 0 ? $"~{n / 1024} kbps" : "Не известно";
            };
            olvColumnFileExt.AspectToStringConverter = (obj) =>
            {
                if (obj == null)
                {
                    return null;
                }
                return (obj as string).ToUpper();
            };
            olvColumnFileSize.AspectToStringConverter = (obj) =>
            {
                long n = (long)obj;
                return n > 0 ? Utils.FormatSize(n) : "Не известно";
            };
            olvColumnChunkCount.AspectToStringConverter = (obj) =>
            {
                int n = (int)obj;
                return n >= 0 ? n.ToString() : null;
            };
        }

        private void btnDownload_Click(object sender, System.EventArgs e)
        {
            var items = listViewTracksSelector.CheckedObjects;
            if (items == null || items.Count == 0)
            {
                MessageBox.Show("Ничего не выбрано!", "Ошибатор ошибок",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (TrackItem item in items)
            {
                SelectedTracks.Add(item.Tag as YouTubeMediaFile);
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

    public sealed class TrackItem
    {
        public string TrackType { get; private set; }
        public string Resolution { get; private set; }
        public int FrameRate { get; private set; }
        public int Bitrate { get; private set; }
        public string FileExtension { get; private set; }
        public long FileSize { get; private set; }
        public int ChunkCount { get; private set; }
        public object Tag { get; private set; }

        public TrackItem(string trackType, string resolution, int frameRate, int bitrate,
            string fileExtension, long fileSize, int chunkCount, object tag)
        {
            TrackType = trackType;
            Resolution = resolution;
            FrameRate = frameRate;
            Bitrate = bitrate;
            FileExtension = fileExtension;
            FileSize = fileSize;
            ChunkCount = chunkCount;
            Tag = tag;
        }
    }
}
