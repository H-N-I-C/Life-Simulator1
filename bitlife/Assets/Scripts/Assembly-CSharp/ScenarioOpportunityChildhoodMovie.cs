public class ScenarioOpportunityChildhoodMovie : Scenario
{
	protected DataMovie _movie;

	protected string _movieTitle;

	protected string _inviterString;

	protected string _inviterPronoun;

	protected bool _requireRewardedVideo;

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

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public static bool MovieAvailable()
	{
		return false;
	}
}
