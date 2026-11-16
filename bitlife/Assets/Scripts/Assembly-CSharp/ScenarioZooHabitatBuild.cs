using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioZooHabitatBuild : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataZooHabitatType, string> _003C_003E9__5_0;

		internal string _003CRefreshScenario_003Eb__5_0(DataZooHabitatType t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string dropDownString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(string t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private List<DataZooHabitatType> _listHabitats;

	private List<string> _listStringNames;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
