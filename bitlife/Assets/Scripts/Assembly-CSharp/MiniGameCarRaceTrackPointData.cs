using UnityEngine;

public class MiniGameCarRaceTrackPointData
{
	private Vector2 Location__BackingField;

	private int Risk__BackingField;

	public Vector2 Location
	{
		get
		{
			return Location__BackingField;
		}
		set
		{
			Location__BackingField = value;
		}
	}

	public int Risk
	{
		get
		{
			return Risk__BackingField;
		}
		set
		{
			Risk__BackingField = value;
		}
	}

	public MiniGameCarRaceTrackPointData(Vector2 location, int risk)
	{
	}
}
