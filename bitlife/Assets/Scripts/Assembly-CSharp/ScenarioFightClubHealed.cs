public class ScenarioFightClubHealed : ScenarioLazyCsvBase
{
	private readonly ActivityLazyCsvBase _activity;

	private readonly SimFightClubInjury _healedInjury;

	private readonly SimFightClubFighterStats _heroStats;

	public ScenarioFightClubHealed(ActivityLazyCsvBase activity, SimFightClubInjury healedInjury)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void BuildRemainingInjuriesTable()
	{
	}

	private void BuildRestoredStatBars()
	{
	}
}
