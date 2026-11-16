public class ScenarioSportsFanInteraction : Scenario
{
	protected SimSpecialJobAthlete _specialJob;

	protected SimLover _fan;

	private string _feedbackSnippet;

	private string _logVersionSnippet;

	private bool _isPositive;

	private string _narrative;

	public ScenarioSportsFanInteraction(SimSpecialJobAthlete theSpecialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static DataSportsFanInteractionScenario GetFanInteractionScenario()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
