using System.Collections.Generic;

public struct ChoiceArguments
{
	public Dictionary<ScenarioInputField, string> inputFieldStrings;

	public Dictionary<ScenarioDropDown, string> dropDownStrings;

	public Dictionary<ScenarioSlider, float> sliderNumbers;

	public ScenarioChoice originatingChoice;
}
