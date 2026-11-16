using System.Collections;

public class ScenarioOutdoorMountedFishGift : Scenario
{
	private readonly SimOutdoorMountedFish _fish;

	private ArrayList _possibleRecipientArray;

	public ScenarioOutdoorMountedFishGift(SimOutdoorMountedFish fish)
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
