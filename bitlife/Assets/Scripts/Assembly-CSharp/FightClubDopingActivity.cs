public class FightClubDopingActivity : ActivityLazyCsvBase
{
	public class FightClubDopingBalancing
	{
		public const int MaxDopingOutcomeChance = 80;

		public const int MinDopingOutcomeChance = 25;

		public const int MinDopingUsageTimes = 1;

		public const int MaxDopingUsageTimes = 20;

		public static bool ForceNegativeOutcome;

		public static int CalculateChance(int numTimesUsedSteroids)
		{
			return 0;
		}
	}

	public enum ScenarioId
	{
		FC_DOPING_ENTRY = 0,
		FC_DOPING_ALREADY_JUICED = 1,
		FC_DOPING_NO_STREET_CRED = 2,
		FC_DOPING_RESULT = 3,
		FC_DOPING_RESULT_GENERAL = 4,
		FC_DOPING_DISEASE = 5,
		FC_DOPING_ROID_RAGE = 6,
		FC_DOPING_ROID_RAGE_APOLOGIZE = 7,
		FC_DOPING_ROID_RAGE_ARGUE = 8,
		FC_DOPING_CHEMICAL_TENSION = 9,
		FC_DOPING_CHEMICAL_TENSION_YELL = 10,
		FC_DOPING_CHEMICAL_TENSION_SOLICIT = 11
	}

	public const string DopingCsvFile = "FightClub/Fight Club Scenarios - Doping";

	private double SteroidCost__BackingField;

	private SimFightClubSteroid Steroid__BackingField;

	private float PreviousStatValue__BackingField;

	private float BoostedStatValue__BackingField;

	private FightClubHelpers.FightClubFamiliar RageTarget__BackingField;

	private string ProstituteTerm__BackingField;

	private GENDER ProstituteGender__BackingField;

	private ScenarioId _currentScenarioId;

	private SimFightClub _fightClub;

	private SimHero _hero;

	public double SteroidCost
	{
		get
		{
			return SteroidCost__BackingField;
		}
		private set
		{
			SteroidCost__BackingField = value;
		}
	}

	public double StreetCredBalance => 0.0;

	public SimFightClubSteroid Steroid
	{
		get
		{
			return Steroid__BackingField;
		}
		private set
		{
			Steroid__BackingField = value;
		}
	}

	public float PreviousStatValue
	{
		get
		{
			return PreviousStatValue__BackingField;
		}
		private set
		{
			PreviousStatValue__BackingField = value;
		}
	}

	public float BoostedStatValue
	{
		get
		{
			return BoostedStatValue__BackingField;
		}
		private set
		{
			BoostedStatValue__BackingField = value;
		}
	}

	public FightClubHelpers.FightClubFamiliar RageTarget
	{
		get
		{
			return RageTarget__BackingField;
		}
		set
		{
			RageTarget__BackingField = value;
		}
	}

	public string ProstituteTerm
	{
		get
		{
			return ProstituteTerm__BackingField;
		}
		set
		{
			ProstituteTerm__BackingField = value;
		}
	}

	public GENDER ProstituteGender
	{
		get
		{
			return ProstituteGender__BackingField;
		}
		set
		{
			ProstituteGender__BackingField = value;
		}
	}

	public SimFightClubFighterStats Stats => null;

	public string CurrentScenarioId => null;

	public FightClubDopingActivity(Life life)
		: base(null)
	{
	}

	public void SelectSteroid(SimFightClubSteroid steroid)
	{
	}

	private void OnFightClubDopingEntry(ChoiceSelection selection)
	{
	}

	private void OnDopingIsDone(ChoiceSelection selection)
	{
	}

	private void OnDopingRoidRageChoiceSelected(ChoiceSelection selection)
	{
	}

	private void OnDopingChemicalTensionChoiceSelected(ChoiceSelection selection)
	{
	}

	private void SolicitProstitute()
	{
	}

	private int GetChanceOfDopingOutcome()
	{
		return 0;
	}

	private void ApplyDopingEffect()
	{
	}

	private float GetHeroStatForType(FIGHT_CLUB_STEROID_TYPE steroidType)
	{
		return 0f;
	}
}
