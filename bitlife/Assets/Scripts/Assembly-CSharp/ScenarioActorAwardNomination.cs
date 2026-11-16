public class ScenarioActorAwardNomination : Scenario
{
	private SimActorFilmographyFilm _filmographyFilm;

	private SimActorFilmographyShowSeason _filmographyShowSeason;

	private int _awardType;

	private int _awardOutcome;

	protected string _narrative;

	public ScenarioActorAwardNomination(SimActorFilmographyFilm theFilmographyFilm)
	{
	}

	public ScenarioActorAwardNomination(SimActorFilmographyShowSeason theFilmographyShowSeason)
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
