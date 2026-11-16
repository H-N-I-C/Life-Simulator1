using System.Collections;
using Game;

public class ScenarioSecretAgentOperativeAssassinationMoreMoney : Scenario
{
	private string _narrative;

	private SimSpyAgency _agency;

	private readonly SimSecretAgent _agent;

	private readonly SimMurderTarget _target;

	private readonly DataSecretAgentAssassinationMethod _murderMethod;

	private readonly float _amount;

	public ScenarioSecretAgentOperativeAssassinationMoreMoney(SimSecretAgent agent, SimMurderTarget target, DataSecretAgentAssassinationMethod murderMethod, float amount)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
