
namespace YouTube_downloader
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.tabControlSettings = new System.Windows.Forms.TabControl();
            this.tabPageFilesAndFolders = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxOutputFileNameFormatWithDate = new System.Windows.Forms.TextBox();
            this.btnResetFileNameFormatWithDate = new System.Windows.Forms.Button();
            this.btnWtfMergerDirectory = new System.Windows.Forms.Button();
            this.textBoxFilesMergerDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseMergerDirectory = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnBrowseFfmpegFilePath = new System.Windows.Forms.Button();
            this.textBoxFfmpegFilePath = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxOutputFileNameFormatWithoutDate = new System.Windows.Forms.TextBox();
            this.btnResetFileNameFormatWithoutDate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBrowseDownloadDirectory = new System.Windows.Forms.Button();
            this.btnSelectWebBrowserFilePath = new System.Windows.Forms.Button();
            this.textBoxDownloadDirectory = new System.Windows.Forms.TextBox();
            this.textBoxWebBrowserFilePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTempDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseTempDirectory = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPageGUI = new System.Windows.Forms.TabPage();
            this.checkBoxShowHlsTracksOnlyForStreams = new System.Windows.Forms.CheckBox();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.checkBoxSortDashFormatsByBitrate = new System.Windows.Forms.CheckBox();
            this.checkBoxMoveAudioTrackId140ToTopOfList = new System.Windows.Forms.CheckBox();
            this.checkBoxSortAdaptiveFormatsByFileSize = new System.Windows.Forms.CheckBox();
            this.groupBoxFontSettings = new System.Windows.Forms.GroupBox();
            this.numericUpDownVideoTitleFontSize = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownFavoritesListFontSize = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDownMenusFontSize = new System.Windows.Forms.NumericUpDown();
            this.tabPageDownloadSettings = new System.Windows.Forms.TabPage();
            this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted = new System.Windows.Forms.CheckBox();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.numericUpDownChunkDownloadErrorCountLimit = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.numericUpDownChunkDownloadTryCountLimit = new System.Windows.Forms.NumericUpDown();
            this.groupBoxAdaptiveFormatsSettings = new System.Windows.Forms.GroupBox();
            this.numericUpDownDelayAfterContainerCreated = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnWtfDownloadAllAdaptiveVideoTracks = new System.Windows.Forms.Button();
            this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks = new System.Windows.Forms.CheckBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.radioButtonContainerTypeMkv = new System.Windows.Forms.RadioButton();
            this.radioButtonContainerTypeMp4 = new System.Windows.Forms.RadioButton();
            this.checkBoxAutomaticallyMergeAdaptiveTracks = new System.Windows.Forms.CheckBox();
            this.checkBoxDeleteSourceFilesWhenMerged = new System.Windows.Forms.CheckBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.checkBoxAutomaticallyDownloadFirstAudioTrack = new System.Windows.Forms.CheckBox();
            this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger = new System.Windows.Forms.CheckBox();
            this.checkBoxAutomaticallyDownloadSecondAudioTrack = new System.Windows.Forms.CheckBox();
            this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks = new System.Windows.Forms.CheckBox();
            this.checkBoxAutomaticallySaveVideoThumbnailImage = new System.Windows.Forms.CheckBox();
            this.tabPageSystemSettings = new System.Windows.Forms.TabPage();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.btnWtfUserAgent = new System.Windows.Forms.Button();
            this.btnRestoreDefaultUserAgent = new System.Windows.Forms.Button();
            this.textBoxUserAgent = new System.Windows.Forms.TextBox();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.numericUpDownDashChunkDownloadTryCountLimit = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.btnWtfAlwaysDownloadAsDash = new System.Windows.Forms.Button();
            this.lblActualDashChunkSize = new System.Windows.Forms.Label();
            this.numericUpDownDashChunkSize = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.checkBoxAlwaysDownloadAsDash = new System.Windows.Forms.CheckBox();
            this.checkBoxUseUniversalTime = new System.Windows.Forms.CheckBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btnWtfUseRam = new System.Windows.Forms.Button();
            this.panelRAM = new System.Windows.Forms.Panel();
            this.checkBoxUseRamForTempFiles = new System.Windows.Forms.CheckBox();
            this.groupBoxYouTubeApiSettings = new System.Windows.Forms.GroupBox();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.checkBoxUseExternalRestApiServerToGetDownloadUrls = new System.Windows.Forms.CheckBox();
            this.checkBoxUseExternalRestApiServerToGetBasicVideoInfo = new System.Windows.Forms.CheckBox();
            this.checkBoxUseExternalRestApiServerToGetAdultVideos = new System.Windows.Forms.CheckBox();
            this.numericUpDownConnectionTimeoutExternalRestApiServer = new System.Windows.Forms.NumericUpDown();
            this.label30 = new System.Windows.Forms.Label();
            this.btnWtfExternalRestApiServer = new System.Windows.Forms.Button();
            this.label25 = new System.Windows.Forms.Label();
            this.numericUpDownExternalRestApiServerPort = new System.Windows.Forms.NumericUpDown();
            this.textBoxExternalRestApiServerUrl = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxCipherDecryptionAlgorythm = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxYouTubeApiV3Key = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.numericUpDownConnectionTimeout = new System.Windows.Forms.NumericUpDown();
            this.label29 = new System.Windows.Forms.Label();
            this.checkBoxUseAccurateMultithreading = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownGlobalThreadCountLimit = new System.Windows.Forms.NumericUpDown();
            this.panelWarningAudioThreads = new System.Windows.Forms.Panel();
            this.panelWarningVideoThreads = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownThreadCountAudio = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownThreadCountVideo = new System.Windows.Forms.NumericUpDown();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnWtfWebPageCode = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSearchByWebPage = new System.Windows.Forms.Button();
            this.richTextBoxWebPageCode = new System.Windows.Forms.RichTextBox();
            this.contextMenuCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCutTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miPasteTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSeparatorLine = new System.Windows.Forms.ToolStripSeparator();
            this.miSelectAllTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerSearchBefore = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerSearchAfter = new System.Windows.Forms.DateTimePicker();
            this.checkBoxSearchRangePublishedAfter = new System.Windows.Forms.CheckBox();
            this.checkBoxSearchRangePublishedBefore = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.checkBoxSearchVideos = new System.Windows.Forms.CheckBox();
            this.checkBoxSearchChannels = new System.Windows.Forms.CheckBox();
            this.groupBoxQuerySearch = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearchByQuery = new System.Windows.Forms.Button();
            this.textBoxSearchQuery = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButtonSearchResultCountLimitUserDefinedNumber = new System.Windows.Forms.RadioButton();
            this.radioButtonSearchResultCountLimitMaxPossible = new System.Windows.Forms.RadioButton();
            this.numericUpDownSearchResultCountLimit = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxVideoUrlOrId = new System.Windows.Forms.TextBox();
            this.btnSearchByVideoUrlOrId = new System.Windows.Forms.Button();
            this.tabPageSearchResults = new System.Windows.Forms.TabPage();
            this.scrollBarSearchResults = new System.Windows.Forms.VScrollBar();
            this.panelSearchResults = new System.Windows.Forms.Panel();
            this.objectTreeViewFavorites = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuFavorites = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miOpenVideoInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyVideoUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenChannelInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyChannelUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyDisplayNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyVideoIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyChannelIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyDisplayNameWithIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlMain.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabControlSettings.SuspendLayout();
            this.tabPageFilesAndFolders.SuspendLayout();
            this.tabPageGUI.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBoxFontSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoTitleFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFavoritesListFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMenusFontSize)).BeginInit();
            this.tabPageDownloadSettings.SuspendLayout();
            this.groupBox19.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChunkDownloadErrorCountLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChunkDownloadTryCountLimit)).BeginInit();
            this.groupBoxAdaptiveFormatsSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayAfterContainerCreated)).BeginInit();
            this.groupBox15.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.tabPageSystemSettings.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.groupBox17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDashChunkDownloadTryCountLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDashChunkSize)).BeginInit();
            this.groupBox13.SuspendLayout();
            this.groupBoxYouTubeApiSettings.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox21.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConnectionTimeoutExternalRestApiServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExternalRestApiServerPort)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConnectionTimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGlobalThreadCountLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadCountAudio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadCountVideo)).BeginInit();
            this.tabPageSearch.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.contextMenuCopyPaste.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBoxQuerySearch.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSearchResultCountLimit)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabPageSearchResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectTreeViewFavorites)).BeginInit();
            this.contextMenuFavorites.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageSettings);
            this.tabControlMain.Controls.Add(this.tabPageSearch);
            this.tabControlMain.Controls.Add(this.tabPageSearchResults);
            this.tabControlMain.Location = new System.Drawing.Point(12, 12);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(555, 420);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageSettings
            // 
            this.tabPageSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageSettings.Controls.Add(this.tabControlSettings);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(547, 394);
            this.tabPageSettings.TabIndex = 0;
            this.tabPageSettings.Text = "Настройки";
            // 
            // tabControlSettings
            // 
            this.tabControlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlSettings.Controls.Add(this.tabPageFilesAndFolders);
            this.tabControlSettings.Controls.Add(this.tabPageGUI);
            this.tabControlSettings.Controls.Add(this.tabPageDownloadSettings);
            this.tabControlSettings.Controls.Add(this.tabPageSystemSettings);
            this.tabControlSettings.Location = new System.Drawing.Point(6, 8);
            this.tabControlSettings.Name = "tabControlSettings";
            this.tabControlSettings.SelectedIndex = 0;
            this.tabControlSettings.Size = new System.Drawing.Size(535, 383);
            this.tabControlSettings.TabIndex = 12;
            // 
            // tabPageFilesAndFolders
            // 
            this.tabPageFilesAndFolders.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPageFilesAndFolders.Controls.Add(this.label18);
            this.tabPageFilesAndFolders.Controls.Add(this.textBoxOutputFileNameFormatWithDate);
            this.tabPageFilesAndFolders.Controls.Add(this.btnResetFileNameFormatWithDate);
            this.tabPageFilesAndFolders.Controls.Add(this.btnWtfMergerDirectory);
            this.tabPageFilesAndFolders.Controls.Add(this.textBoxFilesMergerDirectory);
            this.tabPageFilesAndFolders.Controls.Add(this.btnBrowseMergerDirectory);
            this.tabPageFilesAndFolders.Controls.Add(this.label14);
            this.tabPageFilesAndFolders.Controls.Add(this.label11);
            this.tabPageFilesAndFolders.Controls.Add(this.btnBrowseFfmpegFilePath);
            this.tabPageFilesAndFolders.Controls.Add(this.textBoxFfmpegFilePath);
            this.tabPageFilesAndFolders.Controls.Add(this.label8);
            this.tabPageFilesAndFolders.Controls.Add(this.textBoxOutputFileNameFormatWithoutDate);
            this.tabPageFilesAndFolders.Controls.Add(this.btnResetFileNameFormatWithoutDate);
            this.tabPageFilesAndFolders.Controls.Add(this.label7);
            this.tabPageFilesAndFolders.Controls.Add(this.btnBrowseDownloadDirectory);
            this.tabPageFilesAndFolders.Controls.Add(this.btnSelectWebBrowserFilePath);
            this.tabPageFilesAndFolders.Controls.Add(this.textBoxDownloadDirectory);
            this.tabPageFilesAndFolders.Controls.Add(this.textBoxWebBrowserFilePath);
            this.tabPageFilesAndFolders.Controls.Add(this.label3);
            this.tabPageFilesAndFolders.Controls.Add(this.textBoxTempDirectory);
            this.tabPageFilesAndFolders.Controls.Add(this.btnBrowseTempDirectory);
            this.tabPageFilesAndFolders.Controls.Add(this.label4);
            this.tabPageFilesAndFolders.Location = new System.Drawing.Point(4, 22);
            this.tabPageFilesAndFolders.Name = "tabPageFilesAndFolders";
            this.tabPageFilesAndFolders.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFilesAndFolders.Size = new System.Drawing.Size(527, 357);
            this.tabPageFilesAndFolders.TabIndex = 0;
            this.tabPageFilesAndFolders.Text = "Файлы и папки";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(10, 126);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(217, 13);
            this.label18.TabIndex = 25;
            this.label18.Text = "Формат имени файла (дата определена):";
            // 
            // textBoxOutputFileNameFormatWithDate
            // 
            this.textBoxOutputFileNameFormatWithDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputFileNameFormatWithDate.Location = new System.Drawing.Point(13, 142);
            this.textBoxOutputFileNameFormatWithDate.Name = "textBoxOutputFileNameFormatWithDate";
            this.textBoxOutputFileNameFormatWithDate.Size = new System.Drawing.Size(392, 20);
            this.textBoxOutputFileNameFormatWithDate.TabIndex = 24;
            this.textBoxOutputFileNameFormatWithDate.TextChanged += new System.EventHandler(this.textBoxOutputFileNameFormatWithDate_TextChanged);
            // 
            // btnResetFileNameFormatWithDate
            // 
            this.btnResetFileNameFormatWithDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetFileNameFormatWithDate.Location = new System.Drawing.Point(411, 142);
            this.btnResetFileNameFormatWithDate.Name = "btnResetFileNameFormatWithDate";
            this.btnResetFileNameFormatWithDate.Size = new System.Drawing.Size(110, 20);
            this.btnResetFileNameFormatWithDate.TabIndex = 23;
            this.btnResetFileNameFormatWithDate.Text = "Вернуть как было";
            this.btnResetFileNameFormatWithDate.UseVisualStyleBackColor = true;
            this.btnResetFileNameFormatWithDate.Click += new System.EventHandler(this.btnResetFileNameFormatWithDate_Click);
            // 
            // btnWtfMergerDirectory
            // 
            this.btnWtfMergerDirectory.Location = new System.Drawing.Point(182, 84);
            this.btnWtfMergerDirectory.Name = "btnWtfMergerDirectory";
            this.btnWtfMergerDirectory.Size = new System.Drawing.Size(21, 19);
            this.btnWtfMergerDirectory.TabIndex = 22;
            this.btnWtfMergerDirectory.Text = "?";
            this.btnWtfMergerDirectory.UseVisualStyleBackColor = true;
            this.btnWtfMergerDirectory.Click += new System.EventHandler(this.btnWtfMergerDirectory_Click);
            // 
            // textBoxFilesMergerDirectory
            // 
            this.textBoxFilesMergerDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilesMergerDirectory.Location = new System.Drawing.Point(13, 103);
            this.textBoxFilesMergerDirectory.Name = "textBoxFilesMergerDirectory";
            this.textBoxFilesMergerDirectory.Size = new System.Drawing.Size(470, 20);
            this.textBoxFilesMergerDirectory.TabIndex = 21;
            this.textBoxFilesMergerDirectory.Leave += new System.EventHandler(this.textBoxFilesMergerDirectory_Leave);
            // 
            // btnBrowseMergerDirectory
            // 
            this.btnBrowseMergerDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseMergerDirectory.Location = new System.Drawing.Point(489, 103);
            this.btnBrowseMergerDirectory.Name = "btnBrowseMergerDirectory";
            this.btnBrowseMergerDirectory.Size = new System.Drawing.Size(32, 23);
            this.btnBrowseMergerDirectory.TabIndex = 20;
            this.btnBrowseMergerDirectory.Text = "...";
            this.btnBrowseMergerDirectory.UseVisualStyleBackColor = true;
            this.btnBrowseMergerDirectory.Click += new System.EventHandler(this.btnSelectMergerDirectory_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 87);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(171, 13);
            this.label14.TabIndex = 19;
            this.label14.Text = "Папка для объединения чанков:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 243);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Путь к FFMPEG.EXE:";
            // 
            // btnBrowseFfmpegFilePath
            // 
            this.btnBrowseFfmpegFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFfmpegFilePath.Location = new System.Drawing.Point(489, 259);
            this.btnBrowseFfmpegFilePath.Name = "btnBrowseFfmpegFilePath";
            this.btnBrowseFfmpegFilePath.Size = new System.Drawing.Size(32, 20);
            this.btnBrowseFfmpegFilePath.TabIndex = 17;
            this.btnBrowseFfmpegFilePath.Text = "...";
            this.btnBrowseFfmpegFilePath.UseVisualStyleBackColor = true;
            this.btnBrowseFfmpegFilePath.Click += new System.EventHandler(this.btnBrowseFfmpegFilePath_Click);
            // 
            // textBoxFfmpegFilePath
            // 
            this.textBoxFfmpegFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFfmpegFilePath.Location = new System.Drawing.Point(13, 259);
            this.textBoxFfmpegFilePath.Name = "textBoxFfmpegFilePath";
            this.textBoxFfmpegFilePath.Size = new System.Drawing.Size(470, 20);
            this.textBoxFfmpegFilePath.TabIndex = 16;
            this.textBoxFfmpegFilePath.Leave += new System.EventHandler(this.textBoxFfmpegFilePath_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(232, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Формат имени файла (дата не определена):";
            // 
            // textBoxOutputFileNameFormatWithoutDate
            // 
            this.textBoxOutputFileNameFormatWithoutDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputFileNameFormatWithoutDate.Location = new System.Drawing.Point(13, 181);
            this.textBoxOutputFileNameFormatWithoutDate.Name = "textBoxOutputFileNameFormatWithoutDate";
            this.textBoxOutputFileNameFormatWithoutDate.Size = new System.Drawing.Size(392, 20);
            this.textBoxOutputFileNameFormatWithoutDate.TabIndex = 14;
            this.textBoxOutputFileNameFormatWithoutDate.TextChanged += new System.EventHandler(this.textBoxOutputFileNameFormatWithoutDate_TextChanged);
            // 
            // btnResetFileNameFormatWithoutDate
            // 
            this.btnResetFileNameFormatWithoutDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetFileNameFormatWithoutDate.Location = new System.Drawing.Point(411, 181);
            this.btnResetFileNameFormatWithoutDate.Name = "btnResetFileNameFormatWithoutDate";
            this.btnResetFileNameFormatWithoutDate.Size = new System.Drawing.Size(110, 20);
            this.btnResetFileNameFormatWithoutDate.TabIndex = 13;
            this.btnResetFileNameFormatWithoutDate.Text = "Вернуть как было";
            this.btnResetFileNameFormatWithoutDate.UseVisualStyleBackColor = true;
            this.btnResetFileNameFormatWithoutDate.Click += new System.EventHandler(this.btnResetFileNameFormatWithoutDate_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 204);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Веб-браузер:";
            // 
            // btnBrowseDownloadDirectory
            // 
            this.btnBrowseDownloadDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseDownloadDirectory.Location = new System.Drawing.Point(489, 23);
            this.btnBrowseDownloadDirectory.Name = "btnBrowseDownloadDirectory";
            this.btnBrowseDownloadDirectory.Size = new System.Drawing.Size(32, 20);
            this.btnBrowseDownloadDirectory.TabIndex = 2;
            this.btnBrowseDownloadDirectory.Text = "...";
            this.btnBrowseDownloadDirectory.UseVisualStyleBackColor = true;
            this.btnBrowseDownloadDirectory.Click += new System.EventHandler(this.btnBrowseDownloadDirectory_Click);
            // 
            // btnSelectWebBrowserFilePath
            // 
            this.btnSelectWebBrowserFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectWebBrowserFilePath.Location = new System.Drawing.Point(489, 220);
            this.btnSelectWebBrowserFilePath.Name = "btnSelectWebBrowserFilePath";
            this.btnSelectWebBrowserFilePath.Size = new System.Drawing.Size(32, 20);
            this.btnSelectWebBrowserFilePath.TabIndex = 11;
            this.btnSelectWebBrowserFilePath.Text = "...";
            this.btnSelectWebBrowserFilePath.UseVisualStyleBackColor = true;
            this.btnSelectWebBrowserFilePath.Click += new System.EventHandler(this.btnSelectWebBrowserFilePath_Click);
            // 
            // textBoxDownloadDirectory
            // 
            this.textBoxDownloadDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDownloadDirectory.Location = new System.Drawing.Point(13, 23);
            this.textBoxDownloadDirectory.Name = "textBoxDownloadDirectory";
            this.textBoxDownloadDirectory.Size = new System.Drawing.Size(470, 20);
            this.textBoxDownloadDirectory.TabIndex = 0;
            this.textBoxDownloadDirectory.Leave += new System.EventHandler(this.textBoxDownloadDirectory_Leave);
            // 
            // textBoxWebBrowserFilePath
            // 
            this.textBoxWebBrowserFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxWebBrowserFilePath.Location = new System.Drawing.Point(13, 220);
            this.textBoxWebBrowserFilePath.Name = "textBoxWebBrowserFilePath";
            this.textBoxWebBrowserFilePath.Size = new System.Drawing.Size(470, 20);
            this.textBoxWebBrowserFilePath.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Папка для скачивания:";
            // 
            // textBoxTempDirectory
            // 
            this.textBoxTempDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxTempDirectory.Location = new System.Drawing.Point(13, 64);
            this.textBoxTempDirectory.Name = "textBoxTempDirectory";
            this.textBoxTempDirectory.Size = new System.Drawing.Size(470, 20);
            this.textBoxTempDirectory.TabIndex = 1;
            this.textBoxTempDirectory.Leave += new System.EventHandler(this.textBoxTempDirectory_Leave);
            // 
            // btnBrowseTempDirectory
            // 
            this.btnBrowseTempDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseTempDirectory.Location = new System.Drawing.Point(489, 64);
            this.btnBrowseTempDirectory.Name = "btnBrowseTempDirectory";
            this.btnBrowseTempDirectory.Size = new System.Drawing.Size(32, 20);
            this.btnBrowseTempDirectory.TabIndex = 3;
            this.btnBrowseTempDirectory.Text = "...";
            this.btnBrowseTempDirectory.UseVisualStyleBackColor = true;
            this.btnBrowseTempDirectory.Click += new System.EventHandler(this.btnBrowseTempDirectory_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(164, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Папка для временных файлов:";
            // 
            // tabPageGUI
            // 
            this.tabPageGUI.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageGUI.Controls.Add(this.checkBoxShowHlsTracksOnlyForStreams);
            this.tabPageGUI.Controls.Add(this.groupBox12);
            this.tabPageGUI.Controls.Add(this.groupBoxFontSettings);
            this.tabPageGUI.Location = new System.Drawing.Point(4, 22);
            this.tabPageGUI.Name = "tabPageGUI";
            this.tabPageGUI.Size = new System.Drawing.Size(527, 357);
            this.tabPageGUI.TabIndex = 3;
            this.tabPageGUI.Text = "Интерфейс";
            // 
            // checkBoxShowHlsTracksOnlyForStreams
            // 
            this.checkBoxShowHlsTracksOnlyForStreams.AutoSize = true;
            this.checkBoxShowHlsTracksOnlyForStreams.Location = new System.Drawing.Point(3, 217);
            this.checkBoxShowHlsTracksOnlyForStreams.Name = "checkBoxShowHlsTracksOnlyForStreams";
            this.checkBoxShowHlsTracksOnlyForStreams.Size = new System.Drawing.Size(378, 17);
            this.checkBoxShowHlsTracksOnlyForStreams.TabIndex = 2;
            this.checkBoxShowHlsTracksOnlyForStreams.Text = "Показывать форматы HLS только для прямых трансляций (стримов)";
            this.checkBoxShowHlsTracksOnlyForStreams.UseVisualStyleBackColor = true;
            this.checkBoxShowHlsTracksOnlyForStreams.CheckedChanged += new System.EventHandler(this.checkBoxShowHlsTracksOnlyForStreams_CheckedChanged);
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox12.Controls.Add(this.checkBoxSortDashFormatsByBitrate);
            this.groupBox12.Controls.Add(this.checkBoxMoveAudioTrackId140ToTopOfList);
            this.groupBox12.Controls.Add(this.checkBoxSortAdaptiveFormatsByFileSize);
            this.groupBox12.Location = new System.Drawing.Point(3, 120);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(521, 91);
            this.groupBox12.TabIndex = 1;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Сортировка списка форматов для скачивания";
            // 
            // checkBoxSortDashFormatsByBitrate
            // 
            this.checkBoxSortDashFormatsByBitrate.AutoSize = true;
            this.checkBoxSortDashFormatsByBitrate.Location = new System.Drawing.Point(18, 42);
            this.checkBoxSortDashFormatsByBitrate.Name = "checkBoxSortDashFormatsByBitrate";
            this.checkBoxSortDashFormatsByBitrate.Size = new System.Drawing.Size(320, 17);
            this.checkBoxSortDashFormatsByBitrate.TabIndex = 2;
            this.checkBoxSortDashFormatsByBitrate.Text = "Сортировать форматы DASH по битрейту (если известен)";
            this.checkBoxSortDashFormatsByBitrate.UseVisualStyleBackColor = true;
            this.checkBoxSortDashFormatsByBitrate.CheckedChanged += new System.EventHandler(this.checkBoxSortDashFormatsByBitrate_CheckedChanged);
            // 
            // checkBoxMoveAudioTrackId140ToTopOfList
            // 
            this.checkBoxMoveAudioTrackId140ToTopOfList.AutoSize = true;
            this.checkBoxMoveAudioTrackId140ToTopOfList.Location = new System.Drawing.Point(18, 65);
            this.checkBoxMoveAudioTrackId140ToTopOfList.Name = "checkBoxMoveAudioTrackId140ToTopOfList";
            this.checkBoxMoveAudioTrackId140ToTopOfList.Size = new System.Drawing.Size(302, 17);
            this.checkBoxMoveAudioTrackId140ToTopOfList.TabIndex = 1;
            this.checkBoxMoveAudioTrackId140ToTopOfList.Text = "Перемещать аудио-дорожку с ID 140 на первое место";
            this.toolTip1.SetToolTip(this.checkBoxMoveAudioTrackId140ToTopOfList, "Независимо от сортировки");
            this.checkBoxMoveAudioTrackId140ToTopOfList.UseVisualStyleBackColor = true;
            this.checkBoxMoveAudioTrackId140ToTopOfList.CheckedChanged += new System.EventHandler(this.checkBoxMoveAudioTrackId140ToTopOfList_CheckedChanged);
            // 
            // checkBoxSortAdaptiveFormatsByFileSize
            // 
            this.checkBoxSortAdaptiveFormatsByFileSize.AutoSize = true;
            this.checkBoxSortAdaptiveFormatsByFileSize.Location = new System.Drawing.Point(18, 19);
            this.checkBoxSortAdaptiveFormatsByFileSize.Name = "checkBoxSortAdaptiveFormatsByFileSize";
            this.checkBoxSortAdaptiveFormatsByFileSize.Size = new System.Drawing.Size(320, 17);
            this.checkBoxSortAdaptiveFormatsByFileSize.TabIndex = 0;
            this.checkBoxSortAdaptiveFormatsByFileSize.Text = "Сортировать форматы по размеру файла (если известен)";
            this.toolTip1.SetToolTip(this.checkBoxSortAdaptiveFormatsByFileSize, "Не применяется к HLS, DASH и контейнерным форматам!");
            this.checkBoxSortAdaptiveFormatsByFileSize.UseVisualStyleBackColor = true;
            this.checkBoxSortAdaptiveFormatsByFileSize.CheckedChanged += new System.EventHandler(this.checkBoxSortAdaptiveFormatsByFileSize_CheckedChanged);
            // 
            // groupBoxFontSettings
            // 
            this.groupBoxFontSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFontSettings.Controls.Add(this.numericUpDownVideoTitleFontSize);
            this.groupBoxFontSettings.Controls.Add(this.label17);
            this.groupBoxFontSettings.Controls.Add(this.numericUpDownFavoritesListFontSize);
            this.groupBoxFontSettings.Controls.Add(this.label16);
            this.groupBoxFontSettings.Controls.Add(this.label15);
            this.groupBoxFontSettings.Controls.Add(this.numericUpDownMenusFontSize);
            this.groupBoxFontSettings.Location = new System.Drawing.Point(3, 3);
            this.groupBoxFontSettings.Name = "groupBoxFontSettings";
            this.groupBoxFontSettings.Size = new System.Drawing.Size(521, 111);
            this.groupBoxFontSettings.TabIndex = 0;
            this.groupBoxFontSettings.TabStop = false;
            this.groupBoxFontSettings.Text = "Размер шрифтов";
            // 
            // numericUpDownVideoTitleFontSize
            // 
            this.numericUpDownVideoTitleFontSize.Location = new System.Drawing.Point(129, 26);
            this.numericUpDownVideoTitleFontSize.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownVideoTitleFontSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownVideoTitleFontSize.Name = "numericUpDownVideoTitleFontSize";
            this.numericUpDownVideoTitleFontSize.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownVideoTitleFontSize.TabIndex = 5;
            this.numericUpDownVideoTitleFontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownVideoTitleFontSize.ValueChanged += new System.EventHandler(this.numericUpDownVideoTitleFontSize_ValueChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 28);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(93, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "Название видео:";
            // 
            // numericUpDownFavoritesListFontSize
            // 
            this.numericUpDownFavoritesListFontSize.Location = new System.Drawing.Point(129, 78);
            this.numericUpDownFavoritesListFontSize.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownFavoritesListFontSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownFavoritesListFontSize.Name = "numericUpDownFavoritesListFontSize";
            this.numericUpDownFavoritesListFontSize.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownFavoritesListFontSize.TabIndex = 3;
            this.numericUpDownFavoritesListFontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownFavoritesListFontSize.ValueChanged += new System.EventHandler(this.numericUpDownFavoritesListFontSize_ValueChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 80);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Избранное:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 53);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(108, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Контекстные меню:";
            // 
            // numericUpDownMenusFontSize
            // 
            this.numericUpDownMenusFontSize.Location = new System.Drawing.Point(129, 51);
            this.numericUpDownMenusFontSize.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownMenusFontSize.Minimum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDownMenusFontSize.Name = "numericUpDownMenusFontSize";
            this.numericUpDownMenusFontSize.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownMenusFontSize.TabIndex = 0;
            this.numericUpDownMenusFontSize.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDownMenusFontSize.ValueChanged += new System.EventHandler(this.numericUpDownMenusFontSize_ValueChanged);
            // 
            // tabPageDownloadSettings
            // 
            this.tabPageDownloadSettings.AutoScroll = true;
            this.tabPageDownloadSettings.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPageDownloadSettings.Controls.Add(this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted);
            this.tabPageDownloadSettings.Controls.Add(this.groupBox19);
            this.tabPageDownloadSettings.Controls.Add(this.groupBoxAdaptiveFormatsSettings);
            this.tabPageDownloadSettings.Controls.Add(this.checkBoxAutomaticallySaveVideoThumbnailImage);
            this.tabPageDownloadSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageDownloadSettings.Name = "tabPageDownloadSettings";
            this.tabPageDownloadSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDownloadSettings.Size = new System.Drawing.Size(527, 357);
            this.tabPageDownloadSettings.TabIndex = 2;
            this.tabPageDownloadSettings.Text = "Скачивание";
            // 
            // checkBoxCheckUrlsAccessibilityBeforeDownloadStarted
            // 
            this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.AutoSize = true;
            this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Checked = true;
            this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Location = new System.Drawing.Point(14, 292);
            this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Name = "checkBoxCheckUrlsAccessibilityBeforeDownloadStarted";
            this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Size = new System.Drawing.Size(317, 17);
            this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.TabIndex = 21;
            this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Text = "Проверять доступность всех ссылок перед скачиванием";
            this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.UseVisualStyleBackColor = true;
            this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.CheckedChanged += new System.EventHandler(this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted_CheckedChanged);
            // 
            // groupBox19
            // 
            this.groupBox19.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox19.Controls.Add(this.label28);
            this.groupBox19.Controls.Add(this.label27);
            this.groupBox19.Controls.Add(this.numericUpDownChunkDownloadErrorCountLimit);
            this.groupBox19.Controls.Add(this.label26);
            this.groupBox19.Controls.Add(this.label21);
            this.groupBox19.Controls.Add(this.numericUpDownChunkDownloadTryCountLimit);
            this.groupBox19.Location = new System.Drawing.Point(14, 315);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(490, 72);
            this.groupBox19.TabIndex = 20;
            this.groupBox19.TabStop = false;
            this.groupBox19.Text = "Попытки скачивания";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(289, 47);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(81, 13);
            this.label28.TabIndex = 23;
            this.label28.Text = "0 - бесконечно";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(289, 21);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(81, 13);
            this.label27.TabIndex = 22;
            this.label27.Text = "0 - бесконечно";
            // 
            // numericUpDownChunkDownloadErrorCountLimit
            // 
            this.numericUpDownChunkDownloadErrorCountLimit.Location = new System.Drawing.Point(237, 45);
            this.numericUpDownChunkDownloadErrorCountLimit.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownChunkDownloadErrorCountLimit.Name = "numericUpDownChunkDownloadErrorCountLimit";
            this.numericUpDownChunkDownloadErrorCountLimit.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownChunkDownloadErrorCountLimit.TabIndex = 21;
            this.numericUpDownChunkDownloadErrorCountLimit.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownChunkDownloadErrorCountLimit.ValueChanged += new System.EventHandler(this.numericUpDownChunkDownloadErrorCount_ValueChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(6, 47);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(225, 13);
            this.label26.TabIndex = 20;
            this.label26.Text = "Количество ошибок при скачивании чанка:";
            this.toolTip1.SetToolTip(this.label26, "При достижении этого значения, скачивание чанка перезапускается.");
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(6, 21);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(215, 13);
            this.label21.TabIndex = 18;
            this.label21.Text = "Количество попыток скачивания чанков:";
            // 
            // numericUpDownChunkDownloadTryCountLimit
            // 
            this.numericUpDownChunkDownloadTryCountLimit.Location = new System.Drawing.Point(237, 19);
            this.numericUpDownChunkDownloadTryCountLimit.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownChunkDownloadTryCountLimit.Name = "numericUpDownChunkDownloadTryCountLimit";
            this.numericUpDownChunkDownloadTryCountLimit.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownChunkDownloadTryCountLimit.TabIndex = 19;
            this.numericUpDownChunkDownloadTryCountLimit.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownChunkDownloadTryCountLimit.ValueChanged += new System.EventHandler(this.numericUpDownChunkDownloadTryCountLimit_ValueChanged);
            // 
            // groupBoxAdaptiveFormatsSettings
            // 
            this.groupBoxAdaptiveFormatsSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.numericUpDownDelayAfterContainerCreated);
            this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.label20);
            this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.label19);
            this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.btnWtfDownloadAllAdaptiveVideoTracks);
            this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks);
            this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.groupBox15);
            this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.groupBox14);
            this.groupBoxAdaptiveFormatsSettings.Location = new System.Drawing.Point(14, 6);
            this.groupBoxAdaptiveFormatsSettings.Name = "groupBoxAdaptiveFormatsSettings";
            this.groupBoxAdaptiveFormatsSettings.Size = new System.Drawing.Size(490, 280);
            this.groupBoxAdaptiveFormatsSettings.TabIndex = 17;
            this.groupBoxAdaptiveFormatsSettings.TabStop = false;
            this.groupBoxAdaptiveFormatsSettings.Text = "Адаптивные форматы";
            // 
            // numericUpDownDelayAfterContainerCreated
            // 
            this.numericUpDownDelayAfterContainerCreated.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownDelayAfterContainerCreated.Location = new System.Drawing.Point(219, 249);
            this.numericUpDownDelayAfterContainerCreated.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownDelayAfterContainerCreated.Name = "numericUpDownDelayAfterContainerCreated";
            this.numericUpDownDelayAfterContainerCreated.Size = new System.Drawing.Size(49, 20);
            this.numericUpDownDelayAfterContainerCreated.TabIndex = 23;
            this.numericUpDownDelayAfterContainerCreated.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownDelayAfterContainerCreated.ValueChanged += new System.EventHandler(this.numericUpDownDelayAfterContainerCreated_ValueChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(274, 251);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(74, 13);
            this.label20.TabIndex = 22;
            this.label20.Text = "миллисекунд";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 251);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(207, 13);
            this.label19.TabIndex = 21;
            this.label19.Text = "Задержка после создания контейнера:";
            this.toolTip1.SetToolTip(this.label19, "Позволяет избежать падения программы, если закрыть окно консоли раньше времени");
            // 
            // btnWtfDownloadAllAdaptiveVideoTracks
            // 
            this.btnWtfDownloadAllAdaptiveVideoTracks.Location = new System.Drawing.Point(339, 15);
            this.btnWtfDownloadAllAdaptiveVideoTracks.Name = "btnWtfDownloadAllAdaptiveVideoTracks";
            this.btnWtfDownloadAllAdaptiveVideoTracks.Size = new System.Drawing.Size(30, 23);
            this.btnWtfDownloadAllAdaptiveVideoTracks.TabIndex = 20;
            this.btnWtfDownloadAllAdaptiveVideoTracks.Text = "?";
            this.btnWtfDownloadAllAdaptiveVideoTracks.UseVisualStyleBackColor = true;
            this.btnWtfDownloadAllAdaptiveVideoTracks.Click += new System.EventHandler(this.btnWtfDownloadAllAdaptiveVideoTracks_Click);
            // 
            // checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks
            // 
            this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks.AutoSize = true;
            this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks.Location = new System.Drawing.Point(6, 19);
            this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks.Name = "checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks";
            this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks.Size = new System.Drawing.Size(327, 17);
            this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks.TabIndex = 19;
            this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks.Text = "Автоматически скачивать все адаптивные форматы видео";
            this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks.UseVisualStyleBackColor = true;
            this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks.CheckedChanged += new System.EventHandler(this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks_CheckedChanged);
            // 
            // groupBox15
            // 
            this.groupBox15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox15.Controls.Add(this.groupBox16);
            this.groupBox15.Controls.Add(this.checkBoxAutomaticallyMergeAdaptiveTracks);
            this.groupBox15.Controls.Add(this.checkBoxDeleteSourceFilesWhenMerged);
            this.groupBox15.Location = new System.Drawing.Point(6, 160);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(478, 83);
            this.groupBox15.TabIndex = 18;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Контейнер";
            // 
            // groupBox16
            // 
            this.groupBox16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox16.Controls.Add(this.radioButtonContainerTypeMkv);
            this.groupBox16.Controls.Add(this.radioButtonContainerTypeMp4);
            this.groupBox16.Location = new System.Drawing.Point(295, 19);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(177, 60);
            this.groupBox16.TabIndex = 17;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Тип контейнера";
            // 
            // radioButtonContainerTypeMkv
            // 
            this.radioButtonContainerTypeMkv.AutoSize = true;
            this.radioButtonContainerTypeMkv.Location = new System.Drawing.Point(12, 40);
            this.radioButtonContainerTypeMkv.Name = "radioButtonContainerTypeMkv";
            this.radioButtonContainerTypeMkv.Size = new System.Drawing.Size(48, 17);
            this.radioButtonContainerTypeMkv.TabIndex = 1;
            this.radioButtonContainerTypeMkv.Text = "MKV";
            this.radioButtonContainerTypeMkv.UseVisualStyleBackColor = true;
            this.radioButtonContainerTypeMkv.CheckedChanged += new System.EventHandler(this.radioButtonContainerTypeMkv_CheckedChanged);
            // 
            // radioButtonContainerTypeMp4
            // 
            this.radioButtonContainerTypeMp4.AutoSize = true;
            this.radioButtonContainerTypeMp4.Checked = true;
            this.radioButtonContainerTypeMp4.Location = new System.Drawing.Point(12, 17);
            this.radioButtonContainerTypeMp4.Name = "radioButtonContainerTypeMp4";
            this.radioButtonContainerTypeMp4.Size = new System.Drawing.Size(135, 17);
            this.radioButtonContainerTypeMp4.TabIndex = 0;
            this.radioButtonContainerTypeMp4.TabStop = true;
            this.radioButtonContainerTypeMp4.Text = "MP4 (если возможно)";
            this.radioButtonContainerTypeMp4.UseVisualStyleBackColor = true;
            this.radioButtonContainerTypeMp4.CheckedChanged += new System.EventHandler(this.radioButtonContainerTypeMp4_CheckedChanged);
            // 
            // checkBoxAutomaticallyMergeAdaptiveTracks
            // 
            this.checkBoxAutomaticallyMergeAdaptiveTracks.AutoSize = true;
            this.checkBoxAutomaticallyMergeAdaptiveTracks.Location = new System.Drawing.Point(20, 18);
            this.checkBoxAutomaticallyMergeAdaptiveTracks.Name = "checkBoxAutomaticallyMergeAdaptiveTracks";
            this.checkBoxAutomaticallyMergeAdaptiveTracks.Size = new System.Drawing.Size(274, 17);
            this.checkBoxAutomaticallyMergeAdaptiveTracks.TabIndex = 14;
            this.checkBoxAutomaticallyMergeAdaptiveTracks.Text = "Объединять дорожки видео и аудио в контейнер";
            this.toolTip1.SetToolTip(this.checkBoxAutomaticallyMergeAdaptiveTracks, "Не применяется, если скачаны только аудио-дорожки");
            this.checkBoxAutomaticallyMergeAdaptiveTracks.UseVisualStyleBackColor = true;
            this.checkBoxAutomaticallyMergeAdaptiveTracks.CheckedChanged += new System.EventHandler(this.checkBoxAutomaticallyMergeAdaptiveTracks_CheckedChanged);
            // 
            // checkBoxDeleteSourceFilesWhenMerged
            // 
            this.checkBoxDeleteSourceFilesWhenMerged.AutoSize = true;
            this.checkBoxDeleteSourceFilesWhenMerged.Location = new System.Drawing.Point(30, 41);
            this.checkBoxDeleteSourceFilesWhenMerged.Name = "checkBoxDeleteSourceFilesWhenMerged";
            this.checkBoxDeleteSourceFilesWhenMerged.Size = new System.Drawing.Size(158, 17);
            this.checkBoxDeleteSourceFilesWhenMerged.TabIndex = 16;
            this.checkBoxDeleteSourceFilesWhenMerged.Text = "Удалять исходные файлы";
            this.toolTip1.SetToolTip(this.checkBoxDeleteSourceFilesWhenMerged, "Не применяется, если скачаны только аудио-дорожки");
            this.checkBoxDeleteSourceFilesWhenMerged.UseVisualStyleBackColor = true;
            this.checkBoxDeleteSourceFilesWhenMerged.CheckedChanged += new System.EventHandler(this.checkBoxDeleteSourceFilesWhenMerged_CheckedChanged);
            // 
            // groupBox14
            // 
            this.groupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox14.Controls.Add(this.checkBoxAutomaticallyDownloadFirstAudioTrack);
            this.groupBox14.Controls.Add(this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger);
            this.groupBox14.Controls.Add(this.checkBoxAutomaticallyDownloadSecondAudioTrack);
            this.groupBox14.Controls.Add(this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks);
            this.groupBox14.Location = new System.Drawing.Point(6, 42);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(478, 112);
            this.groupBox14.TabIndex = 17;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Скачивание аудио-дорожек";
            // 
            // checkBoxAutomaticallyDownloadFirstAudioTrack
            // 
            this.checkBoxAutomaticallyDownloadFirstAudioTrack.AutoSize = true;
            this.checkBoxAutomaticallyDownloadFirstAudioTrack.Location = new System.Drawing.Point(20, 19);
            this.checkBoxAutomaticallyDownloadFirstAudioTrack.Name = "checkBoxAutomaticallyDownloadFirstAudioTrack";
            this.checkBoxAutomaticallyDownloadFirstAudioTrack.Size = new System.Drawing.Size(277, 17);
            this.checkBoxAutomaticallyDownloadFirstAudioTrack.TabIndex = 19;
            this.checkBoxAutomaticallyDownloadFirstAudioTrack.Text = "Автоматически скачивать первую аудио-дорожку";
            this.checkBoxAutomaticallyDownloadFirstAudioTrack.UseVisualStyleBackColor = true;
            this.checkBoxAutomaticallyDownloadFirstAudioTrack.CheckedChanged += new System.EventHandler(this.checkBoxAutomaticallyDownloadFirstAudioTrack_CheckedChanged);
            // 
            // checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger
            // 
            this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.AutoSize = true;
            this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Location = new System.Drawing.Point(30, 65);
            this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Name = "checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger";
            this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Size = new System.Drawing.Size(207, 17);
            this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.TabIndex = 18;
            this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.Text = "Только если размер файла больше";
            this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.UseVisualStyleBackColor = true;
            this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger.CheckedChanged += new System.EventHandler(this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger_CheckedChanged);
            // 
            // checkBoxAutomaticallyDownloadSecondAudioTrack
            // 
            this.checkBoxAutomaticallyDownloadSecondAudioTrack.AutoSize = true;
            this.checkBoxAutomaticallyDownloadSecondAudioTrack.Location = new System.Drawing.Point(20, 42);
            this.checkBoxAutomaticallyDownloadSecondAudioTrack.Name = "checkBoxAutomaticallyDownloadSecondAudioTrack";
            this.checkBoxAutomaticallyDownloadSecondAudioTrack.Size = new System.Drawing.Size(276, 17);
            this.checkBoxAutomaticallyDownloadSecondAudioTrack.TabIndex = 1;
            this.checkBoxAutomaticallyDownloadSecondAudioTrack.Text = "Автоматически скачивать вторую аудио-дорожку";
            this.checkBoxAutomaticallyDownloadSecondAudioTrack.UseVisualStyleBackColor = true;
            this.checkBoxAutomaticallyDownloadSecondAudioTrack.CheckedChanged += new System.EventHandler(this.checkBoxAutomaticallyDownloadSecondAudioTrack_CheckedChanged);
            // 
            // checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks
            // 
            this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.AutoSize = true;
            this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.Location = new System.Drawing.Point(20, 88);
            this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.Name = "checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks";
            this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.Size = new System.Drawing.Size(179, 17);
            this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.TabIndex = 0;
            this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.Text = "Скачивать все аудио-дорожки";
            this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.UseVisualStyleBackColor = true;
            this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks.CheckedChanged += new System.EventHandler(this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks_CheckedChanged);
            // 
            // checkBoxAutomaticallySaveVideoThumbnailImage
            // 
            this.checkBoxAutomaticallySaveVideoThumbnailImage.AutoSize = true;
            this.checkBoxAutomaticallySaveVideoThumbnailImage.Location = new System.Drawing.Point(14, 393);
            this.checkBoxAutomaticallySaveVideoThumbnailImage.Name = "checkBoxAutomaticallySaveVideoThumbnailImage";
            this.checkBoxAutomaticallySaveVideoThumbnailImage.Size = new System.Drawing.Size(175, 17);
            this.checkBoxAutomaticallySaveVideoThumbnailImage.TabIndex = 15;
            this.checkBoxAutomaticallySaveVideoThumbnailImage.Text = "Скачивать картинку от видео";
            this.checkBoxAutomaticallySaveVideoThumbnailImage.UseVisualStyleBackColor = true;
            this.checkBoxAutomaticallySaveVideoThumbnailImage.CheckedChanged += new System.EventHandler(this.checkBoxAutomaticallySaveVideoThumbnailImage_CheckedChanged);
            // 
            // tabPageSystemSettings
            // 
            this.tabPageSystemSettings.AutoScroll = true;
            this.tabPageSystemSettings.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPageSystemSettings.Controls.Add(this.groupBox20);
            this.tabPageSystemSettings.Controls.Add(this.groupBox17);
            this.tabPageSystemSettings.Controls.Add(this.checkBoxUseUniversalTime);
            this.tabPageSystemSettings.Controls.Add(this.groupBox13);
            this.tabPageSystemSettings.Controls.Add(this.groupBoxYouTubeApiSettings);
            this.tabPageSystemSettings.Controls.Add(this.groupBox7);
            this.tabPageSystemSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSystemSettings.Name = "tabPageSystemSettings";
            this.tabPageSystemSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSystemSettings.Size = new System.Drawing.Size(527, 357);
            this.tabPageSystemSettings.TabIndex = 1;
            this.tabPageSystemSettings.Text = "Система";
            // 
            // groupBox20
            // 
            this.groupBox20.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox20.Controls.Add(this.btnWtfUserAgent);
            this.groupBox20.Controls.Add(this.btnRestoreDefaultUserAgent);
            this.groupBox20.Controls.Add(this.textBoxUserAgent);
            this.groupBox20.Location = new System.Drawing.Point(6, 354);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(498, 77);
            this.groupBox20.TabIndex = 17;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "User agent";
            // 
            // btnWtfUserAgent
            // 
            this.btnWtfUserAgent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWtfUserAgent.Location = new System.Drawing.Point(417, 48);
            this.btnWtfUserAgent.Name = "btnWtfUserAgent";
            this.btnWtfUserAgent.Size = new System.Drawing.Size(75, 23);
            this.btnWtfUserAgent.TabIndex = 2;
            this.btnWtfUserAgent.Text = "Зачем?";
            this.btnWtfUserAgent.UseVisualStyleBackColor = true;
            this.btnWtfUserAgent.Click += new System.EventHandler(this.btnWtfUserAgent_Click);
            // 
            // btnRestoreDefaultUserAgent
            // 
            this.btnRestoreDefaultUserAgent.Location = new System.Drawing.Point(7, 48);
            this.btnRestoreDefaultUserAgent.Name = "btnRestoreDefaultUserAgent";
            this.btnRestoreDefaultUserAgent.Size = new System.Drawing.Size(99, 23);
            this.btnRestoreDefaultUserAgent.TabIndex = 1;
            this.btnRestoreDefaultUserAgent.Text = "По-умолчанию";
            this.btnRestoreDefaultUserAgent.UseVisualStyleBackColor = true;
            this.btnRestoreDefaultUserAgent.Click += new System.EventHandler(this.btnRestoreDefaultUserAgent_Click);
            // 
            // textBoxUserAgent
            // 
            this.textBoxUserAgent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUserAgent.Location = new System.Drawing.Point(8, 19);
            this.textBoxUserAgent.Name = "textBoxUserAgent";
            this.textBoxUserAgent.Size = new System.Drawing.Size(476, 20);
            this.textBoxUserAgent.TabIndex = 0;
            this.textBoxUserAgent.Leave += new System.EventHandler(this.textBoxUserAgent_Leave);
            // 
            // groupBox17
            // 
            this.groupBox17.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox17.Controls.Add(this.numericUpDownDashChunkDownloadTryCountLimit);
            this.groupBox17.Controls.Add(this.label23);
            this.groupBox17.Controls.Add(this.btnWtfAlwaysDownloadAsDash);
            this.groupBox17.Controls.Add(this.lblActualDashChunkSize);
            this.groupBox17.Controls.Add(this.numericUpDownDashChunkSize);
            this.groupBox17.Controls.Add(this.label22);
            this.groupBox17.Controls.Add(this.checkBoxAlwaysDownloadAsDash);
            this.groupBox17.Location = new System.Drawing.Point(6, 652);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(498, 102);
            this.groupBox17.TabIndex = 16;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Скачивание файлов по частям";
            // 
            // numericUpDownDashChunkDownloadTryCountLimit
            // 
            this.numericUpDownDashChunkDownloadTryCountLimit.Location = new System.Drawing.Point(232, 68);
            this.numericUpDownDashChunkDownloadTryCountLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDashChunkDownloadTryCountLimit.Name = "numericUpDownDashChunkDownloadTryCountLimit";
            this.numericUpDownDashChunkDownloadTryCountLimit.Size = new System.Drawing.Size(48, 20);
            this.numericUpDownDashChunkDownloadTryCountLimit.TabIndex = 6;
            this.numericUpDownDashChunkDownloadTryCountLimit.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownDashChunkDownloadTryCountLimit.ValueChanged += new System.EventHandler(this.numericUpDownDashChunkDownloadTryCountLimit_ValueChanged);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(17, 70);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(209, 13);
            this.label23.TabIndex = 5;
            this.label23.Text = "Количество попыток скачивания чанка:";
            // 
            // btnWtfAlwaysDownloadAsDash
            // 
            this.btnWtfAlwaysDownloadAsDash.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWtfAlwaysDownloadAsDash.Location = new System.Drawing.Point(417, 15);
            this.btnWtfAlwaysDownloadAsDash.Name = "btnWtfAlwaysDownloadAsDash";
            this.btnWtfAlwaysDownloadAsDash.Size = new System.Drawing.Size(75, 23);
            this.btnWtfAlwaysDownloadAsDash.TabIndex = 4;
            this.btnWtfAlwaysDownloadAsDash.Text = "Зачем?";
            this.btnWtfAlwaysDownloadAsDash.UseVisualStyleBackColor = true;
            this.btnWtfAlwaysDownloadAsDash.Click += new System.EventHandler(this.btnWtfAlwaysDownloadAsDash_Click);
            // 
            // lblActualDashChunkSize
            // 
            this.lblActualDashChunkSize.AutoSize = true;
            this.lblActualDashChunkSize.Location = new System.Drawing.Point(330, 44);
            this.lblActualDashChunkSize.Name = "lblActualDashChunkSize";
            this.lblActualDashChunkSize.Size = new System.Drawing.Size(57, 13);
            this.lblActualDashChunkSize.TabIndex = 3;
            this.lblActualDashChunkSize.Text = "50,000 KB";
            // 
            // numericUpDownDashChunkSize
            // 
            this.numericUpDownDashChunkSize.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownDashChunkSize.Location = new System.Drawing.Point(232, 42);
            this.numericUpDownDashChunkSize.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.numericUpDownDashChunkSize.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDownDashChunkSize.Name = "numericUpDownDashChunkSize";
            this.numericUpDownDashChunkSize.Size = new System.Drawing.Size(92, 20);
            this.numericUpDownDashChunkSize.TabIndex = 2;
            this.numericUpDownDashChunkSize.Value = new decimal(new int[] {
            50000,
            0,
            0,
            0});
            this.numericUpDownDashChunkSize.ValueChanged += new System.EventHandler(this.numericUpDownDashChunkSize_ValueChanged);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(17, 44);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(203, 13);
            this.label22.TabIndex = 1;
            this.label22.Text = "Разделять файлы на чанки размером:";
            // 
            // checkBoxAlwaysDownloadAsDash
            // 
            this.checkBoxAlwaysDownloadAsDash.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxAlwaysDownloadAsDash.AutoSize = true;
            this.checkBoxAlwaysDownloadAsDash.Location = new System.Drawing.Point(17, 19);
            this.checkBoxAlwaysDownloadAsDash.Name = "checkBoxAlwaysDownloadAsDash";
            this.checkBoxAlwaysDownloadAsDash.Size = new System.Drawing.Size(187, 17);
            this.checkBoxAlwaysDownloadAsDash.TabIndex = 0;
            this.checkBoxAlwaysDownloadAsDash.Text = "Скачивать все видео как DASH";
            this.checkBoxAlwaysDownloadAsDash.UseVisualStyleBackColor = true;
            this.checkBoxAlwaysDownloadAsDash.CheckedChanged += new System.EventHandler(this.checkBoxAlwaysDownloadAsDash_CheckedChanged);
            // 
            // checkBoxUseUniversalTime
            // 
            this.checkBoxUseUniversalTime.AutoSize = true;
            this.checkBoxUseUniversalTime.Checked = true;
            this.checkBoxUseUniversalTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseUniversalTime.Location = new System.Drawing.Point(7, 760);
            this.checkBoxUseUniversalTime.Name = "checkBoxUseUniversalTime";
            this.checkBoxUseUniversalTime.Size = new System.Drawing.Size(101, 17);
            this.checkBoxUseUniversalTime.TabIndex = 15;
            this.checkBoxUseUniversalTime.Text = "Время по GMT";
            this.toolTip1.SetToolTip(this.checkBoxUseUniversalTime, "После изменения этого параметра необходимо повторить поиск видео!");
            this.checkBoxUseUniversalTime.UseVisualStyleBackColor = true;
            this.checkBoxUseUniversalTime.CheckedChanged += new System.EventHandler(this.checkBoxUseUniversalTime_CheckedChanged);
            // 
            // groupBox13
            // 
            this.groupBox13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox13.Controls.Add(this.btnWtfUseRam);
            this.groupBox13.Controls.Add(this.panelRAM);
            this.groupBox13.Controls.Add(this.checkBoxUseRamForTempFiles);
            this.groupBox13.Location = new System.Drawing.Point(6, 585);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(498, 66);
            this.groupBox13.TabIndex = 14;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Оперативная память (RAM)";
            this.groupBox13.Resize += new System.EventHandler(this.groupBox13_Resize);
            // 
            // btnWtfUseRam
            // 
            this.btnWtfUseRam.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWtfUseRam.Location = new System.Drawing.Point(417, 28);
            this.btnWtfUseRam.Name = "btnWtfUseRam";
            this.btnWtfUseRam.Size = new System.Drawing.Size(75, 23);
            this.btnWtfUseRam.TabIndex = 16;
            this.btnWtfUseRam.Text = "Зачем?";
            this.btnWtfUseRam.UseVisualStyleBackColor = true;
            this.btnWtfUseRam.Click += new System.EventHandler(this.btnWtfUseRam_Click);
            // 
            // panelRAM
            // 
            this.panelRAM.BackgroundImage = global::YouTube_downloader.Properties.Resources.warning;
            this.panelRAM.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelRAM.Location = new System.Drawing.Point(313, 22);
            this.panelRAM.Name = "panelRAM";
            this.panelRAM.Size = new System.Drawing.Size(32, 32);
            this.panelRAM.TabIndex = 15;
            this.toolTip1.SetToolTip(this.panelRAM, "Доступно только в версии x64!");
            this.panelRAM.Visible = false;
            // 
            // checkBoxUseRamForTempFiles
            // 
            this.checkBoxUseRamForTempFiles.Location = new System.Drawing.Point(17, 19);
            this.checkBoxUseRamForTempFiles.Name = "checkBoxUseRamForTempFiles";
            this.checkBoxUseRamForTempFiles.Size = new System.Drawing.Size(288, 42);
            this.checkBoxUseRamForTempFiles.TabIndex = 13;
            this.checkBoxUseRamForTempFiles.Text = "Использовать оперативную память для хранения временных файлов (экспериментально!)" +
    "";
            this.toolTip1.SetToolTip(this.checkBoxUseRamForTempFiles, "Не применяется к DASH и HLS");
            this.checkBoxUseRamForTempFiles.UseVisualStyleBackColor = true;
            this.checkBoxUseRamForTempFiles.CheckedChanged += new System.EventHandler(this.checkBoxUseRamForTempFiles_CheckedChanged);
            // 
            // groupBoxYouTubeApiSettings
            // 
            this.groupBoxYouTubeApiSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxYouTubeApiSettings.Controls.Add(this.groupBox18);
            this.groupBoxYouTubeApiSettings.Controls.Add(this.groupBox4);
            this.groupBoxYouTubeApiSettings.Controls.Add(this.groupBox5);
            this.groupBoxYouTubeApiSettings.Location = new System.Drawing.Point(3, 6);
            this.groupBoxYouTubeApiSettings.Name = "groupBoxYouTubeApiSettings";
            this.groupBoxYouTubeApiSettings.Size = new System.Drawing.Size(501, 349);
            this.groupBoxYouTubeApiSettings.TabIndex = 12;
            this.groupBoxYouTubeApiSettings.TabStop = false;
            this.groupBoxYouTubeApiSettings.Text = "API";
            // 
            // groupBox18
            // 
            this.groupBox18.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox18.Controls.Add(this.groupBox21);
            this.groupBox18.Controls.Add(this.numericUpDownConnectionTimeoutExternalRestApiServer);
            this.groupBox18.Controls.Add(this.label30);
            this.groupBox18.Controls.Add(this.btnWtfExternalRestApiServer);
            this.groupBox18.Controls.Add(this.label25);
            this.groupBox18.Controls.Add(this.numericUpDownExternalRestApiServerPort);
            this.groupBox18.Controls.Add(this.textBoxExternalRestApiServerUrl);
            this.groupBox18.Controls.Add(this.label24);
            this.groupBox18.Location = new System.Drawing.Point(3, 149);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(498, 195);
            this.groupBox18.TabIndex = 17;
            this.groupBox18.TabStop = false;
            this.groupBox18.Text = "Внешний REST API сервер";
            // 
            // groupBox21
            // 
            this.groupBox21.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox21.Controls.Add(this.checkBoxUseExternalRestApiServerToGetDownloadUrls);
            this.groupBox21.Controls.Add(this.checkBoxUseExternalRestApiServerToGetBasicVideoInfo);
            this.groupBox21.Controls.Add(this.checkBoxUseExternalRestApiServerToGetAdultVideos);
            this.groupBox21.Location = new System.Drawing.Point(16, 102);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(476, 88);
            this.groupBox21.TabIndex = 8;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "Использование сервера";
            // 
            // checkBoxUseExternalRestApiServerToGetDownloadUrls
            // 
            this.checkBoxUseExternalRestApiServerToGetDownloadUrls.AutoSize = true;
            this.checkBoxUseExternalRestApiServerToGetDownloadUrls.Location = new System.Drawing.Point(6, 42);
            this.checkBoxUseExternalRestApiServerToGetDownloadUrls.Name = "checkBoxUseExternalRestApiServerToGetDownloadUrls";
            this.checkBoxUseExternalRestApiServerToGetDownloadUrls.Size = new System.Drawing.Size(306, 17);
            this.checkBoxUseExternalRestApiServerToGetDownloadUrls.TabIndex = 7;
            this.checkBoxUseExternalRestApiServerToGetDownloadUrls.Text = "Получение списка форматов и ссылок для скачивания";
            this.checkBoxUseExternalRestApiServerToGetDownloadUrls.UseVisualStyleBackColor = true;
            this.checkBoxUseExternalRestApiServerToGetDownloadUrls.CheckedChanged += new System.EventHandler(this.checkBoxUseExternalRestApiServerToGetDownloadUrls_CheckedChanged);
            // 
            // checkBoxUseExternalRestApiServerToGetBasicVideoInfo
            // 
            this.checkBoxUseExternalRestApiServerToGetBasicVideoInfo.AutoSize = true;
            this.checkBoxUseExternalRestApiServerToGetBasicVideoInfo.Location = new System.Drawing.Point(6, 19);
            this.checkBoxUseExternalRestApiServerToGetBasicVideoInfo.Name = "checkBoxUseExternalRestApiServerToGetBasicVideoInfo";
            this.checkBoxUseExternalRestApiServerToGetBasicVideoInfo.Size = new System.Drawing.Size(178, 17);
            this.checkBoxUseExternalRestApiServerToGetBasicVideoInfo.TabIndex = 6;
            this.checkBoxUseExternalRestApiServerToGetBasicVideoInfo.Text = "Базовая информация о видео";
            this.checkBoxUseExternalRestApiServerToGetBasicVideoInfo.UseVisualStyleBackColor = true;
            this.checkBoxUseExternalRestApiServerToGetBasicVideoInfo.CheckedChanged += new System.EventHandler(this.checkBoxUseExternalRestApiServerToGetBasicVideoInfo_CheckedChanged);
            // 
            // checkBoxUseExternalRestApiServerToGetAdultVideos
            // 
            this.checkBoxUseExternalRestApiServerToGetAdultVideos.AutoSize = true;
            this.checkBoxUseExternalRestApiServerToGetAdultVideos.Location = new System.Drawing.Point(6, 65);
            this.checkBoxUseExternalRestApiServerToGetAdultVideos.Name = "checkBoxUseExternalRestApiServerToGetAdultVideos";
            this.checkBoxUseExternalRestApiServerToGetAdultVideos.Size = new System.Drawing.Size(238, 17);
            this.checkBoxUseExternalRestApiServerToGetAdultVideos.TabIndex = 0;
            this.checkBoxUseExternalRestApiServerToGetAdultVideos.Text = "Использовать этот сервер для видео 18+";
            this.toolTip1.SetToolTip(this.checkBoxUseExternalRestApiServerToGetAdultVideos, "В новом API уже не работает :\'(");
            this.checkBoxUseExternalRestApiServerToGetAdultVideos.UseVisualStyleBackColor = true;
            this.checkBoxUseExternalRestApiServerToGetAdultVideos.CheckedChanged += new System.EventHandler(this.checkBoxUseExternalRestApiServerToGetAdultVideos_CheckedChanged);
            // 
            // numericUpDownConnectionTimeoutExternalRestApiServer
            // 
            this.numericUpDownConnectionTimeoutExternalRestApiServer.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownConnectionTimeoutExternalRestApiServer.Location = new System.Drawing.Point(262, 76);
            this.numericUpDownConnectionTimeoutExternalRestApiServer.Maximum = new decimal(new int[] {
            120000,
            0,
            0,
            0});
            this.numericUpDownConnectionTimeoutExternalRestApiServer.Minimum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownConnectionTimeoutExternalRestApiServer.Name = "numericUpDownConnectionTimeoutExternalRestApiServer";
            this.numericUpDownConnectionTimeoutExternalRestApiServer.Size = new System.Drawing.Size(82, 20);
            this.numericUpDownConnectionTimeoutExternalRestApiServer.TabIndex = 7;
            this.numericUpDownConnectionTimeoutExternalRestApiServer.Value = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            this.numericUpDownConnectionTimeoutExternalRestApiServer.ValueChanged += new System.EventHandler(this.numericUpDownConnectionTimeoutExternalRestApiServer_ValueChanged);
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(13, 78);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(243, 13);
            this.label30.TabIndex = 6;
            this.label30.Text = "Время ожидания соединения (миллисекунды):";
            // 
            // btnWtfExternalRestApiServer
            // 
            this.btnWtfExternalRestApiServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWtfExternalRestApiServer.Location = new System.Drawing.Point(417, 47);
            this.btnWtfExternalRestApiServer.Name = "btnWtfExternalRestApiServer";
            this.btnWtfExternalRestApiServer.Size = new System.Drawing.Size(75, 23);
            this.btnWtfExternalRestApiServer.TabIndex = 5;
            this.btnWtfExternalRestApiServer.Text = "Зачем?";
            this.btnWtfExternalRestApiServer.UseVisualStyleBackColor = true;
            this.btnWtfExternalRestApiServer.Click += new System.EventHandler(this.btnWtfExternalRestApiServer_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(13, 47);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(80, 13);
            this.label25.TabIndex = 4;
            this.label25.Text = "Порт сервера:";
            // 
            // numericUpDownExternalRestApiServerPort
            // 
            this.numericUpDownExternalRestApiServerPort.Location = new System.Drawing.Point(105, 45);
            this.numericUpDownExternalRestApiServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownExternalRestApiServerPort.Minimum = new decimal(new int[] {
            1025,
            0,
            0,
            0});
            this.numericUpDownExternalRestApiServerPort.Name = "numericUpDownExternalRestApiServerPort";
            this.numericUpDownExternalRestApiServerPort.Size = new System.Drawing.Size(79, 20);
            this.numericUpDownExternalRestApiServerPort.TabIndex = 3;
            this.numericUpDownExternalRestApiServerPort.Value = new decimal(new int[] {
            1025,
            0,
            0,
            0});
            this.numericUpDownExternalRestApiServerPort.ValueChanged += new System.EventHandler(this.numericUpDownExternalRestApiServerPort_ValueChanged);
            // 
            // textBoxExternalRestApiServerUrl
            // 
            this.textBoxExternalRestApiServerUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxExternalRestApiServerUrl.Location = new System.Drawing.Point(105, 19);
            this.textBoxExternalRestApiServerUrl.Name = "textBoxExternalRestApiServerUrl";
            this.textBoxExternalRestApiServerUrl.Size = new System.Drawing.Size(387, 20);
            this.textBoxExternalRestApiServerUrl.TabIndex = 2;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(13, 22);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(86, 13);
            this.label24.TabIndex = 1;
            this.label24.Text = "Адрес сервера:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.textBoxCipherDecryptionAlgorythm);
            this.groupBox4.Location = new System.Drawing.Point(11, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(484, 58);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Алгоритм для расшифровки Cipher";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(406, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Для скачивания некоторых видео потребуется ввести специальный алгоритм";
            // 
            // textBoxCipherDecryptionAlgorythm
            // 
            this.textBoxCipherDecryptionAlgorythm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCipherDecryptionAlgorythm.Location = new System.Drawing.Point(8, 32);
            this.textBoxCipherDecryptionAlgorythm.Name = "textBoxCipherDecryptionAlgorythm";
            this.textBoxCipherDecryptionAlgorythm.Size = new System.Drawing.Size(468, 20);
            this.textBoxCipherDecryptionAlgorythm.TabIndex = 0;
            this.textBoxCipherDecryptionAlgorythm.Leave += new System.EventHandler(this.textBoxCipherDecryptionAlgorythm_Leave);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.textBoxYouTubeApiV3Key);
            this.groupBox5.Location = new System.Drawing.Point(11, 82);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(484, 61);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Ключ от YouTube API V3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(352, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Некоторые функции программы не будут работать без этого ключа";
            // 
            // textBoxYouTubeApiV3Key
            // 
            this.textBoxYouTubeApiV3Key.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxYouTubeApiV3Key.Location = new System.Drawing.Point(6, 35);
            this.textBoxYouTubeApiV3Key.Name = "textBoxYouTubeApiV3Key";
            this.textBoxYouTubeApiV3Key.Size = new System.Drawing.Size(472, 20);
            this.textBoxYouTubeApiV3Key.TabIndex = 0;
            this.textBoxYouTubeApiV3Key.Leave += new System.EventHandler(this.textBoxYouTubeApiV3Key_Leave);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.numericUpDownConnectionTimeout);
            this.groupBox7.Controls.Add(this.label29);
            this.groupBox7.Controls.Add(this.checkBoxUseAccurateMultithreading);
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.numericUpDownGlobalThreadCountLimit);
            this.groupBox7.Controls.Add(this.panelWarningAudioThreads);
            this.groupBox7.Controls.Add(this.panelWarningVideoThreads);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.numericUpDownThreadCountAudio);
            this.groupBox7.Controls.Add(this.numericUpDownThreadCountVideo);
            this.groupBox7.Location = new System.Drawing.Point(6, 437);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(498, 142);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Потоки";
            // 
            // numericUpDownConnectionTimeout
            // 
            this.numericUpDownConnectionTimeout.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownConnectionTimeout.Location = new System.Drawing.Point(264, 113);
            this.numericUpDownConnectionTimeout.Maximum = new decimal(new int[] {
            120000,
            0,
            0,
            0});
            this.numericUpDownConnectionTimeout.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownConnectionTimeout.Name = "numericUpDownConnectionTimeout";
            this.numericUpDownConnectionTimeout.Size = new System.Drawing.Size(64, 20);
            this.numericUpDownConnectionTimeout.TabIndex = 10;
            this.numericUpDownConnectionTimeout.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numericUpDownConnectionTimeout.ValueChanged += new System.EventHandler(this.numericUpDownConnectionTimeout_ValueChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(5, 115);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(243, 13);
            this.label29.TabIndex = 9;
            this.label29.Text = "Время ожидания соединения (миллисекунды):";
            // 
            // checkBoxUseAccurateMultithreading
            // 
            this.checkBoxUseAccurateMultithreading.AutoSize = true;
            this.checkBoxUseAccurateMultithreading.Location = new System.Drawing.Point(11, 92);
            this.checkBoxUseAccurateMultithreading.Name = "checkBoxUseAccurateMultithreading";
            this.checkBoxUseAccurateMultithreading.Size = new System.Drawing.Size(124, 17);
            this.checkBoxUseAccurateMultithreading.TabIndex = 8;
            this.checkBoxUseAccurateMultithreading.Text = "Аккуратный режим";
            this.toolTip1.SetToolTip(this.checkBoxUseAccurateMultithreading, "Не влияет на уже начатые загрузки");
            this.checkBoxUseAccurateMultithreading.UseVisualStyleBackColor = true;
            this.checkBoxUseAccurateMultithreading.CheckedChanged += new System.EventHandler(this.checkBoxUseAccurateMultithreading_CheckedChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(252, 13);
            this.label13.TabIndex = 7;
            this.label13.Text = "Максимальное суммарное количество потоков:";
            this.toolTip1.SetToolTip(this.label13, "При достижении этого значения, новые потоки будут ждать завершения предыдущих, пр" +
        "ежде чем запуститься");
            // 
            // numericUpDownGlobalThreadCountLimit
            // 
            this.numericUpDownGlobalThreadCountLimit.Location = new System.Drawing.Point(263, 68);
            this.numericUpDownGlobalThreadCountLimit.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownGlobalThreadCountLimit.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownGlobalThreadCountLimit.Name = "numericUpDownGlobalThreadCountLimit";
            this.numericUpDownGlobalThreadCountLimit.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownGlobalThreadCountLimit.TabIndex = 6;
            this.numericUpDownGlobalThreadCountLimit.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownGlobalThreadCountLimit.ValueChanged += new System.EventHandler(this.numericUpDownGlobalThreadCountLimit_ValueChanged);
            // 
            // panelWarningAudioThreads
            // 
            this.panelWarningAudioThreads.BackgroundImage = global::YouTube_downloader.Properties.Resources.warning;
            this.panelWarningAudioThreads.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelWarningAudioThreads.Location = new System.Drawing.Point(310, 39);
            this.panelWarningAudioThreads.Name = "panelWarningAudioThreads";
            this.panelWarningAudioThreads.Size = new System.Drawing.Size(25, 25);
            this.panelWarningAudioThreads.TabIndex = 5;
            this.toolTip1.SetToolTip(this.panelWarningAudioThreads, "Слишком много потоков! Могут возникнуть проблемы со скачиванием!");
            this.panelWarningAudioThreads.Visible = false;
            // 
            // panelWarningVideoThreads
            // 
            this.panelWarningVideoThreads.BackgroundImage = global::YouTube_downloader.Properties.Resources.warning;
            this.panelWarningVideoThreads.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelWarningVideoThreads.Location = new System.Drawing.Point(310, 11);
            this.panelWarningVideoThreads.Name = "panelWarningVideoThreads";
            this.panelWarningVideoThreads.Size = new System.Drawing.Size(25, 25);
            this.panelWarningVideoThreads.TabIndex = 4;
            this.toolTip1.SetToolTip(this.panelWarningVideoThreads, "Слишком много потоков! Могут возникнуть проблемы со скачиванием!");
            this.panelWarningVideoThreads.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(228, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Количество потоков для скачивания аудио:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(229, 13);
            this.label9.TabIndex = 2;
            this.label9.Text = "Количество потоков для скачивания видео:";
            // 
            // numericUpDownThreadCountAudio
            // 
            this.numericUpDownThreadCountAudio.Location = new System.Drawing.Point(263, 41);
            this.numericUpDownThreadCountAudio.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownThreadCountAudio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownThreadCountAudio.Name = "numericUpDownThreadCountAudio";
            this.numericUpDownThreadCountAudio.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownThreadCountAudio.TabIndex = 1;
            this.numericUpDownThreadCountAudio.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownThreadCountAudio.ValueChanged += new System.EventHandler(this.numericUpDownThreadCountAudio_ValueChanged);
            // 
            // numericUpDownThreadCountVideo
            // 
            this.numericUpDownThreadCountVideo.Location = new System.Drawing.Point(264, 14);
            this.numericUpDownThreadCountVideo.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownThreadCountVideo.Name = "numericUpDownThreadCountVideo";
            this.numericUpDownThreadCountVideo.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownThreadCountVideo.TabIndex = 1;
            this.numericUpDownThreadCountVideo.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownThreadCountVideo.ValueChanged += new System.EventHandler(this.numericUpDownThreadCountVideo_ValueChanged);
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Controls.Add(this.panelSearch);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearch.Size = new System.Drawing.Size(547, 394);
            this.tabPageSearch.TabIndex = 1;
            this.tabPageSearch.Text = "Поиск";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // panelSearch
            // 
            this.panelSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearch.AutoScroll = true;
            this.panelSearch.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelSearch.Controls.Add(this.groupBox9);
            this.panelSearch.Controls.Add(this.groupBox3);
            this.panelSearch.Controls.Add(this.groupBox8);
            this.panelSearch.Controls.Add(this.groupBoxQuerySearch);
            this.panelSearch.Controls.Add(this.groupBox6);
            this.panelSearch.Controls.Add(this.groupBox2);
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(544, 393);
            this.panelSearch.TabIndex = 5;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnWtfWebPageCode);
            this.groupBox9.Controls.Add(this.label12);
            this.groupBox9.Controls.Add(this.btnSearchByWebPage);
            this.groupBox9.Controls.Add(this.richTextBoxWebPageCode);
            this.groupBox9.Location = new System.Drawing.Point(6, 268);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(515, 173);
            this.groupBox9.TabIndex = 5;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Поиск по коду веб-страницы";
            // 
            // btnWtfWebPageCode
            // 
            this.btnWtfWebPageCode.Location = new System.Drawing.Point(434, 10);
            this.btnWtfWebPageCode.Name = "btnWtfWebPageCode";
            this.btnWtfWebPageCode.Size = new System.Drawing.Size(75, 23);
            this.btnWtfWebPageCode.TabIndex = 3;
            this.btnWtfWebPageCode.Text = "Зачем?";
            this.btnWtfWebPageCode.UseVisualStyleBackColor = true;
            this.btnWtfWebPageCode.Click += new System.EventHandler(this.btnWtfWebPageCode_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(201, 13);
            this.label12.TabIndex = 2;
            this.label12.Text = "Вставьте сюда код страницы с видео:";
            // 
            // btnSearchByWebPage
            // 
            this.btnSearchByWebPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearchByWebPage.Location = new System.Drawing.Point(434, 144);
            this.btnSearchByWebPage.Name = "btnSearchByWebPage";
            this.btnSearchByWebPage.Size = new System.Drawing.Size(75, 23);
            this.btnSearchByWebPage.TabIndex = 1;
            this.btnSearchByWebPage.Text = "Искать";
            this.btnSearchByWebPage.UseVisualStyleBackColor = true;
            this.btnSearchByWebPage.Click += new System.EventHandler(this.btnSearchByWebPage_Click);
            // 
            // richTextBoxWebPageCode
            // 
            this.richTextBoxWebPageCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxWebPageCode.ContextMenuStrip = this.contextMenuCopyPaste;
            this.richTextBoxWebPageCode.Location = new System.Drawing.Point(9, 39);
            this.richTextBoxWebPageCode.Name = "richTextBoxWebPageCode";
            this.richTextBoxWebPageCode.Size = new System.Drawing.Size(500, 99);
            this.richTextBoxWebPageCode.TabIndex = 0;
            this.richTextBoxWebPageCode.Text = "";
            // 
            // contextMenuCopyPaste
            // 
            this.contextMenuCopyPaste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCutTextToolStripMenuItem,
            this.miCopyTextToolStripMenuItem,
            this.miPasteTextToolStripMenuItem,
            this.toolStripMenuItemSeparatorLine,
            this.miSelectAllTextToolStripMenuItem});
            this.contextMenuCopyPaste.Name = "menuCopyPaste";
            this.contextMenuCopyPaste.Size = new System.Drawing.Size(149, 98);
            this.contextMenuCopyPaste.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuCopyPaste_Opening);
            // 
            // miCutTextToolStripMenuItem
            // 
            this.miCutTextToolStripMenuItem.Name = "miCutTextToolStripMenuItem";
            this.miCutTextToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.miCutTextToolStripMenuItem.Text = "Вырезать";
            this.miCutTextToolStripMenuItem.Click += new System.EventHandler(this.miCutTextToolStripMenuItem_Click);
            // 
            // miCopyTextToolStripMenuItem
            // 
            this.miCopyTextToolStripMenuItem.Name = "miCopyTextToolStripMenuItem";
            this.miCopyTextToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.miCopyTextToolStripMenuItem.Text = "Копировать";
            this.miCopyTextToolStripMenuItem.Click += new System.EventHandler(this.miCopyTextToolStripMenuItem_Click);
            // 
            // miPasteTextToolStripMenuItem
            // 
            this.miPasteTextToolStripMenuItem.Name = "miPasteTextToolStripMenuItem";
            this.miPasteTextToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.miPasteTextToolStripMenuItem.Text = "Вставить";
            this.miPasteTextToolStripMenuItem.Click += new System.EventHandler(this.miPasteTextToolStripMenuItem_Click);
            // 
            // toolStripMenuItemSeparatorLine
            // 
            this.toolStripMenuItemSeparatorLine.Name = "toolStripMenuItemSeparatorLine";
            this.toolStripMenuItemSeparatorLine.Size = new System.Drawing.Size(145, 6);
            // 
            // miSelectAllTextToolStripMenuItem
            // 
            this.miSelectAllTextToolStripMenuItem.Name = "miSelectAllTextToolStripMenuItem";
            this.miSelectAllTextToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.miSelectAllTextToolStripMenuItem.Text = "Выделить всё";
            this.miSelectAllTextToolStripMenuItem.Click += new System.EventHandler(this.miSelectAllTextToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dateTimePickerSearchBefore);
            this.groupBox3.Controls.Add(this.dateTimePickerSearchAfter);
            this.groupBox3.Controls.Add(this.checkBoxSearchRangePublishedAfter);
            this.groupBox3.Controls.Add(this.checkBoxSearchRangePublishedBefore);
            this.groupBox3.Location = new System.Drawing.Point(310, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(198, 121);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Диапазон поиска";
            // 
            // dateTimePickerSearchBefore
            // 
            this.dateTimePickerSearchBefore.Location = new System.Drawing.Point(17, 91);
            this.dateTimePickerSearchBefore.Name = "dateTimePickerSearchBefore";
            this.dateTimePickerSearchBefore.Size = new System.Drawing.Size(152, 20);
            this.dateTimePickerSearchBefore.TabIndex = 4;
            // 
            // dateTimePickerSearchAfter
            // 
            this.dateTimePickerSearchAfter.Location = new System.Drawing.Point(17, 41);
            this.dateTimePickerSearchAfter.Name = "dateTimePickerSearchAfter";
            this.dateTimePickerSearchAfter.Size = new System.Drawing.Size(152, 20);
            this.dateTimePickerSearchAfter.TabIndex = 3;
            // 
            // checkBoxSearchRangePublishedAfter
            // 
            this.checkBoxSearchRangePublishedAfter.AutoSize = true;
            this.checkBoxSearchRangePublishedAfter.Checked = true;
            this.checkBoxSearchRangePublishedAfter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSearchRangePublishedAfter.Location = new System.Drawing.Point(17, 18);
            this.checkBoxSearchRangePublishedAfter.Name = "checkBoxSearchRangePublishedAfter";
            this.checkBoxSearchRangePublishedAfter.Size = new System.Drawing.Size(146, 17);
            this.checkBoxSearchRangePublishedAfter.TabIndex = 2;
            this.checkBoxSearchRangePublishedAfter.Text = "Опубликованные после";
            this.checkBoxSearchRangePublishedAfter.UseVisualStyleBackColor = true;
            // 
            // checkBoxSearchRangePublishedBefore
            // 
            this.checkBoxSearchRangePublishedBefore.AutoSize = true;
            this.checkBoxSearchRangePublishedBefore.Checked = true;
            this.checkBoxSearchRangePublishedBefore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSearchRangePublishedBefore.Location = new System.Drawing.Point(17, 68);
            this.checkBoxSearchRangePublishedBefore.Name = "checkBoxSearchRangePublishedBefore";
            this.checkBoxSearchRangePublishedBefore.Size = new System.Drawing.Size(128, 17);
            this.checkBoxSearchRangePublishedBefore.TabIndex = 1;
            this.checkBoxSearchRangePublishedBefore.Text = "Опубликованные до";
            this.checkBoxSearchRangePublishedBefore.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.checkBoxSearchVideos);
            this.groupBox8.Controls.Add(this.checkBoxSearchChannels);
            this.groupBox8.Location = new System.Drawing.Point(6, 204);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(293, 58);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "В результатах поиска выдавать";
            // 
            // checkBoxSearchVideos
            // 
            this.checkBoxSearchVideos.AutoSize = true;
            this.checkBoxSearchVideos.Checked = true;
            this.checkBoxSearchVideos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSearchVideos.Location = new System.Drawing.Point(92, 27);
            this.checkBoxSearchVideos.Name = "checkBoxSearchVideos";
            this.checkBoxSearchVideos.Size = new System.Drawing.Size(57, 17);
            this.checkBoxSearchVideos.TabIndex = 1;
            this.checkBoxSearchVideos.Text = "Видео";
            this.checkBoxSearchVideos.UseVisualStyleBackColor = true;
            this.checkBoxSearchVideos.CheckedChanged += new System.EventHandler(this.checkBoxSearchVideos_CheckedChanged);
            // 
            // checkBoxSearchChannels
            // 
            this.checkBoxSearchChannels.AutoSize = true;
            this.checkBoxSearchChannels.Checked = true;
            this.checkBoxSearchChannels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSearchChannels.Location = new System.Drawing.Point(21, 27);
            this.checkBoxSearchChannels.Name = "checkBoxSearchChannels";
            this.checkBoxSearchChannels.Size = new System.Drawing.Size(65, 17);
            this.checkBoxSearchChannels.TabIndex = 0;
            this.checkBoxSearchChannels.Text = "Каналы";
            this.checkBoxSearchChannels.UseVisualStyleBackColor = true;
            this.checkBoxSearchChannels.CheckedChanged += new System.EventHandler(this.checkBoxSearchChannels_CheckedChanged);
            // 
            // groupBoxQuerySearch
            // 
            this.groupBoxQuerySearch.Controls.Add(this.label2);
            this.groupBoxQuerySearch.Controls.Add(this.btnSearchByQuery);
            this.groupBoxQuerySearch.Controls.Add(this.textBoxSearchQuery);
            this.groupBoxQuerySearch.Location = new System.Drawing.Point(0, 6);
            this.groupBoxQuerySearch.Name = "groupBoxQuerySearch";
            this.groupBoxQuerySearch.Size = new System.Drawing.Size(299, 91);
            this.groupBoxQuerySearch.TabIndex = 0;
            this.groupBoxQuerySearch.TabStop = false;
            this.groupBoxQuerySearch.Text = "Поиск по запросу";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Введите поисковый запрос:";
            // 
            // btnSearchByQuery
            // 
            this.btnSearchByQuery.Location = new System.Drawing.Point(219, 61);
            this.btnSearchByQuery.Name = "btnSearchByQuery";
            this.btnSearchByQuery.Size = new System.Drawing.Size(74, 24);
            this.btnSearchByQuery.TabIndex = 1;
            this.btnSearchByQuery.Text = "Искать";
            this.btnSearchByQuery.UseCompatibleTextRendering = true;
            this.btnSearchByQuery.UseVisualStyleBackColor = true;
            this.btnSearchByQuery.Click += new System.EventHandler(this.btnSearchByQuery_Click);
            // 
            // textBoxSearchQuery
            // 
            this.textBoxSearchQuery.Location = new System.Drawing.Point(15, 35);
            this.textBoxSearchQuery.Name = "textBoxSearchQuery";
            this.textBoxSearchQuery.Size = new System.Drawing.Size(278, 20);
            this.textBoxSearchQuery.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButtonSearchResultCountLimitUserDefinedNumber);
            this.groupBox6.Controls.Add(this.radioButtonSearchResultCountLimitMaxPossible);
            this.groupBox6.Controls.Add(this.numericUpDownSearchResultCountLimit);
            this.groupBox6.Location = new System.Drawing.Point(310, 133);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(198, 65);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Количество результатов выдачи";
            // 
            // radioButtonSearchResultCountLimitUserDefinedNumber
            // 
            this.radioButtonSearchResultCountLimitUserDefinedNumber.AutoCheck = false;
            this.radioButtonSearchResultCountLimitUserDefinedNumber.AutoSize = true;
            this.radioButtonSearchResultCountLimitUserDefinedNumber.Location = new System.Drawing.Point(22, 40);
            this.radioButtonSearchResultCountLimitUserDefinedNumber.Name = "radioButtonSearchResultCountLimitUserDefinedNumber";
            this.radioButtonSearchResultCountLimitUserDefinedNumber.Size = new System.Drawing.Size(62, 17);
            this.radioButtonSearchResultCountLimitUserDefinedNumber.TabIndex = 2;
            this.radioButtonSearchResultCountLimitUserDefinedNumber.Text = "Только";
            this.radioButtonSearchResultCountLimitUserDefinedNumber.UseVisualStyleBackColor = true;
            this.radioButtonSearchResultCountLimitUserDefinedNumber.Click += new System.EventHandler(this.radioButtonSearchResultCountLimitUserDefinedNumber_Click);
            // 
            // radioButtonSearchResultCountLimitMaxPossible
            // 
            this.radioButtonSearchResultCountLimitMaxPossible.AutoCheck = false;
            this.radioButtonSearchResultCountLimitMaxPossible.AutoSize = true;
            this.radioButtonSearchResultCountLimitMaxPossible.Checked = true;
            this.radioButtonSearchResultCountLimitMaxPossible.Location = new System.Drawing.Point(22, 17);
            this.radioButtonSearchResultCountLimitMaxPossible.Name = "radioButtonSearchResultCountLimitMaxPossible";
            this.radioButtonSearchResultCountLimitMaxPossible.Size = new System.Drawing.Size(106, 17);
            this.radioButtonSearchResultCountLimitMaxPossible.TabIndex = 1;
            this.radioButtonSearchResultCountLimitMaxPossible.TabStop = true;
            this.radioButtonSearchResultCountLimitMaxPossible.Text = "Максимум (500)";
            this.radioButtonSearchResultCountLimitMaxPossible.UseVisualStyleBackColor = true;
            this.radioButtonSearchResultCountLimitMaxPossible.Click += new System.EventHandler(this.radioButtonSearchResultCountLimitMaxPossible_Click);
            // 
            // numericUpDownSearchResultCountLimit
            // 
            this.numericUpDownSearchResultCountLimit.Location = new System.Drawing.Point(90, 40);
            this.numericUpDownSearchResultCountLimit.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownSearchResultCountLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSearchResultCountLimit.Name = "numericUpDownSearchResultCountLimit";
            this.numericUpDownSearchResultCountLimit.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownSearchResultCountLimit.TabIndex = 0;
            this.numericUpDownSearchResultCountLimit.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownSearchResultCountLimit.ValueChanged += new System.EventHandler(this.numericUpDownSearchResultCountLimit_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxVideoUrlOrId);
            this.groupBox2.Controls.Add(this.btnSearchByVideoUrlOrId);
            this.groupBox2.Location = new System.Drawing.Point(0, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(299, 95);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Поиск видео по ссылке или ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Введите ссылку на видео или его ID:";
            // 
            // textBoxVideoUrlOrId
            // 
            this.textBoxVideoUrlOrId.Location = new System.Drawing.Point(15, 39);
            this.textBoxVideoUrlOrId.Name = "textBoxVideoUrlOrId";
            this.textBoxVideoUrlOrId.Size = new System.Drawing.Size(278, 20);
            this.textBoxVideoUrlOrId.TabIndex = 1;
            // 
            // btnSearchByVideoUrlOrId
            // 
            this.btnSearchByVideoUrlOrId.Location = new System.Drawing.Point(219, 65);
            this.btnSearchByVideoUrlOrId.Name = "btnSearchByVideoUrlOrId";
            this.btnSearchByVideoUrlOrId.Size = new System.Drawing.Size(74, 23);
            this.btnSearchByVideoUrlOrId.TabIndex = 0;
            this.btnSearchByVideoUrlOrId.Text = "Искать";
            this.btnSearchByVideoUrlOrId.UseVisualStyleBackColor = true;
            this.btnSearchByVideoUrlOrId.Click += new System.EventHandler(this.btnSearchByVideoUrlOrId_Click);
            // 
            // tabPageSearchResults
            // 
            this.tabPageSearchResults.Controls.Add(this.scrollBarSearchResults);
            this.tabPageSearchResults.Controls.Add(this.panelSearchResults);
            this.tabPageSearchResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageSearchResults.Name = "tabPageSearchResults";
            this.tabPageSearchResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearchResults.Size = new System.Drawing.Size(547, 394);
            this.tabPageSearchResults.TabIndex = 2;
            this.tabPageSearchResults.Text = "Результаты поиска";
            this.tabPageSearchResults.UseVisualStyleBackColor = true;
            // 
            // scrollBarSearchResults
            // 
            this.scrollBarSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollBarSearchResults.Location = new System.Drawing.Point(527, 0);
            this.scrollBarSearchResults.Name = "scrollBarSearchResults";
            this.scrollBarSearchResults.Size = new System.Drawing.Size(18, 393);
            this.scrollBarSearchResults.TabIndex = 1;
            this.scrollBarSearchResults.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollBarSearchResults_Scroll);
            // 
            // panelSearchResults
            // 
            this.panelSearchResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSearchResults.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelSearchResults.Location = new System.Drawing.Point(0, 0);
            this.panelSearchResults.Name = "panelSearchResults";
            this.panelSearchResults.Size = new System.Drawing.Size(527, 392);
            this.panelSearchResults.TabIndex = 0;
            this.panelSearchResults.Resize += new System.EventHandler(this.panelSearchResults_Resize);
            // 
            // objectTreeViewFavorites
            // 
            this.objectTreeViewFavorites.AllColumns.Add(this.olvColumn1);
            this.objectTreeViewFavorites.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectTreeViewFavorites.CellEditUseWholeCell = false;
            this.objectTreeViewFavorites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.objectTreeViewFavorites.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.objectTreeViewFavorites.HideSelection = false;
            this.objectTreeViewFavorites.Location = new System.Drawing.Point(569, 20);
            this.objectTreeViewFavorites.Name = "objectTreeViewFavorites";
            this.objectTreeViewFavorites.ShowGroups = false;
            this.objectTreeViewFavorites.Size = new System.Drawing.Size(244, 412);
            this.objectTreeViewFavorites.TabIndex = 1;
            this.objectTreeViewFavorites.UseCompatibleStateImageBehavior = false;
            this.objectTreeViewFavorites.View = System.Windows.Forms.View.Details;
            this.objectTreeViewFavorites.VirtualMode = true;
            this.objectTreeViewFavorites.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.objectTreeViewFavorites_CellRightClick);
            this.objectTreeViewFavorites.ItemsRemoving += new System.EventHandler<BrightIdeasSoftware.ItemsRemovingEventArgs>(this.objectTreeViewFavorites_ItemsRemoving);
            this.objectTreeViewFavorites.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.objectTreeViewFavorites_MouseDoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "DisplayName";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Width = 25;
            // 
            // contextMenuFavorites
            // 
            this.contextMenuFavorites.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpenVideoInBrowserToolStripMenuItem,
            this.miCopyVideoUrlToolStripMenuItem,
            this.miOpenChannelInBrowserToolStripMenuItem,
            this.miCopyChannelUrlToolStripMenuItem,
            this.miCopyDisplayNameToolStripMenuItem,
            this.miCopyVideoIdToolStripMenuItem,
            this.miCopyChannelIdToolStripMenuItem,
            this.miCopyDisplayNameWithIdToolStripMenuItem});
            this.contextMenuFavorites.Name = "menuFavarites";
            this.contextMenuFavorites.Size = new System.Drawing.Size(281, 180);
            this.contextMenuFavorites.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuFavorites_Opening);
            // 
            // miOpenVideoInBrowserToolStripMenuItem
            // 
            this.miOpenVideoInBrowserToolStripMenuItem.Name = "miOpenVideoInBrowserToolStripMenuItem";
            this.miOpenVideoInBrowserToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.miOpenVideoInBrowserToolStripMenuItem.Text = "Открыть видео в браузере";
            this.miOpenVideoInBrowserToolStripMenuItem.Click += new System.EventHandler(this.miOpenVideoInBrowserToolStripMenuItem_Click);
            // 
            // miCopyVideoUrlToolStripMenuItem
            // 
            this.miCopyVideoUrlToolStripMenuItem.Name = "miCopyVideoUrlToolStripMenuItem";
            this.miCopyVideoUrlToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.miCopyVideoUrlToolStripMenuItem.Text = "Скопировать ссылку на видео";
            this.miCopyVideoUrlToolStripMenuItem.Click += new System.EventHandler(this.miCopyVideoUrlToolStripMenuItem_Click);
            // 
            // miOpenChannelInBrowserToolStripMenuItem
            // 
            this.miOpenChannelInBrowserToolStripMenuItem.Name = "miOpenChannelInBrowserToolStripMenuItem";
            this.miOpenChannelInBrowserToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.miOpenChannelInBrowserToolStripMenuItem.Text = "Открыть канал в браузере";
            this.miOpenChannelInBrowserToolStripMenuItem.Click += new System.EventHandler(this.miOpenChannelInBrowserToolStripMenuItem_Click);
            // 
            // miCopyChannelUrlToolStripMenuItem
            // 
            this.miCopyChannelUrlToolStripMenuItem.Name = "miCopyChannelUrlToolStripMenuItem";
            this.miCopyChannelUrlToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.miCopyChannelUrlToolStripMenuItem.Text = "Скопировать ссылку на канал";
            this.miCopyChannelUrlToolStripMenuItem.Click += new System.EventHandler(this.miCopyChannelUrlToolStripMenuItem_Click);
            // 
            // miCopyDisplayNameToolStripMenuItem
            // 
            this.miCopyDisplayNameToolStripMenuItem.Name = "miCopyDisplayNameToolStripMenuItem";
            this.miCopyDisplayNameToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.miCopyDisplayNameToolStripMenuItem.Text = "Скопировать отображаемое имя";
            this.miCopyDisplayNameToolStripMenuItem.Click += new System.EventHandler(this.miCopyDisplayNameToolStripMenuItem_Click);
            // 
            // miCopyVideoIdToolStripMenuItem
            // 
            this.miCopyVideoIdToolStripMenuItem.Name = "miCopyVideoIdToolStripMenuItem";
            this.miCopyVideoIdToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.miCopyVideoIdToolStripMenuItem.Text = "Скопировать ID видео";
            this.miCopyVideoIdToolStripMenuItem.Click += new System.EventHandler(this.miCopyVideoIdToolStripMenuItem_Click);
            // 
            // miCopyChannelIdToolStripMenuItem
            // 
            this.miCopyChannelIdToolStripMenuItem.Name = "miCopyChannelIdToolStripMenuItem";
            this.miCopyChannelIdToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.miCopyChannelIdToolStripMenuItem.Text = "Скопировать ID канала";
            this.miCopyChannelIdToolStripMenuItem.Click += new System.EventHandler(this.miCopyChannelIdToolStripMenuItem_Click);
            // 
            // miCopyDisplayNameWithIdToolStripMenuItem
            // 
            this.miCopyDisplayNameWithIdToolStripMenuItem.Name = "miCopyDisplayNameWithIdToolStripMenuItem";
            this.miCopyDisplayNameWithIdToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.miCopyDisplayNameWithIdToolStripMenuItem.Text = "Скопировать отображаемое имя и ID";
            this.miCopyDisplayNameWithIdToolStripMenuItem.Click += new System.EventHandler(this.miCopyDisplayNameWithIdToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 439);
            this.Controls.Add(this.objectTreeViewFavorites);
            this.Controls.Add(this.tabControlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(830, 440);
            this.Name = "Form1";
            this.Text = "Скачивалка с ютуба";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabControlSettings.ResumeLayout(false);
            this.tabPageFilesAndFolders.ResumeLayout(false);
            this.tabPageFilesAndFolders.PerformLayout();
            this.tabPageGUI.ResumeLayout(false);
            this.tabPageGUI.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBoxFontSettings.ResumeLayout(false);
            this.groupBoxFontSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoTitleFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFavoritesListFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMenusFontSize)).EndInit();
            this.tabPageDownloadSettings.ResumeLayout(false);
            this.tabPageDownloadSettings.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChunkDownloadErrorCountLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownChunkDownloadTryCountLimit)).EndInit();
            this.groupBoxAdaptiveFormatsSettings.ResumeLayout(false);
            this.groupBoxAdaptiveFormatsSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayAfterContainerCreated)).EndInit();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.tabPageSystemSettings.ResumeLayout(false);
            this.tabPageSystemSettings.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDashChunkDownloadTryCountLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDashChunkSize)).EndInit();
            this.groupBox13.ResumeLayout(false);
            this.groupBoxYouTubeApiSettings.ResumeLayout(false);
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConnectionTimeoutExternalRestApiServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownExternalRestApiServerPort)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownConnectionTimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGlobalThreadCountLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadCountAudio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadCountVideo)).EndInit();
            this.tabPageSearch.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.contextMenuCopyPaste.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBoxQuerySearch.ResumeLayout(false);
            this.groupBoxQuerySearch.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSearchResultCountLimit)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPageSearchResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectTreeViewFavorites)).EndInit();
            this.contextMenuFavorites.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControlMain;
		private System.Windows.Forms.TabPage tabPageSettings;
		private System.Windows.Forms.TabPage tabPageSearch;
		private System.Windows.Forms.TabPage tabPageSearchResults;
		private System.Windows.Forms.GroupBox groupBoxQuerySearch;
		private System.Windows.Forms.Button btnSearchByQuery;
		private System.Windows.Forms.TextBox textBoxSearchQuery;
		private System.Windows.Forms.Panel panelSearchResults;
		private BrightIdeasSoftware.TreeListView objectTreeViewFavorites;
		private BrightIdeasSoftware.OLVColumn olvColumn1;
		private System.Windows.Forms.Button btnBrowseTempDirectory;
		private System.Windows.Forms.Button btnBrowseDownloadDirectory;
		private System.Windows.Forms.TextBox textBoxTempDirectory;
		private System.Windows.Forms.TextBox textBoxDownloadDirectory;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBoxVideoUrlOrId;
		private System.Windows.Forms.Button btnSearchByVideoUrlOrId;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DateTimePicker dateTimePickerSearchBefore;
		private System.Windows.Forms.DateTimePicker dateTimePickerSearchAfter;
		private System.Windows.Forms.CheckBox checkBoxSearchRangePublishedAfter;
		private System.Windows.Forms.CheckBox checkBoxSearchRangePublishedBefore;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox textBoxCipherDecryptionAlgorythm;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox textBoxYouTubeApiV3Key;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.NumericUpDown numericUpDownSearchResultCountLimit;
		private System.Windows.Forms.RadioButton radioButtonSearchResultCountLimitUserDefinedNumber;
		private System.Windows.Forms.RadioButton radioButtonSearchResultCountLimitMaxPossible;
		private System.Windows.Forms.Button btnSelectWebBrowserFilePath;
		private System.Windows.Forms.TextBox textBoxWebBrowserFilePath;
		private System.Windows.Forms.ContextMenuStrip contextMenuFavorites;
		private System.Windows.Forms.ToolStripMenuItem miOpenVideoInBrowserToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miOpenChannelInBrowserToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControlSettings;
		private System.Windows.Forms.TabPage tabPageFilesAndFolders;
		private System.Windows.Forms.TextBox textBoxOutputFileNameFormatWithoutDate;
		private System.Windows.Forms.Button btnResetFileNameFormatWithoutDate;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TabPage tabPageSystemSettings;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.NumericUpDown numericUpDownThreadCountAudio;
		private System.Windows.Forms.NumericUpDown numericUpDownThreadCountVideo;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnBrowseFfmpegFilePath;
		private System.Windows.Forms.TextBox textBoxFfmpegFilePath;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.CheckBox checkBoxSearchVideos;
		private System.Windows.Forms.CheckBox checkBoxSearchChannels;
		private System.Windows.Forms.TabPage tabPageDownloadSettings;
		private System.Windows.Forms.CheckBox checkBoxDeleteSourceFilesWhenMerged;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyMergeAdaptiveTracks;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallySaveVideoThumbnailImage;
		private System.Windows.Forms.Panel panelSearch;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button btnSearchByWebPage;
		private System.Windows.Forms.RichTextBox richTextBoxWebPageCode;
		private System.Windows.Forms.ContextMenuStrip contextMenuCopyPaste;
		private System.Windows.Forms.ToolStripMenuItem miCutTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miPasteTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItemSeparatorLine;
		private System.Windows.Forms.ToolStripMenuItem miSelectAllTextToolStripMenuItem;
		private System.Windows.Forms.Panel panelWarningVideoThreads;
		private System.Windows.Forms.Panel panelWarningAudioThreads;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown numericUpDownGlobalThreadCountLimit;
		private System.Windows.Forms.GroupBox groupBoxAdaptiveFormatsSettings;
		private System.Windows.Forms.ToolStripMenuItem miCopyDisplayNameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyDisplayNameWithIdToolStripMenuItem;
		private System.Windows.Forms.VScrollBar scrollBarSearchResults;
		private System.Windows.Forms.Button btnWtfWebPageCode;
		private System.Windows.Forms.TextBox textBoxFilesMergerDirectory;
		private System.Windows.Forms.Button btnBrowseMergerDirectory;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button btnWtfMergerDirectory;
		private System.Windows.Forms.GroupBox groupBoxYouTubeApiSettings;
		private System.Windows.Forms.ToolStripMenuItem miCopyVideoUrlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyChannelUrlToolStripMenuItem;
		private System.Windows.Forms.TabPage tabPageGUI;
		private System.Windows.Forms.GroupBox groupBoxFontSettings;
		private System.Windows.Forms.NumericUpDown numericUpDownMenusFontSize;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.NumericUpDown numericUpDownFavoritesListFontSize;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.NumericUpDown numericUpDownVideoTitleFontSize;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.GroupBox groupBox12;
		private System.Windows.Forms.CheckBox checkBoxSortAdaptiveFormatsByFileSize;
		private System.Windows.Forms.CheckBox checkBoxMoveAudioTrackId140ToTopOfList;
		private System.Windows.Forms.CheckBox checkBoxUseRamForTempFiles;
		private System.Windows.Forms.GroupBox groupBox13;
		private System.Windows.Forms.Panel panelRAM;
		private System.Windows.Forms.Button btnWtfUseRam;
		private System.Windows.Forms.GroupBox groupBox14;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyDownloadFirstAudioTrack;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyDownloadSecondAudioTrack;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks;
		private System.Windows.Forms.GroupBox groupBox15;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks;
		private System.Windows.Forms.Button btnWtfDownloadAllAdaptiveVideoTracks;
		private System.Windows.Forms.GroupBox groupBox16;
		private System.Windows.Forms.RadioButton radioButtonContainerTypeMkv;
		private System.Windows.Forms.RadioButton radioButtonContainerTypeMp4;
		private System.Windows.Forms.CheckBox checkBoxSortDashFormatsByBitrate;
		private System.Windows.Forms.ToolStripMenuItem miCopyVideoIdToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyChannelIdToolStripMenuItem;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox textBoxOutputFileNameFormatWithDate;
		private System.Windows.Forms.Button btnResetFileNameFormatWithDate;
		private System.Windows.Forms.CheckBox checkBoxUseUniversalTime;
		private System.Windows.Forms.NumericUpDown numericUpDownDelayAfterContainerCreated;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.NumericUpDown numericUpDownChunkDownloadTryCountLimit;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.GroupBox groupBox17;
		private System.Windows.Forms.CheckBox checkBoxAlwaysDownloadAsDash;
		private System.Windows.Forms.Label lblActualDashChunkSize;
		private System.Windows.Forms.NumericUpDown numericUpDownDashChunkSize;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Button btnWtfAlwaysDownloadAsDash;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.NumericUpDown numericUpDownDashChunkDownloadTryCountLimit;
		private System.Windows.Forms.GroupBox groupBox18;
		private System.Windows.Forms.CheckBox checkBoxUseExternalRestApiServerToGetAdultVideos;
		private System.Windows.Forms.NumericUpDown numericUpDownExternalRestApiServerPort;
		private System.Windows.Forms.TextBox textBoxExternalRestApiServerUrl;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Button btnWtfExternalRestApiServer;
		private System.Windows.Forms.CheckBox checkBoxUseExternalRestApiServerToGetBasicVideoInfo;
		private System.Windows.Forms.GroupBox groupBox19;
		private System.Windows.Forms.NumericUpDown numericUpDownChunkDownloadErrorCountLimit;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.CheckBox checkBoxUseAccurateMultithreading;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.CheckBox checkBoxCheckUrlsAccessibilityBeforeDownloadStarted;
		private System.Windows.Forms.NumericUpDown numericUpDownConnectionTimeout;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.NumericUpDown numericUpDownConnectionTimeoutExternalRestApiServer;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.GroupBox groupBox20;
		private System.Windows.Forms.TextBox textBoxUserAgent;
		private System.Windows.Forms.Button btnWtfUserAgent;
		private System.Windows.Forms.Button btnRestoreDefaultUserAgent;
		private System.Windows.Forms.CheckBox checkBoxShowHlsTracksOnlyForStreams;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.CheckBox checkBoxUseExternalRestApiServerToGetDownloadUrls;
    }
}

