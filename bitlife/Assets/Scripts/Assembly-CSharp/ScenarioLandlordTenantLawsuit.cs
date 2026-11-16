public class ScenarioLandlordTenantLawsuit : Scenario
{
	protected SimTenant _theTenant;

	protected SimHouse _theProperty;

	protected string _narrative;

	protected float _settlementAmount;

	protected int _lawsuitType;

	public ScenarioLandlordTenantLawsuit(SimTenant tenant, SimHouse theProperty, int lawsuitType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public float SettlementAmountForLawsuitType()
	{
		return 0f;
	}
}
