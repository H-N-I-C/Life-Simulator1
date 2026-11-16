public class ScenarioDeathRelative : Scenario
{
	protected SimParent _parent;

	protected SimLover _lover;

	protected SimChild _child;

	protected SimSibling _sibling;

	protected SimPerson _friend;

	protected SimAgent _agent;

	protected SimVampireFamiliar _familiar;

	protected SimPerson _person;

	protected bool _funeralAbroad;

	protected bool _supposedToPlanFuneral;

	private int deathType;

	private SimPlace funeralPlace;

	public ScenarioDeathRelative(SimParent theParent)
	{
	}

	public ScenarioDeathRelative(SimLover theLover)
	{
	}

	public ScenarioDeathRelative(SimChild theChild)
	{
	}

	public ScenarioDeathRelative(SimSibling theSibling)
	{
	}

	public ScenarioDeathRelative(SimPerson theFriend)
	{
	}

	public ScenarioDeathRelative(SimAgent theAgent)
	{
	}

	public ScenarioDeathRelative(SimVampireFamiliar familiar)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void ShowAttendFuneral()
	{
	}

	private void ShowPlanFuneralScenario()
	{
	}

	private void ShowAbroadFuneral()
	{
	}

	private void ShowFamiliarFeedbackScenario()
	{
	}

	private void ShowPrisonFuneralFeedback()
	{
	}

	private void ShowAgentFuneral()
	{
	}

	private void ShowFriendFuneral()
	{
	}

	private void ShowSiblingFuneral()
	{
	}

	private void ShowChildFuneral()
	{
	}

	private void ShowLoverFuneral()
	{
	}

	private void ShowParentFuneral()
	{
	}

	public override void DismissSelected()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public void ImpactHero()
	{
	}

	private bool IsFuneralAbroad()
	{
		return false;
	}

	private bool ShouldPlanFuneral()
	{
		return false;
	}

	private void LogDeath()
	{
	}
}
