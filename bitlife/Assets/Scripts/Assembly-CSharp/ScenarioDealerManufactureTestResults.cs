using System;
using Game;

public class ScenarioDealerManufactureTestResults : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__8_0;

		internal string _003CRefreshScenario_003Eb__8_0(string p)
		{
			return null;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerCartelMember _member;

	private readonly SimDealerDrug _drug;

	private readonly string _cutIngredient;

	private readonly int _cutPercentage;

	private readonly DataDealerCutDrugSubstance _cutIngredientData;

	public ScenarioDealerManufactureTestResults(SimDealerCartelMember member, SimDealerDrug drug, string cutIngredient, int cutPercentage, DataDealerCutDrugSubstance cutIngredientData)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
