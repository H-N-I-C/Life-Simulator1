public class ScenarioZooHabitatAddAnimalTradingPostPurchaseRequestCandidate : Scenario
{
	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooRequestedAnimal _requestedAnimal;

	private readonly SimZooHabitat _habitat;

	private SimZooAnimal _candidateAnimal;

	private readonly double _offerAmount;

	private bool _badAnimalAlreadyOffered;

	private SimZooCompetitor _candidateZoo;

	public ScenarioZooHabitatAddAnimalTradingPostPurchaseRequestCandidate(SimZooRequestedAnimal requestedAnimal, double offerAmount, SimZooHabitat habitat, bool badAnimalAlreadyOffered = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
