using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioValentinesAnniversarySpecialDate : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__3_0;

		internal string _003CRefreshScenario_003Eb__3_0(string q)
		{
			return null;
		}
	}

	private ValentinesAnniversaryActivity _activity;

	private Dictionary<string, DataAnniversaryDate> _dateOptions;

	public ScenarioValentinesAnniversarySpecialDate(ValentinesAnniversaryActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
