public class ScenarioRoyalCelebrityPush : Scenario
{
	protected DataCelebrity _celebrity;

	protected DataRoyalCelebrityInteractionScenario _data;

	protected string _pastTenseSnippet;

	protected string _presentTenseSnippet;

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
