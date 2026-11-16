using System;
using System.Collections;

[Serializable]
public class SimParent : SimPerson
{
	private int _heroAgeAtLastMoneyRequest;

	private int _heroAgeAtLastSpendTimeRequest;

	private bool _lastSpendTimeRequestOutcome;

	private bool Unknown__BackingField;

	private int ParentType__BackingField;

	private bool IsInNonMaritalRelationshipWithHerosFather__BackingField;

	private bool IsMarriedToHerosFather__BackingField;

	private SimParent Spouse__BackingField;

	private ArrayList _ExSpouseArray__BackingField;

	private int NumStepchildren__BackingField;

	private bool WasPreviouslyHero__BackingField;

	private float Att_religiousness__BackingField;

	public bool Unknown
	{
		get
		{
			return Unknown__BackingField;
		}
		set
		{
			Unknown__BackingField = value;
		}
	}

	public int ParentType
	{
		get
		{
			return ParentType__BackingField;
		}
		set
		{
			ParentType__BackingField = value;
		}
	}

	public bool IsInNonMaritalRelationshipWithHerosFather
	{
		get
		{
			return IsInNonMaritalRelationshipWithHerosFather__BackingField;
		}
		set
		{
			IsInNonMaritalRelationshipWithHerosFather__BackingField = value;
		}
	}

	public bool IsMarriedToHerosFather
	{
		get
		{
			return IsMarriedToHerosFather__BackingField;
		}
		set
		{
			IsMarriedToHerosFather__BackingField = value;
		}
	}

	public SimParent Spouse
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

	public int NumStepchildren
	{
		get
		{
			return NumStepchildren__BackingField;
		}
		set
		{
			NumStepchildren__BackingField = value;
		}
	}

	public bool WasPreviouslyHero
	{
		get
		{
			return WasPreviouslyHero__BackingField;
		}
		set
		{
			WasPreviouslyHero__BackingField = value;
		}
	}

	public float Att_religiousness
	{
		get
		{
			return Att_religiousness__BackingField;
		}
		set
		{
			Att_religiousness__BackingField = value;
		}
	}

	public SimParent(SimPerson hero, int fatherType, int fatherGender, DataBirthCircumstance birthCircumstance)
	{
	}

	public SimParent(SimPerson hero, SimParent heroFather, int fatherType, int motherGender, DataBirthCircumstance birthCircumstance)
	{
	}

	public SimParent(bool initAsStepParentHere)
	{
	}

	public SimParent()
	{
	}

	public void SetupMoneyAttribute()
	{
	}

	public void InitEverythingElseAsParentOfHero(SimPerson hero)
	{
	}

	public void SetSkinColorWithHeroSkinColor(int heroSkinColor)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void Die()
	{
	}

	public override float AnnualCostOfLiving()
	{
		return 0f;
	}

	private float GetPrivateSchoolCost()
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

	public override bool IsNearbyAndCanDie()
	{
		return false;
	}

	public string RelationshipStatusString()
	{
		return null;
	}

	public override string GetRelationshipString()
	{
		return null;
	}

	public float WillingnessToFundSchoolScore()
	{
		return 0f;
	}

	public bool IsStepparent()
	{
		return false;
	}

	public bool IsCruelStepparent()
	{
		return false;
	}

	public int CurrentLoverDisapprovalReason()
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

	public override void ThirdPersonComesOutOfTheCloset()
	{
	}

	public override bool ThirdPersonIsEligibleToComeOutOfTheCloset()
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

	public override void ThirdPersonBreaksUp()
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
