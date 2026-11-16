using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioValentinesAnniversaryGift : Scenario
{
	private ValentinesAnniversaryActivity _activity;

	private Dictionary<string, DataAnniversaryGift> _giftDictionary;

	private BracketParser _parser;

	public ScenarioValentinesAnniversaryGift(ValentinesAnniversaryActivity activity)
	{
	}

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
