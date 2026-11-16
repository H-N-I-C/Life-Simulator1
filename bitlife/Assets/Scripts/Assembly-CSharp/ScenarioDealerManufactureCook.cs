using System.Collections;

public class ScenarioDealerManufactureCook : Scenario
{
	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerDrug _drug;

	private readonly string _previousIngredient;

	private readonly int _previousCutPercentage;

	public ScenarioDealerManufactureCook(SimDealerDrug drug, string previousIngredient = null, int previousCutPercentage = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private bool _003CRefreshScenario_003Eb__6_0(string t)
	{
		return false;
	}

	private bool _003CRefreshScenario_003Eb__6_1(string t)
	{
		return false;
	}
}
