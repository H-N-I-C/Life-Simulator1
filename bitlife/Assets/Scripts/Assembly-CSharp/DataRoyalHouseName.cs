using System.Collections;

public class DataRoyalHouseName
{
	private string Name__BackingField;

	private bool Estate__BackingField;

	private bool Palace__BackingField;

	private bool Castle__BackingField;

	private ArrayList CountryArray__BackingField;

	private int CommonnessScore__BackingField;

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

	public bool Estate
	{
		get
		{
			return Estate__BackingField;
		}
		set
		{
			Estate__BackingField = value;
		}
	}

	public bool Palace
	{
		get
		{
			return Palace__BackingField;
		}
		set
		{
			Palace__BackingField = value;
		}
	}

	public bool Castle
	{
		get
		{
			return Castle__BackingField;
		}
		set
		{
			Castle__BackingField = value;
		}
	}

	public ArrayList CountryArray
	{
		get
		{
			return CountryArray__BackingField;
		}
		set
		{
			CountryArray__BackingField = value;
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
}
