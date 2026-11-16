using System.Collections;

public class ScenarioHouseSell : Scenario
{
	protected SimHouse _house;

	protected bool _reaction;

	protected bool _spouseFeedbackYes;

	protected bool _spouseFeedbackNo;

	protected SimTenant _tenant;

	public ScenarioHouseSell(SimHouse theHouse, bool isReaction, SimPerson yourSpouse = null, bool wantsToSell = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public static ArrayList SellPriceArrayForHouse(SimHouse house)
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
