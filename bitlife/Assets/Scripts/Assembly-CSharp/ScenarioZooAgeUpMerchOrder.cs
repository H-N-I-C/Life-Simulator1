public class ScenarioZooAgeUpMerchOrder : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private SimZooGiftShopItem _item;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private static SimZooGiftShopItem GetRandomDepletedGiftShopItem()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
