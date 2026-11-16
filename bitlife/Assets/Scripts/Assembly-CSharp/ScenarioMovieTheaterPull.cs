public class ScenarioMovieTheaterPull : Scenario
{
	protected int _genre;

	protected DataMovie _movie;

	protected string _movieTitle;

	protected bool _requireRewardedVideo;

	public ScenarioMovieTheaterPull(int theGenre)
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
