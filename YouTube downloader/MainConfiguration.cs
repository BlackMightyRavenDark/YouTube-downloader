using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace YouTube_downloader
{
	public sealed class MainConfiguration
	{
		public string FilePath { get; private set; }
		public string SelfDirPath { get; set; }
		public string HomeDirPath { get; set; }
		public string DownloadingDirPath { get; set; }
		public string TempDirPath { get; set; }
		public string ChunksMergingDirPath { get; set; }
		public string FavoritesFilePath { get; set; }
		public int DownloadRetryCount { get; set; }
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
		public bool UseHiddenApiForGettingInfo { get; set; }
		public int VideoTitleFontSize { get; set; }
		public int MenusFontSize { get; set; }
		public int FavoritesListFontSize { get; set; }
		public int ThreadCountVideo { get; set; }
		public int ThreadCountAudio { get; set; }
		public int GlobalThreadCountMaximum { get; set; }

		public delegate void SavingDelegate(object sender, JObject root);
		public delegate void LoadingDelegate(object sender, JObject root);
		public delegate void LoadedDelegate(object sender);
		public SavingDelegate Saving;
		public LoadingDelegate Loading;
		public LoadedDelegate Loaded;

		public MainConfiguration(string fileName)
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
			DownloadRetryCount = 5;
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
			AlwaysDownloadAsDash = false;
			DashManualFragmentationChunkSize = 50000L;
			DashDownloadRetryCountMax = 8;
			SavePreviewImage = true;
			UseHiddenApiForGettingInfo = true;
			VideoTitleFontSize = 8;
			MenusFontSize = 9;
			FavoritesListFontSize = 8;
			ThreadCountVideo = 8;
			ThreadCountAudio = 4;
			GlobalThreadCountMaximum = 300;
		}

		public void Load()
		{
			LoadDefaults();
			if (File.Exists(FilePath))
			{
				JObject json = JObject.Parse(File.ReadAllText(FilePath));
				if (json != null)
				{
					Loading?.Invoke(this, json);
				}
			}
			Loaded?.Invoke(this);
		}
	}

}
