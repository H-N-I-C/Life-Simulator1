public class DataAdoptionScenario
{
	private string Scenario__BackingField;

	private string LogVersion__BackingField;

	private int AdopteeGender__BackingField;

	private int AdopteeMinAge__BackingField;

	private int AdopteeMaxAge__BackingField;

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

	public string LogVersion
	{
		get
		{
			return LogVersion__BackingField;
		}
		set
		{
			LogVersion__BackingField = value;
		}
	}

	public int AdopteeGender
	{
		get
		{
			return AdopteeGender__BackingField;
		}
		set
		{
			AdopteeGender__BackingField = value;
		}
	}

	public int AdopteeMinAge
	{
		get
		{
			return AdopteeMinAge__BackingField;
		}
		set
		{
			AdopteeMinAge__BackingField = value;
		}
	}

	public int AdopteeMaxAge
	{
		get
		{
			return AdopteeMaxAge__BackingField;
		}
		set
		{
			AdopteeMaxAge__BackingField = value;
		}
	}
}
