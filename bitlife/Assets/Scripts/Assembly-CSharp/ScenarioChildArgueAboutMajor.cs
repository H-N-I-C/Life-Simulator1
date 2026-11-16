using System.Collections;

public class ScenarioChildArgueAboutMajor : Scenario
{
	protected SimChild _child;

	protected bool _tuitionContext;

	protected ArrayList _majorArray;

	public ScenarioChildArgueAboutMajor(SimChild theChild, bool theTuitionContext)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}
}
