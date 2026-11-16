using System;
using System.Collections.Generic;

public class ScenarioRacingCupSeriesOutcomeFeedback : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRaceCarDriver, bool> _003C_003E9__10_0;

		internal bool _003CRefreshScenario_003Eb__10_0(SimRaceCarDriver t)
		{
			return false;
		}
	}

	private readonly RacingCupSeriesActiveRace _Race;

	private readonly bool _allowGenericImpacts;

	private readonly List<MiniGameDataCarRaceResult> _MiniGameRaceResults;

	private readonly MiniGameDataCarRaceResult _MiniGameWinner;

	private readonly MiniGameDataCarRaceResult _MiniGameHeroResult;

	private readonly bool _PivotalMomentWinner;

	private readonly RACING_INCOMPLETE_RACE_REASON _PivotalMomentIncompleteReason;

	private string _narrative;

	public ScenarioRacingCupSeriesOutcomeFeedback(RacingCupSeriesActiveRace race, List<MiniGameDataCarRaceResult> miniGameRaceResults, bool allowVehicleImpact = true)
	{
	}

	public ScenarioRacingCupSeriesOutcomeFeedback(RacingCupSeriesActiveRace race, bool heroWasWinner, RACING_INCOMPLETE_RACE_REASON heroIncompleteReason, bool allowVehicleImpact = true)
	{
	}

	public override void RefreshScenario()
	{
	}

	private bool IsHeroWinner()
	{
		return false;
	}

	private RACING_INCOMPLETE_RACE_REASON GetIncompleteReason()
	{
		return RACING_INCOMPLETE_RACE_REASON.NONE;
	}

	private SimRaceCarDriver GetNPCWinningDriver(RacingCupSeriesResults racingCupSeriesResults)
	{
		return null;
	}

	private MiniGameDataCarRaceResult GetMiniGameWinningResult()
	{
		return null;
	}

	private MiniGameDataCarRaceResult GetMiniGameHeroRaceResult()
	{
		return null;
	}

	private RacingCupSeriesResults CompleteRace(bool didWin, RACING_INCOMPLETE_RACE_REASON incompleteReason)
	{
		return default;
	}

	private void ImpactCarCondition(int impact)
	{
	}
}
