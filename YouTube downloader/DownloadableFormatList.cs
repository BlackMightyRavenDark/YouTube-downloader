using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using YouTubeApiLib;
using static YouTube_downloader.Utils;

namespace YouTube_downloader
{
	internal class DownloadableFormatList
	{
		internal DateTime DateReceived { get; }
		internal DateTime UrlsExpirationDate { get; }
		internal bool IsExpired => _isYtdlVideo ? DateTime.UtcNow >= UrlsExpirationDate :
			(_streamingData == null || DateTime.UtcNow >= UrlsExpirationDate);
		internal List<YouTubeMediaTrackVideo> VideoOnlyTracks { get; private set; }
		internal List<YouTubeMediaTrackAudio> AudioOnlyTracks { get; private set; }
		internal List<YouTubeMediaTrackContainer> Containers { get; private set; }
		internal List<YouTubeMediaTrackHlsStream> Streams { get; private set; }
		internal int Count => GetFormatCount();

		private readonly YouTubeVideo _video;
		private readonly YouTubeStreamingData _streamingData;
		private bool _isFormatsSplitted;
		private readonly bool _isYtdlVideo;

		internal DownloadableFormatList(YouTubeVideo video, YouTubeStreamingData streamingData)
		{
			_video = video;
			_streamingData = streamingData;
			_isYtdlVideo = video is YtdlVideo;
			if (_isYtdlVideo)
			{
				DateReceived = (video as YtdlVideo).ReceivedDate;
				UrlsExpirationDate = (video as YtdlVideo).UrlsValidUntil;
			}
			else if (_streamingData != null)
			{
				DateReceived = streamingData.DateReceived;
				UrlsExpirationDate = streamingData.DateReceived + TimeSpan.FromSeconds(streamingData.GetLifeTimeSeconds());
			}
			else
			{
				DateReceived = DateTime.UtcNow;
				UrlsExpirationDate = DateReceived.AddMinutes(5.0);
			}
		}

		internal DownloadableFormatList(YtdlVideo video)
		{
			_video = video;
			DateReceived = video.ReceivedDate;
			_isYtdlVideo = true;
			UrlsExpirationDate = video.UrlsValidUntil;
		}

		private void SplitFormats()
		{
			if (!_isFormatsSplitted)
			{
				_isFormatsSplitted = true;
				if (_isYtdlVideo)
				{
					YtdlVideo v = _video as YtdlVideo;
					VideoOnlyTracks = FilterVideoTracks(v.TrackList).ToList();
					AudioOnlyTracks = FilterAudioTracks(v.TrackList).ToList();
					Containers = FilterContainerTracks(v.TrackList).ToList();
					Streams = FilterHlsTracks(v.TrackList).ToList();
				}
				else if (_streamingData != null)
				{
					YouTubeMediaFormatList formatList = _streamingData.Parse();
					VideoOnlyTracks = FilterVideoTracks(formatList.Tracks).ToList();
					AudioOnlyTracks = FilterAudioTracks(formatList.Tracks).ToList();
					Containers = FilterContainerTracks(formatList.Tracks).ToList();
					Streams = FilterHlsTracks(formatList.Tracks).ToList();
				}
			}
		}

		internal ContextMenuStrip BuildContextMenu(EventHandler menuItemClickHandler)
		{
			SplitFormats();

			if (_video.IsDashed)
			{
				if (config.SortDashFormatsByBitrate)
				{
					int SorterFunc(YouTubeMediaTrack x, YouTubeMediaTrack y)
					{
						if ((x.AverageBitrate <= 0 && y.AverageBitrate <= 0) || x.AverageBitrate == y.AverageBitrate)
						{
							return 0;
						}
						return x.AverageBitrate < y.AverageBitrate ? 1 : -1;
					}

					VideoOnlyTracks.Sort(SorterFunc);
					AudioOnlyTracks.Sort(SorterFunc);
				}
			}
			else if (config.SortFormatsByFileSize)
			{
				int SorterFunc(YouTubeMediaTrack x, YouTubeMediaTrack y)
				{
					if ((x.ContentLength <= 0L && y.ContentLength <= 0L) || x.ContentLength == y.ContentLength)
					{
						return 0;
					}
					return x.ContentLength < y.ContentLength ? 1 : -1;
				}

				VideoOnlyTracks.Sort(SorterFunc);
				AudioOnlyTracks.Sort(SorterFunc);
			}

			if (Streams.Count > 1)
			{
				Streams.Sort((x, y) =>
				{
					if (x.VideoHeight > 0 || y.VideoHeight > 0)
					{
						if (x.VideoHeight == y.VideoHeight)
						{
							return x.AverageBitrate < y.AverageBitrate ? 1 : -1;
						}

						return x.VideoHeight < y.VideoHeight ? 1 : -1;
					}

					if (x.AverageBitrate == y.AverageBitrate) { return 0; }
					return x.AverageBitrate < y.AverageBitrate ? 1 : -1;
				});
			}

			if (config.AlwaysMoveAudioId140ToTopOfList && AudioOnlyTracks.Count > 1)
			{
				for (int i = 0; i < AudioOnlyTracks.Count; ++i)
				{
					if (AudioOnlyTracks[i].FormatId == 140)
					{
						if (i != 0)
						{
							(AudioOnlyTracks[i], AudioOnlyTracks[0]) = (AudioOnlyTracks[0], AudioOnlyTracks[i]);
						}

						break;
					}
				}
			}

			List<TableRow> tableRows = new List<TableRow>();
			List<TableColumn> tableColumns = new List<TableColumn>()
			{
				new TableColumn(TableColumnAlignment.Left),
				new TableColumn(TableColumnAlignment.Left),
				new TableColumn(TableColumnAlignment.Right),
				new TableColumn(TableColumnAlignment.Right),
				new TableColumn(TableColumnAlignment.Right),
				new TableColumn(TableColumnAlignment.Left),
				new TableColumn(TableColumnAlignment.Left),
				new TableColumn(TableColumnAlignment.Right),
				new TableColumn(TableColumnAlignment.Right)
			};

			bool showHls = Streams.Count > 0 && (!config.ShowHlsTracksOnlyForStreams ||
				(config.ShowHlsTracksOnlyForStreams && _video.IsLiveNow) || VideoOnlyTracks.Count == 0);
			if (showHls)
			{
				foreach (YouTubeMediaTrackHlsStream trackHls in Streams)
				{
					tableRows.Add(trackHls.ToTableRow());
				}
			}

			foreach (YouTubeMediaTrackVideo trackVideo in VideoOnlyTracks)
			{
				tableRows.Add(trackVideo.ToTableRow());
			}

			foreach (YouTubeMediaTrackContainer trackContainer in Containers)
			{
				tableRows.Add(trackContainer.ToTableRow());
			}

			foreach (YouTubeMediaTrackAudio trackAudio in AudioOnlyTracks)
			{
				tableRows.Add(trackAudio.ToTableRow());
			}

			Table table = new Table(tableRows, tableColumns);
			table.Format();
			const string columnSeparator = " | ";

			if (table.Rows.Count > 0)
			{
				ContextMenuStrip menu = new ContextMenuStrip();
				menu.Font = new System.Drawing.Font("Lucida Console", config.MenusFontSize);
				foreach (TableRow row in table.Rows)
				{
					string rowText = row.Join(columnSeparator);
					ToolStripMenuItem mi = new ToolStripMenuItem(rowText)
					{
						Padding = new Padding(0, 4, 0, 4),
						Tag = row.Tag
					};
					if (menuItemClickHandler != null)
					{
						mi.Click += menuItemClickHandler;
					}
					menu.Items.Add(mi);
				}

				if (VideoOnlyTracks.Count + AudioOnlyTracks.Count > 0)
				{
					ToolStripMenuItem mi = new ToolStripMenuItem("Выбрать форматы...") { Tag = null };
					if (menuItemClickHandler != null)
					{
						mi.Click += menuItemClickHandler;
					}
					menu.Items.Add(mi);
				}

				menu.Renderer = new FormatListContextMenuRenderer();
				return menu;
			}

			return null;
		}

		private int GetFormatCount()
		{
			int count = 0;
			if (VideoOnlyTracks != null) { count += VideoOnlyTracks.Count; }
			if (AudioOnlyTracks != null) { count += AudioOnlyTracks.Count; }
			if (Containers != null) { count += Containers.Count; }
			if (Streams != null) { count += Streams.Count; }
			return count;
		}

		internal void Clear()
		{
			VideoOnlyTracks?.Clear();
			AudioOnlyTracks?.Clear();
			Containers?.Clear();
			Streams?.Clear();
			_isFormatsSplitted = false;
		}
	}
}
