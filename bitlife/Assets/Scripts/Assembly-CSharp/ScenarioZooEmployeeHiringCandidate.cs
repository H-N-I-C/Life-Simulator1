public class ScenarioZooEmployeeHiringCandidate : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly ZOO_EMPLOYEE_TYPE _type;

	private SimZooEmployee _candidate;

	private string _candidateType;

	private BracketParser _parser;

	public ScenarioZooEmployeeHiringCandidate(ZOO_EMPLOYEE_TYPE type)
	{
	}

	public ScenarioZooEmployeeHiringCandidate(SimZooEmployee candidate)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
