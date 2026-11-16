public class ScenarioEmployeeLawsuitLawyer : Scenario
{
	protected SimEmployee _theEmployee;

	protected SimBusiness _theBusiness;

	protected string _narrative;

	protected int _lawsuitReason;

	protected float _firm1Cost;

	protected float _firm2Cost;

	public ScenarioEmployeeLawsuitLawyer(SimEmployee employee, int lawsuitReason)
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
}
