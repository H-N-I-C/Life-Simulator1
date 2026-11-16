public class FightClubTutorialActivity : ActivityLazyCsvBase
{
	private enum ScenarioId
	{
		FC_TUTORIAL_SKIP = 0,
		FC_TUTORIAL_START = 1,
		FC_TUTORIAL_STYLES = 2,
		FC_TUTORIAL_STATS = 3,
		FC_TUTORIAL_OVERVIEW = 4,
		END = 5
	}

	private SimFightClubFighter Opponent__BackingField;

	private SimFightClubCoach Coach__BackingField;

	private SimFightClubFighterStats HeroStats__BackingField;

	private string OpponentType__BackingField;

	private ScenarioId _currentScenarioId;

	public SimFightClubFighter Opponent
	{
		get
		{
			return Opponent__BackingField;
		}
		private set
		{
			Opponent__BackingField = value;
		}
	}

	public SimFightClubCoach Coach
	{
		get
		{
			return Coach__BackingField;
		}
		private set
		{
			Coach__BackingField = value;
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

	public string OpponentType
	{
		get
		{
			return OpponentType__BackingField;
		}
		private set
		{
			OpponentType__BackingField = value;
		}
	}

	public string CurrentScenarioId => null;

	public FightClubTutorialActivity(Life life)
		: base(null)
	{
	}

	private void OnChoiceMade(ChoiceSelection selection)
	{
	}

	public void SkipToGym()
	{
	}

	private void ShowGymMenu()
	{
	}

	public string GetImageString()
	{
		return null;
	}

	public SimPerson GetParserPerson()
	{
		return null;
	}
}
