public class ScenarioLandlordRemoveSquatter : Scenario
{
	protected SimPropertyLeaseContract _contract;

	protected bool _videoAvailable;

	public ScenarioLandlordRemoveSquatter(SimPropertyLeaseContract contract)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private bool VideoAvailable()
	{
		return false;
	}
}
