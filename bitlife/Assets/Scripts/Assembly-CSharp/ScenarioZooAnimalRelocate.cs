using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioZooAnimalRelocate : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Comparison<SimZooHabitat> _003C_003E9__7_0;

		internal int _003CRefreshScenario_003Eb__7_0(SimZooHabitat x, SimZooHabitat y)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public ScenarioZooAnimalRelocate _003C_003E4__this;

		public SimZooHabitat newHabitat;

		internal void _003CChoiceSelectedAtIndex_003Eb__0(int index)
		{
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimal _animal;

	private SimZooHabitat _currentHabitat;

	private List<SimZooHabitat> _habitatList;

	private ArrayList _dropDownArray;

	public ScenarioZooAnimalRelocate(SimZooAnimal animal)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
