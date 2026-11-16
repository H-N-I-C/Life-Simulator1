using System;

namespace Genesis.CSV
{
	public class CSVLine : IEquatable<CSVLine>
	{
		private readonly CSVDocument Owner__BackingField;

		private readonly CSVValueEntry[] Values__BackingField;

		private CSVDocument Owner => Owner__BackingField;

		public CSVValueEntry[] Values => Values__BackingField;

		public CSVLine(CSVDocument owningDocument, CSVValueEntry[] entries)
		{
		}

		public string GetCSVItemContent(string headerKey)
		{
			return null;
		}

		public CSVValueEntry GetCSVItem(string headerKey)
		{
			return null;
		}

		public CSVValueEntry GetCSVItem(CSVHeader headerKey)
		{
			return null;
		}

		public bool IsEmpty()
		{
			return false;
		}

		public bool Equals(CSVLine other)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
