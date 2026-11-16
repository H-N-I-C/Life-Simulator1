using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimRacingTrack
{
	private float _bestRaceTime__BackingField;

	private float _bestTimeTrialTime__BackingField;

	private float _bestMirrorTimeTrialTime__BackingField;

	private string VehicleUsed__BackingField;

	private int TimesWon__BackingField;

	private int TimesLost__BackingField;

	private double LifetimeWinnings__BackingField;

	private int NumTimesRacedAtAge__BackingField;

	private bool HasFinishedRaceOnce__BackingField;

	private string TrackName__BackingField;

	[NonSerialized]
	private DataRacingRaceTrack _trackData;

	private List<SimRaceGoal> Goals__BackingField;

	private int TotalCupsWon__BackingField;

	private int TotalCupsLost__BackingField;

	private string TimeTrialVehicleUsed__BackingField;

	private bool HasDoneTimeTrialOnce__BackingField;

	private float _bestRaceTime
	{
		get
		{
			return _bestRaceTime__BackingField;
		}
		set
		{
			_bestRaceTime__BackingField = value;
		}
	}

	private float _bestTimeTrialTime
	{
		get
		{
			return _bestTimeTrialTime__BackingField;
		}
		set
		{
			_bestTimeTrialTime__BackingField = value;
		}
	}

	private float _bestMirrorTimeTrialTime
	{
		get
		{
			return _bestMirrorTimeTrialTime__BackingField;
		}
		set
		{
			_bestMirrorTimeTrialTime__BackingField = value;
		}
	}

	public string VehicleUsed
	{
		get
		{
			return VehicleUsed__BackingField;
		}
		set
		{
			VehicleUsed__BackingField = value;
		}
	}

	public int TimesWon
	{
		get
		{
			return TimesWon__BackingField;
		}
		set
		{
			TimesWon__BackingField = value;
		}
	}

	public int TimesLost
	{
		get
		{
			return TimesLost__BackingField;
		}
		set
		{
			TimesLost__BackingField = value;
		}
	}

	public double LifetimeWinnings
	{
		get
		{
			return LifetimeWinnings__BackingField;
		}
		set
		{
			LifetimeWinnings__BackingField = value;
		}
	}

	public int NumTimesRacedAtAge
	{
		get
		{
			return NumTimesRacedAtAge__BackingField;
		}
		set
		{
			NumTimesRacedAtAge__BackingField = value;
		}
	}

	public bool HasFinishedRaceOnce
	{
		get
		{
			return HasFinishedRaceOnce__BackingField;
		}
		set
		{
			HasFinishedRaceOnce__BackingField = value;
		}
	}

	public string TrackName
	{
		get
		{
			return TrackName__BackingField;
		}
		set
		{
			TrackName__BackingField = value;
		}
	}

	public DataRacingRaceTrack TrackData
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public List<SimRaceGoal> Goals
	{
		get
		{
			return Goals__BackingField;
		}
		set
		{
			Goals__BackingField = value;
		}
	}

	public int TotalCupsWon
	{
		get
		{
			return TotalCupsWon__BackingField;
		}
		set
		{
			TotalCupsWon__BackingField = value;
		}
	}

	public int TotalCupsLost
	{
		get
		{
			return TotalCupsLost__BackingField;
		}
		set
		{
			TotalCupsLost__BackingField = value;
		}
	}

	public string TimeTrialVehicleUsed
	{
		get
		{
			return TimeTrialVehicleUsed__BackingField;
		}
		set
		{
			TimeTrialVehicleUsed__BackingField = value;
		}
	}

	public bool HasDoneTimeTrialOnce
	{
		get
		{
			return HasDoneTimeTrialOnce__BackingField;
		}
		set
		{
			HasDoneTimeTrialOnce__BackingField = value;
		}
	}

	public SimRacingTrack(DataRacingRaceTrack trackData)
	{
	}

	public bool IsUnlocked(SimRacing racer, RACING_TRACK_CATEGORY category)
	{
		return false;
	}

	public int GetBaselineLaps()
	{
		return 0;
	}

	public string GetTimeTrialTrackTimeString(bool isMirroredTime)
	{
		return null;
	}

	public string GetRaceTrackTimeString()
	{
		return null;
	}

	public string GetBestTimeTrialTrackTimeString()
	{
		return null;
	}

	public TimeSpan GetBestTimeTrialTime()
	{
		return default;
	}

	public TimeSpan GetBestMirrorTimeTrialTime()
	{
		return default;
	}

	public void SetBestMirrorTimeTrialTime(TimeSpan time)
	{
	}

	public void SetBestTimeTrialTime(TimeSpan time)
	{
	}

	public bool HasBestTimeTrialTime()
	{
		return false;
	}

	public bool HasBestMirrorTimeTrialTime()
	{
		return false;
	}

	public void SetBestRaceTime(TimeSpan heroTime)
	{
	}

	public bool HasBestRaceTime()
	{
		return false;
	}

	public TimeSpan GetBestRaceTime()
	{
		return default;
	}

	public int GetBestTimeTrialStars()
	{
		return 0;
	}
}
