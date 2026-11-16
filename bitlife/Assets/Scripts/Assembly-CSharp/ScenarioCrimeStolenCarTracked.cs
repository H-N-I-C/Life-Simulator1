public class ScenarioCrimeStolenCarTracked : Scenario
{
	protected SimCar _car;

	public ScenarioCrimeStolenCarTracked()
	{
	}

	public ScenarioCrimeStolenCarTracked(SimCar theCar)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public static bool DoesHeroOwnStolenCar()
	{
		return false;
	}

	public static SimCar GetStolenCarOwnedByHero()
	{
		return null;
	}
}
