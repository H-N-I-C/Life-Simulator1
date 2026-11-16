public class ScenarioFriendDrug : Scenario
{
	protected SimPerson _friend;

	protected DataDrug _offeringDrug;

	protected string _offeringDrugNickname;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimPerson GetFriend()
	{
		return null;
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
