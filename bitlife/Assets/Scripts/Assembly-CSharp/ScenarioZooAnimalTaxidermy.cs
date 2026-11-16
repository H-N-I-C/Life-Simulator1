public class ScenarioZooAnimalTaxidermy : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimal _animal;

	private string _highQualityTaxidermist;

	private string _lowQualityTaxidermist;

	private double _highPrice;

	private double _lowPrice;

	public ScenarioZooAnimalTaxidermy(SimZooAnimal animal, string highQualityTaxidermist = null, string lowQualityTaxidermist = null)
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
