using System.Collections;

public class ScenarioExLoverCall : Scenario
{
	protected SimLover _exLover;

	private bool ShouldForceExLoverRekindle()
	{
		return false;
	}

	public ScenarioExLoverCall(SimLover theExLover)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
