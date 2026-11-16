using System.Collections;

public class ScenarioRacingCollectibleGift : Scenario
{
	private readonly SimRacingCollectible _collectible;

	private ArrayList _possibleRecipientArray;

	public ScenarioRacingCollectibleGift(SimRacingCollectible collectible)
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
