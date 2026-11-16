using System.Collections;

public class ScenarioInstrumentSell : Scenario
{
	protected SimMusicInstrumentPhysical _theInstrument;

	public ScenarioInstrumentSell(SimMusicInstrumentPhysical theInstrument)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public void TryToSellInstrumentForPrice(float price)
	{
	}

	public static ArrayList SellPriceArrayForInstrument(SimMusicInstrumentPhysical instrument)
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
