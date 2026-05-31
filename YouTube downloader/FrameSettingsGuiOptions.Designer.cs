
namespace YouTube_downloader
{
	partial class FrameSettingsGuiOptions
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
            this.checkBoxShowHlsTracksOnlyForStreams = new System.Windows.Forms.CheckBox();
            this.groupBoxVideoFormatListItemsSorting = new System.Windows.Forms.GroupBox();
            this.checkBoxSortDashFormatsByBitrate = new System.Windows.Forms.CheckBox();
            this.checkBoxMoveAudioTrackId140ToTopOfList = new System.Windows.Forms.CheckBox();
            this.checkBoxSortAdaptiveFormatsByFileSize = new System.Windows.Forms.CheckBox();
            this.groupBoxFontOptions = new System.Windows.Forms.GroupBox();
            this.numericUpDownVideoTitleFontSize = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDownFavoritesListFontSize = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.numericUpDownMenusFontSize = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxVideoFormatListItemsSorting.SuspendLayout();
            this.groupBoxFontOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoTitleFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFavoritesListFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMenusFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxShowHlsTracksOnlyForStreams
            // 
            this.checkBoxShowHlsTracksOnlyForStreams.AutoSize = true;
            this.checkBoxShowHlsTracksOnlyForStreams.Location = new System.Drawing.Point(3, 217);
            this.checkBoxShowHlsTracksOnlyForStreams.Name = "checkBoxShowHlsTracksOnlyForStreams";
            this.checkBoxShowHlsTracksOnlyForStreams.Size = new System.Drawing.Size(378, 17);
            this.checkBoxShowHlsTracksOnlyForStreams.TabIndex = 5;
            this.checkBoxShowHlsTracksOnlyForStreams.Text = "Показывать форматы HLS только для прямых трансляций (стримов)";
            this.toolTip1.SetToolTip(this.checkBoxShowHlsTracksOnlyForStreams, "Не работает при использовании youtube-dl");
            this.checkBoxShowHlsTracksOnlyForStreams.UseVisualStyleBackColor = true;
            this.checkBoxShowHlsTracksOnlyForStreams.CheckedChanged += new System.EventHandler(this.checkBoxShowHlsTracksOnlyForStreams_CheckedChanged);
            // 
            // groupBoxVideoFormatListItemsSorting
            // 
            this.groupBoxVideoFormatListItemsSorting.Controls.Add(this.checkBoxSortDashFormatsByBitrate);
            this.groupBoxVideoFormatListItemsSorting.Controls.Add(this.checkBoxMoveAudioTrackId140ToTopOfList);
            this.groupBoxVideoFormatListItemsSorting.Controls.Add(this.checkBoxSortAdaptiveFormatsByFileSize);
            this.groupBoxVideoFormatListItemsSorting.Location = new System.Drawing.Point(3, 120);
            this.groupBoxVideoFormatListItemsSorting.Name = "groupBoxVideoFormatListItemsSorting";
            this.groupBoxVideoFormatListItemsSorting.Size = new System.Drawing.Size(355, 91);
            this.groupBoxVideoFormatListItemsSorting.TabIndex = 4;
            this.groupBoxVideoFormatListItemsSorting.TabStop = false;
            this.groupBoxVideoFormatListItemsSorting.Text = "Сортировка списка форматов для скачивания";
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
            // groupBoxFontOptions
            // 
            this.groupBoxFontOptions.Controls.Add(this.numericUpDownVideoTitleFontSize);
            this.groupBoxFontOptions.Controls.Add(this.label17);
            this.groupBoxFontOptions.Controls.Add(this.numericUpDownFavoritesListFontSize);
            this.groupBoxFontOptions.Controls.Add(this.label16);
            this.groupBoxFontOptions.Controls.Add(this.label15);
            this.groupBoxFontOptions.Controls.Add(this.numericUpDownMenusFontSize);
            this.groupBoxFontOptions.Location = new System.Drawing.Point(3, 3);
            this.groupBoxFontOptions.Name = "groupBoxFontOptions";
            this.groupBoxFontOptions.Size = new System.Drawing.Size(188, 111);
            this.groupBoxFontOptions.TabIndex = 3;
            this.groupBoxFontOptions.TabStop = false;
            this.groupBoxFontOptions.Text = "Размер шрифтов";
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
            8,
            0,
            0,
            0});
            this.numericUpDownMenusFontSize.Name = "numericUpDownMenusFontSize";
            this.numericUpDownMenusFontSize.Size = new System.Drawing.Size(45, 20);
            this.numericUpDownMenusFontSize.TabIndex = 0;
            this.numericUpDownMenusFontSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numericUpDownMenusFontSize.ValueChanged += new System.EventHandler(this.numericUpDownMenusFontSize_ValueChanged);
            // 
            // FrameSettingsGuiOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxShowHlsTracksOnlyForStreams);
            this.Controls.Add(this.groupBoxVideoFormatListItemsSorting);
            this.Controls.Add(this.groupBoxFontOptions);
            this.Name = "FrameSettingsGuiOptions";
            this.Size = new System.Drawing.Size(396, 251);
            this.groupBoxVideoFormatListItemsSorting.ResumeLayout(false);
            this.groupBoxVideoFormatListItemsSorting.PerformLayout();
            this.groupBoxFontOptions.ResumeLayout(false);
            this.groupBoxFontOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVideoTitleFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFavoritesListFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMenusFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox checkBoxShowHlsTracksOnlyForStreams;
		private System.Windows.Forms.GroupBox groupBoxVideoFormatListItemsSorting;
		private System.Windows.Forms.CheckBox checkBoxSortDashFormatsByBitrate;
		private System.Windows.Forms.CheckBox checkBoxMoveAudioTrackId140ToTopOfList;
		private System.Windows.Forms.CheckBox checkBoxSortAdaptiveFormatsByFileSize;
		private System.Windows.Forms.GroupBox groupBoxFontOptions;
		private System.Windows.Forms.NumericUpDown numericUpDownVideoTitleFontSize;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.NumericUpDown numericUpDownFavoritesListFontSize;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.NumericUpDown numericUpDownMenusFontSize;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}
