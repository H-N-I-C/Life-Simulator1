using System;
using System.Collections;

[Serializable]
public class ScenarioHustlePerformConfig : Scenario
{
	protected SimSpecialJobHustler _currentSpecialJobHustler;

	protected bool _useDefaultSettings;

	public ScenarioHustlePerformConfig(SimSpecialJobHustler currentSpecialJobHustler, bool useDefaultSettings = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
