public class ScenarioRelationshipConcert : Scenario
{
	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimPerson _friend;

	protected SimPerson _person;

	protected string _personNameScenario;

	protected string _personNameNarrative;

	protected bool _requireRewardedVideo;

	public ScenarioRelationshipConcert(SimParent theParent)
	{
	}

	public ScenarioRelationshipConcert(SimSibling theSibling)
	{
	}

	public ScenarioRelationshipConcert(SimLover theLover)
	{
	}

	public ScenarioRelationshipConcert(SimChild theChild)
	{
	}

	public ScenarioRelationshipConcert(SimPerson theFriend)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
