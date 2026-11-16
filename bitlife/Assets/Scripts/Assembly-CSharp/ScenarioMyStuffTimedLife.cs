using Backend;

public class ScenarioMyStuffTimedLife : Scenario
{
	public enum Source
	{
		Default = 0,
		StartOnly = 1
	}

	private readonly StuffItem _item;

	private readonly Source _scenarioSource;

	private const string SpecialLifeInfo = "Each Special Life comes with their own distinct perks and personality. Play while you can, before time runs out!";

	public ScenarioMyStuffTimedLife(StuffItem item, Source scenarioSource = Source.Default)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void _003CChoiceSelectedAtIndex_003Eb__6_0(bool success, string errorMessage)
	{
	}
}
