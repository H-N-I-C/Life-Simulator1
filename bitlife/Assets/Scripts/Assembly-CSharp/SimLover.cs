using System;
using System.Collections;

[Serializable]
public class SimLover : SimPerson, IComparable
{
	private int _heroAgeAtLastDecisionOnWhetherToAgreeToSignPreNup;

	private bool _lastDecisionOnWhetherToAgreeToSignPreNup;

	private int _heroAgeAtLastDecisionOnWhetherToMakeHeroSignPreNup;

	private bool _lastDecisionOnWhetherToMakeHeroSignPreNup;

	private int _heroAgeAtLastDateRequest;

	private bool _lastDateRequestOutcome;

	private int _heroAgeAtLastMarriageProposal;

	private bool _lastMarriageProposalOutcome;

	private int _heroAgeAtLastBabyRequest;

	private bool _lastBabyRequestOutcome;

	private int _heroAgeAtLastSpendTimeRequest;

	private bool _lastSpendTimeRequestOutcome;

	private int _heroAgeAtLastRenewWeddingVows;

	private int RelationshipStatus__BackingField;

	private int RelationshipAge__BackingField;

	private int MarriageAge__BackingField;

	private int EngagementAge__BackingField;

	private int HeroAgeWhenLastStartedDating__BackingField;

	private int HeroAgeWhenLastHookedUp__BackingField;

	private int HeroAgeWhenLastGotMarried__BackingField;

	private int HeroAgeWhenLastBrokeUp__BackingField;

	private int HeroAgeAtLastInVitroFertilization__BackingField;

	private int HeroAgeAtLastInVitroFertilizationRequest__BackingField;

	private bool LastInVitroFertilizationRequestOutcome__BackingField;

	private int HeroAgeAtLastDecisionOnWhetherToMakeHeroSignPreNup__BackingField;

	private bool LastDecisionOnWhetherToMakeHeroSignPreNup__BackingField;

	private int HeroAgeAtLastDecisionOnWhetherToAgreeToSignPreNup__BackingField;

	private bool LastDecisionOnWhetherToAgreeToSignPreNup__BackingField;

	private int TimesCheatedOnHero__BackingField;

	private int TimesHadThreesome__BackingField;

	private int TimesHeroBeggedToStay__BackingField;

	private int TimesHeroCheatedOn__BackingField;

	private int TimesHeroAbused__BackingField;

	private int TimesHeroBothers__BackingField;

	private int TimesHeroActsCrazy__BackingField;

	private int HeroAgeAtLastAssault__BackingField;

	private int HeroAgeAtLastCheatAttempt__BackingField;

	private int HeroAgeAtLastRestrainingOrderOnHero__BackingField;

	private int HeroAgeAtLastRestrainingOrderOnLover__BackingField;

	private int HeroAgeAtLastHarassment__BackingField;

	private int HeroAgeAtLastAskToStartDating__BackingField;

	private int HeroAgeAtLastWentOnBirthControl__BackingField;

	private int HeroAgeAtLastWentOffBirthControl__BackingField;

	private int HeroAgeAtLastMakeLove__BackingField;

	private int HeroAgeAtLastMakeLoveRejection__BackingField;

	private int HeroAgeAtHeroParentsDisapproval__BackingField;

	private int HeroAgeAtLastBootyCall__BackingField;

	private int HeroAgeAtLastCounseling__BackingField;

	private int HeroAgeAtLastStalk__BackingField;

	private bool DemandedWedding__BackingField;

	private bool DemandedHouse__BackingField;

	private bool DemandedCar__BackingField;

	private bool HeroChangedNameWhenMarrying__BackingField;

	private SimPrenuptialAgreement Prenup__BackingField;

	private int LastHookupWithHeroEnjoyment__BackingField;

	private bool DiedWhileHeroLover__BackingField;

	private bool IsPrude__BackingField;

	private int EngagementRingUniqueID__BackingField;

	private bool IsMailOrderBride__BackingField;

	private bool CelebratedAnniversaryThisYear__BackingField;

	private ArrayList _JewelGiftsToHeroArray__BackingField;

	private double HeroInheritedMoney__BackingField;

	private SimPerson Spouse__BackingField;

	private int AgeAtCurrentSpouseMarriage__BackingField;

	private ArrayList _ExSpouseArray__BackingField;

	private ArrayList _ExLoverArray__BackingField;

	private ArrayList _StepchildArray__BackingField;

	public int RelationshipStatus
	{
		get
		{
			return RelationshipStatus__BackingField;
		}
		set
		{
			RelationshipStatus__BackingField = value;
		}
	}

	public int RelationshipAge
	{
		get
		{
			return RelationshipAge__BackingField;
		}
		set
		{
			RelationshipAge__BackingField = value;
		}
	}

	public int MarriageAge
	{
		get
		{
			return MarriageAge__BackingField;
		}
		set
		{
			MarriageAge__BackingField = value;
		}
	}

	public int EngagementAge
	{
		get
		{
			return EngagementAge__BackingField;
		}
		set
		{
			EngagementAge__BackingField = value;
		}
	}

	public int HeroAgeWhenLastStartedDating
	{
		get
		{
			return HeroAgeWhenLastStartedDating__BackingField;
		}
		set
		{
			HeroAgeWhenLastStartedDating__BackingField = value;
		}
	}

	public int HeroAgeWhenLastHookedUp
	{
		get
		{
			return HeroAgeWhenLastHookedUp__BackingField;
		}
		set
		{
			HeroAgeWhenLastHookedUp__BackingField = value;
		}
	}

	public int HeroAgeWhenLastGotMarried
	{
		get
		{
			return HeroAgeWhenLastGotMarried__BackingField;
		}
		set
		{
			HeroAgeWhenLastGotMarried__BackingField = value;
		}
	}

	public int HeroAgeWhenLastBrokeUp
	{
		get
		{
			return HeroAgeWhenLastBrokeUp__BackingField;
		}
		set
		{
			HeroAgeWhenLastBrokeUp__BackingField = value;
		}
	}

	public int HeroAgeAtLastInVitroFertilization
	{
		get
		{
			return HeroAgeAtLastInVitroFertilization__BackingField;
		}
		set
		{
			HeroAgeAtLastInVitroFertilization__BackingField = value;
		}
	}

	public int HeroAgeAtLastInVitroFertilizationRequest
	{
		get
		{
			return HeroAgeAtLastInVitroFertilizationRequest__BackingField;
		}
		set
		{
			HeroAgeAtLastInVitroFertilizationRequest__BackingField = value;
		}
	}

	public bool LastInVitroFertilizationRequestOutcome
	{
		get
		{
			return LastInVitroFertilizationRequestOutcome__BackingField;
		}
		set
		{
			LastInVitroFertilizationRequestOutcome__BackingField = value;
		}
	}

	public int HeroAgeAtLastDecisionOnWhetherToMakeHeroSignPreNup
	{
		get
		{
			return HeroAgeAtLastDecisionOnWhetherToMakeHeroSignPreNup__BackingField;
		}
		set
		{
			HeroAgeAtLastDecisionOnWhetherToMakeHeroSignPreNup__BackingField = value;
		}
	}

	public bool LastDecisionOnWhetherToMakeHeroSignPreNup
	{
		get
		{
			return LastDecisionOnWhetherToMakeHeroSignPreNup__BackingField;
		}
		set
		{
			LastDecisionOnWhetherToMakeHeroSignPreNup__BackingField = value;
		}
	}

	public int HeroAgeAtLastDecisionOnWhetherToAgreeToSignPreNup
	{
		get
		{
			return HeroAgeAtLastDecisionOnWhetherToAgreeToSignPreNup__BackingField;
		}
		set
		{
			HeroAgeAtLastDecisionOnWhetherToAgreeToSignPreNup__BackingField = value;
		}
	}

	public bool LastDecisionOnWhetherToAgreeToSignPreNup
	{
		get
		{
			return LastDecisionOnWhetherToAgreeToSignPreNup__BackingField;
		}
		set
		{
			LastDecisionOnWhetherToAgreeToSignPreNup__BackingField = value;
		}
	}

	public int TimesCheatedOnHero
	{
		get
		{
			return TimesCheatedOnHero__BackingField;
		}
		set
		{
			TimesCheatedOnHero__BackingField = value;
		}
	}

	public int TimesHadThreesome
	{
		get
		{
			return TimesHadThreesome__BackingField;
		}
		set
		{
			TimesHadThreesome__BackingField = value;
		}
	}

	public int TimesHeroBeggedToStay
	{
		get
		{
			return TimesHeroBeggedToStay__BackingField;
		}
		set
		{
			TimesHeroBeggedToStay__BackingField = value;
		}
	}

	public int TimesHeroCheatedOn
	{
		get
		{
			return TimesHeroCheatedOn__BackingField;
		}
		set
		{
			TimesHeroCheatedOn__BackingField = value;
		}
	}

	public int TimesHeroAbused
	{
		get
		{
			return TimesHeroAbused__BackingField;
		}
		set
		{
			TimesHeroAbused__BackingField = value;
		}
	}

	public int TimesHeroBothers
	{
		get
		{
			return TimesHeroBothers__BackingField;
		}
		set
		{
			TimesHeroBothers__BackingField = value;
		}
	}

	public int TimesHeroActsCrazy
	{
		get
		{
			return TimesHeroActsCrazy__BackingField;
		}
		set
		{
			TimesHeroActsCrazy__BackingField = value;
		}
	}

	public int HeroAgeAtLastAssault
	{
		get
		{
			return HeroAgeAtLastAssault__BackingField;
		}
		set
		{
			HeroAgeAtLastAssault__BackingField = value;
		}
	}

	public int HeroAgeAtLastCheatAttempt
	{
		get
		{
			return HeroAgeAtLastCheatAttempt__BackingField;
		}
		set
		{
			HeroAgeAtLastCheatAttempt__BackingField = value;
		}
	}

	public int HeroAgeAtLastRestrainingOrderOnHero
	{
		get
		{
			return HeroAgeAtLastRestrainingOrderOnHero__BackingField;
		}
		set
		{
			HeroAgeAtLastRestrainingOrderOnHero__BackingField = value;
		}
	}

	public int HeroAgeAtLastRestrainingOrderOnLover
	{
		get
		{
			return HeroAgeAtLastRestrainingOrderOnLover__BackingField;
		}
		set
		{
			HeroAgeAtLastRestrainingOrderOnLover__BackingField = value;
		}
	}

	public int HeroAgeAtLastHarassment
	{
		get
		{
			return HeroAgeAtLastHarassment__BackingField;
		}
		set
		{
			HeroAgeAtLastHarassment__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskToStartDating
	{
		get
		{
			return HeroAgeAtLastAskToStartDating__BackingField;
		}
		set
		{
			HeroAgeAtLastAskToStartDating__BackingField = value;
		}
	}

	public int HeroAgeAtLastWentOnBirthControl
	{
		get
		{
			return HeroAgeAtLastWentOnBirthControl__BackingField;
		}
		set
		{
			HeroAgeAtLastWentOnBirthControl__BackingField = value;
		}
	}

	public int HeroAgeAtLastWentOffBirthControl
	{
		get
		{
			return HeroAgeAtLastWentOffBirthControl__BackingField;
		}
		set
		{
			HeroAgeAtLastWentOffBirthControl__BackingField = value;
		}
	}

	public int HeroAgeAtLastMakeLove
	{
		get
		{
			return HeroAgeAtLastMakeLove__BackingField;
		}
		set
		{
			HeroAgeAtLastMakeLove__BackingField = value;
		}
	}

	public int HeroAgeAtLastMakeLoveRejection
	{
		get
		{
			return HeroAgeAtLastMakeLoveRejection__BackingField;
		}
		set
		{
			HeroAgeAtLastMakeLoveRejection__BackingField = value;
		}
	}

	public int HeroAgeAtHeroParentsDisapproval
	{
		get
		{
			return HeroAgeAtHeroParentsDisapproval__BackingField;
		}
		set
		{
			HeroAgeAtHeroParentsDisapproval__BackingField = value;
		}
	}

	public int HeroAgeAtLastBootyCall
	{
		get
		{
			return HeroAgeAtLastBootyCall__BackingField;
		}
		set
		{
			HeroAgeAtLastBootyCall__BackingField = value;
		}
	}

	public int HeroAgeAtLastCounseling
	{
		get
		{
			return HeroAgeAtLastCounseling__BackingField;
		}
		set
		{
			HeroAgeAtLastCounseling__BackingField = value;
		}
	}

	public int HeroAgeAtLastStalk
	{
		get
		{
			return HeroAgeAtLastStalk__BackingField;
		}
		set
		{
			HeroAgeAtLastStalk__BackingField = value;
		}
	}

	public bool DemandedWedding
	{
		get
		{
			return DemandedWedding__BackingField;
		}
		set
		{
			DemandedWedding__BackingField = value;
		}
	}

	public bool DemandedHouse
	{
		get
		{
			return DemandedHouse__BackingField;
		}
		set
		{
			DemandedHouse__BackingField = value;
		}
	}

	public bool DemandedCar
	{
		get
		{
			return DemandedCar__BackingField;
		}
		set
		{
			DemandedCar__BackingField = value;
		}
	}

	public bool HeroChangedNameWhenMarrying
	{
		get
		{
			return HeroChangedNameWhenMarrying__BackingField;
		}
		set
		{
			HeroChangedNameWhenMarrying__BackingField = value;
		}
	}

	public SimPrenuptialAgreement Prenup
	{
		get
		{
			return Prenup__BackingField;
		}
		set
		{
			Prenup__BackingField = value;
		}
	}

	public int LastHookupWithHeroEnjoyment
	{
		get
		{
			return LastHookupWithHeroEnjoyment__BackingField;
		}
		set
		{
			LastHookupWithHeroEnjoyment__BackingField = value;
		}
	}

	public bool DiedWhileHeroLover
	{
		get
		{
			return DiedWhileHeroLover__BackingField;
		}
		set
		{
			DiedWhileHeroLover__BackingField = value;
		}
	}

	public bool IsPrude
	{
		get
		{
			return IsPrude__BackingField;
		}
		set
		{
			IsPrude__BackingField = value;
		}
	}

	public int EngagementRingUniqueID
	{
		get
		{
			return EngagementRingUniqueID__BackingField;
		}
		set
		{
			EngagementRingUniqueID__BackingField = value;
		}
	}

	public bool IsMailOrderBride
	{
		get
		{
			return IsMailOrderBride__BackingField;
		}
		set
		{
			IsMailOrderBride__BackingField = value;
		}
	}

	public bool CelebratedAnniversaryThisYear
	{
		get
		{
			return CelebratedAnniversaryThisYear__BackingField;
		}
		set
		{
			CelebratedAnniversaryThisYear__BackingField = value;
		}
	}

	private ArrayList _JewelGiftsToHeroArray
	{
		get
		{
			return _JewelGiftsToHeroArray__BackingField;
		}
		set
		{
			_JewelGiftsToHeroArray__BackingField = value;
		}
	}

	public ArrayList JewelGiftsToHeroArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public double HeroInheritedMoney
	{
		get
		{
			return HeroInheritedMoney__BackingField;
		}
		set
		{
			HeroInheritedMoney__BackingField = value;
		}
	}

	public SimPerson Spouse
	{
		get
		{
			return Spouse__BackingField;
		}
		set
		{
			Spouse__BackingField = value;
		}
	}

	public int AgeAtCurrentSpouseMarriage
	{
		get
		{
			return AgeAtCurrentSpouseMarriage__BackingField;
		}
		set
		{
			AgeAtCurrentSpouseMarriage__BackingField = value;
		}
	}

	private ArrayList _ExSpouseArray
	{
		get
		{
			return _ExSpouseArray__BackingField;
		}
		set
		{
			_ExSpouseArray__BackingField = value;
		}
	}

	public ArrayList ExSpouseArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _ExLoverArray
	{
		get
		{
			return _ExLoverArray__BackingField;
		}
		set
		{
			_ExLoverArray__BackingField = value;
		}
	}

	public ArrayList ExLoverArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _StepchildArray
	{
		get
		{
			return _StepchildArray__BackingField;
		}
		set
		{
			_StepchildArray__BackingField = value;
		}
	}

	public ArrayList StepchildArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private SimLover()
	{
	}

	public SimLover(bool initHere)
	{
	}

	public SimLover(SimPerson otherPerson)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void Die()
	{
	}

	public override bool ShouldSufferMiscarriage()
	{
		return false;
	}

	public override float AnnualCostOfLiving()
	{
		return 0f;
	}

	public override float AskForMoneyAmount()
	{
		return 0f;
	}

	public override bool WillAgreeToSpendTime()
	{
		return false;
	}

	public override string GetRelationshipString()
	{
		return null;
	}

	public string RelationshipStatusString()
	{
		return null;
	}

	private bool IsStaleExLover()
	{
		return false;
	}

	public string ExRelationshipStatusString()
	{
		return null;
	}

	public string LowercaseRelationshipString()
	{
		return null;
	}

	public string LowercaseRelationshipStringAndName()
	{
		return null;
	}

	public string HyphenatedLastNameForMarriage()
	{
		return null;
	}

	public int YearsSinceLastWithHero()
	{
		return 0;
	}

	public bool ActiveRestrainingOrderOnHero()
	{
		return false;
	}

	public bool ActiveRestrainingOrderOnLover()
	{
		return false;
	}

	public ArrayList GetAllChildren()
	{
		return null;
	}

	public bool AtLeastOneAliveStepchild()
	{
		return false;
	}

	public bool WillHeroProposalInvolveRing()
	{
		return false;
	}

	public bool WillProposalToHeroInvolveRing()
	{
		return false;
	}

	public bool WillHaveGreatSexLife()
	{
		return false;
	}

	public bool WillHaveTerribleSexLife()
	{
		return false;
	}

	public SimHouse GetDesiredHouseForSale()
	{
		return null;
	}

	public SimCar GetDesiredCarForSale()
	{
		return null;
	}

	public bool WillAcceptDateWithHero()
	{
		return false;
	}

	public bool WillAcceptMarriageProposal()
	{
		return false;
	}

	public bool WillAgreeToGetEloped()
	{
		return false;
	}

	public bool WillAgreeToHaveBaby()
	{
		return false;
	}

	public bool WillAgreeToMakeLove()
	{
		return false;
	}

	public bool WillAgreeToGoOnBirthControl()
	{
		return false;
	}

	public bool WillAgreeToGoOffBirthControl()
	{
		return false;
	}

	public bool WillAgreeToRenewWeddingVows()
	{
		return false;
	}

	public bool WillAgreeToThreesome()
	{
		return false;
	}

	public bool WillAgreeToChangeNameAfterMarriage()
	{
		return false;
	}

	public bool WillAgreeToHireASurrogate()
	{
		return false;
	}

	public bool WillAgreeToSignPrenup()
	{
		return false;
	}

	public bool WillInsistOnPrenup()
	{
		return false;
	}

	public bool IsHoldingOutForMarriageToMakeLove()
	{
		return false;
	}

	public bool WillAgreeToBootyCall()
	{
		return false;
	}

	public bool WillAgreeToDivorceSpouse()
	{
		return false;
	}

	public bool WillGetBackTogetherWithHero()
	{
		return false;
	}

	protected internal virtual bool WillHookupWithHero()
	{
		return false;
	}

	public override float ThirdPersonSuccessPropensityScore()
	{
		return 0f;
	}

	public override void ThirdPersonEarnsIncome(float income)
	{
	}

	public override void ThirdPersonMarries()
	{
	}

	public override void ThirdPersonChangesNameAfterMarriage()
	{
	}

	public override void ThirdPersonDivorces()
	{
	}

	public override void ThirdPersonChangesNameAfterDivorce()
	{
	}

	public override bool ThirdPersonTriesForBaby()
	{
		return false;
	}

	public override void ThirdPersonHasHerosUnknownBaby()
	{
	}

	public override string ThirdPersonLogIdentityStringWithStartOfYearName(SimPersonName cachedName)
	{
		return null;
	}

	public override string ThirdPersonSpouseIdentityStringWithDescription(bool includeDescription)
	{
		return null;
	}

	public int CompareTo(object obj)
	{
		return 0;
	}
}
