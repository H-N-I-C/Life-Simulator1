using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioRacingDriverPinkSlipRace : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRaceCar, string> _003C_003E9__5_0;

		public static Func<SimRaceCar, string> _003C_003E9__5_1;

		public static Func<SimRacingTrack, bool> _003C_003E9__5_2;

		public static Func<SimRacingTrack, int> _003C_003E9__5_3;

		internal string _003CRefreshScenario_003Eb__5_0(SimRaceCar x)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__5_1(SimRaceCar x)
		{
			return null;
		}

		internal bool _003CRefreshScenario_003Eb__5_2(SimRacingTrack t)
		{
			return false;
		}

		internal int _003CRefreshScenario_003Eb__5_3(SimRacingTrack x)
		{
			return 0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string carChoice;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimRaceCar t)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly SimRaceCarDriver _driver;

	private SimRacing _racing;

	private List<SimRaceCar> _orderedList;

	public ScenarioRacingDriverPinkSlipRace(SimRaceCarDriver driver)
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
