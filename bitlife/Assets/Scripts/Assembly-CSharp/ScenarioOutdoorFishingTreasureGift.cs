using System.Collections;

public class ScenarioOutdoorFishingTreasureGift : Scenario
{
	private readonly SimOutdoorFishingTreasure _treasure;

	private ArrayList _possibleRecipientArray;

	public ScenarioOutdoorFishingTreasureGift(SimOutdoorFishingTreasure treasure)
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
