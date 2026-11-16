public class ScenarioEyeExamOutcome : Scenario
{
	public enum Result
	{
		Passed = 0,
		Glasses = 1,
		Contacts = 2
	}

	private Result _result;

	protected string _narrative;

	public ScenarioEyeExamOutcome(Result result)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void UpdateHero(Result result, int happyImpact)
	{
	}

	public override void DismissSelected()
	{
	}
}
