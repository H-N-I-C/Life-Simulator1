public class ScenarioFinancialStockTip : Scenario
{
	protected SimPerson _person;

	protected DataFinancialStockTipScenario _stockTip;

	protected SimStock _stock;

	protected BracketParser _parser;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
