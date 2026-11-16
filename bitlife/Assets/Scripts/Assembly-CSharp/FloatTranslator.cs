using Genesis.CSV.CSVTranslators;

public class FloatTranslator : CSVValueTranslator<float>
{
	public override float ConvertTo(string rawValue)
	{
		return 0f;
	}
}
