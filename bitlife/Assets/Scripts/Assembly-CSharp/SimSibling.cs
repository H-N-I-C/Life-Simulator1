using System;
using System.Collections;

[Serializable]
public class SimSibling : SimPerson, IComparable
{
	private int _heroAgeAtLastSpendTimeRequest;

	private bool _lastSpendTimeRequestOutcome;

	private int SiblingType__BackingField;

	private bool IsOlderSibling__BackingField;

	private bool MovedOut__BackingField;

	private string MotherNameString__BackingField;

	private string FatherNameString__BackingField;

	private SimPerson Spouse__BackingField;

	private int AgeAtCurrentSpouseMarriage__BackingField;

	private ArrayList _ExSpouseArray__BackingField;

	private ArrayList _NieceNephewArray__BackingField;

	private float Att_petulance__BackingField;

	public int SiblingType
	{
		get
		{
			return SiblingType__BackingField;
		}
		set
		{
			SiblingType__BackingField = value;
		}
	}

	public bool IsOlderSibling
	{
		get
		{
			return IsOlderSibling__BackingField;
		}
		set
		{
			IsOlderSibling__BackingField = value;
		}
	}

	public bool MovedOut
	{
		get
		{
			return MovedOut__BackingField;
		}
		set
		{
			MovedOut__BackingField = value;
		}
	}

	public string MotherNameString
	{
		get
		{
			return MotherNameString__BackingField;
		}
		set
		{
			MotherNameString__BackingField = value;
		}
	}

	public string FatherNameString
	{
		get
		{
			return FatherNameString__BackingField;
		}
		set
		{
			FatherNameString__BackingField = value;
		}
	}

	public SimPerson Spouse
	{
		get
		{
			return Spouse__BackingField;
		}
		set
		{
			Spouse__BackingField = value;
		}
	}

	public int AgeAtCurrentSpouseMarriage
	{
		get
		{
			return AgeAtCurrentSpouseMarriage__BackingField;
		}
		set
		{
			AgeAtCurrentSpouseMarriage__BackingField = value;
		}
	}

	private ArrayList _ExSpouseArray
	{
		get
		{
			return _ExSpouseArray__BackingField;
		}
		set
		{
			_ExSpouseArray__BackingField = value;
		}
	}

	public ArrayList ExSpouseArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _NieceNephewArray
	{
		get
		{
			return _NieceNephewArray__BackingField;
		}
		set
		{
			_NieceNephewArray__BackingField = value;
		}
	}

	public ArrayList NieceNephewArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float Att_petulance
	{
		get
		{
			return Att_petulance__BackingField;
		}
		set
		{
			Att_petulance__BackingField = value;
		}
	}

	private SimSibling()
	{
	}

	public SimSibling(bool initHere)
	{
	}

	public override void IncrementAge()
	{
	}

	public override float AnnualCostOfLiving()
	{
		return 0f;
	}

	public override float AskForMoneyAmount()
	{
		return 0f;
	}

	public override bool WillAgreeToSpendTime()
	{
		return false;
	}

	public string RelationshipStatusStringAndIncludeBigLittle(bool bigLittle)
	{
		return null;
	}

	public void ImpactPetulance(int impactScore)
	{
	}

	public int CompareTo(object obj)
	{
		return 0;
	}

	public override string ThirdPersonLogIdentityStringWithStartOfYearName(SimPersonName cachedName)
	{
		return null;
	}

	public override float ThirdPersonSuccessPropensityScore()
	{
		return 0f;
	}

	public override bool ThirdPersonMovesOutAndShouldAnnounce()
	{
		return false;
	}

	public override void ThirdPersonMarries()
	{
	}

	public override void ThirdPersonChangesNameAfterMarriage()
	{
	}

	public override void ThirdPersonDivorces()
	{
	}

	public override void ThirdPersonChangesNameAfterDivorce()
	{
	}

	public override bool ThirdPersonTriesForBaby()
	{
		return false;
	}

	public override string ThirdPersonBabyBirthString()
	{
		return null;
	}

	public override string ThirdPersonSpouseIdentityStringWithDescription(bool includeDescription)
	{
		return null;
	}
}
