using Game;

public class ScenarioDealerDealItemClassmateFeedback : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimClassmate _person;

	private readonly string _itemDescription;

	private readonly DataDealerDealActivityItem _itemData;

	public ScenarioDealerDealItemClassmateFeedback(SimClassmate person, string itemDescription, DataDealerDealActivityItem itemData)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
