using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioOutdoorSpelunking : Scenario
{
	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public string indexDropDown;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataOutdoorCave t)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly SimOutdoor _outdoorLifestyle;

	private List<DataOutdoorCave> _caveList;

	public ScenarioOutdoorSpelunking(SimOutdoor outdoorLifestyle)
	{
	}

	public ScenarioOutdoorSpelunking(SimOutdoor outdoorLifestyle, List<DataOutdoorCave> caveList)
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
