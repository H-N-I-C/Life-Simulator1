using Game;

public class ScenarioZooSellOffer : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly double _targetPrice;

	private double _offerPrice;

	private DataZooBuyerPersonType _data;

	private int _gender;

	public ScenarioZooSellOffer(double targetPrice)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
