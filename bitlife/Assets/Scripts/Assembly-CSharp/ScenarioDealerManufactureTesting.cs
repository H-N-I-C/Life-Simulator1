using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioDealerManufactureTesting : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDealerCartelMember, string> _003C_003E9__9_0;

		internal string _003CRefreshScenario_003Eb__9_0(SimDealerCartelMember t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerDrug _drug;

	private readonly string _cutIngredient;

	private readonly int _cutPercentage;

	private List<SimDealerCartelMember> _dealers;

	private ArrayList _dropDownContents;

	private readonly DataDealerCutDrugSubstance _cutIngredientData;

	public ScenarioDealerManufactureTesting(SimDealerDrug drug, string cutIngredient, int cutPercentage)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
