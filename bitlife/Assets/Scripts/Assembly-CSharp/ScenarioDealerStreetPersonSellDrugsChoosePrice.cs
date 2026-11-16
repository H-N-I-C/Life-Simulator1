using System.Collections;

public class ScenarioDealerStreetPersonSellDrugsChoosePrice : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerDrug _drug;

	private readonly SimDealerStreetPerson _person;

	private int _desiredDoses;

	public ScenarioDealerStreetPersonSellDrugsChoosePrice(SimDealerStreetPerson person, SimDealerDrug drug)
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
