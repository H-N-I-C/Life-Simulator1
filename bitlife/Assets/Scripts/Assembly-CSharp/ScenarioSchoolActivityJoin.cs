public class ScenarioSchoolActivityJoin : Scenario
{
	protected DataSchoolActivity _activityData;

	protected bool _pushed;

	protected SimOccupationStudent _student;

	protected bool _requireRewardedVideo;

	public ScenarioSchoolActivityJoin(DataSchoolActivity theActivityData, bool isPushed)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private bool RequireRewardedVideoToGet()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
