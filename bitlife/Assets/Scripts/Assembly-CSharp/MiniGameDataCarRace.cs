using System.Collections.Generic;
using Game;

public class MiniGameDataCarRace
{
	private DataRacingRaceTrack TrackData__BackingField;

	private bool Clockwise__BackingField;

	private int Laps__BackingField;

	private int Warnings__BackingField;

	private List<MiniGameDataCarRaceResult> RaceResults__BackingField;

	private List<SimRaceCarDriver> NPCDrivers__BackingField;

	private SimRaceCar HeroCar__BackingField;

	private SimRaceCarDriver BetWinningDriver__BackingField;

	private SimRaceCarDriver BetChosenDriver__BackingField;

	private RACING_TRACK_FORMAT Format__BackingField;

	private RACING_TRACK_WEAR_AND_TEAR_STYLE WearAndTearStyle__BackingField;

	private RACING_STYLE RaceStyle__BackingField;

	public DataRacingRaceTrack TrackData
	{
		get
		{
			return TrackData__BackingField;
		}
		set
		{
			TrackData__BackingField = value;
		}
	}

	public bool Clockwise
	{
		get
		{
			return Clockwise__BackingField;
		}
		set
		{
			Clockwise__BackingField = value;
		}
	}

	public int Laps
	{
		get
		{
			return Laps__BackingField;
		}
		set
		{
			Laps__BackingField = value;
		}
	}

	public int Warnings
	{
		get
		{
			return Warnings__BackingField;
		}
		set
		{
			Warnings__BackingField = value;
		}
	}

	public List<MiniGameDataCarRaceResult> RaceResults
	{
		get
		{
			return RaceResults__BackingField;
		}
		set
		{
			RaceResults__BackingField = value;
		}
	}

	public List<SimRaceCarDriver> NPCDrivers
	{
		get
		{
			return NPCDrivers__BackingField;
		}
		set
		{
			NPCDrivers__BackingField = value;
		}
	}

	public SimRaceCar HeroCar
	{
		get
		{
			return HeroCar__BackingField;
		}
		set
		{
			HeroCar__BackingField = value;
		}
	}

	public SimRaceCarDriver BetWinningDriver
	{
		get
		{
			return BetWinningDriver__BackingField;
		}
		set
		{
			BetWinningDriver__BackingField = value;
		}
	}

	public SimRaceCarDriver BetChosenDriver
	{
		get
		{
			return BetChosenDriver__BackingField;
		}
		set
		{
			BetChosenDriver__BackingField = value;
		}
	}

	public RACING_TRACK_FORMAT Format
	{
		get
		{
			return Format__BackingField;
		}
		set
		{
			Format__BackingField = value;
		}
	}

	public RACING_TRACK_WEAR_AND_TEAR_STYLE WearAndTearStyle
	{
		get
		{
			return WearAndTearStyle__BackingField;
		}
		set
		{
			WearAndTearStyle__BackingField = value;
		}
	}

	public RACING_STYLE RaceStyle
	{
		get
		{
			return RaceStyle__BackingField;
		}
		set
		{
			RaceStyle__BackingField = value;
		}
	}
}
