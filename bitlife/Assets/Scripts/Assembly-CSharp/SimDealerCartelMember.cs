using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimDealerCartelMember : SimLover
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCrime, string> _003C_003E9__48_0;

		internal string _003CGetJailedCrimesString_003Eb__48_0(SimCrime t)
		{
			return null;
		}
	}

	private string Nickname__BackingField;

	private DEALER_RANK Rank__BackingField;

	private float Salary__BackingField;

	private int NumTimesTriedToGiveMoneyThisYear__BackingField;

	private int NumTimesTriedToPartyThisYear__BackingField;

	private string CartelName__BackingField;

	private Dictionary<string, float> _addictions;

	private ArrayList _jailedForCrimes;

	private int _jailSentenceLength;

	private int _jailTimeRemaining;

	private float _infamy;

	public string Nickname
	{
		get
		{
			return Nickname__BackingField;
		}
		set
		{
			Nickname__BackingField = value;
		}
	}

	public DEALER_RANK Rank
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

	public float Salary
	{
		get
		{
			return Salary__BackingField;
		}
		set
		{
			Salary__BackingField = value;
		}
	}

	public int NumTimesTriedToGiveMoneyThisYear
	{
		get
		{
			return NumTimesTriedToGiveMoneyThisYear__BackingField;
		}
		set
		{
			NumTimesTriedToGiveMoneyThisYear__BackingField = value;
		}
	}

	public int NumTimesTriedToPartyThisYear
	{
		get
		{
			return NumTimesTriedToPartyThisYear__BackingField;
		}
		set
		{
			NumTimesTriedToPartyThisYear__BackingField = value;
		}
	}

	public string CartelName
	{
		get
		{
			return CartelName__BackingField;
		}
		set
		{
			CartelName__BackingField = value;
		}
	}

	public SimDealerCartelMember(DEALER_RANK rank, int gender, SimDealerCartel cartel)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public float GetInfamy()
	{
		return 0f;
	}

	public void SetInfamy(float infamy)
	{
	}

	public void ImpactInfamy(int impactScore)
	{
	}

	public string GetGenderString(bool capitalize)
	{
		return null;
	}

	public void AddVice(string drugType, float addictionLevel)
	{
	}

	public string GetVicesListString()
	{
		return null;
	}

	public string RelationshipStatusString(bool murderList = false)
	{
		return null;
	}

	public float GetAverageAddictionLevel()
	{
		return 0f;
	}

	public bool IsAddict()
	{
		return false;
	}

	public KeyValuePair<string, float> GetGreatestVice()
	{
		return default;
	}

	public Dictionary<string, float> GetVices()
	{
		return null;
	}

	public void ImpactAddictionToDrug(string drugType, int impactScore)
	{
	}

	public float GetAddictionLevelForDrug(string drugType)
	{
		return 0f;
	}

	public void SentToJail(ArrayList crimes)
	{
	}

	public void FreedFromJail()
	{
	}

	public ArrayList GetJailedCrimes()
	{
		return null;
	}

	public bool Jailed()
	{
		return false;
	}

	public string GetJailedCrimesString()
	{
		return null;
	}

	public string GetSentenceCrimesString()
	{
		return null;
	}

	public int GetJailSentenceLength()
	{
		return 0;
	}

	public int GetJailTimeRemaining()
	{
		return 0;
	}

	private bool _003C_002Ector_003Eb__29_0(SimDealerCartelMember x)
	{
		return false;
	}
}
