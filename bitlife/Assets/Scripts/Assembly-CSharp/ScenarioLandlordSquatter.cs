public class ScenarioLandlordSquatter : Scenario
{
	protected SimPropertyLeaseContract _contract;

	protected bool _videoAvailable;

	protected bool _isPush;

	protected bool _dueToRentIncrease;

	protected string _logColorString;

	public ScenarioLandlordSquatter(bool isPush, SimPropertyLeaseContract contract, bool dueToRentIncrease)
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

	public static bool IsEligibleForSquatter(SimPropertyLeaseContract contract)
	{
		return false;
	}
}
