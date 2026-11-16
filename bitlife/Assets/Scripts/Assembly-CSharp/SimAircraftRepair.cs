using System;

[Serializable]
public class SimAircraftRepair
{
	private float Cost__BackingField;

	private string _DataString;

	private string BrokenPartTerm__BackingField;

	public float Cost
	{
		get
		{
			return Cost__BackingField;
		}
		set
		{
			Cost__BackingField = value;
		}
	}

	public DataAircraftPart Part
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string BrokenPartTerm
	{
		get
		{
			return BrokenPartTerm__BackingField;
		}
		set
		{
			BrokenPartTerm__BackingField = value;
		}
	}

	public string DescriptiveString()
	{
		return null;
	}
}
