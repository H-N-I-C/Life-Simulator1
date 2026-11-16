using System;
using System.Collections;

[Serializable]
public class SimSpaceMission
{
	private bool Completed__BackingField;

	private bool TaskExecuted__BackingField;

	private int MissionStatus__BackingField;

	private int HeroAgeAtStart__BackingField;

	private float Att_performance__BackingField;

	private string MissionTypeString__BackingField;

	private string Objective__BackingField;

	private ArrayList Crew__BackingField;

	private string Location__BackingField;

	private SimSpaceShuttle Shuttle__BackingField;

	private int Type__BackingField;

	private SimSpaceSatellite Satellite__BackingField;

	private SimArtifact ArtifactDiscovered__BackingField;

	private bool PlayedMiniAtStart__BackingField;

	private int Risk__BackingField;

	private int RiskFromData__BackingField;

	public bool Completed
	{
		get
		{
			return Completed__BackingField;
		}
		set
		{
			Completed__BackingField = value;
		}
	}

	public bool TaskExecuted
	{
		get
		{
			return TaskExecuted__BackingField;
		}
		set
		{
			TaskExecuted__BackingField = value;
		}
	}

	public int MissionStatus
	{
		get
		{
			return MissionStatus__BackingField;
		}
		set
		{
			MissionStatus__BackingField = value;
		}
	}

	public int HeroAgeAtStart
	{
		get
		{
			return HeroAgeAtStart__BackingField;
		}
		set
		{
			HeroAgeAtStart__BackingField = value;
		}
	}

	public float Att_performance
	{
		get
		{
			return Att_performance__BackingField;
		}
		set
		{
			Att_performance__BackingField = value;
		}
	}

	public string MissionTypeString
	{
		get
		{
			return MissionTypeString__BackingField;
		}
		set
		{
			MissionTypeString__BackingField = value;
		}
	}

	public string Objective
	{
		get
		{
			return Objective__BackingField;
		}
		set
		{
			Objective__BackingField = value;
		}
	}

	public ArrayList Crew
	{
		get
		{
			return Crew__BackingField;
		}
		set
		{
			Crew__BackingField = value;
		}
	}

	public string Location
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

	public SimSpaceShuttle Shuttle
	{
		get
		{
			return Shuttle__BackingField;
		}
		set
		{
			Shuttle__BackingField = value;
		}
	}

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public SimSpaceSatellite Satellite
	{
		get
		{
			return Satellite__BackingField;
		}
		set
		{
			Satellite__BackingField = value;
		}
	}

	public SimArtifact ArtifactDiscovered
	{
		get
		{
			return ArtifactDiscovered__BackingField;
		}
		set
		{
			ArtifactDiscovered__BackingField = value;
		}
	}

	public bool PlayedMiniAtStart
	{
		get
		{
			return PlayedMiniAtStart__BackingField;
		}
		set
		{
			PlayedMiniAtStart__BackingField = value;
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

	public int RiskFromData
	{
		get
		{
			return RiskFromData__BackingField;
		}
		set
		{
			RiskFromData__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	public Scenario GenerateMissionScenario()
	{
		return null;
	}

	public void GenerateCrew()
	{
	}

	public string GetCrewString()
	{
		return null;
	}

	public void ImpactPerformance(int impactScore)
	{
	}

	public void EndMissionWithReason(int reason)
	{
	}

	public static string GetMissionTypeString(int type)
	{
		return null;
	}

	private Scenario GenerateDescentChainScenarios()
	{
		return null;
	}

	public Scenario GenerateFailedFlightScenarios()
	{
		return null;
	}

	public SimAstronaut GetRandomCrew()
	{
		return null;
	}
}
