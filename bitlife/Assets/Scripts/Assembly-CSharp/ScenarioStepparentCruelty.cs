public class ScenarioStepparentCruelty : Scenario
{
	protected SimParent _parent;

	protected SimParent _stepparent;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimParent GetCruelStepparent()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
