using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimRacingCup
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRacingTrack, int> _003C_003E9__72_0;

		internal int _003C_002Ector_003Eb__72_0(SimRacingTrack x)
		{
			return 0;
		}
	}

	private SimRacingSponsor Sponsor1__BackingField;

	private SimRacingSponsor Sponsor2__BackingField;

	private int NumTimesRacedThisAgeUp__BackingField;

	private int Placement__BackingField;

	private int BestPlacement__BackingField;

	private string VehicleUsed__BackingField;

	private int HeroAgeAtLastCupSeries__BackingField;

	private int BronzeTrophies__BackingField;

	private int SilverTrophies__BackingField;

	private int GoldTrophies__BackingField;

	private string Name__BackingField;

	private string OriginalName__BackingField;

	private int Difficulty__BackingField;

	private float Winnings__BackingField;

	private float LifeTimeEarnings__BackingField;

	private RACING_TRACK_CATEGORY CircuitType__BackingField;

	private RACING_LICENSE CircuitLicense__BackingField;

	private List<SimRacingTrack> Tracks__BackingField;

	public SimRacingSponsor Sponsor1
	{
		get
		{
			return Sponsor1__BackingField;
		}
		set
		{
			Sponsor1__BackingField = value;
		}
	}

	public SimRacingSponsor Sponsor2
	{
		get
		{
			return Sponsor2__BackingField;
		}
		set
		{
			Sponsor2__BackingField = value;
		}
	}

	public int NumTimesRacedThisAgeUp
	{
		get
		{
			return NumTimesRacedThisAgeUp__BackingField;
		}
		set
		{
			NumTimesRacedThisAgeUp__BackingField = value;
		}
	}

	public int Placement
	{
		get
		{
			return Placement__BackingField;
		}
		set
		{
			Placement__BackingField = value;
		}
	}

	public int BestPlacement
	{
		get
		{
			return BestPlacement__BackingField;
		}
		set
		{
			BestPlacement__BackingField = value;
		}
	}

	public string VehicleUsed
	{
		get
		{
			return VehicleUsed__BackingField;
		}
		set
		{
			VehicleUsed__BackingField = value;
		}
	}

	public int HeroAgeAtLastCupSeries
	{
		get
		{
			return HeroAgeAtLastCupSeries__BackingField;
		}
		set
		{
			HeroAgeAtLastCupSeries__BackingField = value;
		}
	}

	public int BronzeTrophies
	{
		get
		{
			return BronzeTrophies__BackingField;
		}
		set
		{
			BronzeTrophies__BackingField = value;
		}
	}

	public int SilverTrophies
	{
		get
		{
			return SilverTrophies__BackingField;
		}
		set
		{
			SilverTrophies__BackingField = value;
		}
	}

	public int GoldTrophies
	{
		get
		{
			return GoldTrophies__BackingField;
		}
		set
		{
			GoldTrophies__BackingField = value;
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

	public string OriginalName
	{
		get
		{
			return OriginalName__BackingField;
		}
		set
		{
			OriginalName__BackingField = value;
		}
	}

	public int Difficulty
	{
		get
		{
			return Difficulty__BackingField;
		}
		set
		{
			Difficulty__BackingField = value;
		}
	}

	public float Winnings
	{
		get
		{
			return Winnings__BackingField;
		}
		set
		{
			Winnings__BackingField = value;
		}
	}

	public float LifeTimeEarnings
	{
		get
		{
			return LifeTimeEarnings__BackingField;
		}
		set
		{
			LifeTimeEarnings__BackingField = value;
		}
	}

	public RACING_TRACK_CATEGORY CircuitType
	{
		get
		{
			return CircuitType__BackingField;
		}
		private set
		{
			CircuitType__BackingField = value;
		}
	}

	public RACING_LICENSE CircuitLicense
	{
		get
		{
			return CircuitLicense__BackingField;
		}
		private set
		{
			CircuitLicense__BackingField = value;
		}
	}

	public List<SimRacingTrack> Tracks
	{
		get
		{
			return Tracks__BackingField;
		}
		private set
		{
			Tracks__BackingField = value;
		}
	}

	public SimRacingCup(string name, RACING_LICENSE cupLicenseType, RACING_TRACK_CATEGORY circuitType, List<SimRacingTrack> tracks)
	{
	}

	private void CreateStreetSponsors()
	{
	}

	private void CreateRaceSponsors()
	{
	}

	public float CalculateWinnings(int finishedPlacement)
	{
		return 0f;
	}

	public string GetTrackDescription()
	{
		return null;
	}

	public string GetSponsorDescription(bool sentenceStart)
	{
		return null;
	}

	public string GetCircuitString()
	{
		return null;
	}

	public string GetLicenseString()
	{
		return null;
	}

	public int GetScoreForPlace(int place)
	{
		return 0;
	}

	public void RecordRaceResult(int place, float winnings)
	{
	}

	private int GetPlaceScoreForCircuitRace(int place)
	{
		return 0;
	}

	private int GetPlaceScoreForStreetRace(int place)
	{
		return 0;
	}

	private int GetPlaceScoreForDemolitionRace(int place)
	{
		return 0;
	}

	private (int, int) GetMinMaxWinnings(DataRacingCupSeriesWinning winnings)
	{
		return default;
	}

	public void IncrementAge()
	{
	}

	public string GetCurrentPlacement()
	{
		return null;
	}

	public int GetSponsorQuantity()
	{
		return 0;
	}
}
