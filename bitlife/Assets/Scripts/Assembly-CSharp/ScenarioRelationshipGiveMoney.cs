using System.Collections;

public class ScenarioRelationshipGiveMoney : Scenario
{
	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimChild _child;

	private readonly SimSeniorPerson _senior;

	protected SimPerson _person;

	protected string _personNameScenario;

	protected string _personNameNarrative;

	private bool _isRelative;

	public ScenarioRelationshipGiveMoney(SimParent theParent)
	{
	}

	public ScenarioRelationshipGiveMoney(SimSibling theSibling)
	{
	}

	public ScenarioRelationshipGiveMoney(SimLover theLover)
	{
	}

	public ScenarioRelationshipGiveMoney(SimSeniorPerson seniorFriend)
	{
	}

	public ScenarioRelationshipGiveMoney(SimChild theChild)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
