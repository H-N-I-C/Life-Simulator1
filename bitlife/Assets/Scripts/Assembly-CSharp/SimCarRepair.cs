using System;

[Serializable]
public class SimCarRepair
{
	private int Type__BackingField;

	private float Cost__BackingField;

	private string _CarPartName;

	private string BrokenPartTerm__BackingField;

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

	public DataCarPart Part
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
