using System;
using System.Collections.Generic;

namespace YouTube_downloader
{
	internal class Table
	{
		public List<TableRow> Rows { get; }
		public List<TableColumn> Columns { get; }

		public Table(List<TableRow> rows, List<TableColumn> columns)
		{
			Rows = rows;
			Columns = columns;
		}

		public void Format()
		{
			for (int i = 0; i < Columns.Count; ++i)
			{
				int max = GetMaxColumnStringLength(i);
				for (int j = 0; j < Rows.Count; ++j)
				{
					if (max > 0)
					{
						switch (Columns[i].Alignment)
						{
							case TableColumnAlignment.Right:
								Rows[j].RawData[i] = Rows[j].RawData[i].PadLeft(max, ' ');
								break;

							case TableColumnAlignment.Left:
								Rows[j].RawData[i] = Rows[j].RawData[i].PadRight(max, ' ');
								break;
						}
					}
				}
			}
		}

		public int GetMaxColumnStringLength(int columnId)
		{
			int max = 0;
			foreach (TableRow row in Rows)
			{
				int length = row.RawData[columnId].Length;
				if (length > max) { max = length; }
			}
			return max;
		}

		public override string ToString()
		{
			string t = string.Empty;
			foreach (TableRow row in Rows)
			{
				string s = row.Join(" | ");
				t += s + Environment.NewLine;
			}
			return t;
		}
	}

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

	internal class TableColumn
	{
		public TableColumnAlignment Alignment { get; }

		public TableColumn(TableColumnAlignment alignment)
		{
			Alignment = alignment;
		}
	}

	internal enum TableColumnAlignment { Left, Right }
}
