public class ScenarioFeedbackLogout : Scenario
{
	private string _emojiString;

	public ScenarioFeedbackLogout(bool instantiateOnLoad = false)
	{
	}

	public ScenarioFeedbackLogout(string theTitle, string theBody, Scenario theNextScenario)
	{
	}

	public ScenarioFeedbackLogout(string emoji)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
