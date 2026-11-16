public class ScenarioActorChallengeDynamic : Scenario
{
	protected SimSpecialJobActor _specialJob;

	protected DataActorDynamicChallenge _data;

	protected SimCostar _costar;

	protected string _feedbackPastBodySnippet;

	protected string _feedbackPastNarrativeSnippet;

	protected string _feedbackPresentBodySnippet;

	protected string _feedbackPresentNarrativeSnippet;

	private string _productionString;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
