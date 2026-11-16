public class ScenarioBlackMarketWildlifeSmugglerPurchase : Scenario
{
	private readonly SimPet _pet;

	private readonly SimBlackMarketVendor _vendor;

	private string _narrative;

	private float _cost;

	private readonly bool _isHaggle;

	public ScenarioBlackMarketWildlifeSmugglerPurchase(SimBlackMarketVendor vendor, SimPet pet)
	{
	}

	public ScenarioBlackMarketWildlifeSmugglerPurchase(SimBlackMarketVendor vendor, SimPet pet, float cost, bool isHaggle)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override void CountdownTimerExpired()
	{
	}

	public override void CountdownTimerUpdated(int currentTimerValue)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
