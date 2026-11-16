public class ScenarioRelationshipInsult : Scenario
{
	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimPerson _friend;

	protected SimSeniorPerson _seniorFriend;

	protected SimPerson _person;

	private SimAstronaut _astronaut;

	protected string _narrative;

	private bool _useBracketParser;

	private string _relationshipTemplate;

	public ScenarioRelationshipInsult(SimParent theParent)
	{
	}

	public ScenarioRelationshipInsult(SimSibling theSibling)
	{
	}

	public ScenarioRelationshipInsult(SimLover theLover)
	{
	}

	public ScenarioRelationshipInsult(SimChild theChild)
	{
	}

	public ScenarioRelationshipInsult(SimPerson theFriend)
	{
	}

	public ScenarioRelationshipInsult(SimSeniorPerson seniorFriend)
	{
	}

	public ScenarioRelationshipInsult(SimPerson person, string relationshipTemplate)
	{
	}

	public ScenarioRelationshipInsult(SimAstronaut astronaut)
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
