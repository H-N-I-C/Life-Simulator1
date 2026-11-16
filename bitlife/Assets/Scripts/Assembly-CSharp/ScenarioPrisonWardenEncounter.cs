public class ScenarioPrisonWardenEncounter : Scenario
{
	protected string _narrative;

	protected int _wardenGender;

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
