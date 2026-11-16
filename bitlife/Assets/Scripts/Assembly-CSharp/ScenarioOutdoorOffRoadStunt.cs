using Game;

public class ScenarioOutdoorOffRoadStunt : Scenario
{
	private readonly SimOutdoorOffRoadingActivity _offRoadingActivity;

	private DataOutdoorOffRoadingTrick _trickPerformed;

	private bool _stuntSuccessful;

	private bool _death;

	private bool _passengerBacksOut;

	private bool _passengerStuntSuccessful;

	private bool _passengerDied;

	private string _trickString;

	private BracketParser _bracketParser;

	private string _narrative;

	public ScenarioOutdoorOffRoadStunt(SimOutdoorOffRoadingActivity offRoadingActivity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void PerformStunt()
	{
	}

	private void StuntSuccessful()
	{
	}

	private void StuntFailed()
	{
	}

	private void KillHero()
	{
	}

	private void KillPassenger()
	{
	}

	private void RefuseStunt()
	{
	}

	private void LetPassengerPerformStunt()
	{
	}

	private void PassengerStuntSuccessful()
	{
	}

	private void PassengerStuntFailed()
	{
	}

	private void PassengerBacksOut()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private string RelationshipString()
	{
		return null;
	}
}
