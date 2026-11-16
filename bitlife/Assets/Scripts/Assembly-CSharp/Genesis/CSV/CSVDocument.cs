using System.IO;
using UnityEngine;

namespace Genesis.CSV
{
	public class CSVDocument
	{
		private CSVHeader[] Headers__BackingField;

		private CSVLine[] Lines__BackingField;

		public CSVHeader[] Headers
		{
			get
			{
				return Headers__BackingField;
			}
			private set
			{
				Headers__BackingField = value;
			}
		}

		public CSVLine[] Lines
		{
			get
			{
				return Lines__BackingField;
			}
			private set
			{
				Lines__BackingField = value;
			}
		}

		private CSVDocument()
		{
		}

		public string GetHeaderText()
		{
			return null;
		}

		public int GetHeaderIndex(string headerContent)
		{
			return 0;
		}

		public int GetHeaderIndex(CSVHeader header)
		{
			return 0;
		}

		public override string ToString()
		{
			return null;
		}

		public static CSVDocument Load(string path)
		{
			return null;
		}

		public static CSVDocument Read(TextAsset textAsset)
		{
			return null;
		}

		public static CSVDocument Read(Stream stream)
		{
			return null;
		}

		private static CSVHeader[] CreateCSVHeaders(string csvHeadersText, CSVDocument owningDocument)
		{
			return null;
		}

		private static CSVLine CreateCSVLine(string lineText, CSVDocument owningDocument)
		{
			return null;
		}
	}
}
