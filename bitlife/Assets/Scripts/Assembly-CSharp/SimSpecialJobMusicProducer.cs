using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimSpecialJobMusicProducer : SimSpecialJob
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimMusicProducerArtist, bool> _003C_003E9__78_0;

		public static Func<SimMusicProducerTrack, bool> _003C_003E9__105_0;

		internal bool _003CGetNumberOfHeroAcquiredArtists_003Eb__78_0(SimMusicProducerArtist artist)
		{
			return false;
		}

		internal bool _003CCanProduceTrackThisYear_003Eb__105_0(SimMusicProducerTrack track)
		{
			return false;
		}
	}

	private SimMusicProducerRecordLabel RecordLabel__BackingField;

	private MUSIC_PRODUCER_JOB_TITLE Title__BackingField;

	private int YearsWithLabel__BackingField;

	private string StageName__BackingField;

	private int HeroAgeAtLastBestSongAward__BackingField;

	private int HeroAgeAtLastBestProducerAward__BackingField;

	private int ClubTakeoversThisYear__BackingField;

	private int TalentScoutEventsThisYear__BackingField;

	private bool HasAcquiredTalentThisYear__BackingField;

	private int AwardsCount__BackingField;

	private int ScoreProducedThisYear__BackingField;

	[NonSerialized]
	private List<Scenario> _MusicProducerIncrementAgeScenarioQueueArray;

	[NonSerialized]
	private static Lazy<SimMusicProducerAgeUp> _lazyAgeUp;

	private float _reputation;

	private List<SimMusicProducerTrack> _producedTracks;

	private double _commissions;

	private double _royalties;

	public SimMusicProducerRecordLabel RecordLabel
	{
		get
		{
			return RecordLabel__BackingField;
		}
		set
		{
			RecordLabel__BackingField = value;
		}
	}

	public MUSIC_PRODUCER_JOB_TITLE Title
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

	public int YearsWithLabel
	{
		get
		{
			return YearsWithLabel__BackingField;
		}
		set
		{
			YearsWithLabel__BackingField = value;
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

	public int HeroAgeAtLastBestSongAward
	{
		get
		{
			return HeroAgeAtLastBestSongAward__BackingField;
		}
		set
		{
			HeroAgeAtLastBestSongAward__BackingField = value;
		}
	}

	public int HeroAgeAtLastBestProducerAward
	{
		get
		{
			return HeroAgeAtLastBestProducerAward__BackingField;
		}
		set
		{
			HeroAgeAtLastBestProducerAward__BackingField = value;
		}
	}

	public int ClubTakeoversThisYear
	{
		get
		{
			return ClubTakeoversThisYear__BackingField;
		}
		set
		{
			ClubTakeoversThisYear__BackingField = value;
		}
	}

	public int TalentScoutEventsThisYear
	{
		get
		{
			return TalentScoutEventsThisYear__BackingField;
		}
		set
		{
			TalentScoutEventsThisYear__BackingField = value;
		}
	}

	public bool HasAcquiredTalentThisYear
	{
		get
		{
			return HasAcquiredTalentThisYear__BackingField;
		}
		set
		{
			HasAcquiredTalentThisYear__BackingField = value;
		}
	}

	public int AwardsCount
	{
		get
		{
			return AwardsCount__BackingField;
		}
		set
		{
			AwardsCount__BackingField = value;
		}
	}

	public int ScoreProducedThisYear
	{
		get
		{
			return ScoreProducedThisYear__BackingField;
		}
		set
		{
			ScoreProducedThisYear__BackingField = value;
		}
	}

	public List<Scenario> MusicProducerIncrementAgeScenarioQueueArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private static SimMusicProducerAgeUp _ageUp => null;

	private static Life _life => null;

	public SimSpecialJobMusicProducer(SimMusicProducerRecordLabel recordLabel, MUSIC_PRODUCER_JOB_TITLE title, float salary)
	{
	}

	public SimSpecialJobMusicProducer(SimMusicProducerRecordLabel recordLabel, Life life)
	{
	}

	public static MUSIC_PRODUCER_JOB_TITLE GetMostRecentJobTitle(Life life)
	{
		return MUSIC_PRODUCER_JOB_TITLE.NONE;
	}

	public static SimSpecialJobMusicProducer GetJobByLife(Life life)
	{
		return null;
	}

	public int GetTalentScoutEventsRemaining()
	{
		return 0;
	}

	public override void IncrementAge()
	{
	}

	public void AddCommission(double amount)
	{
	}

	public double GetCommissions()
	{
		return 0.0;
	}

	public double GetRoyalties()
	{
		return 0.0;
	}

	public float GetReputation()
	{
		return 0f;
	}

	public string JobTitleCapitalized()
	{
		return null;
	}

	public override string JobTitleWithArticle(bool article)
	{
		return null;
	}

	public static SimSpecialJobMusicProducer GetCurrentSpecialJobMusicProducer()
	{
		return null;
	}

	public static ArrayList GetAllHeroSpecialOccupationsAsMusicProducer()
	{
		return null;
	}

	public void PromoteHeroToTitle(MUSIC_PRODUCER_JOB_TITLE title)
	{
	}

	public bool TryPromote()
	{
		return false;
	}

	public bool QualifiesForPromotionToTitle(MUSIC_PRODUCER_JOB_TITLE newTitle)
	{
		return false;
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

	private int GetNumberOfHeroAcquiredArtists()
	{
		return 0;
	}

	public void ImpactReputation(int impactScore)
	{
	}

	public void SetReputation(float reputation)
	{
	}

	private float GetInstrumentSkillPoints()
	{
		return 0f;
	}

	private float GetMusicSpecialTalentSkillPoints()
	{
		return 0f;
	}

	private float GetIndustryReputationPoints()
	{
		return 0f;
	}

	private float GetJobOfferQualificationScore()
	{
		return 0f;
	}

	public string GetReasonForJobOfferRejectionString()
	{
		return null;
	}

	public bool WillGetJobOffer()
	{
		return false;
	}

	public string FormatSalary()
	{
		return null;
	}

	public bool TryNegotiateSalary()
	{
		return false;
	}

	private bool WillNegotiateSalary()
	{
		return false;
	}

	public void AddTrack(SimMusicProducerTrack track)
	{
	}

	public List<SimMusicProducerTrack> GetProducedTracks()
	{
		return null;
	}

	public List<SimMusicProducerTrack> GetProducedTracksForArtist(SimMusicProducerArtist artist)
	{
		return null;
	}

	public static string GetJobTitleStringForTitle(MUSIC_PRODUCER_JOB_TITLE jobTitle, bool capitalize)
	{
		return null;
	}

	public SimMusicProducerArtist FindPotentialTalent(MUSIC_PRODUCER_GENRE genre, int minVocalQuality, int maxVocalQuality)
	{
		return null;
	}

	public override void OnJobEnded()
	{
	}

	private static void CancelAllMusicProducerScenarios()
	{
	}

	private void EnablePreventRandomPush()
	{
	}

	public float GetTrackRoyaltyPercentage()
	{
		return 0f;
	}

	public static MUSIC_PRODUCER_GENRE GetRandomCompatibleGenre(MUSIC_PRODUCER_GENRE primaryGenre)
	{
		return MUSIC_PRODUCER_GENRE.NONE;
	}

	public static bool IsGenreCompatible(MUSIC_PRODUCER_GENRE genreToCheck, MUSIC_PRODUCER_GENRE primaryGenre)
	{
		return false;
	}

	public static string GetRandomVibeForGenre(MUSIC_PRODUCER_GENRE genre)
	{
		return null;
	}

	public static float GetSalaryForTitle(MUSIC_PRODUCER_JOB_TITLE title)
	{
		return 0f;
	}

	public static List<MUSIC_PRODUCER_GENRE> GetCompatibleGenres(MUSIC_PRODUCER_GENRE primaryGenre)
	{
		return null;
	}

	public static MUSIC_PRODUCER_GENRE GetRandomGenre()
	{
		return MUSIC_PRODUCER_GENRE.NONE;
	}

	public bool CanProduceTrackThisYear()
	{
		return false;
	}

	public static int GetMaximumTrackPerYear()
	{
		return 0;
	}
}
