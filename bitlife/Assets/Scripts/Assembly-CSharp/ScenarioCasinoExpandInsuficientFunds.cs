public class ScenarioCasinoExpandInsuficientFunds : Scenario
{
	private string _narrative;

	private SimCasino _casino;

	private readonly double _cost;

	private readonly CASINO_TYPE _expandType;

	public ScenarioCasinoExpandInsuficientFunds(CASINO_TYPE expandType, double cost)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
