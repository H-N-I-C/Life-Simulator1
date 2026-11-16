using System;

public class ScenarioCasinoAgeUpGamblingBoard : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCasinoGameRoom, bool> _003C_003E9__6_0;

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__6_0(SimCasinoGameRoom t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	private double _fine;

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
