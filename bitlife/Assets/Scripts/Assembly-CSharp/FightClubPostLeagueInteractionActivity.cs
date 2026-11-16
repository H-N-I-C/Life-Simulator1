using Game;

public class FightClubPostLeagueInteractionActivity : ActivityLazyCsvBase
{
	private enum ScenarioId
	{
		FC_POST_LEAGUE_FAN_GOOD = 0,
		FC_POST_LEAGUE_FAN_BAD = 1,
		FC_POST_LEAGUE_FAN_AUTOGRAPH = 2,
		FC_POST_LEAGUE_FAN_THANKS = 3,
		FC_POST_LEAGUE_SPORTSMANSHIP = 4
	}

	public const string CsvFile = "FightClub/Fight Club Scenarios - Post League Match Interaction";

	private ScenarioId _currentScenarioId;

	private SimHero _hero;

	private DataFightIngFanInteraction FanInteraction__BackingField;

	private SimLover Fan__BackingField;

	private FightClubPostLeagueData LeagueData__BackingField;

	private int CredEarn__BackingField;

	private DataFightSportsmanshipScenario SportsmanshipScenario__BackingField;

	private string ResolvedScenarioString__BackingField;

	private string ResolvedFeedbackSnippet__BackingField;

	public string CurrentScenarioId => null;

	public DataFightIngFanInteraction FanInteraction
	{
		get
		{
			return FanInteraction__BackingField;
		}
		set
		{
			FanInteraction__BackingField = value;
		}
	}

	public SimLover Fan
	{
		get
		{
			return Fan__BackingField;
		}
		set
		{
			Fan__BackingField = value;
		}
	}

	public FightClubPostLeagueData LeagueData
	{
		get
		{
			return LeagueData__BackingField;
		}
		set
		{
			LeagueData__BackingField = value;
		}
	}

	public int CredEarn
	{
		get
		{
			return CredEarn__BackingField;
		}
		set
		{
			CredEarn__BackingField = value;
		}
	}

	public DataFightSportsmanshipScenario SportsmanshipScenario
	{
		get
		{
			return SportsmanshipScenario__BackingField;
		}
		set
		{
			SportsmanshipScenario__BackingField = value;
		}
	}

	public string ResolvedScenarioString
	{
		get
		{
			return ResolvedScenarioString__BackingField;
		}
		set
		{
			ResolvedScenarioString__BackingField = value;
		}
	}

	public string ResolvedFeedbackSnippet
	{
		get
		{
			return ResolvedFeedbackSnippet__BackingField;
		}
		set
		{
			ResolvedFeedbackSnippet__BackingField = value;
		}
	}

	public FightClubPostLeagueInteractionActivity(Life life, FightClubPostLeagueData leagueData, bool sportsmanship = false)
		: base(null)
	{
	}

	private void CreateSportsmanshipEntryScenario()
	{
	}

	private void CreateFanEntryScenario()
	{
	}

	private void OnPositiveFanInteractionChoiceSelected(ChoiceSelection selection)
	{
	}

	private void OnNegativeFanInteractionChoiceSelected(ChoiceSelection selection)
	{
	}

	private void OnSportmanshipChoiceSelected(ChoiceSelection selection)
	{
	}

	private void GoodSportsmanshipImpact()
	{
	}

	private void PushAttackFighterScenario()
	{
	}

	private void BadSportsmanshipImpact()
	{
	}

	private void PushComplimentScenario()
	{
	}

	private void PushInsultScenario(SimPerson person, bool IsFan)
	{
	}

	private void PushAttackFanScenario()
	{
	}

	private void PushHookupScenario()
	{
	}

	private void PushOfferAutographScenario()
	{
	}

	private void PushThanksScenario()
	{
	}

	private void ImpactAllLeagueFighters(int impact)
	{
	}
}
