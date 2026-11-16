using System;

[Serializable]
public abstract class SimPodcastContent
{
	private string Name__BackingField;

	private string Genre__BackingField;

	private string SubGenre__BackingField;

	private string Emoji__BackingField;

	private int GenrePopularityScore__BackingField;

	private int YearsSinceRelease__BackingField;

	private double NetEarningTotal__BackingField;

	private bool IsViral__BackingField;

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

	public string Genre
	{
		get
		{
			return Genre__BackingField;
		}
		set
		{
			Genre__BackingField = value;
		}
	}

	public string SubGenre
	{
		get
		{
			return SubGenre__BackingField;
		}
		set
		{
			SubGenre__BackingField = value;
		}
	}

	public string Emoji
	{
		get
		{
			return Emoji__BackingField;
		}
		set
		{
			Emoji__BackingField = value;
		}
	}

	public int GenrePopularityScore
	{
		get
		{
			return GenrePopularityScore__BackingField;
		}
		set
		{
			GenrePopularityScore__BackingField = value;
		}
	}

	public int YearsSinceRelease
	{
		get
		{
			return YearsSinceRelease__BackingField;
		}
		set
		{
			YearsSinceRelease__BackingField = value;
		}
	}

	public double NetEarningTotal
	{
		get
		{
			return NetEarningTotal__BackingField;
		}
		set
		{
			NetEarningTotal__BackingField = value;
		}
	}

	public bool IsViral
	{
		get
		{
			return IsViral__BackingField;
		}
		set
		{
			IsViral__BackingField = value;
		}
	}

	public abstract int GetEpisodeCount();

	public abstract double GetNetEarnings();

	public abstract int GetActiveYears();

	public abstract int GetContentPopularity();

	public abstract int GetNumberOfActiveUsers();

	public abstract void IncrementAge();
}
