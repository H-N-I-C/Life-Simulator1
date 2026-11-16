using System.Collections;
using System.Collections.Generic;

public class ScenarioCultCommuneOutreachMission : Scenario
{
	private sealed class _003C_003Ec__DisplayClass13_0
	{
		public string destinationDropDownString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimPlace x)
		{
			return false;
		}
	}

	private string _narrative;

	private SimPlace _destination;

	private int _travelClass;

	private string _recruitmentMessage;

	private int _participants;

	private bool _refreshLists;

	private static ArrayList _destinationDropDownArray;

	private static ArrayList _classDropDownArray;

	private static ArrayList _messageDropDownArray;

	private static List<SimPlace> _destinationList;

	public ScenarioCultCommuneOutreachMission()
	{
	}

	public ScenarioCultCommuneOutreachMission(SimPlace destination, int travelClass, string recruitmentMessage, int participants)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}
}
