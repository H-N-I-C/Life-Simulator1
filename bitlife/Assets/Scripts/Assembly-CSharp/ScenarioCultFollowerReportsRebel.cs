using System;
using Game;

public class ScenarioCultFollowerReportsRebel : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCommunard, bool> _003C_003E9__6_0;

		public static Func<SimCommunard, bool> _003C_003E9__8_0;

		internal bool _003CRefreshScenario_003Eb__6_0(SimCommunard x)
		{
			return false;
		}

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__8_0(SimCommunard x)
		{
			return false;
		}
	}

	private string _narrative;

	private SimCommunard _communard;

	private SimCult _cult;

	private DataCultDoctrineRule _brokenRule;

	private BracketParser _parser;

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
