using Game;

public class ScenarioCelebrityDatingAppResult : Scenario
{
	protected SimCelebrityLover _loverCandidate;

	protected int _cachedAgeSelectedIndexNI;

	protected int _cachedDesiredGenderSelectedIndexNI;

	protected int _cachedCelebrityTypeSelectedIndexNI;

	protected string _celebType;

	public ScenarioCelebrityDatingAppResult(SimCelebrityLover theLover, int theAgeIndex, int theGenderIndex, int theCelebrityTypeIndex)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
