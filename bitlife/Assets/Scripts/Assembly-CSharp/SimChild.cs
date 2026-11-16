using System;
using System.Collections;

[Serializable]
public class SimChild : SimPerson, IComparable
{
	private int RelationshipStatus__BackingField;

	private int ChildType__BackingField;

	private int Generation__BackingField;

	private int Twins__BackingField;

	private bool MovedOut__BackingField;

	private string FatherNameString__BackingField;

	private string MotherNameString__BackingField;

	private string LastNameAtBirth__BackingField;

	private SimPerson Spouse__BackingField;

	private int AgeAtCurrentSpouseMarriage__BackingField;

	private SimChildSupport ChildSupport__BackingField;

	private ArrayList _ExSpouseArray__BackingField;

	private ArrayList _GrandchildArray__BackingField;

	private int NumTimesHeroAskedToTakePaternityTest__BackingField;

	private int NumTimesHeroRejectedPaternityTest__BackingField;

	private int HeroAgeAtLastPaternityTest__BackingField;

	private int HeroAgeAtLastAbandoned__BackingField;

	private int HeroAgeAtLastAskedToStudyMore__BackingField;

	private int HeroAgeAtLastAskedToTakeInstrumentLessons__BackingField;

	private int HeroAgeAtLastAskedToDiet__BackingField;

	private int HeroAgeAtLastAskedToHitGym__BackingField;

	private int HeroAgeAtLastAskedToGetPartTimeJob__BackingField;

	private int HeroAgeAtLastAskedToGetBetterJob__BackingField;

	private int HeroAgeAtLastAgreedToGetBetterJob__BackingField;

	private int HeroAgeAtLastAskedToGetJob__BackingField;

	private int HeroAgeAtLastAskedToGetMarried__BackingField;

	private int HeroAgeAtLastAskedToGetDivorced__BackingField;

	private int _heroAgeAtLastSpendTimeRequest;

	private bool _lastSpendTimeRequestOutcome;

	private int _heroAgeAtLastReconnectRequest;

	private bool _lastReconnectRequestOutcome;

	private int SpecialTalent__BackingField;

	public int RelationshipStatus
	{
		get
		{
			return RelationshipStatus__BackingField;
		}
		set
		{
			RelationshipStatus__BackingField = value;
		}
	}

	public int ChildType
	{
		get
		{
			return ChildType__BackingField;
		}
		set
		{
			ChildType__BackingField = value;
		}
	}

	public int Generation
	{
		get
		{
			return Generation__BackingField;
		}
		set
		{
			Generation__BackingField = value;
		}
	}

	public int Twins
	{
		get
		{
			return Twins__BackingField;
		}
		set
		{
			Twins__BackingField = value;
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

	public string LastNameAtBirth
	{
		get
		{
			return LastNameAtBirth__BackingField;
		}
		set
		{
			LastNameAtBirth__BackingField = value;
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

	public SimChildSupport ChildSupport
	{
		get
		{
			return ChildSupport__BackingField;
		}
		set
		{
			ChildSupport__BackingField = value;
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

	private ArrayList _GrandchildArray
	{
		get
		{
			return _GrandchildArray__BackingField;
		}
		set
		{
			_GrandchildArray__BackingField = value;
		}
	}

	public ArrayList GrandchildArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int NumTimesHeroAskedToTakePaternityTest
	{
		get
		{
			return NumTimesHeroAskedToTakePaternityTest__BackingField;
		}
		set
		{
			NumTimesHeroAskedToTakePaternityTest__BackingField = value;
		}
	}

	public int NumTimesHeroRejectedPaternityTest
	{
		get
		{
			return NumTimesHeroRejectedPaternityTest__BackingField;
		}
		set
		{
			NumTimesHeroRejectedPaternityTest__BackingField = value;
		}
	}

	public int HeroAgeAtLastPaternityTest
	{
		get
		{
			return HeroAgeAtLastPaternityTest__BackingField;
		}
		set
		{
			HeroAgeAtLastPaternityTest__BackingField = value;
		}
	}

	public int HeroAgeAtLastAbandoned
	{
		get
		{
			return HeroAgeAtLastAbandoned__BackingField;
		}
		set
		{
			HeroAgeAtLastAbandoned__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskedToStudyMore
	{
		get
		{
			return HeroAgeAtLastAskedToStudyMore__BackingField;
		}
		set
		{
			HeroAgeAtLastAskedToStudyMore__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskedToTakeInstrumentLessons
	{
		get
		{
			return HeroAgeAtLastAskedToTakeInstrumentLessons__BackingField;
		}
		set
		{
			HeroAgeAtLastAskedToTakeInstrumentLessons__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskedToDiet
	{
		get
		{
			return HeroAgeAtLastAskedToDiet__BackingField;
		}
		set
		{
			HeroAgeAtLastAskedToDiet__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskedToHitGym
	{
		get
		{
			return HeroAgeAtLastAskedToHitGym__BackingField;
		}
		set
		{
			HeroAgeAtLastAskedToHitGym__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskedToGetPartTimeJob
	{
		get
		{
			return HeroAgeAtLastAskedToGetPartTimeJob__BackingField;
		}
		set
		{
			HeroAgeAtLastAskedToGetPartTimeJob__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskedToGetBetterJob
	{
		get
		{
			return HeroAgeAtLastAskedToGetBetterJob__BackingField;
		}
		set
		{
			HeroAgeAtLastAskedToGetBetterJob__BackingField = value;
		}
	}

	public int HeroAgeAtLastAgreedToGetBetterJob
	{
		get
		{
			return HeroAgeAtLastAgreedToGetBetterJob__BackingField;
		}
		set
		{
			HeroAgeAtLastAgreedToGetBetterJob__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskedToGetJob
	{
		get
		{
			return HeroAgeAtLastAskedToGetJob__BackingField;
		}
		set
		{
			HeroAgeAtLastAskedToGetJob__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskedToGetMarried
	{
		get
		{
			return HeroAgeAtLastAskedToGetMarried__BackingField;
		}
		set
		{
			HeroAgeAtLastAskedToGetMarried__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskedToGetDivorced
	{
		get
		{
			return HeroAgeAtLastAskedToGetDivorced__BackingField;
		}
		set
		{
			HeroAgeAtLastAskedToGetDivorced__BackingField = value;
		}
	}

	public int SpecialTalent
	{
		get
		{
			return SpecialTalent__BackingField;
		}
		set
		{
			SpecialTalent__BackingField = value;
		}
	}

	private SimChild()
	{
	}

	public SimChild(bool initHere)
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

	public void SetSkinColorWithParent1SkinColorAndParent2SkinColor(int parent1SkinColor, int parent2SkinColor)
	{
	}

	public bool IsLivingWithHero()
	{
		return false;
	}

	public bool IsChildWithCurrentLover()
	{
		return false;
	}

	public bool IsChildWithLover(SimLover aLover)
	{
		return false;
	}

	public bool WillAgreeToReconnect()
	{
		return false;
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

	public override bool ThirdPersonShouldObserveVaccination()
	{
		return false;
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

	public float GetChildSupportAmount(bool isHero)
	{
		return 0f;
	}
}
