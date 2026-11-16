using System;

public class ScenarioCasinoAgeUpEntertainmentActResignation : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCasinoEntertainmentAct, bool> _003C_003E9__3_0;

		public static Func<SimCasinoEntertainmentAct, bool> _003C_003E9__5_0;

		internal bool _003CRefreshScenario_003Eb__3_0(SimCasinoEntertainmentAct t)
		{
			return false;
		}

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__5_0(SimCasinoEntertainmentAct t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	private SimCasinoEntertainmentAct _act;

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
