public class ScenarioModelCastingCallContractOffer : Scenario
{
	private string _narrative;

	private int _numTimesAskedForMoreMoney;

	private double _compensation;

	private readonly SimModelGig _gig;

	private SimSpecialJobModel _modelJob;

	public ScenarioModelCastingCallContractOffer(SimModelGig gig, double compensation = 0.0, int numTimesAskedForMoreMoney = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
