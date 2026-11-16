public class ScenarioOpportunityMovie : Scenario
{
	protected bool _isNormalMovie;

	protected bool _isChildrensMovie;

	protected SimChild _childToGo;

	protected DataMovie _movie;

	protected string _movieTitle;

	protected SimPerson _companionPerson;

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

	public static bool NormalMovieAvailable()
	{
		return false;
	}

	public static bool ChildrensMovieAvailable()
	{
		return false;
	}

	public static SimChild GetChildToGo()
	{
		return null;
	}
}
