using System.Collections;

public class ScenarioBusinessStartupName : Scenario
{
	protected DataBusinessIndustry _theIndustry;

	protected SimBusiness _theBusiness;

	protected double _yourInvestmentAmount;

	protected double _externalInvestmentAmount;

	public ScenarioBusinessStartupName(DataBusinessIndustry theIndustry, SimBusiness theBusiness, double yourInvestmentAmount, double externalInvestmentAmount)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
