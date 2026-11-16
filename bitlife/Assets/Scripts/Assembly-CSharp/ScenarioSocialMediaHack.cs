public class ScenarioSocialMediaHack : Scenario
{
	protected SimSocialMediaChannel _socialMediaChannel;

	protected bool _willLoseFollowers;

	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private static SimSocialMediaChannel GetSocialMediaChannel(int exceptSocialMedia = -1)
	{
		return null;
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
