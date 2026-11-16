using System.Collections;
using System.Collections.Generic;

public class ScenarioVampireHeroAcquireFamiliar : Scenario
{
	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public SimPerson person;

		internal bool _003CGetPossibleFamiliarsWithUniqueJobs_003Eb__0(SimPerson x)
		{
			return false;
		}
	}

	private string _narrative;

	private List<SimPerson> _persons;

	private Dictionary<string, SimPerson> _personsByCareer;

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

	private List<SimPerson> GetPossibleFamiliarsWithUniqueJobs()
	{
		return null;
	}

	public static string AnOccupationOfPerson(SimPerson person)
	{
		return null;
	}
}
