public class ScenarioExLoverPaternityTest : Scenario
{
	protected SimLover _exLover;

	protected SimChild _child;

	protected string _narrative;

	public ScenarioExLoverPaternityTest(SimLover theExLover, SimChild theChild)
	{
	}

	public ScenarioExLoverPaternityTest()
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimLover GetEligibleExLover()
	{
		return null;
	}

	public static SimChild GetEligibleChildFromExLover(SimLover exLover)
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
