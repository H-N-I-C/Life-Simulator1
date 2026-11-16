using System.Collections.Generic;

public class ScenarioRacingCupSeriesRaceMiniGame : Scenario
{
	private MiniGameDataCarRace _gameData;

	private SimRacing _racing;

	private readonly SimRaceCar _heroCar;

	private readonly string _trackName;

	private readonly List<SimRaceCarDriver> _competitors;

	private readonly RacingCupSeriesActiveRace _activeRace;

	public ScenarioRacingCupSeriesRaceMiniGame(SimRaceCar heroCar, string trackName, List<SimRaceCarDriver> competitors)
	{
	}

	public ScenarioRacingCupSeriesRaceMiniGame(RacingCupSeriesActiveRace race)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void MiniGameCompleted()
	{
	}

	private bool _003CRefreshScenario_003Eb__8_0(SimRacingTrack t)
	{
		return false;
	}
}
