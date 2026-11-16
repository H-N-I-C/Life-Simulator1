public class ScenarioZooAgeUpCustomerService : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
