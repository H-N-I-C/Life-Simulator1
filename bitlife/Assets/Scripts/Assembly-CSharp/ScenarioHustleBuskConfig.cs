using System;
using System.Collections;

[Serializable]
public class ScenarioHustleBuskConfig : Scenario
{
	protected SimSpecialJobHustler _currentSpecialJobHustler;

	protected bool _useDefaultSettings;

	public ScenarioHustleBuskConfig(SimSpecialJobHustler currentSpecialJobHustler, bool useDefaultSettings = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
