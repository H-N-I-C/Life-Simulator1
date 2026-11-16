public class ScenarioLandlordSquatterPossessesHome : Scenario
{
	protected SimPropertyLeaseContract _contract;

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

	public static bool IsEligibleForPossessionClaim()
	{
		return false;
	}
}
