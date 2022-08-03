namespace YouTube_downloader
{
    partial class FormTracksSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTracksSelector));
            this.listViewTracksSelector = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnTrackType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnResolution = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnFrameRate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnFormalBitrate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnAverageBitrate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnFileExt = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnFileSize = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnChunkCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.columnButtonRenderer1 = new BrightIdeasSoftware.ColumnButtonRenderer();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.listViewTracksSelector)).BeginInit();
            this.SuspendLayout();
            // 
            // listViewTracksSelector
            // 
            this.listViewTracksSelector.AllColumns.Add(this.olvColumnTrackType);
            this.listViewTracksSelector.AllColumns.Add(this.olvColumnResolution);
            this.listViewTracksSelector.AllColumns.Add(this.olvColumnFrameRate);
            this.listViewTracksSelector.AllColumns.Add(this.olvColumnFormalBitrate);
            this.listViewTracksSelector.AllColumns.Add(this.olvColumnAverageBitrate);
            this.listViewTracksSelector.AllColumns.Add(this.olvColumnFileExt);
            this.listViewTracksSelector.AllColumns.Add(this.olvColumnFileSize);
            this.listViewTracksSelector.AllColumns.Add(this.olvColumnChunkCount);
            this.listViewTracksSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewTracksSelector.CellEditUseWholeCell = false;
            this.listViewTracksSelector.CheckBoxes = true;
            this.listViewTracksSelector.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnTrackType,
            this.olvColumnResolution,
            this.olvColumnFrameRate,
            this.olvColumnFormalBitrate,
            this.olvColumnAverageBitrate,
            this.olvColumnFileExt,
            this.olvColumnFileSize,
            this.olvColumnChunkCount});
            this.listViewTracksSelector.Cursor = System.Windows.Forms.Cursors.Default;
            this.listViewTracksSelector.FullRowSelect = true;
            this.listViewTracksSelector.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewTracksSelector.HideSelection = false;
            this.listViewTracksSelector.LabelWrap = false;
            this.listViewTracksSelector.Location = new System.Drawing.Point(12, 12);
            this.listViewTracksSelector.MultiSelect = false;
            this.listViewTracksSelector.Name = "listViewTracksSelector";
            this.listViewTracksSelector.ShowGroups = false;
            this.listViewTracksSelector.Size = new System.Drawing.Size(794, 341);
            this.listViewTracksSelector.TabIndex = 0;
            this.listViewTracksSelector.UseCompatibleStateImageBehavior = false;
            this.listViewTracksSelector.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnTrackType
            // 
            this.olvColumnTrackType.AspectName = "TrackType";
            this.olvColumnTrackType.Text = "Тип";
            this.olvColumnTrackType.Width = 65;
            // 
            // olvColumnResolution
            // 
            this.olvColumnResolution.AspectName = "Resolution";
            this.olvColumnResolution.Text = "Разрешение";
            this.olvColumnResolution.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnResolution.Width = 96;
            // 
            // olvColumnFrameRate
            // 
            this.olvColumnFrameRate.AspectName = "FrameRate";
            this.olvColumnFrameRate.Text = "Частота кадров";
            this.olvColumnFrameRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnFrameRate.Width = 96;
            // 
            // olvColumnFormalBitrate
            // 
            this.olvColumnFormalBitrate.AspectName = "FormalBitrate";
            this.olvColumnFormalBitrate.Text = "Битрейт (формальный)";
            this.olvColumnFormalBitrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnFormalBitrate.Width = 128;
            // 
            // olvColumnAverageBitrate
            // 
            this.olvColumnAverageBitrate.AspectName = "AverageBitrate";
            this.olvColumnAverageBitrate.Text = "Битрейт (средний)";
            this.olvColumnAverageBitrate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnAverageBitrate.Width = 109;
            // 
            // olvColumnFileExt
            // 
            this.olvColumnFileExt.AspectName = "FileExtension";
            this.olvColumnFileExt.Text = "Тип файла";
            this.olvColumnFileExt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnFileExt.Width = 71;
            // 
            // olvColumnFileSize
            // 
            this.olvColumnFileSize.AspectName = "FileSize";
            this.olvColumnFileSize.Text = "Размер файла";
            this.olvColumnFileSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.olvColumnFileSize.Width = 90;
            // 
            // olvColumnChunkCount
            // 
            this.olvColumnChunkCount.AspectName = "ChunkCount";
            this.olvColumnChunkCount.Text = "Количество чанков";
            this.olvColumnChunkCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumnChunkCount.Width = 118;
            // 
            // columnButtonRenderer1
            // 
            this.columnButtonRenderer1.ButtonPadding = new System.Drawing.Size(10, 10);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(12, 359);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownload.Location = new System.Drawing.Point(731, 359);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "Скачать";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // FormTracksSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(818, 385);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.listViewTracksSelector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(834, 400);
            this.Name = "FormTracksSelector";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор форматов";
            ((System.ComponentModel.ISupportInitialize)(this.listViewTracksSelector)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView listViewTracksSelector;
        private BrightIdeasSoftware.OLVColumn olvColumnResolution;
        private BrightIdeasSoftware.OLVColumn olvColumnFrameRate;
        private BrightIdeasSoftware.OLVColumn olvColumnFormalBitrate;
        private BrightIdeasSoftware.OLVColumn olvColumnFileExt;
        private BrightIdeasSoftware.OLVColumn olvColumnFileSize;
        private BrightIdeasSoftware.OLVColumn olvColumnTrackType;
        private BrightIdeasSoftware.ColumnButtonRenderer columnButtonRenderer1;
        private BrightIdeasSoftware.OLVColumn olvColumnChunkCount;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDownload;
        private BrightIdeasSoftware.OLVColumn olvColumnAverageBitrate;
    }
}