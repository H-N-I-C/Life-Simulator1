public class ScenarioLandlordNaturalDisaster : Scenario
{
	protected SimHouse _property;

	protected string _narrative;

	protected bool _destroyed;

	protected SimPropertyLeaseContract _contract;

	public ScenarioLandlordNaturalDisaster(SimPropertyLeaseContract contract)
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
