using System;
using Game;

[Serializable]
public class SimStreetName
{
	private string Name__BackingField;

	private int Type__BackingField;

	private int AffluenceScore__BackingField;

	private int NumberMin__BackingField;

	private int NumberMax__BackingField;

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

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public int AffluenceScore
	{
		get
		{
			return AffluenceScore__BackingField;
		}
		set
		{
			AffluenceScore__BackingField = value;
		}
	}

	public int NumberMin
	{
		get
		{
			return NumberMin__BackingField;
		}
		set
		{
			NumberMin__BackingField = value;
		}
	}

	public int NumberMax
	{
		get
		{
			return NumberMax__BackingField;
		}
		set
		{
			NumberMax__BackingField = value;
		}
	}

	public SimStreetName(DataStreetName theData, DataCountry country, string city, int theType)
	{
	}

	public SimStreetName(DataCultCommuneStreetName theData, DataCountry country, string city)
	{
	}

	public SimStreetName(DataSecretAgentAgencyStreetName theData, DataCountry country, string city)
	{
	}

	public SimStreetName(DataVampireStreetName data)
	{
	}

	public SimStreetName(string nameString, bool addRandomStreetType)
	{
	}

	public SimStreetName(DataDealerStreetName theData, DataCountry country, string city)
	{
	}

	public SimStreetName(DataCasinoStreetName theData, DataCountry country, string city)
	{
	}

	public SimStreetName(DataRacingRaceCarGarageStreetName theData, DataCountry country, string city)
	{
	}

	public int GetRandomStreetAddress()
	{
		return 0;
	}
}
