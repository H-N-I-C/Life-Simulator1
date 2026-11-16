using System;

[Serializable]
public class SimPartTimeFreelance
{
	private string _partTimeJobName;

	private float HourlyRate__BackingField;

	private int HoursCompleted__BackingField;

	private int HeroAge__BackingField;

	[NonSerialized]
	public DataStreetPerson Customer;

	public DataPartTimeJob Job
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float HourlyRate
	{
		get
		{
			return HourlyRate__BackingField;
		}
		set
		{
			HourlyRate__BackingField = value;
		}
	}

	public int HoursCompleted
	{
		get
		{
			return HoursCompleted__BackingField;
		}
		set
		{
			HoursCompleted__BackingField = value;
		}
	}

	public int HeroAge
	{
		get
		{
			return HeroAge__BackingField;
		}
		set
		{
			HeroAge__BackingField = value;
		}
	}
}
