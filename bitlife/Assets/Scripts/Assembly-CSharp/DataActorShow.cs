public class DataActorShow
{
	private string Title__BackingField;

	private int ShowGenre__BackingField;

	private bool CanBeEnsemble__BackingField;

	private int MainstreamScore__BackingField;

	private string LeadName__BackingField;

	private int LeadGender__BackingField;

	private int LeadMinAge__BackingField;

	private int LeadMaxAge__BackingField;

	private string Regular1Name__BackingField;

	private int Regular1Gender__BackingField;

	private int Regular1MinAge__BackingField;

	private int Regular1MaxAge__BackingField;

	private string Regular2Name__BackingField;

	private int Regular2Gender__BackingField;

	private int Regular2MinAge__BackingField;

	private int Regular2MaxAge__BackingField;

	private string Regular3Name__BackingField;

	private int Regular3Gender__BackingField;

	private int Regular3MinAge__BackingField;

	private int Regular3MaxAge__BackingField;

	private string RecurringName__BackingField;

	private int RecurringGender__BackingField;

	private int RecurringMinAge__BackingField;

	private int RecurringMaxAge__BackingField;

	public string Title
	{
		get
		{
			return Title__BackingField;
		}
		set
		{
			Title__BackingField = value;
		}
	}

	public int ShowGenre
	{
		get
		{
			return ShowGenre__BackingField;
		}
		set
		{
			ShowGenre__BackingField = value;
		}
	}

	public bool CanBeEnsemble
	{
		get
		{
			return CanBeEnsemble__BackingField;
		}
		set
		{
			CanBeEnsemble__BackingField = value;
		}
	}

	public int MainstreamScore
	{
		get
		{
			return MainstreamScore__BackingField;
		}
		set
		{
			MainstreamScore__BackingField = value;
		}
	}

	public string LeadName
	{
		get
		{
			return LeadName__BackingField;
		}
		set
		{
			LeadName__BackingField = value;
		}
	}

	public int LeadGender
	{
		get
		{
			return LeadGender__BackingField;
		}
		set
		{
			LeadGender__BackingField = value;
		}
	}

	public int LeadMinAge
	{
		get
		{
			return LeadMinAge__BackingField;
		}
		set
		{
			LeadMinAge__BackingField = value;
		}
	}

	public int LeadMaxAge
	{
		get
		{
			return LeadMaxAge__BackingField;
		}
		set
		{
			LeadMaxAge__BackingField = value;
		}
	}

	public string Regular1Name
	{
		get
		{
			return Regular1Name__BackingField;
		}
		set
		{
			Regular1Name__BackingField = value;
		}
	}

	public int Regular1Gender
	{
		get
		{
			return Regular1Gender__BackingField;
		}
		set
		{
			Regular1Gender__BackingField = value;
		}
	}

	public int Regular1MinAge
	{
		get
		{
			return Regular1MinAge__BackingField;
		}
		set
		{
			Regular1MinAge__BackingField = value;
		}
	}

	public int Regular1MaxAge
	{
		get
		{
			return Regular1MaxAge__BackingField;
		}
		set
		{
			Regular1MaxAge__BackingField = value;
		}
	}

	public string Regular2Name
	{
		get
		{
			return Regular2Name__BackingField;
		}
		set
		{
			Regular2Name__BackingField = value;
		}
	}

	public int Regular2Gender
	{
		get
		{
			return Regular2Gender__BackingField;
		}
		set
		{
			Regular2Gender__BackingField = value;
		}
	}

	public int Regular2MinAge
	{
		get
		{
			return Regular2MinAge__BackingField;
		}
		set
		{
			Regular2MinAge__BackingField = value;
		}
	}

	public int Regular2MaxAge
	{
		get
		{
			return Regular2MaxAge__BackingField;
		}
		set
		{
			Regular2MaxAge__BackingField = value;
		}
	}

	public string Regular3Name
	{
		get
		{
			return Regular3Name__BackingField;
		}
		set
		{
			Regular3Name__BackingField = value;
		}
	}

	public int Regular3Gender
	{
		get
		{
			return Regular3Gender__BackingField;
		}
		set
		{
			Regular3Gender__BackingField = value;
		}
	}

	public int Regular3MinAge
	{
		get
		{
			return Regular3MinAge__BackingField;
		}
		set
		{
			Regular3MinAge__BackingField = value;
		}
	}

	public int Regular3MaxAge
	{
		get
		{
			return Regular3MaxAge__BackingField;
		}
		set
		{
			Regular3MaxAge__BackingField = value;
		}
	}

	public string RecurringName
	{
		get
		{
			return RecurringName__BackingField;
		}
		set
		{
			RecurringName__BackingField = value;
		}
	}

	public int RecurringGender
	{
		get
		{
			return RecurringGender__BackingField;
		}
		set
		{
			RecurringGender__BackingField = value;
		}
	}

	public int RecurringMinAge
	{
		get
		{
			return RecurringMinAge__BackingField;
		}
		set
		{
			RecurringMinAge__BackingField = value;
		}
	}

	public int RecurringMaxAge
	{
		get
		{
			return RecurringMaxAge__BackingField;
		}
		set
		{
			RecurringMaxAge__BackingField = value;
		}
	}

	public SimActorShowRole GetARoleForType(int type, bool isEnsemble)
	{
		return null;
	}

	public string GetGenreString()
	{
		return null;
	}

	public bool IsAdult()
	{
		return false;
	}
}
