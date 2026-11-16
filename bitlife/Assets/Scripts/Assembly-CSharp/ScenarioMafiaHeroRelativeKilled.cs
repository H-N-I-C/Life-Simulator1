public class ScenarioMafiaHeroRelativeKilled : Scenario
{
	protected SimSpecialJobMafia _specialJob;

	protected SimPerson _person;

	protected bool _isRivalFamily;

	protected SimParent _parent;

	protected SimSibling _sibling;

	protected SimLover _lover;

	protected SimChild _child;

	protected string _narrative;

	public ScenarioMafiaHeroRelativeKilled()
	{
	}

	public ScenarioMafiaHeroRelativeKilled(SimSpecialJobMafia theSpecialJob, SimPerson thePerson, bool isRival)
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

	public override string EmojiString()
	{
		return null;
	}
}
