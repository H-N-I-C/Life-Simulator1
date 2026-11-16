using System;
using System.Collections;

[Serializable]
public class SimMusicBand
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Comparison<SimBandmate> _003C_003E9__86_0;

		internal int _003CRefreshBand_003Eb__86_0(SimBandmate x, SimBandmate y)
		{
			return 0;
		}
	}

	private string Name__BackingField;

	private int Genre__BackingField;

	private SimCompany RecordCompany__BackingField;

	private int Gender__BackingField;

	private int NumMembersNotIncludingHero__BackingField;

	private int NumSingers__BackingField;

	private int NumGuitarists__BackingField;

	private int NumBassists__BackingField;

	private int NumDrummers__BackingField;

	private int NumKeyboardists__BackingField;

	private ArrayList BandmateArray__BackingField;

	private int AgeAtLastBandmateRefresh__BackingField;

	private float PreHeroAnnualEarnings__BackingField;

	private float PreHeroPopularity__BackingField;

	private int HeroAgeAtJoin__BackingField;

	private int HeroAgeAtLastKickedOut__BackingField;

	private int HeroAgeAtLastBreakUpAttempt__BackingField;

	private int HeroAgeAtLastBandNameChangeAttempt__BackingField;

	private int HeroAgeAtLastBandNameChange__BackingField;

	private int HeroAgeAtLastFailedBandmateKickOutAttempt__BackingField;

	private float Att_magic__BackingField;

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

	public int Genre
	{
		get
		{
			return Genre__BackingField;
		}
		set
		{
			Genre__BackingField = value;
		}
	}

	public SimCompany RecordCompany
	{
		get
		{
			return RecordCompany__BackingField;
		}
		set
		{
			RecordCompany__BackingField = value;
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

	public int NumMembersNotIncludingHero
	{
		get
		{
			return NumMembersNotIncludingHero__BackingField;
		}
		set
		{
			NumMembersNotIncludingHero__BackingField = value;
		}
	}

	public int NumSingers
	{
		get
		{
			return NumSingers__BackingField;
		}
		set
		{
			NumSingers__BackingField = value;
		}
	}

	public int NumGuitarists
	{
		get
		{
			return NumGuitarists__BackingField;
		}
		set
		{
			NumGuitarists__BackingField = value;
		}
	}

	public int NumBassists
	{
		get
		{
			return NumBassists__BackingField;
		}
		set
		{
			NumBassists__BackingField = value;
		}
	}

	public int NumDrummers
	{
		get
		{
			return NumDrummers__BackingField;
		}
		set
		{
			NumDrummers__BackingField = value;
		}
	}

	public int NumKeyboardists
	{
		get
		{
			return NumKeyboardists__BackingField;
		}
		set
		{
			NumKeyboardists__BackingField = value;
		}
	}

	public ArrayList BandmateArray
	{
		get
		{
			return BandmateArray__BackingField;
		}
		set
		{
			BandmateArray__BackingField = value;
		}
	}

	public int AgeAtLastBandmateRefresh
	{
		get
		{
			return AgeAtLastBandmateRefresh__BackingField;
		}
		set
		{
			AgeAtLastBandmateRefresh__BackingField = value;
		}
	}

	public float PreHeroAnnualEarnings
	{
		get
		{
			return PreHeroAnnualEarnings__BackingField;
		}
		set
		{
			PreHeroAnnualEarnings__BackingField = value;
		}
	}

	public float PreHeroPopularity
	{
		get
		{
			return PreHeroPopularity__BackingField;
		}
		set
		{
			PreHeroPopularity__BackingField = value;
		}
	}

	public int HeroAgeAtJoin
	{
		get
		{
			return HeroAgeAtJoin__BackingField;
		}
		set
		{
			HeroAgeAtJoin__BackingField = value;
		}
	}

	public int HeroAgeAtLastKickedOut
	{
		get
		{
			return HeroAgeAtLastKickedOut__BackingField;
		}
		set
		{
			HeroAgeAtLastKickedOut__BackingField = value;
		}
	}

	public int HeroAgeAtLastBreakUpAttempt
	{
		get
		{
			return HeroAgeAtLastBreakUpAttempt__BackingField;
		}
		set
		{
			HeroAgeAtLastBreakUpAttempt__BackingField = value;
		}
	}

	public int HeroAgeAtLastBandNameChangeAttempt
	{
		get
		{
			return HeroAgeAtLastBandNameChangeAttempt__BackingField;
		}
		set
		{
			HeroAgeAtLastBandNameChangeAttempt__BackingField = value;
		}
	}

	public int HeroAgeAtLastBandNameChange
	{
		get
		{
			return HeroAgeAtLastBandNameChange__BackingField;
		}
		set
		{
			HeroAgeAtLastBandNameChange__BackingField = value;
		}
	}

	public int HeroAgeAtLastFailedBandmateKickOutAttempt
	{
		get
		{
			return HeroAgeAtLastFailedBandmateKickOutAttempt__BackingField;
		}
		set
		{
			HeroAgeAtLastFailedBandmateKickOutAttempt__BackingField = value;
		}
	}

	public float Att_magic
	{
		get
		{
			return Att_magic__BackingField;
		}
		set
		{
			Att_magic__BackingField = value;
		}
	}

	public SimMusicBand(int theRole, int theGenre)
	{
	}

	public void IncrementYearsInBand()
	{
	}

	public void RefreshBand()
	{
	}

	public SimSpecialJobMusician GetCurrentSpecialJobMusician()
	{
		return null;
	}

	public float GetAverageRelationshipStrengthWithBand()
	{
		return 0f;
	}

	public float GetAverageBandRespectScore()
	{
		return 0f;
	}

	public SimBandmate GetCraziestBandmate()
	{
		return null;
	}

	public void ImpactAllRelationshipStrengths(int impactScore)
	{
	}

	public float GetAnnualEarningsForPopularity(float popularity)
	{
		return 0f;
	}
}
