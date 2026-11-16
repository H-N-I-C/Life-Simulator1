public class ScenarioMusicFanInteraction : Scenario
{
	protected SimSpecialJobMusician _specialJob;

	protected SimLover _fan;

	protected bool _isPositive;

	protected string _feedbackSnippet;

	protected string _logVersionSnippet;

	public ScenarioMusicFanInteraction(SimSpecialJobMusician theSpecialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static DataMusicFanInteractionScenario GetFanInteractionScenario()
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
