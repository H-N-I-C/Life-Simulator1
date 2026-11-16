public class ScenarioVampireHypnotizeArgument : Scenario
{
	public delegate Scenario SuccessScenarioCallback();

	private readonly SimPerson _person;

	private readonly BracketParser _parser;

	private readonly ChoiceRewardedVideo _rewardedVideo;

	private readonly SuccessScenarioCallback _successScenarioCallback;

	public ScenarioVampireHypnotizeArgument(SimPerson person, BracketParser parser, SuccessScenarioCallback successScenarioCallback)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private string ParseBody(string bodyTemplate)
	{
		return null;
	}

	private void LogNarrative(string narrativeTemplate)
	{
	}
}
