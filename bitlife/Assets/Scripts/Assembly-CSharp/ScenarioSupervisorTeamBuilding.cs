public class ScenarioSupervisorTeamBuilding : Scenario
{
	protected string _activity;

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
