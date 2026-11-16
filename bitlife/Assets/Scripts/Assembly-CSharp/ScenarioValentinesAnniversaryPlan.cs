using System.Collections;
using System.Collections.Generic;

public class ScenarioValentinesAnniversaryPlan : Scenario
{
	private SimLover _lover;

	private Dictionary<string, ValentinesAnniversaryActivity.METHOD> _methodDictionary;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
