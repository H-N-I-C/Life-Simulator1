using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioZooAgeUpAnimalBirthSellOther : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooAnimal, float> _003C_003E9__9_0;

		internal float _003CRefreshScenario_003Eb__9_0(SimZooAnimal t)
		{
			return 0f;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private string _otherZooName;

	private SimZooAnimal _babyAnimal;

	private double _salePrice;

	private List<SimZooAnimal> _animals;

	private ArrayList _dropDownContents;

	private SimZooCompetitor _competitorZoo;

	public ScenarioZooAgeUpAnimalBirthSellOther(SimZooCompetitor otherZoo, SimZooAnimal babyAnimal, double salePrice)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
