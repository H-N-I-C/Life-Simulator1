using System;

[Serializable]
public class SimMusicProfession
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSoundCloudTrack, bool> _003C_003E9__24_0;

		internal bool _003CAgeCheckCoverArtScenario_003Eb__24_0(SimSoundCloudTrack x)
		{
			return false;
		}
	}

	private SimSoundCloud SoundCloud__BackingField;

	private int YearsSinceCoverArtScenario__BackingField;

	private int YearsSinceRoyaltiesScenario__BackingField;

	private float Att_AmateurMusicProducerIndustryReputation__BackingField;

	private SimSoundCloud SoundCloud
	{
		get
		{
			return SoundCloud__BackingField;
		}
		set
		{
			SoundCloud__BackingField = value;
		}
	}

	private int YearsSinceCoverArtScenario
	{
		get
		{
			return YearsSinceCoverArtScenario__BackingField;
		}
		set
		{
			YearsSinceCoverArtScenario__BackingField = value;
		}
	}

	private int YearsSinceRoyaltiesScenario
	{
		get
		{
			return YearsSinceRoyaltiesScenario__BackingField;
		}
		set
		{
			YearsSinceRoyaltiesScenario__BackingField = value;
		}
	}

	private float Att_AmateurMusicProducerIndustryReputation
	{
		get
		{
			return Att_AmateurMusicProducerIndustryReputation__BackingField;
		}
		set
		{
			Att_AmateurMusicProducerIndustryReputation__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	public bool HasSoundCloudAccount()
	{
		return false;
	}

	public SimSoundCloud GetSoundCloudAccount()
	{
		return null;
	}

	public void SetSoundCloudAccount(SimSoundCloud soundCloud)
	{
	}

	public void DeleteSoundCloudAccount()
	{
	}

	public float GetIndustryReputation()
	{
		return 0f;
	}

	public void SetIndustryReputation(float industryReputation)
	{
	}

	public void ImpactIndustryReputation(int impactScore)
	{
	}

	private void AgeCheckCoverArtScenario()
	{
	}

	private void AgeCheckSingerRoyalties()
	{
	}

	public static int GetMinimumAgeForSoundCloudSocialMedia()
	{
		return 0;
	}

	public static bool CanHaveSoundCloudSocialMedia()
	{
		return false;
	}
}
