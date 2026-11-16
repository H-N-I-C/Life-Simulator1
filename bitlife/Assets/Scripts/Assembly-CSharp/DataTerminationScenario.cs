public class DataTerminationScenario
{
	private string Scenario__BackingField;

	private int Condition__BackingField;

	private int Gender__BackingField;

	private bool Clean__BackingField;

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

	public int Condition
	{
		get
		{
			return Condition__BackingField;
		}
		set
		{
			Condition__BackingField = value;
		}
	}

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}

	public bool Clean
	{
		get
		{
			return Clean__BackingField;
		}
		set
		{
			Clean__BackingField = value;
		}
	}

	public string GetScenarioString()
	{
		return null;
	}
}
