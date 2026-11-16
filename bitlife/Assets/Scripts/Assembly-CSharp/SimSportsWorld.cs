using System;
using System.Collections;

[Serializable]
public class SimSportsWorld
{
	private ArrayList LeagueArray__BackingField;

	private int AgeAtLastRefresh__BackingField;

	public ArrayList LeagueArray
	{
		get
		{
			return LeagueArray__BackingField;
		}
		set
		{
			LeagueArray__BackingField = value;
		}
	}

	public int AgeAtLastRefresh
	{
		get
		{
			return AgeAtLastRefresh__BackingField;
		}
		set
		{
			AgeAtLastRefresh__BackingField = value;
		}
	}

	public void RefreshLeagues()
	{
	}

	public void PopulateLeagues()
	{
	}

	public ArrayList GetAvailableLeagueArrayForHeroGender()
	{
		return null;
	}

	public ArrayList GetLeagueArrayForHeroGenderAndSport(int sport)
	{
		return null;
	}

	public SimSportsLeague GetLeagueForIndex(int index)
	{
		return null;
	}

	public SimSportsTeam GetTeamForIndex(int index)
	{
		return null;
	}
}
