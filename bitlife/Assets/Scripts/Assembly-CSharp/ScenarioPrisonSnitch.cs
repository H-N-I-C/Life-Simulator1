public class ScenarioPrisonSnitch : Scenario
{
	protected SimOccupationPrisoner _prisoner;

	protected SimPrisoner _snitchPrisoner;

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
