using System;
using Game;

public class ScenarioDealerSupplyRunPayloadThief : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string, string> _003C_003E9__10_0;

		internal string _003CChoiceSelectedAtIndex_003Eb__10_0(string i, string j)
		{
			return null;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerSupplyRun _supplyRun;

	private GENDER _gender;

	private string _himHerString;

	private string _heSheString;

	private DataDealerDrugWorkerAction _data;

	private BracketParser _parser;

	public ScenarioDealerSupplyRunPayloadThief(SimDealerSupplyRun supplyRun)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void ShowDoseAdjustmentFeedback()
	{
	}
}
