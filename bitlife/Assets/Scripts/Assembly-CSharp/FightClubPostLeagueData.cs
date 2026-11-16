public class FightClubPostLeagueData
{
	private SimFightClubLeagueFighter Fighter__BackingField;

	private string Arena__BackingField;

	private double Earnings__BackingField;

	private bool IsPlayerWinner__BackingField;

	private int HeroLeaguePosition__BackingField;

	private int OpponentLeaguePosition__BackingField;

	private int HeroLeaguePositionBeforeFight__BackingField;

	public SimFightClubLeagueFighter Fighter
	{
		get
		{
			return Fighter__BackingField;
		}
		set
		{
			Fighter__BackingField = value;
		}
	}

	public string Arena
	{
		get
		{
			return Arena__BackingField;
		}
		set
		{
			Arena__BackingField = value;
		}
	}

	public double Earnings
	{
		get
		{
			return Earnings__BackingField;
		}
		set
		{
			Earnings__BackingField = value;
		}
	}

	public bool IsPlayerWinner
	{
		get
		{
			return IsPlayerWinner__BackingField;
		}
		set
		{
			IsPlayerWinner__BackingField = value;
		}
	}

	public int HeroLeaguePosition
	{
		get
		{
			return HeroLeaguePosition__BackingField;
		}
		set
		{
			HeroLeaguePosition__BackingField = value;
		}
	}

	public int OpponentLeaguePosition
	{
		get
		{
			return OpponentLeaguePosition__BackingField;
		}
		set
		{
			OpponentLeaguePosition__BackingField = value;
		}
	}

	public int HeroLeaguePositionBeforeFight
	{
		get
		{
			return HeroLeaguePositionBeforeFight__BackingField;
		}
		set
		{
			HeroLeaguePositionBeforeFight__BackingField = value;
		}
	}
}
