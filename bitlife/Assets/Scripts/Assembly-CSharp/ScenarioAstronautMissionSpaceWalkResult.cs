using System.Collections.Generic;

public class ScenarioAstronautMissionSpaceWalkResult : Scenario
{
	private string _narrative;

	private readonly SimSpaceMission _mission;

	private readonly List<string> _listRepairedItems;

	public ScenarioAstronautMissionSpaceWalkResult(SimSpaceMission mission, List<string> listRepairedItems)
	{
	}

	public override void RefreshScenario()
	{
	}
}
