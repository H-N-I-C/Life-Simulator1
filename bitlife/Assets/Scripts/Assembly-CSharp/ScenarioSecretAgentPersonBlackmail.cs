using System.Collections;

public class ScenarioSecretAgentPersonBlackmail : Scenario
{
	private readonly SimPerson _thePerson;

	private SimSecretAgent _theOperative;

	private SimSpyAgency _agency;

	private readonly string _intel;

	private string _narrative;

	private string _personString;

	public ScenarioSecretAgentPersonBlackmail(SimPerson thePerson, SimSecretAgent theOperative, string intel)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
