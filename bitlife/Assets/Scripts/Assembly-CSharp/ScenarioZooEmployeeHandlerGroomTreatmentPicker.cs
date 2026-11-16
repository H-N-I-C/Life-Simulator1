using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioZooEmployeeHandlerGroomTreatmentPicker : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataZooAnimalGroomMethods, int> _003C_003E9__7_0;

		internal int _003CRefreshScenario_003Eb__7_0(DataZooAnimalGroomMethods t)
		{
			return 0;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimalHandler _handler;

	private readonly SimZooAnimal _animal;

	private List<DataZooAnimalGroomMethods> _listGrooming;

	private ArrayList _dropDownArray;

	public ScenarioZooEmployeeHandlerGroomTreatmentPicker(SimZooAnimalHandler handler, SimZooAnimal animal)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
