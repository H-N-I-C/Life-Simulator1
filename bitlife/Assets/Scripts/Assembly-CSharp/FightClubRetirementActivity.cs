public class FightClubRetirementActivity : ActivityLazyCsvBase
{
	public enum ScenarioId
	{
		FC_RETIREMENT_FEEDBACK = 0,
		FC_RETIREMENT_COME_OUT = 1
	}

	private ScenarioId _currentScenarioId;

	public string CurrentScenarioId => null;

	public SimFightClub FightClub => null;

	public SimFightClubCoach Coach => null;

	public FightClubRetirementActivity(Life life)
		: base(null)
	{
	}

	public void Retire()
	{
	}
}
