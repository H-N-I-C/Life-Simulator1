using System;

[Serializable]
public class SimPodcastEpisode : SimPodcastContent
{
	private bool IsLive__BackingField;

	private int EpisodeSuccessScore__BackingField;

	private int AdmissionFee__BackingField;

	private int NumberOfEarnedSubscribers__BackingField;

	private int Listeners__BackingField;

	public bool IsLive
	{
		get
		{
			return IsLive__BackingField;
		}
		set
		{
			IsLive__BackingField = value;
		}
	}

	public int EpisodeSuccessScore
	{
		get
		{
			return EpisodeSuccessScore__BackingField;
		}
		set
		{
			EpisodeSuccessScore__BackingField = value;
		}
	}

	public int AdmissionFee
	{
		get
		{
			return AdmissionFee__BackingField;
		}
		set
		{
			AdmissionFee__BackingField = value;
		}
	}

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

	public int Listeners
	{
		get
		{
			return Listeners__BackingField;
		}
		set
		{
			Listeners__BackingField = value;
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
