using System.Collections;

public class DataBirthCircumstance
{
	private string Circumstance__BackingField;

	private string SiblingCircumstance__BackingField;

	private string StepfatherOverrideVersion__BackingField;

	private ArrayList FatherTypeArray__BackingField;

	private int CommonnessScore__BackingField;

	private int MinMotherAge__BackingField;

	private int MaxMotherAge__BackingField;

	private bool Adoption__BackingField;

	private bool Surrogacy__BackingField;

	private bool SpermDonor__BackingField;

	private bool Ivf__BackingField;

	private bool Royal__BackingField;

	private bool GayOnly__BackingField;

	private bool GayMaleParent__BackingField;

	private bool GayFemaleParent__BackingField;

	private int MinLooks__BackingField;

	private int MaxLooks__BackingField;

	private int MinSmarts__BackingField;

	private int MaxSmarts__BackingField;

	private bool Clean__BackingField;

	public string Circumstance
	{
		get
		{
			return Circumstance__BackingField;
		}
		set
		{
			Circumstance__BackingField = value;
		}
	}

	public string SiblingCircumstance
	{
		get
		{
			return SiblingCircumstance__BackingField;
		}
		set
		{
			SiblingCircumstance__BackingField = value;
		}
	}

	public string StepfatherOverrideVersion
	{
		get
		{
			return StepfatherOverrideVersion__BackingField;
		}
		set
		{
			StepfatherOverrideVersion__BackingField = value;
		}
	}

	public ArrayList FatherTypeArray
	{
		get
		{
			return FatherTypeArray__BackingField;
		}
		set
		{
			FatherTypeArray__BackingField = value;
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

	public int MinMotherAge
	{
		get
		{
			return MinMotherAge__BackingField;
		}
		set
		{
			MinMotherAge__BackingField = value;
		}
	}

	public int MaxMotherAge
	{
		get
		{
			return MaxMotherAge__BackingField;
		}
		set
		{
			MaxMotherAge__BackingField = value;
		}
	}

	public bool Adoption
	{
		get
		{
			return Adoption__BackingField;
		}
		set
		{
			Adoption__BackingField = value;
		}
	}

	public bool Surrogacy
	{
		get
		{
			return Surrogacy__BackingField;
		}
		set
		{
			Surrogacy__BackingField = value;
		}
	}

	public bool SpermDonor
	{
		get
		{
			return SpermDonor__BackingField;
		}
		set
		{
			SpermDonor__BackingField = value;
		}
	}

	public bool Ivf
	{
		get
		{
			return Ivf__BackingField;
		}
		set
		{
			Ivf__BackingField = value;
		}
	}

	public bool Royal
	{
		get
		{
			return Royal__BackingField;
		}
		set
		{
			Royal__BackingField = value;
		}
	}

	public bool GayOnly
	{
		get
		{
			return GayOnly__BackingField;
		}
		set
		{
			GayOnly__BackingField = value;
		}
	}

	public bool GayMaleParent
	{
		get
		{
			return GayMaleParent__BackingField;
		}
		set
		{
			GayMaleParent__BackingField = value;
		}
	}

	public bool GayFemaleParent
	{
		get
		{
			return GayFemaleParent__BackingField;
		}
		set
		{
			GayFemaleParent__BackingField = value;
		}
	}

	public int MinLooks
	{
		get
		{
			return MinLooks__BackingField;
		}
		set
		{
			MinLooks__BackingField = value;
		}
	}

	public int MaxLooks
	{
		get
		{
			return MaxLooks__BackingField;
		}
		set
		{
			MaxLooks__BackingField = value;
		}
	}

	public int MinSmarts
	{
		get
		{
			return MinSmarts__BackingField;
		}
		set
		{
			MinSmarts__BackingField = value;
		}
	}

	public int MaxSmarts
	{
		get
		{
			return MaxSmarts__BackingField;
		}
		set
		{
			MaxSmarts__BackingField = value;
		}
	}

	public bool Clean
	{
		get
		{
			return Clean__BackingField;
		}
		set
		{
			Clean__BackingField = value;
		}
	}

	public int GetFatherType()
	{
		return 0;
	}

	public bool IncludesFatherType(int fatherType)
	{
		return false;
	}

	public string GetCircumstanceString()
	{
		return null;
	}

	public bool IncludesFathersGenes()
	{
		return false;
	}

	public bool IncludesMothersGenes()
	{
		return false;
	}
}
