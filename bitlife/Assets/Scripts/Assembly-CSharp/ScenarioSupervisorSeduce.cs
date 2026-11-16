public class ScenarioSupervisorSeduce : Scenario
{
	protected SimCoworker _supervisor;

	protected SimLover _loverCandidate;

	protected bool _targeted;

	protected bool _gettingSeduced;

	protected string _dataInboundLogVersion;

	public ScenarioSupervisorSeduce(SimCoworker theSupervisor, bool initatedByHero)
	{
	}

	public ScenarioSupervisorSeduce()
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
