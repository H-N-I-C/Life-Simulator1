public class DataJewelryMetal
{
	private string Name__BackingField;

	private int Color__BackingField;

	private int CostScore__BackingField;

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

	public int CostScore
	{
		get
		{
			return CostScore__BackingField;
		}
		set
		{
			CostScore__BackingField = value;
		}
	}

	public string ColorString()
	{
		return null;
	}
}
