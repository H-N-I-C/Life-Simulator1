using System;
using System.Collections;

[Serializable]
public class SimSpecialJobMusician : SimSpecialJob
{
	private int Role__BackingField;

	private int Genre__BackingField;

	private SimMusicSoloContract SoloContract__BackingField;

	private SimMusicBand Band__BackingField;

	private string StageName__BackingField;

	private int YearsCompleted__BackingField;

	private double TotalEarnings__BackingField;

	private ArrayList _DiscographyArray__BackingField;

	private int AgeAtLastPractice__BackingField;

	private int NumPracticesAtCurrentAge__BackingField;

	private int AgeAtLastAttemptToTransitionToBand__BackingField;

	private int AgeAtLastAttemptToTransitionToSolo__BackingField;

	private int AgeAtLastConcertInteraction__BackingField;

	private int NumConcertsAtCurrentAge__BackingField;

	private int NumConcertRejectionsAtCurrentAge__BackingField;

	private int AgeAtLastTour__BackingField;

	private int AgeAtLastTourRejection__BackingField;

	private float Att_popularity__BackingField;

	private float Att_respect__BackingField;

	private float Att_performance__BackingField;

	public int Role
	{
		get
		{
			return Role__BackingField;
		}
		set
		{
			Role__BackingField = value;
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

	public SimMusicSoloContract SoloContract
	{
		get
		{
			return SoloContract__BackingField;
		}
		set
		{
			SoloContract__BackingField = value;
		}
	}

	public SimMusicBand Band
	{
		get
		{
			return Band__BackingField;
		}
		set
		{
			Band__BackingField = value;
		}
	}

	public string StageName
	{
		get
		{
			return StageName__BackingField;
		}
		set
		{
			StageName__BackingField = value;
		}
	}

	public int YearsCompleted
	{
		get
		{
			return YearsCompleted__BackingField;
		}
		set
		{
			YearsCompleted__BackingField = value;
		}
	}

	public double TotalEarnings
	{
		get
		{
			return TotalEarnings__BackingField;
		}
		set
		{
			TotalEarnings__BackingField = value;
		}
	}

	private ArrayList _DiscographyArray
	{
		get
		{
			return _DiscographyArray__BackingField;
		}
		set
		{
			_DiscographyArray__BackingField = value;
		}
	}

	public ArrayList DiscographyArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastPractice
	{
		get
		{
			return AgeAtLastPractice__BackingField;
		}
		set
		{
			AgeAtLastPractice__BackingField = value;
		}
	}

	public int NumPracticesAtCurrentAge
	{
		get
		{
			return NumPracticesAtCurrentAge__BackingField;
		}
		set
		{
			NumPracticesAtCurrentAge__BackingField = value;
		}
	}

	public int AgeAtLastAttemptToTransitionToBand
	{
		get
		{
			return AgeAtLastAttemptToTransitionToBand__BackingField;
		}
		set
		{
			AgeAtLastAttemptToTransitionToBand__BackingField = value;
		}
	}

	public int AgeAtLastAttemptToTransitionToSolo
	{
		get
		{
			return AgeAtLastAttemptToTransitionToSolo__BackingField;
		}
		set
		{
			AgeAtLastAttemptToTransitionToSolo__BackingField = value;
		}
	}

	public int AgeAtLastConcertInteraction
	{
		get
		{
			return AgeAtLastConcertInteraction__BackingField;
		}
		set
		{
			AgeAtLastConcertInteraction__BackingField = value;
		}
	}

	public int NumConcertsAtCurrentAge
	{
		get
		{
			return NumConcertsAtCurrentAge__BackingField;
		}
		set
		{
			NumConcertsAtCurrentAge__BackingField = value;
		}
	}

	public int NumConcertRejectionsAtCurrentAge
	{
		get
		{
			return NumConcertRejectionsAtCurrentAge__BackingField;
		}
		set
		{
			NumConcertRejectionsAtCurrentAge__BackingField = value;
		}
	}

	public int AgeAtLastTour
	{
		get
		{
			return AgeAtLastTour__BackingField;
		}
		set
		{
			AgeAtLastTour__BackingField = value;
		}
	}

	public int AgeAtLastTourRejection
	{
		get
		{
			return AgeAtLastTourRejection__BackingField;
		}
		set
		{
			AgeAtLastTourRejection__BackingField = value;
		}
	}

	public float Att_popularity
	{
		get
		{
			return Att_popularity__BackingField;
		}
		set
		{
			Att_popularity__BackingField = value;
		}
	}

	public float Att_respect
	{
		get
		{
			return Att_respect__BackingField;
		}
		set
		{
			Att_respect__BackingField = value;
		}
	}

	public float Att_performance
	{
		get
		{
			return Att_performance__BackingField;
		}
		set
		{
			Att_performance__BackingField = value;
		}
	}

	public SimSpecialJobMusician(int theRole, int theGenre, SimMusicSoloContract theContract, SimMusicBand theBand)
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

	public override bool CanAttendRehab()
	{
		return false;
	}

	public void AnnualAttributeAdjustment()
	{
	}

	public SimCompany GetRecordCompany()
	{
		return null;
	}

	public ArrayList GetInstrumentExperienceArrayForCurrentRole()
	{
		return null;
	}

	public ArrayList GetAllStudioAlbumsEligibleForRerelease()
	{
		return null;
	}

	public string GetMostRecentSongTitleString()
	{
		return null;
	}

	public bool CanTransitionToBand()
	{
		return false;
	}

	public bool CanTransitionToSolo()
	{
		return false;
	}

	public static string GetMusicianFameTitle()
	{
		return null;
	}

	public static ArrayList GetAllHeroSpecialOccupationsAsMusician()
	{
		return null;
	}

	public static ArrayList GetAllHeroSpecialOccupationsForRole(int role)
	{
		return null;
	}

	public static ArrayList GetAllHeroSpecialOccupationsForGenre(int genre)
	{
		return null;
	}

	public static ArrayList GetCareerDiscography()
	{
		return null;
	}

	public static double GetCareerEarnings()
	{
		return 0.0;
	}

	public static int PropensityScoreForHeroToPlayRole(int role)
	{
		return 0;
	}

	public static SimMusicSoloContract GetContractForRoleAndGenreAndCompany(int role, int genre, SimCompany company)
	{
		return null;
	}

	public static SimMusicBand GenerateBandForRole(int role)
	{
		return null;
	}

	public static SimMusicBand GenerateBandForRoleAndGenre(int role, int genre)
	{
		return null;
	}

	public static int GetRandomMusicGenreForHeroInRoleAndIsSoloOrForBand(int role, bool solo, bool band)
	{
		return 0;
	}

	public ObjectList GetCompilationNameBank()
	{
		return null;
	}

	public ObjectList GetAlbumNameBank()
	{
		return null;
	}

	public ObjectList GetSongNameBank()
	{
		return null;
	}

	public static ArrayList GetStageNameBankForNameAndGenreAndGender(SimPersonName name, int genre, int gender)
	{
		return null;
	}

	public static ArrayList GetBandNameBankForGenreAndGender(int genre, int gender)
	{
		return null;
	}

	public void ImpactPopularity(int impactScore)
	{
	}

	public void ImpactRespect(int impactScore)
	{
	}

	public void ImpactPerformance(int impactScore)
	{
	}
}
