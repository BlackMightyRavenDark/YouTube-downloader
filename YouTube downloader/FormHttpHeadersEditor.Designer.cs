namespace YouTube_downloader
{
	partial class FormHttpHeadersEditor
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
			this.components = new System.ComponentModel.Container();
			this.textBoxHeaders = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.btnRestoreDefaults = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxHeaders
			// 
			this.textBoxHeaders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxHeaders.Location = new System.Drawing.Point(12, 38);
			this.textBoxHeaders.Multiline = true;
			this.textBoxHeaders.Name = "textBoxHeaders";
			this.textBoxHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxHeaders.Size = new System.Drawing.Size(360, 142);
			this.textBoxHeaders.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(140, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Введите HTTP-заголовки:";
			this.toolTip1.SetToolTip(this.label1, "Внимание! Некоторые заголовки игнорируются, либо их значения автоматически заменя" +
		"ются на нужные!");
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(297, 186);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(12, 186);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnRestoreDefaults
			// 
			this.btnRestoreDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRestoreDefaults.Location = new System.Drawing.Point(271, 9);
			this.btnRestoreDefaults.Name = "btnRestoreDefaults";
			this.btnRestoreDefaults.Size = new System.Drawing.Size(101, 23);
			this.btnRestoreDefaults.TabIndex = 4;
			this.btnRestoreDefaults.Text = "По-умолчанию";
			this.btnRestoreDefaults.UseVisualStyleBackColor = true;
			this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
			// 
			// FormHttpHeadersEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 221);
			this.Controls.Add(this.btnRestoreDefaults);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBoxHeaders);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(400, 260);
			this.Name = "FormHttpHeadersEditor";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Редактор HTTP-заголовков";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBoxHeaders;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button btnRestoreDefaults;
	}
}