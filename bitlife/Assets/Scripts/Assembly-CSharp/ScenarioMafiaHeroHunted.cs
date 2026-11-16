public class ScenarioMafiaHeroHunted : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected bool _stillInFamily;

	protected bool _afterCallingCard;

	protected bool _isRivalFamily;

	protected string _confronter;

	protected string _narrative;

	public ScenarioMafiaHeroHunted()
	{
	}

	public ScenarioMafiaHeroHunted(SimSpecialJobMafia theSpecialJob, bool isStillInFamily, bool isAfterCallingCard)
	{
	}

	public ScenarioMafiaHeroHunted(SimSpecialJobMafia theSpecialJob)
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
