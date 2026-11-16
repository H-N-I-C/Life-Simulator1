public class ScenarioFameStalker : Scenario
{
	protected string _narrative;

	protected int _stalkerGender;

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
