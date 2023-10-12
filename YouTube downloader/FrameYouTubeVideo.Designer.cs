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
            this.imagePreview = new System.Windows.Forms.PictureBox();
            this.lblChannelTitle = new System.Windows.Forms.Label();
            this.lblDatePublished = new System.Windows.Forms.Label();
            this.imageFavorite = new System.Windows.Forms.PictureBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.contextMenuDownloads = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.imgScrollbar = new System.Windows.Forms.PictureBox();
            this.btnGetVideoInfo = new System.Windows.Forms.Button();
            this.imageFavoriteChannel = new System.Windows.Forms.PictureBox();
            this.contextMenuImage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miOpenVideoInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyVideoUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miOpenImageInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyImageUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miSaveImageAssToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuChannelTitle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCopyChannelTitleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyChannelIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyChannelNameWithIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyChannelUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miOpenChannelInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGetWebPage = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnGetDashManifest = new System.Windows.Forms.Button();
            this.btnGetHlsManifest = new System.Windows.Forms.Button();
            this.btnGetPlayerCode = new System.Windows.Forms.Button();
            this.contextMenuVideoTitle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miCopyTitleAsIsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyFixedTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuDate = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miUpdateVideoPublishedDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.miCopyVideoPublishedDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageFavorite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgScrollbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageFavoriteChannel)).BeginInit();
            this.contextMenuImage.SuspendLayout();
            this.contextMenuChannelTitle.SuspendLayout();
            this.contextMenuVideoTitle.SuspendLayout();
            this.contextMenuDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblVideoTitle
            // 
            this.lblVideoTitle.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblVideoTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVideoTitle.Location = new System.Drawing.Point(179, 12);
            this.lblVideoTitle.Name = "lblVideoTitle";
            this.lblVideoTitle.Size = new System.Drawing.Size(277, 68);
            this.lblVideoTitle.TabIndex = 0;
            this.lblVideoTitle.Text = "lblVideoTitle";
            this.lblVideoTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblVideoTitle_MouseDown);
            // 
            // imagePreview
            // 
            this.imagePreview.BackColor = System.Drawing.SystemColors.ControlLight;
            this.imagePreview.Location = new System.Drawing.Point(3, 12);
            this.imagePreview.Name = "imagePreview";
            this.imagePreview.Size = new System.Drawing.Size(170, 111);
            this.imagePreview.TabIndex = 1;
            this.imagePreview.TabStop = false;
            this.imagePreview.Paint += new System.Windows.Forms.PaintEventHandler(this.imagePreview_Paint);
            this.imagePreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imagePreview_MouseDown);
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
            // imageFavorite
            // 
            this.imageFavorite.Location = new System.Drawing.Point(462, 12);
            this.imageFavorite.Name = "imageFavorite";
            this.imageFavorite.Size = new System.Drawing.Size(25, 25);
            this.imageFavorite.TabIndex = 4;
            this.imageFavorite.TabStop = false;
            this.toolTip1.SetToolTip(this.imageFavorite, "Избранное видео");
            this.imageFavorite.Paint += new System.Windows.Forms.PaintEventHandler(this.imageFavorite_Paint);
            this.imageFavorite.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageFavorite_MouseDown);
            // 
            // btnDownload
            // 
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
            // contextMenuDownloads
            // 
            this.contextMenuDownloads.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.contextMenuDownloads.Name = "contextMenuDownloads";
            this.contextMenuDownloads.Size = new System.Drawing.Size(61, 4);
            // 
            // progressBarDownload
            // 
            this.progressBarDownload.Location = new System.Drawing.Point(3, 142);
            this.progressBarDownload.Name = "progressBarDownload";
            this.progressBarDownload.Size = new System.Drawing.Size(416, 23);
            this.progressBarDownload.TabIndex = 6;
            this.progressBarDownload.MouseDown += new System.Windows.Forms.MouseEventHandler(this.progressBarDownload_MouseDown);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(3, 126);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(47, 13);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "lblStatus";
            this.lblStatus.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgScrollbar_MouseDown);
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(58, 126);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(58, 13);
            this.lblProgress.TabIndex = 8;
            this.lblProgress.Text = "lblProgress";
            this.lblProgress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgScrollbar_MouseDown);
            // 
            // imgScrollbar
            // 
            this.imgScrollbar.Location = new System.Drawing.Point(3, 166);
            this.imgScrollbar.Name = "imgScrollbar";
            this.imgScrollbar.Size = new System.Drawing.Size(484, 12);
            this.imgScrollbar.TabIndex = 9;
            this.imgScrollbar.TabStop = false;
            this.toolTip1.SetToolTip(this.imgScrollbar, "Скроллбар");
            this.imgScrollbar.Paint += new System.Windows.Forms.PaintEventHandler(this.imgScrollbar_Paint);
            this.imgScrollbar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgScrollbar_MouseDown);
            this.imgScrollbar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imgScrollbar_MouseMove);
            this.imgScrollbar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imgScrollbar_MouseUp);
            // 
            // btnGetVideoInfo
            // 
            this.btnGetVideoInfo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGetVideoInfo.Location = new System.Drawing.Point(508, 43);
            this.btnGetVideoInfo.Name = "btnGetVideoInfo";
            this.btnGetVideoInfo.Size = new System.Drawing.Size(112, 24);
            this.btnGetVideoInfo.TabIndex = 10;
            this.btnGetVideoInfo.Text = "Get video info";
            this.btnGetVideoInfo.UseVisualStyleBackColor = false;
            this.btnGetVideoInfo.Click += new System.EventHandler(this.btnGetVideoInfo_Click);
            this.btnGetVideoInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnGetVideoInfo_MouseDown);
            // 
            // imageFavoriteChannel
            // 
            this.imageFavoriteChannel.Location = new System.Drawing.Point(182, 81);
            this.imageFavoriteChannel.Name = "imageFavoriteChannel";
            this.imageFavoriteChannel.Size = new System.Drawing.Size(25, 25);
            this.imageFavoriteChannel.TabIndex = 11;
            this.imageFavoriteChannel.TabStop = false;
            this.toolTip1.SetToolTip(this.imageFavoriteChannel, "Избранный канал");
            this.imageFavoriteChannel.Paint += new System.Windows.Forms.PaintEventHandler(this.imgFavoriteChannel_Paint);
            this.imageFavoriteChannel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageFavoriteChannel_MouseDown);
            // 
            // contextMenuImage
            // 
            this.contextMenuImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpenVideoInBrowserToolStripMenuItem,
            this.miCopyVideoUrlToolStripMenuItem,
            this.toolStripMenuItem2,
            this.miOpenImageInBrowserToolStripMenuItem,
            this.miCopyImageUrlToolStripMenuItem,
            this.miSaveImageAssToolStripMenuItem});
            this.contextMenuImage.Name = "contextMenuImage";
            this.contextMenuImage.Size = new System.Drawing.Size(283, 120);
            // 
            // miOpenVideoInBrowserToolStripMenuItem
            // 
            this.miOpenVideoInBrowserToolStripMenuItem.Name = "miOpenVideoInBrowserToolStripMenuItem";
            this.miOpenVideoInBrowserToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.miOpenVideoInBrowserToolStripMenuItem.Text = "Открыть видео в браузере";
            this.miOpenVideoInBrowserToolStripMenuItem.Click += new System.EventHandler(this.miOpenVideoInBrowserToolStripMenuItem_Click);
            // 
            // miCopyVideoUrlToolStripMenuItem
            // 
            this.miCopyVideoUrlToolStripMenuItem.Name = "miCopyVideoUrlToolStripMenuItem";
            this.miCopyVideoUrlToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.miCopyVideoUrlToolStripMenuItem.Text = "Скопировать ссылку на видео";
            this.miCopyVideoUrlToolStripMenuItem.Click += new System.EventHandler(this.miCopyVideoUrlToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(279, 6);
            // 
            // miOpenImageInBrowserToolStripMenuItem
            // 
            this.miOpenImageInBrowserToolStripMenuItem.Name = "miOpenImageInBrowserToolStripMenuItem";
            this.miOpenImageInBrowserToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.miOpenImageInBrowserToolStripMenuItem.Text = "Открыть изображение в браузере";
            this.miOpenImageInBrowserToolStripMenuItem.Click += new System.EventHandler(this.miOpenImageInBrowserToolStripMenuItem_Click);
            // 
            // miCopyImageUrlToolStripMenuItem
            // 
            this.miCopyImageUrlToolStripMenuItem.Name = "miCopyImageUrlToolStripMenuItem";
            this.miCopyImageUrlToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.miCopyImageUrlToolStripMenuItem.Text = "Скопировать ссылку на изображение";
            this.miCopyImageUrlToolStripMenuItem.Click += new System.EventHandler(this.miCopyImageUrlToolStripMenuItem_Click);
            // 
            // miSaveImageAssToolStripMenuItem
            // 
            this.miSaveImageAssToolStripMenuItem.Name = "miSaveImageAssToolStripMenuItem";
            this.miSaveImageAssToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.miSaveImageAssToolStripMenuItem.Text = "Сохранить изображение...";
            this.miSaveImageAssToolStripMenuItem.Click += new System.EventHandler(this.miSaveImageAssToolStripMenuItem_Click);
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
            // btnGetWebPage
            // 
            this.btnGetWebPage.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGetWebPage.Location = new System.Drawing.Point(508, 12);
            this.btnGetWebPage.Name = "btnGetWebPage";
            this.btnGetWebPage.Size = new System.Drawing.Size(112, 25);
            this.btnGetWebPage.TabIndex = 12;
            this.btnGetWebPage.Text = "Get web page";
            this.btnGetWebPage.UseVisualStyleBackColor = false;
            this.btnGetWebPage.Click += new System.EventHandler(this.btnGetWebPage_Click);
            // 
            // btnGetDashManifest
            // 
            this.btnGetDashManifest.Location = new System.Drawing.Point(508, 73);
            this.btnGetDashManifest.Name = "btnGetDashManifest";
            this.btnGetDashManifest.Size = new System.Drawing.Size(112, 23);
            this.btnGetDashManifest.TabIndex = 13;
            this.btnGetDashManifest.Text = "Get DASH manifest";
            this.btnGetDashManifest.UseVisualStyleBackColor = true;
            this.btnGetDashManifest.Click += new System.EventHandler(this.btnGetDashManifest_Click);
            // 
            // btnGetHlsManifest
            // 
            this.btnGetHlsManifest.Location = new System.Drawing.Point(508, 102);
            this.btnGetHlsManifest.Name = "btnGetHlsManifest";
            this.btnGetHlsManifest.Size = new System.Drawing.Size(112, 23);
            this.btnGetHlsManifest.TabIndex = 14;
            this.btnGetHlsManifest.Text = "Get HLS manifest";
            this.btnGetHlsManifest.UseVisualStyleBackColor = true;
            this.btnGetHlsManifest.Click += new System.EventHandler(this.btnGetHlsManifest_Click);
            // 
            // btnGetPlayerCode
            // 
            this.btnGetPlayerCode.Location = new System.Drawing.Point(508, 131);
            this.btnGetPlayerCode.Name = "btnGetPlayerCode";
            this.btnGetPlayerCode.Size = new System.Drawing.Size(112, 23);
            this.btnGetPlayerCode.TabIndex = 15;
            this.btnGetPlayerCode.Text = "Get player code";
            this.btnGetPlayerCode.UseVisualStyleBackColor = true;
            this.btnGetPlayerCode.Click += new System.EventHandler(this.btnGetPlayerCode_Click);
            // 
            // contextMenuVideoTitle
            // 
            this.contextMenuVideoTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miCopyTitleAsIsToolStripMenuItem,
            this.miCopyFixedTitleToolStripMenuItem});
            this.contextMenuVideoTitle.Name = "contextMenuVideoTitle";
            this.contextMenuVideoTitle.Size = new System.Drawing.Size(317, 48);
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
            // contextMenuDate
            // 
            this.contextMenuDate.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miUpdateVideoPublishedDateToolStripMenuItem,
            this.miCopyVideoPublishedDateToolStripMenuItem});
            this.contextMenuDate.Name = "contextMenuDate";
            this.contextMenuDate.Size = new System.Drawing.Size(243, 70);
            // 
            // miUpdateVideoPublishedDateToolStripMenuItem
            // 
            this.miUpdateVideoPublishedDateToolStripMenuItem.Name = "miUpdateVideoPublishedDateToolStripMenuItem";
            this.miUpdateVideoPublishedDateToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.miUpdateVideoPublishedDateToolStripMenuItem.Text = "Обновить дату публикации";
            this.miUpdateVideoPublishedDateToolStripMenuItem.Click += new System.EventHandler(this.miUpdateVideoPublishedDateToolStripMenuItem_Click);
            // 
            // miCopyVideoPublishedDateToolStripMenuItem
            // 
            this.miCopyVideoPublishedDateToolStripMenuItem.Name = "miCopyVideoPublishedDateToolStripMenuItem";
            this.miCopyVideoPublishedDateToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.miCopyVideoPublishedDateToolStripMenuItem.Text = "Скопировать дату публикации";
            this.miCopyVideoPublishedDateToolStripMenuItem.Click += new System.EventHandler(this.miCopyVideoPublishedDateToolStripMenuItem_Click);
            // 
            // FrameYouTubeVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.btnGetPlayerCode);
            this.Controls.Add(this.btnGetHlsManifest);
            this.Controls.Add(this.btnGetDashManifest);
            this.Controls.Add(this.btnGetWebPage);
            this.Controls.Add(this.imageFavoriteChannel);
            this.Controls.Add(this.btnGetVideoInfo);
            this.Controls.Add(this.imgScrollbar);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBarDownload);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.imageFavorite);
            this.Controls.Add(this.lblDatePublished);
            this.Controls.Add(this.lblChannelTitle);
            this.Controls.Add(this.imagePreview);
            this.Controls.Add(this.lblVideoTitle);
            this.Name = "FrameYouTubeVideo";
            this.Size = new System.Drawing.Size(663, 179);
            this.Load += new System.EventHandler(this.FrameYouTubeVideo_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrameYouTubeVideo_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FrameYouTubeVideo_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FrameYouTubeVideo_MouseUp);
            this.Resize += new System.EventHandler(this.FrameYouTubeVideo_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageFavorite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgScrollbar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageFavoriteChannel)).EndInit();
            this.contextMenuImage.ResumeLayout(false);
            this.contextMenuChannelTitle.ResumeLayout(false);
            this.contextMenuVideoTitle.ResumeLayout(false);
            this.contextMenuDate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVideoTitle;
        private System.Windows.Forms.PictureBox imagePreview;
        private System.Windows.Forms.Label lblChannelTitle;
        private System.Windows.Forms.Label lblDatePublished;
        private System.Windows.Forms.PictureBox imageFavorite;
        private System.Windows.Forms.ContextMenuStrip contextMenuDownloads;
        private System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.PictureBox imgScrollbar;
        private System.Windows.Forms.Button btnGetVideoInfo;
        private System.Windows.Forms.PictureBox imageFavoriteChannel;
        private System.Windows.Forms.ContextMenuStrip contextMenuImage;
        private System.Windows.Forms.ToolStripMenuItem miCopyVideoUrlToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuChannelTitle;
        private System.Windows.Forms.ToolStripMenuItem miCopyChannelTitleToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem miOpenChannelInBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miOpenVideoInBrowserToolStripMenuItem;
        public System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnGetWebPage;
        private System.Windows.Forms.ToolStripMenuItem miSaveImageAssToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCopyChannelNameWithIdToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnGetDashManifest;
        private System.Windows.Forms.Button btnGetHlsManifest;
        private System.Windows.Forms.Button btnGetPlayerCode;
        private System.Windows.Forms.ContextMenuStrip contextMenuVideoTitle;
        private System.Windows.Forms.ToolStripMenuItem miCopyTitleAsIsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCopyFixedTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miOpenImageInBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCopyImageUrlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCopyChannelUrlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCopyChannelIdToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuDate;
        private System.Windows.Forms.ToolStripMenuItem miUpdateVideoPublishedDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem miCopyVideoPublishedDateToolStripMenuItem;
    }
}
