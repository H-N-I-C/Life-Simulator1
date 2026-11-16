using System.Collections;

public class ScenarioDealerSellDrugsChoosePrice : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerDrug _drug;

	private readonly SimPerson _person;

	private int _desiredDoses;

	private readonly string _personRelationshipString;

	public ScenarioDealerSellDrugsChoosePrice(SimPerson person, string relationship, SimDealerDrug drug)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
