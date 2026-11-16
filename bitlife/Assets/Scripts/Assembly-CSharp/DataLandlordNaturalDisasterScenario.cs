using System.Collections;

public class DataLandlordNaturalDisasterScenario
{
	private string Type__BackingField;

	private string Scenario__BackingField;

	private int ImpactScore__BackingField;

	private bool CanDemolish__BackingField;

	private ArrayList HouseTypeBlacklist__BackingField;

	public string Type
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

	public string Scenario
	{
		get
		{
			return Scenario__BackingField;
		}
		set
		{
			Scenario__BackingField = value;
		}
	}

	public int ImpactScore
	{
		get
		{
			return ImpactScore__BackingField;
		}
		set
		{
			ImpactScore__BackingField = value;
		}
	}

	public bool CanDemolish
	{
		get
		{
			return CanDemolish__BackingField;
		}
		set
		{
			CanDemolish__BackingField = value;
		}
	}

	public ArrayList HouseTypeBlacklist
	{
		get
		{
			return HouseTypeBlacklist__BackingField;
		}
		set
		{
			HouseTypeBlacklist__BackingField = value;
		}
	}
}
