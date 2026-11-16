using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioZooEmployeeHandlerShowAnimalPicker : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooAnimal, bool> _003C_003E9__8_1;

		public static Func<SimZooAnimal, bool> _003C_003E9__8_2;

		public static Func<SimZooAnimal, bool> _003C_003E9__8_3;

		public static Func<SimZooAnimal, string> _003C_003E9__8_0;

		internal bool _003CGenerateAnimalDropDown_003Eb__8_1(SimZooAnimal t)
		{
			return false;
		}

		internal bool _003CGenerateAnimalDropDown_003Eb__8_2(SimZooAnimal t)
		{
			return false;
		}

		internal bool _003CGenerateAnimalDropDown_003Eb__8_3(SimZooAnimal t)
		{
			return false;
		}

		internal string _003CGenerateAnimalDropDown_003Eb__8_0(SimZooAnimal t)
		{
			return null;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooAnimalHandler _handler;

	private List<SimZooAnimal> _animals;

	private ArrayList _dropDownContents;

	public ScenarioZooEmployeeHandlerShowAnimalPicker(SimZooAnimalHandler handler)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private List<SimZooAnimal> GenerateAnimalDropDown()
	{
		return null;
	}
}
