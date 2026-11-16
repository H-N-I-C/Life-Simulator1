public class ScenarioEthicsFindObject : Scenario
{
	protected bool _happenedOnWalk;

	protected string _foundObject;

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
