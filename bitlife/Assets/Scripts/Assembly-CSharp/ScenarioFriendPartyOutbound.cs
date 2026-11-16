public class ScenarioFriendPartyOutbound : Scenario
{
	protected SimPerson _friend;

	protected SimSeniorPerson _seniorFriend;

	protected string _narrative;

	public ScenarioFriendPartyOutbound(SimPerson theFriend)
	{
	}

	public ScenarioFriendPartyOutbound(SimSeniorPerson seniorFriend)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	protected string GetFriendRelationshipStatusForDisplay()
	{
		return null;
	}
}
