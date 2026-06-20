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
	}
}
