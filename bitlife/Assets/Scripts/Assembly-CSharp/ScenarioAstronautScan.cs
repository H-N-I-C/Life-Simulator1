using System.Collections;
using System.Collections.Generic;

public class ScenarioAstronautScan : Scenario
{
	private string _narrative;

	private List<string> _planetNames;

	private readonly SimSpecialJobAstronaut _specialJob;

	public ScenarioAstronautScan(SimSpecialJobAstronaut specialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
