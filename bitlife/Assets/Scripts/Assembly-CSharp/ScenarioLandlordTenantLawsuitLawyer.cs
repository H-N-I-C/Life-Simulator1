public class ScenarioLandlordTenantLawsuitLawyer : Scenario
{
	protected SimTenant _theTenant;

	protected SimHouse _theProperty;

	protected string _narrative;

	protected int _lawsuitReason;

	protected float _firm1Cost;

	protected float _firm2Cost;

	public ScenarioLandlordTenantLawsuitLawyer(SimTenant tenant, SimHouse theProperty, int lawsuitReason)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private float GetOddsOfSuccessForLawsuitType(int tier)
	{
		return 0f;
	}

	public double DamageForLawsuitType()
	{
		return 0.0;
	}

	private bool _003CChoiceSelectedAtIndex_003Eb__8_0(SimPropertyLeaseContract x)
	{
		return false;
	}

	private bool _003CChoiceSelectedAtIndex_003Eb__8_1(SimPropertyLeaseContract t)
	{
		return false;
	}
}
