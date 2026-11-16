public class ScenarioFameFeud : Scenario
{
	protected string _narrative;

	protected string _insulteeName;

	protected string _insulteeTitle;

	protected int _insulteeGender;

	public override void RefreshScenario()
	{
	}

	public static string GetRandomMedium()
	{
		return null;
	}

	public static string GetRandomInsultModifier()
	{
		return null;
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
