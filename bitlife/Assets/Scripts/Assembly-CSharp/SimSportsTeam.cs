using System;
using UnityEngine;

[Serializable]
public class SimSportsTeam : IComparable
{
	private string _Name__BackingField;

	[NonSerialized]
	public DataSportsTeam _Data;

	private int TeamIndex__BackingField;

	private SimSportsTeamRecord Record__BackingField;

	private int StrengthScore__BackingField;

	private int AgeAtLastContractOffer__BackingField;

	private int AgeAtLastContractNegotiation__BackingField;

	private int AgeAtLastContractNegotiationMax__BackingField;

	private int AgeAtLastRejectedHero__BackingField;

	private int AgeAtLastAttemptToJoin__BackingField;

	private int AgeAtLastAttemptToLeave__BackingField;

	private int AgeAtLastLeftTeam__BackingField;

	private string _Name
	{
		get
		{
			return _Name__BackingField;
		}
		set
		{
			_Name__BackingField = value;
		}
	}

	public DataSportsTeam Data
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int TeamIndex
	{
		get
		{
			return TeamIndex__BackingField;
		}
		set
		{
			TeamIndex__BackingField = value;
		}
	}

	public SimSportsTeamRecord Record
	{
		get
		{
			return Record__BackingField;
		}
		set
		{
			Record__BackingField = value;
		}
	}

	public int StrengthScore
	{
		get
		{
			return StrengthScore__BackingField;
		}
		set
		{
			StrengthScore__BackingField = value;
		}
	}

	public int AgeAtLastContractOffer
	{
		get
		{
			return AgeAtLastContractOffer__BackingField;
		}
		set
		{
			AgeAtLastContractOffer__BackingField = value;
		}
	}

	public int AgeAtLastContractNegotiation
	{
		get
		{
			return AgeAtLastContractNegotiation__BackingField;
		}
		set
		{
			AgeAtLastContractNegotiation__BackingField = value;
		}
	}

	public int AgeAtLastContractNegotiationMax
	{
		get
		{
			return AgeAtLastContractNegotiationMax__BackingField;
		}
		set
		{
			AgeAtLastContractNegotiationMax__BackingField = value;
		}
	}

	public int AgeAtLastRejectedHero
	{
		get
		{
			return AgeAtLastRejectedHero__BackingField;
		}
		set
		{
			AgeAtLastRejectedHero__BackingField = value;
		}
	}

	public int AgeAtLastAttemptToJoin
	{
		get
		{
			return AgeAtLastAttemptToJoin__BackingField;
		}
		set
		{
			AgeAtLastAttemptToJoin__BackingField = value;
		}
	}

	public int AgeAtLastAttemptToLeave
	{
		get
		{
			return AgeAtLastAttemptToLeave__BackingField;
		}
		set
		{
			AgeAtLastAttemptToLeave__BackingField = value;
		}
	}

	public int AgeAtLastLeftTeam
	{
		get
		{
			return AgeAtLastLeftTeam__BackingField;
		}
		set
		{
			AgeAtLastLeftTeam__BackingField = value;
		}
	}

	public SimSportsTeam(DataSportsTeam theData)
	{
	}

	public string Description()
	{
		return null;
	}

	public string Name()
	{
		return null;
	}

	public string FullName()
	{
		return null;
	}

	public string SportName()
	{
		return null;
	}

	public string LeagueName()
	{
		return null;
	}

	public int GetPrimaryColorEnum()
	{
		return 0;
	}

	public int GetSecondaryColorEnum()
	{
		return 0;
	}

	public Color32 GetPrimaryColor()
	{
		return default;
	}

	public Color32 GetSecondaryColor()
	{
		return default;
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}
}
