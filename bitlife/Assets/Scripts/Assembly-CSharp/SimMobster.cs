using System;

[Serializable]
public class SimMobster : SimLover, IComparable
{
	private int RankNI__BackingField;

	[NonSerialized]
	public DataMafiaMob _Mob;

	private string MobName__BackingField;

	private string Nickname__BackingField;

	private string UnparsedNickname__BackingField;

	private bool IsSnitch__BackingField;

	private int KnownYearsInMob__BackingField;

	private int AgeAtLastTriedToCollectEvidenceAgainst__BackingField;

	private int AgeAtLastAccusedHeroOfCollectingEvidence__BackingField;

	private int AgeAtLastGaveHeroClueAboutBeingASnitch__BackingField;

	public int RankNI
	{
		get
		{
			return RankNI__BackingField;
		}
		set
		{
			RankNI__BackingField = value;
		}
	}

	public DataMafiaMob Mob
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string MobName
	{
		get
		{
			return MobName__BackingField;
		}
		set
		{
			MobName__BackingField = value;
		}
	}

	public string Nickname
	{
		get
		{
			return Nickname__BackingField;
		}
		set
		{
			Nickname__BackingField = value;
		}
	}

	public string UnparsedNickname
	{
		get
		{
			return UnparsedNickname__BackingField;
		}
		set
		{
			UnparsedNickname__BackingField = value;
		}
	}

	public bool IsSnitch
	{
		get
		{
			return IsSnitch__BackingField;
		}
		set
		{
			IsSnitch__BackingField = value;
		}
	}

	public int KnownYearsInMob
	{
		get
		{
			return KnownYearsInMob__BackingField;
		}
		set
		{
			KnownYearsInMob__BackingField = value;
		}
	}

	public int AgeAtLastTriedToCollectEvidenceAgainst
	{
		get
		{
			return AgeAtLastTriedToCollectEvidenceAgainst__BackingField;
		}
		set
		{
			AgeAtLastTriedToCollectEvidenceAgainst__BackingField = value;
		}
	}

	public int AgeAtLastAccusedHeroOfCollectingEvidence
	{
		get
		{
			return AgeAtLastAccusedHeroOfCollectingEvidence__BackingField;
		}
		set
		{
			AgeAtLastAccusedHeroOfCollectingEvidence__BackingField = value;
		}
	}

	public int AgeAtLastGaveHeroClueAboutBeingASnitch
	{
		get
		{
			return AgeAtLastGaveHeroClueAboutBeingASnitch__BackingField;
		}
		set
		{
			AgeAtLastGaveHeroClueAboutBeingASnitch__BackingField = value;
		}
	}

	public SimMobster()
		: base(initHere: false)
	{
	}

	public SimMobster(int theRankNI, DataMafiaMob theMob)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public string GetName()
	{
		return null;
	}

	public bool NeedsRealNameClarified()
	{
		return false;
	}

	public string GetRankString()
	{
		return null;
	}

	public bool IsMadeMan()
	{
		return false;
	}

	public bool IsGodfather()
	{
		return false;
	}

	public int GetEmojiMood()
	{
		return 0;
	}

	public new int CompareTo(object otherObject)
	{
		return 0;
	}
}
