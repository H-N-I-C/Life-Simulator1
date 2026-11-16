using System.Collections.Generic;

public class ScenarioFinancialAssetSell : Scenario
{
	protected MiniGameDataFinancialAssetSell _gameData;

	protected SimInvestment _investment;

	protected SimStock _stock;

	protected SimFund _fund;

	protected SimCrypto _crypto;

	public ScenarioFinancialAssetSell(SimStock stock)
	{
	}

	public ScenarioFinancialAssetSell(SimFund fund)
	{
	}

	public ScenarioFinancialAssetSell(SimCrypto crypto)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private bool _003CRefreshScenario_003Eb__8_0(KeyValuePair<int, SimPortfolioAnnualReport> x)
	{
		return false;
	}

	private bool _003CRefreshScenario_003Eb__8_2(SimInvestmentAnnualReport x)
	{
		return false;
	}

	private bool _003CRefreshScenario_003Eb__8_1(KeyValuePair<int, SimPortfolioAnnualReport> x)
	{
		return false;
	}

	private bool _003CRefreshScenario_003Eb__8_3(SimInvestmentAnnualReport x)
	{
		return false;
	}
}
