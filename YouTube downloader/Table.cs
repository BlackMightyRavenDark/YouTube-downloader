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
				if (max > 0)
				{
					for (int j = 0; j < Rows.Count; ++j)
					{
						switch (Columns[i].Alignment)
						{
							case TableColumnAlignment.Right:
								Rows[j].RawData[i] = (string.IsNullOrEmpty(Rows[j].RawData[i]) ? string.Empty : Rows[j].RawData[i]).PadLeft(max);
								break;

							case TableColumnAlignment.Left:
								Rows[j].RawData[i] = (string.IsNullOrEmpty(Rows[j].RawData[i]) ? string.Empty : Rows[j].RawData[i]).PadRight(max);
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
				string t = row.RawData[columnId];
				int length = string.IsNullOrEmpty(t) ? 0 : t.Length;
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

	internal enum TableColumnAlignment { Left, Right }
}
