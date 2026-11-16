using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioRacingTimeTrialTrackProfile : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRaceCar, string> _003C_003E9__8_1;

		internal string _003CRefreshScenario_003Eb__8_1(SimRaceCar x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass9_0
	{
		public string dropDownCarString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimRaceCar x)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly SimRacing _racing;

	private readonly RACING_TRACK_CATEGORY _category;

	private readonly string _categoryString;

	private readonly SimRacingTrack _track;

	private List<SimRaceCar> _orderedList;

	private readonly RACING_CAR_STAT _recommendedStat;

	public ScenarioRacingTimeTrialTrackProfile(SimRacingTrack track)
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

	private float _003CRefreshScenario_003Eb__8_0(SimRaceCar x)
	{
		return 0f;
	}
}
