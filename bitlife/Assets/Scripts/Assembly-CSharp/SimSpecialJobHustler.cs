using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimSpecialJobHustler : SimSpecialJob
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Scenario, bool> _003C_003E9__99_0;

		public static Predicate<Scenario> _003C_003E9__99_2;

		public static Func<Scenario, bool> _003C_003E9__99_1;

		public static Predicate<Scenario> _003C_003E9__99_3;

		internal bool _003CGetScenarioArrayForHustleType_003Eb__99_0(Scenario x)
		{
			return false;
		}

		internal bool _003CGetScenarioArrayForHustleType_003Eb__99_2(Scenario x)
		{
			return false;
		}

		internal bool _003CGetScenarioArrayForHustleType_003Eb__99_1(Scenario x)
		{
			return false;
		}

		internal bool _003CGetScenarioArrayForHustleType_003Eb__99_3(Scenario x)
		{
			return false;
		}
	}

	private SimHustle CurrentHustle__BackingField;

	private SimHustleStreet Street__BackingField;

	private List<SimHustle> AllHustlesList__BackingField;

	private List<SimHustleStreet> HustleStreetHistoryArray__BackingField;

	private int Arrests__BackingField;

	[NonSerialized]
	public List<Scenario> _HustlerIncrementAgeScenarioQueueArray;

	private int AgeAtStartHustling__BackingField;

	private int AgeAtLastPracticePerformance__BackingField;

	private int AgeAtLastPracticeInstrument__BackingField;

	private int NumPerformancePracticesAtCurrentAge__BackingField;

	private int NumInstrumentPracticesAtCurrentAge__BackingField;

	private int PanhandlingIncreaseCount__BackingField;

	private Dictionary<int, int> PerformingIncreaseCountDictionary__BackingField;

	private Dictionary<int, int> ScammingIncreaseCountDictionary__BackingField;

	private float Att_streetsmarts__BackingField;

	private float Att_panhandling__BackingField;

	private Dictionary<int, float> PerformanceSkillDictionary__BackingField;

	private Dictionary<int, float> ScamSkillDictionary__BackingField;

	public const string NO_INSTRUMENT_TITLE = "Where's my instrument?";

	public const string BUSKING_HUSTLE_ENDED_BODY = "You no longer have the instrument you were busking with, so your busking hustle came to an end. Pick up a new instrument to start again.";

	public const string NO_BUSKABLE_INSTRUMENT_BODY = "You don't have an instrument you can play right now. Pick up an instrument to serenade someone.";

	public const string BUSKING_SETUP_ENDED_TITLE = "Change of tune";

	public const string BUSKING_MUSIC_TYPE_ENDED_BODY = "The music you were busking with isn't available anymore, so your busking hustle came to an end. Set up a new busking hustle to start again.";

	public const string BUSKING_ATTIRE_ENDED_BODY = "The outfit you were busking in isn't available anymore, so your busking hustle came to an end. Set up a new busking hustle to start again.";

	public SimHustle CurrentHustle
	{
		get
		{
			return CurrentHustle__BackingField;
		}
		set
		{
			CurrentHustle__BackingField = value;
		}
	}

	public SimHustleStreet Street
	{
		get
		{
			return Street__BackingField;
		}
		set
		{
			Street__BackingField = value;
		}
	}

	public List<SimHustle> AllHustlesList
	{
		get
		{
			return AllHustlesList__BackingField;
		}
		set
		{
			AllHustlesList__BackingField = value;
		}
	}

	public List<SimHustleStreet> HustleStreetHistoryArray
	{
		get
		{
			return HustleStreetHistoryArray__BackingField;
		}
		set
		{
			HustleStreetHistoryArray__BackingField = value;
		}
	}

	public int Arrests
	{
		get
		{
			return Arrests__BackingField;
		}
		set
		{
			Arrests__BackingField = value;
		}
	}

	public List<Scenario> HustlerIncrementAgeScenarioQueueArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtStartHustling
	{
		get
		{
			return AgeAtStartHustling__BackingField;
		}
		set
		{
			AgeAtStartHustling__BackingField = value;
		}
	}

	public int AgeAtLastPracticePerformance
	{
		get
		{
			return AgeAtLastPracticePerformance__BackingField;
		}
		set
		{
			AgeAtLastPracticePerformance__BackingField = value;
		}
	}

	public int AgeAtLastPracticeInstrument
	{
		get
		{
			return AgeAtLastPracticeInstrument__BackingField;
		}
		set
		{
			AgeAtLastPracticeInstrument__BackingField = value;
		}
	}

	public int NumPerformancePracticesAtCurrentAge
	{
		get
		{
			return NumPerformancePracticesAtCurrentAge__BackingField;
		}
		set
		{
			NumPerformancePracticesAtCurrentAge__BackingField = value;
		}
	}

	public int NumInstrumentPracticesAtCurrentAge
	{
		get
		{
			return NumInstrumentPracticesAtCurrentAge__BackingField;
		}
		set
		{
			NumInstrumentPracticesAtCurrentAge__BackingField = value;
		}
	}

	public int PanhandlingIncreaseCount
	{
		get
		{
			return PanhandlingIncreaseCount__BackingField;
		}
		set
		{
			PanhandlingIncreaseCount__BackingField = value;
		}
	}

	public Dictionary<int, int> PerformingIncreaseCountDictionary
	{
		get
		{
			return PerformingIncreaseCountDictionary__BackingField;
		}
		set
		{
			PerformingIncreaseCountDictionary__BackingField = value;
		}
	}

	public Dictionary<int, int> ScammingIncreaseCountDictionary
	{
		get
		{
			return ScammingIncreaseCountDictionary__BackingField;
		}
		set
		{
			ScammingIncreaseCountDictionary__BackingField = value;
		}
	}

	public float Att_streetsmarts
	{
		get
		{
			return Att_streetsmarts__BackingField;
		}
		set
		{
			Att_streetsmarts__BackingField = value;
		}
	}

	public float Att_panhandling
	{
		get
		{
			return Att_panhandling__BackingField;
		}
		set
		{
			Att_panhandling__BackingField = value;
		}
	}

	public Dictionary<int, float> PerformanceSkillDictionary
	{
		get
		{
			return PerformanceSkillDictionary__BackingField;
		}
		set
		{
			PerformanceSkillDictionary__BackingField = value;
		}
	}

	public Dictionary<int, float> ScamSkillDictionary
	{
		get
		{
			return ScamSkillDictionary__BackingField;
		}
		set
		{
			ScamSkillDictionary__BackingField = value;
		}
	}

	public SimSpecialJobHustler(SimHustleStreet aStreet)
	{
	}

	public float GetStartingStreetSmarts()
	{
		return 0f;
	}

	public override void IncrementAge()
	{
	}

	public void StartNewHustle(SimHustle theHustle)
	{
	}

	public void MoveToRandomStreet()
	{
	}

	public void RestartChain(Scenario currentScenario)
	{
	}

	public int GetYearsAsSpecialJobHustler()
	{
		return 0;
	}

	public static SimSpecialJobHustler GetCurrentSpecialJobHustler()
	{
		return null;
	}

	public static ArrayList GetAllHeroSpecialJobsAsHustler()
	{
		return null;
	}

	public static SimSpecialJobHustler GetMostRecentSpecialJobHustler()
	{
		return null;
	}

	public static float GetStartingHustleSkill(int hustleType)
	{
		return 0f;
	}

	public static int GetTotalYearsHustling()
	{
		return 0;
	}

	public static int GetTotalArrests()
	{
		return 0;
	}

	public static float GetTotalEarningsForHustleType(int hustleType)
	{
		return 0f;
	}

	public static float GetTotalEarningsForAllHustles()
	{
		return 0f;
	}

	public static int GetTotalYearsInHustle(int hustleType)
	{
		return 0;
	}

	public static string GetPresentVerbForHustle(int hustleType, bool capitalize)
	{
		return null;
	}

	public static string GetPastVerbForHustle(int hustleType, bool capitalize)
	{
		return null;
	}

	public static string GetPresentParticipleVerbForHustle(int hustleType, bool capitalize)
	{
		return null;
	}

	public static string GetHustleNameForHustleType(int hustleType)
	{
		return null;
	}

	public static string GetRandomContextForHustle()
	{
		return null;
	}

	public string GetOccupationString()
	{
		return null;
	}

	public override string JobTitleWithArticle(bool article)
	{
		return null;
	}

	public static string GetTitleForScamType(int scamType)
	{
		return null;
	}

	public static float GetTotalSkillInPerformanceAct(int actIndex)
	{
		return 0f;
	}

	public static float GetTotalSkillForScamType(int scamType)
	{
		return 0f;
	}

	public ArrayList GetScenarioStringBankArray(bool annualChain)
	{
		return null;
	}

	public ArrayList GetScenarioArrayForHustleType(int hustleType, bool sessionHaul)
	{
		return null;
	}

	public bool RecoverInvalidBuskingHustle()
	{
		return false;
	}

	public bool RecoverInvalidBuskingHustle(out string invalidReason)
	{
		invalidReason = null;
		return false;
	}

	public static string GetBuskingHustleEndedTitle(string invalidReason)
	{
		return null;
	}

	public static string GetBuskingHustleEndedBody(string invalidReason)
	{
		return null;
	}

	public static Scenario GetBuskingHustleEndedScenario(string invalidReason)
	{
		return null;
	}

	public static bool TryGetActiveBuskingHustler(out SimSpecialJobHustler hustler, out SimMusicInstrumentPhysical instrument, out string invalidReason)
	{
		hustler = null;
		instrument = null;
		invalidReason = null;
		return false;
	}

	public static void EndBuskingHustleIfInstrumentRemoved(SimMusicInstrumentPhysical removedInstrument)
	{
	}

	public void BreakBuskingScenarioChain(bool willLoseInstrument)
	{
	}

	public float GetCurrentHustleSkill()
	{
		return 0f;
	}

	public void ImpactCurrentPerformSkill(int impactScore, bool manualImpact = false)
	{
	}

	public void ImpactCurrentScamSkill(int impactScore, int scamType, bool manualImpact = false)
	{
	}

	public void ImpactCurrentInstrumentSkill(int impactScore, bool manualImpact = false)
	{
	}

	public void GenesisImpactPanhandling(int impactScore)
	{
	}

	public void ImpactPanhandling(int impactScore, bool manualImpact = false)
	{
	}

	public void GenesisImpactStreetSmarts(int impactScore)
	{
	}

	public void ImpactStreetSmarts(int impactScore, bool manualImpact = false)
	{
	}

	public int ImpactScoreAndIsManual(int impactScore, bool manualImpact)
	{
		return 0;
	}

	public int DetermineImpactScoreBySuccess()
	{
		return 0;
	}
}
