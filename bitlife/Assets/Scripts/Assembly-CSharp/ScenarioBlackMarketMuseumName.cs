using System.Collections;

public class ScenarioBlackMarketMuseumName : Scenario
{
	private readonly SimMuseum _newMuseum;

	private string _narrative;

	private readonly bool _isNewMuseum;

	public ScenarioBlackMarketMuseumName(SimMuseum newMuseum, bool isNewMuseum)
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
