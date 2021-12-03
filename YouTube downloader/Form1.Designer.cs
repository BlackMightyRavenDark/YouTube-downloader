
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSettings = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageFilesAndFolders = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.btnBrowseFfmpeg = new System.Windows.Forms.Button();
            this.editFfmpeg = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.editOutputFileNameFormat = new System.Windows.Forms.TextBox();
            this.btnResetFileNameFormat = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnBrowseDowloadingPath = new System.Windows.Forms.Button();
            this.btnSelectBrowser = new System.Windows.Forms.Button();
            this.editDownloadingPath = new System.Windows.Forms.TextBox();
            this.editBrowser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.editTempPath = new System.Windows.Forms.TextBox();
            this.btnBrowseTempPath = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.chkDeleteSourceFiles = new System.Windows.Forms.CheckBox();
            this.chkMergeAdaptive = new System.Windows.Forms.CheckBox();
            this.chkSaveImage = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkUseApiForGettingInfo = new System.Windows.Forms.CheckBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDownGlobalThreadsMaximum = new System.Windows.Forms.NumericUpDown();
            this.panelWarningAudioThreads = new System.Windows.Forms.Panel();
            this.panelWarningVideoThreads = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDownThreadsAudio = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownThreadsVideo = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.editCipherDecryptionAlgo = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.editYouTubeApiKey = new System.Windows.Forms.TextBox();
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
            this.openChannelInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDisplayNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDisplayNameWithIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageSettings.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPageFilesAndFolders.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGlobalThreadsMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadsAudio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadsVideo)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPageSettings);
            this.tabControl1.Controls.Add(this.tabPageSearch);
            this.tabControl1.Controls.Add(this.tabPageSearchResults);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(555, 420);
            this.tabControl1.TabIndex = 0;
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
            this.tabControl2.Controls.Add(this.tabPage1);
            this.tabControl2.Controls.Add(this.tabPage2);
            this.tabControl2.Location = new System.Drawing.Point(6, 8);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(535, 383);
            this.tabControl2.TabIndex = 12;
            // 
            // tabPageFilesAndFolders
            // 
            this.tabPageFilesAndFolders.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPageFilesAndFolders.Controls.Add(this.label11);
            this.tabPageFilesAndFolders.Controls.Add(this.btnBrowseFfmpeg);
            this.tabPageFilesAndFolders.Controls.Add(this.editFfmpeg);
            this.tabPageFilesAndFolders.Controls.Add(this.label8);
            this.tabPageFilesAndFolders.Controls.Add(this.editOutputFileNameFormat);
            this.tabPageFilesAndFolders.Controls.Add(this.btnResetFileNameFormat);
            this.tabPageFilesAndFolders.Controls.Add(this.label7);
            this.tabPageFilesAndFolders.Controls.Add(this.btnBrowseDowloadingPath);
            this.tabPageFilesAndFolders.Controls.Add(this.btnSelectBrowser);
            this.tabPageFilesAndFolders.Controls.Add(this.editDownloadingPath);
            this.tabPageFilesAndFolders.Controls.Add(this.editBrowser);
            this.tabPageFilesAndFolders.Controls.Add(this.label3);
            this.tabPageFilesAndFolders.Controls.Add(this.editTempPath);
            this.tabPageFilesAndFolders.Controls.Add(this.btnBrowseTempPath);
            this.tabPageFilesAndFolders.Controls.Add(this.label4);
            this.tabPageFilesAndFolders.Location = new System.Drawing.Point(4, 22);
            this.tabPageFilesAndFolders.Name = "tabPageFilesAndFolders";
            this.tabPageFilesAndFolders.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFilesAndFolders.Size = new System.Drawing.Size(527, 357);
            this.tabPageFilesAndFolders.TabIndex = 0;
            this.tabPageFilesAndFolders.Text = "Файлы и папки";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 165);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Путь к FFMPEG.EXE:";
            // 
            // btnBrowseFfmpeg
            // 
            this.btnBrowseFfmpeg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFfmpeg.Location = new System.Drawing.Point(489, 181);
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
            this.editFfmpeg.Location = new System.Drawing.Point(13, 181);
            this.editFfmpeg.Name = "editFfmpeg";
            this.editFfmpeg.Size = new System.Drawing.Size(470, 20);
            this.editFfmpeg.TabIndex = 16;
            this.editFfmpeg.Leave += new System.EventHandler(this.editFfmpeg_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 87);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Формат имени файла:";
            // 
            // editOutputFileNameFormat
            // 
            this.editOutputFileNameFormat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editOutputFileNameFormat.Location = new System.Drawing.Point(13, 103);
            this.editOutputFileNameFormat.Name = "editOutputFileNameFormat";
            this.editOutputFileNameFormat.Size = new System.Drawing.Size(392, 20);
            this.editOutputFileNameFormat.TabIndex = 14;
            this.editOutputFileNameFormat.Leave += new System.EventHandler(this.editOutputFileNameFormat_Leave);
            // 
            // btnResetFileNameFormat
            // 
            this.btnResetFileNameFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetFileNameFormat.Location = new System.Drawing.Point(411, 103);
            this.btnResetFileNameFormat.Name = "btnResetFileNameFormat";
            this.btnResetFileNameFormat.Size = new System.Drawing.Size(110, 20);
            this.btnResetFileNameFormat.TabIndex = 13;
            this.btnResetFileNameFormat.Text = "Вернуть как было";
            this.btnResetFileNameFormat.UseVisualStyleBackColor = true;
            this.btnResetFileNameFormat.Click += new System.EventHandler(this.btnResetFileNameFormat_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 126);
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
            this.btnBrowseDowloadingPath.Click += new System.EventHandler(this.btnBrowseDowloadingPath_Click);
            // 
            // btnSelectBrowser
            // 
            this.btnSelectBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectBrowser.Location = new System.Drawing.Point(489, 142);
            this.btnSelectBrowser.Name = "btnSelectBrowser";
            this.btnSelectBrowser.Size = new System.Drawing.Size(32, 20);
            this.btnSelectBrowser.TabIndex = 11;
            this.btnSelectBrowser.Text = "...";
            this.btnSelectBrowser.UseVisualStyleBackColor = true;
            this.btnSelectBrowser.Click += new System.EventHandler(this.btnSelectBrowser_Click);
            // 
            // editDownloadingPath
            // 
            this.editDownloadingPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editDownloadingPath.Location = new System.Drawing.Point(13, 23);
            this.editDownloadingPath.Name = "editDownloadingPath";
            this.editDownloadingPath.Size = new System.Drawing.Size(470, 20);
            this.editDownloadingPath.TabIndex = 0;
            this.editDownloadingPath.Leave += new System.EventHandler(this.editDownloadingPath_Leave);
            // 
            // editBrowser
            // 
            this.editBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editBrowser.Location = new System.Drawing.Point(13, 142);
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
            // editTempPath
            // 
            this.editTempPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editTempPath.Location = new System.Drawing.Point(13, 64);
            this.editTempPath.Name = "editTempPath";
            this.editTempPath.Size = new System.Drawing.Size(470, 20);
            this.editTempPath.TabIndex = 1;
            this.editTempPath.Leave += new System.EventHandler(this.editTempPath_Leave);
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
            this.btnBrowseTempPath.Click += new System.EventHandler(this.btnBrowseTempPath_Click);
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
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage1.Controls.Add(this.groupBox10);
            this.tabPage1.Controls.Add(this.chkSaveImage);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(527, 357);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Скачивание";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.chkDeleteSourceFiles);
            this.groupBox10.Controls.Add(this.chkMergeAdaptive);
            this.groupBox10.Location = new System.Drawing.Point(6, 7);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(268, 63);
            this.groupBox10.TabIndex = 17;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Адаптивные форматы";
            // 
            // chkDeleteSourceFiles
            // 
            this.chkDeleteSourceFiles.AutoSize = true;
            this.chkDeleteSourceFiles.Location = new System.Drawing.Point(25, 42);
            this.chkDeleteSourceFiles.Name = "chkDeleteSourceFiles";
            this.chkDeleteSourceFiles.Size = new System.Drawing.Size(158, 17);
            this.chkDeleteSourceFiles.TabIndex = 16;
            this.chkDeleteSourceFiles.Text = "Удалить исходные файлы";
            this.chkDeleteSourceFiles.UseVisualStyleBackColor = true;
            this.chkDeleteSourceFiles.CheckedChanged += new System.EventHandler(this.chkDeleteSourceFiles_CheckedChanged);
            // 
            // chkMergeAdaptive
            // 
            this.chkMergeAdaptive.AutoSize = true;
            this.chkMergeAdaptive.Location = new System.Drawing.Point(14, 19);
            this.chkMergeAdaptive.Name = "chkMergeAdaptive";
            this.chkMergeAdaptive.Size = new System.Drawing.Size(254, 17);
            this.chkMergeAdaptive.TabIndex = 14;
            this.chkMergeAdaptive.Text = "Объединять видео и аудио в один контейнер";
            this.chkMergeAdaptive.UseVisualStyleBackColor = true;
            this.chkMergeAdaptive.CheckedChanged += new System.EventHandler(this.chkMergeAdaptive_CheckedChanged);
            // 
            // chkSaveImage
            // 
            this.chkSaveImage.AutoSize = true;
            this.chkSaveImage.Location = new System.Drawing.Point(280, 7);
            this.chkSaveImage.Name = "chkSaveImage";
            this.chkSaveImage.Size = new System.Drawing.Size(175, 17);
            this.chkSaveImage.TabIndex = 15;
            this.chkSaveImage.Text = "Скачивать картинку от видео";
            this.chkSaveImage.UseVisualStyleBackColor = true;
            this.chkSaveImage.CheckedChanged += new System.EventHandler(this.chkSaveImage_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabPage2.Controls.Add(this.chkUseApiForGettingInfo);
            this.tabPage2.Controls.Add(this.groupBox7);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(527, 357);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Система";
            // 
            // chkUseApiForGettingInfo
            // 
            this.chkUseApiForGettingInfo.AutoSize = true;
            this.chkUseApiForGettingInfo.Location = new System.Drawing.Point(6, 12);
            this.chkUseApiForGettingInfo.Name = "chkUseApiForGettingInfo";
            this.chkUseApiForGettingInfo.Size = new System.Drawing.Size(304, 17);
            this.chkUseApiForGettingInfo.TabIndex = 11;
            this.chkUseApiForGettingInfo.Text = "Использовать API для получения информации о видео";
            this.toolTip1.SetToolTip(this.chkUseApiForGettingInfo, "Позволяет немного оттянуть неизбжный момент возникновения ошибки \"HTTP 429: Too m" +
        "any requests\".");
            this.chkUseApiForGettingInfo.UseVisualStyleBackColor = true;
            this.chkUseApiForGettingInfo.Click += new System.EventHandler(this.chkUseApiForGettingInfo_Click);
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
            this.groupBox7.Location = new System.Drawing.Point(6, 166);
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
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.editCipherDecryptionAlgo);
            this.groupBox4.Location = new System.Drawing.Point(6, 102);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(515, 58);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Алгоитм для расшифровки Cipher";
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
            this.editCipherDecryptionAlgo.Size = new System.Drawing.Size(499, 20);
            this.editCipherDecryptionAlgo.TabIndex = 0;
            this.editCipherDecryptionAlgo.Leave += new System.EventHandler(this.editCipherDecryptionAlgo_Leave);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.editYouTubeApiKey);
            this.groupBox5.Location = new System.Drawing.Point(6, 35);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(515, 61);
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
            this.editYouTubeApiKey.Size = new System.Drawing.Size(503, 20);
            this.editYouTubeApiKey.TabIndex = 0;
            this.editYouTubeApiKey.Leave += new System.EventHandler(this.editYouTubeApiKey_Leave);
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
            this.rbSearchResultsMax.Click += new System.EventHandler(this.rbSearchRessultsMax_Click);
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
            this.openChannelInBrowserToolStripMenuItem,
            this.copyDisplayNameToolStripMenuItem,
            this.copyDisplayNameWithIdToolStripMenuItem});
            this.menuFavorites.Name = "menuFavarites";
            this.menuFavorites.Size = new System.Drawing.Size(281, 92);
            this.menuFavorites.Opening += new System.ComponentModel.CancelEventHandler(this.menuFavorites_Opening);
            // 
            // openVideoInBrowserToolStripMenuItem
            // 
            this.openVideoInBrowserToolStripMenuItem.Name = "openVideoInBrowserToolStripMenuItem";
            this.openVideoInBrowserToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.openVideoInBrowserToolStripMenuItem.Text = "Открыть видео в браузере";
            this.openVideoInBrowserToolStripMenuItem.Click += new System.EventHandler(this.openVideoInBrowserToolStripMenuItem_Click);
            // 
            // openChannelInBrowserToolStripMenuItem
            // 
            this.openChannelInBrowserToolStripMenuItem.Name = "openChannelInBrowserToolStripMenuItem";
            this.openChannelInBrowserToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.openChannelInBrowserToolStripMenuItem.Text = "Открыть канал в браузере";
            this.openChannelInBrowserToolStripMenuItem.Click += new System.EventHandler(this.openChannelInBrowserToolStripMenuItem_Click);
            // 
            // copyDisplayNameToolStripMenuItem
            // 
            this.copyDisplayNameToolStripMenuItem.Name = "copyDisplayNameToolStripMenuItem";
            this.copyDisplayNameToolStripMenuItem.Size = new System.Drawing.Size(280, 22);
            this.copyDisplayNameToolStripMenuItem.Text = "Скопировать отображаемое имя";
            this.copyDisplayNameToolStripMenuItem.Click += new System.EventHandler(this.copyDisplayNameToolStripMenuItem_Click);
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
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(820, 440);
            this.Name = "Form1";
            this.Text = "Скачивалка с ютуба";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSettings.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPageFilesAndFolders.ResumeLayout(false);
            this.tabPageFilesAndFolders.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownGlobalThreadsMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadsAudio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownThreadsVideo)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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

        private System.Windows.Forms.TabControl tabControl1;
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
        private System.Windows.Forms.TextBox editTempPath;
        private System.Windows.Forms.TextBox editDownloadingPath;
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
        private System.Windows.Forms.TextBox editOutputFileNameFormat;
        private System.Windows.Forms.Button btnResetFileNameFormat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.NumericUpDown numericUpDownThreadsAudio;
        private System.Windows.Forms.NumericUpDown numericUpDownThreadsVideo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkUseApiForGettingInfo;
        private System.Windows.Forms.Button btnBrowseFfmpeg;
        private System.Windows.Forms.TextBox editFfmpeg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox chkSearchVideos;
        private System.Windows.Forms.CheckBox chkSearchChannels;
        private System.Windows.Forms.TabPage tabPage1;
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
    }
}

