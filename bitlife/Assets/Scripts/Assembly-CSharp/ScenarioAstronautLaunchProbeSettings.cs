public class ScenarioAstronautLaunchProbeSettings : Scenario
{
	private readonly SimProbe _probe;

	private readonly SimDiscovery _discovery;

	private readonly bool _isExploring;

	private readonly SimSpecialJobAstronaut _specialJob;

	private MiniGameDataAstronautLaunchProbeSettings _gameData;

	private string _narrative;

	public ScenarioAstronautLaunchProbeSettings(SimProbe probe, SimDiscovery discovery)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private Scenario DiscoveryFeedback()
	{
		return null;
	}

	private Scenario SuccessFeedback()
	{
		return null;
	}

	private Scenario FailureFeedback(int casualties)
	{
		return null;
	}

	private bool ProbeWillLaunch()
	{
		return false;
	}
}
