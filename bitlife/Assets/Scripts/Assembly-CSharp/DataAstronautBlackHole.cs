public class DataAstronautBlackHole
{
	private string Name__BackingField;

	private float Diameter__BackingField;

	private float Mass__BackingField;

	private float EventHorizon__BackingField;

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

	public float EventHorizon
	{
		get
		{
			return EventHorizon__BackingField;
		}
		set
		{
			EventHorizon__BackingField = value;
		}
	}
}
