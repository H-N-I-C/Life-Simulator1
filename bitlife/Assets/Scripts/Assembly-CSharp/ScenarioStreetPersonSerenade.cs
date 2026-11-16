using System.Collections;

public class ScenarioStreetPersonSerenade : Scenario
{
	protected string _narrative;

	protected SimStreetPerson _thePerson;

	protected SimSpecialJobHustler _currentSpecialJobHustler;

	public ScenarioStreetPersonSerenade(SimStreetPerson thePerson)
	{
	}

	private ArrayList BuildPlayableInstrumentArray()
	{
		return null;
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
