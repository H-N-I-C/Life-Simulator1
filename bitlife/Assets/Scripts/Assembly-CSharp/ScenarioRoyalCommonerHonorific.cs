public class ScenarioRoyalCommonerHonorific : Scenario
{
	protected string _narrative;

	protected DataStreetPerson _streetPerson;

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
