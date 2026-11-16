using Game;

public class ScenarioDealerDealItemFriendsLoversFamilyFeedback : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimPerson _person;

	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimLover _exlover;

	protected SimChild _child;

	protected SimPerson _friend;

	private readonly string _item;

	private readonly SimDealerDrug _drug;

	private readonly int _drugDoses;

	private readonly double _pricePerDose;

	private readonly string _itemDescription;

	private readonly DataDealerDealActivityItem _itemData;

	private readonly string _personType;

	private string _personRelationString;

	public ScenarioDealerDealItemFriendsLoversFamilyFeedback(SimPerson person, string item, DataDealerDealActivityItem itemData, string personType)
	{
	}

	public ScenarioDealerDealItemFriendsLoversFamilyFeedback(SimPerson person, SimDealerDrug drug, int drugDoses, double pricePerDose, string personType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
