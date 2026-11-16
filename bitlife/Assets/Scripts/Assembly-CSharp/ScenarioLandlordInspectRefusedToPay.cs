public class ScenarioLandlordInspectRefusedToPay : Scenario
{
	protected SimPropertyLeaseContract _contract;

	protected string _narrative;

	protected double _repairCost;

	protected float _originalCondition;

	public ScenarioLandlordInspectRefusedToPay(SimPropertyLeaseContract contract, double repairCost, float originalCondition)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
