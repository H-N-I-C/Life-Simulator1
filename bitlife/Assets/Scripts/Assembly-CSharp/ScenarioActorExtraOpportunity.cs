public class ScenarioActorExtraOpportunity : Scenario
{
	protected SimActorFilm _film;

	protected SimActorShow _show;

	protected string _extraRoleString;

	protected float _extraCompensation;

	public ScenarioActorExtraOpportunity(SimActorFilm theFilm)
	{
	}

	public ScenarioActorExtraOpportunity(SimActorShow theShow)
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
