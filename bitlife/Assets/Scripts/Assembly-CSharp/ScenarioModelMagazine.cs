public class ScenarioModelMagazine : Scenario
{
	private string _narrative;

	private SimModelingAgencyClient _client;

	private SimModelGig _gig;

	private SimSpecialJobModel _modelJob;

	private int _numTimesAskedForMoreMoney;

	public ScenarioModelMagazine(SimModelingAgencyClient client = null, SimModelGig gig = null, int numTimesAskedForMoreMoney = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public void GenerateMagazineGig()
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
