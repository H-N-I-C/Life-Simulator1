using System.Collections.Generic;

public class ScenarioRacingRaceMiniGame : Scenario
{
	private MiniGameDataCarRace _gameData;

	private SimRacing _racing;

	private readonly SimRaceCar _heroCar;

	private readonly string _trackName;

	private readonly float _prizeMoney;

	private readonly string _raceSponsor;

	private readonly List<SimRaceCarDriver> _competitors;

	private List<MiniGameDataCarRaceResult> RaceResults => null;

	public ScenarioRacingRaceMiniGame(SimRaceCar heroCar, string trackName, float prizeMoney, string raceSponsor, List<SimRaceCarDriver> competitors)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void MiniGameCompleted()
	{
	}

	private bool _003CRefreshScenario_003Eb__10_0(SimRacingTrack t)
	{
		return false;
	}

	private bool _003CMiniGameCompleted_003Eb__11_0(SimRacingTrack t)
	{
		return false;
	}
}
