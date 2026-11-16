public class ScenarioChildTuition : Scenario
{
	protected float _tuitionCost;

	protected SimChild _child;

	protected bool _canArgue;

	public ScenarioChildTuition(bool allowArgue, SimChild theChild)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}

	public static SimChild GetCollegeBoundChild()
	{
		return null;
	}
}
