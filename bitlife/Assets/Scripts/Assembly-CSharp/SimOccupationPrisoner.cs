using System;
using System.Collections;

[Serializable]
public class SimOccupationPrisoner : SimOccupation
{
	public SimPrison _Prison;

	private string CountryCode__BackingField;

	[NonSerialized]
	public DataCountry _Country;

	private int YearsCompleted__BackingField;

	private int YearsLeft__BackingField;

	private bool OnDeathRow__BackingField;

	private string DeathPenaltyMethod__BackingField;

	private int PrisonerDischargeType__BackingField;

	private bool Appealed__BackingField;

	private float AppealCost__BackingField;

	private int AgeAtLastEscapeAttempt__BackingField;

	private int AgeAtLastEscape__BackingField;

	private int AgeAtLastRiotAttempt__BackingField;

	private int AgeAtLastSentLetter__BackingField;

	private int AgeAtLastConjugalVisit__BackingField;

	private int AgeAtLastConjugalVisitRequest__BackingField;

	private int AgeAtLastBribeAttempt__BackingField;

	private int AgeAtLastInfirmaryVisit__BackingField;

	private int AgeAtLastInfirmaryVisitRequest__BackingField;

	private int AgeAtLastCry__BackingField;

	private ArrayList _CrimeArray__BackingField;

	private int RiotsStarted__BackingField;

	private int ConjugalVisitsRequested__BackingField;

	private float Att_behavior__BackingField;

	private float Att_respect__BackingField;

	private SimSpecialJobDealer Dealer__BackingField;

	public SimPrison Prison
	{
		get
		{
			return null;
		}
		set
		{
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

	public int YearsCompleted
	{
		get
		{
			return YearsCompleted__BackingField;
		}
		set
		{
			YearsCompleted__BackingField = value;
		}
	}

	public int YearsLeft
	{
		get
		{
			return YearsLeft__BackingField;
		}
		set
		{
			YearsLeft__BackingField = value;
		}
	}

	public bool OnDeathRow
	{
		get
		{
			return OnDeathRow__BackingField;
		}
		set
		{
			OnDeathRow__BackingField = value;
		}
	}

	public string DeathPenaltyMethod
	{
		get
		{
			return DeathPenaltyMethod__BackingField;
		}
		set
		{
			DeathPenaltyMethod__BackingField = value;
		}
	}

	public int PrisonerDischargeType
	{
		get
		{
			return PrisonerDischargeType__BackingField;
		}
		set
		{
			PrisonerDischargeType__BackingField = value;
		}
	}

	public bool Appealed
	{
		get
		{
			return Appealed__BackingField;
		}
		set
		{
			Appealed__BackingField = value;
		}
	}

	public float AppealCost
	{
		get
		{
			return AppealCost__BackingField;
		}
		set
		{
			AppealCost__BackingField = value;
		}
	}

	public int AgeAtLastEscapeAttempt
	{
		get
		{
			return AgeAtLastEscapeAttempt__BackingField;
		}
		set
		{
			AgeAtLastEscapeAttempt__BackingField = value;
		}
	}

	public int AgeAtLastEscape
	{
		get
		{
			return AgeAtLastEscape__BackingField;
		}
		set
		{
			AgeAtLastEscape__BackingField = value;
		}
	}

	public int AgeAtLastRiotAttempt
	{
		get
		{
			return AgeAtLastRiotAttempt__BackingField;
		}
		set
		{
			AgeAtLastRiotAttempt__BackingField = value;
		}
	}

	public int AgeAtLastSentLetter
	{
		get
		{
			return AgeAtLastSentLetter__BackingField;
		}
		set
		{
			AgeAtLastSentLetter__BackingField = value;
		}
	}

	public int AgeAtLastConjugalVisit
	{
		get
		{
			return AgeAtLastConjugalVisit__BackingField;
		}
		set
		{
			AgeAtLastConjugalVisit__BackingField = value;
		}
	}

	public int AgeAtLastConjugalVisitRequest
	{
		get
		{
			return AgeAtLastConjugalVisitRequest__BackingField;
		}
		set
		{
			AgeAtLastConjugalVisitRequest__BackingField = value;
		}
	}

	public int AgeAtLastBribeAttempt
	{
		get
		{
			return AgeAtLastBribeAttempt__BackingField;
		}
		set
		{
			AgeAtLastBribeAttempt__BackingField = value;
		}
	}

	public int AgeAtLastInfirmaryVisit
	{
		get
		{
			return AgeAtLastInfirmaryVisit__BackingField;
		}
		set
		{
			AgeAtLastInfirmaryVisit__BackingField = value;
		}
	}

	public int AgeAtLastInfirmaryVisitRequest
	{
		get
		{
			return AgeAtLastInfirmaryVisitRequest__BackingField;
		}
		set
		{
			AgeAtLastInfirmaryVisitRequest__BackingField = value;
		}
	}

	public int AgeAtLastCry
	{
		get
		{
			return AgeAtLastCry__BackingField;
		}
		set
		{
			AgeAtLastCry__BackingField = value;
		}
	}

	private ArrayList _CrimeArray
	{
		get
		{
			return _CrimeArray__BackingField;
		}
		set
		{
			_CrimeArray__BackingField = value;
		}
	}

	public ArrayList CrimeArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int RiotsStarted
	{
		get
		{
			return RiotsStarted__BackingField;
		}
		set
		{
			RiotsStarted__BackingField = value;
		}
	}

	public int ConjugalVisitsRequested
	{
		get
		{
			return ConjugalVisitsRequested__BackingField;
		}
		set
		{
			ConjugalVisitsRequested__BackingField = value;
		}
	}

	public float Att_behavior
	{
		get
		{
			return Att_behavior__BackingField;
		}
		set
		{
			Att_behavior__BackingField = value;
		}
	}

	public float Att_respect
	{
		get
		{
			return Att_respect__BackingField;
		}
		set
		{
			Att_respect__BackingField = value;
		}
	}

	public SimSpecialJobDealer Dealer
	{
		get
		{
			return Dealer__BackingField;
		}
		set
		{
			Dealer__BackingField = value;
		}
	}

	private SimOccupationPrisoner()
	{
	}

	public SimOccupationPrisoner(ArrayList theCrimes, int years, SimPrison thePrison, DataCountry theCountry)
	{
	}

	public void SentenceToDeathPenaltyViaMethod(string method)
	{
	}

	public void BackwardsCompatibilityDelayedPrisonInit()
	{
	}

	public void IncrementYears()
	{
	}

	public int NextSecurityLevelForPrisonTransfer()
	{
		return 0;
	}

	public bool IsLifeSentence()
	{
		return false;
	}

	public bool IsHeroEligibleToJoinGang(DataPrisonGang gang)
	{
		return false;
	}

	public void JoinGang(DataPrisonGang gangData)
	{
	}

	public void LeaveCurrentGang()
	{
	}

	public void ImpactBehavior(int impactScore)
	{
	}

	public void ImpactRespect(int impactScore)
	{
	}
}
