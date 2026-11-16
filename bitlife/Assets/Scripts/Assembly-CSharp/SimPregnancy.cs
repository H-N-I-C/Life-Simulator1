using System;
using System.Collections;

[Serializable]
public class SimPregnancy
{
	private int Type__BackingField;

	private string MotherName__BackingField;

	private string FatherName__BackingField;

	private string FatherLastName__BackingField;

	private bool GPOveride__BackingField;

	private int GPGender__BackingField;

	private int GPNumKids__BackingField;

	private SPECIAL_TALENT GPSpecialTalent__BackingField;

	[NonSerialized]
	public SimSurrogateSituation SurrogateSituation;

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

	public string MotherName
	{
		get
		{
			return MotherName__BackingField;
		}
		set
		{
			MotherName__BackingField = value;
		}
	}

	public string FatherName
	{
		get
		{
			return FatherName__BackingField;
		}
		set
		{
			FatherName__BackingField = value;
		}
	}

	public string FatherLastName
	{
		get
		{
			return FatherLastName__BackingField;
		}
		set
		{
			FatherLastName__BackingField = value;
		}
	}

	public bool GPOveride
	{
		get
		{
			return GPOveride__BackingField;
		}
		set
		{
			GPOveride__BackingField = value;
		}
	}

	public int GPGender
	{
		get
		{
			return GPGender__BackingField;
		}
		set
		{
			GPGender__BackingField = value;
		}
	}

	public int GPNumKids
	{
		get
		{
			return GPNumKids__BackingField;
		}
		set
		{
			GPNumKids__BackingField = value;
		}
	}

	public SPECIAL_TALENT GPSpecialTalent
	{
		get
		{
			return GPSpecialTalent__BackingField;
		}
		set
		{
			GPSpecialTalent__BackingField = value;
		}
	}

	public bool IsMotherHero()
	{
		return false;
	}

	public bool IsFatherHero()
	{
		return false;
	}

	public bool IsMotherCurrentLover()
	{
		return false;
	}

	public bool IsFatherCurrentLover()
	{
		return false;
	}

	public bool IsResultOfHeroAndCurrentLover()
	{
		return false;
	}

	public SimLover GetMotherLover()
	{
		return null;
	}

	public SimLover GetFatherLover()
	{
		return null;
	}

	public ArrayList GetParentsArray()
	{
		return null;
	}

	public static SimPregnancy GetResultingPregnancyForMotherAndFatherAndTypeAndAssignToMother(SimPerson mother, SimPerson father, int theType, bool assign)
	{
		return null;
	}

	public static bool IsEligibleForDeterministicConception(SimPerson mother, SimPerson father)
	{
		return false;
	}

	public static SimPregnancy GetResultingPregnancyForEligibleMotherAndFatherAndTypeAndAssignToMother(SimPerson mother, SimPerson father, int theType, bool assign)
	{
		return null;
	}

	private static bool HasBasePregnancyEligibility(SimPerson mother, SimPerson father)
	{
		return false;
	}

	private static float GetNaturalConceptionAgeProbability(int age)
	{
		return 0f;
	}

	private static SimPregnancy CreatePregnancyAndOptionallyAssign(SimPerson mother, SimPerson father, int theType, bool assign)
	{
		return null;
	}
}
