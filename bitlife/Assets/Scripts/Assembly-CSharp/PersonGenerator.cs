using System.Collections;
using System.Collections.Generic;
using Game;

public class PersonGenerator
{
	public static SimLover GenerateLoverCandidateAndMustBeSingle(bool single)
	{
		return null;
	}

	public static SimLover GenerateLoverCandidateOfAgeAndMustBeSingleAndCanBeCustom(int age, bool single, bool custom)
	{
		return null;
	}

	public static SimLover GenerateLoverCandidateOfGenderAndSexualityAndOverrideAgeToAndOverrideBirthplaceToAndMustBeSingleAndCanBeCustom(int gender, int sexuality, int age, SimPlace birthplace, bool single, bool custom)
	{
		return null;
	}

	public static SimLover GenerateFamousHeroLoverCandidateOfGenderAndSexualityAndCanBeCustom(int gender, int sexuality, bool custom, bool ageOverride = false)
	{
		return null;
	}

	public static SimLover GenerateRoyalLoverOfGenderAndSexualityAndCanBeCustom(int gender, int sexuality, bool custom)
	{
		return null;
	}

	public static SimLover GenerateLoverFromClassmate(SimClassmate classmate)
	{
		return null;
	}

	public static SimLover GenerateLoverFromTeacher(SimTeacher teacher)
	{
		return null;
	}

	public static SimLover GenerateLoverFromCoworker(SimCoworker coworker)
	{
		return null;
	}

	public static SimLover GenerateLoverFromFriend(SimPerson friend)
	{
		return null;
	}

	public static SimCelebrityLover GenerateFamousLoverOfGenderAndSexualityAndOverrideAgeToAndCanBeCustom(string fameType, int gender, int sexuality, int age, bool custom)
	{
		return null;
	}

	public static SimCelebrityLover GenerateFamousLoverValentine(string fameType, int gender, int sexuality, int age, SimPersonName name, int royalRank, string birthPlaceString = "")
	{
		return null;
	}

	public static void CatchUpScriptsForAllDependentsOfLover(SimLover lover)
	{
	}

	private static bool ShouldForceRoyalChild()
	{
		return false;
	}

	public static SimChild GenerateChildInPlaceWithParentsAndGeneration(SimPlace place, ArrayList parentsArray, int generation, int forcedGender = 2)
	{
		return null;
	}

	public static ArrayList GenerateHeroChildArrayInPlaceWithParentsAndGeneration(SimPlace place, ArrayList parentsArray, int generation, SimPregnancy thePregnancy = null)
	{
		return null;
	}

	public static SimChild GenerateAdoptionChildInPlaceAndCanBeForeignBorn(SimPlace place, bool canBeForeign)
	{
		return null;
	}

	public static SimSibling GenerateSiblingWithFatherAtBeginningOfHeroLife(SimPerson father, bool beginning, GENDER gender = GENDER.BOTH)
	{
		return null;
	}

	public static SimSibling GenerateAdoptedSibling()
	{
		return null;
	}

	public static SimParent GenerateStepparentSpouseForParent(SimParent parent)
	{
		return null;
	}

	public static SimSibling GenerateStepSiblingFromStepparent(SimParent stepparent)
	{
		return null;
	}

	public static SimPerson GenerateSpouseForThirdPerson(SimPerson person)
	{
		return null;
	}

	public static SimPerson GenerateRandomPersonOfGenderInPlaceWithMinAgeAndMaxAgeAndCanBeCustom(int gender, SimPlace place, int minAge, int maxAge, bool canBeCustom)
	{
		return null;
	}

	public static SimSeniorPerson GenerateSeniorPersonOfGenderInPlaceWithMinAgeAndMaxAgeAndCanBeCustom(int gender, SimPlace place, int minAge, int maxAge, bool canBeCustom)
	{
		return null;
	}

	private static void PopulateSeniorProfileFamily(SimSeniorPerson senior, SimPlace place, int profileIndex)
	{
	}

	private static void AddSeniorProfileChild(SimSeniorPerson senior, SimPlace place, SimPerson otherParent)
	{
	}

	private static int GetSeniorProfileChildAge(SimSeniorPerson senior)
	{
		return 0;
	}

	private static void AssignSeniorProfileChildName(SimChild child, SimSeniorPerson senior)
	{
	}

	public static List<SimSeniorPerson> GetSeniorCenterList(int count, SimPlace place = null, int profileStartIndex = 0)
	{
		return null;
	}

	public static SimPerson GenerateAdoptionParentOfGenderInPlace(int gender, SimPlace place)
	{
		return null;
	}

	public static SimPrisoner GenerateRandomPrisonerOfGenderInPlaceForPrisonSecurity(int gender, SimPlace place, int prisonSecurity)
	{
		return null;
	}

	public static SimClassmate GenerateRandomClassmateOfGenderInPlaceWithAge(int gender, SimPlace place, int age, int schoolType)
	{
		return null;
	}

	public static SimTeacher GenerateRandomTeacherOfTypeForSchoolTypeInPlace(int teacherType, int schoolType, SimPlace place)
	{
		return null;
	}

	public static SimCoworker GenerateRandomCoworkerInCompanyAndHeroCareerAndInPlaceAndIsSupervisor(SimCompany company, DataCareer heroCareer, SimPlace place, bool isSupervisor)
	{
		return null;
	}

	public static SimCoworker GenerateCandywriterCoworkerIndexAtCompanyAndInPlaceAndIsSupervisor(int indexZI, SimCompany company, SimPlace place, bool isSupervisor)
	{
		return null;
	}

	public static SimCoworker GenerateRandomCoworkerInMilitaryBranchAndStatusAndInPlaceAndMinimumRankAndIsSupervisor(int militaryBranch, int militaryStatus, SimPlace place, int minimumRank, bool isSupervisor)
	{
		return null;
	}

	public static SimPerson GeneratePoliticalOpponentInPlace(SimPlace place)
	{
		return null;
	}

	public static SimBandmate GenerateBandmateOfGenderInPlaceWithRoleInBand(int gender, SimPlace place, int role, SimMusicBand band)
	{
		return null;
	}

	public static SimMobster GenerateMobsterOfGenderInPlaceInMobWithOtherMobsterArrayWithRankAndIsInHeroFamily(int gender, SimPlace place, DataMafiaMob mob, ArrayList mobsterArray, int rankNI, bool heroFamily)
	{
		return null;
	}

	public static SimAgent GenerateAgentInPlace(SimPlace place)
	{
		return null;
	}

	public static SimCostar GenerateFilmDirector(SimPlace place, SimActorFilm film)
	{
		return null;
	}

	public static SimCostar GenerateFilmCostar(SimPlace place, SimActorFilmRole role)
	{
		return null;
	}

	public static SimCostar GenerateShowCostar(SimPlace place, SimActorShowRole role)
	{
		return null;
	}

	public static SimCostar GenerateExtraCostar(SimPlace place, int actorType)
	{
		return null;
	}

	public static SimPerson GeneratePersonFromStreetPerson(DataStreetPerson streetPerson, SimPlace place)
	{
		return null;
	}

	public static SimEmployee GenerateRandomEmployeeCandidate(SimPlace place, DataEmployeeType type)
	{
		return null;
	}

	public static SimLover GenerateMailOrderBride()
	{
		return null;
	}

	public static SimTenant GenerateRandomTenantCandidate(SimPlace place, double askingPrice, GENDER gender = GENDER.BOTH, SimPersonName name = null)
	{
		return null;
	}

	public static SimCommunard GenerateRandomCommunard(SimPlace place)
	{
		return null;
	}

	public static SimSecretAgent GenerateRandomSecretAgentOfRank(SimPlace place, bool existingAgent)
	{
		return null;
	}

	public static SimZooEmployee GenerateRandomZooEmployeeCandidate(SimPlace place, ZOO_EMPLOYEE_TYPE type, bool starterZooEmployee, string habitatSpecialization = null)
	{
		return null;
	}

	public static SimDealerCartelMember GenerateRandomDealerCartelMember(SimPlace place, DEALER_RANK rank, SimDealerCartel cartel)
	{
		return null;
	}

	public static SimCasinoGambler GenerateRandomCasinoGambler(SimPlace place, bool mafiaMember = false)
	{
		return null;
	}

	public static SimCasinoCelebrity GenerateRandomCasinoCelebrity(SimPlace place, int fameScore)
	{
		return null;
	}

	public static SimRaceCarDriver GenerateUniqueRaceCarDriver(SimPlace place, RACING_TRACK_CATEGORY category, RACING_LICENSE license, ISet<string> takenNames)
	{
		return null;
	}

	public static SimRaceCarDriver GenerateRandomRaceCarDriver(SimPlace place, RACING_TRACK_CATEGORY category, RACING_LICENSE license)
	{
		return null;
	}

	public static SimAstronaut GenerateRandomAstronautProgramCandidate(SimPlace place, bool isInstructor)
	{
		return null;
	}

	public static SimFinancialAdvisor GenerateRandomFinancialAdvisorCandidate(SimPlace place)
	{
		return null;
	}

	public static SimModel GenerateRandomModel(SimPlace place, SimModelingAgency agency)
	{
		return null;
	}

	public static SimBookingAgent GenerateRandomModelBookAgent(SimPlace place, SimModelingAgency agency)
	{
		return null;
	}

	public static SimVampireHunter GenerateRandomVampireHunterInPlace(SimPlace place)
	{
		return null;
	}

	public static SimPerson GenerateRandomVampireInPlace(SimPlace place)
	{
		return null;
	}

	public static SimVampireCouncilMember GenerateRandomVampireCouncilMember()
	{
		return null;
	}

	public static void ConvertGeneratedPersonToVampire(SimPerson person)
	{
	}

	public static SimSoundCloudArtist GenerateRandomSoundCloudArtist(SimPlace place)
	{
		return null;
	}

	public static SimMusicProducerArtist GenerateRandomMusicProducerArtist(SimPlace place, MUSIC_PRODUCER_GENRE genre, string vibe, float vocalQuality, float popularity, bool scoutedByHero)
	{
		return null;
	}

	private static bool IsMemberOfCouncil(SimPerson person)
	{
		return false;
	}

	public static SimFightClubCoach GenerateRandomFightClubCoach(SimPlace place, bool isAutoReplacement = false, int maxQuality = 101)
	{
		return null;
	}

	private static void ApplyFightClubNpcTargetLevel(SimFightClubFighter fighter, int targetLevel)
	{
	}

	public static SimFightClubStreetFighter GenerateRandomFightClubStreetFighter(SimPlace place, FIGHT_CLUB_FIGHTING_STYLE style, int level, int gender, bool allowAccessories = true)
	{
		return null;
	}

	public static SimFightClubLeagueFighter GenerateRandomFightClubLeagueFighter(SimPlace place, FIGHT_CLUB_FIGHTING_STYLE style, int level, int gender)
	{
		return null;
	}

	public static DataCareer GetRandomCareerForPersonAtAgeWithHighestCompletedSchoolTypeAndUndergraduateMajor(SimPerson person, int age, int schoolType, int major)
	{
		return null;
	}

	public static float GetAttributeOfSimilarValueToOtherAttribute(float otherAttribute)
	{
		return 0f;
	}

	public static int GetGenderForLoverOfPerson(SimPerson person)
	{
		return 0;
	}
}
