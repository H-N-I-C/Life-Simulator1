using System;

[Serializable]
public class CustomCity
{
	public string Name;

	public string CountryName;

	[NonSerialized]
	private DataCountry _Country;

	public DataCountry Country
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private CustomCity()
	{
	}

	public CustomCity(string theName, string countryName)
	{
	}
}
