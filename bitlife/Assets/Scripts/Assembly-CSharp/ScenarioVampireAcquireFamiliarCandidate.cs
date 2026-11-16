public class ScenarioVampireAcquireFamiliarCandidate : Scenario
{
	private string _narrative;

	private readonly SimHero _hero;

	private readonly SimVampire _heroVampire;

	private SimVampireFamiliar _familiar;

	private int _attempts;

	private string _anOccupation;

	public ScenarioVampireAcquireFamiliarCandidate(SimVampireFamiliar familiar, int attempts = 1)
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

	private SimPerson GeneratePersonMatchingCareer()
	{
		return null;
	}
}
