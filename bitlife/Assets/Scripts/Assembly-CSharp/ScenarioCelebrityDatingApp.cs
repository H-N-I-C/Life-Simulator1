using System.Collections;
using Game;

public class ScenarioCelebrityDatingApp : Scenario
{
	protected float _cost;

	protected int _cachedAgeSelectedIndexNI;

	protected int _cachedDesiredGenderSelectedIndexNI;

	protected int _cachedCelebrityTypeSelectedIndexNI;

	public ScenarioCelebrityDatingApp()
	{
	}

	public ScenarioCelebrityDatingApp(int theAgeIndex, int theDesiredGenderIndex, int theCelebrityTypeIndex)
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

	public ArrayList GetDesiredGenderArray()
	{
		return null;
	}

	public ArrayList GetDesiredCelebrityTypeArray()
	{
		return null;
	}

	public SimCelebrityLover GetLoverCandidateWithMinAgeAndMaxAgeAndGenderAndCelebrityType(int ageMin, int ageMax, int gender, string celebrityType, int sexuality)
	{
		return null;
	}
}
