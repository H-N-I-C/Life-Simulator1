public class ScenarioZooEmployeeHiringBackgroundCheckResults : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooEmployee _candidate;

	private BracketParser _parser;

	private string _candidateType;

	public ScenarioZooEmployeeHiringBackgroundCheckResults(SimZooEmployee candidate)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
