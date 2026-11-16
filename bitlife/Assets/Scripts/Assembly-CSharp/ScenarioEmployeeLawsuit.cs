public class ScenarioEmployeeLawsuit : Scenario
{
	protected SimEmployee _theEmployee;

	protected SimBusiness _currentBusiness;

	protected string _narrative;

	protected float _settlementAmount;

	protected int _lawsuitType;

	public ScenarioEmployeeLawsuit(SimEmployee employee, int lawsuitType)
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
