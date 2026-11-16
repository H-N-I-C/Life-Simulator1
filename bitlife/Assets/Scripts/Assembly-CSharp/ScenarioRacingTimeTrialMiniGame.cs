public class ScenarioRacingTimeTrialMiniGame : Scenario
{
	private MiniGameDataCarRace _gameData;

	private SimRacing _racing;

	private SimRaceCar _heroCar;

	private string _trackName;

	private bool _clockwise;

	private SimRacingTrack _track;

	private float _preRaceCondition;

	public ScenarioRacingTimeTrialMiniGame(SimRaceCar heroCar, string trackName, bool clockwise)
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
