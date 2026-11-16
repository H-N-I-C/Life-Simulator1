using System;
using System.Collections.Generic;

[Serializable]
public class SimRaceCarDetails
{
	private int RacesWon__BackingField;

	private int RacesLost__BackingField;

	private float _baseSpeed;

	private float _baseAcceleration;

	private float _baseHandling;

	private float _baseDurability;

	private int _numberOfModSlots;

	private List<SimRaceCarMod> _mods;

	public int RacesWon
	{
		get
		{
			return RacesWon__BackingField;
		}
		set
		{
			RacesWon__BackingField = value;
		}
	}

	public int RacesLost
	{
		get
		{
			return RacesLost__BackingField;
		}
		set
		{
			RacesLost__BackingField = value;
		}
	}

	public SimRaceCarDetails(float baseSpeed, float baseAcceleration, float baseHandling, float baseDurability, int modSlots)
	{
	}

	public void IncrementAge()
	{
	}

	public float GetSpeed(float currentCarCondition)
	{
		return 0f;
	}

	public float GetAcceleration(float currentCarCondition)
	{
		return 0f;
	}

	public float GetHandling(float currentCarCondition)
	{
		return 0f;
	}

	public float GetDurability(float currentCarCondition)
	{
		return 0f;
	}

	public List<SimRaceCarMod> GetModsList()
	{
		return null;
	}

	public void AddMod(SimRaceCarMod mod)
	{
	}

	public void RemoveMod(SimRaceCarMod mod)
	{
	}

	public int GetNumberOfModSlots()
	{
		return 0;
	}

	public int GetNumberOfOpenModSlots()
	{
		return 0;
	}

	public void RemoveAllMods()
	{
	}
}
