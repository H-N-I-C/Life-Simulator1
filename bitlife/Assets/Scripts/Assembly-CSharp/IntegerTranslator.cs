using Genesis.CSV.CSVTranslators;

public class IntegerTranslator : CSVValueTranslator<int>
{
	public override int ConvertTo(string rawValue)
	{
		return 0;
	}
}
