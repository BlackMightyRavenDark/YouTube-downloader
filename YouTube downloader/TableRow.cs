
namespace YouTube_downloader
{
	internal class TableRow
	{
		public string[] RawData { get; }
		public object Tag { get; }

		public TableRow(string[] rawData, object tag)
		{
			RawData = rawData;
			Tag = tag;
		}

		public string Join(string separator)
		{
			int length = RawData.Length;
			string t = length > 0 ? RawData[0] : string.Empty;
			if (length > 1)
			{
				for (int i = 1; i < length; ++i)
				{
					if (i < length - 2 ||
						(!string.IsNullOrEmpty(RawData[i]) &&
						!string.IsNullOrWhiteSpace(RawData[i])))
					{
						t += separator + RawData[i];
					}
				}
			}
			return t;
		}
	}
}
