public class ScenarioLandlordTenantMoveOut : Scenario
{
	protected SimPropertyLeaseContract _leaseContract;

	protected string _reasonString;

	protected bool _forcedMoveOut;

	public ScenarioLandlordTenantMoveOut(SimPropertyLeaseContract leaseContract, string reasonString, bool forcedMoveOut = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
