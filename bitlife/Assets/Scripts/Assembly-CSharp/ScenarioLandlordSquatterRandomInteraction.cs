public class ScenarioLandlordSquatterRandomInteraction : Scenario
{
	private readonly SimPropertyLeaseContract _contract;

	private bool _videoAvailable;

	private string _logColorString;

	public ScenarioLandlordSquatterRandomInteraction(SimPropertyLeaseContract contract)
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

	public static bool IsEligibleForRandomSquatterInteract(SimPropertyLeaseContract contract)
	{
		return false;
	}
}
