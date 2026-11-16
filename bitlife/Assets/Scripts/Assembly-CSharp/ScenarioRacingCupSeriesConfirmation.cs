using System.Collections.Generic;

public class ScenarioRacingCupSeriesConfirmation : Scenario
{
	private readonly SimRacing _racing;

	private readonly SimRacingCup _racingCup;

	private readonly SimRaceCar _car;

	private readonly List<SimRaceCarDriver> _opponentDrivers;

	private readonly RacingCupSeriesRegistration _raceCupRegistration;

	private string _narrative;

	public ScenarioRacingCupSeriesConfirmation(RacingCupSeriesRegistration raceCupRegistration)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
