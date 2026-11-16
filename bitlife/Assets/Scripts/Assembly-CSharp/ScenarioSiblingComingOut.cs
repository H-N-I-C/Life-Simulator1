public class ScenarioSiblingComingOut : Scenario
{
	protected SimSibling _sibling;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}

	public static SimSibling GetComingOutSibling()
	{
		return null;
	}
}
