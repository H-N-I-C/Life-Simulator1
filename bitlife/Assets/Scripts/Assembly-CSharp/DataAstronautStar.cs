public class DataAstronautStar
{
	private string Name__BackingField;

	private string Classification__BackingField;

	private string Color__BackingField;

	private float Mass__BackingField;

	private float Diameter__BackingField;

	private int PlanetsInOrbit__BackingField;

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

	public string Classification
	{
		get
		{
			return Classification__BackingField;
		}
		set
		{
			Classification__BackingField = value;
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

	public float Mass
	{
		get
		{
			return Mass__BackingField;
		}
		set
		{
			Mass__BackingField = value;
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

	public int PlanetsInOrbit
	{
		get
		{
			return PlanetsInOrbit__BackingField;
		}
		set
		{
			PlanetsInOrbit__BackingField = value;
		}
	}
}
