public class ScenarioLandlordRentLateInsist : Scenario
{
	protected SimTenant _theTenant;

	protected SimPropertyLeaseContract _theContract;

	protected bool _willPay;

	protected string _narrative;

	public ScenarioLandlordRentLateInsist(SimPropertyLeaseContract contract, bool willPay)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override void DismissSelected()
	{
	}
}
