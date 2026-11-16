using System;
using Game;

[Serializable]
public class SimFightClubInjury
{
	private string Type__BackingField;

	private string ResolvedType__BackingField;

	private string ResolvedFeedbackSnippet__BackingField;

	private string ResolvedLogSnippet__BackingField;

	private int HeroAgeWhenAcquired__BackingField;

	private int HeroAgeAtOrganicHeal__BackingField;

	private int HeroAgeWhenHealed__BackingField;

	private int HeroAgeAtLastPhysicalTherapyAttempt__BackingField;

	private int HeroAgeAtLastSurgeryAttempt__BackingField;

	private int HeroAgeAtLastFightWithInjury__BackingField;

	private int HealCost__BackingField;

	private bool _isHealed;

	[NonSerialized]
	private DataFightIngInjurie _injuryData;

	public string Type
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

	public string ResolvedType
	{
		get
		{
			return ResolvedType__BackingField;
		}
		set
		{
			ResolvedType__BackingField = value;
		}
	}

	public string ResolvedFeedbackSnippet
	{
		get
		{
			return ResolvedFeedbackSnippet__BackingField;
		}
		set
		{
			ResolvedFeedbackSnippet__BackingField = value;
		}
	}

	public string ResolvedLogSnippet
	{
		get
		{
			return ResolvedLogSnippet__BackingField;
		}
		set
		{
			ResolvedLogSnippet__BackingField = value;
		}
	}

	public int HeroAgeWhenAcquired
	{
		get
		{
			return HeroAgeWhenAcquired__BackingField;
		}
		set
		{
			HeroAgeWhenAcquired__BackingField = value;
		}
	}

	public int HeroAgeAtOrganicHeal
	{
		get
		{
			return HeroAgeAtOrganicHeal__BackingField;
		}
		set
		{
			HeroAgeAtOrganicHeal__BackingField = value;
		}
	}

	public int HeroAgeWhenHealed
	{
		get
		{
			return HeroAgeWhenHealed__BackingField;
		}
		set
		{
			HeroAgeWhenHealed__BackingField = value;
		}
	}

	public int HeroAgeAtLastPhysicalTherapyAttempt
	{
		get
		{
			return HeroAgeAtLastPhysicalTherapyAttempt__BackingField;
		}
		set
		{
			HeroAgeAtLastPhysicalTherapyAttempt__BackingField = value;
		}
	}

	public int HeroAgeAtLastSurgeryAttempt
	{
		get
		{
			return HeroAgeAtLastSurgeryAttempt__BackingField;
		}
		set
		{
			HeroAgeAtLastSurgeryAttempt__BackingField = value;
		}
	}

	public int HeroAgeAtLastFightWithInjury
	{
		get
		{
			return HeroAgeAtLastFightWithInjury__BackingField;
		}
		set
		{
			HeroAgeAtLastFightWithInjury__BackingField = value;
		}
	}

	public int HealCost
	{
		get
		{
			return HealCost__BackingField;
		}
		private set
		{
			HealCost__BackingField = value;
		}
	}

	public DataFightIngInjurie InjuryData
	{
		get
		{
			return null;
		}
		private set
		{
		}
	}

	public SimFightClubInjury(DataFightIngInjurie data)
	{
	}

	public string GetDisplayType()
	{
		return null;
	}

	public string GetDisplayFeedbackSnippet()
	{
		return null;
	}

	public string GetDisplayLogSnippet()
	{
		return null;
	}

	public string GetTypeWithIndefiniteArticle(bool useIndefiniteArticle)
	{
		return null;
	}

	private static string ResolveRandomBracketExpression(string text, out string selectedValue)
	{
		selectedValue = null;
		return null;
	}

	public float GetAttributeDecrease(FIGHT_CLUB_ATTRIBUTE attribute)
	{
		return 0f;
	}

	public bool AffectsAttribute(FIGHT_CLUB_ATTRIBUTE attribute)
	{
		return false;
	}

	public int GetPainScore()
	{
		return 0;
	}

	public bool IsHealed()
	{
		return false;
	}

	public void Heal()
	{
	}

	public bool RequiresPhysicalTherapy()
	{
		return false;
	}

	public bool RequiresSurgery()
	{
		return false;
	}

	public int GetDurationScore()
	{
		return 0;
	}
}
