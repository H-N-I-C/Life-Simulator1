public class ScenarioModelLessons : Scenario
{
	private string _narrative;

	private double _cost;

	private bool _requireRewardedVideo;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
