namespace YouTube_downloader
{
	partial class FrameYouTubeVideo
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

		#region Код, автоматически созданный конструктором компонентов

		/// <summary> 
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.lblVideoTitle = new System.Windows.Forms.Label();
			this.pictureBoxVideoThumbnail = new System.Windows.Forms.PictureBox();
			this.lblChannelTitle = new System.Windows.Forms.Label();
			this.lblDatePublished = new System.Windows.Forms.Label();
			this.pictureBoxFavoriteVideo = new System.Windows.Forms.PictureBox();
			this.btnDownload = new System.Windows.Forms.Button();
			this.lblStatus = new System.Windows.Forms.Label();
			this.lblDowndloadProgress = new System.Windows.Forms.Label();
			this.pictureBoxFavoriteChannel = new System.Windows.Forms.PictureBox();
			this.contextMenuThumnailImage = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miOpenVideoInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miCopyVideoUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miCopyVideoIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miCopyPlayerUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.miOpenThumnailImageInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miCopyThumbnailUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miSaveThumbnailImageAssToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.miReloadActiveThumbnailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miThumbnailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuChannelTitle = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miCopyChannelTitleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.miCopyChannelIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miCopyChannelNameWithIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miCopyChannelUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miOpenChannelInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuVideoTitle = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miCopyTitleAsIsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miCopyFixedTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.miCopyFormattedFileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuDate = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miCopyVideoPublishedDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miUpdateVideoPublishedDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuProgressBar = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miSingleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miMultipleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuFrameActions = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.miActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miGetVideoWebPageCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miGetVideoInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miGetDownloadUrlsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miGetDashManifestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miGetHlsManifestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miGetPlayerCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miOptimizeFormatListReceivingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miUpdateFormatListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.lblBtnOpenFrameContextMenu = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.progressBarDownload = new YouTube_downloader.MultipleProgressBar();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideoThumbnail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxFavoriteVideo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxFavoriteChannel)).BeginInit();
			this.contextMenuThumnailImage.SuspendLayout();
			this.contextMenuChannelTitle.SuspendLayout();
			this.contextMenuVideoTitle.SuspendLayout();
			this.contextMenuDate.SuspendLayout();
			this.contextMenuProgressBar.SuspendLayout();
			this.contextMenuFrameActions.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblVideoTitle
			// 
			this.lblVideoTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.lblVideoTitle.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblVideoTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblVideoTitle.Location = new System.Drawing.Point(179, 12);
			this.lblVideoTitle.Name = "lblVideoTitle";
			this.lblVideoTitle.Size = new System.Drawing.Size(277, 68);
			this.lblVideoTitle.TabIndex = 0;
			this.lblVideoTitle.Text = "lblVideoTitle";
			this.lblVideoTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblVideoTitle_MouseDown);
			// 
			// pictureBoxVideoThumbnail
			// 
			this.pictureBoxVideoThumbnail.BackColor = System.Drawing.SystemColors.ControlLight;
			this.pictureBoxVideoThumbnail.Location = new System.Drawing.Point(3, 12);
			this.pictureBoxVideoThumbnail.Name = "pictureBoxVideoThumbnail";
			this.pictureBoxVideoThumbnail.Size = new System.Drawing.Size(170, 111);
			this.pictureBoxVideoThumbnail.TabIndex = 1;
			this.pictureBoxVideoThumbnail.TabStop = false;
			this.pictureBoxVideoThumbnail.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxVideoThumbnail_Paint);
			this.pictureBoxVideoThumbnail.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxVideoThumbnail_MouseDown);
			// 
			// lblChannelTitle
			// 
			this.lblChannelTitle.AutoSize = true;
			this.lblChannelTitle.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblChannelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblChannelTitle.Location = new System.Drawing.Point(213, 86);
			this.lblChannelTitle.Name = "lblChannelTitle";
			this.lblChannelTitle.Size = new System.Drawing.Size(96, 16);
			this.lblChannelTitle.TabIndex = 2;
			this.lblChannelTitle.Text = "lblChannelTitle";
			this.lblChannelTitle.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lblChannelTitle_MouseDoubleClick);
			this.lblChannelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblChannelTitle_MouseDown);
			// 
			// lblDatePublished
			// 
			this.lblDatePublished.AutoSize = true;
			this.lblDatePublished.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblDatePublished.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblDatePublished.Location = new System.Drawing.Point(179, 107);
			this.lblDatePublished.Name = "lblDatePublished";
			this.lblDatePublished.Size = new System.Drawing.Size(110, 16);
			this.lblDatePublished.TabIndex = 3;
			this.lblDatePublished.Text = "lblDatePublished";
			this.lblDatePublished.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblDatePublished_MouseDown);
			// 
			// pictureBoxFavoriteVideo
			// 
			this.pictureBoxFavoriteVideo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxFavoriteVideo.Location = new System.Drawing.Point(462, 12);
			this.pictureBoxFavoriteVideo.Name = "pictureBoxFavoriteVideo";
			this.pictureBoxFavoriteVideo.Size = new System.Drawing.Size(25, 25);
			this.pictureBoxFavoriteVideo.TabIndex = 4;
			this.pictureBoxFavoriteVideo.TabStop = false;
			this.toolTip1.SetToolTip(this.pictureBoxFavoriteVideo, "Избранное видео");
			this.pictureBoxFavoriteVideo.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxFavoriteVideo_Paint);
			this.pictureBoxFavoriteVideo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxFavoriteVideo_MouseDown);
			// 
			// btnDownload
			// 
			this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDownload.BackColor = System.Drawing.SystemColors.ControlLight;
			this.btnDownload.Location = new System.Drawing.Point(425, 142);
			this.btnDownload.Name = "btnDownload";
			this.btnDownload.Size = new System.Drawing.Size(62, 23);
			this.btnDownload.TabIndex = 5;
			this.btnDownload.Text = "Скачать";
			this.btnDownload.UseVisualStyleBackColor = false;
			this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
			this.btnDownload.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDownload_MouseDown);
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(3, 126);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(47, 13);
			this.lblStatus.TabIndex = 7;
			this.lblStatus.Text = "lblStatus";
			this.lblStatus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblStatus_MouseDown);
			// 
			// lblDowndloadProgress
			// 
			this.lblDowndloadProgress.AutoSize = true;
			this.lblDowndloadProgress.Location = new System.Drawing.Point(56, 126);
			this.lblDowndloadProgress.Name = "lblDowndloadProgress";
			this.lblDowndloadProgress.Size = new System.Drawing.Size(58, 13);
			this.lblDowndloadProgress.TabIndex = 8;
			this.lblDowndloadProgress.Text = "lblProgress";
			this.lblDowndloadProgress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblDowndloadProgress_MouseDown);
			// 
			// pictureBoxFavoriteChannel
			// 
			this.pictureBoxFavoriteChannel.Location = new System.Drawing.Point(182, 81);
			this.pictureBoxFavoriteChannel.Name = "pictureBoxFavoriteChannel";
			this.pictureBoxFavoriteChannel.Size = new System.Drawing.Size(25, 25);
			this.pictureBoxFavoriteChannel.TabIndex = 11;
			this.pictureBoxFavoriteChannel.TabStop = false;
			this.toolTip1.SetToolTip(this.pictureBoxFavoriteChannel, "Избранный канал");
			this.pictureBoxFavoriteChannel.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxFavoriteChannel_Paint);
			this.pictureBoxFavoriteChannel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxFavoriteChannel_MouseDown);
			// 
			// contextMenuThumnailImage
			// 
			this.contextMenuThumnailImage.Font = new System.Drawing.Font("Lucida Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.contextMenuThumnailImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.miOpenVideoInBrowserToolStripMenuItem,
			this.miCopyVideoUrlToolStripMenuItem,
			this.miCopyVideoIdToolStripMenuItem,
			this.miCopyPlayerUrlToolStripMenuItem,
			this.toolStripMenuItem2,
			this.miOpenThumnailImageInBrowserToolStripMenuItem,
			this.miCopyThumbnailUrlToolStripMenuItem,
			this.miSaveThumbnailImageAssToolStripMenuItem,
			this.toolStripMenuItem3,
			this.miReloadActiveThumbnailToolStripMenuItem,
			this.miThumbnailsToolStripMenuItem});
			this.contextMenuThumnailImage.Name = "contextMenuImage";
			this.contextMenuThumnailImage.Size = new System.Drawing.Size(302, 214);
			// 
			// miOpenVideoInBrowserToolStripMenuItem
			// 
			this.miOpenVideoInBrowserToolStripMenuItem.Name = "miOpenVideoInBrowserToolStripMenuItem";
			this.miOpenVideoInBrowserToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
			this.miOpenVideoInBrowserToolStripMenuItem.Text = "Открыть видео в браузере";
			this.miOpenVideoInBrowserToolStripMenuItem.Click += new System.EventHandler(this.miOpenVideoInBrowserToolStripMenuItem_Click);
			// 
			// miCopyVideoUrlToolStripMenuItem
			// 
			this.miCopyVideoUrlToolStripMenuItem.Name = "miCopyVideoUrlToolStripMenuItem";
			this.miCopyVideoUrlToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
			this.miCopyVideoUrlToolStripMenuItem.Text = "Скопировать ссылку на видео";
			this.miCopyVideoUrlToolStripMenuItem.Click += new System.EventHandler(this.miCopyVideoUrlToolStripMenuItem_Click);
			// 
			// miCopyVideoIdToolStripMenuItem
			// 
			this.miCopyVideoIdToolStripMenuItem.Name = "miCopyVideoIdToolStripMenuItem";
			this.miCopyVideoIdToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
			this.miCopyVideoIdToolStripMenuItem.Text = "Скопировать ID видео";
			this.miCopyVideoIdToolStripMenuItem.Click += new System.EventHandler(this.miCopyVideoIdToolStripMenuItem_Click);
			// 
			// miCopyPlayerUrlToolStripMenuItem
			// 
			this.miCopyPlayerUrlToolStripMenuItem.Name = "miCopyPlayerUrlToolStripMenuItem";
			this.miCopyPlayerUrlToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
			this.miCopyPlayerUrlToolStripMenuItem.Text = "Скопировать ссылку на код плеера";
			this.miCopyPlayerUrlToolStripMenuItem.Click += new System.EventHandler(this.miCopyPlayerUrlToolStripMenuItem_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(298, 6);
			// 
			// miOpenThumnailImageInBrowserToolStripMenuItem
			// 
			this.miOpenThumnailImageInBrowserToolStripMenuItem.Name = "miOpenThumnailImageInBrowserToolStripMenuItem";
			this.miOpenThumnailImageInBrowserToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
			this.miOpenThumnailImageInBrowserToolStripMenuItem.Text = "Открыть изображение в браузере";
			this.miOpenThumnailImageInBrowserToolStripMenuItem.Click += new System.EventHandler(this.miOpenThumbnailImageInBrowserToolStripMenuItem_Click);
			// 
			// miCopyThumbnailUrlToolStripMenuItem
			// 
			this.miCopyThumbnailUrlToolStripMenuItem.Name = "miCopyThumbnailUrlToolStripMenuItem";
			this.miCopyThumbnailUrlToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
			this.miCopyThumbnailUrlToolStripMenuItem.Text = "Скопировать ссылку на изображение";
			this.miCopyThumbnailUrlToolStripMenuItem.Click += new System.EventHandler(this.miCopyThumbnailUrlToolStripMenuItem_Click);
			// 
			// miSaveThumbnailImageAssToolStripMenuItem
			// 
			this.miSaveThumbnailImageAssToolStripMenuItem.Name = "miSaveThumbnailImageAssToolStripMenuItem";
			this.miSaveThumbnailImageAssToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
			this.miSaveThumbnailImageAssToolStripMenuItem.Text = "Сохранить изображение...";
			this.miSaveThumbnailImageAssToolStripMenuItem.Click += new System.EventHandler(this.miSaveThumbnailImageAssToolStripMenuItem_Click);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(298, 6);
			// 
			// miReloadActiveThumbnailToolStripMenuItem
			// 
			this.miReloadActiveThumbnailToolStripMenuItem.Name = "miReloadActiveThumbnailToolStripMenuItem";
			this.miReloadActiveThumbnailToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
			this.miReloadActiveThumbnailToolStripMenuItem.Text = "Перезагрузить эскиз";
			this.miReloadActiveThumbnailToolStripMenuItem.Click += new System.EventHandler(this.miReloadActiveThumbnailToolStripMenuItem_Click);
			// 
			// miThumbnailsToolStripMenuItem
			// 
			this.miThumbnailsToolStripMenuItem.Name = "miThumbnailsToolStripMenuItem";
			this.miThumbnailsToolStripMenuItem.Size = new System.Drawing.Size(301, 22);
			this.miThumbnailsToolStripMenuItem.Text = "Эскизы";
			// 
			// contextMenuChannelTitle
			// 
			this.contextMenuChannelTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.miCopyChannelTitleToolStripMenuItem1,
			this.miCopyChannelIdToolStripMenuItem,
			this.miCopyChannelNameWithIdToolStripMenuItem,
			this.miCopyChannelUrlToolStripMenuItem,
			this.miOpenChannelInBrowserToolStripMenuItem});
			this.contextMenuChannelTitle.Name = "contextMenuChannelTitle";
			this.contextMenuChannelTitle.Size = new System.Drawing.Size(265, 114);
			// 
			// miCopyChannelTitleToolStripMenuItem1
			// 
			this.miCopyChannelTitleToolStripMenuItem1.Name = "miCopyChannelTitleToolStripMenuItem1";
			this.miCopyChannelTitleToolStripMenuItem1.Size = new System.Drawing.Size(264, 22);
			this.miCopyChannelTitleToolStripMenuItem1.Text = "Скопировать название канала";
			this.miCopyChannelTitleToolStripMenuItem1.Click += new System.EventHandler(this.miCopyChannelTitleToolStripMenuItem_Click);
			// 
			// miCopyChannelIdToolStripMenuItem
			// 
			this.miCopyChannelIdToolStripMenuItem.Name = "miCopyChannelIdToolStripMenuItem";
			this.miCopyChannelIdToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.miCopyChannelIdToolStripMenuItem.Text = "Скопировать ID канала";
			this.miCopyChannelIdToolStripMenuItem.Click += new System.EventHandler(this.miCopyChannelIdToolStripMenuItem_Click);
			// 
			// miCopyChannelNameWithIdToolStripMenuItem
			// 
			this.miCopyChannelNameWithIdToolStripMenuItem.Name = "miCopyChannelNameWithIdToolStripMenuItem";
			this.miCopyChannelNameWithIdToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.miCopyChannelNameWithIdToolStripMenuItem.Text = "Скопировать название и ID канала";
			this.miCopyChannelNameWithIdToolStripMenuItem.Click += new System.EventHandler(this.miCopyChannelNameWithIdToolStripMenuItem_Click);
			// 
			// miCopyChannelUrlToolStripMenuItem
			// 
			this.miCopyChannelUrlToolStripMenuItem.Name = "miCopyChannelUrlToolStripMenuItem";
			this.miCopyChannelUrlToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.miCopyChannelUrlToolStripMenuItem.Text = "Скопировать ссылку на канал";
			this.miCopyChannelUrlToolStripMenuItem.Click += new System.EventHandler(this.miCopyChannelUrlToolStripMenuItem_Click);
			// 
			// miOpenChannelInBrowserToolStripMenuItem
			// 
			this.miOpenChannelInBrowserToolStripMenuItem.Name = "miOpenChannelInBrowserToolStripMenuItem";
			this.miOpenChannelInBrowserToolStripMenuItem.Size = new System.Drawing.Size(264, 22);
			this.miOpenChannelInBrowserToolStripMenuItem.Text = "Открыть канал в браузере";
			this.miOpenChannelInBrowserToolStripMenuItem.Click += new System.EventHandler(this.miOpenChannelInBrowserToolStripMenuItem_Click);
			// 
			// contextMenuVideoTitle
			// 
			this.contextMenuVideoTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.miCopyTitleAsIsToolStripMenuItem,
			this.miCopyFixedTitleToolStripMenuItem,
			this.toolStripMenuItem1,
			this.miCopyFormattedFileNameToolStripMenuItem});
			this.contextMenuVideoTitle.Name = "contextMenuVideoTitle";
			this.contextMenuVideoTitle.Size = new System.Drawing.Size(317, 76);
			// 
			// miCopyTitleAsIsToolStripMenuItem
			// 
			this.miCopyTitleAsIsToolStripMenuItem.Name = "miCopyTitleAsIsToolStripMenuItem";
			this.miCopyTitleAsIsToolStripMenuItem.Size = new System.Drawing.Size(316, 22);
			this.miCopyTitleAsIsToolStripMenuItem.Text = "Скопировать название как есть";
			this.miCopyTitleAsIsToolStripMenuItem.Click += new System.EventHandler(this.miCopyTitleAsIsToolStripMenuItem_Click);
			// 
			// miCopyFixedTitleToolStripMenuItem
			// 
			this.miCopyFixedTitleToolStripMenuItem.Name = "miCopyFixedTitleToolStripMenuItem";
			this.miCopyFixedTitleToolStripMenuItem.Size = new System.Drawing.Size(316, 22);
			this.miCopyFixedTitleToolStripMenuItem.Text = "Скопировать название с заменой символов";
			this.miCopyFixedTitleToolStripMenuItem.Click += new System.EventHandler(this.miCopyFixedTitleToolStripMenuItem_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(313, 6);
			// 
			// miCopyFormattedFileNameToolStripMenuItem
			// 
			this.miCopyFormattedFileNameToolStripMenuItem.Name = "miCopyFormattedFileNameToolStripMenuItem";
			this.miCopyFormattedFileNameToolStripMenuItem.Size = new System.Drawing.Size(316, 22);
			this.miCopyFormattedFileNameToolStripMenuItem.Text = "Скопировать имя файла на выходе";
			this.miCopyFormattedFileNameToolStripMenuItem.Click += new System.EventHandler(this.miCopyFormattedFileNameToolStripMenuItem_Click);
			// 
			// contextMenuDate
			// 
			this.contextMenuDate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.miCopyVideoPublishedDateToolStripMenuItem,
			this.miUpdateVideoPublishedDateToolStripMenuItem});
			this.contextMenuDate.Name = "contextMenuDate";
			this.contextMenuDate.Size = new System.Drawing.Size(243, 48);
			// 
			// miCopyVideoPublishedDateToolStripMenuItem
			// 
			this.miCopyVideoPublishedDateToolStripMenuItem.Name = "miCopyVideoPublishedDateToolStripMenuItem";
			this.miCopyVideoPublishedDateToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
			this.miCopyVideoPublishedDateToolStripMenuItem.Text = "Скопировать дату публикации";
			this.miCopyVideoPublishedDateToolStripMenuItem.Click += new System.EventHandler(this.miCopyVideoPublishedDateToolStripMenuItem_Click);
			// 
			// miUpdateVideoPublishedDateToolStripMenuItem
			// 
			this.miUpdateVideoPublishedDateToolStripMenuItem.Name = "miUpdateVideoPublishedDateToolStripMenuItem";
			this.miUpdateVideoPublishedDateToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
			this.miUpdateVideoPublishedDateToolStripMenuItem.Text = "Обновить дату публикации";
			this.miUpdateVideoPublishedDateToolStripMenuItem.Click += new System.EventHandler(this.miUpdateVideoPublishedDateToolStripMenuItem_Click);
			// 
			// contextMenuProgressBar
			// 
			this.contextMenuProgressBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.miSingleToolStripMenuItem,
			this.miMultipleToolStripMenuItem});
			this.contextMenuProgressBar.Name = "contextMenuProgressBar";
			this.contextMenuProgressBar.Size = new System.Drawing.Size(218, 48);
			// 
			// miSingleToolStripMenuItem
			// 
			this.miSingleToolStripMenuItem.Name = "miSingleToolStripMenuItem";
			this.miSingleToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.miSingleToolStripMenuItem.Text = "Общий прогресс";
			this.miSingleToolStripMenuItem.Click += new System.EventHandler(this.miSingleToolStripMenuItem_Click);
			// 
			// miMultipleToolStripMenuItem
			// 
			this.miMultipleToolStripMenuItem.Checked = true;
			this.miMultipleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.miMultipleToolStripMenuItem.Name = "miMultipleToolStripMenuItem";
			this.miMultipleToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
			this.miMultipleToolStripMenuItem.Text = "Прогресс каждого потока";
			this.miMultipleToolStripMenuItem.Click += new System.EventHandler(this.miMultipleToolStripMenuItem_Click);
			// 
			// contextMenuFrameActions
			// 
			this.contextMenuFrameActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.miActionsToolStripMenuItem,
			this.miOptimizeFormatListReceivingToolStripMenuItem,
			this.miUpdateFormatListToolStripMenuItem});
			this.contextMenuFrameActions.Name = "contextMenuFrameActions";
			this.contextMenuFrameActions.Size = new System.Drawing.Size(331, 70);
			// 
			// miActionsToolStripMenuItem
			// 
			this.miActionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.miGetVideoWebPageCodeToolStripMenuItem,
			this.miGetVideoInfoToolStripMenuItem,
			this.miGetDownloadUrlsToolStripMenuItem,
			this.miGetDashManifestToolStripMenuItem,
			this.miGetHlsManifestToolStripMenuItem,
			this.miGetPlayerCodeToolStripMenuItem});
			this.miActionsToolStripMenuItem.Name = "miActionsToolStripMenuItem";
			this.miActionsToolStripMenuItem.Size = new System.Drawing.Size(330, 22);
			this.miActionsToolStripMenuItem.Text = "Действия";
			// 
			// miGetVideoWebPageCodeToolStripMenuItem
			// 
			this.miGetVideoWebPageCodeToolStripMenuItem.Name = "miGetVideoWebPageCodeToolStripMenuItem";
			this.miGetVideoWebPageCodeToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.miGetVideoWebPageCodeToolStripMenuItem.Text = "Получить код веб-страницы";
			this.miGetVideoWebPageCodeToolStripMenuItem.Click += new System.EventHandler(this.miGetVideoWebPageCodeToolStripMenuItem_Click);
			// 
			// miGetVideoInfoToolStripMenuItem
			// 
			this.miGetVideoInfoToolStripMenuItem.Name = "miGetVideoInfoToolStripMenuItem";
			this.miGetVideoInfoToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.miGetVideoInfoToolStripMenuItem.Text = "Получить информацию о видео";
			this.miGetVideoInfoToolStripMenuItem.Click += new System.EventHandler(this.miGetVideoInfoToolStripMenuItem_Click);
			// 
			// miGetDownloadUrlsToolStripMenuItem
			// 
			this.miGetDownloadUrlsToolStripMenuItem.Name = "miGetDownloadUrlsToolStripMenuItem";
			this.miGetDownloadUrlsToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.miGetDownloadUrlsToolStripMenuItem.Text = "Получить ссылки для скачивания";
			this.miGetDownloadUrlsToolStripMenuItem.Click += new System.EventHandler(this.miGetDownloadUrlsToolStripMenuItem_Click);
			// 
			// miGetDashManifestToolStripMenuItem
			// 
			this.miGetDashManifestToolStripMenuItem.Name = "miGetDashManifestToolStripMenuItem";
			this.miGetDashManifestToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.miGetDashManifestToolStripMenuItem.Text = "Получить манифест DASH";
			this.miGetDashManifestToolStripMenuItem.Click += new System.EventHandler(this.miGetDashManifestToolStripMenuItem_Click);
			// 
			// miGetHlsManifestToolStripMenuItem
			// 
			this.miGetHlsManifestToolStripMenuItem.Name = "miGetHlsManifestToolStripMenuItem";
			this.miGetHlsManifestToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.miGetHlsManifestToolStripMenuItem.Text = "Получить манифест HLS";
			this.miGetHlsManifestToolStripMenuItem.Click += new System.EventHandler(this.miGetHlsManifestToolStripMenuItem_Click);
			// 
			// miGetPlayerCodeToolStripMenuItem
			// 
			this.miGetPlayerCodeToolStripMenuItem.Name = "miGetPlayerCodeToolStripMenuItem";
			this.miGetPlayerCodeToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
			this.miGetPlayerCodeToolStripMenuItem.Text = "Получить код плеера";
			this.miGetPlayerCodeToolStripMenuItem.Click += new System.EventHandler(this.miGetPlayerCodeToolStripMenuItem_Click);
			// 
			// miOptimizeFormatListReceivingToolStripMenuItem
			// 
			this.miOptimizeFormatListReceivingToolStripMenuItem.Checked = true;
			this.miOptimizeFormatListReceivingToolStripMenuItem.CheckOnClick = true;
			this.miOptimizeFormatListReceivingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
			this.miOptimizeFormatListReceivingToolStripMenuItem.Name = "miOptimizeFormatListReceivingToolStripMenuItem";
			this.miOptimizeFormatListReceivingToolStripMenuItem.Size = new System.Drawing.Size(330, 22);
			this.miOptimizeFormatListReceivingToolStripMenuItem.Text = "Оптимизировать получение списка форматов";
			// 
			// miUpdateFormatListToolStripMenuItem
			// 
			this.miUpdateFormatListToolStripMenuItem.Name = "miUpdateFormatListToolStripMenuItem";
			this.miUpdateFormatListToolStripMenuItem.Size = new System.Drawing.Size(330, 22);
			this.miUpdateFormatListToolStripMenuItem.Text = "Обновить список форматов";
			this.miUpdateFormatListToolStripMenuItem.Click += new System.EventHandler(this.miUpdateFormatListToolStripMenuItem_Click);
			// 
			// lblBtnOpenFrameContextMenu
			// 
			this.lblBtnOpenFrameContextMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblBtnOpenFrameContextMenu.BackColor = System.Drawing.SystemColors.ControlLight;
			this.lblBtnOpenFrameContextMenu.Location = new System.Drawing.Point(462, 42);
			this.lblBtnOpenFrameContextMenu.Name = "lblBtnOpenFrameContextMenu";
			this.lblBtnOpenFrameContextMenu.Size = new System.Drawing.Size(25, 25);
			this.lblBtnOpenFrameContextMenu.TabIndex = 19;
			this.lblBtnOpenFrameContextMenu.Text = "...";
			this.lblBtnOpenFrameContextMenu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip1.SetToolTip(this.lblBtnOpenFrameContextMenu, "Меню");
			this.lblBtnOpenFrameContextMenu.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblBtnOpenFrameContextMenu_MouseDown);
			this.lblBtnOpenFrameContextMenu.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblBtnOpenFrameContextMenu_MouseUp);
			// 
			// progressBarDownload
			// 
			this.progressBarDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBarDownload.Location = new System.Drawing.Point(3, 142);
			this.progressBarDownload.Name = "progressBarDownload";
			this.progressBarDownload.Size = new System.Drawing.Size(416, 23);
			this.progressBarDownload.TabIndex = 18;
			this.progressBarDownload.Text = "multipleProgressBar1";
			this.progressBarDownload.MouseDown += new System.Windows.Forms.MouseEventHandler(this.progressBarDownload_MouseDown);
			this.progressBarDownload.MouseUp += new System.Windows.Forms.MouseEventHandler(this.progressBarDownload_MouseUp);
			// 
			// FrameYouTubeVideo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.Controls.Add(this.lblBtnOpenFrameContextMenu);
			this.Controls.Add(this.progressBarDownload);
			this.Controls.Add(this.pictureBoxFavoriteChannel);
			this.Controls.Add(this.lblDowndloadProgress);
			this.Controls.Add(this.lblStatus);
			this.Controls.Add(this.btnDownload);
			this.Controls.Add(this.pictureBoxFavoriteVideo);
			this.Controls.Add(this.lblDatePublished);
			this.Controls.Add(this.lblChannelTitle);
			this.Controls.Add(this.pictureBoxVideoThumbnail);
			this.Controls.Add(this.lblVideoTitle);
			this.Name = "FrameYouTubeVideo";
			this.Size = new System.Drawing.Size(496, 170);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrameYouTubeVideo_Paint);
			this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrameYouTubeVideo_MouseDown);
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxVideoThumbnail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxFavoriteVideo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxFavoriteChannel)).EndInit();
			this.contextMenuThumnailImage.ResumeLayout(false);
			this.contextMenuChannelTitle.ResumeLayout(false);
			this.contextMenuVideoTitle.ResumeLayout(false);
			this.contextMenuDate.ResumeLayout(false);
			this.contextMenuProgressBar.ResumeLayout(false);
			this.contextMenuFrameActions.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblVideoTitle;
		private System.Windows.Forms.PictureBox pictureBoxVideoThumbnail;
		private System.Windows.Forms.Label lblChannelTitle;
		private System.Windows.Forms.Label lblDatePublished;
		private System.Windows.Forms.PictureBox pictureBoxFavoriteVideo;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblDowndloadProgress;
		private System.Windows.Forms.PictureBox pictureBoxFavoriteChannel;
		private System.Windows.Forms.ContextMenuStrip contextMenuThumnailImage;
		private System.Windows.Forms.ToolStripMenuItem miCopyVideoUrlToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuChannelTitle;
		private System.Windows.Forms.ToolStripMenuItem miCopyChannelTitleToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem miOpenChannelInBrowserToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miOpenVideoInBrowserToolStripMenuItem;
		public System.Windows.Forms.Button btnDownload;
		private System.Windows.Forms.ToolStripMenuItem miSaveThumbnailImageAssToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyChannelNameWithIdToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuVideoTitle;
		private System.Windows.Forms.ToolStripMenuItem miCopyTitleAsIsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyFixedTitleToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem miOpenThumnailImageInBrowserToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyThumbnailUrlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyChannelUrlToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyChannelIdToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuDate;
		private System.Windows.Forms.ToolStripMenuItem miUpdateVideoPublishedDateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyVideoPublishedDateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyPlayerUrlToolStripMenuItem;
		private MultipleProgressBar progressBarDownload;
		private System.Windows.Forms.ContextMenuStrip contextMenuProgressBar;
		private System.Windows.Forms.ToolStripMenuItem miSingleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miMultipleToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem miCopyFormattedFileNameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miCopyVideoIdToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuFrameActions;
		private System.Windows.Forms.ToolStripMenuItem miGetVideoWebPageCodeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miGetVideoInfoToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miGetDownloadUrlsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miGetDashManifestToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miGetHlsManifestToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miGetPlayerCodeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miActionsToolStripMenuItem;
		private System.Windows.Forms.Label lblBtnOpenFrameContextMenu;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolStripMenuItem miOptimizeFormatListReceivingToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem miThumbnailsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miReloadActiveThumbnailToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miUpdateFormatListToolStripMenuItem;
	}
}
