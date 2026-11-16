using System.Collections;

public class ScenarioZooEmployeeHiringSalaryOffer : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooEmployee _candidate;

	private readonly bool _backgroundCheckPerformed;

	public ScenarioZooEmployeeHiringSalaryOffer(SimZooEmployee candidate, bool backgroundCheckPerformed)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
