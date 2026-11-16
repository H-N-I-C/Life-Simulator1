public class ScenarioLandlordInspectTenantActivity : Scenario
{
	protected SimPropertyLeaseContract _contract;

	protected SimTenant _tenant;

	protected SimHouse _property;

	protected bool _badThingFound;

	protected string _activityString;

	protected string _logActivityString;

	public ScenarioLandlordInspectTenantActivity(SimPropertyLeaseContract contract, bool badThingFound)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
