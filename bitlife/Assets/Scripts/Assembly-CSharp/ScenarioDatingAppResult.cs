public class ScenarioDatingAppResult : Scenario
{
	protected SimLover _loverCandidate;

	protected int _cachedAgeSelectedIndexNI;

	protected int _cachedNetWorthSelectedIndexNI;

	protected int _cachedGenderSelectedIndexNI;

	protected bool _usedGayDatingApp;

	public ScenarioDatingAppResult(SimLover theLover, int theAgeIndex, int theNetWorthIndex)
	{
	}

	public ScenarioDatingAppResult(SimLover theLover, int theAgeIndex, int theGenderIndex, bool gender = false)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
