public class ScenarioBlackMarketMillionBalancePush : Scenario
{
	private string _narrative;

	private BracketParser _parser;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
