using System;
using System.Drawing;
using System.Windows.Forms;
using YouTubeApiLib;

namespace YouTube_downloader
{
	internal class FormatListContextMenuRenderer : ToolStripProfessionalRenderer
	{
		protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
		{
			try
			{
				Rectangle rectangle = new(0, 0, e.Item.Width, e.Item.Height);
				Color color = GetItemBackgroundColor(e.Item.Tag, e.Item.BackColor);
				using (Brush brush = new SolidBrush(color))
				{
					e.Graphics.FillRectangle(brush, rectangle);
				}
				if (e.Item.Selected)
				{
					e.Graphics.DrawRectangle(Pens.Black, rectangle.Deflate(1, 0, 1, 1));
				}
			}
#if DEBUG
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
#else
			catch { }
#endif
		}

		protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
		{
			e.TextRectangle = new(10, 0, e.Item.Width, e.Item.Height);
			e.TextFormat = TextFormatFlags.VerticalCenter;
			base.OnRenderItemText(e);
		}

		private static Color GetItemBackgroundColor(object tag, Color defaultColor)
		{
			if (tag == null) { return defaultColor; }
			else
			{
				Type typeOfTag = tag.GetType();
				if (typeOfTag == typeof(YouTubeMediaTrackVideo)) { return Color.Lime; }
				else if (typeOfTag == typeof(YouTubeMediaTrackHlsStream)) { return Color.LightPink; }
				else if (typeOfTag == typeof(YouTubeMediaTrackContainer))
				{
					Color someShadeOfOrange = Color.FromArgb(0xFF, 0xFF, 0xBD, 0x51);
					return someShadeOfOrange;
				}
				else // audio track
				{
					return Color.LightSkyBlue;
				}
			}
		}
	}
}
