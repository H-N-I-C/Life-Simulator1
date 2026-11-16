using System;

[Serializable]
public class SimMusicProducerTrack
{
	private string Title__BackingField;

	private MUSIC_PRODUCER_GENRE Genre__BackingField;

	private string Subject__BackingField;

	private string LyricalHook__BackingField;

	private string Beat__BackingField;

	private string Vibe__BackingField;

	private SimMusicProducerArtist Artist__BackingField;

	private int YearsSinceRelease__BackingField;

	private bool WonAward__BackingField;

	private float _popularity;

	private float _launchReception;

	private int _lastYearsCopiesSold;

	private int _thisYearsCopiesSold;

	private double _totalCopiesSold;

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

	public string Subject
	{
		get
		{
			return Subject__BackingField;
		}
		set
		{
			Subject__BackingField = value;
		}
	}

	public string LyricalHook
	{
		get
		{
			return LyricalHook__BackingField;
		}
		set
		{
			LyricalHook__BackingField = value;
		}
	}

	public string Beat
	{
		get
		{
			return Beat__BackingField;
		}
		set
		{
			Beat__BackingField = value;
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

	public SimMusicProducerArtist Artist
	{
		get
		{
			return Artist__BackingField;
		}
		set
		{
			Artist__BackingField = value;
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

	public bool WonAward
	{
		get
		{
			return WonAward__BackingField;
		}
		set
		{
			WonAward__BackingField = value;
		}
	}

	public SimMusicProducerTrack(string title, MUSIC_PRODUCER_GENRE genre, SimMusicProducerArtist artist, string subject, string lyricalHook, string beat)
	{
	}

	public void IncrementAge()
	{
	}

	public void AssignArtist(SimMusicProducerArtist artist)
	{
	}

	public void SetLaunchReception(float launchReception)
	{
	}

	public string GetVibe()
	{
		return null;
	}

	public float GetPopularity()
	{
		return 0f;
	}

	public void ImpactPopularity(int impactScore)
	{
	}

	public void SetPopularity(float popularity)
	{
	}

	public void SetCopiesSold(int copiesSold)
	{
	}

	private void AdjustTotalCopiesSoldCount(int amount)
	{
	}

	public double GetTotalCopiesSoldCount()
	{
		return 0.0;
	}

	public int GetThisYearsCopiesSoldCount()
	{
		return 0;
	}

	public int GetLastYearCopiesSoldCount()
	{
		return 0;
	}

	public float GetSingleUnitProfit()
	{
		return 0f;
	}

	public string GetEmojiStringForCopiesSold()
	{
		return null;
	}

	public static string GetSalesRankString(double copiesSold)
	{
		return null;
	}

	public static string GetSalesRankEmoji(double copiesSold)
	{
		return null;
	}
}
