using System;
using System.Net;
using System.Windows.Forms;

namespace YouTube_downloader
{
	public partial class FormHttpHeadersEditor : Form
	{
		public WebHeaderCollection Headers { get; private set; }

		public FormHttpHeadersEditor(WebHeaderCollection headers)
		{
			InitializeComponent();
			if (headers != null)
			{
				textBoxHeaders.Text = MultiThreadedDownloaderLib.Utils.HttpHeadersToString(headers);
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			Headers = MultiThreadedDownloaderLib.Utils.ParseHttpHeaderList(textBoxHeaders.Text);
			Headers.Remove("Host");
			Headers.Remove("Range");
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnRestoreDefaults_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Вернуть значения по-умолчанию?", Text,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				Headers?.Clear();
				Utils.config.HttpHeaders = MultiThreadedDownloaderLib.Utils.ParseHttpHeaderList(Configurator.DEFAULT_HTTP_REQUEST_HEADERS);
				textBoxHeaders.Text = Configurator.DEFAULT_HTTP_REQUEST_HEADERS;
			}
		}
	}
}
