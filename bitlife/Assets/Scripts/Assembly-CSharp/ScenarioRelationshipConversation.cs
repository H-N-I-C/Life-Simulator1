public class ScenarioRelationshipConversation : Scenario
{
	protected int _audience;

	protected SimChild _audienceChild;

	protected SimLover _audienceLover;

	protected SimParent _audienceParent;

	protected SimSibling _audienceSibling;

	protected SimPerson _audienceFriend;

	protected SimSeniorPerson _audienceSeniorFriend;

	protected string _narrative;

	public ScenarioRelationshipConversation(SimChild child)
	{
	}

	public ScenarioRelationshipConversation(SimLover lover)
	{
	}

	public ScenarioRelationshipConversation(SimParent parent)
	{
	}

	public ScenarioRelationshipConversation(SimSibling sibling)
	{
	}

	public ScenarioRelationshipConversation(SimPerson friend)
	{
	}

	public ScenarioRelationshipConversation(SimSeniorPerson seniorFriend)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
