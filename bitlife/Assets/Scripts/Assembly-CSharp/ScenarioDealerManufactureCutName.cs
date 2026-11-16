using System.Collections;

public class ScenarioDealerManufactureCutName : Scenario
{
	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public string cutName;

		internal bool _003CRefreshScenario_003Eb__0(SimDealerDrugCut t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerDrug _drug;

	private readonly string _cutIngredient;

	private readonly int _cutPercentage;

	private readonly int _doses;

	public ScenarioDealerManufactureCutName(SimDealerDrug drug, string cutIngredient, int cutPercentage, int doses)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
