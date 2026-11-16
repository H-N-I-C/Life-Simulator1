using System.Collections.Generic;

public class ScenarioFinancialAssetBuy : Scenario
{
	protected MiniGameDataFinancialAssetBuy _gameData;

	protected SimStock _stock;

	protected SimInvestment _investment;

	protected SimFund _fund;

	protected SimCrypto _crypto;

	protected string _exchangeRateString;

	protected bool _isPush;

	public ScenarioFinancialAssetBuy(SimStock stock, bool isPush = false)
	{
	}

	public ScenarioFinancialAssetBuy(SimFund fund)
	{
	}

	public ScenarioFinancialAssetBuy(SimCrypto crypto)
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

	private bool _003CRefreshScenario_003Eb__10_0(KeyValuePair<int, SimPortfolioAnnualReport> x)
	{
		return false;
	}

	private bool _003CRefreshScenario_003Eb__10_1(SimInvestmentAnnualReport x)
	{
		return false;
	}
}
