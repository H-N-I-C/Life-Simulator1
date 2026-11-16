using System.Collections.Generic;

public class DataDumbScenarioDealer : DataDumbScenario
{
	private int MinInfamy__BackingField;

	private int MaxInfamy__BackingField;

	private int MinStanding__BackingField;

	private int MaxStanding__BackingField;

	private List<DEALER_RANK> HeroRank__BackingField;

	private bool HeroHasLover__BackingField;

	private int LoverGender__BackingField;

	public int MinInfamy
	{
		get
		{
			return MinInfamy__BackingField;
		}
		set
		{
			MinInfamy__BackingField = value;
		}
	}

	public int MaxInfamy
	{
		get
		{
			return MaxInfamy__BackingField;
		}
		set
		{
			MaxInfamy__BackingField = value;
		}
	}

	public int MinStanding
	{
		get
		{
			return MinStanding__BackingField;
		}
		set
		{
			MinStanding__BackingField = value;
		}
	}

	public int MaxStanding
	{
		get
		{
			return MaxStanding__BackingField;
		}
		set
		{
			MaxStanding__BackingField = value;
		}
	}

	public List<DEALER_RANK> HeroRank
	{
		get
		{
			return HeroRank__BackingField;
		}
		set
		{
			HeroRank__BackingField = value;
		}
	}

	public bool HeroHasLover
	{
		get
		{
			return HeroHasLover__BackingField;
		}
		set
		{
			HeroHasLover__BackingField = value;
		}
	}

	public int LoverGender
	{
		get
		{
			return LoverGender__BackingField;
		}
		set
		{
			LoverGender__BackingField = value;
		}
	}
}
