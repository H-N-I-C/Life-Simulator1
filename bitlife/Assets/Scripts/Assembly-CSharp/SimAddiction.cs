using System;

[Serializable]
public class SimAddiction
{
	private int Type__BackingField;

	private int YearsSuffered__BackingField;

	private string _DrugName;

	public int Type
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

	public int YearsSuffered
	{
		get
		{
			return YearsSuffered__BackingField;
		}
		set
		{
			YearsSuffered__BackingField = value;
		}
	}

	public DataDrug Drug
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string AddictionString()
	{
		return null;
	}

	public bool IsSubstance()
	{
		return false;
	}

	public bool IsAlcohol()
	{
		return false;
	}

	public int AddictivenessScore()
	{
		return 0;
	}

	public int AnnualHealthImpact()
	{
		return 0;
	}

	public DataDiseaseType GetAResultingDiseaseType()
	{
		return null;
	}

	public DataBodyPart GetImpactingBodyPart()
	{
		return null;
	}
}
