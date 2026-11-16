public class ScenarioAstronautMissionExperiment : Scenario
{
	private string _narrative;

	private readonly SimSpecialJobAstronaut _specialJob;

	private readonly SimSpaceMission _mission;

	private int _riskScore;

	private int _performanceImpact;

	private int _deceptionScore;

	private BracketParser _parser;

	private DataAstronautExperimentScenario _data;

	private string _snippet;

	private string _logVersion;

	private string _feedbackLog;

	public ScenarioAstronautMissionExperiment(SimSpaceMission mission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private Scenario SuccessScenario()
	{
		return null;
	}

	private Scenario RefuseScenario()
	{
		return null;
	}

	private Scenario DeceptionScenario()
	{
		return null;
	}

	private Scenario DeceptionScenarioCaught()
	{
		return null;
	}
}
