using System.Collections;

public class DataBusinessProductName
{
	private ArrayList SpecificIndustry__BackingField;

	private ArrayList SpecificProduct__BackingField;

	private ArrayList Format__BackingField;

	private int CommonnessScore__BackingField;

	private bool HeroOwnsCompany__BackingField;

	public ArrayList SpecificIndustry
	{
		get
		{
			return SpecificIndustry__BackingField;
		}
		set
		{
			SpecificIndustry__BackingField = value;
		}
	}

	public ArrayList SpecificProduct
	{
		get
		{
			return SpecificProduct__BackingField;
		}
		set
		{
			SpecificProduct__BackingField = value;
		}
	}

	public ArrayList Format
	{
		get
		{
			return Format__BackingField;
		}
		set
		{
			Format__BackingField = value;
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

	public bool HeroOwnsCompany
	{
		get
		{
			return HeroOwnsCompany__BackingField;
		}
		set
		{
			HeroOwnsCompany__BackingField = value;
		}
	}
}
