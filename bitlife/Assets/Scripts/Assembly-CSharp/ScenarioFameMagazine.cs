public class ScenarioFameMagazine : Scenario
{
	protected SimMagazineOpportunity _magazineOpportunity;

	protected int _monthNI;

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
