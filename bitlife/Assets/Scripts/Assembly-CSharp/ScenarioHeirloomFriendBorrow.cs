public class ScenarioHeirloomFriendBorrow : Scenario
{
	protected SimPerson _friend;

	protected SimHeirloom _heirloom;

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

	public static SimHeirloom GetHeirloom()
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
