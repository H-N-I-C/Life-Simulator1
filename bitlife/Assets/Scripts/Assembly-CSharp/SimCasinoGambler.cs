using System;
using System.Collections.Generic;

[Serializable]
public class SimCasinoGambler : SimLover
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string, string> _003C_003E9__78_0;

		internal string _003CGetFavoriteGamesString_003Eb__78_0(string i, string j)
		{
			return null;
		}
	}

	private int HeroAgeAtLastInteractedWithHero__BackingField;

	private CASINO_GAMBLING_STYLE GamblingStyle__BackingField;

	private int TimesHeroMessedWith__BackingField;

	private int NumDrinksHeroOfferedThisYear__BackingField;

	private int NumTimesHeroEncouragedToGambleThisYear__BackingField;

	private int NumGiftsHeroTriedToGiveThisYear__BackingField;

	private int TournamentsWon__BackingField;

	private int TournamentsLost__BackingField;

	private int HeroAgeAtLastInsultHero__BackingField;

	private string LastInsultToHero__BackingField;

	private int HeroAgeAtLastAssaultHero__BackingField;

	private double TotalMoneyBet__BackingField;

	private double LastYearWinnings__BackingField;

	private double TotalWinnings__BackingField;

	private double TotalLoses__BackingField;

	private double _currentYearWinnings;

	private float _addictionLevel;

	private List<CASINO_GAME_TYPE> _favoriteGames;

	public int HeroAgeAtLastInteractedWithHero
	{
		get
		{
			return HeroAgeAtLastInteractedWithHero__BackingField;
		}
		set
		{
			HeroAgeAtLastInteractedWithHero__BackingField = value;
		}
	}

	public CASINO_GAMBLING_STYLE GamblingStyle
	{
		get
		{
			return GamblingStyle__BackingField;
		}
		set
		{
			GamblingStyle__BackingField = value;
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

	public int NumDrinksHeroOfferedThisYear
	{
		get
		{
			return NumDrinksHeroOfferedThisYear__BackingField;
		}
		set
		{
			NumDrinksHeroOfferedThisYear__BackingField = value;
		}
	}

	public int NumTimesHeroEncouragedToGambleThisYear
	{
		get
		{
			return NumTimesHeroEncouragedToGambleThisYear__BackingField;
		}
		set
		{
			NumTimesHeroEncouragedToGambleThisYear__BackingField = value;
		}
	}

	public int NumGiftsHeroTriedToGiveThisYear
	{
		get
		{
			return NumGiftsHeroTriedToGiveThisYear__BackingField;
		}
		set
		{
			NumGiftsHeroTriedToGiveThisYear__BackingField = value;
		}
	}

	public int TournamentsWon
	{
		get
		{
			return TournamentsWon__BackingField;
		}
		set
		{
			TournamentsWon__BackingField = value;
		}
	}

	public int TournamentsLost
	{
		get
		{
			return TournamentsLost__BackingField;
		}
		set
		{
			TournamentsLost__BackingField = value;
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

	public double TotalMoneyBet
	{
		get
		{
			return TotalMoneyBet__BackingField;
		}
		set
		{
			TotalMoneyBet__BackingField = value;
		}
	}

	public double LastYearWinnings
	{
		get
		{
			return LastYearWinnings__BackingField;
		}
		set
		{
			LastYearWinnings__BackingField = value;
		}
	}

	public double TotalWinnings
	{
		get
		{
			return TotalWinnings__BackingField;
		}
		set
		{
			TotalWinnings__BackingField = value;
		}
	}

	public double TotalLoses
	{
		get
		{
			return TotalLoses__BackingField;
		}
		set
		{
			TotalLoses__BackingField = value;
		}
	}

	public SimCasinoGambler(bool initHere)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public new string RelationshipStatusString()
	{
		return null;
	}

	public bool IsHighRoller()
	{
		return false;
	}

	public bool ShouldUpdateGamblingStyle()
	{
		return false;
	}

	public CASINO_GAMBLING_STYLE DetermineCorrectGamblingStyle()
	{
		return CASINO_GAMBLING_STYLE.NONE;
	}

	public CASINO_GAMBLING_STYLE UpdateGamblingStyle()
	{
		return CASINO_GAMBLING_STYLE.NONE;
	}

	public static string GetGamblingStyleString(CASINO_GAMBLING_STYLE style)
	{
		return null;
	}

	public virtual string GamblerName()
	{
		return null;
	}

	public string GamblerTypeString()
	{
		return null;
	}

	public float GetAddictionLevel()
	{
		return 0f;
	}

	public void SetAddictionLevel(float value)
	{
	}

	public void ImpactAddictionlevel(int impactScore)
	{
	}

	public new virtual void ImpactWillpower(int impactScore)
	{
	}

	public static bool IsMafiaMember(SimCasinoGambler gambler)
	{
		return false;
	}

	public string GetFavoriteGamesString()
	{
		return null;
	}

	public List<CASINO_GAME_TYPE> GetFavoriteGames()
	{
		return null;
	}

	private void GenerateFavGames()
	{
	}

	public CASINO_GAME_TYPE GetRandomFavoriteGame()
	{
		return CASINO_GAME_TYPE.NONE;
	}

	public void AddWinningValue(double bet)
	{
	}

	public void AddLosingValue(double bet)
	{
	}
}
