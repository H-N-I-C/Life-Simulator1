using System.Collections;

public class DataBusinessDemandFluctuation
{
	private ArrayList SpecificIndustry__BackingField;

	private ArrayList SpecificProduct__BackingField;

	private int FluctuationType__BackingField;

	private int FluctuationAmount__BackingField;

	private string Snippet__BackingField;

	private int CommonnessScore__BackingField;

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

	public int FluctuationType
	{
		get
		{
			return FluctuationType__BackingField;
		}
		set
		{
			FluctuationType__BackingField = value;
		}
	}

	public int FluctuationAmount
	{
		get
		{
			return FluctuationAmount__BackingField;
		}
		set
		{
			FluctuationAmount__BackingField = value;
		}
	}

	public string Snippet
	{
		get
		{
			return Snippet__BackingField;
		}
		set
		{
			Snippet__BackingField = value;
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
