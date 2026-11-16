public class ScenarioFinancialAdvice : Scenario
{
	protected SimPerson _advisor;

	protected SimInvestmentMarket _investmentMarket;

	public bool givesAdvice;

	public ScenarioFinancialAdvice(SimPerson advisor, SimInvestmentMarket market)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
