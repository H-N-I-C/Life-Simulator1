public class ScenarioPrisonReceiveGift : Scenario
{
	protected SimOccupationPrisoner _prisoner;

	protected DataPrisonGift _gift;

	protected string _giftString;

	protected bool _giftWillBeCrazy;

	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimPerson _friend;

	protected SimCommunard _communard;

	protected SimPerson _person;

	protected string _narrative;

	public ScenarioPrisonReceiveGift()
	{
	}

	public ScenarioPrisonReceiveGift(SimPerson thePerson)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
