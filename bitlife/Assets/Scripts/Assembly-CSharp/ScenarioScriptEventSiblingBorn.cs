public class ScenarioScriptEventSiblingBorn : Scenario
{
	protected SimSibling _sibling;

	public new bool PreventRandomPushAfterScenario()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
