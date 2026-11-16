using System.Collections.Generic;
using Game;

public class ScenarioDealerSupplyRunDropOff : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerSupplyRun _supplyRun;

	private DataDealerDrugDropOffLocation _dropLocation;

	private List<DataDealerDrugDropOffLocation> _dropLocations;

	public ScenarioDealerSupplyRunDropOff(SimDealerSupplyRun supplyRun, List<DataDealerDrugDropOffLocation> dropLocations = null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
