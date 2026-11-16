using System.Collections.Generic;

public class DataCarPart
{
	private string Name__BackingField;

	private int NumParts__BackingField;

	private float BaseCost__BackingField;

	private int BreakCommonnessScore__BackingField;

	public List<string> BrokenTerms;

	public List<string> OptionalDescriptions;

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public int NumParts
	{
		get
		{
			return NumParts__BackingField;
		}
		set
		{
			NumParts__BackingField = value;
		}
	}

	public float BaseCost
	{
		get
		{
			return BaseCost__BackingField;
		}
		set
		{
			BaseCost__BackingField = value;
		}
	}

	public int BreakCommonnessScore
	{
		get
		{
			return BreakCommonnessScore__BackingField;
		}
		set
		{
			BreakCommonnessScore__BackingField = value;
		}
	}
}
