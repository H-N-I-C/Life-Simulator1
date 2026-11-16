public class ScenarioSocialMediaCelebrityFollowPush : Scenario
{
	protected string _narrative;

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	private DataCelebrity GetCelebrityToFollowOnChannel(SimSocialMediaChannel socialMediaChannel)
	{
		return null;
	}

	public static SimSocialMediaChannel GetSocialMediaChannel()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
