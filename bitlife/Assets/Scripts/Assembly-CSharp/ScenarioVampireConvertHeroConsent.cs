public class ScenarioVampireConvertHeroConsent : Scenario
{
	private string _narrative;

	private readonly SimPerson _vampire;

	private readonly string _description;

	private readonly VampireHeroConverter _heroConverter;

	private readonly string _reason;

	public ScenarioVampireConvertHeroConsent(SimPerson vampire, string description, string reason)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override void Closed()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
