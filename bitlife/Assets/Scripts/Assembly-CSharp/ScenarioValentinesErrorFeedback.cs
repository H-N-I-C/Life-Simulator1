public class ScenarioValentinesErrorFeedback : Scenario
{
	public enum ERROR_TYPE
	{
		OFFLINE = 0,
		UNDER_AGE = 1
	}

	private ERROR_TYPE _errorType;

	public ScenarioValentinesErrorFeedback(ERROR_TYPE errorType)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
