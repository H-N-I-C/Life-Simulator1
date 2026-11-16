using System.Collections;

public class ScenarioRelationshipGift : Scenario
{
	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimPerson _friend;

	protected SimSeniorPerson _seniorFriend;

	protected SimPerson _person;

	protected string _personNameScenario;

	protected string _personNameNarrative;

	protected ArrayList _giftArray;

	protected bool _requireRewardedVideo;

	public ScenarioRelationshipGift(SimParent theParent)
	{
	}

	public ScenarioRelationshipGift(SimSibling theSibling)
	{
	}

	public ScenarioRelationshipGift(SimLover theLover)
	{
	}

	public ScenarioRelationshipGift(SimChild theChild)
	{
	}

	public ScenarioRelationshipGift(SimPerson theFriend)
	{
	}

	public ScenarioRelationshipGift(SimSeniorPerson seniorFriend)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
