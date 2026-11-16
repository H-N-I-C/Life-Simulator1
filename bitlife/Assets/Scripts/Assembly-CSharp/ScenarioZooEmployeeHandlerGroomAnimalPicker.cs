using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioZooEmployeeHandlerGroomAnimalPicker : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooAnimal, string> _003C_003E9__6_0;

		internal string _003CRefreshScenario_003Eb__6_0(SimZooAnimal t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimalHandler _handler;

	private List<SimZooAnimal> _animals;

	private ArrayList _dropDownContents;

	public ScenarioZooEmployeeHandlerGroomAnimalPicker(SimZooAnimalHandler handler)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
