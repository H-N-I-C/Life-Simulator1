public class ScenarioVampireRequestConversionFeedback : Scenario
{
	private string _narrative;

	private readonly SimPerson _vampire;

	private readonly VampireHeroConverter _heroConverter;

	private readonly string _reason;

	public ScenarioVampireRequestConversionFeedback(SimPerson vampire, string reason)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void Closed()
	{
	}

	private string GetFeedbackString()
	{
		return null;
	}

	public override void DismissSelected()
	{
	}

	private bool _003CGetFeedbackString_003Eb__7_0(SimLover t)
	{
		return false;
	}

	private bool _003CGetFeedbackString_003Eb__7_1(SimPerson t)
	{
		return false;
	}
}
