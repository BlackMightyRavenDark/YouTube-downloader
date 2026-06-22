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

		/// <summary>
		/// Creates an sub-element of the existing 'parent' element.
		/// </summary>
		public FavoriteItem(string title, string displayName, FavoriteItem parent)
		{
			Title = title;
			DisplayName = displayName;
			Parent = parent;
			Children = new();
		}

		/// <summary>
		/// Creates an element in the root folder.
		/// </summary>
		public FavoriteItem(string title, string displayName) : this(title, displayName, Utils.favoritesRootNode) { }

		/// <summary>
		/// Creates a root folder.
		/// </summary>
		public FavoriteItem(string displayName) : this(displayName, displayName, Utils.favoritesRootNode)
		{
			ItemType = FavoriteItemType.Directory;
		}
	}

	public enum FavoriteItemType { Video, Channel, Directory };
}
