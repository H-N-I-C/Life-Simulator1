public class ScenarioRelationshipMovieTheater : Scenario
{
	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimPerson _friend;

	protected SimPerson _person;

	protected bool _movieAlreadyDeterminedUnavailable;

	protected bool _personAlreadyRejectedHero;

	protected DataMovie _movie;

	protected string _movieTitle;

	protected string _personNameScenario;

	protected string _personNameNarrative;

	protected string _narrative;

	protected bool _requireRewardedVideo;

	public ScenarioRelationshipMovieTheater(SimParent theParent)
	{
	}

	public ScenarioRelationshipMovieTheater(SimSibling theSibling)
	{
	}

	public ScenarioRelationshipMovieTheater(SimLover theLover)
	{
	}

	public ScenarioRelationshipMovieTheater(SimChild theChild)
	{
	}

	public ScenarioRelationshipMovieTheater(SimPerson theFriend)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static bool NormalMovieAvailable()
	{
		return false;
	}

	public static bool ChildrensMovieAvailable()
	{
		return false;
	}

	public bool RequireRewardedVideoToGet()
	{
		return false;
	}
}
