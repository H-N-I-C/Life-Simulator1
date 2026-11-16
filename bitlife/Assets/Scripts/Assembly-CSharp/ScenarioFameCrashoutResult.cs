using Game;

public class ScenarioFameCrashoutResult : Scenario
{
	private readonly DataCrashoutScenario _scenarioData;

	private string _parsedBody;

	private string _parsedLog;

	private BracketParser _parser;

	public ScenarioFameCrashoutResult(DataCrashoutScenario scenarioData)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	private Scenario BuildFollowUp(DataCrashoutScenario scenarioData)
	{
		return null;
	}
}
