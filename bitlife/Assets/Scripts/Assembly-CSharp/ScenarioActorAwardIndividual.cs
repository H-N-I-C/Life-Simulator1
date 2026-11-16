public class ScenarioActorAwardIndividual : Scenario
{
	private SimActorFilmographyFilm _filmographyFilm;

	private SimActorFilmographyShowSeason _filmographyShowSeason;

	private bool _didAttendInPerson;

	protected string _narrative;

	public ScenarioActorAwardIndividual(SimActorFilmographyFilm theFilmographyFilm, bool theDidAttendInPerson)
	{
	}

	public ScenarioActorAwardIndividual(SimActorFilmographyShowSeason theFilmographyShowSeason, bool theDidAttendInPerson)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
