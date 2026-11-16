using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimSpecialJobAstronaut : SimSpecialJob
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSpaceMission, bool> _003C_003E9__104_0;

		public static Func<SimAstronaut, bool> _003C_003E9__116_0;

		public static Func<SimAstronaut, bool> _003C_003E9__119_0;

		public static Func<SimAstronaut, bool> _003C_003E9__119_1;

		public static Func<char, bool> _003C_003E9__151_1;

		public static Func<char, bool> _003C_003E9__151_0;

		public static Func<SimSpaceMission, bool> _003C_003E9__152_0;

		internal bool _003CCheckForPromotion_003Eb__104_0(SimSpaceMission x)
		{
			return false;
		}

		internal bool _003CGetInstructor_003Eb__116_0(SimAstronaut t)
		{
			return false;
		}

		internal bool _003CGetRandomAstronaut_003Eb__119_0(SimAstronaut t)
		{
			return false;
		}

		internal bool _003CGetRandomAstronaut_003Eb__119_1(SimAstronaut t)
		{
			return false;
		}

		internal bool _003CGetAgencyName_003Eb__151_1(char t)
		{
			return false;
		}

		internal bool _003CGetAgencyName_003Eb__151_0(char t)
		{
			return false;
		}

		internal bool _003CGetNumFailedMissions_003Eb__152_0(SimSpaceMission t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass109_0
	{
		public int type;

		internal bool _003CNumMissionsCompleted_003Eb__0(SimSpaceMission x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass135_0
	{
		public SimDiscovery discovery;

		internal bool _003CGetPublication_003Eb__0(SimPublication t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass142_0
	{
		public int type;

		internal bool _003CGetArtifactEmoji_003Eb__0(SimDiscovery t)
		{
			return false;
		}
	}

	private static bool ForceEnroll__BackingField;

	private SimAstronautSkills Skills__BackingField;

	private SimAstronautAgency Agency__BackingField;

	private SimAstronautTrainingProgram TrainingProgram__BackingField;

	private int LastRefreshClassmateListAge__BackingField;

	private int LastRefreshCoworkerListAge__BackingField;

	private int YearsCompleted__BackingField;

	private int Rank__BackingField;

	private int StressScore__BackingField;

	private string Country__BackingField;

	private int HeroNextAstronautPromotion__BackingField;

	private int HeroAgeAtStartAstronautJob__BackingField;

	private int NumFlightMissions__BackingField;

	private int NumTimesSavedEarth__BackingField;

	private bool HasBeenToSpace__BackingField;

	private int NextAstronautDumbScenarioAge__BackingField;

	private List<SimSpaceMission> _MissionHistory__BackingField;

	private List<SimProbe> _probes__BackingField;

	private List<SimPublication> _publicationItems__BackingField;

	private List<SimDiscovery> _discoveries__BackingField;

	private int NumProbeCrashed__BackingField;

	private int LastMissionHeroAge__BackingField;

	public ArrayList CoWorkerArray;

	public static bool ForceEnroll
	{
		get
		{
			return ForceEnroll__BackingField;
		}
		set
		{
			ForceEnroll__BackingField = value;
		}
	}

	public SimAstronautSkills Skills
	{
		get
		{
			return Skills__BackingField;
		}
		set
		{
			Skills__BackingField = value;
		}
	}

	public SimAstronautAgency Agency
	{
		get
		{
			return Agency__BackingField;
		}
		set
		{
			Agency__BackingField = value;
		}
	}

	public SimAstronautTrainingProgram TrainingProgram
	{
		get
		{
			return TrainingProgram__BackingField;
		}
		set
		{
			TrainingProgram__BackingField = value;
		}
	}

	public int LastRefreshClassmateListAge
	{
		get
		{
			return LastRefreshClassmateListAge__BackingField;
		}
		set
		{
			LastRefreshClassmateListAge__BackingField = value;
		}
	}

	public int LastRefreshCoworkerListAge
	{
		get
		{
			return LastRefreshCoworkerListAge__BackingField;
		}
		set
		{
			LastRefreshCoworkerListAge__BackingField = value;
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

	public int Rank
	{
		get
		{
			return Rank__BackingField;
		}
		set
		{
			Rank__BackingField = value;
		}
	}

	public int StressScore
	{
		get
		{
			return StressScore__BackingField;
		}
		set
		{
			StressScore__BackingField = value;
		}
	}

	public string Country
	{
		get
		{
			return Country__BackingField;
		}
		set
		{
			Country__BackingField = value;
		}
	}

	public int HeroNextAstronautPromotion
	{
		get
		{
			return HeroNextAstronautPromotion__BackingField;
		}
		set
		{
			HeroNextAstronautPromotion__BackingField = value;
		}
	}

	public int HeroAgeAtStartAstronautJob
	{
		get
		{
			return HeroAgeAtStartAstronautJob__BackingField;
		}
		set
		{
			HeroAgeAtStartAstronautJob__BackingField = value;
		}
	}

	public int NumFlightMissions
	{
		get
		{
			return NumFlightMissions__BackingField;
		}
		set
		{
			NumFlightMissions__BackingField = value;
		}
	}

	public int NumTimesSavedEarth
	{
		get
		{
			return NumTimesSavedEarth__BackingField;
		}
		set
		{
			NumTimesSavedEarth__BackingField = value;
		}
	}

	public bool HasBeenToSpace
	{
		get
		{
			return HasBeenToSpace__BackingField;
		}
		set
		{
			HasBeenToSpace__BackingField = value;
		}
	}

	public int NextAstronautDumbScenarioAge
	{
		get
		{
			return NextAstronautDumbScenarioAge__BackingField;
		}
		set
		{
			NextAstronautDumbScenarioAge__BackingField = value;
		}
	}

	private List<SimSpaceMission> _MissionHistory
	{
		get
		{
			return _MissionHistory__BackingField;
		}
		set
		{
			_MissionHistory__BackingField = value;
		}
	}

	public List<SimSpaceMission> MissionHistory
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private List<SimProbe> _probes
	{
		get
		{
			return _probes__BackingField;
		}
		set
		{
			_probes__BackingField = value;
		}
	}

	public List<SimProbe> Probes
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private List<SimPublication> _publicationItems
	{
		get
		{
			return _publicationItems__BackingField;
		}
		set
		{
			_publicationItems__BackingField = value;
		}
	}

	public List<SimPublication> PublicationItems
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private List<SimDiscovery> _discoveries
	{
		get
		{
			return _discoveries__BackingField;
		}
		set
		{
			_discoveries__BackingField = value;
		}
	}

	public List<SimDiscovery> Discoveries
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int NumProbeCrashed
	{
		get
		{
			return NumProbeCrashed__BackingField;
		}
		set
		{
			NumProbeCrashed__BackingField = value;
		}
	}

	public int LastMissionHeroAge
	{
		get
		{
			return LastMissionHeroAge__BackingField;
		}
		set
		{
			LastMissionHeroAge__BackingField = value;
		}
	}

	public static SimSpecialJobAstronaut GetCurrentSpecialJobAstronaut()
	{
		return null;
	}

	public override void IncrementAge()
	{
	}

	private void CheckForDumbScenarios(int countAddedScenarios)
	{
	}

	private bool CheckForPromotion()
	{
		return false;
	}

	public override string JobTitleWithArticle(bool article)
	{
		return null;
	}

	public string GetNameForRank()
	{
		return null;
	}

	public List<string> GetClearanceItemsForRank(int rank)
	{
		return null;
	}

	public static string GetNameFromRank(int rank)
	{
		return null;
	}

	public int NumMissionsCompleted(int type = -1)
	{
		return 0;
	}

	public void StartTrainingProgram(SimAstronautTrainingProgram program)
	{
	}

	private void CheckPastCompletedTrainings()
	{
	}

	public string EncryptAlienMessage(string message, SimSpacePlanet planet)
	{
		return null;
	}

	public ArrayList RefreshCoWorkers()
	{
		return null;
	}

	public ArrayList RefreshClassmates()
	{
		return null;
	}

	private void CreateAstronautList(ref ArrayList array, bool isFullAstronaut, int overrideNecessary = 0, bool replace = false)
	{
	}

	public SimAstronaut GetInstructor(bool isFullAstronaut)
	{
		return null;
	}

	public void LaunchProbe(SimProbe probe)
	{
	}

	public static SimSpecialJobAstronaut GetSpecialJobIfAstronaut()
	{
		return null;
	}

	public SimAstronaut GetRandomAstronaut()
	{
		return null;
	}

	public SimAstronaut GetAstronautByName(string fullName)
	{
		return null;
	}

	public List<string> PossibleColorList()
	{
		return null;
	}

	public static double GetRandomAstronomicValue(double exampleVal)
	{
		return 0.0;
	}

	public static string GetRandomDynamicColor()
	{
		return null;
	}

	public static string GetHexCode(string color)
	{
		return null;
	}

	public static SimSpacePlanet GeneratePlanet(string planetName, SimProbe probe)
	{
		return null;
	}

	public static SimSpacePlanet GeneratePlanet(SimProbe discoveryProbe, bool generateName = false)
	{
		return null;
	}

	public static SimSpaceComet GenerateComet(SimProbe discoveryProbe)
	{
		return null;
	}

	public static SimSpaceAsteroid GenerateAsteroid(SimProbe discoveryProbe)
	{
		return null;
	}

	public static SimSpaceStar GenerateStar(SimProbe discoveryProbe)
	{
		return null;
	}

	public static SimSpaceBlackHole GenerateBlackHole(SimProbe discoveryProbe)
	{
		return null;
	}

	public static SimSpaceGalaxy GenerateGalaxy(SimProbe discoveryProbe)
	{
		return null;
	}

	public static SimSpaceNebula GenerateNebula(SimProbe discoveryProbe)
	{
		return null;
	}

	public void SetRank(int rank)
	{
	}

	public int NumPublications()
	{
		return 0;
	}

	public SimPublication GetPublication(SimDiscovery discovery)
	{
		return null;
	}

	public void RemovePublication(SimDiscovery discovery)
	{
	}

	public SimSpaceMission GenerateMission(int missionType)
	{
		return null;
	}

	private SimSpaceSatellite GenerateSatelliteWithRepairParts()
	{
		return null;
	}

	public string GetPlanetOriginByType(int discoveryType)
	{
		return null;
	}

	public string GetPlanetOriginByTypeAdj(int discoveryType)
	{
		return null;
	}

	public void AddNewArtifact(SimArtifact artifact)
	{
	}

	public string GetArtifactEmoji(int type)
	{
		return null;
	}

	public bool IsOnMission()
	{
		return false;
	}

	public Scenario DieScenario(int deathType)
	{
		return null;
	}

	public SimSpaceMission GetCurrentMission()
	{
		return null;
	}

	public static BAR_GRAPH_COLOR_SCHEME GetBarCodeColorForPublicationReview(int reviewValue)
	{
		return BAR_GRAPH_COLOR_SCHEME.LOW_IS_RED;
	}

	public static bool CountryHasSpaceProgram()
	{
		return false;
	}

	public bool HasClearanceLevel(ASTRONAUT_CLEARANCE_ITEM item)
	{
		return false;
	}

	public Scenario CreateClearanceFeedbackScenario(Scenario nextScenario = null)
	{
		return null;
	}

	public static void CancelAllAstronautScenarios()
	{
	}

	public static string GetAgencyName(bool addArticle, bool fullName = true)
	{
		return null;
	}

	public int GetNumFailedMissions()
	{
		return 0;
	}

	public bool HasClearanceLevelByMissionType(ASTRONAUT_MISSION_TYPE missionType)
	{
		return false;
	}

	public static List<SimSpecialJobAstronaut> GetPastAstronautJobs()
	{
		return null;
	}

	public static void ResignAstronautJobWithReason(int reason)
	{
	}

	public override bool ShouldBecomeFamous()
	{
		return false;
	}

	public override int GetMaxFame()
	{
		return 0;
	}

	public override int GetFameType()
	{
		return 0;
	}
}
