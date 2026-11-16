using System.Collections;

public class ScenarioCasinoCollectibleGift : Scenario
{
	private readonly SimCasinoCollectible _collectible;

	private ArrayList _possibleRecipientArray;

	public ScenarioCasinoCollectibleGift(SimCasinoCollectible collectible)
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
