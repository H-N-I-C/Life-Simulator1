public class ScenarioMafiaHeroHuntedCallingCard : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected bool _stillInFamily;

	protected string _narrative;

	public ScenarioMafiaHeroHuntedCallingCard()
	{
	}

	public ScenarioMafiaHeroHuntedCallingCard(SimSpecialJobMafia theSpecialJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
