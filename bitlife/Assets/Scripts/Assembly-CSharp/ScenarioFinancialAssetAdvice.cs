public class ScenarioFinancialAssetAdvice : Scenario
{
	protected SimCrypto _crypto;

	protected SimStock _stock;

	protected bool _giveBuyAdvice;

	protected SimPerson _advisor;

	public ScenarioFinancialAssetAdvice(SimPerson advisor, SimStock stock, bool giveBuyAdvice)
	{
	}

	public ScenarioFinancialAssetAdvice(SimPerson advisor, SimCrypto crypto, bool giveBuyAdvice)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
