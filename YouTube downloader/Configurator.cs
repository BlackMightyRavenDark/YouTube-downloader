using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace YouTube_downloader
{
	public sealed class Configurator
	{
		public string FilePath { get; private set; }
		public string SelfDirectory { get; set; }
		public string HomeDirectory { get; set; }
		public string FavoritesFilePath { get; set; }

		#region Files and folders options
		public string DownloadDirectory { get; set; }
		public string TemporaryDirectory { get; set; }
		public string ChunkMergerDirectory { get; set; }
		public string OutputFileNameFormatWithDate { get; set; }
		public string OutputFileNameFormatWithoutDate { get; set; }
		public string WebBrowserExeFilePath { get; set; }
		public string FfmpegExeFilePath { get; set; }
		#endregion

		#region GUI options
		public int VideoTitleFontSize { get; set; }
		public int MenusFontSize
		{
			get => _menusFontSize;
			set { if (_menusFontSize != value) { _menusFontSize = value; MenusFontSizeChanged?.Invoke(this, value); } }
		}
		public int FavoritesListFontSize
		{
			get => _favoritesListFontSize;
			set { if (_favoritesListFontSize != value) { _favoritesListFontSize = value; FavoritesListFontSizeChanged?.Invoke(this, value); } }
		}
		public bool SortFormatsByFileSize { get; set; }
		public bool SortDashFormatsByBitrate { get; set; }
		public bool AlwaysMoveAudioId140ToTopOfList { get; set; }
		public bool ShowHlsTracksOnlyForStreams { get; set; }
		#endregion

		#region Download options
		public bool AutomaticallyDownloadAllAdaptiveVideoTracks { get; set; }
		public bool AutomaticallyDownloadFirstAudioTrack { get; set; }
		public bool AutomaticallyDownloadSecondAudioTrack { get; set; }
		public bool AutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger { get; set; }
		public bool AutomaticallyDownloadAllAdaptiveAudioTracks { get; set; }
		public bool AutomaticallyMergeToContainer { get; set; }
		public bool DeleteSourceFilesWhenMerged { get; set; }
		public bool AlwaysUseMkvContainerIfPossible { get; set; }
		public int ExtraDelayAfterContainerWasBuilt { get; set; }
		public bool CheckUrlsAccessibilityBeforeDownloadStarted { get; set; }
		public int ChunkDownloadTryCountLimit { get; set; }
		public int ChunkDownloadInnerErrorCountLimit { get; set; }
		public bool AutomaticallySaveVideoThumbnailImage { get; set; }
		#endregion

		#region System options
		public string CipherDecryptionAlgorythm { get; set; }
		public string YouTubeApiV3Key { get; set; }
		public string ExternalRestApiServerUrl { get; set; }
		public ushort ExternalRestApiServerPort { get; set; }
		public int ConnectionTimeoutExternalRestApiServer { get; set; }
		public bool UseExternalRestApiServerToGetBasicVideoInfo { get; set; }
		public bool UseExternalRestApiServerToGetDownloadUrls { get; set; }
		public bool UseExternalRestApiServerToGetAdultVideos { get; set; }
		public string UserAgent { get; set; }
		public int ThreadCountVideo { get; set; }
		public int ThreadCountAudio { get; set; }
		public int GlobalThreadCountMaximum { get; set; }
		public bool UseAccurateMultithreading { get; set; }
		public int ConnectionTimeout { get; set; }
		public bool UseRamToStoreTemporaryFiles { get; set; }
		public bool AlwaysDownloadAsDash { get; set; }
		public long DashManualFragmentationChunkSize { get; set; }
		public int DashChunkDownloadTryCountLimit { get; set; }
		public bool UseUniversalTime { get; set; }
		#endregion

		#region Other options
		public int MaximumSearchResults { get; set; }
		#endregion

		private int _menusFontSize;
		private int _favoritesListFontSize;

		public const string DEFAULT_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:133.0) Gecko/20100101 Firefox/133.0";
		public const string FILENAME_FORMAT_DEFAULT_WITH_DATE =
			"[<year>-<month>-<day> <hour>-<minute>-<second><GMT>] <video_title> (id_<video_id>)";
		public const string FILENAME_FORMAT_DEFAULT_WITHOUT_DATE = "<video_title> (id_<video_id>)";

		public delegate void SavingDelegate(object sender, JObject root);
		public delegate void LoadingDelegate(object sender, JObject root);
		public delegate void LoadedDelegate(object sender);
		public delegate void MenusFontSizeChangedDelegate(object sender, int newSize);
		public delegate void FavoritesListFontSizeChangedDelegate(object sender, int newSize);
		public SavingDelegate Saving;
		public LoadingDelegate Loading;
		public LoadedDelegate Loaded;
		public MenusFontSizeChangedDelegate MenusFontSizeChanged;
		public FavoritesListFontSizeChangedDelegate FavoritesListFontSizeChanged;

		public Configurator()
		{
			string selfExePath = Application.ExecutablePath;
			string selfFileName = Path.GetFileName(selfExePath);
			SelfDirectory = Path.GetDirectoryName(selfExePath);
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
			HomeDirectory = (useAppData ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"YouTube downloader") : SelfDirectory) + "\\";
			FavoritesFilePath = HomeDirectory + "fav.json";
			FilePath = HomeDirectory + (useAppData ? "config_ytdl.json" : (Path.GetFileNameWithoutExtension(selfFileName) + "_config.json"));

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
			#region Files and folders options
			DownloadDirectory = null;
			TemporaryDirectory = null;
			ChunkMergerDirectory = null;
			OutputFileNameFormatWithDate = FILENAME_FORMAT_DEFAULT_WITH_DATE;
			OutputFileNameFormatWithoutDate = FILENAME_FORMAT_DEFAULT_WITHOUT_DATE;
			WebBrowserExeFilePath = null;
			FfmpegExeFilePath = "FFMPEG.EXE";
			#endregion

			#region GUI options
			VideoTitleFontSize = 8;
			MenusFontSize = 10;
			FavoritesListFontSize = 8;
			SortFormatsByFileSize = true;
			SortDashFormatsByBitrate = true;
			AlwaysMoveAudioId140ToTopOfList = false;
			ShowHlsTracksOnlyForStreams = true;
			#endregion

			#region Download options
			AutomaticallyDownloadAllAdaptiveVideoTracks = false;
			AutomaticallyDownloadFirstAudioTrack = true;
			AutomaticallyDownloadSecondAudioTrack = false;
			AutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger = true;
			AutomaticallyDownloadAllAdaptiveAudioTracks = false;
			AutomaticallyMergeToContainer = true;
			DeleteSourceFilesWhenMerged = true;
			AlwaysUseMkvContainerIfPossible = false;
			ExtraDelayAfterContainerWasBuilt = 300;
			CheckUrlsAccessibilityBeforeDownloadStarted = true;
			ChunkDownloadTryCountLimit = 5;
			ChunkDownloadInnerErrorCountLimit = 3;
			AutomaticallySaveVideoThumbnailImage = true;
			#endregion

			#region System options
			CipherDecryptionAlgorythm = null;
			YouTubeApiV3Key = null;
			ExternalRestApiServerUrl = "http://127.0.0.1";
			ExternalRestApiServerPort = 12345;
			ConnectionTimeoutExternalRestApiServer = 15000;
			UseExternalRestApiServerToGetBasicVideoInfo = false;
			UseExternalRestApiServerToGetDownloadUrls = false;
			UseExternalRestApiServerToGetAdultVideos = false;
			UserAgent = DEFAULT_USER_AGENT;
			ThreadCountVideo = 8;
			ThreadCountAudio = 4;
			GlobalThreadCountMaximum = 300;
			UseAccurateMultithreading = true;
			ConnectionTimeout = 5000;
			UseRamToStoreTemporaryFiles = false;
			AlwaysDownloadAsDash = false;
			DashManualFragmentationChunkSize = 50000L;
			DashChunkDownloadTryCountLimit = 8;
			UseUniversalTime = true;
			#endregion

			#region Other options
			MaximumSearchResults = 50;
			#endregion
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
