using System;

[Serializable]
public class SimPoliticalCampaign
{
	private int Office__BackingField;

	private string _countryCode;

	private string _PlaceString;

	[NonSerialized]
	public SimPlace _Place;

	private bool IsDirectReelection__BackingField;

	private int NumPreviousTerms__BackingField;

	private double Budget__BackingField;

	private string _partyName;

	private int ElectionPlatformIssue__BackingField;

	private SimPerson Opponent__BackingField;

	private string _opponentPartyName;

	private int CumIssueScore__BackingField;

	private float Att_campaign__BackingField;

	public int Office
	{
		get
		{
			return Office__BackingField;
		}
		set
		{
			Office__BackingField = value;
		}
	}

	public DataCountryPoliticalSystem System
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimPlace Place
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool IsDirectReelection
	{
		get
		{
			return IsDirectReelection__BackingField;
		}
		set
		{
			IsDirectReelection__BackingField = value;
		}
	}

	public int NumPreviousTerms
	{
		get
		{
			return NumPreviousTerms__BackingField;
		}
		set
		{
			NumPreviousTerms__BackingField = value;
		}
	}

	public double Budget
	{
		get
		{
			return Budget__BackingField;
		}
		set
		{
			Budget__BackingField = value;
		}
	}

	public DataPoliticalParty PoliticalParty
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int ElectionPlatformIssue
	{
		get
		{
			return ElectionPlatformIssue__BackingField;
		}
		set
		{
			ElectionPlatformIssue__BackingField = value;
		}
	}

	public SimPerson Opponent
	{
		get
		{
			return Opponent__BackingField;
		}
		set
		{
			Opponent__BackingField = value;
		}
	}

	public DataPoliticalParty OpponentPoliticalParty
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int CumIssueScore
	{
		get
		{
			return CumIssueScore__BackingField;
		}
		set
		{
			CumIssueScore__BackingField = value;
		}
	}

	public float Att_campaign
	{
		get
		{
			return Att_campaign__BackingField;
		}
		set
		{
			Att_campaign__BackingField = value;
		}
	}

	public SimPoliticalCampaign(int theOffice, DataPoliticalParty theParty, int thePlatform, double theBudget, bool theIsDirectReelection, int theNumPreviousTerms, SimPlace thePlace)
	{
	}

	public void SetupCampaignAttribute()
	{
	}

	public void InheritCampaignAttributeFromPreviousApprovalRating(float previousApproval)
	{
	}

	public bool WillMakeItOntoBallot()
	{
		return false;
	}

	public int ElectionPercentagePointDelta()
	{
		return 0;
	}

	public int GetOpponentCampaignAttribute()
	{
		return 0;
	}

	public int GetCareerScore()
	{
		return 0;
	}

	public int GetExperienceScore()
	{
		return 0;
	}

	public int GetAttributesScore()
	{
		return 0;
	}

	public int GetEducationScore()
	{
		return 0;
	}

	public int GetMoneyScore()
	{
		return 0;
	}

	public void ImpactCampaign(int impactScore)
	{
	}
}
