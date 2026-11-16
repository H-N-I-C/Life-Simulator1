public class ScenarioParentCheats : Scenario
{
	protected SimParent _parent;

	protected SimParent _otherParent;

	protected bool _parentNoticed;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimParent GetCheatingParent()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
