using System;
using System.Collections.Generic;
using Game;

public class OutdoorHikingActivityData
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimSibling, bool> _003C_003E9__33_0;

		public static Func<SimChild, bool> _003C_003E9__33_1;

		public static Func<SimPerson, bool> _003C_003E9__33_2;

		public static Func<SimPet, bool> _003C_003E9__36_0;

		public static Func<SimPet, bool> _003C_003E9__36_1;

		internal bool _003CGetPersonsList_003Eb__33_0(SimSibling t)
		{
			return false;
		}

		internal bool _003CGetPersonsList_003Eb__33_1(SimChild t)
		{
			return false;
		}

		internal bool _003CGetPersonsList_003Eb__33_2(SimPerson t)
		{
			return false;
		}

		internal bool _003CGetPetList_003Eb__36_0(SimPet x)
		{
			return false;
		}

		internal bool _003CGetPetList_003Eb__36_1(SimPet t)
		{
			return false;
		}
	}

	private bool _playedDumb;

	private bool _playedScenario;

	private SimPerson _person;

	private SimPet _pet;

	private DataOutdoorDestination _destination;

	private string _findingDescription;

	private readonly SimOutdoor _outdoor;

	private float _enjoyment;

	private string _selectedLandmark;

	private bool CheckLoverAtEnd__BackingField;

	private string LastDumbScenarioTitle__BackingField;

	private List<DataOutdoorDestination> Destinations__BackingField;

	public bool CheckLoverAtEnd
	{
		get
		{
			return CheckLoverAtEnd__BackingField;
		}
		set
		{
			CheckLoverAtEnd__BackingField = value;
		}
	}

	public string FindingDescription => null;

	public string LastDumbScenarioTitle
	{
		get
		{
			return LastDumbScenarioTitle__BackingField;
		}
		set
		{
			LastDumbScenarioTitle__BackingField = value;
		}
	}

	public List<DataOutdoorDestination> Destinations
	{
		get
		{
			return Destinations__BackingField;
		}
		set
		{
			Destinations__BackingField = value;
		}
	}

	public OutdoorHikingActivityData(SimOutdoor outdoor)
	{
	}

	public void Setup(SimPerson person, SimPet pet, DataOutdoorDestination destination)
	{
	}

	public SimPerson GetPerson()
	{
		return null;
	}

	public SimPet GetDog()
	{
		return null;
	}

	public bool IsPerson()
	{
		return false;
	}

	public bool IsDog()
	{
		return false;
	}

	public Scenario GenerateNextStep(bool getEndScenario = false)
	{
		return null;
	}

	private Scenario GetEndScenario()
	{
		return null;
	}

	public string GetDestinationString()
	{
		return null;
	}

	public string GetDestinationName(bool useDefiniteArticle = true)
	{
		return null;
	}

	public List<SimPerson> GetPersonsList()
	{
		return null;
	}

	public string CreatePersonString(SimPerson person)
	{
		return null;
	}

	public string GetCompanionString(bool addComma = true)
	{
		return null;
	}

	public List<SimPet> GetPetList()
	{
		return null;
	}

	public Scenario GetAnimalEncounter()
	{
		return null;
	}

	public SimOutdoorAnimal GenerateAnimal()
	{
		return null;
	}

	public bool IsSolo()
	{
		return false;
	}

	public void RegisterFinding(string finding)
	{
	}

	public void ImpactEnjoyment(int score)
	{
	}

	public string GetCompanionFirstName()
	{
		return null;
	}

	private string RelationshipStatusString(SimPerson person)
	{
		return null;
	}

	public string GetLandmark()
	{
		return null;
	}

	public int GetAppealScore()
	{
		return 0;
	}

	public float GetEnjoyment()
	{
		return 0f;
	}

	public void ResetCompanion()
	{
	}

	public SimOutdoorPlant GeneratePlant()
	{
		return null;
	}

	public Scenario KillGuest(SimOutdoorPlant plant)
	{
		return null;
	}

	public Scenario KillGuest(SimOutdoorAnimal animal)
	{
		return null;
	}

	public DataOutdoorDestination GetDestination()
	{
		return null;
	}
}
