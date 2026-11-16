public class ScenarioAstronautMissionDirectorWarning : Scenario
{
	private readonly SimSpecialJobAstronaut _special;

	private readonly SimAstronaut _director;

	private string _narrative;

	private readonly int _reprimandType;

	public ScenarioAstronautMissionDirectorWarning(SimSpecialJobAstronaut special, SimSpaceMission mission, int reprimandType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
