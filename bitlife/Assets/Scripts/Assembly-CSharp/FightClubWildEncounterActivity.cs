public class FightClubWildEncounterActivity : ActivityLazyCsvBase
{
	public enum Context
	{
		None = 0,
		Clubbing = 1,
		Movies = 2,
		Walk = 3,
		Gym = 4
	}

	private SimFightClub Club__BackingField;

	private SimFightClubFighterStats HeroStats__BackingField;

	private SimFightClubStreetFighter Fighter__BackingField;

	private string OpponentTypeDescription__BackingField;

	private string ColorString__BackingField;

	private string ColorStringNarrative__BackingField;

	private readonly SimHero _hero;

	private string _context;

	public SimFightClub Club
	{
		get
		{
			return Club__BackingField;
		}
		private set
		{
			Club__BackingField = value;
		}
	}

	public SimFightClubFighterStats HeroStats
	{
		get
		{
			return HeroStats__BackingField;
		}
		private set
		{
			HeroStats__BackingField = value;
		}
	}

	public SimFightClubStreetFighter Fighter
	{
		get
		{
			return Fighter__BackingField;
		}
		set
		{
			Fighter__BackingField = value;
		}
	}

	public string OpponentTypeDescription
	{
		get
		{
			return OpponentTypeDescription__BackingField;
		}
		private set
		{
			OpponentTypeDescription__BackingField = value;
		}
	}

	public string ColorString
	{
		get
		{
			return ColorString__BackingField;
		}
		private set
		{
			ColorString__BackingField = value;
		}
	}

	public string ColorStringNarrative
	{
		get
		{
			return ColorStringNarrative__BackingField;
		}
		private set
		{
			ColorStringNarrative__BackingField = value;
		}
	}

	public FightClubWildEncounterActivity(Life life, SimFightClubStreetFighter fighter, Context context = Context.None)
		: base(null)
	{
	}

	private void ResolveDisplayData()
	{
	}

	private void OnChoice(ChoiceSelection selection)
	{
	}

	private void OnRunFailDismissed()
	{
	}

	public static void TryChainWildEncounter(Life life, Scenario feedbackScenario, Context context)
	{
	}
}
