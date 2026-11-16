public class ScenarioSecretAgentInfiltrateCaughtFeedback : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimRivalSpyAgency _rivalAgency;

	private readonly SECRET_AGENT_INFILTRATION_EXTRACTION_TYPE _caughtExtractionType;

	private double _fundsStolen;

	public ScenarioSecretAgentInfiltrateCaughtFeedback(SimRivalSpyAgency rivalAgency, SECRET_AGENT_INFILTRATION_EXTRACTION_TYPE caughtExtractionType, double fundsStolen = 0.0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
