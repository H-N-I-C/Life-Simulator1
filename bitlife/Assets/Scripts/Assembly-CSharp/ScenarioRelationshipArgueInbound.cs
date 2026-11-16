public class ScenarioRelationshipArgueInbound : Scenario
{
	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimChild _child;

	protected SimPerson _friend;

	protected SimPerson _person;

	protected SimPet _pet;

	protected SimMagazineOpportunity _magazineOpportunity;

	protected DataGift _badGift;

	protected SimJewel _jewel;

	protected string _jewelString;

	protected SimTeacher _teacher;

	protected int _schoolType;

	protected SimMusicInstrumentPhysical _instrument;

	protected int _reason;

	protected string _personNameScenario;

	protected string _personNameNarrative;

	protected string _narrative;

	public ScenarioRelationshipArgueInbound(SimParent theParent, SimPet thePet, int theReason)
	{
	}

	public ScenarioRelationshipArgueInbound(SimParent theParent, SimMagazineOpportunity theOpportunity)
	{
	}

	public ScenarioRelationshipArgueInbound(SimSibling theSibling, SimPet thePet, int theReason)
	{
	}

	public ScenarioRelationshipArgueInbound(SimChild theChild, SimPet thePet, int theReason)
	{
	}

	public ScenarioRelationshipArgueInbound(SimParent theParent, DataGift theGift)
	{
	}

	public ScenarioRelationshipArgueInbound(SimSibling theSibling, DataGift theGift)
	{
	}

	public ScenarioRelationshipArgueInbound(SimChild theChild, DataGift theGift)
	{
	}

	public ScenarioRelationshipArgueInbound(SimPerson theFriend, DataGift theGift)
	{
	}

	public ScenarioRelationshipArgueInbound(SimParent theParent, SimJewel theJewel)
	{
	}

	public ScenarioRelationshipArgueInbound(SimSibling theSibling, SimJewel theJewel)
	{
	}

	public ScenarioRelationshipArgueInbound(SimChild theChild, SimJewel theJewel)
	{
	}

	public ScenarioRelationshipArgueInbound(SimPerson theFriend, SimJewel theJewel)
	{
	}

	public ScenarioRelationshipArgueInbound(SimParent theParent, SimTeacher theTeacher, int theSchoolType)
	{
	}

	public ScenarioRelationshipArgueInbound(SimParent theParent, int theReason)
	{
	}

	public ScenarioRelationshipArgueInbound(SimParent theParent, SimMusicInstrumentPhysical instrument)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
