using System;

public class ScenarioDealerAgeUpDropOut : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDealerCartelMember, bool> _003C_003E9__4_0;

		public static Func<SimDealerCartelMember, bool> _003C_003E9__6_0;

		internal bool _003CRefreshScenario_003Eb__4_0(SimDealerCartelMember x)
		{
			return false;
		}

		internal bool _003CScenarioIsEligibleForRandomPush_003Eb__6_0(SimDealerCartelMember x)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private SimDealerCartelMember _member;

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
