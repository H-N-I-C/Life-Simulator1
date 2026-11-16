public class ScenarioPodcastPromote : ScenarioSocialMediaPromote
{
	private enum ResponseType
	{
		Neutral = 0,
		Positive = 1,
		Negative = 2
	}

	private readonly SimPodcastActivity _activity;

	private int _subscribersGainedOrLost;

	private ResponseType _responseType;

	private bool _userDidSelectOption;

	public ScenarioPodcastPromote()
	{
	}

	public ScenarioPodcastPromote(SimPodcastActivity podcastActivity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override void Closed()
	{
	}

	private void LogMessage(string message)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private Scenario CreateFallbackFeedbackScenario()
	{
		return null;
	}
}
