using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioRacingBettingPickDriver : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRaceCarDriver, string> _003C_003E9__5_0;

		internal string _003CRefreshScenario_003Eb__5_0(SimRaceCarDriver t)
		{
			return null;
		}
	}

	private string _narrative;

	private readonly string _trackName;

	private List<SimRaceCarDriver> _drivers;

	private RACING_TRACK_CATEGORY _trackCategory;

	public ScenarioRacingBettingPickDriver(string trackName)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public int GetBettingOdds(SimRaceCarDriver driver)
	{
		return 0;
	}

	private float GetDriverAndCarStrengthScore(SimRaceCarDriver driver)
	{
		return 0f;
	}

	public override string EmojiString()
	{
		return null;
	}

	private float _003CChoiceSelectedAtIndex_003Eb__6_0(SimRaceCarDriver t)
	{
		return 0f;
	}
}
