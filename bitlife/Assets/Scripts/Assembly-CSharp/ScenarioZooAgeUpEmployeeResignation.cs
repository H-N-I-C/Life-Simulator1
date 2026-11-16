public class ScenarioZooAgeUpEmployeeResignation : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private SimZooEmployee _employee;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private static SimZooEmployee GetDisgruntledEmployee()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
