public class DataNewsEvent
{
	private string Type__BackingField;

	private string Scenario__BackingField;

	public string Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public string Scenario
	{
		get
		{
			return Scenario__BackingField;
		}
		set
		{
			Scenario__BackingField = value;
		}
	}

	public string FormatScenarioForCountry(DataCountry country)
	{
		return null;
	}
}
