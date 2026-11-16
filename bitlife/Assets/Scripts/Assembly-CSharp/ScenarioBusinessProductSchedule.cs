public class ScenarioBusinessProductSchedule : Scenario
{
	protected MiniGameDataBusinessProductSchedule _gameData;

	private SimBusiness _business;

	private SimProduct _product;

	private string _fixedCostString;

	private string _marketingCostString;

	private string _priceString;

	protected bool _isOnboarding;

	protected bool _isPush;

	public ScenarioBusinessProductSchedule(SimProduct product, bool isPush, bool isOnboarding = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void MiniGameCompleted()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
