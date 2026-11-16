public class ScenarioFightingLeagueToManyFightsFeedback : ScenarioLazyCsvBase
{
	private readonly FightingLeagueActivity _activity;

	public ScenarioFightingLeagueToManyFightsFeedback(FightingLeagueActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
