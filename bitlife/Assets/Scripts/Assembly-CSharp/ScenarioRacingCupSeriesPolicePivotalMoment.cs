using Game;

public class ScenarioRacingCupSeriesPolicePivotalMoment : ScenarioRacingCupSeriesPivotalMoment
{
	private readonly RacingCupSeriesActiveRace _activeRace;

	private BracketParser _parser;

	private DataRacingStreetRaceCopPivotalMoment _data;

	private string _narrative;

	public ScenarioRacingCupSeriesPolicePivotalMoment(RacingCupSeriesActiveRace race)
		: base((RacingCupSeriesActiveRace)null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void ProcessOutcome(int idx)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
