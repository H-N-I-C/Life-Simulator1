public class ScenarioFriendHookup : Scenario
{
	protected SimPerson _friend;

	protected SimSeniorPerson _seniorFriend;

	protected SimLover _loverCandidate;

	public ScenarioFriendHookup(SimPerson theFriend)
	{
	}

	public ScenarioFriendHookup(SimSeniorPerson seniorFriend)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	protected string GetFriendRelationshipStatusForDisplay()
	{
		return null;
	}
}
