public class ScenarioBusinessTeamRequest : Scenario
{
	protected string _narrative;

	protected SimBusiness _theBusiness;

	protected SimEmployee _theEmployee;

	protected DataBusinessTeamRequestScenario _scenarioData;

	protected int _cost;

	public ScenarioBusinessTeamRequest()
	{
	}

	public ScenarioBusinessTeamRequest(DataBusinessTeamRequestScenario scenarioData)
	{
	}

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
