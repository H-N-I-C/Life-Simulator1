public class FightClubDopingChallengeActivity : ActivityLazyCsvBase
{
	private enum ScenarioId
	{
		FC_DOPING_ACCUSATION = 0,
		FC_DOPING_ACCUSATION_RUN = 1,
		FC_DOPING_ACCUSATION_RUN_FAIL = 2
	}

	private ScenarioId _currentScenarioId;

	private SimHero _hero;

	private SimFightClubStreetFighter Fighter__BackingField;

	public string CurrentScenarioId => null;

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

	public FightClubDopingChallengeActivity(Life life)
		: base(null)
	{
	}

	private void OnChoiceSelection(ChoiceSelection selection)
	{
	}

	private void StartFight()
	{
	}
}
