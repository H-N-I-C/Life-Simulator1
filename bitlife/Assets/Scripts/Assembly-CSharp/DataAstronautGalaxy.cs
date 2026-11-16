public class DataAstronautGalaxy
{
	private string Name__BackingField;

	private string Classification__BackingField;

	private float Diameter__BackingField;

	private float NumberOfStars__BackingField;

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

	public float NumberOfStars
	{
		get
		{
			return NumberOfStars__BackingField;
		}
		set
		{
			NumberOfStars__BackingField = value;
		}
	}
}
