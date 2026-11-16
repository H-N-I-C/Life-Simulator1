public class ScenarioLandlordMaintenanceRequest : Scenario
{
	protected SimPropertyLeaseContract _contract;

	protected SimTenant _tenant;

	protected SimHouse _property;

	protected double _cost;

	protected bool _tenantInsists;

	protected DataLandlordMaintenanceRequestScenario _requestScenario;

	protected string _bodySnippet;

	protected string _feedbackPresentString;

	protected string _feedbackPastString;

	public ScenarioLandlordMaintenanceRequest(SimPropertyLeaseContract contract)
	{
	}

	public ScenarioLandlordMaintenanceRequest(SimPropertyLeaseContract contract, string feedbackPresentString, string feedbackPastString, string bodySnippet, double cost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
