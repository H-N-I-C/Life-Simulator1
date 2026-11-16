using Game;

public class ScenarioDealerDealItemCoworkerFeedback : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimCoworker _person;

	private readonly string _itemDescription;

	private readonly DataDealerDealActivityItem _itemData;

	public ScenarioDealerDealItemCoworkerFeedback(SimCoworker person, string itemDescription, DataDealerDealActivityItem itemData)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
