public class FightClubProfileActivity : ActivityLazyCsvBase
{
	private enum ScenarioId
	{
		FC_PROFILE_LEVEL_CAP = 0,
		FC_PROFILE_ACTIVE = 1,
		FC_PROFILE_RETIREMENT = 2
	}

	private SimFightClub Club__BackingField;

	private SimFightClubFighterStats Stats__BackingField;

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

	public SimFightClubFighterStats Stats
	{
		get
		{
			return Stats__BackingField;
		}
		private set
		{
			Stats__BackingField = value;
		}
	}

	public FightClubProfileActivity(Life life)
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
