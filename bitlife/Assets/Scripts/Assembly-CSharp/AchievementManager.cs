using System;
using System.Collections;
using System.Collections.Generic;
using Game.Seasons.Models;

public class AchievementManager
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SeasonRedeemModel, string> _003C_003E9__111_0;

		internal string _003CCheckForAchievementBitPassOverachiever_003Eb__111_0(SeasonRedeemModel i)
		{
			return null;
		}
	}

	public ArrayList OrderedAchievementArray;

	protected ArrayList AgingArray;

	protected bool AgingAlphabetize;

	protected ArrayList WealthArray;

	protected bool WealthAlphabetize;

	protected ArrayList MilitaryArray;

	protected bool MilitaryAlphabetize;

	protected ArrayList JobsArray;

	protected bool JobsAlphabetize;

	protected ArrayList SocialMediaArray;

	protected bool SocialMediaAlphabetize;

	protected ArrayList ModelArray;

	protected bool ModelAlphabetize;

	protected ArrayList SecretAgentArray;

	protected bool SecretAgentAlphabetize;

	protected ArrayList EntertainmentArray;

	protected bool EntertainmentAlphabetize;

	protected ArrayList SchoolArray;

	protected bool SchoolAlphabetize;

	protected ArrayList PrisonArray;

	protected bool PrisonAlphabetize;

	protected ArrayList FertilityArray;

	protected bool FertilityAlphabetize;

	protected ArrayList LoveArray;

	protected bool LoveAlphabetize;

	protected ArrayList AnimalsArray;

	protected bool AnimalsAlphabetize;

	protected ArrayList HousesArray;

	protected bool HousesAlphabetize;

	protected ArrayList DiseaseArray;

	protected bool DiseaseAlphabetize;

	protected ArrayList CombatArray;

	protected bool CombatAlphabetize;

	protected ArrayList VehiclesArray;

	protected bool VehiclesAlphabetize;

	protected ArrayList CrimeArray;

	protected bool CrimeAlphabetize;

	protected ArrayList FameArray;

	protected bool FameAlphabetize;

	protected ArrayList PetsArray;

	protected bool PetsAlphabetize;

	protected ArrayList RoyaltyArray;

	protected bool RoyaltyAlphabetize;

	protected ArrayList SportsArray;

	protected bool SportsAlphabetize;

	protected ArrayList MusicArray;

	protected bool MusicAlphabetize;

	protected ArrayList ActorArray;

	protected bool ActorAlphabetize;

	protected ArrayList HustlerArray;

	protected bool HustlerAlphabetize;

	protected ArrayList BusinessArray;

	protected bool BusinessAlphabetize;

	protected ArrayList StockMarketArray;

	protected bool StockMarketAlphabetize;

	protected ArrayList LandlordArray;

	protected bool LandlordAlphabetize;

	protected ArrayList AstronautArray;

	protected bool AstronautAlphabetize;

	protected ArrayList BlackMarketArray;

	protected bool BlackMarketAlphabetize;

	protected ArrayList CultArray;

	protected bool CultAlphabetize;

	protected ArrayList ZooArray;

	protected bool ZooAlphabetize;

	protected ArrayList DealerArray;

	protected bool DealerAlphabetize;

	protected ArrayList CasinoArray;

	protected bool CasinoAlphabetize;

	protected ArrayList LuxuryArray;

	protected bool LuxuryAlphabetize;

	protected ArrayList RacingArray;

	protected bool RacingAlphabetize;

	protected ArrayList OutdoorLifestyleArray;

	protected bool OutdoorLifestyleAlphabetize;

	protected ArrayList VampireArray;

	protected bool VampireAlphabetize;

	protected ArrayList MiscArray;

	protected bool MiscAlphabetize;

	protected ArrayList StreaksArray;

	protected bool StreaksAlphabetize;

	protected ArrayList SeasonsArray;

	protected bool SeasonsAlphabetize;

	private static AchievementManager sharedAchievement;

	private const string BitPassOverAchieverAchievement = "acSeasonsClaimLevel050";

	private static Dictionary<int, string> StreaksAchievements;

	private static Dictionary<int, string> ClaimsAchievements;

	public static AchievementManager SharedAchievementManager => null;

	public void LoadDefaults()
	{
	}

	public ArrayList GetSectionArrayOrder()
	{
		return null;
	}

	public string GetStringForSectionArray(ArrayList array)
	{
		return null;
	}

	public ArrayList GetStringsForAchievement(string pAchievement)
	{
		return null;
	}

	public void LoadAchievements()
	{
	}

	public void SaveAchievements()
	{
	}

	public AchievementData GetAchievementDataForAchievement(string pAchievement)
	{
		return null;
	}

	public AchievementData GetAchievementDataForAchievementByName(string pAchievementName)
	{
		return null;
	}

	public void CompleteAchievement(string achievement)
	{
	}

	public ArrayList GetAllAchievementsWithEarnedFirstArray()
	{
		return null;
	}

	public ArrayList GetSortedAchievementsForSectionArray(ArrayList pSectionArray)
	{
		return null;
	}

	public ArrayList GetSectionsArrayWithEarnedFirst()
	{
		return null;
	}

	public bool GetCompletedSection(ArrayList pSectionArray)
	{
		return false;
	}

	public int GetNumberOfCompletionsInSection(ArrayList pSectionArray)
	{
		return 0;
	}

	public bool AchievementEarnedFor(string pAchievement)
	{
		return false;
	}

	public bool AchievementEarnedForTitle(string pAchievementName)
	{
		return false;
	}

	public int NumberOfPossibleAchievements()
	{
		return 0;
	}

	public int NumberOfEarnedAchievements()
	{
		return 0;
	}

	public bool GetSortAlphabeticallyForSectionArray(ArrayList array)
	{
		return false;
	}

	public void AnnualCheckForAchievements()
	{
	}

	public void CheckForAchievementForCivilianJobTitleAndCareer(string title, string career)
	{
	}

	public void CheckForAchievementForMilitaryRankAndStatusAndBranch(int rank, int status, int branch)
	{
	}

	public bool CheckForAchievementForSeasonsStreaks(int streakCount)
	{
		return false;
	}

	public void CheckForAchievementForSeasonsClaims()
	{
	}

	private bool CheckForAchievementBitPassOverachiever()
	{
		return false;
	}

	public void CheckForAchievementForSeasonsSpecialLifeRIP(Life life)
	{
	}
}
