public class ScenarioActorAwardGeneric : Scenario
{
	private SimActorFilmographyFilm _filmographyFilm;

	private SimActorFilmographyShowSeason _filmographyShowSeason;

	private int _awardType;

	private int _awardCategory;

	private int _awardOutcome;

	protected string _narrative;

	public ScenarioActorAwardGeneric(SimActorFilmographyFilm theFilmographyFilm)
	{
	}

	public ScenarioActorAwardGeneric(SimActorFilmographyShowSeason theFilmographyShowSeason)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
