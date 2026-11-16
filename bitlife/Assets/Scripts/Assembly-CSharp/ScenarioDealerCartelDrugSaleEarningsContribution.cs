using System.Collections;

public class ScenarioDealerCartelDrugSaleEarningsContribution : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerCartelMember _receivingMember;

	private readonly double _moneyReceived;

	private readonly double _totalStreetValue;

	private readonly double _totalDrugManufacturingCost;

	private readonly SimDealerDrug _drug;

	public ScenarioDealerCartelDrugSaleEarningsContribution(SimDealerDrug drug, SimDealerCartelMember receivingMember, double moneyReceived, double totalStreetValue, double totalDrugManufacturingCost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
