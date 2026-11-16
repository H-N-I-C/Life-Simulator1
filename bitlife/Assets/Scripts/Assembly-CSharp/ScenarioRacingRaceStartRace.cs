using System.Collections.Generic;

public class ScenarioRacingRaceStartRace : Scenario
{
	private readonly SimRaceCar _car;

	private readonly SimRacingTrack _track;

	private readonly float _prize;

	private readonly List<SimRaceCarDriver> _competitors;

	private readonly SimRacing _racing;

	private readonly string _sponsorName;

	private string _narrative;

	public ScenarioRacingRaceStartRace(SimRacing racing, SimRaceCar car, SimRacingTrack track, float prize, List<SimRaceCarDriver> competitors, string sponsorName)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void ShowGameRules()
	{
	}

	private void StartRace()
	{
	}
}
