public class ScenarioVampireCouncilCrimsonAuditFamiliar : Scenario
{
	private readonly SimPerson _person;

	private readonly SimVampireFamiliar _familiar;

	private BracketParser _parser;

	private const int MinFamiliarAttribute = 70;

	private const int MaxFamiliarAttribute = 100;

	private const int MinFamiliarAttributeCraziness = 5;

	private const int MaxFamiliarAttributeCraziness = 30;

	public ScenarioVampireCouncilCrimsonAuditFamiliar(SimPerson person)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
