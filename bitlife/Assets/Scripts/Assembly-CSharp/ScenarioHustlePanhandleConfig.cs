using System;
using System.Collections;

[Serializable]
public class ScenarioHustlePanhandleConfig : Scenario
{
	protected SimSpecialJobHustler _currentSpecialJobHustler;

	protected SimChild _eligibleChild;

	protected SimPet _eligiblePet;

	protected bool _useDefaultSettings;

	public ScenarioHustlePanhandleConfig(SimSpecialJobHustler currentSpecialJobHustler, bool useDefaultSettings = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
