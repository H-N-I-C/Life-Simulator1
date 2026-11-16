public class DataAstronautComet
{
	private string Name__BackingField;

	private string Color__BackingField;

	private float Period__BackingField;

	private float Diameter__BackingField;

	private float TailLength__BackingField;

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public string Color
	{
		get
		{
			return Color__BackingField;
		}
		set
		{
			Color__BackingField = value;
		}
	}

	public float Period
	{
		get
		{
			return Period__BackingField;
		}
		set
		{
			Period__BackingField = value;
		}
	}

	public float Diameter
	{
		get
		{
			return Diameter__BackingField;
		}
		set
		{
			Diameter__BackingField = value;
		}
	}

	public float TailLength
	{
		get
		{
			return TailLength__BackingField;
		}
		set
		{
			TailLength__BackingField = value;
		}
	}
}
