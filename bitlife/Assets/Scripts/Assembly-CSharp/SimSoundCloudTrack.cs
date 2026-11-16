using System;

[Serializable]
public class SimSoundCloudTrack
{
	private string Title__BackingField;

	private MUSIC_PRODUCER_GENRE Genre__BackingField;

	private string Vibe__BackingField;

	private SimSoundCloudArtist Collaborator__BackingField;

	private SimPerson FeaturedArtist__BackingField;

	private string FeaturedInstrument__BackingField;

	private SimMusicProducerRecordLabel RecordLabelOwner__BackingField;

	private int YearsSinceRelease__BackingField;

	private bool FeaturedArtistAskedForCompensation__BackingField;

	private bool _hasCoverArt;

	private float _popularity;

	private float _launchReception;

	private int _thisYearsPlayCount;

	private int _lastYearsPlayCount;

	private double _totalPlayCount;

	private double _price;

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

	public MUSIC_PRODUCER_GENRE Genre
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

	public string Vibe
	{
		get
		{
			return Vibe__BackingField;
		}
		set
		{
			Vibe__BackingField = value;
		}
	}

	public SimSoundCloudArtist Collaborator
	{
		get
		{
			return Collaborator__BackingField;
		}
		set
		{
			Collaborator__BackingField = value;
		}
	}

	public SimPerson FeaturedArtist
	{
		get
		{
			return FeaturedArtist__BackingField;
		}
		set
		{
			FeaturedArtist__BackingField = value;
		}
	}

	public string FeaturedInstrument
	{
		get
		{
			return FeaturedInstrument__BackingField;
		}
		set
		{
			FeaturedInstrument__BackingField = value;
		}
	}

	public SimMusicProducerRecordLabel RecordLabelOwner
	{
		get
		{
			return RecordLabelOwner__BackingField;
		}
		set
		{
			RecordLabelOwner__BackingField = value;
		}
	}

	public int YearsSinceRelease
	{
		get
		{
			return YearsSinceRelease__BackingField;
		}
		set
		{
			YearsSinceRelease__BackingField = value;
		}
	}

	public bool FeaturedArtistAskedForCompensation
	{
		get
		{
			return FeaturedArtistAskedForCompensation__BackingField;
		}
		set
		{
			FeaturedArtistAskedForCompensation__BackingField = value;
		}
	}

	public double Price => 0.0;

	public SimSoundCloudTrack(string title, MUSIC_PRODUCER_GENRE genre, string vibe, string featuredInstrument)
	{
	}

	public void IncrementAge()
	{
	}

	public void AddFeaturedArtist(SimPerson artist)
	{
	}

	public void AddCollaborator(SimSoundCloudArtist collaborator)
	{
	}

	public void AdjustThisYearsPlayCount(int amount)
	{
	}

	private void AdjustTotalPlayCount(int amount)
	{
	}

	public double GetTotalPlayCount()
	{
		return 0.0;
	}

	public int GetThisYearsPlayCount()
	{
		return 0;
	}

	public int GetLastYearPlayCount()
	{
		return 0;
	}

	public bool HasCoverArt()
	{
		return false;
	}

	public void AddCoverArt()
	{
	}

	public void SetLaunchReception(float launchReception)
	{
	}

	public float GetPopularity()
	{
		return 0f;
	}

	public void ImpactPopularity(int impactScore)
	{
	}

	public void SellTrack(SimMusicProducerRecordLabel buyer, double price)
	{
	}

	public bool IsSold()
	{
		return false;
	}

	public string GetStatusString()
	{
		return null;
	}
}
