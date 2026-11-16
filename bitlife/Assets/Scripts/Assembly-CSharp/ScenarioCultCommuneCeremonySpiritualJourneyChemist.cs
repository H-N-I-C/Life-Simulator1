using Game;

public class ScenarioCultCommuneCeremonySpiritualJourneyChemist : Scenario
{
	private string _narrative;

	private SimCult _cult;

	private readonly string _productString;

	private readonly string _dosageString;

	private readonly DataCultSpiritualJourneyDrug _chosenDrugData;

	private readonly int _riskScore;

	private BracketParser _bracketParser;

	private double _totalCost;

	private int _followersForJourney;

	private int _numberOfDoses;

	public ScenarioCultCommuneCeremonySpiritualJourneyChemist(string productString, string dosageString)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
