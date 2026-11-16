using System;
using System.Collections.Generic;

public class ClubTakeoverActivity
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimMusicProducerArtist, float> _003C_003E9__19_0;

		internal float _003CGetArtistsByPopularity_003Eb__19_0(SimMusicProducerArtist x)
		{
			return 0f;
		}
	}

	private static bool ClubTakeoverAlwaysSucceed__BackingField;

	private static bool ClubTakeoverIgnoreYearlyLimit__BackingField;

	private readonly SimMusicProducerRecordLabel _recordLabel;

	private readonly SimHero _hero;

	private readonly Life _heroLife;

	private readonly SimSpecialJobMusicProducer _musicProducerJob;

	private SimCompany _clubTakenOver;

	private SimMusicProducerArtist _artistToPromote;

	private float PopularityBefore__BackingField;

	private static bool ClubTakeoverAlwaysSucceed
	{
		get
		{
			return ClubTakeoverAlwaysSucceed__BackingField;
		}
		set
		{
			ClubTakeoverAlwaysSucceed__BackingField = value;
		}
	}

	private static bool ClubTakeoverIgnoreYearlyLimit
	{
		get
		{
			return ClubTakeoverIgnoreYearlyLimit__BackingField;
		}
		set
		{
			ClubTakeoverIgnoreYearlyLimit__BackingField = value;
		}
	}

	public float PopularityBefore
	{
		get
		{
			return PopularityBefore__BackingField;
		}
		private set
		{
			PopularityBefore__BackingField = value;
		}
	}

	public List<SimMusicProducerArtist> GetArtistsByPopularity()
	{
		return null;
	}

	public List<SimCompany> GetLocalNightClubs(int amount)
	{
		return null;
	}

	public void SetClubToTakeOver(SimCompany clubTakenOver)
	{
	}

	public SimCompany GetClubTakenOver()
	{
		return null;
	}

	public void SetArtistToPromote(SimMusicProducerArtist artistToPromote)
	{
	}

	public SimMusicProducerArtist GetArtistToPromote()
	{
		return null;
	}

	public float GetHeroReputation()
	{
		return 0f;
	}

	public float GetArtistReputation()
	{
		return 0f;
	}

	public bool CanTakeOverThisYear()
	{
		return false;
	}

	public int GetTakeOversThisYear()
	{
		return 0;
	}

	public void SetTakeOversThisYear(int takeOversThisYear)
	{
	}

	public void MarkArtistAsTakenOverClub()
	{
	}

	public void ImpactArtistPopularity(int impact)
	{
	}

	public void ImpactHeroReputation(int impact)
	{
	}

	public void ImpactHappiness(int impact)
	{
	}

	public void ImpactRelationship(int impact)
	{
	}

	public void RemoveArtistFromLabel()
	{
	}

	public List<SimCar> GetGiftCars()
	{
		return null;
	}

	public List<SimJewel> GetGiftJewelry()
	{
		return null;
	}

	public List<SimWatercraft> GetGiftBoats()
	{
		return null;
	}

	public List<SimMusicInstrumentPhysical> GetGiftMusicalInstruments()
	{
		return null;
	}

	public void AcceptCar(SimCar car)
	{
	}

	public void AcceptJewelry(SimJewel jewel)
	{
	}

	public void AcceptBoat(SimWatercraft boat)
	{
	}

	public void AcceptInstrument(SimMusicInstrumentPhysical instrument)
	{
	}

	public bool GetClubTakeoverRollSuccess()
	{
		return false;
	}
}
