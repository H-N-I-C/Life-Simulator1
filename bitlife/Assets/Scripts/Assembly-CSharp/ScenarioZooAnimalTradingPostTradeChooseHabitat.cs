using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioZooAnimalTradingPostTradeChooseHabitat : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooHabitat, string> _003C_003E9__7_0;

		internal string _003CRefreshScenario_003Eb__7_0(SimZooHabitat t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimal _animal;

	private List<SimZooHabitat> _habitatList;

	private ArrayList _dropDownArray;

	private SimZooCompetitor _owningZoo;

	public ScenarioZooAnimalTradingPostTradeChooseHabitat(SimZooAnimal animal, SimZooCompetitor owningZoo)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
