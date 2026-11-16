using System;

public class ScenarioRacingCupSeriesCupCeremony : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRaceCarDriver, bool> _003C_003E9__5_0;

		public static Func<SimRaceCarDriver, bool> _003C_003E9__6_0;

		internal bool _003CCreateWinCeremony_003Eb__5_0(SimRaceCarDriver driver)
		{
			return false;
		}

		internal bool _003CCreatePlacementCeremony_003Eb__6_0(SimRaceCarDriver driver)
		{
			return false;
		}
	}

	private readonly RacingCupSeriesResults _raceResults;

	private string _narrative;

	public ScenarioRacingCupSeriesCupCeremony(RacingCupSeriesResults raceCupResults)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void CreateWinCeremony()
	{
	}

	private void CreatePlacementCeremony()
	{
	}

	private void CreateLoseCeremony()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private int GetWinnings()
	{
		return 0;
	}

	private bool DidWin()
	{
		return false;
	}

	private bool DidPlace()
	{
		return false;
	}

	private int GetPlacement()
	{
		return 0;
	}

	private string GetPlacementDescription()
	{
		return null;
	}
}
