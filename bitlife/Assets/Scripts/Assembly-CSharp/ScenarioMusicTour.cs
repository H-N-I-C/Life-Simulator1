using System.Collections;

public class ScenarioMusicTour : Scenario
{
	protected SimSpecialJobMusician _specialJob;

	protected float _marketingScore;

	protected ArrayList _cityArray;

	protected ArrayList _countryArray;

	protected string _citiesString;

	protected int _numWeeks;

	protected string _narrative;

	public ScenarioMusicTour(SimSpecialJobMusician theSpecialJob)
	{
	}

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
}
