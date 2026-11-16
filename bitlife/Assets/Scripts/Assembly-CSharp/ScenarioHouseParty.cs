public class ScenarioHouseParty : Scenario
{
	protected SimHouse _house;

	protected int _partyOutcome;

	protected int _overdoseGuests;

	protected string _overdoseDrugName;

	protected string _narrative;

	protected bool _successfulParty;

	public ScenarioHouseParty(SimHouse theHouse)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static bool CanPartyInTheSameCountry(SimHouse house)
	{
		return false;
	}
}
