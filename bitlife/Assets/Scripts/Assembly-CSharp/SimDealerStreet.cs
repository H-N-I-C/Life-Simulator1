using System;
using System.Collections.Generic;

[Serializable]
public class SimDealerStreet
{
	private SimStreetName StreetName__BackingField;

	private float TotalEarnings__BackingField;

	private int AgeAtLastStreetPeopleRefresh__BackingField;

	private float _control;

	private List<SimDealerStreetPerson> _personsPool;

	private List<SimDealerStreetPerson> _activePersons;

	private int _numActivePeopleNeeded;

	public SimStreetName StreetName
	{
		get
		{
			return StreetName__BackingField;
		}
		set
		{
			StreetName__BackingField = value;
		}
	}

	public float TotalEarnings
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

	public int AgeAtLastStreetPeopleRefresh
	{
		get
		{
			return AgeAtLastStreetPeopleRefresh__BackingField;
		}
		set
		{
			AgeAtLastStreetPeopleRefresh__BackingField = value;
		}
	}

	public SimDealerStreet(SimStreetName theStreetName, int peopleNeeded, float control)
	{
	}

	public string GetNameAndCapitalize(bool capitalize = true)
	{
		return null;
	}

	public void IncrementAge()
	{
	}

	public void AdjustTotalEarningsOnStreet(float earnings)
	{
	}

	public bool UpdatePersonPool()
	{
		return false;
	}

	private void AddPersonToPoolOfType(DEALER_STREET_PERSON_TYPE type)
	{
	}

	public void BuildPersonPool()
	{
	}

	private bool NameAppearsInPool(string name)
	{
		return false;
	}

	private SimDealerStreetPerson GetPoolStreetPersonOfType(DEALER_STREET_PERSON_TYPE type)
	{
		return null;
	}

	public void RefreshAvailablePeopleOnStreet(bool forceRefresh = false)
	{
	}

	public float GetControl()
	{
		return 0f;
	}

	public List<SimDealerStreetPerson> GetStreetPersonsList()
	{
		return null;
	}

	public int GetNumberOfAddicts()
	{
		return 0;
	}

	public int GetNumberOfRivals()
	{
		return 0;
	}

	public void RemovePerson(SimDealerStreetPerson person, bool died = false)
	{
	}

	public SimDealerStreetPerson ReplacePoliceOfficer(SimDealerStreetPerson person)
	{
		return null;
	}

	public void ImpactControl(int impactScore)
	{
	}

	public void AddNewPerson()
	{
	}
}
