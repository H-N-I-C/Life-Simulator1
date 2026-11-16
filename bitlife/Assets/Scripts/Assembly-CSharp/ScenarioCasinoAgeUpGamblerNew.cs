using System;

public class ScenarioCasinoAgeUpGamblerNew : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCasinoGambler, string> _003C_003E9__2_0;

		internal string _003CRefreshScenario_003Eb__2_0(SimCasinoGambler t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	public override void RefreshScenario()
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
