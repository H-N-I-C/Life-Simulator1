namespace Genesis.CSV.CSVTranslators.Primitives
{
	public sealed class BooleanTranslator : CSVValueTranslator<bool>
	{
		private const char COMMON_CHAR_X = 'x';

		public override bool ConvertTo(string rawValue)
		{
			return false;
		}
	}
}
