using System;
using System.Collections.Generic;

public class ScenarioChoiceHandler
{
	private readonly ScenarioChoice _scenarioChoice;

	private readonly Func<ChoiceArguments, Scenario> _handler;

	public ScenarioChoiceHandler(ScenarioChoice scenarioChoice, Func<ChoiceArguments, Scenario> handler)
	{
	}

	public ScenarioChoice GetScenarioChoice()
	{
		return null;
	}

	public Scenario Invoke(Dictionary<ScenarioInputField, string> inputFieldStrings, Dictionary<ScenarioDropDown, string> dropDownStrings, Dictionary<ScenarioSlider, float> sliderNumbers)
	{
		return null;
	}
}
