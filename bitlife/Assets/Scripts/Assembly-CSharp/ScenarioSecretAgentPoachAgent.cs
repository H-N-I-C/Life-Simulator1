using System.Collections;

public class ScenarioSecretAgentPoachAgent : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgent _agent;

	private readonly SimRivalSpyAgency _rivalAgency;

	private readonly bool _fromDataBreach;

	public ScenarioSecretAgentPoachAgent(SimSecretAgent agent, SimRivalSpyAgency rivalAgency, bool fromDataBreach = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
