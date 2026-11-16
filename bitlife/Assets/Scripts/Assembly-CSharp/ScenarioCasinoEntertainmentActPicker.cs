using System;
using System.Collections;
using Game;

public class ScenarioCasinoEntertainmentActPicker : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__3_0;

		public static Func<DataCasinoCasinoPerformanceAct, string> _003C_003E9__3_1;

		public static Func<DataCasinoCasinoPerformanceAct, string> _003C_003E9__3_2;

		internal string _003CRefreshScenario_003Eb__3_0(string t)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__3_1(DataCasinoCasinoPerformanceAct t)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__3_2(DataCasinoCasinoPerformanceAct x)
		{
			return null;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
