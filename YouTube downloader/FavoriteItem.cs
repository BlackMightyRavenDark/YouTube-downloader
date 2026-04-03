using System.Collections.Generic;

namespace YouTube_downloader
{
	public sealed class FavoriteItem
	{
		public List<FavoriteItem> Children { get; private set; } = new List<FavoriteItem>();
		public FavoriteItem Parent { get; private set; } = null;
		public string DisplayName { get; set; }
		public string Title { get; set; }
		public FavoriteItemType ItemType { get; set; }
		public string ID { get; set; } = null;
		public string ChannelTitle { get; set; } = null;
		public string ChannelId { get; set; } = null;

		public FavoriteItem(string displayName)
		{
			DisplayName = displayName;
			Title = displayName;
		}

		public FavoriteItem(string title, string displayName, string id,
			string channelTitle, string channelId, FavoriteItem parent)
		{
			Title = title;
			DisplayName = displayName;
			ID = id;
			ChannelTitle = channelTitle;
			ChannelId = channelId;
			Parent = parent;
		}
	}

	public enum FavoriteItemType { Video, Channel, Directory };
}
