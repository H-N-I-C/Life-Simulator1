public class ScenarioFriendAdvice : Scenario
{
	protected SimPerson _friend;

	protected string _narrative;

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

	public static DataFriendAdvice GetAdviceForFriend(SimPerson theFriend)
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
