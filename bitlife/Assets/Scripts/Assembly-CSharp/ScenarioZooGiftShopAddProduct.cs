using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioZooGiftShopAddProduct : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooGiftShopItem, SimZooAnimal> _003C_003E9__5_2;

		public static Func<SimZooGiftShopItem, string> _003C_003E9__5_0;

		internal SimZooAnimal _003CRefreshScenario_003Eb__5_2(SimZooGiftShopItem x)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__5_0(SimZooGiftShopItem t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public DataZooGiftShopItem item;

		internal bool _003CRefreshScenario_003Eb__1(SimZooGiftShopItem t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private List<SimZooGiftShopItem> _items;

	private ArrayList _dropDownContents;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
