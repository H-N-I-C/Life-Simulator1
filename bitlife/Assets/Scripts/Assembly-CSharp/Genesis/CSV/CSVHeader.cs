using System;

namespace Genesis.CSV
{
	public class CSVHeader : IEquatable<CSVHeader>
	{
		private readonly string HeaderName__BackingField;

		private readonly CSVDocument Owner__BackingField;

		public string HeaderName => HeaderName__BackingField;

		public CSVDocument Owner => Owner__BackingField;

		public CSVHeader(string headerName, CSVDocument owningDocument)
		{
		}

		public bool Equals(CSVHeader other)
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
