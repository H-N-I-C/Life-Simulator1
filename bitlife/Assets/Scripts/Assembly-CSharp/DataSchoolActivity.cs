using System;
using System.Collections.Generic;

public class DataSchoolActivity : IComparable
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__128_0;

		internal string _003CAvailableInCountry_003Eb__128_0(string p)
		{
			return null;
		}
	}

	private int Index__BackingField;

	private string Name__BackingField;

	private bool DefiniteArticle__BackingField;

	private bool ProperNoun__BackingField;

	private string ReferenceNoun__BackingField;

	private int Type__BackingField;

	private string LeaderTitle__BackingField;

	private List<string> OtherOfficersArray__BackingField;

	private bool TrackPerformance__BackingField;

	private int Gender__BackingField;

	private string CountriesString__BackingField;

	private int WeeklyHours__BackingField;

	private bool SportsActivity__BackingField;

	private bool NerdActivity__BackingField;

	private bool BandActivity__BackingField;

	private bool ArtsActivity__BackingField;

	private bool ActingActivity__BackingField;

	private bool GamingActivity__BackingField;

	private bool WeebActivity__BackingField;

	private int MinHealth__BackingField;

	private int MinSmarts__BackingField;

	private int MinAthleticism__BackingField;

	private int MinLooks__BackingField;

	private int MinPopularity__BackingField;

	private int MinGrades__BackingField;

	private int PopularityScoreMale__BackingField;

	private int PopularityScoreFemale__BackingField;

	private bool RequiresTryouts__BackingField;

	private bool ExistsInMiddleSchool__BackingField;

	private bool ExistsInHighSchool__BackingField;

	private bool ExistsInCollege__BackingField;

	private int CommonnessScore__BackingField;

	public int Index
	{
		get
		{
			return Index__BackingField;
		}
		set
		{
			Index__BackingField = value;
		}
	}

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public bool DefiniteArticle
	{
		get
		{
			return DefiniteArticle__BackingField;
		}
		set
		{
			DefiniteArticle__BackingField = value;
		}
	}

	public bool ProperNoun
	{
		get
		{
			return ProperNoun__BackingField;
		}
		set
		{
			ProperNoun__BackingField = value;
		}
	}

	public string ReferenceNoun
	{
		get
		{
			return ReferenceNoun__BackingField;
		}
		set
		{
			ReferenceNoun__BackingField = value;
		}
	}

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public string LeaderTitle
	{
		get
		{
			return LeaderTitle__BackingField;
		}
		set
		{
			LeaderTitle__BackingField = value;
		}
	}

	public List<string> OtherOfficersArray
	{
		get
		{
			return OtherOfficersArray__BackingField;
		}
		set
		{
			OtherOfficersArray__BackingField = value;
		}
	}

	public bool TrackPerformance
	{
		get
		{
			return TrackPerformance__BackingField;
		}
		set
		{
			TrackPerformance__BackingField = value;
		}
	}

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}

	public string CountriesString
	{
		get
		{
			return CountriesString__BackingField;
		}
		set
		{
			CountriesString__BackingField = value;
		}
	}

	public int WeeklyHours
	{
		get
		{
			return WeeklyHours__BackingField;
		}
		set
		{
			WeeklyHours__BackingField = value;
		}
	}

	public bool SportsActivity
	{
		get
		{
			return SportsActivity__BackingField;
		}
		set
		{
			SportsActivity__BackingField = value;
		}
	}

	public bool NerdActivity
	{
		get
		{
			return NerdActivity__BackingField;
		}
		set
		{
			NerdActivity__BackingField = value;
		}
	}

	public bool BandActivity
	{
		get
		{
			return BandActivity__BackingField;
		}
		set
		{
			BandActivity__BackingField = value;
		}
	}

	public bool ArtsActivity
	{
		get
		{
			return ArtsActivity__BackingField;
		}
		set
		{
			ArtsActivity__BackingField = value;
		}
	}

	public bool ActingActivity
	{
		get
		{
			return ActingActivity__BackingField;
		}
		set
		{
			ActingActivity__BackingField = value;
		}
	}

	public bool GamingActivity
	{
		get
		{
			return GamingActivity__BackingField;
		}
		set
		{
			GamingActivity__BackingField = value;
		}
	}

	public bool WeebActivity
	{
		get
		{
			return WeebActivity__BackingField;
		}
		set
		{
			WeebActivity__BackingField = value;
		}
	}

	public int MinHealth
	{
		get
		{
			return MinHealth__BackingField;
		}
		set
		{
			MinHealth__BackingField = value;
		}
	}

	public int MinSmarts
	{
		get
		{
			return MinSmarts__BackingField;
		}
		set
		{
			MinSmarts__BackingField = value;
		}
	}

	public int MinAthleticism
	{
		get
		{
			return MinAthleticism__BackingField;
		}
		set
		{
			MinAthleticism__BackingField = value;
		}
	}

	public int MinLooks
	{
		get
		{
			return MinLooks__BackingField;
		}
		set
		{
			MinLooks__BackingField = value;
		}
	}

	public int MinPopularity
	{
		get
		{
			return MinPopularity__BackingField;
		}
		set
		{
			MinPopularity__BackingField = value;
		}
	}

	public int MinGrades
	{
		get
		{
			return MinGrades__BackingField;
		}
		set
		{
			MinGrades__BackingField = value;
		}
	}

	public int PopularityScoreMale
	{
		get
		{
			return PopularityScoreMale__BackingField;
		}
		set
		{
			PopularityScoreMale__BackingField = value;
		}
	}

	public int PopularityScoreFemale
	{
		get
		{
			return PopularityScoreFemale__BackingField;
		}
		set
		{
			PopularityScoreFemale__BackingField = value;
		}
	}

	public bool RequiresTryouts
	{
		get
		{
			return RequiresTryouts__BackingField;
		}
		set
		{
			RequiresTryouts__BackingField = value;
		}
	}

	public bool ExistsInMiddleSchool
	{
		get
		{
			return ExistsInMiddleSchool__BackingField;
		}
		set
		{
			ExistsInMiddleSchool__BackingField = value;
		}
	}

	public bool ExistsInHighSchool
	{
		get
		{
			return ExistsInHighSchool__BackingField;
		}
		set
		{
			ExistsInHighSchool__BackingField = value;
		}
	}

	public bool ExistsInCollege
	{
		get
		{
			return ExistsInCollege__BackingField;
		}
		set
		{
			ExistsInCollege__BackingField = value;
		}
	}

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}

	public bool AvailableInCountry(DataCountry country)
	{
		return false;
	}

	public string DescriptiveNameAndUseDefiniteArticle(bool useDefiniteArticle)
	{
		return null;
	}

	public int HoursPerWeek()
	{
		return 0;
	}

	public int CompareTo(object otherObject)
	{
		return 0;
	}
}
