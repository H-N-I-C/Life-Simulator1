using System;
using System.Collections.Generic;

[Serializable]
public class SimTransmission : SimDiscovery
{
	private int AgeGetNextTransmission__BackingField;

	private bool IsMessageReceived__BackingField;

	private int HeroAgeWhenStartedTransmission__BackingField;

	private SimSpacePlanet OriginPlanet__BackingField;

	public KeyValuePair<string, float>? AudioFileKVP;

	private string ReceivedMessage__BackingField;

	private string DecryptedMessage__BackingField;

	private bool DecryptedMessageCorrect__BackingField;

	private string Message__BackingField;

	public int AgeGetNextTransmission
	{
		get
		{
			return AgeGetNextTransmission__BackingField;
		}
		set
		{
			AgeGetNextTransmission__BackingField = value;
		}
	}

	public bool IsMessageReceived
	{
		get
		{
			return IsMessageReceived__BackingField;
		}
		set
		{
			IsMessageReceived__BackingField = value;
		}
	}

	public int HeroAgeWhenStartedTransmission
	{
		get
		{
			return HeroAgeWhenStartedTransmission__BackingField;
		}
		set
		{
			HeroAgeWhenStartedTransmission__BackingField = value;
		}
	}

	public SimSpacePlanet OriginPlanet
	{
		get
		{
			return OriginPlanet__BackingField;
		}
		set
		{
			OriginPlanet__BackingField = value;
		}
	}

	public string ReceivedMessage
	{
		get
		{
			return ReceivedMessage__BackingField;
		}
		set
		{
			ReceivedMessage__BackingField = value;
		}
	}

	public string DecryptedMessage
	{
		get
		{
			return DecryptedMessage__BackingField;
		}
		set
		{
			DecryptedMessage__BackingField = value;
		}
	}

	public bool DecryptedMessageCorrect
	{
		get
		{
			return DecryptedMessageCorrect__BackingField;
		}
		set
		{
			DecryptedMessageCorrect__BackingField = value;
		}
	}

	public string Message
	{
		get
		{
			return Message__BackingField;
		}
		set
		{
			Message__BackingField = value;
		}
	}

	public DataAstronautInboundMessage MessageData
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimTransmission(string message)
	{
	}

	public override bool IncrementAge()
	{
		return false;
	}

	public void ScheduleFutureTransmission()
	{
	}
}
