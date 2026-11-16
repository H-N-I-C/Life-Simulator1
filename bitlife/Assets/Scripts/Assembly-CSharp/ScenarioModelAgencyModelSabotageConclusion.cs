public class ScenarioModelAgencyModelSabotageConclusion : Scenario
{
	private string _narrative;

	private readonly string _method;

	private SimSpecialJobModel _modelJob;

	private readonly SimModel _modelVictim;

	private readonly SimModelGig _gig;

	private readonly SimModelingAgencyClient _client;

	public ScenarioModelAgencyModelSabotageConclusion(SimModel modeVictim, string method, SimModelingAgencyClient possibleClient, SimModelGig possibleGig)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
