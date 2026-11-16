using System.Collections;

public class DataDrugScenario
{
	private int Condition__BackingField;

	private string Scenario__BackingField;

	private string Cta__BackingField;

	private string YesChoice__BackingField;

	private string NoChoice__BackingField;

	private ArrayList SpecificDrugArray__BackingField;

	public int Condition
	{
		get
		{
			return Condition__BackingField;
		}
		set
		{
			Condition__BackingField = value;
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

	public string Cta
	{
		get
		{
			return Cta__BackingField;
		}
		set
		{
			Cta__BackingField = value;
		}
	}

	public string YesChoice
	{
		get
		{
			return YesChoice__BackingField;
		}
		set
		{
			YesChoice__BackingField = value;
		}
	}

	public string NoChoice
	{
		get
		{
			return NoChoice__BackingField;
		}
		set
		{
			NoChoice__BackingField = value;
		}
	}

	public ArrayList SpecificDrugArray
	{
		get
		{
			return SpecificDrugArray__BackingField;
		}
		set
		{
			SpecificDrugArray__BackingField = value;
		}
	}

	public DataDrug GetDrugForScenario()
	{
		return null;
	}

	public string GetScenarioStringForDrugName(string drugName)
	{
		return null;
	}
}
