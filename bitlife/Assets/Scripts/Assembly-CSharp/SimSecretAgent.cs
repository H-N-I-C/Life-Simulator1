using System;
using System.Collections.Generic;

[Serializable]
public class SimSecretAgent : SimLover
{
	private string CodeName__BackingField;

	private float Potential__BackingField;

	private int Proficiency__BackingField;

	private float CommissionPercentage__BackingField;

	private SECRET_AGENT_STATUS Status__BackingField;

	private SECRET_AGENT_RANK Rank__BackingField;

	private SECRET_AGENT_SPECIALIZATION Specialization__BackingField;

	private string Profession__BackingField;

	private bool IsFamily__BackingField;

	private int KillCount__BackingField;

	private int NumTrainingAvailableThisYear__BackingField;

	private int YearsInService__BackingField;

	private bool IsDoubleAgent__BackingField;

	private List<string> MartialArtsTrainedMoves__BackingField;

	private int AgeLastUsedInMission__BackingField;

	public string CodeName
	{
		get
		{
			return CodeName__BackingField;
		}
		set
		{
			CodeName__BackingField = value;
		}
	}

	public float Potential
	{
		get
		{
			return Potential__BackingField;
		}
		set
		{
			Potential__BackingField = value;
		}
	}

	public int Proficiency
	{
		get
		{
			return Proficiency__BackingField;
		}
		set
		{
			Proficiency__BackingField = value;
		}
	}

	public float CommissionPercentage
	{
		get
		{
			return CommissionPercentage__BackingField;
		}
		set
		{
			CommissionPercentage__BackingField = value;
		}
	}

	public SECRET_AGENT_STATUS Status
	{
		get
		{
			return Status__BackingField;
		}
		set
		{
			Status__BackingField = value;
		}
	}

	public SECRET_AGENT_RANK Rank
	{
		get
		{
			return Rank__BackingField;
		}
		set
		{
			Rank__BackingField = value;
		}
	}

	public SECRET_AGENT_SPECIALIZATION Specialization
	{
		get
		{
			return Specialization__BackingField;
		}
		set
		{
			Specialization__BackingField = value;
		}
	}

	public string Profession
	{
		get
		{
			return Profession__BackingField;
		}
		set
		{
			Profession__BackingField = value;
		}
	}

	public bool IsFamily
	{
		get
		{
			return IsFamily__BackingField;
		}
		set
		{
			IsFamily__BackingField = value;
		}
	}

	public int KillCount
	{
		get
		{
			return KillCount__BackingField;
		}
		set
		{
			KillCount__BackingField = value;
		}
	}

	public int NumTrainingAvailableThisYear
	{
		get
		{
			return NumTrainingAvailableThisYear__BackingField;
		}
		set
		{
			NumTrainingAvailableThisYear__BackingField = value;
		}
	}

	public int YearsInService
	{
		get
		{
			return YearsInService__BackingField;
		}
		set
		{
			YearsInService__BackingField = value;
		}
	}

	public bool IsDoubleAgent
	{
		get
		{
			return IsDoubleAgent__BackingField;
		}
		set
		{
			IsDoubleAgent__BackingField = value;
		}
	}

	public List<string> MartialArtsTrainedMoves
	{
		get
		{
			return MartialArtsTrainedMoves__BackingField;
		}
		set
		{
			MartialArtsTrainedMoves__BackingField = value;
		}
	}

	public int AgeLastUsedInMission
	{
		get
		{
			return AgeLastUsedInMission__BackingField;
		}
		set
		{
			AgeLastUsedInMission__BackingField = value;
		}
	}

	public SimSecretAgent(bool initHere)
		: base(initHere: false)
	{
	}

	public SimSecretAgent(SimPerson otherPerson)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void Die()
	{
	}

	public int NumMissionsCompleted()
	{
		return 0;
	}

	public string SpecializationString()
	{
		return null;
	}

	public void ImpactProficiency(int impact, SECRET_AGENT_SPECIALIZATION specialization = SECRET_AGENT_SPECIALIZATION.NONE, bool includeBuffs = true, bool missionImpact = false)
	{
	}

	public new string RelationshipStatusString()
	{
		return null;
	}

	public string TitledCodeName()
	{
		return null;
	}

	public double MinimumAmountNeededToAgreeToAssassination()
	{
		return 0.0;
	}

	public string RankStringForRank(SECRET_AGENT_RANK rank)
	{
		return null;
	}

	public bool ShouldBePromoted()
	{
		return false;
	}

	public void Promote()
	{
	}

	private bool _003CNumMissionsCompleted_003Eb__64_0(SimSecretAgentMission x)
	{
		return false;
	}
}
