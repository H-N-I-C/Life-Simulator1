public class ScenarioRacingDriverCashOrPinkSlipRaceMiniGame : Scenario
{
	private string _narrative;

	private SimRacing _racing;

	private SimRaceCarDriver _opponentDriver;

	private SimRaceCar _heroCar;

	private float _cashAmount;

	private bool _pinkSlipRace;

	private MiniGameDataCarRace _gameData;

	private string _trackName;

	public ScenarioRacingDriverCashOrPinkSlipRaceMiniGame(SimRaceCarDriver opponentDriver, SimRaceCar heroCar, string trackName, float cashAmount = 0f)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void MiniGameCompleted()
	{
	}

	private bool _003CRefreshScenario_003Eb__9_0(SimRacingTrack t)
	{
		return false;
	}
}
