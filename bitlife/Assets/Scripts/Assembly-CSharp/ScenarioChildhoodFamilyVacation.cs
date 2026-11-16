public class ScenarioChildhoodFamilyVacation : Scenario
{
	protected DataVacationDestination _vacationDestination;

	protected bool _isRoadTrip;

	protected string _vacationDescriptionString;

	protected string _parentsString;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
