using System.Collections.Generic;
using Game;

public class ScenarioDealerSupplyRunPayloadPackaging : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerSupplyRun _supplyRun;

	private DataDealerDrugPackageType _packagingType;

	private List<DataDealerDrugPackageType> _packagingOptions;

	public ScenarioDealerSupplyRunPayloadPackaging(SimDealerSupplyRun supplyRun, List<DataDealerDrugPackageType> packagingOptions = null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
