public class ScenarioOpportunitySmuggle : Scenario
{
	protected int _prizeAmount;

	protected DataCountry _destinationCountry;

	protected string _smuggleMedium;

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

	public override string EmojiString()
	{
		return null;
	}
}
