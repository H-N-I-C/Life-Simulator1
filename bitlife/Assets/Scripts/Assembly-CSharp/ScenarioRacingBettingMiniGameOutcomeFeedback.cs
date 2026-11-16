public class ScenarioRacingBettingMiniGameOutcomeFeedback : Scenario
{
	private string _narrative;

	private readonly SimRaceCarDriver _pickedDriver;

	private readonly float _betAmount;

	private readonly float _oddsOfWinning;

	private readonly SimRaceCarDriver _winningDriver;

	public ScenarioRacingBettingMiniGameOutcomeFeedback(SimRaceCarDriver pickedDriver, SimRaceCarDriver winningDriver, float betAmount, float oddsOfWinning)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
