public class ScenarioOfferAlcohol : Scenario
{
	protected DataAlcohol _alcohol;

	protected int _alcoholScenarioCondition;

	public ScenarioOfferAlcohol()
	{
	}

	public ScenarioOfferAlcohol(bool clubOffer)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
