using System;

public class ScenarioRacingTimeTrialOutcomeFeedback : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRacingTrack, bool> _003C_003E9__12_0;

		public static Func<SimRacingTrack, bool> _003C_003E9__12_1;

		public static Func<SimRacingTrack, bool> _003C_003E9__12_2;

		public static Func<SimRaceGoal, bool> _003C_003E9__12_4;

		public static Func<SimRacingTrack, bool> _003C_003E9__12_3;

		public static Func<SimRacingTrack, bool> _003C_003E9__14_0;

		public static Func<SimRacingTrack, bool> _003C_003E9__14_1;

		public static Func<SimRacingTrack, bool> _003C_003E9__14_2;

		public static Func<SimRaceGoal, bool> _003C_003E9__14_4;

		public static Func<SimRacingTrack, bool> _003C_003E9__14_3;

		internal bool _003CMirrorNewTrackRecord_003Eb__12_0(SimRacingTrack x)
		{
			return false;
		}

		internal bool _003CMirrorNewTrackRecord_003Eb__12_1(SimRacingTrack x)
		{
			return false;
		}

		internal bool _003CMirrorNewTrackRecord_003Eb__12_2(SimRacingTrack x)
		{
			return false;
		}

		internal bool _003CMirrorNewTrackRecord_003Eb__12_3(SimRacingTrack x)
		{
			return false;
		}

		internal bool _003CMirrorNewTrackRecord_003Eb__12_4(SimRaceGoal y)
		{
			return false;
		}

		internal bool _003CNewTrackRecord_003Eb__14_0(SimRacingTrack x)
		{
			return false;
		}

		internal bool _003CNewTrackRecord_003Eb__14_1(SimRacingTrack x)
		{
			return false;
		}

		internal bool _003CNewTrackRecord_003Eb__14_2(SimRacingTrack x)
		{
			return false;
		}

		internal bool _003CNewTrackRecord_003Eb__14_3(SimRacingTrack x)
		{
			return false;
		}

		internal bool _003CNewTrackRecord_003Eb__14_4(SimRaceGoal y)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly RACING_INCOMPLETE_RACE_REASON _incompleteRaceReason;

	private readonly SimRaceCar _car;

	private readonly SimRacingTrack _track;

	private readonly TimeSpan _time;

	private readonly string _direction;

	private readonly float _preRaceCondition;

	public ScenarioRacingTimeTrialOutcomeFeedback(TimeSpan time, RACING_INCOMPLETE_RACE_REASON incompleteRaceReason, SimRaceCar car, SimRacingTrack track, string direction, float preRaceCondition)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void UnfinishedRace()
	{
	}

	private void FinishedRace()
	{
	}

	private void MirrorRegularFinishRace()
	{
	}

	private void MirrorNewTrackRecord()
	{
	}

	private void RegularFinishRace()
	{
	}

	private void NewTrackRecord()
	{
	}

	public override void DismissSelected()
	{
	}
}
