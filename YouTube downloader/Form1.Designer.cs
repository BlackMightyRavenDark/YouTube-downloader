
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
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageFilesAndFolders = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.editOutputFileNameFormatWithDate = new System.Windows.Forms.TextBox();
            this.btnResetFileNameFormatWithDate = new System.Windows.Forms.Button();
            this.btnQ = new System.Windows.Forms.Button();
            this.editMergingDirPath = new System.Windows.Forms.TextBox();
            this.btnSelectMergingPath = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnBrowseFfmpeg = new System.Windows.Forms.Button();
            this.editFfmpeg = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.editOutputFileNameFormatWithoutDate = new System.Windows.Forms.TextBox();
            this.btnResetFileNameFormatWithoutDate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBrowseDowloadingPath = new System.Windows.Forms.Button();
            this.btnSelectBrowser = new System.Windows.Forms.Button();
            this.editDownloadingDirPath = new System.Windows.Forms.TextBox();
            this.editBrowser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.editTempDirPath = new System.Windows.Forms.TextBox();
            this.btnBrowseTempPath = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPageGUI = new System.Windows.Forms.TabPage();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.chkSortDashFormatsByBitrate = new System.Windows.Forms.CheckBox();
            this.chkMoveAudioId140First = new System.Windows.Forms.CheckBox();
            this.chkSortFormatsByFileSize = new System.Windows.Forms.CheckBox();
            this.groupBoxFonts = new System.Windows.Forms.GroupBox();
            this.numericUpDownVideoTitleFontSize = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownFavoritesListFontSize = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDownMenusFontSize = new System.Windows.Forms.NumericUpDown();
            this.tabPageDownloadingSettings = new System.Windows.Forms.TabPage();
            this.numericUpDownDownloadRetryCount = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.numericUpDownDelayAfterContainerCreated = new System.Windows.Forms.NumericUpDown();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnDownloadAllAdaptiveVideoTracksWtf = new System.Windows.Forms.Button();
            this.chkDownloadAllAdaptiveVideoTracks = new System.Windows.Forms.CheckBox();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.radioButtonContainerTypeMkv = new System.Windows.Forms.RadioButton();
            this.radioButtonContainerTypeMp4 = new System.Windows.Forms.RadioButton();
            this.chkMergeAdaptive = new System.Windows.Forms.CheckBox();
            this.chkDeleteSourceFiles = new System.Windows.Forms.CheckBox();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.chkDownloadFirstAudioTrack = new System.Windows.Forms.CheckBox();
            this.chkIfOnlyBiggerFileSize = new System.Windows.Forms.CheckBox();
            this.chkDownloadSecondAudioTrack = new System.Windows.Forms.CheckBox();
            this.chkDownloadAllAudioTracks = new System.Windows.Forms.CheckBox();
            this.chkSaveImage = new System.Windows.Forms.CheckBox();
            this.tabPageSystemSettings = new System.Windows.Forms.TabPage();
            this.checkBoxUseGmtTime = new System.Windows.Forms.CheckBox();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.btnUseRamWhy = new System.Windows.Forms.Button();
            this.panelRAM = new System.Windows.Forms.Panel();
            this.chkUseRamForTempFiles = new System.Windows.Forms.CheckBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.btnApiWtf = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.editCipherDecryptionAlgo = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.editYouTubeApiKey = new System.Windows.Forms.TextBox();
            this.chkUseHiddenApiForGettingInfo = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownGlobalThreadsMaximum = new System.Windows.Forms.NumericUpDown();
            this.panelWarningAudioThreads = new System.Windows.Forms.Panel();
            this.panelWarningVideoThreads = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownThreadsAudio = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownThreadsVideo = new System.Windows.Forms.NumericUpDown();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.btnWhy = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSearchByWebPage = new System.Windows.Forms.Button();
            this.richTextBoxWebPage = new System.Windows.Forms.RichTextBox();
            this.menuCopyPaste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerBefore = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerAfter = new System.Windows.Forms.DateTimePicker();
            this.chkPublishedAfter = new System.Windows.Forms.CheckBox();
            this.chkPublishedBefore = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.chkSearchVideos = new System.Windows.Forms.CheckBox();
            this.chkSearchChannels = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearchByQuery = new System.Windows.Forms.Button();
            this.editQuery = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.rbSearchResultsUserDefined = new System.Windows.Forms.RadioButton();
            this.rbSearchResultsMax = new System.Windows.Forms.RadioButton();
            this.numericUpDownSearchResult = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.editSearchUrl = new System.Windows.Forms.TextBox();
            this.btnSearchByUrl = new System.Windows.Forms.Button();
            this.tabPageSearchResults = new System.Windows.Forms.TabPage();
            this.scrollBarSearchResults = new System.Windows.Forms.VScrollBar();
            this.panelSearchResults = new System.Windows.Forms.Panel();
            this.tvFavorites = new BrightIdeasSoftware.TreeListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.menuFavorites = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openVideoInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyVideoUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openChannelInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyChannelUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDisplayNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyVideoIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyChannelIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDisplayNameWithIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControlMain.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPageFilesAndFolders.SuspendLayout();
            this.tabPageGUI.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBoxFonts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoTitleFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFavoritesListFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMenusFontSize)).BeginInit();
            this.tabPageDownloadingSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDownloadRetryCount)).BeginInit();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayAfterContainerCreated)).BeginInit();
            this.groupBox15.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.tabPageSystemSettings.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGlobalThreadsMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadsAudio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadsVideo)).BeginInit();
            this.tabPageSearch.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.menuCopyPaste.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSearchResult)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabPageSearchResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tvFavorites)).BeginInit();
            this.menuFavorites.SuspendLayout();
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
            this.tabPageSettings.Controls.Add(this.tabControl2);
            this.tabPageSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSettings.Name = "tabPageSettings";
            this.tabPageSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSettings.Size = new System.Drawing.Size(547, 394);
            this.tabPageSettings.TabIndex = 0;
            this.tabPageSettings.Text = "Настройки";
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.tabPageFilesAndFolders);
            this.tabControl2.Controls.Add(this.tabPageGUI);
            this.tabControl2.Controls.Add(this.tabPageDownloadingSettings);
            this.tabControl2.Controls.Add(this.tabPageSystemSettings);
            this.tabControl2.Location = new System.Drawing.Point(6, 8);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(535, 383);
            this.tabControl2.TabIndex = 12;
            // 
            // tabPageFilesAndFolders
            // 
            this.tabPageFilesAndFolders.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPageFilesAndFolders.Controls.Add(this.label18);
            this.tabPageFilesAndFolders.Controls.Add(this.editOutputFileNameFormatWithDate);
            this.tabPageFilesAndFolders.Controls.Add(this.btnResetFileNameFormatWithDate);
            this.tabPageFilesAndFolders.Controls.Add(this.btnQ);
            this.tabPageFilesAndFolders.Controls.Add(this.editMergingDirPath);
            this.tabPageFilesAndFolders.Controls.Add(this.btnSelectMergingPath);
            this.tabPageFilesAndFolders.Controls.Add(this.label14);
            this.tabPageFilesAndFolders.Controls.Add(this.label11);
            this.tabPageFilesAndFolders.Controls.Add(this.btnBrowseFfmpeg);
            this.tabPageFilesAndFolders.Controls.Add(this.editFfmpeg);
            this.tabPageFilesAndFolders.Controls.Add(this.label8);
            this.tabPageFilesAndFolders.Controls.Add(this.editOutputFileNameFormatWithoutDate);
            this.tabPageFilesAndFolders.Controls.Add(this.btnResetFileNameFormatWithoutDate);
            this.tabPageFilesAndFolders.Controls.Add(this.label7);
            this.tabPageFilesAndFolders.Controls.Add(this.btnBrowseDowloadingPath);
            this.tabPageFilesAndFolders.Controls.Add(this.btnSelectBrowser);
            this.tabPageFilesAndFolders.Controls.Add(this.editDownloadingDirPath);
            this.tabPageFilesAndFolders.Controls.Add(this.editBrowser);
            this.tabPageFilesAndFolders.Controls.Add(this.label3);
            this.tabPageFilesAndFolders.Controls.Add(this.editTempDirPath);
            this.tabPageFilesAndFolders.Controls.Add(this.btnBrowseTempPath);
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
            // editOutputFileNameFormatWithDate
            // 
            this.editOutputFileNameFormatWithDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editOutputFileNameFormatWithDate.Location = new System.Drawing.Point(13, 142);
            this.editOutputFileNameFormatWithDate.Name = "editOutputFileNameFormatWithDate";
            this.editOutputFileNameFormatWithDate.Size = new System.Drawing.Size(392, 20);
            this.editOutputFileNameFormatWithDate.TabIndex = 24;
            this.editOutputFileNameFormatWithDate.TextChanged += new System.EventHandler(this.editOutputFileNameFormatWithDate_TextChanged);
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
            // btnQ
            // 
            this.btnQ.Location = new System.Drawing.Point(182, 84);
            this.btnQ.Name = "btnQ";
            this.btnQ.Size = new System.Drawing.Size(21, 19);
            this.btnQ.TabIndex = 22;
            this.btnQ.Text = "?";
            this.btnQ.UseVisualStyleBackColor = true;
            this.btnQ.Click += new System.EventHandler(this.btnQ_Click);
            // 
            // editMergingDirPath
            // 
            this.editMergingDirPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editMergingDirPath.Location = new System.Drawing.Point(13, 103);
            this.editMergingDirPath.Name = "editMergingDirPath";
            this.editMergingDirPath.Size = new System.Drawing.Size(470, 20);
            this.editMergingDirPath.TabIndex = 21;
            this.editMergingDirPath.Leave += new System.EventHandler(this.editMergingDirPath_Leave);
            // 
            // btnSelectMergingPath
            // 
            this.btnSelectMergingPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectMergingPath.Location = new System.Drawing.Point(489, 103);
            this.btnSelectMergingPath.Name = "btnSelectMergingPath";
            this.btnSelectMergingPath.Size = new System.Drawing.Size(32, 23);
            this.btnSelectMergingPath.TabIndex = 20;
            this.btnSelectMergingPath.Text = "...";
            this.btnSelectMergingPath.UseVisualStyleBackColor = true;
            this.btnSelectMergingPath.Click += new System.EventHandler(this.btnSelectMergingDirPath_Click);
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
            // btnBrowseFfmpeg
            // 
            this.btnBrowseFfmpeg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFfmpeg.Location = new System.Drawing.Point(489, 259);
            this.btnBrowseFfmpeg.Name = "btnBrowseFfmpeg";
            this.btnBrowseFfmpeg.Size = new System.Drawing.Size(32, 20);
            this.btnBrowseFfmpeg.TabIndex = 17;
            this.btnBrowseFfmpeg.Text = "...";
            this.btnBrowseFfmpeg.UseVisualStyleBackColor = true;
            this.btnBrowseFfmpeg.Click += new System.EventHandler(this.btnBrowseFfmpeg_Click);
            // 
            // editFfmpeg
            // 
            this.editFfmpeg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editFfmpeg.Location = new System.Drawing.Point(13, 259);
            this.editFfmpeg.Name = "editFfmpeg";
            this.editFfmpeg.Size = new System.Drawing.Size(470, 20);
            this.editFfmpeg.TabIndex = 16;
            this.editFfmpeg.Leave += new System.EventHandler(this.editFfmpeg_Leave);
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
            // editOutputFileNameFormatWithoutDate
            // 
            this.editOutputFileNameFormatWithoutDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editOutputFileNameFormatWithoutDate.Location = new System.Drawing.Point(13, 181);
            this.editOutputFileNameFormatWithoutDate.Name = "editOutputFileNameFormatWithoutDate";
            this.editOutputFileNameFormatWithoutDate.Size = new System.Drawing.Size(392, 20);
            this.editOutputFileNameFormatWithoutDate.TabIndex = 14;
            this.editOutputFileNameFormatWithoutDate.TextChanged += new System.EventHandler(this.editOutputFileNameFormatWithoutDate_TextChanged);
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
            // btnBrowseDowloadingPath
            // 
            this.btnBrowseDowloadingPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseDowloadingPath.Location = new System.Drawing.Point(489, 23);
            this.btnBrowseDowloadingPath.Name = "btnBrowseDowloadingPath";
            this.btnBrowseDowloadingPath.Size = new System.Drawing.Size(32, 20);
            this.btnBrowseDowloadingPath.TabIndex = 2;
            this.btnBrowseDowloadingPath.Text = "...";
            this.btnBrowseDowloadingPath.UseVisualStyleBackColor = true;
            this.btnBrowseDowloadingPath.Click += new System.EventHandler(this.btnBrowseDownloadingDirPath_Click);
            // 
            // btnSelectBrowser
            // 
            this.btnSelectBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectBrowser.Location = new System.Drawing.Point(489, 220);
            this.btnSelectBrowser.Name = "btnSelectBrowser";
            this.btnSelectBrowser.Size = new System.Drawing.Size(32, 20);
            this.btnSelectBrowser.TabIndex = 11;
            this.btnSelectBrowser.Text = "...";
            this.btnSelectBrowser.UseVisualStyleBackColor = true;
            this.btnSelectBrowser.Click += new System.EventHandler(this.btnSelectBrowser_Click);
            // 
            // editDownloadingDirPath
            // 
            this.editDownloadingDirPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editDownloadingDirPath.Location = new System.Drawing.Point(13, 23);
            this.editDownloadingDirPath.Name = "editDownloadingDirPath";
            this.editDownloadingDirPath.Size = new System.Drawing.Size(470, 20);
            this.editDownloadingDirPath.TabIndex = 0;
            this.editDownloadingDirPath.Leave += new System.EventHandler(this.editDownloadingPath_Leave);
            // 
            // editBrowser
            // 
            this.editBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editBrowser.Location = new System.Drawing.Point(13, 220);
            this.editBrowser.Name = "editBrowser";
            this.editBrowser.Size = new System.Drawing.Size(470, 20);
            this.editBrowser.TabIndex = 10;
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
            // editTempDirPath
            // 
            this.editTempDirPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editTempDirPath.Location = new System.Drawing.Point(13, 64);
            this.editTempDirPath.Name = "editTempDirPath";
            this.editTempDirPath.Size = new System.Drawing.Size(470, 20);
            this.editTempDirPath.TabIndex = 1;
            this.editTempDirPath.Leave += new System.EventHandler(this.editTempPath_Leave);
            // 
            // btnBrowseTempPath
            // 
            this.btnBrowseTempPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseTempPath.Location = new System.Drawing.Point(489, 64);
            this.btnBrowseTempPath.Name = "btnBrowseTempPath";
            this.btnBrowseTempPath.Size = new System.Drawing.Size(32, 20);
            this.btnBrowseTempPath.TabIndex = 3;
            this.btnBrowseTempPath.Text = "...";
            this.btnBrowseTempPath.UseVisualStyleBackColor = true;
            this.btnBrowseTempPath.Click += new System.EventHandler(this.btnBrowseTempDirPath_Click);
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
            this.tabPageGUI.Controls.Add(this.groupBox12);
            this.tabPageGUI.Controls.Add(this.groupBoxFonts);
            this.tabPageGUI.Location = new System.Drawing.Point(4, 22);
            this.tabPageGUI.Name = "tabPageGUI";
            this.tabPageGUI.Size = new System.Drawing.Size(527, 357);
            this.tabPageGUI.TabIndex = 3;
            this.tabPageGUI.Text = "Интерфейс";
            // 
            // groupBox12
            // 
            this.groupBox12.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox12.Controls.Add(this.chkSortDashFormatsByBitrate);
            this.groupBox12.Controls.Add(this.chkMoveAudioId140First);
            this.groupBox12.Controls.Add(this.chkSortFormatsByFileSize);
            this.groupBox12.Location = new System.Drawing.Point(3, 120);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(521, 91);
            this.groupBox12.TabIndex = 1;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Сортировка списка форматов для скачивания";
            // 
            // chkSortDashFormatsByBitrate
            // 
            this.chkSortDashFormatsByBitrate.AutoSize = true;
            this.chkSortDashFormatsByBitrate.Location = new System.Drawing.Point(18, 42);
            this.chkSortDashFormatsByBitrate.Name = "chkSortDashFormatsByBitrate";
            this.chkSortDashFormatsByBitrate.Size = new System.Drawing.Size(320, 17);
            this.chkSortDashFormatsByBitrate.TabIndex = 2;
            this.chkSortDashFormatsByBitrate.Text = "Сортировать форматы DASH по битрейту (если известен)";
            this.chkSortDashFormatsByBitrate.UseVisualStyleBackColor = true;
            this.chkSortDashFormatsByBitrate.CheckedChanged += new System.EventHandler(this.chkSortDashFormatsByBitrate_CheckedChanged);
            // 
            // chkMoveAudioId140First
            // 
            this.chkMoveAudioId140First.AutoSize = true;
            this.chkMoveAudioId140First.Location = new System.Drawing.Point(18, 65);
            this.chkMoveAudioId140First.Name = "chkMoveAudioId140First";
            this.chkMoveAudioId140First.Size = new System.Drawing.Size(302, 17);
            this.chkMoveAudioId140First.TabIndex = 1;
            this.chkMoveAudioId140First.Text = "Перемещать аудио-дорожку с ID 140 на первое место";
            this.toolTip1.SetToolTip(this.chkMoveAudioId140First, "Независимо от сортировки");
            this.chkMoveAudioId140First.UseVisualStyleBackColor = true;
            this.chkMoveAudioId140First.CheckedChanged += new System.EventHandler(this.chkMoveAudioId140First_CheckedChanged);
            // 
            // chkSortFormatsByFileSize
            // 
            this.chkSortFormatsByFileSize.AutoSize = true;
            this.chkSortFormatsByFileSize.Location = new System.Drawing.Point(18, 19);
            this.chkSortFormatsByFileSize.Name = "chkSortFormatsByFileSize";
            this.chkSortFormatsByFileSize.Size = new System.Drawing.Size(320, 17);
            this.chkSortFormatsByFileSize.TabIndex = 0;
            this.chkSortFormatsByFileSize.Text = "Сортировать форматы по размеру файла (если известен)";
            this.toolTip1.SetToolTip(this.chkSortFormatsByFileSize, "Не применяется к HLS, DASH и контейнерным форматам!");
            this.chkSortFormatsByFileSize.UseVisualStyleBackColor = true;
            this.chkSortFormatsByFileSize.CheckedChanged += new System.EventHandler(this.chkSortFormatsByFileSize_CheckedChanged);
            // 
            // groupBoxFonts
            // 
            this.groupBoxFonts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxFonts.Controls.Add(this.numericUpDownVideoTitleFontSize);
            this.groupBoxFonts.Controls.Add(this.label17);
            this.groupBoxFonts.Controls.Add(this.numericUpDownFavoritesListFontSize);
            this.groupBoxFonts.Controls.Add(this.label16);
            this.groupBoxFonts.Controls.Add(this.label15);
            this.groupBoxFonts.Controls.Add(this.numericUpDownMenusFontSize);
            this.groupBoxFonts.Location = new System.Drawing.Point(3, 3);
            this.groupBoxFonts.Name = "groupBoxFonts";
            this.groupBoxFonts.Size = new System.Drawing.Size(521, 111);
            this.groupBoxFonts.TabIndex = 0;
            this.groupBoxFonts.TabStop = false;
            this.groupBoxFonts.Text = "Размер шрифтов";
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
            // tabPageDownloadingSettings
            // 
            this.tabPageDownloadingSettings.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPageDownloadingSettings.Controls.Add(this.numericUpDownDownloadRetryCount);
            this.tabPageDownloadingSettings.Controls.Add(this.label21);
            this.tabPageDownloadingSettings.Controls.Add(this.groupBox10);
            this.tabPageDownloadingSettings.Controls.Add(this.chkSaveImage);
            this.tabPageDownloadingSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageDownloadingSettings.Name = "tabPageDownloadingSettings";
            this.tabPageDownloadingSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDownloadingSettings.Size = new System.Drawing.Size(527, 357);
            this.tabPageDownloadingSettings.TabIndex = 2;
            this.tabPageDownloadingSettings.Text = "Скачивание";
            // 
            // numericUpDownDownloadRetryCount
            // 
            this.numericUpDownDownloadRetryCount.Location = new System.Drawing.Point(194, 292);
            this.numericUpDownDownloadRetryCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDownloadRetryCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownDownloadRetryCount.Name = "numericUpDownDownloadRetryCount";
            this.numericUpDownDownloadRetryCount.Size = new System.Drawing.Size(46, 20);
            this.numericUpDownDownloadRetryCount.TabIndex = 19;
            this.numericUpDownDownloadRetryCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDownDownloadRetryCount.ValueChanged += new System.EventHandler(this.numericUpDownDownloadRetryCount_ValueChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(11, 294);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(183, 13);
            this.label21.TabIndex = 18;
            this.label21.Text = "Количество ппопыток скачивания:";
            // 
            // groupBox10
            // 
            this.groupBox10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox10.Controls.Add(this.numericUpDownDelayAfterContainerCreated);
            this.groupBox10.Controls.Add(this.label20);
            this.groupBox10.Controls.Add(this.label19);
            this.groupBox10.Controls.Add(this.btnDownloadAllAdaptiveVideoTracksWtf);
            this.groupBox10.Controls.Add(this.chkDownloadAllAdaptiveVideoTracks);
            this.groupBox10.Controls.Add(this.groupBox15);
            this.groupBox10.Controls.Add(this.groupBox14);
            this.groupBox10.Location = new System.Drawing.Point(14, 6);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(507, 280);
            this.groupBox10.TabIndex = 17;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Адаптивные форматы";
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
            // btnDownloadAllAdaptiveVideoTracksWtf
            // 
            this.btnDownloadAllAdaptiveVideoTracksWtf.Location = new System.Drawing.Point(339, 15);
            this.btnDownloadAllAdaptiveVideoTracksWtf.Name = "btnDownloadAllAdaptiveVideoTracksWtf";
            this.btnDownloadAllAdaptiveVideoTracksWtf.Size = new System.Drawing.Size(30, 23);
            this.btnDownloadAllAdaptiveVideoTracksWtf.TabIndex = 20;
            this.btnDownloadAllAdaptiveVideoTracksWtf.Text = "?";
            this.btnDownloadAllAdaptiveVideoTracksWtf.UseVisualStyleBackColor = true;
            this.btnDownloadAllAdaptiveVideoTracksWtf.Click += new System.EventHandler(this.btnDownloadAllAdaptiveVideoTracksWtf_Click);
            // 
            // chkDownloadAllAdaptiveVideoTracks
            // 
            this.chkDownloadAllAdaptiveVideoTracks.AutoSize = true;
            this.chkDownloadAllAdaptiveVideoTracks.Location = new System.Drawing.Point(6, 19);
            this.chkDownloadAllAdaptiveVideoTracks.Name = "chkDownloadAllAdaptiveVideoTracks";
            this.chkDownloadAllAdaptiveVideoTracks.Size = new System.Drawing.Size(327, 17);
            this.chkDownloadAllAdaptiveVideoTracks.TabIndex = 19;
            this.chkDownloadAllAdaptiveVideoTracks.Text = "Автоматически скачивать все адаптивные форматы видео";
            this.chkDownloadAllAdaptiveVideoTracks.UseVisualStyleBackColor = true;
            this.chkDownloadAllAdaptiveVideoTracks.CheckedChanged += new System.EventHandler(this.chkDownloadAllAdaptiveVideoTracks_CheckedChanged);
            // 
            // groupBox15
            // 
            this.groupBox15.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox15.Controls.Add(this.groupBox16);
            this.groupBox15.Controls.Add(this.chkMergeAdaptive);
            this.groupBox15.Controls.Add(this.chkDeleteSourceFiles);
            this.groupBox15.Location = new System.Drawing.Point(6, 160);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(495, 83);
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
            this.groupBox16.Size = new System.Drawing.Size(194, 60);
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
            // chkMergeAdaptive
            // 
            this.chkMergeAdaptive.AutoSize = true;
            this.chkMergeAdaptive.Location = new System.Drawing.Point(20, 18);
            this.chkMergeAdaptive.Name = "chkMergeAdaptive";
            this.chkMergeAdaptive.Size = new System.Drawing.Size(274, 17);
            this.chkMergeAdaptive.TabIndex = 14;
            this.chkMergeAdaptive.Text = "Объединять дорожки видео и аудио в контейнер";
            this.toolTip1.SetToolTip(this.chkMergeAdaptive, "Не применяется, если скачаны только аудио-дорожки");
            this.chkMergeAdaptive.UseVisualStyleBackColor = true;
            this.chkMergeAdaptive.CheckedChanged += new System.EventHandler(this.chkMergeAdaptive_CheckedChanged);
            // 
            // chkDeleteSourceFiles
            // 
            this.chkDeleteSourceFiles.AutoSize = true;
            this.chkDeleteSourceFiles.Location = new System.Drawing.Point(30, 41);
            this.chkDeleteSourceFiles.Name = "chkDeleteSourceFiles";
            this.chkDeleteSourceFiles.Size = new System.Drawing.Size(158, 17);
            this.chkDeleteSourceFiles.TabIndex = 16;
            this.chkDeleteSourceFiles.Text = "Удалять исходные файлы";
            this.toolTip1.SetToolTip(this.chkDeleteSourceFiles, "Не применяется, если скачаны только аудио-дорожки");
            this.chkDeleteSourceFiles.UseVisualStyleBackColor = true;
            this.chkDeleteSourceFiles.CheckedChanged += new System.EventHandler(this.chkDeleteSourceFiles_CheckedChanged);
            // 
            // groupBox14
            // 
            this.groupBox14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox14.Controls.Add(this.chkDownloadFirstAudioTrack);
            this.groupBox14.Controls.Add(this.chkIfOnlyBiggerFileSize);
            this.groupBox14.Controls.Add(this.chkDownloadSecondAudioTrack);
            this.groupBox14.Controls.Add(this.chkDownloadAllAudioTracks);
            this.groupBox14.Location = new System.Drawing.Point(6, 42);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(495, 112);
            this.groupBox14.TabIndex = 17;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Скачивание аудио-дорожек";
            // 
            // chkDownloadFirstAudioTrack
            // 
            this.chkDownloadFirstAudioTrack.AutoSize = true;
            this.chkDownloadFirstAudioTrack.Location = new System.Drawing.Point(20, 19);
            this.chkDownloadFirstAudioTrack.Name = "chkDownloadFirstAudioTrack";
            this.chkDownloadFirstAudioTrack.Size = new System.Drawing.Size(277, 17);
            this.chkDownloadFirstAudioTrack.TabIndex = 19;
            this.chkDownloadFirstAudioTrack.Text = "Автоматически скачивать первую аудио-дорожку";
            this.chkDownloadFirstAudioTrack.UseVisualStyleBackColor = true;
            this.chkDownloadFirstAudioTrack.CheckedChanged += new System.EventHandler(this.chkDownloadFirstAudioTrack_CheckedChanged);
            // 
            // chkIfOnlyBiggerFileSize
            // 
            this.chkIfOnlyBiggerFileSize.AutoSize = true;
            this.chkIfOnlyBiggerFileSize.Location = new System.Drawing.Point(30, 65);
            this.chkIfOnlyBiggerFileSize.Name = "chkIfOnlyBiggerFileSize";
            this.chkIfOnlyBiggerFileSize.Size = new System.Drawing.Size(207, 17);
            this.chkIfOnlyBiggerFileSize.TabIndex = 18;
            this.chkIfOnlyBiggerFileSize.Text = "Только если размер файла больше";
            this.chkIfOnlyBiggerFileSize.UseVisualStyleBackColor = true;
            this.chkIfOnlyBiggerFileSize.CheckedChanged += new System.EventHandler(this.chkIfOnlyBiggerFileSize_CheckedChanged);
            // 
            // chkDownloadSecondAudioTrack
            // 
            this.chkDownloadSecondAudioTrack.AutoSize = true;
            this.chkDownloadSecondAudioTrack.Location = new System.Drawing.Point(20, 42);
            this.chkDownloadSecondAudioTrack.Name = "chkDownloadSecondAudioTrack";
            this.chkDownloadSecondAudioTrack.Size = new System.Drawing.Size(276, 17);
            this.chkDownloadSecondAudioTrack.TabIndex = 1;
            this.chkDownloadSecondAudioTrack.Text = "Автоматически скачивать вторую аудио-дорожку";
            this.chkDownloadSecondAudioTrack.UseVisualStyleBackColor = true;
            this.chkDownloadSecondAudioTrack.CheckedChanged += new System.EventHandler(this.chkDownloadSecondAudioTrack_CheckedChanged);
            // 
            // chkDownloadAllAudioTracks
            // 
            this.chkDownloadAllAudioTracks.AutoSize = true;
            this.chkDownloadAllAudioTracks.Location = new System.Drawing.Point(20, 88);
            this.chkDownloadAllAudioTracks.Name = "chkDownloadAllAudioTracks";
            this.chkDownloadAllAudioTracks.Size = new System.Drawing.Size(179, 17);
            this.chkDownloadAllAudioTracks.TabIndex = 0;
            this.chkDownloadAllAudioTracks.Text = "Скачивать все аудио-дорожки";
            this.chkDownloadAllAudioTracks.UseVisualStyleBackColor = true;
            this.chkDownloadAllAudioTracks.CheckedChanged += new System.EventHandler(this.chkDownloadAllAudioTracks_CheckedChanged);
            // 
            // chkSaveImage
            // 
            this.chkSaveImage.AutoSize = true;
            this.chkSaveImage.Location = new System.Drawing.Point(14, 316);
            this.chkSaveImage.Name = "chkSaveImage";
            this.chkSaveImage.Size = new System.Drawing.Size(175, 17);
            this.chkSaveImage.TabIndex = 15;
            this.chkSaveImage.Text = "Скачивать картинку от видео";
            this.chkSaveImage.UseVisualStyleBackColor = true;
            this.chkSaveImage.CheckedChanged += new System.EventHandler(this.chkSaveImage_CheckedChanged);
            // 
            // tabPageSystemSettings
            // 
            this.tabPageSystemSettings.AutoScroll = true;
            this.tabPageSystemSettings.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPageSystemSettings.Controls.Add(this.checkBoxUseGmtTime);
            this.tabPageSystemSettings.Controls.Add(this.groupBox13);
            this.tabPageSystemSettings.Controls.Add(this.groupBox11);
            this.tabPageSystemSettings.Controls.Add(this.groupBox7);
            this.tabPageSystemSettings.Location = new System.Drawing.Point(4, 22);
            this.tabPageSystemSettings.Name = "tabPageSystemSettings";
            this.tabPageSystemSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSystemSettings.Size = new System.Drawing.Size(527, 357);
            this.tabPageSystemSettings.TabIndex = 1;
            this.tabPageSystemSettings.Text = "Система";
            // 
            // checkBoxUseGmtTime
            // 
            this.checkBoxUseGmtTime.AutoSize = true;
            this.checkBoxUseGmtTime.Checked = true;
            this.checkBoxUseGmtTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUseGmtTime.Location = new System.Drawing.Point(12, 365);
            this.checkBoxUseGmtTime.Name = "checkBoxUseGmtTime";
            this.checkBoxUseGmtTime.Size = new System.Drawing.Size(101, 17);
            this.checkBoxUseGmtTime.TabIndex = 15;
            this.checkBoxUseGmtTime.Text = "Время по GMT";
            this.toolTip1.SetToolTip(this.checkBoxUseGmtTime, "После изменения этого параметра необходимо повторить поиск видео!");
            this.checkBoxUseGmtTime.UseVisualStyleBackColor = true;
            this.checkBoxUseGmtTime.CheckedChanged += new System.EventHandler(this.checkBoxUseGmtTime_CheckedChanged);
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.btnUseRamWhy);
            this.groupBox13.Controls.Add(this.panelRAM);
            this.groupBox13.Controls.Add(this.chkUseRamForTempFiles);
            this.groupBox13.Location = new System.Drawing.Point(12, 290);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(459, 66);
            this.groupBox13.TabIndex = 14;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Оперативная память (RAM)";
            // 
            // btnUseRamWhy
            // 
            this.btnUseRamWhy.Location = new System.Drawing.Point(371, 25);
            this.btnUseRamWhy.Name = "btnUseRamWhy";
            this.btnUseRamWhy.Size = new System.Drawing.Size(75, 23);
            this.btnUseRamWhy.TabIndex = 16;
            this.btnUseRamWhy.Text = "Зачем?";
            this.btnUseRamWhy.UseVisualStyleBackColor = true;
            this.btnUseRamWhy.Click += new System.EventHandler(this.btnUseRamWhy_Click);
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
            // chkUseRamForTempFiles
            // 
            this.chkUseRamForTempFiles.Location = new System.Drawing.Point(17, 19);
            this.chkUseRamForTempFiles.Name = "chkUseRamForTempFiles";
            this.chkUseRamForTempFiles.Size = new System.Drawing.Size(288, 42);
            this.chkUseRamForTempFiles.TabIndex = 13;
            this.chkUseRamForTempFiles.Text = "Использовать оперативную память для хранения временных файлов (экспериментально!)" +
    "";
            this.toolTip1.SetToolTip(this.chkUseRamForTempFiles, "Не применяется к DASH и HLS");
            this.chkUseRamForTempFiles.UseVisualStyleBackColor = true;
            this.chkUseRamForTempFiles.CheckedChanged += new System.EventHandler(this.chkUseRamForTempFiles_CheckedChanged);
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.btnApiWtf);
            this.groupBox11.Controls.Add(this.groupBox4);
            this.groupBox11.Controls.Add(this.groupBox5);
            this.groupBox11.Controls.Add(this.chkUseHiddenApiForGettingInfo);
            this.groupBox11.Location = new System.Drawing.Point(12, 6);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(495, 174);
            this.groupBox11.TabIndex = 12;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "API";
            // 
            // btnApiWtf
            // 
            this.btnApiWtf.Location = new System.Drawing.Point(367, 15);
            this.btnApiWtf.Name = "btnApiWtf";
            this.btnApiWtf.Size = new System.Drawing.Size(32, 23);
            this.btnApiWtf.TabIndex = 12;
            this.btnApiWtf.Text = "?";
            this.toolTip1.SetToolTip(this.btnApiWtf, "Помогите!");
            this.btnApiWtf.UseVisualStyleBackColor = true;
            this.btnApiWtf.Click += new System.EventHandler(this.btnApiWtf_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.editCipherDecryptionAlgo);
            this.groupBox4.Location = new System.Drawing.Point(11, 44);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(478, 58);
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
            // editCipherDecryptionAlgo
            // 
            this.editCipherDecryptionAlgo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editCipherDecryptionAlgo.Location = new System.Drawing.Point(8, 32);
            this.editCipherDecryptionAlgo.Name = "editCipherDecryptionAlgo";
            this.editCipherDecryptionAlgo.Size = new System.Drawing.Size(462, 20);
            this.editCipherDecryptionAlgo.TabIndex = 0;
            this.editCipherDecryptionAlgo.Leave += new System.EventHandler(this.editCipherDecryptionAlgo_Leave);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.editYouTubeApiKey);
            this.groupBox5.Location = new System.Drawing.Point(11, 107);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(478, 61);
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
            // editYouTubeApiKey
            // 
            this.editYouTubeApiKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editYouTubeApiKey.Location = new System.Drawing.Point(6, 35);
            this.editYouTubeApiKey.Name = "editYouTubeApiKey";
            this.editYouTubeApiKey.Size = new System.Drawing.Size(466, 20);
            this.editYouTubeApiKey.TabIndex = 0;
            this.editYouTubeApiKey.Leave += new System.EventHandler(this.editYouTubeApiKey_Leave);
            // 
            // chkUseHiddenApiForGettingInfo
            // 
            this.chkUseHiddenApiForGettingInfo.AutoSize = true;
            this.chkUseHiddenApiForGettingInfo.Location = new System.Drawing.Point(11, 19);
            this.chkUseHiddenApiForGettingInfo.Name = "chkUseHiddenApiForGettingInfo";
            this.chkUseHiddenApiForGettingInfo.Size = new System.Drawing.Size(350, 17);
            this.chkUseHiddenApiForGettingInfo.TabIndex = 11;
            this.chkUseHiddenApiForGettingInfo.Text = "Использовать скрытое API для получения информации о видео";
            this.chkUseHiddenApiForGettingInfo.UseVisualStyleBackColor = true;
            this.chkUseHiddenApiForGettingInfo.CheckedChanged += new System.EventHandler(this.chkUseHiddenApiForGettingInfo_CheckedChanged);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label13);
            this.groupBox7.Controls.Add(this.numericUpDownGlobalThreadsMaximum);
            this.groupBox7.Controls.Add(this.panelWarningAudioThreads);
            this.groupBox7.Controls.Add(this.panelWarningVideoThreads);
            this.groupBox7.Controls.Add(this.label10);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.numericUpDownThreadsAudio);
            this.groupBox7.Controls.Add(this.numericUpDownThreadsVideo);
            this.groupBox7.Location = new System.Drawing.Point(12, 186);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(318, 98);
            this.groupBox7.TabIndex = 10;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Потоки";
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
            // numericUpDownGlobalThreadsMaximum
            // 
            this.numericUpDownGlobalThreadsMaximum.Location = new System.Drawing.Point(263, 68);
            this.numericUpDownGlobalThreadsMaximum.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDownGlobalThreadsMaximum.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownGlobalThreadsMaximum.Name = "numericUpDownGlobalThreadsMaximum";
            this.numericUpDownGlobalThreadsMaximum.Size = new System.Drawing.Size(51, 20);
            this.numericUpDownGlobalThreadsMaximum.TabIndex = 6;
            this.numericUpDownGlobalThreadsMaximum.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericUpDownGlobalThreadsMaximum.ValueChanged += new System.EventHandler(this.numericUpDownGlobalThreadsMaximum_ValueChanged);
            // 
            // panelWarningAudioThreads
            // 
            this.panelWarningAudioThreads.BackgroundImage = global::YouTube_downloader.Properties.Resources.warning;
            this.panelWarningAudioThreads.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelWarningAudioThreads.Location = new System.Drawing.Point(289, 39);
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
            this.panelWarningVideoThreads.Location = new System.Drawing.Point(289, 11);
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
            // numericUpDownThreadsAudio
            // 
            this.numericUpDownThreadsAudio.Location = new System.Drawing.Point(242, 41);
            this.numericUpDownThreadsAudio.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDownThreadsAudio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownThreadsAudio.Name = "numericUpDownThreadsAudio";
            this.numericUpDownThreadsAudio.Size = new System.Drawing.Size(41, 20);
            this.numericUpDownThreadsAudio.TabIndex = 1;
            this.numericUpDownThreadsAudio.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numericUpDownThreadsAudio.ValueChanged += new System.EventHandler(this.numericUpDownThreadsAudio_ValueChanged);
            // 
            // numericUpDownThreadsVideo
            // 
            this.numericUpDownThreadsVideo.Location = new System.Drawing.Point(243, 14);
            this.numericUpDownThreadsVideo.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownThreadsVideo.Name = "numericUpDownThreadsVideo";
            this.numericUpDownThreadsVideo.Size = new System.Drawing.Size(40, 20);
            this.numericUpDownThreadsVideo.TabIndex = 1;
            this.numericUpDownThreadsVideo.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownThreadsVideo.ValueChanged += new System.EventHandler(this.numericUpDownThreadsVideo_ValueChanged);
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
            this.panelSearch.Controls.Add(this.groupBox1);
            this.panelSearch.Controls.Add(this.groupBox6);
            this.panelSearch.Controls.Add(this.groupBox2);
            this.panelSearch.Location = new System.Drawing.Point(0, 0);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(544, 393);
            this.panelSearch.TabIndex = 5;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.btnWhy);
            this.groupBox9.Controls.Add(this.label12);
            this.groupBox9.Controls.Add(this.btnSearchByWebPage);
            this.groupBox9.Controls.Add(this.richTextBoxWebPage);
            this.groupBox9.Location = new System.Drawing.Point(6, 268);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(515, 173);
            this.groupBox9.TabIndex = 5;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Поиск по коду веб-страницы";
            // 
            // btnWhy
            // 
            this.btnWhy.Location = new System.Drawing.Point(434, 10);
            this.btnWhy.Name = "btnWhy";
            this.btnWhy.Size = new System.Drawing.Size(75, 23);
            this.btnWhy.TabIndex = 3;
            this.btnWhy.Text = "Зачем?";
            this.btnWhy.UseVisualStyleBackColor = true;
            this.btnWhy.Click += new System.EventHandler(this.btnWhy_Click);
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
            // richTextBoxWebPage
            // 
            this.richTextBoxWebPage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxWebPage.ContextMenuStrip = this.menuCopyPaste;
            this.richTextBoxWebPage.Location = new System.Drawing.Point(9, 39);
            this.richTextBoxWebPage.Name = "richTextBoxWebPage";
            this.richTextBoxWebPage.Size = new System.Drawing.Size(500, 99);
            this.richTextBoxWebPage.TabIndex = 0;
            this.richTextBoxWebPage.Text = "";
            // 
            // menuCopyPaste
            // 
            this.menuCopyPaste.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutTextToolStripMenuItem,
            this.copyTextToolStripMenuItem,
            this.pasteTextToolStripMenuItem,
            this.toolStripMenuItem1,
            this.selectAllTextToolStripMenuItem});
            this.menuCopyPaste.Name = "menuCopyPaste";
            this.menuCopyPaste.Size = new System.Drawing.Size(149, 98);
            this.menuCopyPaste.Opening += new System.ComponentModel.CancelEventHandler(this.menuCopyPaste_Opening);
            // 
            // cutTextToolStripMenuItem
            // 
            this.cutTextToolStripMenuItem.Name = "cutTextToolStripMenuItem";
            this.cutTextToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.cutTextToolStripMenuItem.Text = "Вырезать";
            this.cutTextToolStripMenuItem.Click += new System.EventHandler(this.cutTextToolStripMenuItem_Click);
            // 
            // copyTextToolStripMenuItem
            // 
            this.copyTextToolStripMenuItem.Name = "copyTextToolStripMenuItem";
            this.copyTextToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.copyTextToolStripMenuItem.Text = "Копировать";
            this.copyTextToolStripMenuItem.Click += new System.EventHandler(this.copyTextToolStripMenuItem_Click);
            // 
            // pasteTextToolStripMenuItem
            // 
            this.pasteTextToolStripMenuItem.Name = "pasteTextToolStripMenuItem";
            this.pasteTextToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.pasteTextToolStripMenuItem.Text = "Вставить";
            this.pasteTextToolStripMenuItem.Click += new System.EventHandler(this.pasteTextToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(145, 6);
            // 
            // selectAllTextToolStripMenuItem
            // 
            this.selectAllTextToolStripMenuItem.Name = "selectAllTextToolStripMenuItem";
            this.selectAllTextToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.selectAllTextToolStripMenuItem.Text = "Выделить всё";
            this.selectAllTextToolStripMenuItem.Click += new System.EventHandler(this.selectAllTextToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dateTimePickerBefore);
            this.groupBox3.Controls.Add(this.dateTimePickerAfter);
            this.groupBox3.Controls.Add(this.chkPublishedAfter);
            this.groupBox3.Controls.Add(this.chkPublishedBefore);
            this.groupBox3.Location = new System.Drawing.Point(310, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(198, 121);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Диапазон поиска";
            // 
            // dateTimePickerBefore
            // 
            this.dateTimePickerBefore.Location = new System.Drawing.Point(17, 91);
            this.dateTimePickerBefore.Name = "dateTimePickerBefore";
            this.dateTimePickerBefore.Size = new System.Drawing.Size(152, 20);
            this.dateTimePickerBefore.TabIndex = 4;
            // 
            // dateTimePickerAfter
            // 
            this.dateTimePickerAfter.Location = new System.Drawing.Point(17, 41);
            this.dateTimePickerAfter.Name = "dateTimePickerAfter";
            this.dateTimePickerAfter.Size = new System.Drawing.Size(152, 20);
            this.dateTimePickerAfter.TabIndex = 3;
            // 
            // chkPublishedAfter
            // 
            this.chkPublishedAfter.AutoSize = true;
            this.chkPublishedAfter.Checked = true;
            this.chkPublishedAfter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPublishedAfter.Location = new System.Drawing.Point(17, 18);
            this.chkPublishedAfter.Name = "chkPublishedAfter";
            this.chkPublishedAfter.Size = new System.Drawing.Size(146, 17);
            this.chkPublishedAfter.TabIndex = 2;
            this.chkPublishedAfter.Text = "Опубликованные после";
            this.chkPublishedAfter.UseVisualStyleBackColor = true;
            // 
            // chkPublishedBefore
            // 
            this.chkPublishedBefore.AutoSize = true;
            this.chkPublishedBefore.Checked = true;
            this.chkPublishedBefore.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPublishedBefore.Location = new System.Drawing.Point(17, 68);
            this.chkPublishedBefore.Name = "chkPublishedBefore";
            this.chkPublishedBefore.Size = new System.Drawing.Size(128, 17);
            this.chkPublishedBefore.TabIndex = 1;
            this.chkPublishedBefore.Text = "Опубликованные до";
            this.chkPublishedBefore.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.chkSearchVideos);
            this.groupBox8.Controls.Add(this.chkSearchChannels);
            this.groupBox8.Location = new System.Drawing.Point(6, 204);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(293, 58);
            this.groupBox8.TabIndex = 4;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "В результатах поиска выдавать";
            // 
            // chkSearchVideos
            // 
            this.chkSearchVideos.AutoSize = true;
            this.chkSearchVideos.Checked = true;
            this.chkSearchVideos.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearchVideos.Location = new System.Drawing.Point(92, 27);
            this.chkSearchVideos.Name = "chkSearchVideos";
            this.chkSearchVideos.Size = new System.Drawing.Size(57, 17);
            this.chkSearchVideos.TabIndex = 1;
            this.chkSearchVideos.Text = "Видео";
            this.chkSearchVideos.UseVisualStyleBackColor = true;
            this.chkSearchVideos.CheckedChanged += new System.EventHandler(this.chkSearchVideos_CheckedChanged);
            // 
            // chkSearchChannels
            // 
            this.chkSearchChannels.AutoSize = true;
            this.chkSearchChannels.Checked = true;
            this.chkSearchChannels.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearchChannels.Location = new System.Drawing.Point(21, 27);
            this.chkSearchChannels.Name = "chkSearchChannels";
            this.chkSearchChannels.Size = new System.Drawing.Size(65, 17);
            this.chkSearchChannels.TabIndex = 0;
            this.chkSearchChannels.Text = "Каналы";
            this.chkSearchChannels.UseVisualStyleBackColor = true;
            this.chkSearchChannels.CheckedChanged += new System.EventHandler(this.chkSearchChannels_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSearchByQuery);
            this.groupBox1.Controls.Add(this.editQuery);
            this.groupBox1.Location = new System.Drawing.Point(0, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(299, 91);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поиск по запросу";
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
            this.btnSearchByQuery.Click += new System.EventHandler(this.BtnSearchByQuery_Click);
            // 
            // editQuery
            // 
            this.editQuery.Location = new System.Drawing.Point(15, 35);
            this.editQuery.Name = "editQuery";
            this.editQuery.Size = new System.Drawing.Size(278, 20);
            this.editQuery.TabIndex = 0;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.rbSearchResultsUserDefined);
            this.groupBox6.Controls.Add(this.rbSearchResultsMax);
            this.groupBox6.Controls.Add(this.numericUpDownSearchResult);
            this.groupBox6.Location = new System.Drawing.Point(310, 133);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(198, 65);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Количество результатов выдачи";
            // 
            // rbSearchResultsUserDefined
            // 
            this.rbSearchResultsUserDefined.AutoCheck = false;
            this.rbSearchResultsUserDefined.AutoSize = true;
            this.rbSearchResultsUserDefined.Location = new System.Drawing.Point(22, 40);
            this.rbSearchResultsUserDefined.Name = "rbSearchResultsUserDefined";
            this.rbSearchResultsUserDefined.Size = new System.Drawing.Size(62, 17);
            this.rbSearchResultsUserDefined.TabIndex = 2;
            this.rbSearchResultsUserDefined.Text = "Только";
            this.rbSearchResultsUserDefined.UseVisualStyleBackColor = true;
            this.rbSearchResultsUserDefined.Click += new System.EventHandler(this.rbSearchResultsUserDefined_Click);
            // 
            // rbSearchResultsMax
            // 
            this.rbSearchResultsMax.AutoCheck = false;
            this.rbSearchResultsMax.AutoSize = true;
            this.rbSearchResultsMax.Checked = true;
            this.rbSearchResultsMax.Location = new System.Drawing.Point(22, 17);
            this.rbSearchResultsMax.Name = "rbSearchResultsMax";
            this.rbSearchResultsMax.Size = new System.Drawing.Size(106, 17);
            this.rbSearchResultsMax.TabIndex = 1;
            this.rbSearchResultsMax.TabStop = true;
            this.rbSearchResultsMax.Text = "Максимум (500)";
            this.rbSearchResultsMax.UseVisualStyleBackColor = true;
            this.rbSearchResultsMax.Click += new System.EventHandler(this.rbSearchResultsMax_Click);
            // 
            // numericUpDownSearchResult
            // 
            this.numericUpDownSearchResult.Location = new System.Drawing.Point(90, 40);
            this.numericUpDownSearchResult.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.numericUpDownSearchResult.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownSearchResult.Name = "numericUpDownSearchResult";
            this.numericUpDownSearchResult.Size = new System.Drawing.Size(50, 20);
            this.numericUpDownSearchResult.TabIndex = 0;
            this.numericUpDownSearchResult.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDownSearchResult.ValueChanged += new System.EventHandler(this.numericUpDownSearchResult_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.editSearchUrl);
            this.groupBox2.Controls.Add(this.btnSearchByUrl);
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
            // editSearchUrl
            // 
            this.editSearchUrl.Location = new System.Drawing.Point(15, 39);
            this.editSearchUrl.Name = "editSearchUrl";
            this.editSearchUrl.Size = new System.Drawing.Size(278, 20);
            this.editSearchUrl.TabIndex = 1;
            // 
            // btnSearchByUrl
            // 
            this.btnSearchByUrl.Location = new System.Drawing.Point(219, 65);
            this.btnSearchByUrl.Name = "btnSearchByUrl";
            this.btnSearchByUrl.Size = new System.Drawing.Size(74, 23);
            this.btnSearchByUrl.TabIndex = 0;
            this.btnSearchByUrl.Text = "Искать";
            this.btnSearchByUrl.UseVisualStyleBackColor = true;
            this.btnSearchByUrl.Click += new System.EventHandler(this.btnSearchByUrl_Click);
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
            // tvFavorites
            // 
            this.tvFavorites.AllColumns.Add(this.olvColumn1);
            this.tvFavorites.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvFavorites.CellEditUseWholeCell = false;
            this.tvFavorites.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.tvFavorites.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.tvFavorites.HideSelection = false;
            this.tvFavorites.Location = new System.Drawing.Point(569, 20);
            this.tvFavorites.Name = "tvFavorites";
            this.tvFavorites.ShowGroups = false;
            this.tvFavorites.Size = new System.Drawing.Size(244, 412);
            this.tvFavorites.TabIndex = 1;
            this.tvFavorites.UseCompatibleStateImageBehavior = false;
            this.tvFavorites.View = System.Windows.Forms.View.Details;
            this.tvFavorites.VirtualMode = true;
            this.tvFavorites.CellRightClick += new System.EventHandler<BrightIdeasSoftware.CellRightClickEventArgs>(this.tvFavorites_CellRightClick);
            this.tvFavorites.ItemsRemoving += new System.EventHandler<BrightIdeasSoftware.ItemsRemovingEventArgs>(this.tvFavorites_ItemsRemoving);
            this.tvFavorites.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tvFavorites_MouseDoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "DisplayName";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Width = 25;
            // 
            // menuFavorites
            // 
            this.menuFavorites.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openVideoInBrowserToolStripMenuItem,
            this.miCopyVideoUrlToolStripMenuItem,
            this.openChannelInBrowserToolStripMenuItem,
            this.miCopyChannelUrlToolStripMenuItem,
            this.copyDisplayNameToolStripMenuItem,
            this.miCopyVideoIdToolStripMenuItem,
            this.miCopyChannelIdToolStripMenuItem,
            this.copyDisplayNameWithIdToolStripMenuItem});
            this.menuFavorites.Name = "menuFavarites";
            this.menuFavorites.Size = new System.Drawing.Size(281, 180);
            this.menuFavorites.Opening += new System.ComponentModel.CancelEventHandler(this.menuFavorites_Opening);
            // 
            // openVideoInBrowserToolStripMenuItem
            // 
            this.openVideoInBrowserToolStripMenuItem.Name = "openVideoInBrowserToolStripMenuItem";
            this.openVideoInBrowserToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.openVideoInBrowserToolStripMenuItem.Text = "Открыть видео в браузере";
            this.openVideoInBrowserToolStripMenuItem.Click += new System.EventHandler(this.openVideoInBrowserToolStripMenuItem_Click);
            // 
            // miCopyVideoUrlToolStripMenuItem
            // 
            this.miCopyVideoUrlToolStripMenuItem.Name = "miCopyVideoUrlToolStripMenuItem";
            this.miCopyVideoUrlToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.miCopyVideoUrlToolStripMenuItem.Text = "Скопировать ссылку на видео";
            this.miCopyVideoUrlToolStripMenuItem.Click += new System.EventHandler(this.miCopyVideoUrlToolStripMenuItem_Click);
            // 
            // openChannelInBrowserToolStripMenuItem
            // 
            this.openChannelInBrowserToolStripMenuItem.Name = "openChannelInBrowserToolStripMenuItem";
            this.openChannelInBrowserToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.openChannelInBrowserToolStripMenuItem.Text = "Открыть канал в браузере";
            this.openChannelInBrowserToolStripMenuItem.Click += new System.EventHandler(this.openChannelInBrowserToolStripMenuItem_Click);
            // 
            // miCopyChannelUrlToolStripMenuItem
            // 
            this.miCopyChannelUrlToolStripMenuItem.Name = "miCopyChannelUrlToolStripMenuItem";
            this.miCopyChannelUrlToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.miCopyChannelUrlToolStripMenuItem.Text = "Скопировать ссылку на канал";
            this.miCopyChannelUrlToolStripMenuItem.Click += new System.EventHandler(this.miCopyChannelUrlToolStripMenuItem_Click);
            // 
            // copyDisplayNameToolStripMenuItem
            // 
            this.copyDisplayNameToolStripMenuItem.Name = "copyDisplayNameToolStripMenuItem";
            this.copyDisplayNameToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.copyDisplayNameToolStripMenuItem.Text = "Скопировать отображаемое имя";
            this.copyDisplayNameToolStripMenuItem.Click += new System.EventHandler(this.copyDisplayNameToolStripMenuItem_Click);
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
            // copyDisplayNameWithIdToolStripMenuItem
            // 
            this.copyDisplayNameWithIdToolStripMenuItem.Name = "copyDisplayNameWithIdToolStripMenuItem";
            this.copyDisplayNameWithIdToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.copyDisplayNameWithIdToolStripMenuItem.Text = "Скопировать отображаемое имя и ID";
            this.copyDisplayNameWithIdToolStripMenuItem.Click += new System.EventHandler(this.copyDisplayNameWithIdToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 439);
            this.Controls.Add(this.tvFavorites);
            this.Controls.Add(this.tabControlMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(830, 440);
            this.Name = "Form1";
            this.Text = "Скачивалка с ютуба";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPageFilesAndFolders.ResumeLayout(false);
            this.tabPageFilesAndFolders.PerformLayout();
            this.tabPageGUI.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBoxFonts.ResumeLayout(false);
            this.groupBoxFonts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoTitleFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFavoritesListFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMenusFontSize)).EndInit();
            this.tabPageDownloadingSettings.ResumeLayout(false);
            this.tabPageDownloadingSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDownloadRetryCount)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayAfterContainerCreated)).EndInit();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.tabPageSystemSettings.ResumeLayout(false);
            this.tabPageSystemSettings.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGlobalThreadsMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadsAudio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadsVideo)).EndInit();
            this.tabPageSearch.ResumeLayout(false);
            this.panelSearch.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.menuCopyPaste.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSearchResult)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPageSearchResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tvFavorites)).EndInit();
            this.menuFavorites.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControlMain;
		private System.Windows.Forms.TabPage tabPageSettings;
		private System.Windows.Forms.TabPage tabPageSearch;
		private System.Windows.Forms.TabPage tabPageSearchResults;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnSearchByQuery;
		private System.Windows.Forms.TextBox editQuery;
		private System.Windows.Forms.Panel panelSearchResults;
		private BrightIdeasSoftware.TreeListView tvFavorites;
		private BrightIdeasSoftware.OLVColumn olvColumn1;
		private System.Windows.Forms.Button btnBrowseTempPath;
		private System.Windows.Forms.Button btnBrowseDowloadingPath;
		private System.Windows.Forms.TextBox editTempDirPath;
		private System.Windows.Forms.TextBox editDownloadingDirPath;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox editSearchUrl;
		private System.Windows.Forms.Button btnSearchByUrl;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.DateTimePicker dateTimePickerBefore;
		private System.Windows.Forms.DateTimePicker dateTimePickerAfter;
		private System.Windows.Forms.CheckBox chkPublishedAfter;
		private System.Windows.Forms.CheckBox chkPublishedBefore;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox editCipherDecryptionAlgo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox editYouTubeApiKey;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.NumericUpDown numericUpDownSearchResult;
		private System.Windows.Forms.RadioButton rbSearchResultsUserDefined;
		private System.Windows.Forms.RadioButton rbSearchResultsMax;
		private System.Windows.Forms.Button btnSelectBrowser;
		private System.Windows.Forms.TextBox editBrowser;
		private System.Windows.Forms.ContextMenuStrip menuFavorites;
		private System.Windows.Forms.ToolStripMenuItem openVideoInBrowserToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openChannelInBrowserToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage tabPageFilesAndFolders;
		private System.Windows.Forms.TextBox editOutputFileNameFormatWithoutDate;
		private System.Windows.Forms.Button btnResetFileNameFormatWithoutDate;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TabPage tabPageSystemSettings;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox groupBox7;
		private System.Windows.Forms.NumericUpDown numericUpDownThreadsAudio;
		private System.Windows.Forms.NumericUpDown numericUpDownThreadsVideo;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chkUseHiddenApiForGettingInfo;
		private System.Windows.Forms.Button btnBrowseFfmpeg;
		private System.Windows.Forms.TextBox editFfmpeg;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.CheckBox chkSearchVideos;
		private System.Windows.Forms.CheckBox chkSearchChannels;
		private System.Windows.Forms.TabPage tabPageDownloadingSettings;
		private System.Windows.Forms.CheckBox chkDeleteSourceFiles;
		private System.Windows.Forms.CheckBox chkMergeAdaptive;
		private System.Windows.Forms.CheckBox chkSaveImage;
		private System.Windows.Forms.Panel panelSearch;
		private System.Windows.Forms.GroupBox groupBox9;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button btnSearchByWebPage;
		private System.Windows.Forms.RichTextBox richTextBoxWebPage;
		private System.Windows.Forms.ContextMenuStrip menuCopyPaste;
		private System.Windows.Forms.ToolStripMenuItem cutTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteTextToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem selectAllTextToolStripMenuItem;
		private System.Windows.Forms.Panel panelWarningVideoThreads;
		private System.Windows.Forms.Panel panelWarningAudioThreads;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown numericUpDownGlobalThreadsMaximum;
		private System.Windows.Forms.GroupBox groupBox10;
		private System.Windows.Forms.ToolStripMenuItem copyDisplayNameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyDisplayNameWithIdToolStripMenuItem;
		private System.Windows.Forms.VScrollBar scrollBarSearchResults;
		private System.Windows.Forms.Button btnWhy;
		private System.Windows.Forms.TextBox editMergingDirPath;
		private System.Windows.Forms.Button btnSelectMergingPath;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Button btnQ;
		private System.Windows.Forms.GroupBox groupBox11;
		private System.Windows.Forms.Button btnApiWtf;
		private System.Windows.Forms.ToolStripMenuItem miCopyVideoUrlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyChannelUrlToolStripMenuItem;
		private System.Windows.Forms.TabPage tabPageGUI;
		private System.Windows.Forms.GroupBox groupBoxFonts;
		private System.Windows.Forms.NumericUpDown numericUpDownMenusFontSize;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.NumericUpDown numericUpDownFavoritesListFontSize;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.NumericUpDown numericUpDownVideoTitleFontSize;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.GroupBox groupBox12;
		private System.Windows.Forms.CheckBox chkSortFormatsByFileSize;
		private System.Windows.Forms.CheckBox chkMoveAudioId140First;
		private System.Windows.Forms.CheckBox chkUseRamForTempFiles;
		private System.Windows.Forms.GroupBox groupBox13;
		private System.Windows.Forms.Panel panelRAM;
		private System.Windows.Forms.Button btnUseRamWhy;
		private System.Windows.Forms.GroupBox groupBox14;
		private System.Windows.Forms.CheckBox chkDownloadFirstAudioTrack;
		private System.Windows.Forms.CheckBox chkIfOnlyBiggerFileSize;
		private System.Windows.Forms.CheckBox chkDownloadSecondAudioTrack;
		private System.Windows.Forms.CheckBox chkDownloadAllAudioTracks;
		private System.Windows.Forms.GroupBox groupBox15;
		private System.Windows.Forms.CheckBox chkDownloadAllAdaptiveVideoTracks;
		private System.Windows.Forms.Button btnDownloadAllAdaptiveVideoTracksWtf;
		private System.Windows.Forms.GroupBox groupBox16;
		private System.Windows.Forms.RadioButton radioButtonContainerTypeMkv;
		private System.Windows.Forms.RadioButton radioButtonContainerTypeMp4;
		private System.Windows.Forms.CheckBox chkSortDashFormatsByBitrate;
		private System.Windows.Forms.ToolStripMenuItem miCopyVideoIdToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyChannelIdToolStripMenuItem;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox editOutputFileNameFormatWithDate;
		private System.Windows.Forms.Button btnResetFileNameFormatWithDate;
		private System.Windows.Forms.CheckBox checkBoxUseGmtTime;
        private System.Windows.Forms.NumericUpDown numericUpDownDelayAfterContainerCreated;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.NumericUpDown numericUpDownDownloadRetryCount;
        private System.Windows.Forms.Label label21;
    }
}

