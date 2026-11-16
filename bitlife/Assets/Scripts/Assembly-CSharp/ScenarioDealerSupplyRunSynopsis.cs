using System;

public class ScenarioDealerSupplyRunSynopsis : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDealerCartelMember, bool> _003C_003E9__4_0;

		internal bool _003CRefreshScenario_003Eb__4_0(SimDealerCartelMember t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerSupplyRun _supplyRun;

	public ScenarioDealerSupplyRunSynopsis(SimDealerSupplyRun supplyRun)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
