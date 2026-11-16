using System.Collections.Generic;

public class ScenarioRacingRaceConfirmCar : Scenario
{
	private readonly SimRaceCar _car;

	private readonly SimRacingTrack _track;

	private readonly float _prize;

	private readonly List<SimRaceCarDriver> _competitors;

	private readonly SimRacing _racing;

	private readonly string _sponsorName;

	public ScenarioRacingRaceConfirmCar(SimRacing racing, SimRaceCar car, SimRacingTrack track, float prize, List<SimRaceCarDriver> competitors, string sponsorName)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void ChooseAnotherCar()
	{
	}

	private void AcceptRace()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
