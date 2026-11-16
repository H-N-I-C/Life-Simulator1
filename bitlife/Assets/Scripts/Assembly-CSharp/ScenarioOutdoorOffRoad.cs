using System;
using System.Collections;

public class ScenarioOutdoorOffRoad : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRecreationalVehicle, bool> _003C_003E9__12_0;

		public static Func<SimRecreationalVehicle, int> _003C_003E9__12_1;

		public static Func<SimRecreationalVehicle, string> _003C_003E9__12_2;

		public static Func<SimPerson, bool> _003C_003E9__14_0;

		public static Func<SimPerson, bool> _003C_003E9__14_1;

		internal bool _003CCreateVehicleList_003Eb__12_0(SimRecreationalVehicle x)
		{
			return false;
		}

		internal int _003CCreateVehicleList_003Eb__12_1(SimRecreationalVehicle x)
		{
			return 0;
		}

		internal string _003CCreateVehicleList_003Eb__12_2(SimRecreationalVehicle x)
		{
			return null;
		}

		internal bool _003CGetPersonsList_003Eb__14_0(SimPerson x)
		{
			return false;
		}

		internal bool _003CGetPersonsList_003Eb__14_1(SimPerson x)
		{
			return false;
		}
	}

	private readonly SimRecreationalVehicle _optionalOffRoadVehicle;

	private readonly SimOutdoorOffRoadingActivity _optionalPreviousActivity;

	private string _narrative;

	public ScenarioOutdoorOffRoad()
	{
	}

	public ScenarioOutdoorOffRoad(SimRecreationalVehicle withVehicle)
	{
	}

	public ScenarioOutdoorOffRoad(SimOutdoorOffRoadingActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private ScenarioDropDown FindDropDownByTitle(ArrayList dropDowns, string title)
	{
		return null;
	}

	private SimRecreationalVehicle GetChosenVehicle(ArrayList dropDownStrings)
	{
		return null;
	}

	private string GetLocation(ArrayList dropDownStrings)
	{
		return null;
	}

	private SimPerson GetPassenger(ArrayList dropDownStrings)
	{
		return null;
	}

	private ArrayList CreateVehicleList()
	{
		return null;
	}

	private ArrayList CreatePassengerList()
	{
		return null;
	}

	private ArrayList GetPersonsList()
	{
		return null;
	}

	private string CreateBody()
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

	public override string EmojiString()
	{
		return null;
	}
}
