using System;

[Serializable]
public class SimMusicProducerArtist : SimLover
{
	private MUSIC_PRODUCER_GENRE Genre__BackingField;

	private string Vibe__BackingField;

	private bool ScoutedByHero__BackingField;

	private float VocalQuality__BackingField;

	private string StageName__BackingField;

	private bool HasDoneClubTakeoverThisYear__BackingField;

	private int YearsSinceWorkingWithHero__BackingField;

	private float _popularity;

	public MUSIC_PRODUCER_GENRE Genre
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

	public string Vibe
	{
		get
		{
			return Vibe__BackingField;
		}
		set
		{
			Vibe__BackingField = value;
		}
	}

	public bool ScoutedByHero
	{
		get
		{
			return ScoutedByHero__BackingField;
		}
		set
		{
			ScoutedByHero__BackingField = value;
		}
	}

	public float VocalQuality
	{
		get
		{
			return VocalQuality__BackingField;
		}
		set
		{
			VocalQuality__BackingField = value;
		}
	}

	public string StageName
	{
		get
		{
			return StageName__BackingField;
		}
		set
		{
			StageName__BackingField = value;
		}
	}

	public bool HasDoneClubTakeoverThisYear
	{
		get
		{
			return HasDoneClubTakeoverThisYear__BackingField;
		}
		set
		{
			HasDoneClubTakeoverThisYear__BackingField = value;
		}
	}

	public int YearsSinceWorkingWithHero
	{
		get
		{
			return YearsSinceWorkingWithHero__BackingField;
		}
		set
		{
			YearsSinceWorkingWithHero__BackingField = value;
		}
	}

	public SimMusicProducerArtist(bool initHere, MUSIC_PRODUCER_GENRE genre, string vibe, float vocalQuality, float popularity, bool scoutedByHero)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public void SetPopularity(float popularity)
	{
	}

	public float GetPopularity()
	{
		return 0f;
	}

	public void ImpactPopularity(int impactScore)
	{
	}

	public override string GetRelationshipString()
	{
		return null;
	}

	public override string GetPreferredName()
	{
		return null;
	}
}
