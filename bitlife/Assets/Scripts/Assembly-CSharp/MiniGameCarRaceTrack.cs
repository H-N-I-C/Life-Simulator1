using System.Collections.Generic;
using UnityEngine;

public class MiniGameCarRaceTrack : MonoBehaviour
{
	private List<MiniGameCarRaceTrackPointData> TrackPoints1__BackingField;

	private List<MiniGameCarRaceTrackPointData> TrackPoints2__BackingField;

	private List<MiniGameCarRaceTrackPointData> TrackPoints3__BackingField;

	private List<MiniGameCarRaceTrackPointData> TrackPoints4__BackingField;

	private List<MiniGameCarRaceTrackPointData> TrackPoints5__BackingField;

	private List<MiniGameCarRaceTrackPointData> TrackPoints6__BackingField;

	private List<MiniGameCarRaceTrackPointData> CopPoints1__BackingField;

	private List<MiniGameCarRaceTrackPointData> CopPoints2__BackingField;

	private List<MiniGameCarRaceTrackPointData> CopPoints3__BackingField;

	private List<MiniGameCarRaceTrackPointData> CopPoints4__BackingField;

	private float ClockwiseLaneSpeedMult1__BackingField;

	private float ClockwiseLaneSpeedMult2__BackingField;

	private float ClockwiseLaneSpeedMult3__BackingField;

	private float CounterclockwiseLaneSpeedMult1__BackingField;

	private float CounterclockwiseLaneSpeedMult2__BackingField;

	private float CounterclockwiseLaneSpeedMult3__BackingField;

	private float NPCRaceClockwiseLaneSpeedMult1__BackingField;

	private float NPCRaceClockwiseLaneSpeedMult2__BackingField;

	private float NPCRaceClockwiseLaneSpeedMult3__BackingField;

	private float NPCCarSpeedAdvantage__BackingField;

	private bool IsClockwise__BackingField;

	private bool RightHandedSideDriving__BackingField;

	private bool IsDemolitionTrack__BackingField;

	private bool IsStreetTrack__BackingField;

	private bool IsCircuitTrack__BackingField;

	private List<List<Vector2>> Risk1Zones__BackingField;

	private List<List<Vector2>> Risk2Zones__BackingField;

	private List<List<Vector2>> Risk3Zones__BackingField;

	private List<List<Vector2>> Risk4Zones__BackingField;

	private RACING_TRACK_INDEX _index;

	private const float segmentDistance = 31f;

	private Vector2 _clockwiseFinishLineLocation;

	private Vector2 _counterclockwiseFinishLineLocation;

	private float _clockwiseFinishLineRotation;

	private float _counterclockwiseFinishLineRotation;

	public List<MiniGameCarRaceTrackPointData> TrackPoints1
	{
		get
		{
			return TrackPoints1__BackingField;
		}
		set
		{
			TrackPoints1__BackingField = value;
		}
	}

	public List<MiniGameCarRaceTrackPointData> TrackPoints2
	{
		get
		{
			return TrackPoints2__BackingField;
		}
		set
		{
			TrackPoints2__BackingField = value;
		}
	}

	public List<MiniGameCarRaceTrackPointData> TrackPoints3
	{
		get
		{
			return TrackPoints3__BackingField;
		}
		set
		{
			TrackPoints3__BackingField = value;
		}
	}

	public List<MiniGameCarRaceTrackPointData> TrackPoints4
	{
		get
		{
			return TrackPoints4__BackingField;
		}
		set
		{
			TrackPoints4__BackingField = value;
		}
	}

	public List<MiniGameCarRaceTrackPointData> TrackPoints5
	{
		get
		{
			return TrackPoints5__BackingField;
		}
		set
		{
			TrackPoints5__BackingField = value;
		}
	}

	public List<MiniGameCarRaceTrackPointData> TrackPoints6
	{
		get
		{
			return TrackPoints6__BackingField;
		}
		set
		{
			TrackPoints6__BackingField = value;
		}
	}

	public List<MiniGameCarRaceTrackPointData> CopPoints1
	{
		get
		{
			return CopPoints1__BackingField;
		}
		set
		{
			CopPoints1__BackingField = value;
		}
	}

	public List<MiniGameCarRaceTrackPointData> CopPoints2
	{
		get
		{
			return CopPoints2__BackingField;
		}
		set
		{
			CopPoints2__BackingField = value;
		}
	}

	public List<MiniGameCarRaceTrackPointData> CopPoints3
	{
		get
		{
			return CopPoints3__BackingField;
		}
		set
		{
			CopPoints3__BackingField = value;
		}
	}

	public List<MiniGameCarRaceTrackPointData> CopPoints4
	{
		get
		{
			return CopPoints4__BackingField;
		}
		set
		{
			CopPoints4__BackingField = value;
		}
	}

	public float ClockwiseLaneSpeedMult1
	{
		get
		{
			return ClockwiseLaneSpeedMult1__BackingField;
		}
		set
		{
			ClockwiseLaneSpeedMult1__BackingField = value;
		}
	}

	public float ClockwiseLaneSpeedMult2
	{
		get
		{
			return ClockwiseLaneSpeedMult2__BackingField;
		}
		set
		{
			ClockwiseLaneSpeedMult2__BackingField = value;
		}
	}

	public float ClockwiseLaneSpeedMult3
	{
		get
		{
			return ClockwiseLaneSpeedMult3__BackingField;
		}
		set
		{
			ClockwiseLaneSpeedMult3__BackingField = value;
		}
	}

	public float CounterclockwiseLaneSpeedMult1
	{
		get
		{
			return CounterclockwiseLaneSpeedMult1__BackingField;
		}
		set
		{
			CounterclockwiseLaneSpeedMult1__BackingField = value;
		}
	}

	public float CounterclockwiseLaneSpeedMult2
	{
		get
		{
			return CounterclockwiseLaneSpeedMult2__BackingField;
		}
		set
		{
			CounterclockwiseLaneSpeedMult2__BackingField = value;
		}
	}

	public float CounterclockwiseLaneSpeedMult3
	{
		get
		{
			return CounterclockwiseLaneSpeedMult3__BackingField;
		}
		set
		{
			CounterclockwiseLaneSpeedMult3__BackingField = value;
		}
	}

	public float NPCRaceClockwiseLaneSpeedMult1
	{
		get
		{
			return NPCRaceClockwiseLaneSpeedMult1__BackingField;
		}
		set
		{
			NPCRaceClockwiseLaneSpeedMult1__BackingField = value;
		}
	}

	public float NPCRaceClockwiseLaneSpeedMult2
	{
		get
		{
			return NPCRaceClockwiseLaneSpeedMult2__BackingField;
		}
		set
		{
			NPCRaceClockwiseLaneSpeedMult2__BackingField = value;
		}
	}

	public float NPCRaceClockwiseLaneSpeedMult3
	{
		get
		{
			return NPCRaceClockwiseLaneSpeedMult3__BackingField;
		}
		set
		{
			NPCRaceClockwiseLaneSpeedMult3__BackingField = value;
		}
	}

	public float NPCCarSpeedAdvantage
	{
		get
		{
			return NPCCarSpeedAdvantage__BackingField;
		}
		set
		{
			NPCCarSpeedAdvantage__BackingField = value;
		}
	}

	public bool IsClockwise
	{
		get
		{
			return IsClockwise__BackingField;
		}
		set
		{
			IsClockwise__BackingField = value;
		}
	}

	public bool RightHandedSideDriving
	{
		get
		{
			return RightHandedSideDriving__BackingField;
		}
		set
		{
			RightHandedSideDriving__BackingField = value;
		}
	}

	public bool IsDemolitionTrack
	{
		get
		{
			return IsDemolitionTrack__BackingField;
		}
		set
		{
			IsDemolitionTrack__BackingField = value;
		}
	}

	public bool IsStreetTrack
	{
		get
		{
			return IsStreetTrack__BackingField;
		}
		set
		{
			IsStreetTrack__BackingField = value;
		}
	}

	public bool IsCircuitTrack
	{
		get
		{
			return IsCircuitTrack__BackingField;
		}
		set
		{
			IsCircuitTrack__BackingField = value;
		}
	}

	public List<List<Vector2>> Risk1Zones
	{
		get
		{
			return Risk1Zones__BackingField;
		}
		set
		{
			Risk1Zones__BackingField = value;
		}
	}

	public List<List<Vector2>> Risk2Zones
	{
		get
		{
			return Risk2Zones__BackingField;
		}
		set
		{
			Risk2Zones__BackingField = value;
		}
	}

	public List<List<Vector2>> Risk3Zones
	{
		get
		{
			return Risk3Zones__BackingField;
		}
		set
		{
			Risk3Zones__BackingField = value;
		}
	}

	public List<List<Vector2>> Risk4Zones
	{
		get
		{
			return Risk4Zones__BackingField;
		}
		set
		{
			Risk4Zones__BackingField = value;
		}
	}

	public void InitWithParameters(RACING_TRACK_INDEX trackIndexIndex, bool clockwise, bool rightHandedSideDriving, bool forceDisplayFinishLine)
	{
	}

	private void ReverseTrackPointData(List<MiniGameCarRaceTrackPointData> trackPointData)
	{
	}

	private List<MiniGameCarRaceTrackPointData> ProcessPoints(List<MiniGameCarRaceTrackPointData> trackPointData, bool closedPath)
	{
		return null;
	}

	public List<MiniGameCarRaceTrackPointData> CollectLanePoints(List<Vector2> trackPoints, bool closedLoop = true)
	{
		return null;
	}

	public void CollectRiskZonePointsForTarget(List<Vector2> riskZonePoints, List<List<Vector2>> target)
	{
	}

	public List<MiniGameCarRaceTrackPointData> ShiftPoints(List<MiniGameCarRaceTrackPointData> trackPoints, bool forward)
	{
		return null;
	}

	private Vector2 GetLocalTrackPointForPoint(Vector2 point)
	{
		return default;
	}

	private bool PointInPoly(List<Vector2> poly, Vector2 point)
	{
		return false;
	}

	private int GetRiskForPoint(Vector2 point)
	{
		return 0;
	}
}
