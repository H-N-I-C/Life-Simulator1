using System;

namespace Genesis.CSV.CSVTranslators
{
	public abstract class CSVValueTranslator<T>
	{
		private class TypeConversionException : Exception
		{
		}

		public T ConvertTo(CSVValueEntry entry)
		{
			return default;
		}

		public bool TryConvertTo(CSVValueEntry entry, out T value)
		{
			value = default;
			return false;
		}

		public bool TryConvertTo(string rawValue, out T value)
		{
			value = default;
			return false;
		}

		public bool CanConvert(CSVValueEntry entry)
		{
			return false;
		}

		public bool CanConvert(string rawValue)
		{
			return false;
		}

		protected Exception GetTypeMismatchException(string rawValue)
		{
			return null;
		}

		public abstract T ConvertTo(string rawValue);
	}
}
