using System;
using System.Collections.Generic;

public class RacingCupSeriesActiveRace
{
	private class TrackResults
	{
		public int Score;

		public int Placement;

		public TimeSpan? FinishTime;

		public SimRacingTrack TrackCompleted;

		public Dictionary<SimRaceCarDriver, TrackResults> CompetitorScores;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<MiniGameDataCarRaceResult, SimRaceCar> _003C_003E9__26_0;

		public static Func<MiniGameDataCarRaceResult, int> _003C_003E9__27_0;

		public static Comparison<MiniGameDataCarRaceResult> _003C_003E9__27_1;

		internal SimRaceCar _003CHandleMissingRacer_003Eb__26_0(MiniGameDataCarRaceResult item)
		{
			return null;
		}

		internal int _003CHandleMissingRacer_003Eb__27_0(MiniGameDataCarRaceResult item)
		{
			return 0;
		}

		internal int _003CHandleMissingRacer_003Eb__27_1(MiniGameDataCarRaceResult left, MiniGameDataCarRaceResult right)
		{
			return 0;
		}
	}

	public readonly RacingCupSeriesRegistration raceCupRegistration;

	private readonly List<TrackResults> _raceResults;

	private int _trackIndex;

	private bool _trackCompleted;

	private int _pivotalMomentScore;

	public SimRacingTrack ActiveTrack => null;

	public RacingCupSeriesActiveRace(RacingCupSeriesRegistration registration)
	{
	}

	public int GetPivotalMomentScore()
	{
		return 0;
	}

	public void AddPivotalMomentFail()
	{
	}

	public void AddPivotalMomentSuccess()
	{
	}

	public bool HasAdditionalTracks()
	{
		return false;
	}

	public RacingCupSeriesResults CompleteRace(bool playerWin, RACING_INCOMPLETE_RACE_REASON incompleteReason)
	{
		return default;
	}

	public RacingCupSeriesResults CompleteRace(List<MiniGameDataCarRaceResult> miniGameResults)
	{
		return default;
	}

	public RacingCupSeriesResults GetResultsFor(SimRacingTrack track)
	{
		return default;
	}

	public int GetPlayerTotalPoints()
	{
		return 0;
	}

	public TimeSpan GetPlayerTotalTime()
	{
		return default;
	}

	public int GetRacerTotalPoints(SimRaceCarDriver driver)
	{
		return 0;
	}

	public TimeSpan GetRacerTotalTime(SimRaceCarDriver driver)
	{
		return default;
	}

	public int GetCurrentPlacement()
	{
		return 0;
	}

	public void ActivateNextTrack()
	{
	}

	public bool DidWin()
	{
		return false;
	}

	public bool DidPlace()
	{
		return false;
	}

	private RacingCupSeriesResults ConvertTrackResults(TrackResults trackResult)
	{
		return default;
	}

	private TrackResults CreateTrackResults(List<MiniGameDataCarRaceResult> miniGameResults)
	{
		return null;
	}

	private void HandleMissingRacer(List<MiniGameDataCarRaceResult> miniGameResults)
	{
	}

	private void HandleMissingRacer(List<MiniGameDataCarRaceResult> miniGameResults, SimRaceCarDriver driver)
	{
	}

	private SimRaceCarDriver FindDriverByCar(SimRaceCar car)
	{
		return null;
	}

	private TrackResults CalculateResults(bool playerWin, RACING_INCOMPLETE_RACE_REASON incompleteReason)
	{
		return null;
	}
}
