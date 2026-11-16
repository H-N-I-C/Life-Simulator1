public class ScenarioVampireAgeUpWithered : Scenario
{
	private SimVampire _vampire;

	private string _narrative;

	public override void RefreshScenario()
	{
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
