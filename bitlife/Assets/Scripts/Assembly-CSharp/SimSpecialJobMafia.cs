using System;
using System.Collections;

[Serializable]
public class SimSpecialJobMafia : SimSpecialJob
{
	private SimMafiaFamily Family__BackingField;

	private int RankNI__BackingField;

	private int YearsCompleted__BackingField;

	private double YearBeforeLastContribution__BackingField;

	private double LastYearContribution__BackingField;

	private double ThisYearContribution__BackingField;

	private double TotalContribution__BackingField;

	private double ThisYearCommission__BackingField;

	private double LastYearIncome__BackingField;

	private string Nickname__BackingField;

	private SimMafiaInformantCase InformantCase__BackingField;

	private ArrayList TargetArray__BackingField;

	private int OrdersFollowed__BackingField;

	private int OrdersDelegated__BackingField;

	private int BadChoices__BackingField;

	private int AgeAtJoin__BackingField;

	private int AgeAtLastPromotion__BackingField;

	private int AgeAtLastPromotionFailure__BackingField;

	private int AgeAtLastStupidBehavior__BackingField;

	private int AgeAtLastUpsetRivalFamily__BackingField;

	private int AgeAtLastBadChoice__BackingField;

	private int AgeAtLastInformantOpportunity__BackingField;

	private int AgeAtLastBecameInformant__BackingField;

	private int AgeAtLastMafiaDumb__BackingField;

	private int AgeAtLastExtortion__BackingField;

	private int AgeAtLastPromotionRequest__BackingField;

	private int AgeAtLastReportedMaterialEarnings__BackingField;

	private int AgeAtLastCaughtNotReportingFullEarnings__BackingField;

	private int AgeAtLastFailedShakedown__BackingField;

	private int AgeAtLastPunishmentFromRivalFamily__BackingField;

	private int AgeAtLastPositiveFeedbackForEarningsHaul__BackingField;

	private int AgeAtLastStingOperation__BackingField;

	private float Att_notoriety__BackingField;

	private float Att_standing__BackingField;

	public SimMafiaFamily Family
	{
		get
		{
			return Family__BackingField;
		}
		set
		{
			Family__BackingField = value;
		}
	}

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

	public int YearsCompleted
	{
		get
		{
			return YearsCompleted__BackingField;
		}
		set
		{
			YearsCompleted__BackingField = value;
		}
	}

	public double YearBeforeLastContribution
	{
		get
		{
			return YearBeforeLastContribution__BackingField;
		}
		set
		{
			YearBeforeLastContribution__BackingField = value;
		}
	}

	public double LastYearContribution
	{
		get
		{
			return LastYearContribution__BackingField;
		}
		set
		{
			LastYearContribution__BackingField = value;
		}
	}

	public double ThisYearContribution
	{
		get
		{
			return ThisYearContribution__BackingField;
		}
		set
		{
			ThisYearContribution__BackingField = value;
		}
	}

	public double TotalContribution
	{
		get
		{
			return TotalContribution__BackingField;
		}
		set
		{
			TotalContribution__BackingField = value;
		}
	}

	public double ThisYearCommission
	{
		get
		{
			return ThisYearCommission__BackingField;
		}
		set
		{
			ThisYearCommission__BackingField = value;
		}
	}

	public double LastYearIncome
	{
		get
		{
			return LastYearIncome__BackingField;
		}
		set
		{
			LastYearIncome__BackingField = value;
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

	public SimMafiaInformantCase InformantCase
	{
		get
		{
			return InformantCase__BackingField;
		}
		set
		{
			InformantCase__BackingField = value;
		}
	}

	public ArrayList TargetArray
	{
		get
		{
			return TargetArray__BackingField;
		}
		set
		{
			TargetArray__BackingField = value;
		}
	}

	public int OrdersFollowed
	{
		get
		{
			return OrdersFollowed__BackingField;
		}
		set
		{
			OrdersFollowed__BackingField = value;
		}
	}

	public int OrdersDelegated
	{
		get
		{
			return OrdersDelegated__BackingField;
		}
		set
		{
			OrdersDelegated__BackingField = value;
		}
	}

	public int BadChoices
	{
		get
		{
			return BadChoices__BackingField;
		}
		set
		{
			BadChoices__BackingField = value;
		}
	}

	public int AgeAtJoin
	{
		get
		{
			return AgeAtJoin__BackingField;
		}
		set
		{
			AgeAtJoin__BackingField = value;
		}
	}

	public int AgeAtLastPromotion
	{
		get
		{
			return AgeAtLastPromotion__BackingField;
		}
		set
		{
			AgeAtLastPromotion__BackingField = value;
		}
	}

	public int AgeAtLastPromotionFailure
	{
		get
		{
			return AgeAtLastPromotionFailure__BackingField;
		}
		set
		{
			AgeAtLastPromotionFailure__BackingField = value;
		}
	}

	public int AgeAtLastStupidBehavior
	{
		get
		{
			return AgeAtLastStupidBehavior__BackingField;
		}
		set
		{
			AgeAtLastStupidBehavior__BackingField = value;
		}
	}

	public int AgeAtLastUpsetRivalFamily
	{
		get
		{
			return AgeAtLastUpsetRivalFamily__BackingField;
		}
		set
		{
			AgeAtLastUpsetRivalFamily__BackingField = value;
		}
	}

	public int AgeAtLastBadChoice
	{
		get
		{
			return AgeAtLastBadChoice__BackingField;
		}
		set
		{
			AgeAtLastBadChoice__BackingField = value;
		}
	}

	public int AgeAtLastInformantOpportunity
	{
		get
		{
			return AgeAtLastInformantOpportunity__BackingField;
		}
		set
		{
			AgeAtLastInformantOpportunity__BackingField = value;
		}
	}

	public int AgeAtLastBecameInformant
	{
		get
		{
			return AgeAtLastBecameInformant__BackingField;
		}
		set
		{
			AgeAtLastBecameInformant__BackingField = value;
		}
	}

	public int AgeAtLastMafiaDumb
	{
		get
		{
			return AgeAtLastMafiaDumb__BackingField;
		}
		set
		{
			AgeAtLastMafiaDumb__BackingField = value;
		}
	}

	public int AgeAtLastExtortion
	{
		get
		{
			return AgeAtLastExtortion__BackingField;
		}
		set
		{
			AgeAtLastExtortion__BackingField = value;
		}
	}

	public int AgeAtLastPromotionRequest
	{
		get
		{
			return AgeAtLastPromotionRequest__BackingField;
		}
		set
		{
			AgeAtLastPromotionRequest__BackingField = value;
		}
	}

	public int AgeAtLastReportedMaterialEarnings
	{
		get
		{
			return AgeAtLastReportedMaterialEarnings__BackingField;
		}
		set
		{
			AgeAtLastReportedMaterialEarnings__BackingField = value;
		}
	}

	public int AgeAtLastCaughtNotReportingFullEarnings
	{
		get
		{
			return AgeAtLastCaughtNotReportingFullEarnings__BackingField;
		}
		set
		{
			AgeAtLastCaughtNotReportingFullEarnings__BackingField = value;
		}
	}

	public int AgeAtLastFailedShakedown
	{
		get
		{
			return AgeAtLastFailedShakedown__BackingField;
		}
		set
		{
			AgeAtLastFailedShakedown__BackingField = value;
		}
	}

	public int AgeAtLastPunishmentFromRivalFamily
	{
		get
		{
			return AgeAtLastPunishmentFromRivalFamily__BackingField;
		}
		set
		{
			AgeAtLastPunishmentFromRivalFamily__BackingField = value;
		}
	}

	public int AgeAtLastPositiveFeedbackForEarningsHaul
	{
		get
		{
			return AgeAtLastPositiveFeedbackForEarningsHaul__BackingField;
		}
		set
		{
			AgeAtLastPositiveFeedbackForEarningsHaul__BackingField = value;
		}
	}

	public int AgeAtLastStingOperation
	{
		get
		{
			return AgeAtLastStingOperation__BackingField;
		}
		set
		{
			AgeAtLastStingOperation__BackingField = value;
		}
	}

	public float Att_notoriety
	{
		get
		{
			return Att_notoriety__BackingField;
		}
		set
		{
			Att_notoriety__BackingField = value;
		}
	}

	public float Att_standing
	{
		get
		{
			return Att_standing__BackingField;
		}
		set
		{
			Att_standing__BackingField = value;
		}
	}

	public SimSpecialJobMafia(SimMafiaFamily theFamily, int theRank)
	{
	}

	public override void IncrementAge()
	{
	}

	public override string JobTitleWithArticle(bool article)
	{
		return null;
	}

	public override bool CanAttendRehab()
	{
		return false;
	}

	public float GetRandomSalaryIncome()
	{
		return 0f;
	}

	public void ContributeIncome(double income)
	{
	}

	public void AnnualAttributeAdjustment()
	{
	}

	public bool IsEligibleForPromotion()
	{
		return false;
	}

	public bool IsMadeMan()
	{
		return false;
	}

	public bool IsGodfather()
	{
		return false;
	}

	public bool IsInformant()
	{
		return false;
	}

	public string GetRankString()
	{
		return null;
	}

	public string GetDescriptiveMafiaOnlyJobTitle()
	{
		return null;
	}

	public void HeroLeavesFamily()
	{
	}

	public void SetupTargets()
	{
	}

	public SimCompany PickAcceptableExtortionTargetCompanyFromCompanyArray(ArrayList companyArray)
	{
		return null;
	}

	public void RefreshTargets()
	{
	}

	public void ImpactAllTargetFears(int impactScore)
	{
	}

	public ArrayList GetCurrentExtortionTargets()
	{
		return null;
	}

	public float GetCrimeEarningsKickBackPercent()
	{
		return 0f;
	}

	public static SimSpecialJobMafia GetCurrentSpecialJobMafia()
	{
		return null;
	}

	public static bool HeroIsCurrentlyConfidentialInformantInMafia()
	{
		return false;
	}

	public static SimOccupationSpecial GetSpecialOccupationWhereHeroBrokeMafiaOath()
	{
		return null;
	}

	public static float GetProbabilityOfDiscoveryAfterHeroBrokeMafiaOath()
	{
		return 0f;
	}

	public static ArrayList GetAllHeroSpecialOccupationsInMafia()
	{
		return null;
	}

	public static SimPerson GetHeroRelativeToKillInCountryCode(string countryCode)
	{
		return null;
	}

	public static bool HeroSharesHeritageWithMobCountryCode(string countryCode)
	{
		return false;
	}

	public void ImpactNotoriety(int impactScore)
	{
	}

	public void ImpactStanding(int impactScore)
	{
	}
}
