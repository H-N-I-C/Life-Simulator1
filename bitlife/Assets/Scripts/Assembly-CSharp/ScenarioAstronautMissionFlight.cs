public class ScenarioAstronautMissionFlight : Scenario
{
	private string _narrative;

	private readonly SimSpecialJobAstronaut _specialJob;

	private readonly SimSpaceMission _mission;

	private DataAstronautFlightScenario _data;

	private BracketParser _parser;

	public ScenarioAstronautMissionFlight(SimSpecialJobAstronaut specialJob, SimSpaceMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
