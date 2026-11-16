using System.Collections;

public class DataAstronautPlanet
{
	private string Name__BackingField;

	private string Classification__BackingField;

	private ArrayList Color__BackingField;

	private float Diameter__BackingField;

	private float Mass__BackingField;

	private double Age__BackingField;

	private bool HasRings__BackingField;

	private int NumberOfMoons__BackingField;

	private float HabitabilityScore__BackingField;

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

	public ArrayList Color
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

	public double Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public bool HasRings
	{
		get
		{
			return HasRings__BackingField;
		}
		set
		{
			HasRings__BackingField = value;
		}
	}

	public int NumberOfMoons
	{
		get
		{
			return NumberOfMoons__BackingField;
		}
		set
		{
			NumberOfMoons__BackingField = value;
		}
	}

	public float HabitabilityScore
	{
		get
		{
			return HabitabilityScore__BackingField;
		}
		set
		{
			HabitabilityScore__BackingField = value;
		}
	}
}
