public class ScenarioExLoverInboundRekindle : Scenario
{
	protected SimLover _exLover;

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

	public static SimLover GetTheExLover()
	{
		return null;
	}
}
