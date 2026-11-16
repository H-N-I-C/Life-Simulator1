public class FightClubFighterProfileActivity : ActivityLazyCsvBase
{
	private enum ScenarioId
	{
		FC_PROFILE_FIGHTER = 0,
		FC_PROFILE_STREET_FIGHTER = 1,
		FC_PROFILE_LEAGUE_FIGHTER = 2
	}

	private SimFightClub Club__BackingField;

	private SimFightClubFighter Fighter__BackingField;

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

	public SimFightClubFighter Fighter
	{
		get
		{
			return Fighter__BackingField;
		}
		private set
		{
			Fighter__BackingField = value;
		}
	}

	public string FighterDifficulty => null;

	public FightClubFighterProfileActivity(Life life, SimFightClubFighter fighter)
		: base(null)
	{
	}

	public string GetScenarioId()
	{
		return null;
	}

	private void OnChoiceMade(ChoiceSelection selection)
	{
	}
}
