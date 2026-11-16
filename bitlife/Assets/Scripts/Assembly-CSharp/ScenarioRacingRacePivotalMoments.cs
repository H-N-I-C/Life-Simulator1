using System.Collections.Generic;
using Game;

public class ScenarioRacingRacePivotalMoments : Scenario
{
	private readonly SimRacing _racing;

	private readonly SimRaceCar _car;

	private readonly SimRacingTrack _track;

	private readonly float _prize;

	private readonly List<SimRaceCarDriver> _competitors;

	private readonly string _sponsorName;

	private readonly HashSet<DataRacingRacePivotalMoment> _previousRacingMoments;

	private BracketParser _parser;

	private DataRacingRacePivotalMoment _data;

	private int _performanceScore;

	private readonly bool _endScenario;

	private string _narrative;

	public ScenarioRacingRacePivotalMoments(SimRacing racing, SimRaceCar car, SimRacingTrack track, float prize, List<SimRaceCarDriver> competitors, string sponsorName)
	{
	}

	public ScenarioRacingRacePivotalMoments(ScenarioRacingRacePivotalMoments other)
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
