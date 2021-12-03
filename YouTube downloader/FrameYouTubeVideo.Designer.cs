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

        //#region Код, автоматически созданный конструктором компонентов

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
            this.menuDownloads = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.progressBarDownload = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.imgScrollbar = new System.Windows.Forms.PictureBox();
            this.btnGetVideoInfo = new System.Windows.Forms.Button();
            this.imageFavoriteChannel = new System.Windows.Forms.PictureBox();
            this.menuImage = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyVideoTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyChannelTitleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyVideoUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveImageAssToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.openVideoInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChannelTitle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyChannelTitleToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyChannelNameWithIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openChannelInBrowserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGetWebPage = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageFavorite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgScrollbar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageFavoriteChannel)).BeginInit();
            this.menuImage.SuspendLayout();
            this.menuChannelTitle.SuspendLayout();
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
            this.imagePreview.Location = new System.Drawing.Point(3, 12);
            this.imagePreview.Name = "imagePreview";
            this.imagePreview.Size = new System.Drawing.Size(170, 111);
            this.imagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
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
            this.lblChannelTitle.Size = new System.Drawing.Size(97, 16);
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
            this.lblDatePublished.Size = new System.Drawing.Size(111, 16);
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
            // menuDownloads
            // 
            this.menuDownloads.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.menuDownloads.Name = "menuDownloads";
            this.menuDownloads.Size = new System.Drawing.Size(61, 4);
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
            this.imgScrollbar.Paint += new System.Windows.Forms.PaintEventHandler(this.imgScrollbar_Paint);
            this.imgScrollbar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imgScrollbar_MouseDown);
            // 
            // btnGetVideoInfo
            // 
            this.btnGetVideoInfo.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGetVideoInfo.Location = new System.Drawing.Point(508, 12);
            this.btnGetVideoInfo.Name = "btnGetVideoInfo";
            this.btnGetVideoInfo.Size = new System.Drawing.Size(87, 24);
            this.btnGetVideoInfo.TabIndex = 10;
            this.btnGetVideoInfo.Text = "get video info";
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
            // menuImage
            // 
            this.menuImage.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyVideoTitleToolStripMenuItem,
            this.copyChannelTitleToolStripMenuItem,
            this.copyVideoUrlToolStripMenuItem,
            this.saveImageAssToolStripMenuItem,
            this.toolStripMenuItem1,
            this.openVideoInBrowserToolStripMenuItem});
            this.menuImage.Name = "menuImage";
            this.menuImage.Size = new System.Drawing.Size(241, 120);
            // 
            // copyVideoTitleToolStripMenuItem
            // 
            this.copyVideoTitleToolStripMenuItem.Name = "copyVideoTitleToolStripMenuItem";
            this.copyVideoTitleToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.copyVideoTitleToolStripMenuItem.Text = "Скопировать название видео";
            this.copyVideoTitleToolStripMenuItem.Click += new System.EventHandler(this.copyVideoTitleToolStripMenuItem_Click);
            // 
            // copyChannelTitleToolStripMenuItem
            // 
            this.copyChannelTitleToolStripMenuItem.Name = "copyChannelTitleToolStripMenuItem";
            this.copyChannelTitleToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.copyChannelTitleToolStripMenuItem.Text = "Скопировать название канала";
            this.copyChannelTitleToolStripMenuItem.Click += new System.EventHandler(this.copyChannelTitleToolStripMenuItem_Click);
            // 
            // copyVideoUrlToolStripMenuItem
            // 
            this.copyVideoUrlToolStripMenuItem.Name = "copyVideoUrlToolStripMenuItem";
            this.copyVideoUrlToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.copyVideoUrlToolStripMenuItem.Text = "Скопировать ссылку на видео";
            this.copyVideoUrlToolStripMenuItem.Click += new System.EventHandler(this.copyVideoUrlToolStripMenuItem_Click);
            // 
            // saveImageAssToolStripMenuItem
            // 
            this.saveImageAssToolStripMenuItem.Name = "saveImageAssToolStripMenuItem";
            this.saveImageAssToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.saveImageAssToolStripMenuItem.Text = "Сохранить изображение...";
            this.saveImageAssToolStripMenuItem.Click += new System.EventHandler(this.saveImageAssToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(237, 6);
            // 
            // openVideoInBrowserToolStripMenuItem
            // 
            this.openVideoInBrowserToolStripMenuItem.Name = "openVideoInBrowserToolStripMenuItem";
            this.openVideoInBrowserToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.openVideoInBrowserToolStripMenuItem.Text = "Открыть видео в браузере";
            this.openVideoInBrowserToolStripMenuItem.Click += new System.EventHandler(this.openVideoInBrowserToolStripMenuItem_Click);
            // 
            // menuChannelTitle
            // 
            this.menuChannelTitle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyChannelTitleToolStripMenuItem1,
            this.copyChannelNameWithIdToolStripMenuItem,
            this.openChannelInBrowserToolStripMenuItem});
            this.menuChannelTitle.Name = "menuChannelTitle";
            this.menuChannelTitle.Size = new System.Drawing.Size(262, 70);
            // 
            // copyChannelTitleToolStripMenuItem1
            // 
            this.copyChannelTitleToolStripMenuItem1.Name = "copyChannelTitleToolStripMenuItem1";
            this.copyChannelTitleToolStripMenuItem1.Size = new System.Drawing.Size(261, 22);
            this.copyChannelTitleToolStripMenuItem1.Text = "Скопировать название канала";
            this.copyChannelTitleToolStripMenuItem1.Click += new System.EventHandler(this.copyChannelTitleToolStripMenuItem1_Click);
            // 
            // copyChannelNameWithIdToolStripMenuItem
            // 
            this.copyChannelNameWithIdToolStripMenuItem.Name = "copyChannelNameWithIdToolStripMenuItem";
            this.copyChannelNameWithIdToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.copyChannelNameWithIdToolStripMenuItem.Text = "Скопировать назвние и IID канала";
            this.copyChannelNameWithIdToolStripMenuItem.Click += new System.EventHandler(this.copyChannelNameWithIdToolStripMenuItem_Click);
            // 
            // openChannelInBrowserToolStripMenuItem
            // 
            this.openChannelInBrowserToolStripMenuItem.Name = "openChannelInBrowserToolStripMenuItem";
            this.openChannelInBrowserToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.openChannelInBrowserToolStripMenuItem.Text = "Открыть канал в браузере";
            this.openChannelInBrowserToolStripMenuItem.Click += new System.EventHandler(this.openChannelInBrowserToolStripMenuItem_Click);
            // 
            // btnGetWebPage
            // 
            this.btnGetWebPage.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnGetWebPage.Location = new System.Drawing.Point(508, 60);
            this.btnGetWebPage.Name = "btnGetWebPage";
            this.btnGetWebPage.Size = new System.Drawing.Size(87, 31);
            this.btnGetWebPage.TabIndex = 12;
            this.btnGetWebPage.Text = "Get web page";
            this.btnGetWebPage.UseVisualStyleBackColor = false;
            this.btnGetWebPage.Click += new System.EventHandler(this.btnGetWebPage_Click);
            // 
            // FrameYouTubeVideo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
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
            this.menuImage.ResumeLayout(false);
            this.menuChannelTitle.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        //#endregion

        private System.Windows.Forms.Label lblVideoTitle;
        private System.Windows.Forms.PictureBox imagePreview;
        private System.Windows.Forms.Label lblChannelTitle;
        private System.Windows.Forms.Label lblDatePublished;
        private System.Windows.Forms.PictureBox imageFavorite;
        private System.Windows.Forms.ContextMenuStrip menuDownloads;
        private System.Windows.Forms.ProgressBar progressBarDownload;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.PictureBox imgScrollbar;
        private System.Windows.Forms.Button btnGetVideoInfo;
        private System.Windows.Forms.PictureBox imageFavoriteChannel;
        private System.Windows.Forms.ContextMenuStrip menuImage;
        private System.Windows.Forms.ToolStripMenuItem copyVideoTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyChannelTitleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyVideoUrlToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip menuChannelTitle;
        private System.Windows.Forms.ToolStripMenuItem copyChannelTitleToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openChannelInBrowserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openVideoInBrowserToolStripMenuItem;
        public System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnGetWebPage;
        private System.Windows.Forms.ToolStripMenuItem saveImageAssToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copyChannelNameWithIdToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
