using System.Collections.Generic;

public class DataAircraftPart
{
	private string Name__BackingField;

	private bool OnlyOnePerAircraft__BackingField;

	private int CostScore__BackingField;

	private List<string> BrokenTerms__BackingField;

	private bool Helicopter__BackingField;

	private bool Jet__BackingField;

	private bool Propeller__BackingField;

	private bool Glider__BackingField;

	private bool Blimp__BackingField;

	private bool Balloon__BackingField;

	private List<string> OptionalDescriptions__BackingField;

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

	public bool OnlyOnePerAircraft
	{
		get
		{
			return OnlyOnePerAircraft__BackingField;
		}
		set
		{
			OnlyOnePerAircraft__BackingField = value;
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

	public List<string> BrokenTerms
	{
		get
		{
			return BrokenTerms__BackingField;
		}
		set
		{
			BrokenTerms__BackingField = value;
		}
	}

	public bool Helicopter
	{
		get
		{
			return Helicopter__BackingField;
		}
		set
		{
			Helicopter__BackingField = value;
		}
	}

	public bool Jet
	{
		get
		{
			return Jet__BackingField;
		}
		set
		{
			Jet__BackingField = value;
		}
	}

	public bool Propeller
	{
		get
		{
			return Propeller__BackingField;
		}
		set
		{
			Propeller__BackingField = value;
		}
	}

	public bool Glider
	{
		get
		{
			return Glider__BackingField;
		}
		set
		{
			Glider__BackingField = value;
		}
	}

	public bool Blimp
	{
		get
		{
			return Blimp__BackingField;
		}
		set
		{
			Blimp__BackingField = value;
		}
	}

	public bool Balloon
	{
		get
		{
			return Balloon__BackingField;
		}
		set
		{
			Balloon__BackingField = value;
		}
	}

	public List<string> OptionalDescriptions
	{
		get
		{
			return OptionalDescriptions__BackingField;
		}
		set
		{
			OptionalDescriptions__BackingField = value;
		}
	}
}
