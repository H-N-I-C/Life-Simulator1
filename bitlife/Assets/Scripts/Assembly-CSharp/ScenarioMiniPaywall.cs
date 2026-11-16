using Backend;

public class ScenarioMiniPaywall : Scenario
{
	private MiniGameDataMiniPaywall _gameData;

	private readonly int _paywallType;

	private readonly int _origin;

	private readonly bool _isFeatured;

	private readonly Scenario _purchaseFollowUpScenario;

	private WebShopStatus webShopType;

	public SimPregnancy ThePregnancy;

	public ScenarioMiniPaywall(int paywallType, int origin, bool isFeatured = false)
	{
	}

	public ScenarioMiniPaywall(int paywallType, int origin, Scenario purchaseFollowUpScenario, bool isFeatured = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void PurchaseWebShop()
	{
	}
}
