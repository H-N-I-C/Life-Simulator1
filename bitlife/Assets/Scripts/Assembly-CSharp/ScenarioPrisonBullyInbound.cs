public class ScenarioPrisonBullyInbound : Scenario
{
	protected SimOccupationPrisoner _prisoner;

	protected SimPrisoner _bullyPrisoner;

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
}
