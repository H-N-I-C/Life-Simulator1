public class ScenarioFightingLeagueFightNight : ScenarioLazyCsvBase
{
	private readonly FightingLeagueActivity _activity;

	public ScenarioFightingLeagueFightNight(FightingLeagueActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
