public class DataJewelryGemstone
{
	private string Name__BackingField;

	private int Color__BackingField;

	private float MinCarats__BackingField;

	private float MaxCarats__BackingField;

	private float SuperMaxCarats__BackingField;

	private bool ShowCarats__BackingField;

	private float FirstCaratPrice__BackingField;

	private float MinCost__BackingField;

	private float MaxCost__BackingField;

	private int CommonnessScore__BackingField;

	private bool Luxury__BackingField;

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

	public int Color
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

	public float MinCarats
	{
		get
		{
			return MinCarats__BackingField;
		}
		set
		{
			MinCarats__BackingField = value;
		}
	}

	public float MaxCarats
	{
		get
		{
			return MaxCarats__BackingField;
		}
		set
		{
			MaxCarats__BackingField = value;
		}
	}

	public float SuperMaxCarats
	{
		get
		{
			return SuperMaxCarats__BackingField;
		}
		set
		{
			SuperMaxCarats__BackingField = value;
		}
	}

	public bool ShowCarats
	{
		get
		{
			return ShowCarats__BackingField;
		}
		set
		{
			ShowCarats__BackingField = value;
		}
	}

	public float FirstCaratPrice
	{
		get
		{
			return FirstCaratPrice__BackingField;
		}
		set
		{
			FirstCaratPrice__BackingField = value;
		}
	}

	public float MinCost
	{
		get
		{
			return MinCost__BackingField;
		}
		set
		{
			MinCost__BackingField = value;
		}
	}

	public float MaxCost
	{
		get
		{
			return MaxCost__BackingField;
		}
		set
		{
			MaxCost__BackingField = value;
		}
	}

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}

	public bool Luxury
	{
		get
		{
			return Luxury__BackingField;
		}
		set
		{
			Luxury__BackingField = value;
		}
	}

	public string ColorString()
	{
		return null;
	}

	public float GetValueForCarats(float numCarats)
	{
		return 0f;
	}
}
