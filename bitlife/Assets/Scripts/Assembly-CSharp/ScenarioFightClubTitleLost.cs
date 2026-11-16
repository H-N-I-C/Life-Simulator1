public class ScenarioFightClubTitleLost : ScenarioLazyCsvBase
{
	private readonly int _newRank;

	private readonly GameFightingOutcomeActivity _activity;

	public ScenarioFightClubTitleLost(GameFightingOutcomeActivity activity, int newRank, ChoiceCallback choiceCallback = null)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
