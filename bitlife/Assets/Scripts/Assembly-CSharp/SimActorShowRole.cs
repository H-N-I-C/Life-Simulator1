using System;

[Serializable]
public class SimActorShowRole : IComparable
{
	private int Type__BackingField;

	private string Title__BackingField;

	private int Gender__BackingField;

	private int Age__BackingField;

	private string _RawShowTitle;

	private PersonEmojiScript EmojiScript__BackingField;

	private float CompensationPerEpisode__BackingField;

	private float StartingCompensationPerEpisodeOffer__BackingField;

	private int HeroAgeAtLastInterest__BackingField;

	private int HeroAgeAtLastInterestViaAgent__BackingField;

	private int NumNegotiations__BackingField;

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public string Title
	{
		get
		{
			return Title__BackingField;
		}
		set
		{
			Title__BackingField = value;
		}
	}

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public DataActorShow ShowData
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public PersonEmojiScript EmojiScript
	{
		get
		{
			return EmojiScript__BackingField;
		}
		set
		{
			EmojiScript__BackingField = value;
		}
	}

	public float CompensationPerEpisode
	{
		get
		{
			return CompensationPerEpisode__BackingField;
		}
		set
		{
			CompensationPerEpisode__BackingField = value;
		}
	}

	public float StartingCompensationPerEpisodeOffer
	{
		get
		{
			return StartingCompensationPerEpisodeOffer__BackingField;
		}
		set
		{
			StartingCompensationPerEpisodeOffer__BackingField = value;
		}
	}

	public int HeroAgeAtLastInterest
	{
		get
		{
			return HeroAgeAtLastInterest__BackingField;
		}
		set
		{
			HeroAgeAtLastInterest__BackingField = value;
		}
	}

	public int HeroAgeAtLastInterestViaAgent
	{
		get
		{
			return HeroAgeAtLastInterestViaAgent__BackingField;
		}
		set
		{
			HeroAgeAtLastInterestViaAgent__BackingField = value;
		}
	}

	public int NumNegotiations
	{
		get
		{
			return NumNegotiations__BackingField;
		}
		set
		{
			NumNegotiations__BackingField = value;
		}
	}

	public bool CanHeroPlayRole()
	{
		return false;
	}

	public float GetMatchForHeroScore()
	{
		return 0f;
	}

	public SimActorShow GetCorrespondingShow()
	{
		return null;
	}

	public string GetRoleTypeString(bool useArticle, bool useRole, bool capitalize)
	{
		return null;
	}

	public string GetEmojiString()
	{
		return null;
	}

	public int CompareTo(object obj)
	{
		return 0;
	}
}
