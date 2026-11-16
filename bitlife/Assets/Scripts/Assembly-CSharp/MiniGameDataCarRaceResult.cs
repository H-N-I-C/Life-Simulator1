using System;

public class MiniGameDataCarRaceResult
{
	private string DriverName__BackingField;

	private SimRaceCar RaceCar__BackingField;

	private int Place__BackingField;

	private TimeSpan FinishTime__BackingField;

	private RACING_INCOMPLETE_RACE_REASON IncompleteRaceReason__BackingField;

	public string DriverName
	{
		get
		{
			return DriverName__BackingField;
		}
		set
		{
			DriverName__BackingField = value;
		}
	}

	public SimRaceCar RaceCar
	{
		get
		{
			return RaceCar__BackingField;
		}
		set
		{
			RaceCar__BackingField = value;
		}
	}

	public int Place
	{
		get
		{
			return Place__BackingField;
		}
		set
		{
			Place__BackingField = value;
		}
	}

	public TimeSpan FinishTime
	{
		get
		{
			return FinishTime__BackingField;
		}
		set
		{
			FinishTime__BackingField = value;
		}
	}

	public RACING_INCOMPLETE_RACE_REASON IncompleteRaceReason
	{
		get
		{
			return IncompleteRaceReason__BackingField;
		}
		set
		{
			IncompleteRaceReason__BackingField = value;
		}
	}
}
