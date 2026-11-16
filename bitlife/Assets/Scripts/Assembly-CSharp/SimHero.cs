using System;
using Genesis.Controllers;
using Genesis.Entities;

[Serializable]
public class SimHero : SimPerson
{
	private sealed class _003C_003Ec__DisplayClass113_0
	{
		public SimOccupationStudent highestSchoolCompleted;

		internal bool _003CEducationString_003Eb__0(SimOccupationStudent x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass31_0
	{
		public SimPerson person;

		internal bool _003CTryBefriendFocusedNpc_003Eb__0(SimPerson friend)
		{
			return false;
		}
	}

	private const float SIGNIFICANT_DEBT_THRESHOLD = -25000f;

	private SimHero()
	{
	}

	public SimHero(bool initHere)
	{
	}

	public SimHero(SimPersonName name, int gender, string countryCode, string city, GodModePersonConfig config)
	{
	}

	public void SetupEverythingElseForNewHero()
	{
	}

	public string GetDefaultOccupationString()
	{
		return null;
	}

	public override void IncrementAge()
	{
	}

	public override void Die()
	{
	}

	private void CheckForWholeSomeLife()
	{
	}

	private static void TrackWholesomeLifeMilestone(WholesomeLifeCpeMilestone milestone, bool completed, string lifeId)
	{
	}

	public override void AnnualAttributeAdjustment()
	{
	}

	public override void OnLifeIncrementAgeEnded()
	{
	}

	public override float AnnualCostOfLiving()
	{
		return 0f;
	}

	public int CurrentWeeklyHours()
	{
		return 0;
	}

	public bool CurrentlySufferingFromOverwork()
	{
		return false;
	}

	public bool ScheduleIsTooFullWithPartTimeJobsForOccupation()
	{
		return false;
	}

	public int MaxWorkableHours()
	{
		return 0;
	}

	public float CurrentStress()
	{
		return 0f;
	}

	public Scenario GetCrimeScenarioForCrimeType(int theCrimeType)
	{
		return null;
	}

	public void LeavePrisonWithDischargeType(int dischargeType)
	{
	}

	public bool CurrentlyInPrison()
	{
		return false;
	}

	public string GetHeroCurrentRelationshipString()
	{
		return null;
	}

	public bool HasLover()
	{
		return false;
	}

	public bool IsRVAvailable()
	{
		return false;
	}

	public bool LoverLeavesHero()
	{
		return false;
	}

	public bool StartDatingNpc()
	{
		return false;
	}

	public bool TryStartDatingFocusedNpc(ScenarioController controller, out Scenario nextScenario)
	{
		nextScenario = null;
		return false;
	}

	public bool TryCompleteHookupWithFocusedNpc(ScenarioController controller, int npcEnjoyment)
	{
		return false;
	}

	public bool TryCompleteHookupWithFocusedNpc(ScenarioController controller, SimEntity expectedActor, int npcEnjoyment)
	{
		return false;
	}

	public bool CanCompleteHookupWithFocusedNpc(ScenarioController controller, int npcEnjoyment, out SimEntity actor)
	{
		actor = null;
		return false;
	}

	private void ApplyNativeHookupCheatingState(Life life, SimLover hookup, bool wasAlreadyAnEx)
	{
	}

	public bool TryBefriendFocusedNpc(ScenarioController controller, Scenario currentScenario)
	{
		return false;
	}

	private static void RemoveExactExLoverReferences(SimPast managerPast, SimPast lifePast, SimPerson person)
	{
	}

	private static void RemoveExactExLoverReference(SimPast past, SimPerson person)
	{
	}

	private static bool ContainsEquivalentExLoverReference(SimPast past, SimPerson person)
	{
		return false;
	}

	private static void NormalizeEquivalentExLoverReference(SimPast past, SimPerson person)
	{
	}

	private static void PreserveExLoverLifecycle(SimLover source, SimLover destination)
	{
	}

	private static void RemoveEquivalentFriendReferences(Life life, SimPerson person)
	{
	}

	public bool IsBelowRetirementAge()
	{
		return false;
	}

	public bool OwnsIAP(int iapType)
	{
		return false;
	}

	public bool IsIAPContentActive(int iapValue)
	{
		return false;
	}

	public bool IsOrphan()
	{
		return false;
	}

	public bool CanThrowParty()
	{
		return false;
	}

	public float GetPopularity()
	{
		return 0f;
	}

	public int GetPartySize(string size)
	{
		return 0;
	}

	public int GetHousePartyOutcome(int enjoymentScore, int permissionStatus)
	{
		return 0;
	}

	private HousePartyOutcome CalculatePartyOutcome(int enjoymentScore, int permission)
	{
		return default;
	}

	public override bool IsRoyalty()
	{
		return false;
	}

	public override bool IsActor()
	{
		return false;
	}

	public bool IsSpecialJobType(SPECIAL_JOB_TYPE jobType)
	{
		return false;
	}

	public override bool IsSpecificSportsPlayer(SPORT_TYPE sportsType)
	{
		return false;
	}

	public override bool IsPolitician()
	{
		return false;
	}

	public bool IsProducer()
	{
		return false;
	}

	public bool InPrison()
	{
		return false;
	}

	public bool IsFamous()
	{
		return false;
	}

	public string BusinessName()
	{
		return null;
	}

	public bool IsInvestedIn(string name)
	{
		return false;
	}

	public bool InCartel()
	{
		return false;
	}

	public bool InMafia()
	{
		return false;
	}

	public bool IsAstronaut()
	{
		return false;
	}

	public bool IsHustler()
	{
		return false;
	}

	public bool IsModel()
	{
		return false;
	}

	public string CareerName()
	{
		return null;
	}

	public bool OwnsCasino()
	{
		return false;
	}

	public bool OwnsZoo()
	{
		return false;
	}

	public void StartSchoolAsStudent(SimOccupationStudent student)
	{
	}

	public SimOccupationStudent StartPublicSchoolAfterExpulsion(SimOccupationStudent previousSchool)
	{
		return null;
	}

	public void TransferStudentYears(SimOccupationStudent previousSchool, SimOccupationStudent nextSchool)
	{
	}

	public bool CurrentlyInSchool()
	{
		return false;
	}

	public float EducationalAbilityScore()
	{
		return 0f;
	}

	public void StartMilitary(SimOccupationMilitary military)
	{
	}

	public void LeaveMilitaryWithDischargeType(int dischargeType)
	{
	}

	public bool CurrentlyInTheMilitary()
	{
		return false;
	}

	public void StartJob(SimJob job)
	{
	}

	public void LeaveJobWithTerminationType(int terminationType)
	{
	}

	public bool CurrentlyHasAJob()
	{
		return false;
	}

	public void StartSpecialJob(SimSpecialJob specialJob)
	{
	}

	public void LeaveSpecialJobWithTerminationType(int terminationType)
	{
	}

	public bool CurrentlyHasASpecialJob()
	{
		return false;
	}

	public void StartPartTimeJob(SimPartTimeJob job)
	{
	}

	public void LeavePartTimeJobWithTerminationType(SimPartTimeJob job, int terminationType)
	{
	}

	public bool ShouldPassADrugTest()
	{
		return false;
	}

	public bool ShouldBecomeAddictedToDrug(DataDrug drug)
	{
		return false;
	}

	public SimAddiction BecomeAddictedToDrug(DataDrug drug)
	{
		return null;
	}

	public bool ShouldBecomeAddictedToGambling()
	{
		return false;
	}

	public SimAddiction BecomeAddictedToGambling()
	{
		return null;
	}

	public void CureAddiction(SimAddiction addiction)
	{
	}

	public SimDisease ContractDiseaseTypeAndShouldBeDiagnosedImmediately(DataDiseaseType diseaseType, bool diagnosed)
	{
		return null;
	}

	public bool CurrentlySufferingFromDiseaseType(DataDiseaseType diseaseType)
	{
		return false;
	}

	public bool CurrentlySufferingFromSTD()
	{
		return false;
	}

	public void CureDisease(SimDisease disease)
	{
	}

	public void RestoreAttributesAfterCureFromDisease(SimDisease disease)
	{
	}

	public void StartDatingLover(SimLover lover)
	{
	}

	public void AddCurrentLoverToExesAndCanBecomeStalkerAndDidDie(bool canBecomeStalker, bool didDie)
	{
	}

	public int MaxNumberOfPetsPermissable()
	{
		return 0;
	}

	public int NumHorsesOwned()
	{
		return 0;
	}

	public int NumLlamasOwned()
	{
		return 0;
	}

	public int NumExoticAnimalsOwned()
	{
		return 0;
	}

	public void VisitVacationDestination(DataVacationDestination destination)
	{
	}

	public bool IsEligibleForAdoption()
	{
		return false;
	}

	public void EnterOrphanage()
	{
	}

	public bool IsARoyal()
	{
		return false;
	}

	public override void BecomeVampire(int infectedAge, bool shouldGetVampireName, SimPerson sourceVampire = null)
	{
	}

	public bool ShouldPassLicenseTestCar()
	{
		return false;
	}

	public bool ShouldPassLicenseTestPlane()
	{
		return false;
	}

	public bool ShouldPassLicenseTestBoat()
	{
		return false;
	}

	public void ReevaluateAgeOfDeath()
	{
	}

	public override void ImpactHappiness(int impactScore)
	{
	}

	public override void ImpactHealth(int impactScore)
	{
	}

	public override void ImpactIntelligence(int impactScore)
	{
	}

	public override void ImpactAppearance(int impactScore)
	{
	}

	public override void ImpactFame(int impactScore)
	{
	}

	public void CheckForPerfectStats()
	{
	}

	public override string EducationString()
	{
		return null;
	}

	public override string OccupationString()
	{
		return null;
	}
}
