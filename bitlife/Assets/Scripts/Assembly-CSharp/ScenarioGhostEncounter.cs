public class ScenarioGhostEncounter : Scenario
{
	protected SimGhost _ghost;

	protected bool _didSummon;

	public ScenarioGhostEncounter()
	{
	}

	public ScenarioGhostEncounter(SimGhost theGhost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimGhost GetGhost()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
