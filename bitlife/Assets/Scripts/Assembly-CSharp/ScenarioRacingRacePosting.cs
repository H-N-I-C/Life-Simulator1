using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioRacingRacePosting : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRaceCarDriver, string> _003C_003E9__10_0;

		public static Func<string, string, string> _003C_003E9__10_1;

		public static Func<SimRaceCar, string> _003C_003E9__10_2;

		public static Func<SimRaceCar, string> _003C_003E9__10_3;

		internal string _003CRefreshScenario_003Eb__10_0(SimRaceCarDriver t)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__10_1(string i, string j)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__10_2(SimRaceCar x)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__10_3(SimRaceCar x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass11_0
	{
		public string selectedChoice;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimRaceCar t)
		{
			return false;
		}
	}

	private readonly SimRacingTrack _track;

	private readonly SimRacing _racing;

	private string _sponsorName;

	private List<SimRaceCar> _orderedList;

	private readonly int _prize;

	private readonly List<SimRaceCarDriver> _competitors;

	private readonly RACING_TRACK_CATEGORY _category;

	private string _narrative;

	public ScenarioRacingRacePosting(RACING_TRACK_CATEGORY category, SimRacingTrack track, SimRacing racing)
	{
	}

	public ScenarioRacingRacePosting(RACING_TRACK_CATEGORY category, SimRacingTrack track, SimRacing racing, float prize, List<SimRaceCarDriver> competitors, string sponsorName)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
