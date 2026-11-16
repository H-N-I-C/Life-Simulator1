public class ScenarioDealerCartelDrugSaleCollectShareFeedback : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerCartelMember _member;

	private readonly double _moneyReceived;

	private readonly double _drugCosts;

	private readonly SimDealerDrug _drug;

	public ScenarioDealerCartelDrugSaleCollectShareFeedback(SimDealerDrug drug, SimDealerCartelMember member, double moneyReceived, double drugCosts)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
