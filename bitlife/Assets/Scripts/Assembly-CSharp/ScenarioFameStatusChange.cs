public class ScenarioFameStatusChange : Scenario
{
	protected bool _addingFame;

	protected int _fameType;

	protected bool _removingFame;

	private bool _giveInstantFame;

	public ScenarioFameStatusChange(int theFameType, bool removingFame)
	{
	}

	public ScenarioFameStatusChange(int theFameType, bool removingFame, bool giveInstantFameOnStartJob)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	private string StartJobInstantFameNarrative()
	{
		return null;
	}
}
