public class ScenarioSocialMediaJoinPush : Scenario
{
	protected int _channel;

	protected bool _requireShare;

	protected bool _requireRV;

	public ScenarioSocialMediaJoinPush()
	{
	}

	public ScenarioSocialMediaJoinPush(int theChannel)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static int GetEligibleSocialMediaChannel()
	{
		return 0;
	}

	private bool RequireRewardedVideoToGet()
	{
		return false;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
