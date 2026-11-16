using System.Collections.Generic;

public class ScenarioRacingBettingMiniGame : Scenario
{
	private string _narrative;

	private MiniGameDataCarRace _gameData;

	private List<SimRaceCarDriver> _drivers;

	private readonly SimRaceCarDriver _pickedDriver;

	private readonly SimRaceCarDriver _recommendedWinningDriver;

	private readonly string _trackName;

	private RACING_TRACK_CATEGORY _trackCategory;

	private readonly float _betAmount;

	private readonly float _oddsOfWinning;

	public ScenarioRacingBettingMiniGame(List<SimRaceCarDriver> drivers, SimRaceCarDriver pickedDriver, SimRaceCarDriver recommendedWinningDriver, string trackName, RACING_TRACK_CATEGORY trackCategory, float betAmount, float oddsOfWinning)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void MiniGameCompleted()
	{
	}
}
