using Game;

public class ScenarioCultCommuneCeremonyCommunalFastCommunard : Scenario
{
	private string _narrative;

	private SimCult _cult;

	private SimCommunard _communard;

	private int _fastDuration;

	private DataCultFastReason _reason;

	private int _numUnconscious;

	private int _numberDied;

	private string _causeSnippet;

	private DataCultFastHungerFood _food;

	public ScenarioCultCommuneCeremonyCommunalFastCommunard(SimCommunard communard, int fastDuration, DataCultFastReason reason, int numUnconscious, int numberDied)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private void EmptyBellyFeedbackWithPossibleDeath()
	{
	}
}
