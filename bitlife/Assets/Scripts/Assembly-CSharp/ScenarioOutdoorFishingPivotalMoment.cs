using Game;

public class ScenarioOutdoorFishingPivotalMoment : Scenario
{
	private readonly SimOutdoorFishingTrip _fishingTrip;

	private DataOutdoorLureFishColorString _lureFishColor;

	private DataOutdoorCatchableFish _possibleCatch;

	public ScenarioOutdoorFishingPivotalMoment(SimOutdoorFishingTrip trip)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void HookedAFish()
	{
	}

	private void DidNotCatchAFish()
	{
	}

	private bool KeepWaiting()
	{
		return false;
	}

	private bool ReelItInALittle()
	{
		return false;
	}

	private bool JerkMyRod()
	{
		return false;
	}

	private string GetBodyString(DataOutdoorLureFishColorString lureFishColor)
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
