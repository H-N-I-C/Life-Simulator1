public class ScenarioAstronautSpaceCamp : Scenario
{
	private float _cost;

	private bool _requireRewardedVideo;

	private bool _parentsAgreed;

	public ScenarioAstronautSpaceCamp(bool parentsAgreed = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
