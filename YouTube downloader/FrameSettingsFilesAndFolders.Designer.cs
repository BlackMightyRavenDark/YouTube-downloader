
namespace YouTube_downloader
{
	partial class FrameSettingsFilesAndFolders
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label18 = new System.Windows.Forms.Label();
			this.textBoxOutputFileNameFormatWithDate = new System.Windows.Forms.TextBox();
			this.btnResetFileNameFormatWithDate = new System.Windows.Forms.Button();
			this.btnWtfFileMergerDirectory = new System.Windows.Forms.Button();
			this.textBoxFileMergerDirectory = new System.Windows.Forms.TextBox();
			this.btnBrowseFileMergerDirectory = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.btnBrowseFfmpegFilePath = new System.Windows.Forms.Button();
			this.textBoxFfmpegExeFilePath = new System.Windows.Forms.TextBox();
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
			this.SuspendLayout();
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(3, 125);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(217, 13);
			this.label18.TabIndex = 47;
			this.label18.Text = "Формат имени файла (дата определена):";
			// 
			// textBoxOutputFileNameFormatWithDate
			// 
			this.textBoxOutputFileNameFormatWithDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxOutputFileNameFormatWithDate.Location = new System.Drawing.Point(6, 141);
			this.textBoxOutputFileNameFormatWithDate.Name = "textBoxOutputFileNameFormatWithDate";
			this.textBoxOutputFileNameFormatWithDate.Size = new System.Drawing.Size(342, 20);
			this.textBoxOutputFileNameFormatWithDate.TabIndex = 46;
			this.textBoxOutputFileNameFormatWithDate.TextChanged += new System.EventHandler(this.textBoxOutputFileNameFormatWithDate_TextChanged);
			// 
			// btnResetFileNameFormatWithDate
			// 
			this.btnResetFileNameFormatWithDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResetFileNameFormatWithDate.Location = new System.Drawing.Point(354, 141);
			this.btnResetFileNameFormatWithDate.Name = "btnResetFileNameFormatWithDate";
			this.btnResetFileNameFormatWithDate.Size = new System.Drawing.Size(110, 20);
			this.btnResetFileNameFormatWithDate.TabIndex = 45;
			this.btnResetFileNameFormatWithDate.Text = "Вернуть как было";
			this.btnResetFileNameFormatWithDate.UseVisualStyleBackColor = true;
			this.btnResetFileNameFormatWithDate.Click += new System.EventHandler(this.btnResetFileNameFormatWithDate_Click);
			// 
			// btnWtfFileMergerDirectory
			// 
			this.btnWtfFileMergerDirectory.Location = new System.Drawing.Point(175, 83);
			this.btnWtfFileMergerDirectory.Name = "btnWtfFileMergerDirectory";
			this.btnWtfFileMergerDirectory.Size = new System.Drawing.Size(21, 19);
			this.btnWtfFileMergerDirectory.TabIndex = 44;
			this.btnWtfFileMergerDirectory.Text = "?";
			this.btnWtfFileMergerDirectory.UseVisualStyleBackColor = true;
			this.btnWtfFileMergerDirectory.Click += new System.EventHandler(this.btnWtfFileMergerDirectory_Click);
			// 
			// textBoxFileMergerDirectory
			// 
			this.textBoxFileMergerDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFileMergerDirectory.Location = new System.Drawing.Point(6, 102);
			this.textBoxFileMergerDirectory.Name = "textBoxFileMergerDirectory";
			this.textBoxFileMergerDirectory.Size = new System.Drawing.Size(420, 20);
			this.textBoxFileMergerDirectory.TabIndex = 43;
			this.textBoxFileMergerDirectory.Leave += new System.EventHandler(this.textBoxFileMergerDirectory_Leave);
			// 
			// btnBrowseFileMergerDirectory
			// 
			this.btnBrowseFileMergerDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseFileMergerDirectory.Location = new System.Drawing.Point(432, 102);
			this.btnBrowseFileMergerDirectory.Name = "btnBrowseFileMergerDirectory";
			this.btnBrowseFileMergerDirectory.Size = new System.Drawing.Size(32, 23);
			this.btnBrowseFileMergerDirectory.TabIndex = 42;
			this.btnBrowseFileMergerDirectory.Text = "...";
			this.btnBrowseFileMergerDirectory.UseVisualStyleBackColor = true;
			this.btnBrowseFileMergerDirectory.Click += new System.EventHandler(this.btnBrowseFileMergerDirectory_Click);
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(3, 86);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(171, 13);
			this.label14.TabIndex = 41;
			this.label14.Text = "Папка для объединения чанков:";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(3, 242);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(113, 13);
			this.label11.TabIndex = 40;
			this.label11.Text = "Путь к FFMPEG.EXE:";
			// 
			// btnBrowseFfmpegFilePath
			// 
			this.btnBrowseFfmpegFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseFfmpegFilePath.Location = new System.Drawing.Point(432, 258);
			this.btnBrowseFfmpegFilePath.Name = "btnBrowseFfmpegFilePath";
			this.btnBrowseFfmpegFilePath.Size = new System.Drawing.Size(32, 20);
			this.btnBrowseFfmpegFilePath.TabIndex = 39;
			this.btnBrowseFfmpegFilePath.Text = "...";
			this.btnBrowseFfmpegFilePath.UseVisualStyleBackColor = true;
			this.btnBrowseFfmpegFilePath.Click += new System.EventHandler(this.btnBrowseFfmpegFilePath_Click);
			// 
			// textBoxFfmpegExeFilePath
			// 
			this.textBoxFfmpegExeFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxFfmpegExeFilePath.Location = new System.Drawing.Point(6, 258);
			this.textBoxFfmpegExeFilePath.Name = "textBoxFfmpegExeFilePath";
			this.textBoxFfmpegExeFilePath.Size = new System.Drawing.Size(420, 20);
			this.textBoxFfmpegExeFilePath.TabIndex = 38;
			this.textBoxFfmpegExeFilePath.Leave += new System.EventHandler(this.textBoxFfmpegFilePath_Leave);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(3, 164);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(232, 13);
			this.label8.TabIndex = 37;
			this.label8.Text = "Формат имени файла (дата не определена):";
			// 
			// textBoxOutputFileNameFormatWithoutDate
			// 
			this.textBoxOutputFileNameFormatWithoutDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxOutputFileNameFormatWithoutDate.Location = new System.Drawing.Point(6, 180);
			this.textBoxOutputFileNameFormatWithoutDate.Name = "textBoxOutputFileNameFormatWithoutDate";
			this.textBoxOutputFileNameFormatWithoutDate.Size = new System.Drawing.Size(342, 20);
			this.textBoxOutputFileNameFormatWithoutDate.TabIndex = 36;
			this.textBoxOutputFileNameFormatWithoutDate.TextChanged += new System.EventHandler(this.textBoxOutputFileNameFormatWithoutDate_TextChanged);
			// 
			// btnResetFileNameFormatWithoutDate
			// 
			this.btnResetFileNameFormatWithoutDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnResetFileNameFormatWithoutDate.Location = new System.Drawing.Point(354, 180);
			this.btnResetFileNameFormatWithoutDate.Name = "btnResetFileNameFormatWithoutDate";
			this.btnResetFileNameFormatWithoutDate.Size = new System.Drawing.Size(110, 20);
			this.btnResetFileNameFormatWithoutDate.TabIndex = 35;
			this.btnResetFileNameFormatWithoutDate.Text = "Вернуть как было";
			this.btnResetFileNameFormatWithoutDate.UseVisualStyleBackColor = true;
			this.btnResetFileNameFormatWithoutDate.Click += new System.EventHandler(this.btnResetFileNameFormatWithoutDate_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(3, 203);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(73, 13);
			this.label7.TabIndex = 34;
			this.label7.Text = "Веб-браузер:";
			// 
			// btnBrowseDownloadDirectory
			// 
			this.btnBrowseDownloadDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseDownloadDirectory.Location = new System.Drawing.Point(432, 22);
			this.btnBrowseDownloadDirectory.Name = "btnBrowseDownloadDirectory";
			this.btnBrowseDownloadDirectory.Size = new System.Drawing.Size(32, 20);
			this.btnBrowseDownloadDirectory.TabIndex = 28;
			this.btnBrowseDownloadDirectory.Text = "...";
			this.btnBrowseDownloadDirectory.UseVisualStyleBackColor = true;
			this.btnBrowseDownloadDirectory.Click += new System.EventHandler(this.btnBrowseDownloadDirectory_Click);
			// 
			// btnSelectWebBrowserFilePath
			// 
			this.btnSelectWebBrowserFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSelectWebBrowserFilePath.Location = new System.Drawing.Point(432, 219);
			this.btnSelectWebBrowserFilePath.Name = "btnSelectWebBrowserFilePath";
			this.btnSelectWebBrowserFilePath.Size = new System.Drawing.Size(32, 20);
			this.btnSelectWebBrowserFilePath.TabIndex = 33;
			this.btnSelectWebBrowserFilePath.Text = "...";
			this.btnSelectWebBrowserFilePath.UseVisualStyleBackColor = true;
			this.btnSelectWebBrowserFilePath.Click += new System.EventHandler(this.btnSelectWebBrowserFilePath_Click);
			// 
			// textBoxDownloadDirectory
			// 
			this.textBoxDownloadDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxDownloadDirectory.Location = new System.Drawing.Point(6, 22);
			this.textBoxDownloadDirectory.Name = "textBoxDownloadDirectory";
			this.textBoxDownloadDirectory.Size = new System.Drawing.Size(420, 20);
			this.textBoxDownloadDirectory.TabIndex = 26;
			this.textBoxDownloadDirectory.Leave += new System.EventHandler(this.textBoxDownloadDirectory_Leave);
			// 
			// textBoxWebBrowserFilePath
			// 
			this.textBoxWebBrowserFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxWebBrowserFilePath.Location = new System.Drawing.Point(6, 219);
			this.textBoxWebBrowserFilePath.Name = "textBoxWebBrowserFilePath";
			this.textBoxWebBrowserFilePath.Size = new System.Drawing.Size(420, 20);
			this.textBoxWebBrowserFilePath.TabIndex = 32;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(125, 13);
			this.label3.TabIndex = 30;
			this.label3.Text = "Папка для скачивания:";
			// 
			// textBoxTempDirectory
			// 
			this.textBoxTempDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxTempDirectory.Location = new System.Drawing.Point(6, 63);
			this.textBoxTempDirectory.Name = "textBoxTempDirectory";
			this.textBoxTempDirectory.Size = new System.Drawing.Size(420, 20);
			this.textBoxTempDirectory.TabIndex = 27;
			this.textBoxTempDirectory.Leave += new System.EventHandler(this.textBoxTempDirectory_Leave);
			// 
			// btnBrowseTempDirectory
			// 
			this.btnBrowseTempDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnBrowseTempDirectory.Location = new System.Drawing.Point(432, 63);
			this.btnBrowseTempDirectory.Name = "btnBrowseTempDirectory";
			this.btnBrowseTempDirectory.Size = new System.Drawing.Size(32, 20);
			this.btnBrowseTempDirectory.TabIndex = 29;
			this.btnBrowseTempDirectory.Text = "...";
			this.btnBrowseTempDirectory.UseVisualStyleBackColor = true;
			this.btnBrowseTempDirectory.Click += new System.EventHandler(this.btnBrowseTempDirectory_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(3, 47);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(164, 13);
			this.label4.TabIndex = 31;
			this.label4.Text = "Папка для временных файлов:";
			// 
			// FrameSettingsFilesAndFolders
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label18);
			this.Controls.Add(this.textBoxOutputFileNameFormatWithDate);
			this.Controls.Add(this.btnResetFileNameFormatWithDate);
			this.Controls.Add(this.btnWtfFileMergerDirectory);
			this.Controls.Add(this.textBoxFileMergerDirectory);
			this.Controls.Add(this.btnBrowseFileMergerDirectory);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.btnBrowseFfmpegFilePath);
			this.Controls.Add(this.textBoxFfmpegExeFilePath);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.textBoxOutputFileNameFormatWithoutDate);
			this.Controls.Add(this.btnResetFileNameFormatWithoutDate);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.btnBrowseDownloadDirectory);
			this.Controls.Add(this.btnSelectWebBrowserFilePath);
			this.Controls.Add(this.textBoxDownloadDirectory);
			this.Controls.Add(this.textBoxWebBrowserFilePath);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.textBoxTempDirectory);
			this.Controls.Add(this.btnBrowseTempDirectory);
			this.Controls.Add(this.label4);
			this.Name = "FrameSettingsFilesAndFolders";
			this.Size = new System.Drawing.Size(467, 286);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox textBoxOutputFileNameFormatWithDate;
		private System.Windows.Forms.Button btnResetFileNameFormatWithDate;
		private System.Windows.Forms.Button btnWtfFileMergerDirectory;
		private System.Windows.Forms.TextBox textBoxFileMergerDirectory;
		private System.Windows.Forms.Button btnBrowseFileMergerDirectory;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Button btnBrowseFfmpegFilePath;
		private System.Windows.Forms.TextBox textBoxFfmpegExeFilePath;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox textBoxOutputFileNameFormatWithoutDate;
		private System.Windows.Forms.Button btnResetFileNameFormatWithoutDate;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnBrowseDownloadDirectory;
		private System.Windows.Forms.Button btnSelectWebBrowserFilePath;
		private System.Windows.Forms.TextBox textBoxDownloadDirectory;
		private System.Windows.Forms.TextBox textBoxWebBrowserFilePath;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox textBoxTempDirectory;
		private System.Windows.Forms.Button btnBrowseTempDirectory;
		private System.Windows.Forms.Label label4;
	}
}
