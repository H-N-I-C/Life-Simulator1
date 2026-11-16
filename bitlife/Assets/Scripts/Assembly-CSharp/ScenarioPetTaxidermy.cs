public class ScenarioPetTaxidermy : Scenario
{
	private string _narrative;

	private SimPet _pet;

	private string _highQualityTaxidermist;

	private string _lowQualityTaxidermist;

	private double _highPrice;

	private double _lowPrice;

	public ScenarioPetTaxidermy(SimPet pet, string highQualityTaxidermist = null, string lowQualityTaxidermist = null)
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
