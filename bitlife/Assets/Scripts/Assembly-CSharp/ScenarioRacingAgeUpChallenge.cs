public class ScenarioRacingAgeUpChallenge : Scenario
{
	private readonly SimRaceCarDriver _rival;

	private readonly SimRaceCar _car;

	private readonly RACING_TRACK_CATEGORY _category;

	private readonly SimRacing _racing;

	private SimRacingTrack _randomTrack;

	private string _heroInsultString;

	private string _narrative;

	public ScenarioRacingAgeUpChallenge(SimRaceCar theCar, SimRaceCarDriver rival, SimRacing racing)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
