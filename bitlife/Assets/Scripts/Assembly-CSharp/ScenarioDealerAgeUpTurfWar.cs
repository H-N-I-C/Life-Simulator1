public class ScenarioDealerAgeUpTurfWar : Scenario
{
	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public SimSpecialJobDealer dealer;

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__0(SimDealerCartel x)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	public override void RefreshScenario()
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
