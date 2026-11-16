using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioOutdoorCampingPlanTrip : Scenario
{
	private string _narrative;

	private readonly SimOutdoor _outdoorLifestyle;

	private readonly SimOutdoorCampingActivity _previousCampingActivity;

	private List<DataOutdoorDestination> _outdoorDestinations;

	public ScenarioOutdoorCampingPlanTrip(SimOutdoor outdoorLifestyle)
	{
	}

	public ScenarioOutdoorCampingPlanTrip(SimOutdoorCampingActivity previousPlans)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private DataOutdoorDestination FindDestinationDescription(string description)
	{
		return null;
	}

	private object FindPassengerPersonOrPet(string personOrPetDescription)
	{
		return null;
	}

	private string CreateDestinationDescription(DataOutdoorDestination destination)
	{
		return null;
	}

	private List<DataOutdoorDestination> CreateRandomDestinationList()
	{
		return null;
	}

	private ArrayList CreatePassengerDescriptionList()
	{
		return null;
	}

	private string CreatePersonString(SimPerson person)
	{
		return null;
	}

	private string RelationshipStatusString(SimPerson person)
	{
		return null;
	}

	private string CreatePassengerDescription(object personOrPet)
	{
		return null;
	}

	private ArrayList GetPersonsList()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
