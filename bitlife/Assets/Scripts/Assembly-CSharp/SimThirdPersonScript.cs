using System;
using System.Collections;
using Game;

[Serializable]
public class SimThirdPersonScript
{
	[NonSerialized]
	public SimPerson TheDelegate;

	private SimDeath Death__BackingField;

	private ArrayList _DestinyEventArray__BackingField;

	private int UndergraduateMajor__BackingField;

	private int PostUniversitySchoolType__BackingField;

	private int OccupationType__BackingField;

	private int SchoolType__BackingField;

	private int HighestCompletedSchoolType__BackingField;

	private string CareerName__BackingField;

	private SimJob Job__BackingField;

	private SimCompany Company__BackingField;

	private int AgeAtLastJobStart__BackingField;

	private int AgeAtLastPromotion__BackingField;

	private int MilitaryBranch__BackingField;

	private int MilitaryStatus__BackingField;

	private int MilitaryRank__BackingField;

	private int AgeAtMilitaryStart__BackingField;

	private int MilitaryPromotions__BackingField;

	private string SpecialJobTitle__BackingField;

	private float SpecialJobSalary__BackingField;

	private int AgeAtRetirement__BackingField;

	private float Pension__BackingField;

	private int AgeAtLastPressuredToChangeJobs__BackingField;

	private ArrayList _DiseaseArray__BackingField;

	private SimDisease LastCuredDisease__BackingField;

	private bool Royal__BackingField;

	private int RoyalRank__BackingField;

	private string RoyalSeat__BackingField;

	private bool RoyalByBirth__BackingField;

	private bool RoyalDowager__BackingField;

	private int AgeAtLastRoyalRankChange__BackingField;

	private string LifeId__BackingField;

	private SimPersonName CachedName__BackingField;

	private SimThirdPersonScript CachedScript__BackingField;

	public SimDeath Death
	{
		get
		{
			return Death__BackingField;
		}
		set
		{
			Death__BackingField = value;
		}
	}

	private ArrayList _DestinyEventArray
	{
		get
		{
			return _DestinyEventArray__BackingField;
		}
		set
		{
			_DestinyEventArray__BackingField = value;
		}
	}

	public ArrayList DestinyEventArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int UndergraduateMajor
	{
		get
		{
			return UndergraduateMajor__BackingField;
		}
		set
		{
			UndergraduateMajor__BackingField = value;
		}
	}

	public int PostUniversitySchoolType
	{
		get
		{
			return PostUniversitySchoolType__BackingField;
		}
		set
		{
			PostUniversitySchoolType__BackingField = value;
		}
	}

	public int OccupationType
	{
		get
		{
			return OccupationType__BackingField;
		}
		set
		{
			OccupationType__BackingField = value;
		}
	}

	public int SchoolType
	{
		get
		{
			return SchoolType__BackingField;
		}
		set
		{
			SchoolType__BackingField = value;
		}
	}

	public int HighestCompletedSchoolType
	{
		get
		{
			return HighestCompletedSchoolType__BackingField;
		}
		set
		{
			HighestCompletedSchoolType__BackingField = value;
		}
	}

	public string CareerName
	{
		get
		{
			return CareerName__BackingField;
		}
		set
		{
			CareerName__BackingField = value;
		}
	}

	public DataCareer Career
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimJob Job
	{
		get
		{
			return Job__BackingField;
		}
		set
		{
			Job__BackingField = value;
		}
	}

	public SimCompany Company
	{
		get
		{
			return Company__BackingField;
		}
		set
		{
			Company__BackingField = value;
		}
	}

	public int AgeAtLastJobStart
	{
		get
		{
			return AgeAtLastJobStart__BackingField;
		}
		set
		{
			AgeAtLastJobStart__BackingField = value;
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

	public int MilitaryBranch
	{
		get
		{
			return MilitaryBranch__BackingField;
		}
		set
		{
			MilitaryBranch__BackingField = value;
		}
	}

	public int MilitaryStatus
	{
		get
		{
			return MilitaryStatus__BackingField;
		}
		set
		{
			MilitaryStatus__BackingField = value;
		}
	}

	public int MilitaryRank
	{
		get
		{
			return MilitaryRank__BackingField;
		}
		set
		{
			MilitaryRank__BackingField = value;
		}
	}

	public int AgeAtMilitaryStart
	{
		get
		{
			return AgeAtMilitaryStart__BackingField;
		}
		set
		{
			AgeAtMilitaryStart__BackingField = value;
		}
	}

	public int MilitaryPromotions
	{
		get
		{
			return MilitaryPromotions__BackingField;
		}
		set
		{
			MilitaryPromotions__BackingField = value;
		}
	}

	public string SpecialJobTitle
	{
		get
		{
			return SpecialJobTitle__BackingField;
		}
		set
		{
			SpecialJobTitle__BackingField = value;
		}
	}

	public float SpecialJobSalary
	{
		get
		{
			return SpecialJobSalary__BackingField;
		}
		set
		{
			SpecialJobSalary__BackingField = value;
		}
	}

	public int AgeAtRetirement
	{
		get
		{
			return AgeAtRetirement__BackingField;
		}
		set
		{
			AgeAtRetirement__BackingField = value;
		}
	}

	public float Pension
	{
		get
		{
			return Pension__BackingField;
		}
		set
		{
			Pension__BackingField = value;
		}
	}

	public int AgeAtLastPressuredToChangeJobs
	{
		get
		{
			return AgeAtLastPressuredToChangeJobs__BackingField;
		}
		set
		{
			AgeAtLastPressuredToChangeJobs__BackingField = value;
		}
	}

	private ArrayList _DiseaseArray
	{
		get
		{
			return _DiseaseArray__BackingField;
		}
		set
		{
			_DiseaseArray__BackingField = value;
		}
	}

	public ArrayList DiseaseArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimDisease LastCuredDisease
	{
		get
		{
			return LastCuredDisease__BackingField;
		}
		set
		{
			LastCuredDisease__BackingField = value;
		}
	}

	public bool Royal
	{
		get
		{
			return Royal__BackingField;
		}
		set
		{
			Royal__BackingField = value;
		}
	}

	public int RoyalRank
	{
		get
		{
			return RoyalRank__BackingField;
		}
		set
		{
			RoyalRank__BackingField = value;
		}
	}

	public string RoyalSeat
	{
		get
		{
			return RoyalSeat__BackingField;
		}
		set
		{
			RoyalSeat__BackingField = value;
		}
	}

	public bool RoyalByBirth
	{
		get
		{
			return RoyalByBirth__BackingField;
		}
		set
		{
			RoyalByBirth__BackingField = value;
		}
	}

	public bool RoyalDowager
	{
		get
		{
			return RoyalDowager__BackingField;
		}
		set
		{
			RoyalDowager__BackingField = value;
		}
	}

	public int AgeAtLastRoyalRankChange
	{
		get
		{
			return AgeAtLastRoyalRankChange__BackingField;
		}
		set
		{
			AgeAtLastRoyalRankChange__BackingField = value;
		}
	}

	public string LifeId
	{
		get
		{
			return LifeId__BackingField;
		}
		set
		{
			LifeId__BackingField = value;
		}
	}

	public SimPersonName CachedName
	{
		get
		{
			return CachedName__BackingField;
		}
		set
		{
			CachedName__BackingField = value;
		}
	}

	public SimThirdPersonScript CachedScript
	{
		get
		{
			return CachedScript__BackingField;
		}
		set
		{
			CachedScript__BackingField = value;
		}
	}

	public void SetupForPerson(SimPerson person)
	{
	}

	public void SetupForMotherWithFatherType(SimPerson mother, int fatherType)
	{
	}

	public void SetupForFatherWithFatherType(SimParent father, int fatherType)
	{
	}

	public void SetupForChild(SimChild child)
	{
	}

	public void SetupForSibling(SimSibling sibling)
	{
	}

	public void SetupForLoverAndMustBeSingle(SimLover lover, bool single)
	{
	}

	public void SetupForMailOrderBrideLover(SimLover lover)
	{
	}

	public void SetupForClassmateWhoNeedsToBeInSchoolTypeAndHaveStartedAtAge(SimClassmate classmate, int theSchoolType, int theAge)
	{
	}

	public void SetupForClassmateLoverWhoNeedsToBeInSchoolTypeAndHaveStartedAtAge(SimLover lover, int theSchoolType, int theAge)
	{
	}

	public void SetupForCelebrityLover(SimCelebrityLover lover)
	{
	}

	public void SetupForTeacherLoverOfTeacherTypeInSchoolTypeWithHeroMajorAndMinimumYearsInPosition(SimLover lover, int theTeacherType, int theSchoolType, int theHeroMajor, int theMinYears)
	{
	}

	public void SetupForCoworkerInCareerAtCompany(SimCoworker coworker, DataCareer career, SimCompany company)
	{
	}

	public void SetupForCoworkerInMilitaryBranchAndStatusAndMinimumRank(SimCoworker coworker, int militaryBranch, int militaryStatus, int minimumRank)
	{
	}

	public void SetupForParentWhoWasPreviouslyHeroInLife(Life oldLife)
	{
	}

	public void SetupForRoyalPersonWithRoyalRankAndRoyalSeatAndRoyalByBirth(SimPerson person, int theRoyalRank, string theRoyalSeat, bool theRoyalByBirth)
	{
	}

	public void SetupForBandmateWithRoleAndGenre(SimBandmate bandmate, int role, int genre)
	{
	}

	public void SetupForMobster(SimMobster mobster)
	{
	}

	public void SetupForAgent(SimAgent agent)
	{
	}

	public void SetupForCostar(SimCostar costar)
	{
	}

	public void SetupForFinancialAdvisor(SimFinancialAdvisor advisor)
	{
	}

	public void SetupForZooEmployee(SimZooEmployee employee)
	{
	}

	public void SetupForDealerCartelMember(SimDealerCartelMember member)
	{
	}

	public void SetupForCasinoGambler(SimCasinoGambler gambler)
	{
	}

	public void SetupForCasinoCelebrity(SimCasinoCelebrity celebrity)
	{
	}

	public void SetupForRaceCarDriver(SimRaceCarDriver driver)
	{
	}

	public void SetupForAstronaut(SimAstronaut astronaut)
	{
	}

	public void SetupForCommunard(SimCommunard communard)
	{
	}

	public void SetupForModel(SimModel model, float agencyProfile)
	{
	}

	public void SetupForModelBookingAgent(SimBookingAgent agent, float agencyProfile)
	{
	}

	public void SetupForSecretAgent(SimSecretAgent agent)
	{
	}

	public void SetupForTenant(SimTenant tenant)
	{
	}

	public void SetupForHighIncomeTenant(DataLandlordTenantEmploymentCheck data, SimTenant tenant)
	{
	}

	public void SetupForVampireHunter(SimVampireHunter hunter)
	{
	}

	public void SetupForSpecialJobTitle(string specialJobTitle)
	{
	}

	public void SetupComprehensiveDestinyForPersonAndIncludeMovingOut(SimPerson person, bool includeMovingOut)
	{
	}

	public void SetupDiseaseDestinyStartingAtAge(int age)
	{
	}

	public void SetupVampireDestinyWithCurrentAge(SimPerson person, bool allowBecomingVampireAtCurrentAge = true)
	{
	}

	public void SetupSchoolAndJobDestinyWithExistingAge(int age)
	{
	}

	public void SetupAccessoryDestiny()
	{
	}

	public void SetupMotherLoveLifeWithFatherTypeAndAge(int fatherType, int age)
	{
	}

	public void SetupFatherLoveLifeWithAge(int age)
	{
	}

	public void SetupDeathForPersonOfGenderInCountryWithMinimumAge(int gender, DataCountry country, int age)
	{
	}

	public void CancelDestinyEventOfType(int eventType)
	{
	}

	public string LogStringAfterAgeIncrement()
	{
		return null;
	}

	public void UpdateStatesForAge(int age)
	{
	}

	public void UpdateOnlyLoveLifeStatesForAge(int age)
	{
	}

	public int GetAvailableAgeNearAge(int age)
	{
		return 0;
	}

	public bool IsSomethingSupposedToHappenAtAge(int age)
	{
		return false;
	}

	public int GetAgeForDestinyEventType(int eventType)
	{
		return 0;
	}

	public bool IsEventTypeHappeningThisYear(int eventType)
	{
		return false;
	}

	public bool WillGoToUniversityNextYear()
	{
		return false;
	}

	public bool WillGetFirstJobWithoutGoingToUniversityNextYear()
	{
		return false;
	}

	public bool CameOutOfTheClosetThisYear()
	{
		return false;
	}

	public bool ComingOutOfTheClosetNextYear()
	{
		return false;
	}

	public void ThirdPersonSpouseJustDied()
	{
	}

	public void EmigrateWithHero()
	{
	}

	public void DelayComingOut()
	{
	}

	public void InsertCollegeIntoDestiny()
	{
	}

	public void InsertCollegeRejectionIntoDestiny()
	{
	}

	public void CancelFutureLoveLifeAndIncludeBabies(bool babies)
	{
	}

	public void CancelGenderIdentityEvents()
	{
	}

	public void SetupFutureLoveLifeAndIncludeBabies(bool babies)
	{
	}

	public void InsertNextYearDivorceIntoDestiny()
	{
	}

	public void InsertNextYearParentsDivorceIntoDestinyForMother()
	{
	}

	public void InsertNextYearBabyByHeroUnknown()
	{
	}

	public void StartNewJobAtAge(int age)
	{
	}

	public void ChangeJobAtAge(int age)
	{
	}

	public void StartMilitaryAtAgeForStatus(int age, int status)
	{
	}

	public void CheckForPromotionAtAge(int age)
	{
	}

	public void JoinRoyalFamilyWithRankAndSeat(int theRank, string theSeat)
	{
	}

	public void UpdateRoyalFamilyWithNewRank(int theRank)
	{
	}

	public void LeaveRoyalFamily()
	{
	}

	public float GetAnnualIncome()
	{
		return 0f;
	}

	public bool CheckForContractingDiseaseAtAge(int age)
	{
		return false;
	}

	public bool CheckToCureDisease()
	{
		return false;
	}

	public void CheckToInsertDiseaseForChildAfterOptingOutOfVaccination()
	{
	}

	public void CureDisease(SimDisease disease)
	{
	}

	public void ContractDiseaseTypeAtAge(DataDiseaseType diseaseType, int age)
	{
	}

	public bool IsCurrentlySufferingFromDiseaseType(DataDiseaseType diseaseType)
	{
		return false;
	}

	public void HandlePressureToStudyMore()
	{
	}

	public void HandlePressureToGetAJob()
	{
	}

	public void HandlePressureToGetABetterJob()
	{
	}

	public void HandlePressureToGetMarried()
	{
	}

	public void HandlePressureToGetDivorced()
	{
	}

	public void HandlePressureToHaveBaby()
	{
	}

	public static SimThirdPersonScript CreateDeepCopy(SimThirdPersonScript script)
	{
		return null;
	}
}
