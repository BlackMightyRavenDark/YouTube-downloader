using System.Collections.Generic;

namespace YouTube_downloader
{
	public sealed class FavoriteItem
	{
		public string DisplayName { get; }
		public string Title { get; }
		public string Id { get; set; }
		public FavoriteItemType ItemType { get; set; }
		public string ChannelTitle { get; set; }
		public string ChannelId { get; set; }
		public FavoriteItem Parent { get; }
		public List<FavoriteItem> Children { get; }

		public FavoriteItem(string title, string displayName, FavoriteItem parent = null)
		{
			Title = title;
			DisplayName = displayName;
			Parent = parent ?? null;
			Children = new List<FavoriteItem>();
		}

		public FavoriteItem(string displayName) : this(displayName, displayName, null) { }
	}

	public enum FavoriteItemType { Video, Channel, Directory };
}
