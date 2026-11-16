public class DataAstronautAsteroid
{
	private string Name__BackingField;

	private string Type__BackingField;

	private float Diameter__BackingField;

	private int SizeScore__BackingField;

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

	public string Type
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

	public int SizeScore
	{
		get
		{
			return SizeScore__BackingField;
		}
		set
		{
			SizeScore__BackingField = value;
		}
	}
}
