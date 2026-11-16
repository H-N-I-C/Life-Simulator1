using System.Collections.Generic;
using Game;

public class SimOutdoorCampingActivity
{
	private sealed class _003C_003Ec__DisplayClass48_0
	{
		public object blacklistedList;

		internal bool _003CRemoveBlacklistedGuestsFrom_003Eb__0(object item)
		{
			return false;
		}
	}

	private readonly SimOutdoor _simOutdoor;

	private readonly List<object> _blacklistedGuests;

	private SimOutdoorDestinationDataCache Destination__BackingField;

	private List<SimOutdoorPlantDataCache> PlantsDiscovered__BackingField;

	private List<SimOutdoorAnimalDataCache> AnimalsDiscovered__BackingField;

	public readonly SimOutdoorCampingNarratives Narratives;

	private object SimPersonOrPetGuest__BackingField;

	private float Enjoyment__BackingField;

	public SimOutdoorDestinationDataCache Destination
	{
		get
		{
			return Destination__BackingField;
		}
		set
		{
			Destination__BackingField = value;
		}
	}

	public List<SimOutdoorPlantDataCache> PlantsDiscovered
	{
		get
		{
			return PlantsDiscovered__BackingField;
		}
		private set
		{
			PlantsDiscovered__BackingField = value;
		}
	}

	public List<SimOutdoorAnimalDataCache> AnimalsDiscovered
	{
		get
		{
			return AnimalsDiscovered__BackingField;
		}
		private set
		{
			AnimalsDiscovered__BackingField = value;
		}
	}

	public object SimPersonOrPetGuest
	{
		get
		{
			return SimPersonOrPetGuest__BackingField;
		}
		set
		{
			SimPersonOrPetGuest__BackingField = value;
		}
	}

	public SimOutdoor OutdoorInstance => null;

	public SimPerson Person => null;

	public SimPet Pet => null;

	public SimLover Lover => null;

	public float RelationshipStrength => 0f;

	public float Enjoyment
	{
		get
		{
			return Enjoyment__BackingField;
		}
		private set
		{
			Enjoyment__BackingField = value;
		}
	}

	public SimOutdoorCampingActivity(SimOutdoor outdoor, SimOutdoorDestinationDataCache destination)
	{
	}

	public void ImpactEnjoyment(int enjoymentScore)
	{
	}

	public void ImpactGuestRelationship(int relationshipScore)
	{
	}

	public string GuestPronoun(PRONOUN_TYPE pronounType)
	{
		return null;
	}

	public bool HasGuest()
	{
		return false;
	}

	public string GetGuestType()
	{
		return null;
	}

	public bool GuestIsPet()
	{
		return false;
	}

	public bool GuestIsPerson()
	{
		return false;
	}

	public bool GuestIsLover()
	{
		return false;
	}

	public bool HaveMetCampingQuotaForTheYear()
	{
		return false;
	}

	public string GetDestinationDescription()
	{
		return null;
	}

	public string GetGuestDescription(bool addComma = true)
	{
		return null;
	}

	public string GetGuestFirstName()
	{
		return null;
	}

	public string GetGuestName()
	{
		return null;
	}

	public void BlacklistGuest(object blacklistedGuest)
	{
	}

	public void RemoveBlacklistedGuestsFrom(List<object> guests)
	{
	}

	public bool HasHandbookEntry(string species, OUTDOOR_HANDBOOK_ENTRY_TYPE entryType)
	{
		return false;
	}

	public void AddAnimalEntry(DataOutdoorAnimal animal)
	{
	}

	public void AddPlantEntry(DataOutdoorPlant plant)
	{
	}

	public Scenario CreateTripItinerary()
	{
		return null;
	}

	public Scenario KillHero(SimOutdoorAnimal animal)
	{
		return null;
	}

	public Scenario KillHero(SimOutdoorPlant plant)
	{
		return null;
	}

	public Scenario KillGuest(SimOutdoorAnimal animal)
	{
		return null;
	}

	public Scenario KillGuest(SimOutdoorPlant plant)
	{
		return null;
	}

	public bool DiscoverPlantIfApplicable(DataOutdoorPlant plant, out Scenario scenario)
	{
		scenario = null;
		return false;
	}

	public bool DiscoverAnimalIfApplicable(DataOutdoorAnimal animal, out Scenario scenario)
	{
		scenario = null;
		return false;
	}

	private Scenario GetRandomStandardScenario()
	{
		return null;
	}

	private Scenario GetRandomSoloEnabledScenario()
	{
		return null;
	}

	private Scenario GetRandomDogEnabledScenario()
	{
		return null;
	}

	private Scenario GetRandomLoverEnabledScenario()
	{
		return null;
	}

	public SimOutdoorPlant GeneratePlant()
	{
		return null;
	}
}
