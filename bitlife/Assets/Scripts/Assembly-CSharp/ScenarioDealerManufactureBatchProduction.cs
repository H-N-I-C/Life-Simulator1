public class ScenarioDealerManufactureBatchProduction : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerDrug _baseDrug;

	private readonly SimDealerDrugCut _cutDrug;

	private readonly string _cutIngredient;

	private readonly int _cutPercentage;

	private MiniGameDataDealerDrugCutProduction _gameData;

	public ScenarioDealerManufactureBatchProduction(SimDealerDrug baseDrug, string cutIngredient, int cutPercentage)
	{
	}

	public ScenarioDealerManufactureBatchProduction(SimDealerDrug baseDrug, SimDealerDrugCut cutDrug)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void MiniGameCompleted()
	{
	}
}
