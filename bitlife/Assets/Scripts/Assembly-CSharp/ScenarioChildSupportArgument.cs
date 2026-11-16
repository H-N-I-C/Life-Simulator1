public class ScenarioChildSupportArgument : Scenario
{
	private readonly SimChild _child;

	private readonly SimLover _exLover;

	private string _pronoun;

	private string _narrative;

	public ScenarioChildSupportArgument(SimLover exLover, SimChild child)
	{
	}

	public ScenarioChildSupportArgument(SimLover exLover)
	{
	}

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
