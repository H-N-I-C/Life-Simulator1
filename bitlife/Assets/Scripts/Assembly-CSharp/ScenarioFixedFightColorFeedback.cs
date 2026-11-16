public class ScenarioFixedFightColorFeedback : ScenarioLazyCsvBase
{
	private readonly FightingLeagueActivity _activity;

	public ScenarioFixedFightColorFeedback(FightingLeagueActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
