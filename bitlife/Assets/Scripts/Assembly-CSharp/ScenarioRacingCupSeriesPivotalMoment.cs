using System.Collections.Generic;
using Game;

public class ScenarioRacingCupSeriesPivotalMoment : Scenario
{
	private readonly RacingCupSeriesActiveRace _activeRace;

	private readonly SimRacingCup _cup;

	private readonly HashSet<DataRacingRacePivotalMoment> _previousMomentsShown;

	private readonly SimRacing _racing;

	private readonly SimRaceCar _car;

	private readonly SimRacingTrack _track;

	private readonly float _prize;

	private readonly List<SimRaceCarDriver> _competitors;

	private readonly string _sponsorName;

	private BracketParser _parser;

	private DataRacingRacePivotalMoment _data;

	private int _performanceScore;

	private readonly bool _endScenario;

	private string _narrative;

	public ScenarioRacingCupSeriesPivotalMoment(RacingCupSeriesActiveRace race)
	{
	}

	public ScenarioRacingCupSeriesPivotalMoment(ScenarioRacingCupSeriesPivotalMoment other)
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

	private void ProcessEndScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
