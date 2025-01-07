using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace YouTube_downloader
{
	public sealed class Configurator
	{
		public string FilePath { get; private set; }
		public string SelfDirPath { get; set; }
		public string HomeDirPath { get; set; }
		public string DownloadingDirPath { get; set; }
		public string TempDirPath { get; set; }
		public string ChunksMergingDirPath { get; set; }
		public string FavoritesFilePath { get; set; }
		public string UserAgent { get; set; }
		public int ChunkDownloadRetryCountMax { get; set; }
		public int ChunkDownloadErrorCountMax { get; set; }
		public string OutputFileNameFormatWithDate { get; set; }
		public string OutputFileNameFormatWithoutDate { get; set; }
		public bool UseGmtTime { get; set; }
		public int MaxSearch { get; set; }
		public bool SortFormatsByFileSize { get; set; }
		public bool SortDashFormatsByBitrate { get; set; }
		public bool MoveAudioId140First { get; set; }
		public bool DownloadFirstAudioTrack { get; set; }
		public bool DownloadSecondAudioTrack { get; set; }
		public bool IfOnlySecondAudioTrackIsBetter { get; set; }
		public bool DownloadAllAudioTracks { get; set; }
		public bool DownloadAllAdaptiveVideoTracks { get; set; }
		public bool ShowHlsTracksOnlyForStreams { get; set; }
		public bool CheckUrlsAccessibilityBeforeDownloading { get; set; }
		public bool AlwaysDownloadAsDash { get; set; }
		public long DashManualFragmentationChunkSize { get; set; }
		public int DashDownloadRetryCountMax { get; set; }
		public bool UseRamToStoreTemporaryFiles { get; set; }
		public bool MergeToContainer { get; set; }
		public bool AlwaysUseMkvContainer { get; set; }
		public int ExtraDelayAfterContainerWasBuilt { get; set; }
		public bool DeleteSourceFiles { get; set; }
		public string CipherDecryptionAlgo { get; set; }
		public string YouTubeApiV3Key { get; set; }
		public string BrowserExeFilePath { get; set; }
		public string FfmpegExeFilePath { get; set; }
		public bool SavePreviewImage { get; set; }
		public bool UseExternalVideoInfoServerForAdultVideos { get; set; }
		public bool AlwaysUseExternalVideoInfoServer { get; set; }
		public string ExternalVideoInfoServerUrl { get; set; }
		public ushort ExternalVideoInfoServerPort { get; set; }
		public int VideoTitleFontSize { get; set; }
		public int MenusFontSize { get; set; }
		public int FavoritesListFontSize { get; set; }
		public int ThreadCountVideo { get; set; }
		public int ThreadCountAudio { get; set; }
		public int GlobalThreadCountMaximum { get; set; }
		public bool AccurateMultithreading { get; set; }
		public int ConnectionTimeout { get; set; }
		public int ConnectionTimeoutServer { get; set; }

		public const string DEFAULT_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:133.0) Gecko/20100101 Firefox/133.0";

		public delegate void SavingDelegate(object sender, JObject root);
		public delegate void LoadingDelegate(object sender, JObject root);
		public delegate void LoadedDelegate(object sender);
		public SavingDelegate Saving;
		public LoadingDelegate Loading;
		public LoadedDelegate Loaded;

		public Configurator(string fileName)
		{
			SelfDirPath = Path.GetDirectoryName(Application.ExecutablePath);
			bool useAppData = false;
			string[] args = Environment.GetCommandLineArgs();
			foreach (string arg in args)
			{
				if (!string.IsNullOrEmpty(arg) && arg.Equals("/standalone", StringComparison.OrdinalIgnoreCase))
				{
					useAppData = true;
					break;
				}
			}
			HomeDirPath = useAppData ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
				"\\YouTube downloader\\" : SelfDirPath + "\\";
			FavoritesFilePath = HomeDirPath + "fav.json";
			FilePath = HomeDirPath + fileName;

			LoadDefaults();
		}

		public void Save()
		{
			if (File.Exists(FilePath))
			{
				File.Delete(FilePath);
			}
			JObject json = new JObject();
			Saving?.Invoke(this, json);
			File.WriteAllText(FilePath, json.ToString());
		}

		private void LoadDefaults()
		{
			DownloadingDirPath = null;
			TempDirPath = null;
			ChunksMergingDirPath = null;
			ChunkDownloadRetryCountMax = 5;
			ChunkDownloadErrorCountMax = 3;
			MergeToContainer = true;
			AlwaysUseMkvContainer = false;
			ExtraDelayAfterContainerWasBuilt = 300;
			DeleteSourceFiles = true;
			CipherDecryptionAlgo = null;
			YouTubeApiV3Key = null;
			BrowserExeFilePath = null;
			FfmpegExeFilePath = "FFMPEG.EXE";
			OutputFileNameFormatWithDate = Utils.FILENAME_FORMAT_DEFAULT_WITH_DATE;
			OutputFileNameFormatWithoutDate = Utils.FILENAME_FORMAT_DEFAULT_WITHOUT_DATE;
			UseGmtTime = true;
			MaxSearch = 50;
			SortFormatsByFileSize = true;
			SortDashFormatsByBitrate = true;
			MoveAudioId140First = false;
			DownloadFirstAudioTrack = true;
			DownloadSecondAudioTrack = false;
			IfOnlySecondAudioTrackIsBetter = true;
			DownloadAllAudioTracks = false;
			DownloadAllAdaptiveVideoTracks = false;
			ShowHlsTracksOnlyForStreams = true;
			CheckUrlsAccessibilityBeforeDownloading = true;
			AlwaysDownloadAsDash = false;
			DashManualFragmentationChunkSize = 50000L;
			DashDownloadRetryCountMax = 8;
			SavePreviewImage = true;
			UseExternalVideoInfoServerForAdultVideos = false;
			AlwaysUseExternalVideoInfoServer = false;
			ExternalVideoInfoServerUrl = "http://127.0.0.1";
			ExternalVideoInfoServerPort = 12345;
			VideoTitleFontSize = 8;
			MenusFontSize = 10;
			FavoritesListFontSize = 8;
			ThreadCountVideo = 8;
			ThreadCountAudio = 4;
			GlobalThreadCountMaximum = 300;
			AccurateMultithreading = true;
			ConnectionTimeout = 5000;
			ConnectionTimeoutServer = 15000;
		}

		public void Load()
		{
			LoadDefaults();
			if (File.Exists(FilePath))
			{
				JObject json = Utils.TryParseJson(File.ReadAllText(FilePath));
				if (json != null)
				{
					Loading?.Invoke(this, json);
				}
			}
			Loaded?.Invoke(this);
		}
	}

}
