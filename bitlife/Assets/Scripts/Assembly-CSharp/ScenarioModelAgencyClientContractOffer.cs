public class ScenarioModelAgencyClientContractOffer : Scenario
{
	private string _narrative;

	private int _numTimesAskedForMoreMoney;

	private double _compensation;

	private SimSpecialJobModel _modelJob;

	private readonly SimModelingAgencyClient _client;

	private readonly SimModelGig _gig;

	public ScenarioModelAgencyClientContractOffer(SimModelingAgencyClient client, SimModelGig gig, double compensation = 0.0, int numTimesAskedForMoreMoney = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
