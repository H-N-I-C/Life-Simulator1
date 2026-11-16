using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioZooGiftShopAddProductBuildDetails : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooGiftShopItem, SimZooAnimal> _003C_003E9__6_1;

		public static Func<SimZooAnimal, float> _003C_003E9__6_3;

		internal SimZooAnimal _003CRefreshScenario_003Eb__6_1(SimZooGiftShopItem t)
		{
			return null;
		}

		internal float _003CRefreshScenario_003Eb__6_3(SimZooAnimal t)
		{
			return 0f;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public ScenarioZooGiftShopAddProductBuildDetails _003C_003E4__this;

		public List<SimZooAnimal> allFeaturedAnimals;

		internal bool _003CRefreshScenario_003Eb__0(SimZooGiftShopItem x)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__2(SimZooAnimal t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooGiftShopItem _item;

	private List<SimZooAnimal> _animals;

	private ArrayList _dropDownContents;

	public ScenarioZooGiftShopAddProductBuildDetails(SimZooGiftShopItem item)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
