using System;
using System.Drawing;
using System.Windows.Forms;

namespace YouTube_downloader
{
	//TODO: Complete writing this class
	public partial class FrameYouTubeChannel : UserControl
	{
		public YouTubeChannelInfo ChannelInfo { get; private set; } = null;
		public delegate void ActivatedDelegate(object sender);
		public delegate void OpenChannelDelegate(object sender, string channelName, string channelId);
		public ActivatedDelegate Activated;
		public OpenChannelDelegate OpenChannel;

		public FrameYouTubeChannel()
		{
			InitializeComponent();
		}

		public void SetChannelInfo(YouTubeChannelInfo channelInfo)
		{
			ChannelInfo = channelInfo;
			lblChannelTitle.Text = channelInfo.Title;
			if (channelInfo.ImageData != null)
			{
				imagePreview.Image = Image.FromStream(channelInfo.ImageData);
			}
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			OpenChannel?.Invoke(this, ChannelInfo.Title, ChannelInfo.Id);
		}

		private void btnGo_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
		}

		private void imagePreview_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
		}

		private void lblChannelTitle_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
		}

		private void FrameYouTubeChannel_MouseDown(object sender, MouseEventArgs e)
		{
			Activated?.Invoke(this);
		}
	}
}
