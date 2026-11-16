using System.Collections.Generic;
using Game;

public class ScenarioRacingRacePolicePivotalMoments : Scenario
{
	private readonly SimRacing _racing;

	private readonly SimRaceCar _car;

	private readonly SimRacingTrack _track;

	private readonly float _prize;

	private readonly List<SimRaceCarDriver> _competitors;

	private readonly string _sponsorName;

	private readonly int _racePerformance;

	private BracketParser _parser;

	private DataRacingStreetRaceCopPivotalMoment _data;

	private string _narrative;

	public ScenarioRacingRacePolicePivotalMoments(SimRacing racing, SimRaceCar car, SimRacingTrack track, float prize, List<SimRaceCarDriver> competitors, string sponsorName, int racePerformance)
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
