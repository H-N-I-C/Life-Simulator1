using System.Collections;

public class ScenarioZooHabitatAddAnimalTradingPostPurchaseRequestOffer : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly string _breed;

	private readonly GENDER _gender;

	private readonly SimZooHabitat _habitat;

	private double _avgPrice;

	public ScenarioZooHabitatAddAnimalTradingPostPurchaseRequestOffer(string breed, GENDER gender, SimZooHabitat habitat)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
