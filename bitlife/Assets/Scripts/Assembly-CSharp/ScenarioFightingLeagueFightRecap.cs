public class ScenarioFightingLeagueFightRecap : ScenarioLazyCsvBase
{
	private readonly FightingLeagueActivity _activity;

	public ScenarioFightingLeagueFightRecap(FightingLeagueActivity activity, ChoiceCallback choiceCallback)
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
