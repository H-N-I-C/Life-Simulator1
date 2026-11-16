public class FightClubHookupActivity : ActivityLazyCsvBase
{
	public static class FightClubHookupActivityBalancing
	{
		public static int MinRelationshipToHookup;

		public static int MinReceptivenessToHookUp;

		public static int CheaterMaxWillPower;

		public static bool AlwaysAcceptHookup;
	}

	private enum ScenarioId
	{
		FC_HOOK_UP_ASK = 0,
		FC_HOOK_UP_ASK_FOREIGN = 1,
		FC_HOOK_UP_ASK_ACCEPTED = 2,
		FC_HOOK_UP_OUTCOME = 3,
		FC_HOOK_UP_SAFE_SEX = 4,
		FC_HOOK_UP_REFUSAL = 5,
		FC_HOOK_UP_HERO_BUFF = 6,
		FC_HOOK_UP_LOVER_BUFF = 7,
		FC_HOOK_UP_HERO_DEBUFF = 8,
		FC_HOOK_UP_LOVER_DEBUFF = 9,
		FC_HOOK_UP_CHANGED_MIND_INSULT = 10,
		FC_HOOK_UP_CHANGED_MIND_NO_CONDOM = 11,
		FC_HOOK_UP_OUTCOME_PULL_OUT_FAIL = 12,
		FC_HOOK_UP_OUTCOME_PULL_GIVE_STD = 13
	}

	private enum FightClubHookupSafeSexScenarioId
	{
		FC_HOOK_UP_SAFE_SEX_MF_STD_COND = 0,
		FC_HOOK_UP_SAFE_SEX_MF_STD_NOCOND = 1,
		FC_HOOK_UP_SAFE_SEX_MF_OK_BC_COND = 2,
		FC_HOOK_UP_SAFE_SEX_MF_OK_NORM_COND = 3,
		FC_HOOK_UP_SAFE_SEX_MF_OK_BC_NOCOND = 4,
		FC_HOOK_UP_SAFE_SEX_MF_OK_NOCOND = 5,
		FC_HOOK_UP_SAFE_SEX_FM_STD_COND = 6,
		FC_HOOK_UP_SAFE_SEX_FM_STD_NOCOND = 7,
		FC_HOOK_UP_SAFE_SEX_FM_OK_COND = 8,
		FC_HOOK_UP_SAFE_SEX_FM_OK_NOCOND = 9,
		FC_HOOK_UP_SAFE_SEX_MM_STD_COND = 10,
		FC_HOOK_UP_SAFE_SEX_MM_STD_NOCOND = 11,
		FC_HOOK_UP_SAFE_SEX_MM_OK_COND = 12,
		FC_HOOK_UP_SAFE_SEX_MM_OK_NOCOND = 13
	}

	public enum SafeSexAction
	{
		USE_CONDOM = 0,
		NO_CONDOM = 1,
		PULL_OUT = 2,
		BACK_OUT = 3
	}

	public enum PossibleSexOutcome
	{
		NONE = 0,
		PREGNANCY = 1,
		GET_STD = 2,
		GIVE_STD = 3,
		CHEATER_ARGUMENT = 4
	}

	public const string StreetFightHookupCsvFile = "FightClub/Fight Club Scenarios - Hookup Fighter";

	public const string FanHookupCsvFile = "FightClub/Fight Club Scenarios - Hookup Fan";

	private string HookupCsvFile__BackingField;

	private SimLover Lover__BackingField;

	private bool IsCondomAvailable__BackingField;

	private bool IsStdLikely__BackingField;

	private bool UsesBirthControlSafeSexLine__BackingField;

	public int HeroEnjoyment;

	public int LoverEnjoyment;

	private string LastStdSpreadToLoverName__BackingField;

	private string Snippet__BackingField;

	private ScenarioId _currentScenarioId;

	private SimPerson _person;

	private SimHero _hero;

	public PossibleSexOutcome SexOutcome;

	private FightClubInteractionBuff _interactionBuff;

	private bool _gotStd;

	private SimDisease _resultingDisease;

	public string HookupCsvFile
	{
		get
		{
			return HookupCsvFile__BackingField;
		}
		set
		{
			HookupCsvFile__BackingField = value;
		}
	}

	public SimLover Lover
	{
		get
		{
			return Lover__BackingField;
		}
		private set
		{
			Lover__BackingField = value;
		}
	}

	public SimPerson Person => null;

	public bool IsCondomAvailable
	{
		get
		{
			return IsCondomAvailable__BackingField;
		}
		private set
		{
			IsCondomAvailable__BackingField = value;
		}
	}

	public bool IsStdLikely
	{
		get
		{
			return IsStdLikely__BackingField;
		}
		set
		{
			IsStdLikely__BackingField = value;
		}
	}

	public bool UsesBirthControlSafeSexLine
	{
		get
		{
			return UsesBirthControlSafeSexLine__BackingField;
		}
		private set
		{
			UsesBirthControlSafeSexLine__BackingField = value;
		}
	}

	public string LastStdSpreadToLoverName
	{
		get
		{
			return LastStdSpreadToLoverName__BackingField;
		}
		private set
		{
			LastStdSpreadToLoverName__BackingField = value;
		}
	}

	public string Snippet
	{
		get
		{
			return Snippet__BackingField;
		}
		set
		{
			Snippet__BackingField = value;
		}
	}

	public string CurrentScenarioId => null;

	public FightClubHookupActivity(Life life, SimPerson person, string csv, bool showHookupAskScenario)
		: base(null)
	{
	}

	private void CreateDirectHookupFeedbackEntryScenario()
	{
	}

	private void CreateAskEntryScenario()
	{
	}

	private void HookupAskChoiceCallback(ChoiceSelection selection)
	{
	}

	private void HeroTryToHookup()
	{
	}

	private void PushRefusalScenario()
	{
	}

	private void PushHookupOutcome()
	{
	}

	private void HandleChallengeTrackers()
	{
	}

	private void PushSafeSexScenario()
	{
	}

	private void OnSafeSexScenarioCallback(ChoiceSelection selection)
	{
	}

	private void OnHookupOutcomeCallback()
	{
	}

	private void TryPushStdScenario()
	{
	}

	private void HandleBuffOutcome()
	{
	}

	private void HandleStd(SafeSexAction selectedAction)
	{
	}

	private void HandlePregnancy(SafeSexAction selectedAction)
	{
	}

	private void HandleSafeSexBackOut()
	{
	}

	private void PushHookupNoSafe()
	{
	}

	private void CheckPregnancy(SimPerson mother, SimPerson father)
	{
	}

	private void HandleSexResult()
	{
	}

	private void HandleEventCollectables()
	{
	}

	private void HandleCheaterHero()
	{
	}

	private bool TryPushVampireLoverTurnHero()
	{
		return false;
	}

	private void ApplyHookupRefusalImpacts()
	{
	}

	private bool WillHookup()
	{
		return false;
	}

	private void GetLoverFromPerson()
	{
	}

	public string GetSafeSexScenarioIdString()
	{
		return null;
	}

	private string GetHookupSafeSexIdMaleMale()
	{
		return null;
	}

	private string GetHookupSafeSexIdFemaleMale()
	{
		return null;
	}

	private string GetHookupSafeSexIdMaleFemale()
	{
		return null;
	}
}
