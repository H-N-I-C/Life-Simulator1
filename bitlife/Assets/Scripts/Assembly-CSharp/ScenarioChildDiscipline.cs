public class ScenarioChildDiscipline : Scenario
{
	protected SimChild _child;

	protected string _narrative;

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

	public static SimChild GetChildToDiscipline()
	{
		return null;
	}
}
