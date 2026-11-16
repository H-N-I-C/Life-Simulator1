public class ScenarioCoworkerHookup : Scenario
{
	protected SimCoworker _coworker;

	protected SimLover _loverCandidate;

	protected bool _targeted;

	protected bool _gettingSeduced;

	protected string _dataInboundLogVersion;

	protected bool _requireRewardedVideo;

	public ScenarioCoworkerHookup()
	{
	}

	public ScenarioCoworkerHookup(SimCoworker theCoworker, bool initatedByHero)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}

	public static SimCoworker GetSeducingCoworker()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
