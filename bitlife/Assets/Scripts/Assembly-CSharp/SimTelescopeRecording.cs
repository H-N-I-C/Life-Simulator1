using System;

[Serializable]
public class SimTelescopeRecording : SimDiscovery
{
	private string PlanetName__BackingField;

	private string EventName__BackingField;

	private string EventLogString__BackingField;

	private string EventTime__BackingField;

	private int ResearchScore__BackingField;

	public string PlanetName
	{
		get
		{
			return PlanetName__BackingField;
		}
		set
		{
			PlanetName__BackingField = value;
		}
	}

	public string EventName
	{
		get
		{
			return EventName__BackingField;
		}
		set
		{
			EventName__BackingField = value;
		}
	}

	public string EventLogString
	{
		get
		{
			return EventLogString__BackingField;
		}
		set
		{
			EventLogString__BackingField = value;
		}
	}

	public string EventTime
	{
		get
		{
			return EventTime__BackingField;
		}
		set
		{
			EventTime__BackingField = value;
		}
	}

	public int ResearchScore
	{
		get
		{
			return ResearchScore__BackingField;
		}
		set
		{
			ResearchScore__BackingField = value;
		}
	}

	public override bool IncrementAge()
	{
		return false;
	}
}
