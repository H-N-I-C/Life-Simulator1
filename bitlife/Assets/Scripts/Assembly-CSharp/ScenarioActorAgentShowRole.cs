public class ScenarioActorAgentShowRole : Scenario
{
	protected SimActorShowRole _role;

	protected SimActorShow _show;

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
