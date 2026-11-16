public class ScenarioSocialMediaTrollInbound : Scenario
{
	protected SimSocialMediaChannel _socialMediaChannel;

	protected SimSocialMediaTroll _troll;

	protected bool _response;

	protected string _narrative;

	public ScenarioSocialMediaTrollInbound()
	{
	}

	public ScenarioSocialMediaTrollInbound(int theChannel, SimSocialMediaTroll theTroll)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static SimSocialMediaChannel GetSocialMediaChannel()
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
