using System;
using Game;

[Serializable]
public class SimLuxurySecretSociety
{
	private string Name__BackingField;

	private double EntryFee__BackingField;

	private int MembersInChapter__BackingField;

	private int YearsInChapter__BackingField;

	private int Rank__BackingField;

	private int NumTimesHeroAttendedGatheringThisYear__BackingField;

	private float _standing;

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

	public double EntryFee
	{
		get
		{
			return EntryFee__BackingField;
		}
		set
		{
			EntryFee__BackingField = value;
		}
	}

	public int MembersInChapter
	{
		get
		{
			return MembersInChapter__BackingField;
		}
		set
		{
			MembersInChapter__BackingField = value;
		}
	}

	public int YearsInChapter
	{
		get
		{
			return YearsInChapter__BackingField;
		}
		set
		{
			YearsInChapter__BackingField = value;
		}
	}

	public int Rank
	{
		get
		{
			return Rank__BackingField;
		}
		set
		{
			Rank__BackingField = value;
		}
	}

	public int NumTimesHeroAttendedGatheringThisYear
	{
		get
		{
			return NumTimesHeroAttendedGatheringThisYear__BackingField;
		}
		set
		{
			NumTimesHeroAttendedGatheringThisYear__BackingField = value;
		}
	}

	public DataLuxurySecretSocietie SocietyData => null;

	public SimLuxurySecretSociety(DataLuxurySecretSocietie societyData)
	{
	}

	public void IncrementAge()
	{
	}

	public bool ImpactStanding(int impact)
	{
		return false;
	}

	public string GetRankStringForRank(int rank)
	{
		return null;
	}

	public void ResetStanding()
	{
	}
}
