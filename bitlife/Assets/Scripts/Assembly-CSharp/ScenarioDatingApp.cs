using System.Collections;

public class ScenarioDatingApp : Scenario
{
	protected float _cost;

	protected int _cachedAgeSelectedIndexNI;

	protected int _cachedNetWorthSelectedIndexNI;

	public ScenarioDatingApp()
	{
	}

	public ScenarioDatingApp(int theAgeIndex, int theNetWorthIndex)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public ArrayList GetAgeRangeArray()
	{
		return null;
	}

	public ArrayList GetNetWorthRangeArray()
	{
		return null;
	}

	public SimLover GetLoverCandidateWithMinAgeAndMaxAgeAndMinNetWorth(int ageMin, int ageMax, int netWorthMin)
	{
		return null;
	}
}
