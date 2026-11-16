using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioZooEmployeeManagerEventPicker : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataZooFundraiser, string> _003C_003E9__6_0;

		internal string _003CRefreshScenario_003Eb__6_0(DataZooFundraiser t)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public string dropDownString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(string t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimZoo _zoo;

	private readonly SimZooOperationsManager _manager;

	private List<DataZooFundraiser> _events;

	private ArrayList _dropDownContents;

	public ScenarioZooEmployeeManagerEventPicker(SimZooOperationsManager manager)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
