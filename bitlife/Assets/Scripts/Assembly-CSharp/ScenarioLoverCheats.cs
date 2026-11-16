public class ScenarioLoverCheats : Scenario
{
	protected bool _postMilitaryDeployment;

	protected bool _loverAdmits;

	protected string _narrative;

	public ScenarioLoverCheats()
	{
	}

	public ScenarioLoverCheats(bool loverAdmits, bool postMilitaryDeployment)
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
