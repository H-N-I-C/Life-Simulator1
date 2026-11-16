public class ScenarioFriendRumorInbound : Scenario
{
	protected SimPerson _friend;

	protected bool _isRetaliatory;

	protected string _narrative;

	public ScenarioFriendRumorInbound(SimPerson theFriend)
	{
	}

	public ScenarioFriendRumorInbound()
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

	public static SimPerson GetFriend()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
