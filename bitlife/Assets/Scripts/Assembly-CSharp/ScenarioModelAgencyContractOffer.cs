public class ScenarioModelAgencyContractOffer : Scenario
{
	private string _narrative;

	private bool _negotiationAttempted;

	private SimSpecialJobModel _modelJob;

	private SimModelingAgency _agency;

	private float _salary;

	private MODEL_CAREER_TYPE _lastPosition;

	public ScenarioModelAgencyContractOffer(SimModelingAgency agency, float salary = 0f, bool negotiationAttempted = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
