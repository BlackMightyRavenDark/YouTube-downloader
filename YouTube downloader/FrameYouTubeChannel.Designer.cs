namespace YouTube_downloader
{
    partial class FrameYouTubeChannel
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
            this.imagePreview = new System.Windows.Forms.PictureBox();
            this.lblChannelTitle = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // imagePreview
            // 
            this.imagePreview.Location = new System.Drawing.Point(3, 3);
            this.imagePreview.Name = "imagePreview";
            this.imagePreview.Size = new System.Drawing.Size(79, 73);
            this.imagePreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imagePreview.TabIndex = 0;
            this.imagePreview.TabStop = false;
            this.imagePreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imagePreview_MouseDown);
            // 
            // lblChannelTitle
            // 
            this.lblChannelTitle.AutoSize = true;
            this.lblChannelTitle.Location = new System.Drawing.Point(88, 13);
            this.lblChannelTitle.Name = "lblChannelTitle";
            this.lblChannelTitle.Size = new System.Drawing.Size(76, 13);
            this.lblChannelTitle.TabIndex = 1;
            this.lblChannelTitle.Text = "lblChannelTitle";
            this.lblChannelTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblChannelTitle_MouseDown);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(281, 49);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 2;
            this.btnGo.Text = "GO";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            this.btnGo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnGo_MouseDown);
            // 
            // FrameYouTubeChannel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.lblChannelTitle);
            this.Controls.Add(this.imagePreview);
            this.Name = "FrameYouTubeChannel";
            this.Size = new System.Drawing.Size(394, 84);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrameYouTubeChannel_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.imagePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imagePreview;
        private System.Windows.Forms.Label lblChannelTitle;
        public System.Windows.Forms.Button btnGo;
    }
}
