public class ScenarioFightingLeagueMatchFixing : ScenarioLazyCsvBase
{
	private readonly FightingLeagueActivity _activity;

	public ScenarioFightingLeagueMatchFixing(FightingLeagueActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
