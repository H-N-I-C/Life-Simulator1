using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioZooEmployeeVetCheckups : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooAnimal, bool> _003C_003E9__6_0;

		public static Func<SimZooAnimal, float> _003C_003E9__6_1;

		internal bool _003CRefreshScenario_003Eb__6_0(SimZooAnimal t)
		{
			return false;
		}

		internal float _003CRefreshScenario_003Eb__6_1(SimZooAnimal t)
		{
			return 0f;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooVeterinarian _vet;

	private List<SimZooAnimal> _animals;

	private ArrayList _dropDownContents;

	public ScenarioZooEmployeeVetCheckups(SimZooVeterinarian vet)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
