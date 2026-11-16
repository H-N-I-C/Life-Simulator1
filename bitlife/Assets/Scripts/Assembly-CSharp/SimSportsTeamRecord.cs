using System;

[Serializable]
public class SimSportsTeamRecord
{
	private int Wins__BackingField;

	private int Losses__BackingField;

	private int Ties__BackingField;

	private int LeagueRank__BackingField;

	private int LeagueTeams__BackingField;

	public int Wins
	{
		get
		{
			return Wins__BackingField;
		}
		set
		{
			Wins__BackingField = value;
		}
	}

	public int Losses
	{
		get
		{
			return Losses__BackingField;
		}
		set
		{
			Losses__BackingField = value;
		}
	}

	public int Ties
	{
		get
		{
			return Ties__BackingField;
		}
		set
		{
			Ties__BackingField = value;
		}
	}

	public int LeagueRank
	{
		get
		{
			return LeagueRank__BackingField;
		}
		set
		{
			LeagueRank__BackingField = value;
		}
	}

	public int LeagueTeams
	{
		get
		{
			return LeagueTeams__BackingField;
		}
		set
		{
			LeagueTeams__BackingField = value;
		}
	}

	public int Points()
	{
		return 0;
	}

	public int RecordScore()
	{
		return 0;
	}
}
