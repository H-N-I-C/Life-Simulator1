using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioOutdoorHiking : Scenario
{
	private sealed class _003C_003Ec__DisplayClass8_0
	{
		public string selectCompanion;

		public ScenarioOutdoorHiking _003C_003E4__this;

		public string selectDestination;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimPerson t)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__1(SimPet t)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__2(DataOutdoorDestination t)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly SimOutdoor _outdoor;

	private List<SimPerson> _companions;

	private List<SimPet> _pets;

	private OutdoorHikingActivityData _activityData;

	public ScenarioOutdoorHiking(SimOutdoor outdoor)
	{
	}

	public ScenarioOutdoorHiking(SimOutdoor outdoor, List<SimPerson> companions, OutdoorHikingActivityData activityData)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
