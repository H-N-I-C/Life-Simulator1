public class ScenarioRoyalCommonerNegative : Scenario
{
	protected int _commonerGender;

	protected string _feedbackSnippet;

	protected string _logVersionSnippet;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
