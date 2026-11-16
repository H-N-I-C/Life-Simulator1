using System;

[Serializable]
public class SimPodcastSeason : SimPodcastContent
{
	private int NumberOfEarnedSubscribers__BackingField;

	private int ActiveUsers__BackingField;

	private int NumberOfEpisodes__BackingField;

	private int SeasonSuccessScore__BackingField;

	private int SeasonNumber__BackingField;

	private string PremierEpisodeName__BackingField;

	public int NumberOfEarnedSubscribers
	{
		get
		{
			return NumberOfEarnedSubscribers__BackingField;
		}
		set
		{
			NumberOfEarnedSubscribers__BackingField = value;
		}
	}

	public int ActiveUsers
	{
		get
		{
			return ActiveUsers__BackingField;
		}
		set
		{
			ActiveUsers__BackingField = value;
		}
	}

	public int NumberOfEpisodes
	{
		get
		{
			return NumberOfEpisodes__BackingField;
		}
		set
		{
			NumberOfEpisodes__BackingField = value;
		}
	}

	public int SeasonSuccessScore
	{
		get
		{
			return SeasonSuccessScore__BackingField;
		}
		set
		{
			SeasonSuccessScore__BackingField = value;
		}
	}

	public int SeasonNumber
	{
		get
		{
			return SeasonNumber__BackingField;
		}
		set
		{
			SeasonNumber__BackingField = value;
		}
	}

	public string PremierEpisodeName
	{
		get
		{
			return PremierEpisodeName__BackingField;
		}
		set
		{
			PremierEpisodeName__BackingField = value;
		}
	}

	public override int GetEpisodeCount()
	{
		return 0;
	}

	public override double GetNetEarnings()
	{
		return 0.0;
	}

	public override int GetActiveYears()
	{
		return 0;
	}

	public override int GetContentPopularity()
	{
		return 0;
	}

	public override int GetNumberOfActiveUsers()
	{
		return 0;
	}

	public override void IncrementAge()
	{
	}
}
