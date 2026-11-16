using System;

[Serializable]
public class SimMusicAlbum
{
	private string Name__BackingField;

	private int Type__BackingField;

	private int Genre__BackingField;

	private string Subgenre__BackingField;

	private string AlbumCover__BackingField;

	private int NumTracks__BackingField;

	private string MainTrackName__BackingField;

	private bool DesignedByHero__BackingField;

	private int HeroAgeAtRelease__BackingField;

	private string CountryCode__BackingField;

	private int FirstYearCopiesSold__BackingField;

	private int CopiesSold__BackingField;

	private int Award__BackingField;

	private double GrossSales__BackingField;

	private double NetEarnings__BackingField;

	private double HeroProceeds__BackingField;

	private double FirstYearHeroProceeds__BackingField;

	private double CurrentYearHeroProceeds__BackingField;

	private float Att_success__BackingField;

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

	public string Subgenre
	{
		get
		{
			return Subgenre__BackingField;
		}
		set
		{
			Subgenre__BackingField = value;
		}
	}

	public string AlbumCover
	{
		get
		{
			return AlbumCover__BackingField;
		}
		set
		{
			AlbumCover__BackingField = value;
		}
	}

	public int NumTracks
	{
		get
		{
			return NumTracks__BackingField;
		}
		set
		{
			NumTracks__BackingField = value;
		}
	}

	public string MainTrackName
	{
		get
		{
			return MainTrackName__BackingField;
		}
		set
		{
			MainTrackName__BackingField = value;
		}
	}

	public bool DesignedByHero
	{
		get
		{
			return DesignedByHero__BackingField;
		}
		set
		{
			DesignedByHero__BackingField = value;
		}
	}

	public int HeroAgeAtRelease
	{
		get
		{
			return HeroAgeAtRelease__BackingField;
		}
		set
		{
			HeroAgeAtRelease__BackingField = value;
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

	public int FirstYearCopiesSold
	{
		get
		{
			return FirstYearCopiesSold__BackingField;
		}
		set
		{
			FirstYearCopiesSold__BackingField = value;
		}
	}

	public int CopiesSold
	{
		get
		{
			return CopiesSold__BackingField;
		}
		set
		{
			CopiesSold__BackingField = value;
		}
	}

	public int Award
	{
		get
		{
			return Award__BackingField;
		}
		set
		{
			Award__BackingField = value;
		}
	}

	public double GrossSales
	{
		get
		{
			return GrossSales__BackingField;
		}
		set
		{
			GrossSales__BackingField = value;
		}
	}

	public double NetEarnings
	{
		get
		{
			return NetEarnings__BackingField;
		}
		set
		{
			NetEarnings__BackingField = value;
		}
	}

	public double HeroProceeds
	{
		get
		{
			return HeroProceeds__BackingField;
		}
		set
		{
			HeroProceeds__BackingField = value;
		}
	}

	public double FirstYearHeroProceeds
	{
		get
		{
			return FirstYearHeroProceeds__BackingField;
		}
		set
		{
			FirstYearHeroProceeds__BackingField = value;
		}
	}

	public double CurrentYearHeroProceeds
	{
		get
		{
			return CurrentYearHeroProceeds__BackingField;
		}
		set
		{
			CurrentYearHeroProceeds__BackingField = value;
		}
	}

	public float Att_success
	{
		get
		{
			return Att_success__BackingField;
		}
		set
		{
			Att_success__BackingField = value;
		}
	}

	public string GetTypeStringWithAlbum(bool album)
	{
		return null;
	}

	public string GetDescriptiveString()
	{
		return null;
	}

	public string GetBand()
	{
		return null;
	}

	public void DetermineAlbumStatsAtRelease()
	{
	}

	public void UpdateCurrentYearHeroProceeds()
	{
	}

	public void CheckForAward()
	{
	}

	public string GetAwardString()
	{
		return null;
	}

	public void ImpactSuccess(int impactScore)
	{
	}
}
