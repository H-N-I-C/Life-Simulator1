using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimOutdoorFishingTrip
{
	private sealed class _003C_003Ec__DisplayClass33_0
	{
		public int difficulty;

		internal bool _003CGetRandomFishForDifficulty_003Eb__0(DataOutdoorCatchableFish fish)
		{
			return false;
		}
	}

	private readonly string _fishingLocation;

	private readonly SimOutdoor _outdoorInstance;

	private readonly List<SimOutdoorCatchableFish> _caughtFish;

	private string _hookedFishSpecies;

	private SimOutdoorCatchableFish _lastCaught;

	private string _fishingBait;

	private int _numberOfTimesCastLine;

	public string FishingLocation => null;

	public int HookedFishDifficulty => 0;

	public List<SimOutdoorCatchableFish> CaughtFish => null;

	public int CatchableFishCount => 0;

	public SimOutdoorFishingTrip(SimOutdoor simOutdoor, string location, string bait)
	{
	}

	public void SetBait(string bait)
	{
	}

	public void IncrementNumberOfTimesCastLine()
	{
	}

	public void ResetNumberOfTimesCastLine()
	{
	}

	public bool CanCastLine()
	{
		return false;
	}

	public DataOutdoorCatchableFish GetRandomCatchableFish()
	{
		return null;
	}

	public void SetHookedFish(DataOutdoorCatchableFish fish)
	{
	}

	public void ReleaseHookedFish()
	{
	}

	public DataOutdoorCatchableFish GetHookedFish()
	{
		return null;
	}

	public SimOutdoorCatchableFish GetLastCaughtFish()
	{
		return null;
	}

	public bool FishEntryExists(SimOutdoorCatchableFish fish)
	{
		return false;
	}

	public void AddFishEntry(SimOutdoorCatchableFish fish)
	{
	}

	public SimOutdoorFishEntry GetFishEntryBySpecies(string fishSpecies)
	{
		return null;
	}

	public void UpdateFishEntry(SimOutdoorCatchableFish fish)
	{
	}

	public void IncrementSuccessfulFishingTrip()
	{
	}

	public void AddCaughtFish(SimOutdoorCatchableFish fish)
	{
	}

	public List<DataOutdoorCatchableFish> GetCatchableFish()
	{
		return null;
	}

	private List<DataOutdoorCatchableFish> GetFishByCommonScore()
	{
		return null;
	}

	private DataOutdoorCatchableFish GetRandomFishForDifficulty(int difficulty)
	{
		return null;
	}

	public void AddMountedFish(SimOutdoorMountedFish fish)
	{
	}
}
