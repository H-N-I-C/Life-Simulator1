public class ScenarioChildComesOut : Scenario
{
	protected SimChild _child;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}

	public static SimChild GetComingOutChild()
	{
		return null;
	}
}
