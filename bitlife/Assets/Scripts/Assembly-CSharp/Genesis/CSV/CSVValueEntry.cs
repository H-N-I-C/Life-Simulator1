using System;
using Genesis.CSV.CSVTranslators;

namespace Genesis.CSV
{
	public class CSVValueEntry : IEquatable<CSVValueEntry>
	{
		private readonly CSVDocument Owner__BackingField;

		private readonly string Text__BackingField;

		public CSVDocument Owner => Owner__BackingField;

		public string Text => Text__BackingField;

		public CSVValueEntry(CSVDocument owner, string text)
		{
		}

		public T ConvertTo<T>(CSVValueTranslator<T> translator)
		{
			return default;
		}

		public bool TryConvertTo<T>(CSVValueTranslator<T> translator, out T value)
		{
			value = default;
			return false;
		}

		public bool CanConvertTo<T>(CSVValueTranslator<T> translator)
		{
			return false;
		}

		public bool Equals(CSVValueEntry other)
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
