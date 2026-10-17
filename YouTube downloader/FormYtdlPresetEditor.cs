using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace YouTube_downloader
{
	public partial class FormYtdlPresetEditor : Form
	{
		internal List<string> Presets { get; private set; }

		public FormYtdlPresetEditor(IEnumerable<string> commandLines)
		{
			InitializeComponent();
			if (commandLines != null)
			{
				textBoxYtdlCommandLinePresets.Text = string.Join(Environment.NewLine, commandLines);
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(textBoxYtdlCommandLinePresets.Text))
			{
				MessageBox.Show("Ничего не введено!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			Presets = textBoxYtdlCommandLinePresets.Lines.ToList();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnRestoreDefaults_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Вернуть значения по-умолчанию?", Text,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				textBoxYtdlCommandLinePresets.Text = Configurator.DEFAULT_YTDL_COMMAND_LINE;
			}
		}
	}
}
