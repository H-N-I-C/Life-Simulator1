using Game;

public abstract class ScenarioGameFightingOutcomeBase : ScenarioLazyCsvBase
{
	protected readonly DataFightOutcomeColor _colorData;

	private readonly GameFightingOutcomeActivity _activity;

	public ScenarioGameFightingOutcomeBase(GameFightingOutcomeActivity activity)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
