public class FightClubAgeUpLoseBelt : ActivityLazyCsvBase
{
	private enum ScenarioId
	{
		FC_AGEUP_LEAGUE_NOTICE = 0,
		FC_AGEUP_LEAGUE_LOST_TITLE = 1
	}

	private readonly SimFightClub _fightClub;

	private ScenarioId _currentScenarioId;

	public string CurrentScenarioId => null;

	public FightClubAgeUpLoseBelt(Life life)
		: base(null)
	{
	}
}
