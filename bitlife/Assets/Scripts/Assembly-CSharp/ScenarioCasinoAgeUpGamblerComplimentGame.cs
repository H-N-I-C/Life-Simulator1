public class ScenarioCasinoAgeUpGamblerComplimentGame : Scenario
{
	private sealed class _003C_003Ec__DisplayClass3_0
	{
		public CASINO_GAME_TYPE game;

		internal bool _003CRefreshScenario_003Eb__0(SimCasinoGameRoom t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public CASINO_GAME_TYPE game;

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__0(SimCasinoGameRoom t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	private SimCasinoGambler _gambler;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
