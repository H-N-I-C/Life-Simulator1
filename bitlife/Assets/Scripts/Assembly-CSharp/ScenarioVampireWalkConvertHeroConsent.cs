public class ScenarioVampireWalkConvertHeroConsent : Scenario
{
	private string _narrative;

	private readonly SimPerson _vampire;

	private string _walkString;

	private readonly float _enjoyment;

	private readonly VampireHeroConverter _heroConverter;

	public ScenarioVampireWalkConvertHeroConsent(string walkString, float enjoyment)
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
