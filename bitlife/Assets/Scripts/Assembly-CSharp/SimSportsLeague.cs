using System;
using System.Collections;

[Serializable]
public class SimSportsLeague : IComparable
{
	[NonSerialized]
	public DataSportsLeague _Data;

	private int LeagueIndex__BackingField;

	private ArrayList _TeamArray__BackingField;

	public DataSportsLeague Data
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int LeagueIndex
	{
		get
		{
			return LeagueIndex__BackingField;
		}
		set
		{
			LeagueIndex__BackingField = value;
		}
	}

	private ArrayList _TeamArray
	{
		get
		{
			return _TeamArray__BackingField;
		}
		set
		{
			_TeamArray__BackingField = value;
		}
	}

	public ArrayList TeamArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimSportsLeague(DataSportsLeague theLeague, ArrayList theTeamArray)
	{
	}

	public void UpdateRecordsAtEndOfSeason()
	{
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}
}
