using System;

[Serializable]
public class SimCommunard : SimLover
{
	private int YearsFollowed__BackingField;

	private bool ExCommunard__BackingField;

	private bool IsThirdPartyPerson__BackingField;

	private bool IsFamily__BackingField;

	private CULT_COMMUNARD_RANKING Ranking__BackingField;

	private int TimesHeroMessedWith__BackingField;

	private int TimesHeroPranked__BackingField;

	private int HeroAgeAtLastAssaultHero__BackingField;

	private string LastInsultToHero__BackingField;

	private int HeroAgeAtLastInsultHero__BackingField;

	private int HeroAgeAtLastBlackmailComeback__BackingField;

	private int NumAgeUpsForBlackmailComeback__BackingField;

	private bool HasDissentComeback__BackingField;

	public int YearsFollowed
	{
		get
		{
			return YearsFollowed__BackingField;
		}
		set
		{
			YearsFollowed__BackingField = value;
		}
	}

	public bool ExCommunard
	{
		get
		{
			return ExCommunard__BackingField;
		}
		set
		{
			ExCommunard__BackingField = value;
		}
	}

	public bool IsThirdPartyPerson
	{
		get
		{
			return IsThirdPartyPerson__BackingField;
		}
		set
		{
			IsThirdPartyPerson__BackingField = value;
		}
	}

	public bool IsFamily
	{
		get
		{
			return IsFamily__BackingField;
		}
		set
		{
			IsFamily__BackingField = value;
		}
	}

	public CULT_COMMUNARD_RANKING Ranking
	{
		get
		{
			return Ranking__BackingField;
		}
		set
		{
			Ranking__BackingField = value;
		}
	}

	public int TimesHeroMessedWith
	{
		get
		{
			return TimesHeroMessedWith__BackingField;
		}
		set
		{
			TimesHeroMessedWith__BackingField = value;
		}
	}

	public int TimesHeroPranked
	{
		get
		{
			return TimesHeroPranked__BackingField;
		}
		set
		{
			TimesHeroPranked__BackingField = value;
		}
	}

	public int HeroAgeAtLastAssaultHero
	{
		get
		{
			return HeroAgeAtLastAssaultHero__BackingField;
		}
		set
		{
			HeroAgeAtLastAssaultHero__BackingField = value;
		}
	}

	public string LastInsultToHero
	{
		get
		{
			return LastInsultToHero__BackingField;
		}
		set
		{
			LastInsultToHero__BackingField = value;
		}
	}

	public int HeroAgeAtLastInsultHero
	{
		get
		{
			return HeroAgeAtLastInsultHero__BackingField;
		}
		set
		{
			HeroAgeAtLastInsultHero__BackingField = value;
		}
	}

	public int HeroAgeAtLastBlackmailComeback
	{
		get
		{
			return HeroAgeAtLastBlackmailComeback__BackingField;
		}
		set
		{
			HeroAgeAtLastBlackmailComeback__BackingField = value;
		}
	}

	public int NumAgeUpsForBlackmailComeback
	{
		get
		{
			return NumAgeUpsForBlackmailComeback__BackingField;
		}
		set
		{
			NumAgeUpsForBlackmailComeback__BackingField = value;
		}
	}

	public bool HasDissentComeback
	{
		get
		{
			return HasDissentComeback__BackingField;
		}
		set
		{
			HasDissentComeback__BackingField = value;
		}
	}

	public SimCommunard(bool initHere)
		: base(initHere: false)
	{
	}

	public SimCommunard(SimPerson otherPerson)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public new void ImpactLoyalty(int impactScore)
	{
	}

	public string HonorificName()
	{
		return null;
	}

	public new string RelationshipStatusString()
	{
		return null;
	}

	public string GetRankingString()
	{
		return null;
	}

	public void Demote()
	{
	}

	public void Promote()
	{
	}
}
