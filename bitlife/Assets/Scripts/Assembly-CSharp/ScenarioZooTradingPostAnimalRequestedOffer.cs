using System.Collections;
using System.Collections.Generic;

public class ScenarioZooTradingPostAnimalRequestedOffer : Scenario
{
	private sealed class _003C_003Ec__DisplayClass10_0
	{
		public ScenarioZooTradingPostAnimalRequestedOffer _003C_003E4__this;

		public SimZooAnimal yourAnimal;

		public BracketParser parser;

		internal void _003CChoiceSelectedAtIndex_003Eb__0(int index)
		{
		}
	}

	private string _narrative;

	private readonly SimZoo _zoo;

	private readonly SimZooRequestedAnimal _animal;

	private readonly List<SimZooAnimal> _matchingAnimals;

	private readonly SimZooAnimal _singleMatchAnimal;

	private ArrayList _dropDownArray;

	private BracketParser _parser;

	public ScenarioZooTradingPostAnimalRequestedOffer(SimZooRequestedAnimal animal)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private bool CompetitorZooWillAccept(SimZooAnimal heroAnimal)
	{
		return false;
	}
}
