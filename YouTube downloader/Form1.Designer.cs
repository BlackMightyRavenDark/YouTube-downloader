
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
			this.frameSettingsFilesAndFolders1 = new YouTube_downloader.FrameSettingsFilesAndFolders();
			this.tabPageGUI = new System.Windows.Forms.TabPage();
			this.frameSettingsGuiOptions1 = new YouTube_downloader.FrameSettingsGuiOptions();
			this.tabPageDownloadSettings = new System.Windows.Forms.TabPage();
			this.frameSettingsDownloadOptions1 = new YouTube_downloader.FrameSettingsDownloadOptions();
			this.tabPageSystemSettings = new System.Windows.Forms.TabPage();
			this.frameSettingsSystemOptions = new YouTube_downloader.FrameSettingsSystemOptions();
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
			this.btnWtfSearchApiV3 = new System.Windows.Forms.Button();
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
			this.tabControlMain.SuspendLayout();
			this.tabPageSettings.SuspendLayout();
			this.tabControlSettings.SuspendLayout();
			this.tabPageFilesAndFolders.SuspendLayout();
			this.tabPageGUI.SuspendLayout();
			this.tabPageDownloadSettings.SuspendLayout();
			this.tabPageSystemSettings.SuspendLayout();
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
			this.tabPageFilesAndFolders.Controls.Add(this.frameSettingsFilesAndFolders1);
			this.tabPageFilesAndFolders.Location = new System.Drawing.Point(4, 22);
			this.tabPageFilesAndFolders.Name = "tabPageFilesAndFolders";
			this.tabPageFilesAndFolders.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageFilesAndFolders.Size = new System.Drawing.Size(527, 357);
			this.tabPageFilesAndFolders.TabIndex = 0;
			this.tabPageFilesAndFolders.Text = "Файлы и папки";
			// 
			// frameSettingsFilesAndFolders1
			// 
			this.frameSettingsFilesAndFolders1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.frameSettingsFilesAndFolders1.AutoScroll = true;
			this.frameSettingsFilesAndFolders1.Location = new System.Drawing.Point(6, 6);
			this.frameSettingsFilesAndFolders1.Name = "frameSettingsFilesAndFolders1";
			this.frameSettingsFilesAndFolders1.Size = new System.Drawing.Size(515, 345);
			this.frameSettingsFilesAndFolders1.TabIndex = 0;
			// 
			// tabPageGUI
			// 
			this.tabPageGUI.BackColor = System.Drawing.SystemColors.Control;
			this.tabPageGUI.Controls.Add(this.frameSettingsGuiOptions1);
			this.tabPageGUI.Location = new System.Drawing.Point(4, 22);
			this.tabPageGUI.Name = "tabPageGUI";
			this.tabPageGUI.Size = new System.Drawing.Size(527, 357);
			this.tabPageGUI.TabIndex = 3;
			this.tabPageGUI.Text = "Интерфейс";
			// 
			// frameSettingsGuiOptions1
			// 
			this.frameSettingsGuiOptions1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.frameSettingsGuiOptions1.AutoScroll = true;
			this.frameSettingsGuiOptions1.Location = new System.Drawing.Point(3, 3);
			this.frameSettingsGuiOptions1.Name = "frameSettingsGuiOptions1";
			this.frameSettingsGuiOptions1.Size = new System.Drawing.Size(521, 351);
			this.frameSettingsGuiOptions1.TabIndex = 0;
			// 
			// tabPageDownloadSettings
			// 
			this.tabPageDownloadSettings.AutoScroll = true;
			this.tabPageDownloadSettings.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.tabPageDownloadSettings.Controls.Add(this.frameSettingsDownloadOptions1);
			this.tabPageDownloadSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageDownloadSettings.Name = "tabPageDownloadSettings";
			this.tabPageDownloadSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageDownloadSettings.Size = new System.Drawing.Size(527, 357);
			this.tabPageDownloadSettings.TabIndex = 2;
			this.tabPageDownloadSettings.Text = "Скачивание";
			// 
			// frameSettingsDownloadOptions1
			// 
			this.frameSettingsDownloadOptions1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.frameSettingsDownloadOptions1.AutoScroll = true;
			this.frameSettingsDownloadOptions1.Location = new System.Drawing.Point(6, 6);
			this.frameSettingsDownloadOptions1.MinimumSize = new System.Drawing.Size(490, 0);
			this.frameSettingsDownloadOptions1.Name = "frameSettingsDownloadOptions1";
			this.frameSettingsDownloadOptions1.Size = new System.Drawing.Size(515, 333);
			this.frameSettingsDownloadOptions1.TabIndex = 0;
			// 
			// tabPageSystemSettings
			// 
			this.tabPageSystemSettings.AutoScroll = true;
			this.tabPageSystemSettings.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.tabPageSystemSettings.Controls.Add(this.frameSettingsSystemOptions);
			this.tabPageSystemSettings.Location = new System.Drawing.Point(4, 22);
			this.tabPageSystemSettings.Name = "tabPageSystemSettings";
			this.tabPageSystemSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tabPageSystemSettings.Size = new System.Drawing.Size(527, 357);
			this.tabPageSystemSettings.TabIndex = 1;
			this.tabPageSystemSettings.Text = "Система";
			// 
			// frameSettingsSystemOptions
			// 
			this.frameSettingsSystemOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.frameSettingsSystemOptions.AutoScroll = true;
			this.frameSettingsSystemOptions.Location = new System.Drawing.Point(3, 6);
			this.frameSettingsSystemOptions.MinimumSize = new System.Drawing.Size(460, 0);
			this.frameSettingsSystemOptions.Name = "frameSettingsSystemOptions";
			this.frameSettingsSystemOptions.Size = new System.Drawing.Size(521, 345);
			this.frameSettingsSystemOptions.TabIndex = 0;
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
			this.groupBoxQuerySearch.Controls.Add(this.btnWtfSearchApiV3);
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
			// btnWtfSearchApiV3
			// 
			this.btnWtfSearchApiV3.Location = new System.Drawing.Point(15, 61);
			this.btnWtfSearchApiV3.Name = "btnWtfSearchApiV3";
			this.btnWtfSearchApiV3.Size = new System.Drawing.Size(20, 23);
			this.btnWtfSearchApiV3.TabIndex = 3;
			this.btnWtfSearchApiV3.Text = "!";
			this.btnWtfSearchApiV3.UseVisualStyleBackColor = true;
			this.btnWtfSearchApiV3.Click += new System.EventHandler(this.btnWtfSearchApiV3_Click);
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
			this.tabPageGUI.ResumeLayout(false);
			this.tabPageDownloadSettings.ResumeLayout(false);
			this.tabPageSystemSettings.ResumeLayout(false);
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
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.NumericUpDown numericUpDownSearchResultCountLimit;
		private System.Windows.Forms.RadioButton radioButtonSearchResultCountLimitUserDefinedNumber;
		private System.Windows.Forms.RadioButton radioButtonSearchResultCountLimitMaxPossible;
		private System.Windows.Forms.ContextMenuStrip contextMenuFavorites;
		private System.Windows.Forms.ToolStripMenuItem miOpenVideoInBrowserToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miOpenChannelInBrowserToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControlSettings;
		private System.Windows.Forms.TabPage tabPageFilesAndFolders;
		private System.Windows.Forms.TabPage tabPageSystemSettings;
		private System.Windows.Forms.GroupBox groupBox8;
		private System.Windows.Forms.CheckBox checkBoxSearchVideos;
		private System.Windows.Forms.CheckBox checkBoxSearchChannels;
		private System.Windows.Forms.TabPage tabPageDownloadSettings;
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
		private System.Windows.Forms.ToolStripMenuItem miCopyDisplayNameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyDisplayNameWithIdToolStripMenuItem;
		private System.Windows.Forms.VScrollBar scrollBarSearchResults;
		private System.Windows.Forms.Button btnWtfWebPageCode;
		private System.Windows.Forms.ToolStripMenuItem miCopyVideoUrlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyChannelUrlToolStripMenuItem;
		private System.Windows.Forms.TabPage tabPageGUI;
		private System.Windows.Forms.ToolStripMenuItem miCopyVideoIdToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyChannelIdToolStripMenuItem;
		private FrameSettingsSystemOptions frameSettingsSystemOptions;
		private FrameSettingsDownloadOptions frameSettingsDownloadOptions1;
		private FrameSettingsGuiOptions frameSettingsGuiOptions1;
		private FrameSettingsFilesAndFolders frameSettingsFilesAndFolders1;
		private System.Windows.Forms.Button btnWtfSearchApiV3;
	}
}

