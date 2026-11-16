public class ScenarioFriendSpendTime : Scenario
{
	protected SimPerson _friend;

	protected SimSeniorPerson _seniorFriend;

	protected string _narrative;

	public ScenarioFriendSpendTime(SimPerson theFriend)
	{
	}

	public ScenarioFriendSpendTime(SimSeniorPerson seniorFriend)
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
