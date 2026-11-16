using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioZooHabitatDemolish : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimZooHabitat, string> _003C_003E9__5_0;

		internal string _003CRefreshScenario_003Eb__5_0(SimZooHabitat t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string dropDownString;

		public SimZooHabitat habitat;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(string t)
		{
			return false;
		}

		internal void _003CChoiceSelectedAtIndex_003Eb__1(int index)
		{
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private List<SimZooHabitat> _orderedHabitatList;

	private List<string> _habitatNames;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
