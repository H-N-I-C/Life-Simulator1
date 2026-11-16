public class ScenarioLoveClassmate : Scenario
{
	protected SimOccupationStudent _student;

	protected SimClassmate _classmate;

	protected SimLover _loverCandidate;

	protected bool _targeted;

	protected bool _gettingAskedOut;

	public ScenarioLoveClassmate()
	{
	}

	public ScenarioLoveClassmate(SimClassmate theClassmate, bool initiatedByHero)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static bool HeroGettingAskedOut()
	{
		return false;
	}

	public static SimClassmate GetClassmateLoveInterest()
	{
		return null;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
