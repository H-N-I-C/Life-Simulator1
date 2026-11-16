using System;
using System.Collections;

public class ScenarioRacingDriverCashRace : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRacingTrack, bool> _003C_003E9__3_0;

		public static Func<SimRacingTrack, int> _003C_003E9__3_1;

		internal bool _003CRefreshScenario_003Eb__3_0(SimRacingTrack t)
		{
			return false;
		}

		internal int _003CRefreshScenario_003Eb__3_1(SimRacingTrack x)
		{
			return 0;
		}
	}

	private string _narrative;

	private readonly SimRaceCarDriver _driver;

	public ScenarioRacingDriverCashRace(SimRaceCarDriver driver)
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
