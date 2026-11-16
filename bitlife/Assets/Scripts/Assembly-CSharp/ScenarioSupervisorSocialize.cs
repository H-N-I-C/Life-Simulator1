public class ScenarioSupervisorSocialize : Scenario
{
	protected string _activity;

	protected SimOccupationEmployee _employee;

	protected SimCoworker _supervisor;

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
