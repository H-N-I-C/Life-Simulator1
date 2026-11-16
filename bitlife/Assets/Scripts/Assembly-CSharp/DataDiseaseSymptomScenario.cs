public class DataDiseaseSymptomScenario
{
	private string Symptom__BackingField;

	private string AbbreviatedSymptom__BackingField;

	private bool IsSupplementary__BackingField;

	private string Scenario__BackingField;

	private string LogVersion__BackingField;

	private int Gender__BackingField;

	private int MinAge__BackingField;

	private int ConcernScore__BackingField;

	public string Symptom
	{
		get
		{
			return Symptom__BackingField;
		}
		set
		{
			Symptom__BackingField = value;
		}
	}

	public string AbbreviatedSymptom
	{
		get
		{
			return AbbreviatedSymptom__BackingField;
		}
		set
		{
			AbbreviatedSymptom__BackingField = value;
		}
	}

	public bool IsSupplementary
	{
		get
		{
			return IsSupplementary__BackingField;
		}
		set
		{
			IsSupplementary__BackingField = value;
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

	public int MinAge
	{
		get
		{
			return MinAge__BackingField;
		}
		set
		{
			MinAge__BackingField = value;
		}
	}

	public int ConcernScore
	{
		get
		{
			return ConcernScore__BackingField;
		}
		set
		{
			ConcernScore__BackingField = value;
		}
	}

	public string GetShortName()
	{
		return null;
	}
}
