public class ScenarioChildhoodPeriod : Scenario
{
	protected SimParent _parent;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private SimSibling GetOlderSister()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
