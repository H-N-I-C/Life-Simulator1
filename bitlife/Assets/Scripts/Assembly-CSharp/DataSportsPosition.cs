using System;
using System.Collections;

public class DataSportsPosition : IComparable
{
	private int Index__BackingField;

	private int Sport__BackingField;

	private string Name__BackingField;

	private int MoneyScore__BackingField;

	private int FameScore__BackingField;

	private int MaxAge__BackingField;

	private int NumInTeam__BackingField;

	private int NumInSquad__BackingField;

	private ArrayList Tier1StatsArray__BackingField;

	private ArrayList Tier2StatsArray__BackingField;

	private ArrayList Tier3StatsArray__BackingField;

	private ArrayList SkillsArray__BackingField;

	public int Index
	{
		get
		{
			return Index__BackingField;
		}
		set
		{
			Index__BackingField = value;
		}
	}

	public int Sport
	{
		get
		{
			return Sport__BackingField;
		}
		set
		{
			Sport__BackingField = value;
		}
	}

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

	public int MoneyScore
	{
		get
		{
			return MoneyScore__BackingField;
		}
		set
		{
			MoneyScore__BackingField = value;
		}
	}

	public int FameScore
	{
		get
		{
			return FameScore__BackingField;
		}
		set
		{
			FameScore__BackingField = value;
		}
	}

	public int MaxAge
	{
		get
		{
			return MaxAge__BackingField;
		}
		set
		{
			MaxAge__BackingField = value;
		}
	}

	public int NumInTeam
	{
		get
		{
			return NumInTeam__BackingField;
		}
		set
		{
			NumInTeam__BackingField = value;
		}
	}

	public int NumInSquad
	{
		get
		{
			return NumInSquad__BackingField;
		}
		set
		{
			NumInSquad__BackingField = value;
		}
	}

	public ArrayList Tier1StatsArray
	{
		get
		{
			return Tier1StatsArray__BackingField;
		}
		set
		{
			Tier1StatsArray__BackingField = value;
		}
	}

	public ArrayList Tier2StatsArray
	{
		get
		{
			return Tier2StatsArray__BackingField;
		}
		set
		{
			Tier2StatsArray__BackingField = value;
		}
	}

	public ArrayList Tier3StatsArray
	{
		get
		{
			return Tier3StatsArray__BackingField;
		}
		set
		{
			Tier3StatsArray__BackingField = value;
		}
	}

	public ArrayList SkillsArray
	{
		get
		{
			return SkillsArray__BackingField;
		}
		set
		{
			SkillsArray__BackingField = value;
		}
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}
}
