public class ScenarioSocialMediaJoinPull : Scenario
{
	protected int _channel;

	protected bool _requireShare;

	protected bool _requireRV;

	public ScenarioSocialMediaJoinPull(int theChannel)
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
