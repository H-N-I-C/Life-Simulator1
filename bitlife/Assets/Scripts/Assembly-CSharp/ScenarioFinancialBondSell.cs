public class ScenarioFinancialBondSell : Scenario
{
	protected SimInvestment _investment;

	protected SimBond _bond;

	protected double _offerValue;

	protected DataStreetPerson _randomPerson;

	public ScenarioFinancialBondSell(SimBond bond)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private bool GenerateRandomPersonAndCanAffordBond()
	{
		return false;
	}
}
