public class ScenarioMusicConcert : Scenario
{
	protected SimSpecialJobMusician _specialJob;

	protected string _venue;

	protected DataMusicConcertVenue _venueData;

	protected int _capacity;

	protected string _city;

	protected DataCountry _cityCountry;

	protected bool _isSurpriseGoodVenue;

	protected bool _isSurpriseBadVenue;

	protected string _narrative;

	public ScenarioMusicConcert(SimSpecialJobMusician theSpecialJob)
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
