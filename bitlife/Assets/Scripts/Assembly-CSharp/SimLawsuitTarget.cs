using System;

public class SimLawsuitTarget
{
	private int TargetType__BackingField;

	private string Name__BackingField;

	private string CountryCode__BackingField;

	[NonSerialized]
	public DataCountry _Country;

	private SimParent Parent__BackingField;

	private SimSibling Sibling__BackingField;

	private SimLover Lover__BackingField;

	private SimChild Child__BackingField;

	private SimPerson Friend__BackingField;

	private SimPerson Supervisor__BackingField;

	private SimPerson Surrogate__BackingField;

	private SimCostar Costar__BackingField;

	private SimTenant Tenant__BackingField;

	private SimAstronaut Astronaut__BackingField;

	private string Taxidermist__BackingField;

	private float DamagesBaseline__BackingField;

	private string ReasonString__BackingField;

	private int EmployerTerminationReason__BackingField;

	private bool RansomWasAnimal__BackingField;

	private int HeroAgeAtEligibility__BackingField;

	private bool Sued__BackingField;

	public int TargetType
	{
		get
		{
			return TargetType__BackingField;
		}
		set
		{
			TargetType__BackingField = value;
		}
	}

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

	public string CountryCode
	{
		get
		{
			return CountryCode__BackingField;
		}
		set
		{
			CountryCode__BackingField = value;
		}
	}

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

	public SimParent Parent
	{
		get
		{
			return Parent__BackingField;
		}
		set
		{
			Parent__BackingField = value;
		}
	}

	public SimSibling Sibling
	{
		get
		{
			return Sibling__BackingField;
		}
		set
		{
			Sibling__BackingField = value;
		}
	}

	public SimLover Lover
	{
		get
		{
			return Lover__BackingField;
		}
		set
		{
			Lover__BackingField = value;
		}
	}

	public SimChild Child
	{
		get
		{
			return Child__BackingField;
		}
		set
		{
			Child__BackingField = value;
		}
	}

	public SimPerson Friend
	{
		get
		{
			return Friend__BackingField;
		}
		set
		{
			Friend__BackingField = value;
		}
	}

	public SimPerson Supervisor
	{
		get
		{
			return Supervisor__BackingField;
		}
		set
		{
			Supervisor__BackingField = value;
		}
	}

	public SimPerson Surrogate
	{
		get
		{
			return Surrogate__BackingField;
		}
		set
		{
			Surrogate__BackingField = value;
		}
	}

	public SimCostar Costar
	{
		get
		{
			return Costar__BackingField;
		}
		set
		{
			Costar__BackingField = value;
		}
	}

	public SimTenant Tenant
	{
		get
		{
			return Tenant__BackingField;
		}
		set
		{
			Tenant__BackingField = value;
		}
	}

	public SimAstronaut Astronaut
	{
		get
		{
			return Astronaut__BackingField;
		}
		set
		{
			Astronaut__BackingField = value;
		}
	}

	public string Taxidermist
	{
		get
		{
			return Taxidermist__BackingField;
		}
		set
		{
			Taxidermist__BackingField = value;
		}
	}

	public float DamagesBaseline
	{
		get
		{
			return DamagesBaseline__BackingField;
		}
		set
		{
			DamagesBaseline__BackingField = value;
		}
	}

	public string ReasonString
	{
		get
		{
			return ReasonString__BackingField;
		}
		set
		{
			ReasonString__BackingField = value;
		}
	}

	public int EmployerTerminationReason
	{
		get
		{
			return EmployerTerminationReason__BackingField;
		}
		set
		{
			EmployerTerminationReason__BackingField = value;
		}
	}

	public bool RansomWasAnimal
	{
		get
		{
			return RansomWasAnimal__BackingField;
		}
		set
		{
			RansomWasAnimal__BackingField = value;
		}
	}

	public int HeroAgeAtEligibility
	{
		get
		{
			return HeroAgeAtEligibility__BackingField;
		}
		set
		{
			HeroAgeAtEligibility__BackingField = value;
		}
	}

	public bool Sued
	{
		get
		{
			return Sued__BackingField;
		}
		set
		{
			Sued__BackingField = value;
		}
	}

	private SimLawsuitTarget()
	{
	}

	public SimLawsuitTarget(int theTargetType, string theName, DataCountry theCountry, float theDamagesBaseline, string theReasonString)
	{
	}

	public string GetNameStringForListItem()
	{
		return null;
	}

	public string GetNameStringForScenario()
	{
		return null;
	}

	public string GetNameStringForLog()
	{
		return null;
	}

	public string GetReasonStringForScenario()
	{
		return null;
	}

	public string GetReasonStringForLog()
	{
		return null;
	}

	public bool IsIndividual()
	{
		return false;
	}

	public int GetOddsOfSuccessForDamagesTier(int tier)
	{
		return 0;
	}
}
