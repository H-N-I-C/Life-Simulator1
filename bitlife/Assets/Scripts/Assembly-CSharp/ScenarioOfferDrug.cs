public class ScenarioOfferDrug : Scenario
{
	protected DataDrug _offeringDrug;

	protected string _offeringDrugNickname;

	protected int _drugScenarioCondition;

	public ScenarioOfferDrug()
	{
	}

	public ScenarioOfferDrug(bool clubOffer)
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

	public override string EmojiString()
	{
		return null;
	}
}
