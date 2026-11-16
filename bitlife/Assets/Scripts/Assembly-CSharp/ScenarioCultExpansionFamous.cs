public class ScenarioCultExpansionFamous : Scenario
{
	private string _narrative;

	private BracketParser _parser;

	private readonly bool _secondPass;

	public ScenarioCultExpansionFamous()
	{
	}

	public ScenarioCultExpansionFamous(BracketParser parser)
	{
	}

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
