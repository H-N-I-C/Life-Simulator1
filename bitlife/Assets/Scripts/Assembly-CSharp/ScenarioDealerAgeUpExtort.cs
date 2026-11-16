public class ScenarioDealerAgeUpExtort : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private DataStreetPerson _person;

	private double _amount;

	private double _reward;

	private SimDealerCartelMember _askingMember;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void FirebaseLogIfNull(bool objectIsNull, string msg)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
