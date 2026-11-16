using System;
using System.Collections.Generic;

[Serializable]
public class SimFashionWeek
{
	private Dictionary<SimModelingAgencyClient, float> ResultDict__BackingField;

	private List<SimModelingAgencyClient> AvailableClientList__BackingField;

	private List<SimModelingAgencyClient> ClientShowsCompletedList__BackingField;

	private string Venue__BackingField;

	private double Compensation__BackingField;

	public Dictionary<SimModelingAgencyClient, float> ResultDict
	{
		get
		{
			return ResultDict__BackingField;
		}
		set
		{
			ResultDict__BackingField = value;
		}
	}

	public List<SimModelingAgencyClient> AvailableClientList
	{
		get
		{
			return AvailableClientList__BackingField;
		}
		set
		{
			AvailableClientList__BackingField = value;
		}
	}

	public List<SimModelingAgencyClient> ClientShowsCompletedList
	{
		get
		{
			return ClientShowsCompletedList__BackingField;
		}
		set
		{
			ClientShowsCompletedList__BackingField = value;
		}
	}

	public string Venue
	{
		get
		{
			return Venue__BackingField;
		}
		set
		{
			Venue__BackingField = value;
		}
	}

	public double Compensation
	{
		get
		{
			return Compensation__BackingField;
		}
		set
		{
			Compensation__BackingField = value;
		}
	}
}
