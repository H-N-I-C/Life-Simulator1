using System.Collections;

public class ScenarioDatingAppGay : Scenario
{
	protected float _cost;

	protected int _cachedAgeSelectedIndexNI;

	protected int _cachedGenderSelectedIndexNI;

	public ScenarioDatingAppGay()
	{
	}

	public ScenarioDatingAppGay(int theAgeIndex, int theGenderIndex)
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

	public ArrayList GetGenderArray()
	{
		return null;
	}

	public SimLover GetLoverCandidateWithMinAgeAndMaxAgeAndGenderAndGenderIdentity(int ageMin, int ageMax, int gender, int genderIdentity)
	{
		return null;
	}
}
