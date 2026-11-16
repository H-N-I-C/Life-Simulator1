public class ScenarioBusinessSellCompanyPush : Scenario
{
	protected SimBusiness _theBusiness;

	protected string _customer;

	protected int _customerGender;

	protected double _offer;

	protected bool _isGroup;

	protected bool _isBitLifeDevs;

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
