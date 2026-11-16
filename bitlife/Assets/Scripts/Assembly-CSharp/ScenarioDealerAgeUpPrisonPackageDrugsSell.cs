using System;
using System.Collections;

public class ScenarioDealerAgeUpPrisonPackageDrugsSell : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPrisoner, string> _003C_003E9__5_0;

		internal string _003CRefreshScenario_003Eb__5_0(SimPrisoner x)
		{
			return null;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerDrug _drug;

	private readonly int _doses;

	public ScenarioDealerAgeUpPrisonPackageDrugsSell(SimDealerDrug drug, int doses)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
