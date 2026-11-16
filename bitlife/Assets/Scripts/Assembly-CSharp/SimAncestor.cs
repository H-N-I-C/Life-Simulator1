using System;

[Serializable]
public class SimAncestor
{
	private string FullName__BackingField;

	private string LastName__BackingField;

	private int Gender__BackingField;

	private int AgeHeroTookOver__BackingField;

	private int AgeAtDeath__BackingField;

	private double NetWorth__BackingField;

	private double AmountLeftToDescendantHero__BackingField;

	public string FullName
	{
		get
		{
			return FullName__BackingField;
		}
		set
		{
			FullName__BackingField = value;
		}
	}

	public string LastName
	{
		get
		{
			return LastName__BackingField;
		}
		set
		{
			LastName__BackingField = value;
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

	public int AgeHeroTookOver
	{
		get
		{
			return AgeHeroTookOver__BackingField;
		}
		set
		{
			AgeHeroTookOver__BackingField = value;
		}
	}

	public int AgeAtDeath
	{
		get
		{
			return AgeAtDeath__BackingField;
		}
		set
		{
			AgeAtDeath__BackingField = value;
		}
	}

	public double NetWorth
	{
		get
		{
			return NetWorth__BackingField;
		}
		set
		{
			NetWorth__BackingField = value;
		}
	}

	public double AmountLeftToDescendantHero
	{
		get
		{
			return AmountLeftToDescendantHero__BackingField;
		}
		set
		{
			AmountLeftToDescendantHero__BackingField = value;
		}
	}

	public string AncestorString()
	{
		return null;
	}
}
