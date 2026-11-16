using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioZooEmployeeManagerEventPlanning : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooAnimal, float> _003C_003E9__12_0;

		internal float _003CGetPopularAnimals_003Eb__12_0(SimZooAnimal t)
		{
			return 0f;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooOperationsManager _manager;

	private readonly DataZooFundraiser _data;

	private readonly string _eventType;

	private readonly float _cost;

	private List<SimZooAnimal> _animals;

	private ArrayList _dropDownContents;

	public ScenarioZooEmployeeManagerEventPlanning(SimZooOperationsManager manager, DataZooFundraiser data)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private List<SimZooAnimal> GetRandomAnimals(int count, List<SimZooAnimal> selectedAnimals)
	{
		return null;
	}

	private List<SimZooAnimal> GetPopularAnimals(int count)
	{
		return null;
	}
}
