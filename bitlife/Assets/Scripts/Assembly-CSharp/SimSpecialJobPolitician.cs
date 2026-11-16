using System;
using System.Collections;

[Serializable]
public class SimSpecialJobPolitician : SimSpecialJob
{
	private int Office__BackingField;

	private string _countryCode;

	private string _PlaceString;

	[NonSerialized]
	public SimPlace _Place;

	private int YearsInOffice__BackingField;

	private int TermsInOffice__BackingField;

	private string _partyName;

	private int ElectionPlatformIssue__BackingField;

	private ArrayList IssueArray__BackingField;

	private ArrayList _HeadlineArray__BackingField;

	private int HeadlineTopIssue__BackingField;

	private int HeadlineTopIssueYearAgo1__BackingField;

	private int HeadlineTopIssueYearAgo2__BackingField;

	private int ConsecutiveYearsOfBadPerformance__BackingField;

	private int CurrentLawIndex__BackingField;

	private ArrayList _LawIndexHistoryArray__BackingField;

	private int AgeAtLastSpeech__BackingField;

	private int SpeechesAtCurrentAge__BackingField;

	private int AgeAtLastRally__BackingField;

	private float Att_approval__BackingField;

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

	public int YearsInOffice
	{
		get
		{
			return YearsInOffice__BackingField;
		}
		set
		{
			YearsInOffice__BackingField = value;
		}
	}

	public int TermsInOffice
	{
		get
		{
			return TermsInOffice__BackingField;
		}
		set
		{
			TermsInOffice__BackingField = value;
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

	public ArrayList IssueArray
	{
		get
		{
			return IssueArray__BackingField;
		}
		set
		{
			IssueArray__BackingField = value;
		}
	}

	private ArrayList _HeadlineArray
	{
		get
		{
			return _HeadlineArray__BackingField;
		}
		set
		{
			_HeadlineArray__BackingField = value;
		}
	}

	public ArrayList HeadlineArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int HeadlineTopIssue
	{
		get
		{
			return HeadlineTopIssue__BackingField;
		}
		set
		{
			HeadlineTopIssue__BackingField = value;
		}
	}

	public int HeadlineTopIssueYearAgo1
	{
		get
		{
			return HeadlineTopIssueYearAgo1__BackingField;
		}
		set
		{
			HeadlineTopIssueYearAgo1__BackingField = value;
		}
	}

	public int HeadlineTopIssueYearAgo2
	{
		get
		{
			return HeadlineTopIssueYearAgo2__BackingField;
		}
		set
		{
			HeadlineTopIssueYearAgo2__BackingField = value;
		}
	}

	public int ConsecutiveYearsOfBadPerformance
	{
		get
		{
			return ConsecutiveYearsOfBadPerformance__BackingField;
		}
		set
		{
			ConsecutiveYearsOfBadPerformance__BackingField = value;
		}
	}

	public int CurrentLawIndex
	{
		get
		{
			return CurrentLawIndex__BackingField;
		}
		set
		{
			CurrentLawIndex__BackingField = value;
		}
	}

	private ArrayList _LawIndexHistoryArray
	{
		get
		{
			return _LawIndexHistoryArray__BackingField;
		}
		set
		{
			_LawIndexHistoryArray__BackingField = value;
		}
	}

	public ArrayList LawIndexHistoryArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastSpeech
	{
		get
		{
			return AgeAtLastSpeech__BackingField;
		}
		set
		{
			AgeAtLastSpeech__BackingField = value;
		}
	}

	public int SpeechesAtCurrentAge
	{
		get
		{
			return SpeechesAtCurrentAge__BackingField;
		}
		set
		{
			SpeechesAtCurrentAge__BackingField = value;
		}
	}

	public int AgeAtLastRally
	{
		get
		{
			return AgeAtLastRally__BackingField;
		}
		set
		{
			AgeAtLastRally__BackingField = value;
		}
	}

	public float Att_approval
	{
		get
		{
			return Att_approval__BackingField;
		}
		set
		{
			Att_approval__BackingField = value;
		}
	}

	public SimSpecialJobPolitician()
	{
	}

	public SimSpecialJobPolitician(int theOffice, DataPoliticalParty theParty, int thePlatform, SimPlace thePlace)
	{
	}

	public override void IncrementAge()
	{
	}

	public override string JobTitleWithArticle(bool article)
	{
		return null;
	}

	public override bool ShouldBecomeFamous()
	{
		return false;
	}

	public override int GetMaxFame()
	{
		return 0;
	}

	public override int GetFameType()
	{
		return 0;
	}

	public void SetupInitialIssueArray()
	{
	}

	public int GetApprovalRating()
	{
		return 0;
	}

	public int GetYearsLeftInTerm()
	{
		return 0;
	}

	public void UpdateApprovalRatings()
	{
	}

	public void RefreshHeadlines()
	{
	}

	public void RefreshCurrentLaw()
	{
	}

	public string GetGenericHeadlineString()
	{
		return null;
	}

	public ArrayList GetIssueArraySortedAlphabetically()
	{
		return null;
	}

	public void ImpactApproval(int impactScore)
	{
	}
}
