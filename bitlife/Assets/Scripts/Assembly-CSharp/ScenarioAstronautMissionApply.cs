public class ScenarioAstronautMissionApply : Scenario
{
	private string _narrative;

	private readonly SimSpecialJobAstronaut _specialJob;

	private readonly SimSpaceMission _mission;

	public ScenarioAstronautMissionApply(SimSpecialJobAstronaut special, SimSpaceMission missionItem)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private bool GetWillBeAccepted(ref string reason)
	{
		return false;
	}
}
