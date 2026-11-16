public class ScenarioDealerManufactureCutDrugProfile : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerDrug _baseDrug;

	private readonly SimDealerDrugCut _cutDrug;

	public ScenarioDealerManufactureCutDrugProfile(SimDealerDrug baseDrug, SimDealerDrugCut cutDrug)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
