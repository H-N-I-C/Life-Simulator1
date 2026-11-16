using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimVampire
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Predicate<SimVampireProgeny> _003C_003E9__183_0;

		internal bool _003CRemoveDeadProgeny_003Eb__183_0(SimVampireProgeny progeny)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass167_0
	{
		public SimPerson person;

		internal bool _003CBloodSuckedFromThisYearCount_003Eb__0(SimPerson x)
		{
			return false;
		}
	}

	public const int MAX_PROGENY = 50;

	private VAMPIRE_BLOOD_CURSE BloodCurse__BackingField;

	private VAMPIRE_BLOOD_CURSE SourceBloodCurse__BackingField;

	private VAMPIRE_POWER VampiricPower__BackingField;

	private string AnimalForm__BackingField;

	private int InfectedAge__BackingField;

	private SimVampireGoals Goals__BackingField;

	private VAMPIRE_BLOOD_RESTRICTION BloodRestriction__BackingField;

	private string BloodRestrictionType__BackingField;

	private VAMPIRE_FAVORITE_BLOOD FavoriteBlood__BackingField;

	private int HumansConverted__BackingField;

	private int KillCount__BackingField;

	private int AgeLastKilledPerson__BackingField;

	private int YearsSinceLastBloodDrink__BackingField;

	private SimVampireHonorific Honorific__BackingField;

	private int NumTimesAttemptedToRobBloodBankThisYear__BackingField;

	private int SacrificesThisYear__BackingField;

	private int HuntsThisYear__BackingField;

	private bool AcquiredFamiliarThisYear__BackingField;

	private int TimesRestedInCoffinThisYear__BackingField;

	private int NumTimesPerformedCrimsonAuditThisYear__BackingField;

	private int NumTimesPerformedVampireCodeReviewThisYear__BackingField;

	private bool IsMemberOfVampireCouncil__BackingField;

	private int NumPrisonersConverted__BackingField;

	private List<SimPerson> _personsSuckedThisYear;

	private int NumDrainedToDeath__BackingField;

	private List<string> _livingPersonsDrained;

	private List<SimVampireProgeny> _vampireProgeny;

	private float _notoriety;

	private float _essence;

	private bool _heroIsSourceVampire;

	private SimPerson _vampireThatInfectedHero;

	private List<SimVampireHunter> _huntersEncountered;

	private SimVampireFamiliar _familiar;

	private List<SimVampireCouncilMember> _vampireCouncilMembers;

	private float _standing;

	private bool IsCouncilVoteBanned__BackingField;

	public VAMPIRE_BLOOD_CURSE BloodCurse
	{
		get
		{
			return BloodCurse__BackingField;
		}
		set
		{
			BloodCurse__BackingField = value;
		}
	}

	public VAMPIRE_BLOOD_CURSE SourceBloodCurse
	{
		get
		{
			return SourceBloodCurse__BackingField;
		}
		set
		{
			SourceBloodCurse__BackingField = value;
		}
	}

	public VAMPIRE_POWER VampiricPower
	{
		get
		{
			return VampiricPower__BackingField;
		}
		set
		{
			VampiricPower__BackingField = value;
		}
	}

	public string AnimalForm
	{
		get
		{
			return AnimalForm__BackingField;
		}
		set
		{
			AnimalForm__BackingField = value;
		}
	}

	public int InfectedAge
	{
		get
		{
			return InfectedAge__BackingField;
		}
		set
		{
			InfectedAge__BackingField = value;
		}
	}

	public SimVampireGoals Goals
	{
		get
		{
			return Goals__BackingField;
		}
		set
		{
			Goals__BackingField = value;
		}
	}

	public VAMPIRE_BLOOD_RESTRICTION BloodRestriction
	{
		get
		{
			return BloodRestriction__BackingField;
		}
		set
		{
			BloodRestriction__BackingField = value;
		}
	}

	public string BloodRestrictionType
	{
		get
		{
			return BloodRestrictionType__BackingField;
		}
		set
		{
			BloodRestrictionType__BackingField = value;
		}
	}

	public VAMPIRE_FAVORITE_BLOOD FavoriteBlood
	{
		get
		{
			return FavoriteBlood__BackingField;
		}
		set
		{
			FavoriteBlood__BackingField = value;
		}
	}

	public int HumansConverted
	{
		get
		{
			return HumansConverted__BackingField;
		}
		set
		{
			HumansConverted__BackingField = value;
		}
	}

	public int KillCount
	{
		get
		{
			return KillCount__BackingField;
		}
		set
		{
			KillCount__BackingField = value;
		}
	}

	public int AgeLastKilledPerson
	{
		get
		{
			return AgeLastKilledPerson__BackingField;
		}
		set
		{
			AgeLastKilledPerson__BackingField = value;
		}
	}

	public int YearsSinceLastBloodDrink
	{
		get
		{
			return YearsSinceLastBloodDrink__BackingField;
		}
		private set
		{
			YearsSinceLastBloodDrink__BackingField = value;
		}
	}

	public SimVampireHonorific Honorific
	{
		get
		{
			return Honorific__BackingField;
		}
		set
		{
			Honorific__BackingField = value;
		}
	}

	public bool IsWithered => false;

	public int NumTimesAttemptedToRobBloodBankThisYear
	{
		get
		{
			return NumTimesAttemptedToRobBloodBankThisYear__BackingField;
		}
		set
		{
			NumTimesAttemptedToRobBloodBankThisYear__BackingField = value;
		}
	}

	public int SacrificesThisYear
	{
		get
		{
			return SacrificesThisYear__BackingField;
		}
		set
		{
			SacrificesThisYear__BackingField = value;
		}
	}

	public int HuntsThisYear
	{
		get
		{
			return HuntsThisYear__BackingField;
		}
		set
		{
			HuntsThisYear__BackingField = value;
		}
	}

	public bool AcquiredFamiliarThisYear
	{
		get
		{
			return AcquiredFamiliarThisYear__BackingField;
		}
		set
		{
			AcquiredFamiliarThisYear__BackingField = value;
		}
	}

	public int TimesRestedInCoffinThisYear
	{
		get
		{
			return TimesRestedInCoffinThisYear__BackingField;
		}
		set
		{
			TimesRestedInCoffinThisYear__BackingField = value;
		}
	}

	public int NumTimesPerformedCrimsonAuditThisYear
	{
		get
		{
			return NumTimesPerformedCrimsonAuditThisYear__BackingField;
		}
		set
		{
			NumTimesPerformedCrimsonAuditThisYear__BackingField = value;
		}
	}

	public int NumTimesPerformedVampireCodeReviewThisYear
	{
		get
		{
			return NumTimesPerformedVampireCodeReviewThisYear__BackingField;
		}
		set
		{
			NumTimesPerformedVampireCodeReviewThisYear__BackingField = value;
		}
	}

	public bool IsMemberOfVampireCouncil
	{
		get
		{
			return IsMemberOfVampireCouncil__BackingField;
		}
		set
		{
			IsMemberOfVampireCouncil__BackingField = value;
		}
	}

	public int NumPrisonersConverted
	{
		get
		{
			return NumPrisonersConverted__BackingField;
		}
		set
		{
			NumPrisonersConverted__BackingField = value;
		}
	}

	public List<SimPerson> BloodSuckedFromThisYear
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int NumDrainedToDeath
	{
		get
		{
			return NumDrainedToDeath__BackingField;
		}
		set
		{
			NumDrainedToDeath__BackingField = value;
		}
	}

	private List<string> LivingPersonsDrained
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimVampireProgeny> SortedVampireProgeny
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimVampireHunter> HuntersEncountered => null;

	public float Att_essence
	{
		get
		{
			return 0f;
		}
		set
		{
		}
	}

	public bool IsCouncilVoteBanned
	{
		get
		{
			return IsCouncilVoteBanned__BackingField;
		}
		set
		{
			IsCouncilVoteBanned__BackingField = value;
		}
	}

	static SimVampire()
	{
	}

	public SimVampire(int infectedAge, SimPerson sourceVampire)
	{
	}

	public void RefreshBloodCurseDetails()
	{
	}

	private string GetRandomBloodRestrictionType(VAMPIRE_BLOOD_RESTRICTION bloodRestriction)
	{
		return null;
	}

	public string GetHonorificString(SimPerson person)
	{
		return null;
	}

	public void IncrementAge(bool isHero, float craziness)
	{
	}

	public void OnLifeIncrementAgeEnded()
	{
	}

	public SimPerson GetSourceVampireOrNull()
	{
		return null;
	}

	public string BloodCurseString()
	{
		return null;
	}

	public string GetBloodCurseEffectDescription()
	{
		return null;
	}

	public string VampiricPowerString()
	{
		return null;
	}

	public string VampiricPowerEffectString()
	{
		return null;
	}

	public float GetNotoriety()
	{
		return 0f;
	}

	public void ImpactNotoriety(int impactScore)
	{
	}

	public void AdjustNotoriety(float adjustmentValue)
	{
	}

	public float GetEssence()
	{
		return 0f;
	}

	public void ImpactEssence(int impactScore)
	{
	}

	public void AdjustEssence(float adjustmentValue)
	{
	}

	public float GetEssenceNeededToConvertPerson()
	{
		return 0f;
	}

	public float GetEssenceGainPerSuckOnRandomPerson()
	{
		return 0f;
	}

	public float GetEssenceGainPerSuck(SimPerson person, bool willKill = false)
	{
		return 0f;
	}

	public float CalculateEssenceAmountOnRandomPerson(float essenceGain)
	{
		return 0f;
	}

	public float CalculateEssenceAmountBasedOnCurse(SimPerson person, float essenceGain, bool willKill)
	{
		return 0f;
	}

	public static SimPerson CreateRandomPerson()
	{
		return null;
	}

	private bool IsPersonBloodRestricted(SimPerson person)
	{
		return false;
	}

	private static string BloodRestrictionTypeByGender(GENDER gender)
	{
		return null;
	}

	private static string BloodRestrictionTypeBySexuality(SEXUALITY sexuality)
	{
		return null;
	}

	private float GetFavoriteBloodAttribute(SimPerson person)
	{
		return 0f;
	}

	public bool HasEnoughEssenceToConvertPerson()
	{
		return false;
	}

	public bool HasEnoughEssenceToUsePower()
	{
		return false;
	}

	public float GetEssenceNeededToUsePower()
	{
		return 0f;
	}

	public void AddVampireHunterEncountered(SimVampireHunter hunter)
	{
	}

	public SimVampireHunter GetVampireHunter(bool mustBeNew = false)
	{
		return null;
	}

	public bool HasEncounteredHunterBefore(SimVampireHunter hunter)
	{
		return false;
	}

	public void AssignFamiliar(SimVampireFamiliar familiar)
	{
	}

	public void ReleaseFamiliar()
	{
	}

	public SimVampireFamiliar GetFamiliarOrNull()
	{
		return null;
	}

	public void RecordKill(SimPerson vampire, SimPerson victim)
	{
	}

	public void DrainHealthFromPerson(SimPerson person, float amount)
	{
	}

	public int BloodSuckedFromThisYearCount(SimPerson person)
	{
		return 0;
	}

	public float GetStanding()
	{
		return 0f;
	}

	public void ImpactStanding(int impactScore)
	{
	}

	public void UpdateVampireCouncilMembers(bool clear = false)
	{
	}

	public List<SimVampireCouncilMember> GetVampireCouncilMembers()
	{
		return null;
	}

	public SimVampireCouncilMember GetRandomVampireCouncilMember()
	{
		return null;
	}

	public void JoinVampireCouncil()
	{
	}

	private int GetRequiredNumberOfVampireCouncilMembers()
	{
		return 0;
	}

	private int GetBaseEssenceGainPerSuck()
	{
		return 0;
	}

	private int GetBaseEssenceCostToConvertPerson()
	{
		return 0;
	}

	private int GetBaseEssenceCostToUsePower()
	{
		return 0;
	}

	public bool DidInheritBloodCurseFrom(SimPerson person)
	{
		return false;
	}

	private void SetSourceBloodCurse(VAMPIRE_BLOOD_CURSE sourceBloodCurse)
	{
	}

	private static VAMPIRE_BLOOD_CURSE RandomBloodCurse()
	{
		return VAMPIRE_BLOOD_CURSE.NONE;
	}

	private float MultiplyEssenceByCurse(float essenceNeeded)
	{
		return 0f;
	}

	public void UpdateProgenyWith(SimPerson person)
	{
	}

	public void RemoveDeadProgeny()
	{
	}

	public Scenario GetImmunityFeedback(string deathPenaltyMethod)
	{
		return null;
	}

	public static int GetVampireMinAge()
	{
		return 0;
	}

	public static int GetVampireMaxAge()
	{
		return 0;
	}

	public static int GetOddsOfNPCBecomingVampire()
	{
		return 0;
	}

	public static bool DoesUserHaveExpansionPack()
	{
		return false;
	}

	public static int GetHealthDrainPerSuck()
	{
		return 0;
	}

	public static int GetOddsOfPersonBeingCarrierForVampireDisease()
	{
		return 0;
	}

	public static int GetOddsOfContractingVampireDiseaseFromCarrier()
	{
		return 0;
	}

	public static int GetHeroEssenceLostPerYear()
	{
		return 0;
	}

	public static int GetOddsOfFailingInScapeFromHunterInAnimalForm()
	{
		return 0;
	}

	public static int GetRandomVampireInfectionAge(int minAge)
	{
		return 0;
	}

	public static void InjectVampireStatsIntoScenario(Scenario scenario, SimPerson person)
	{
	}

	private static bool DidEitherInfectTheOther(SimPerson left, SimPerson right)
	{
		return false;
	}

	private static void ReplaceHealthBarWithEssenceBar(ArrayList barGraphArray, float essence)
	{
	}

	public static DEATH_TYPE GetRandomVampireDeathType()
	{
		return DEATH_TYPE.NATURAL;
	}

	public static void AssignNameToVampire(SimPerson vampire)
	{
	}

	public static bool IsCutOffDateForHouseList()
	{
		return false;
	}

	public static List<SimPerson> GetPeopleToSacrifice()
	{
		return null;
	}

	public static void PersonAttributeImpacted(SimPerson person)
	{
	}

	public static int GetRandomVampireAttackImpact()
	{
		return 0;
	}

	public static int GetVampireCouncilJoinEssenceCost()
	{
		return 0;
	}

	public static int GetVampireCouncilJoinMinimumFame()
	{
		return 0;
	}

	public void BecomeHero(SimPerson oldHero)
	{
	}

	public void BecomeThirdPerson(SimPerson oldHero)
	{
	}

	private void UpdateMaker(SimPerson oldHero)
	{
	}

	public static int GetOddsOfHunterAppearAfterKillingVampire()
	{
		return 0;
	}

	public static int GetOddsOfVampireAppearAfterKillingVampire()
	{
		return 0;
	}

	public void CheckAchievementBloodBath(SimPerson person, bool fullyDrain)
	{
	}

	public static void OnKillCheckAchievementByOccupation(string occupation)
	{
	}

	public void RecordDrinkBlood()
	{
	}

	public static void ConfirmationBeforeConversion(ScenarioChoice convertButton)
	{
	}
}
