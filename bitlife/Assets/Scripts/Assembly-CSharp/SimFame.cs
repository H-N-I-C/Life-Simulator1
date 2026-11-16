using System;
using System.Collections;

[Serializable]
public class SimFame
{
	private int FameType__BackingField;

	private string CountryCode__BackingField;

	private int YearsFamous__BackingField;

	private ArrayList _CommercialArray__BackingField;

	private ArrayList _MagazineArray__BackingField;

	private ArrayList _BookArray__BackingField;

	private ArrayList _TalkShowArray__BackingField;

	public int FameType
	{
		get
		{
			return FameType__BackingField;
		}
		set
		{
			FameType__BackingField = value;
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

	public int YearsFamous
	{
		get
		{
			return YearsFamous__BackingField;
		}
		set
		{
			YearsFamous__BackingField = value;
		}
	}

	private ArrayList _CommercialArray
	{
		get
		{
			return _CommercialArray__BackingField;
		}
		set
		{
			_CommercialArray__BackingField = value;
		}
	}

	public ArrayList CommercialArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _MagazineArray
	{
		get
		{
			return _MagazineArray__BackingField;
		}
		set
		{
			_MagazineArray__BackingField = value;
		}
	}

	public ArrayList MagazineArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _BookArray
	{
		get
		{
			return _BookArray__BackingField;
		}
		set
		{
			_BookArray__BackingField = value;
		}
	}

	public ArrayList BookArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _TalkShowArray
	{
		get
		{
			return _TalkShowArray__BackingField;
		}
		set
		{
			_TalkShowArray__BackingField = value;
		}
	}

	public ArrayList TalkShowArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private SimFame()
	{
	}

	public SimFame(int theFameType)
	{
	}

	public void SetupInitialFame()
	{
	}

	public void IncrementYearsFamous()
	{
	}

	public string FameTitle()
	{
		return null;
	}

	public string DescriptiveFameTitle()
	{
		return null;
	}

	public bool IsFameBasedOnCurrentCareer()
	{
		return false;
	}

	public static int GetFameTypeForCareer(DataCareer career)
	{
		return 0;
	}

	public static bool ShouldShowFameTitleAheadOfOccupationString()
	{
		return false;
	}

	public static bool AreHeroAttributesEligibleForFameViaCareer(DataCareer career)
	{
		return false;
	}
}
