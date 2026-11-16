public class ScenarioFailingEyesight : Scenario
{
	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}
}
