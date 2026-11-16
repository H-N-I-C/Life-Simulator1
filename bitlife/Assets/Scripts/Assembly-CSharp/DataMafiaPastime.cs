using System.Collections;

public class DataMafiaPastime
{
	private string Pastime__BackingField;

	private bool WorksWithAllMobCountries__BackingField;

	private ArrayList SpecificMobCountryCodeArray__BackingField;

	public string Pastime
	{
		get
		{
			return Pastime__BackingField;
		}
		set
		{
			Pastime__BackingField = value;
		}
	}

	public bool WorksWithAllMobCountries
	{
		get
		{
			return WorksWithAllMobCountries__BackingField;
		}
		set
		{
			WorksWithAllMobCountries__BackingField = value;
		}
	}

	public ArrayList SpecificMobCountryCodeArray
	{
		get
		{
			return SpecificMobCountryCodeArray__BackingField;
		}
		set
		{
			SpecificMobCountryCodeArray__BackingField = value;
		}
	}
}
