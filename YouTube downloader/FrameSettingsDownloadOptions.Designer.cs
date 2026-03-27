
namespace YouTube_downloader
{
	partial class FrameSettingsDownloadOptions
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
			this.components = new System.ComponentModel.Container();
			this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted = new System.Windows.Forms.CheckBox();
			this.groupBoxAttempts = new System.Windows.Forms.GroupBox();
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
			this.groupBoxContainerOptions = new System.Windows.Forms.GroupBox();
			this.groupBoxConainerType = new System.Windows.Forms.GroupBox();
			this.radioButtonContainerTypeMkv = new System.Windows.Forms.RadioButton();
			this.radioButtonContainerTypeMp4 = new System.Windows.Forms.RadioButton();
			this.checkBoxAutomaticallyMergeAdaptiveTracks = new System.Windows.Forms.CheckBox();
			this.checkBoxDeleteSourceFilesWhenMerged = new System.Windows.Forms.CheckBox();
			this.groupBoxAudioTracks = new System.Windows.Forms.GroupBox();
			this.checkBoxAutomaticallyDownloadFirstAudioTrack = new System.Windows.Forms.CheckBox();
			this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger = new System.Windows.Forms.CheckBox();
			this.checkBoxAutomaticallyDownloadSecondAudioTrack = new System.Windows.Forms.CheckBox();
			this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks = new System.Windows.Forms.CheckBox();
			this.checkBoxAutomaticallySaveVideoThumbnailImage = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBoxAttempts.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownChunkDownloadErrorCountLimit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownChunkDownloadTryCountLimit)).BeginInit();
			this.groupBoxAdaptiveFormatsSettings.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayAfterContainerCreated)).BeginInit();
			this.groupBoxContainerOptions.SuspendLayout();
			this.groupBoxConainerType.SuspendLayout();
			this.groupBoxAudioTracks.SuspendLayout();
			this.SuspendLayout();
			// 
			// checkBoxCheckUrlsAccessibilityBeforeDownloadStarted
			// 
			this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.AutoSize = true;
			this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Location = new System.Drawing.Point(3, 289);
			this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Name = "checkBoxCheckUrlsAccessibilityBeforeDownloadStarted";
			this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Size = new System.Drawing.Size(317, 17);
			this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.TabIndex = 25;
			this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.Text = "Проверять доступность всех ссылок перед скачиванием";
			this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.UseVisualStyleBackColor = true;
			this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted.CheckedChanged += new System.EventHandler(this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted_CheckedChanged);
			// 
			// groupBoxAttempts
			// 
			this.groupBoxAttempts.Controls.Add(this.label28);
			this.groupBoxAttempts.Controls.Add(this.label27);
			this.groupBoxAttempts.Controls.Add(this.numericUpDownChunkDownloadErrorCountLimit);
			this.groupBoxAttempts.Controls.Add(this.label26);
			this.groupBoxAttempts.Controls.Add(this.label21);
			this.groupBoxAttempts.Controls.Add(this.numericUpDownChunkDownloadTryCountLimit);
			this.groupBoxAttempts.Location = new System.Drawing.Point(3, 312);
			this.groupBoxAttempts.Name = "groupBoxAttempts";
			this.groupBoxAttempts.Size = new System.Drawing.Size(381, 72);
			this.groupBoxAttempts.TabIndex = 24;
			this.groupBoxAttempts.TabStop = false;
			this.groupBoxAttempts.Text = "Попытки скачивания";
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
			this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.numericUpDownDelayAfterContainerCreated);
			this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.label20);
			this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.label19);
			this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.btnWtfDownloadAllAdaptiveVideoTracks);
			this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks);
			this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.groupBoxContainerOptions);
			this.groupBoxAdaptiveFormatsSettings.Controls.Add(this.groupBoxAudioTracks);
			this.groupBoxAdaptiveFormatsSettings.Location = new System.Drawing.Point(3, 3);
			this.groupBoxAdaptiveFormatsSettings.Name = "groupBoxAdaptiveFormatsSettings";
			this.groupBoxAdaptiveFormatsSettings.Size = new System.Drawing.Size(469, 280);
			this.groupBoxAdaptiveFormatsSettings.TabIndex = 23;
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
			// groupBoxContainerOptions
			// 
			this.groupBoxContainerOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxContainerOptions.Controls.Add(this.groupBoxConainerType);
			this.groupBoxContainerOptions.Controls.Add(this.checkBoxAutomaticallyMergeAdaptiveTracks);
			this.groupBoxContainerOptions.Controls.Add(this.checkBoxDeleteSourceFilesWhenMerged);
			this.groupBoxContainerOptions.Location = new System.Drawing.Point(6, 160);
			this.groupBoxContainerOptions.Name = "groupBoxContainerOptions";
			this.groupBoxContainerOptions.Size = new System.Drawing.Size(457, 83);
			this.groupBoxContainerOptions.TabIndex = 18;
			this.groupBoxContainerOptions.TabStop = false;
			this.groupBoxContainerOptions.Text = "Контейнер";
			// 
			// groupBoxConainerType
			// 
			this.groupBoxConainerType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxConainerType.Controls.Add(this.radioButtonContainerTypeMkv);
			this.groupBoxConainerType.Controls.Add(this.radioButtonContainerTypeMp4);
			this.groupBoxConainerType.Location = new System.Drawing.Point(295, 19);
			this.groupBoxConainerType.Name = "groupBoxConainerType";
			this.groupBoxConainerType.Size = new System.Drawing.Size(156, 60);
			this.groupBoxConainerType.TabIndex = 17;
			this.groupBoxConainerType.TabStop = false;
			this.groupBoxConainerType.Text = "Тип контейнера";
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
			// groupBoxAudioTracks
			// 
			this.groupBoxAudioTracks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBoxAudioTracks.Controls.Add(this.checkBoxAutomaticallyDownloadFirstAudioTrack);
			this.groupBoxAudioTracks.Controls.Add(this.checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger);
			this.groupBoxAudioTracks.Controls.Add(this.checkBoxAutomaticallyDownloadSecondAudioTrack);
			this.groupBoxAudioTracks.Controls.Add(this.checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks);
			this.groupBoxAudioTracks.Location = new System.Drawing.Point(6, 42);
			this.groupBoxAudioTracks.Name = "groupBoxAudioTracks";
			this.groupBoxAudioTracks.Size = new System.Drawing.Size(457, 112);
			this.groupBoxAudioTracks.TabIndex = 17;
			this.groupBoxAudioTracks.TabStop = false;
			this.groupBoxAudioTracks.Text = "Скачивание аудио-дорожек";
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
			this.checkBoxAutomaticallySaveVideoThumbnailImage.Location = new System.Drawing.Point(3, 390);
			this.checkBoxAutomaticallySaveVideoThumbnailImage.Name = "checkBoxAutomaticallySaveVideoThumbnailImage";
			this.checkBoxAutomaticallySaveVideoThumbnailImage.Size = new System.Drawing.Size(175, 17);
			this.checkBoxAutomaticallySaveVideoThumbnailImage.TabIndex = 22;
			this.checkBoxAutomaticallySaveVideoThumbnailImage.Text = "Скачивать картинку от видео";
			this.checkBoxAutomaticallySaveVideoThumbnailImage.UseVisualStyleBackColor = true;
			this.checkBoxAutomaticallySaveVideoThumbnailImage.CheckedChanged += new System.EventHandler(this.checkBoxAutomaticallySaveVideoThumbnailImage_CheckedChanged);
			// 
			// FrameSettingsDownloadOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.Controls.Add(this.checkBoxCheckUrlsAccessibilityBeforeDownloadStarted);
			this.Controls.Add(this.groupBoxAttempts);
			this.Controls.Add(this.groupBoxAdaptiveFormatsSettings);
			this.Controls.Add(this.checkBoxAutomaticallySaveVideoThumbnailImage);
			this.MinimumSize = new System.Drawing.Size(490, 0);
			this.Name = "FrameSettingsDownloadOptions";
			this.Size = new System.Drawing.Size(473, 290);
			this.groupBoxAttempts.ResumeLayout(false);
			this.groupBoxAttempts.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownChunkDownloadErrorCountLimit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownChunkDownloadTryCountLimit)).EndInit();
			this.groupBoxAdaptiveFormatsSettings.ResumeLayout(false);
			this.groupBoxAdaptiveFormatsSettings.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownDelayAfterContainerCreated)).EndInit();
			this.groupBoxContainerOptions.ResumeLayout(false);
			this.groupBoxContainerOptions.PerformLayout();
			this.groupBoxConainerType.ResumeLayout(false);
			this.groupBoxConainerType.PerformLayout();
			this.groupBoxAudioTracks.ResumeLayout(false);
			this.groupBoxAudioTracks.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxCheckUrlsAccessibilityBeforeDownloadStarted;
		private System.Windows.Forms.GroupBox groupBoxAttempts;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.NumericUpDown numericUpDownChunkDownloadErrorCountLimit;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.NumericUpDown numericUpDownChunkDownloadTryCountLimit;
		private System.Windows.Forms.GroupBox groupBoxAdaptiveFormatsSettings;
		private System.Windows.Forms.NumericUpDown numericUpDownDelayAfterContainerCreated;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Button btnWtfDownloadAllAdaptiveVideoTracks;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyDownloadAllAdaptiveVideoTracks;
		private System.Windows.Forms.GroupBox groupBoxContainerOptions;
		private System.Windows.Forms.GroupBox groupBoxConainerType;
		private System.Windows.Forms.RadioButton radioButtonContainerTypeMkv;
		private System.Windows.Forms.RadioButton radioButtonContainerTypeMp4;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyMergeAdaptiveTracks;
		private System.Windows.Forms.CheckBox checkBoxDeleteSourceFilesWhenMerged;
		private System.Windows.Forms.GroupBox groupBoxAudioTracks;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyDownloadFirstAudioTrack;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyDownloadSecondAudioTrackOnlyIfFileSizeIsBigger;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyDownloadSecondAudioTrack;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallyDownloadAllAdaptiveAudioTracks;
		private System.Windows.Forms.CheckBox checkBoxAutomaticallySaveVideoThumbnailImage;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
