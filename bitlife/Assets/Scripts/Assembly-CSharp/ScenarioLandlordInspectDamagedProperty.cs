public class ScenarioLandlordInspectDamagedProperty : Scenario
{
	protected SimPropertyLeaseContract _contract;

	protected SimTenant _tenant;

	protected SimHouse _property;

	protected double _repairCost;

	protected string _logString;

	protected float _originalCondition;

	protected string _scenarioString;

	protected bool _firstPass;

	public ScenarioLandlordInspectDamagedProperty(SimPropertyLeaseContract contract, double repairCost = 0.0, string logString = null, string scenarioString = null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
