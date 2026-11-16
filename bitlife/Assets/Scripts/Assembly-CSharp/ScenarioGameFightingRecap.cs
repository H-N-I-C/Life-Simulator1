public class ScenarioGameFightingRecap : ScenarioLazyCsvBase
{
	private readonly GameFightingOutcomeActivity _activity;

	private readonly SimFightClubFighterStats _heroStats;

	public ScenarioGameFightingRecap(GameFightingOutcomeActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	public override void RefreshScenario()
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
