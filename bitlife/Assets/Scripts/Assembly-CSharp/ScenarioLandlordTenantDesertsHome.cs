public class ScenarioLandlordTenantDesertsHome : Scenario
{
	protected SimPropertyLeaseContract _contract;

	protected SimTenant _tenant;

	protected SimHouse _property;

	protected string _narrative;

	protected double _belongingValue;

	protected string _belongingsString;

	protected bool _singularGift;

	public ScenarioLandlordTenantDesertsHome(SimPropertyLeaseContract contract)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static bool IsEligibleForDesertingHome(SimPropertyLeaseContract contract)
	{
		return false;
	}
}
