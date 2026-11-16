using System;

public class ScenarioItemPixieDustFantasyVacay : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPerson, bool> _003C_003E9__3_0;

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__3_0(SimPerson t)
		{
			return false;
		}
	}

	private BracketParser _parser;

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
