public class ScenarioSiblingBully : Scenario
{
	protected SimSibling _sibling;

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

	public static SimSibling GetBulliedSibling()
	{
		return null;
	}
}
