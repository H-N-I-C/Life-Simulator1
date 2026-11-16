public class ScenarioOutdoorHikingEncounterAnimal : Scenario
{
	private readonly OutdoorHikingActivityData _hikingActivityData;

	private string _narrative;

	private readonly SimOutdoor _outdoor;

	private SimOutdoorAnimal _animal;

	public ScenarioOutdoorHikingEncounterAnimal(SimOutdoor outdoor, OutdoorHikingActivityData hikingActivityData)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void TryFeedAnimal()
	{
	}

	private void FeedAnimal(string treatString)
	{
	}

	private void CallDogBack()
	{
	}

	private void PetAnimal()
	{
	}

	private void ThrowFoodToAnimal()
	{
	}

	private void LetDogInvestigate()
	{
	}

	private (bool, bool, bool) GetDogAttackedChecks()
	{
		return default;
	}

	private (bool, bool, bool) GetAttackedChecks()
	{
		return default;
	}

	private void TakeSelfieSolo()
	{
	}

	private void MissedAttackFeedback()
	{
	}

	private void GetAttackedFeedback()
	{
	}

	private void PushNextScenario()
	{
	}

	private void PushDiscoveryScenario()
	{
	}

	private void TakeSelfieWithPerson()
	{
	}
}
