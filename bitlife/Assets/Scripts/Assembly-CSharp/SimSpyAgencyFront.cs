using System;

[Serializable]
public class SimSpyAgencyFront
{
	private float Price__BackingField;

	private string Address__BackingField;

	private int AgentCapacity__BackingField;

	private int SecurityCapacity__BackingField;

	private float Location__BackingField;

	private float Age__BackingField;

	private SimLoan Mortgage__BackingField;

	private string BusinessName__BackingField;

	private string FrontColor__BackingField;

	private SECRET_AGENT_AGENCY_FRONT_TYPE FrontType__BackingField;

	public float Price
	{
		get
		{
			return Price__BackingField;
		}
		set
		{
			Price__BackingField = value;
		}
	}

	public string Address
	{
		get
		{
			return Address__BackingField;
		}
		set
		{
			Address__BackingField = value;
		}
	}

	public int AgentCapacity
	{
		get
		{
			return AgentCapacity__BackingField;
		}
		set
		{
			AgentCapacity__BackingField = value;
		}
	}

	public int SecurityCapacity
	{
		get
		{
			return SecurityCapacity__BackingField;
		}
		set
		{
			SecurityCapacity__BackingField = value;
		}
	}

	public float Location
	{
		get
		{
			return Location__BackingField;
		}
		set
		{
			Location__BackingField = value;
		}
	}

	public float Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public SimLoan Mortgage
	{
		get
		{
			return Mortgage__BackingField;
		}
		set
		{
			Mortgage__BackingField = value;
		}
	}

	public string BusinessName
	{
		get
		{
			return BusinessName__BackingField;
		}
		set
		{
			BusinessName__BackingField = value;
		}
	}

	public string FrontColor
	{
		get
		{
			return FrontColor__BackingField;
		}
		set
		{
			FrontColor__BackingField = value;
		}
	}

	public SECRET_AGENT_AGENCY_FRONT_TYPE FrontType
	{
		get
		{
			return FrontType__BackingField;
		}
		set
		{
			FrontType__BackingField = value;
		}
	}

	public SimSpyAgencyFront(bool isStarter)
	{
	}

	public void IncrementAge()
	{
	}

	public float Cover()
	{
		return 0f;
	}

	public void OriginateMortgage()
	{
	}

	public int MortgageRejectionReason()
	{
		return 0;
	}

	public void SellFront()
	{
	}

	public double GetMonthlyExpense()
	{
		return 0.0;
	}

	public double GetMonthlyMortgageExpense()
	{
		return 0.0;
	}

	public string GetSecretAgentAgencyFrontTypeString()
	{
		return null;
	}

	public void ImpactLocation(int impact)
	{
	}
}
