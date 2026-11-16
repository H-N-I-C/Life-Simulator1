using System;

[Serializable]
public class SimLuxuryIslandStructure
{
	private LUXURY_ISLAND_STRUCTURE_TYPE Type__BackingField;

	private int Age__BackingField;

	private float _condition;

	public LUXURY_ISLAND_STRUCTURE_TYPE Type
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

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public SimLuxuryIslandStructure(LUXURY_ISLAND_STRUCTURE_TYPE type)
	{
	}

	public void IncrementAge()
	{
	}

	public float GetCondition()
	{
		return 0f;
	}

	public string GetListItemDescription()
	{
		return null;
	}

	public string GetProfileDescription()
	{
		return null;
	}

	public double GetValue()
	{
		return 0.0;
	}

	public static double GetPriceForType(LUXURY_ISLAND_STRUCTURE_TYPE type)
	{
		return 0.0;
	}

	public static string GetTypeStringForType(LUXURY_ISLAND_STRUCTURE_TYPE type, bool capitalize = false)
	{
		return null;
	}
}
