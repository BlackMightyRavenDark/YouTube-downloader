using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
	public partial class FrameSettingsGuiOptions : UserControl
	{
		public FrameSettingsGuiOptions()
		{
			InitializeComponent();

			config.Saving += (s, json) =>
			{
				json["videoTitleFontSize"] = config.VideoTitleFontSize;
				json["menusFontSize"] = config.MenusFontSize;
				json["favoritesListFontSize"] = config.FavoritesListFontSize;
				json["sortFormatsByFileSize"] = config.SortFormatsByFileSize;
				json["sortDashFormatsByBitrate"] = config.SortDashFormatsByBitrate;
				json["alwaysMoveAudioId140ToTopOfList"] = config.AlwaysMoveAudioId140ToTopOfList;
				json["showHlsFormatsOnlyForStreams"] = config.ShowHlsTracksOnlyForStreams;
			};

			config.Loading += (s, json) =>
			{
				{
					JToken jt = json.Value<JToken>("videoTitleFontSize");
					if (jt != null)
					{
						config.VideoTitleFontSize = ClampValue(jt.Value<int>(), 8, 16);
					}
				}
				{
					JToken jt = json.Value<JToken>("menusFontSize");
					if (jt != null)
					{
						config.MenusFontSize = ClampValue(jt.Value<int>(), 9, 16);
					}
				}
				{
					JToken jt = json.Value<JToken>("favoritesListFontSize");
					if (jt != null)
					{
						config.FavoritesListFontSize = ClampValue(jt.Value<int>(), 8, 16);
					}
				}
				{
					JToken jt = json.Value<JToken>("sortFormatsByFileSize");
					if (jt != null)
					{
						config.SortFormatsByFileSize = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("sortDashFormatsByBitrate");
					if (jt != null)
					{
						config.SortDashFormatsByBitrate = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("alwaysMoveAudioId140ToTopOfList");
					if (jt != null)
					{
						config.AlwaysMoveAudioId140ToTopOfList = jt.Value<bool>();
					}
				}
				{
					JToken jt = json.Value<JToken>("showHlsFormatsOnlyForStreams");
					if (jt != null)
					{
						config.ShowHlsTracksOnlyForStreams = jt.Value<bool>();
					}
				}
			};

			config.Loaded += (s) =>
			{
				numericUpDownVideoTitleFontSize.Value = config.VideoTitleFontSize;
				numericUpDownMenusFontSize.Value = config.MenusFontSize;
				numericUpDownFavoritesListFontSize.Value = config.FavoritesListFontSize;
				checkBoxSortAdaptiveFormatsByFileSize.Checked = config.SortFormatsByFileSize;
				checkBoxSortDashFormatsByBitrate.Checked = config.SortDashFormatsByBitrate;
				checkBoxMoveAudioTrackId140ToTopOfList.Checked = config.AlwaysMoveAudioId140ToTopOfList;
				checkBoxShowHlsTracksOnlyForStreams.Checked = config.ShowHlsTracksOnlyForStreams;
			};
		}

		private void checkBoxSortAdaptiveFormatsByFileSize_CheckedChanged(object sender, EventArgs e)
		{
			config.SortFormatsByFileSize = checkBoxSortAdaptiveFormatsByFileSize.Checked;
		}

		private void checkBoxSortDashFormatsByBitrate_CheckedChanged(object sender, EventArgs e)
		{
			config.SortDashFormatsByBitrate = checkBoxSortDashFormatsByBitrate.Checked;
		}

		private void checkBoxMoveAudioTrackId140ToTopOfList_CheckedChanged(object sender, EventArgs e)
		{
			config.AlwaysMoveAudioId140ToTopOfList = checkBoxMoveAudioTrackId140ToTopOfList.Checked;
		}

		private void checkBoxShowHlsTracksOnlyForStreams_CheckedChanged(object sender, EventArgs e)
		{
			config.ShowHlsTracksOnlyForStreams = checkBoxShowHlsTracksOnlyForStreams.Checked;
		}

		private void numericUpDownVideoTitleFontSize_ValueChanged(object sender, EventArgs e)
		{
			config.VideoTitleFontSize = (int)numericUpDownVideoTitleFontSize.Value;
			foreach (FrameYouTubeVideo frameYouTubeVideo in framesVideo)
			{
				frameYouTubeVideo.SetVideoTitleFontSize(config.VideoTitleFontSize);
			}
		}

		private void numericUpDownMenusFontSize_ValueChanged(object sender, EventArgs e)
		{
			config.MenusFontSize = (int)numericUpDownMenusFontSize.Value;
			foreach (FrameYouTubeVideo frameYouTubeVideo in framesVideo)
			{
				frameYouTubeVideo.SetMenusFontSize(config.MenusFontSize);
			}
		}

		private void numericUpDownFavoritesListFontSize_ValueChanged(object sender, EventArgs e)
		{
			config.FavoritesListFontSize = (int)numericUpDownFavoritesListFontSize.Value;
		}
	}
}
