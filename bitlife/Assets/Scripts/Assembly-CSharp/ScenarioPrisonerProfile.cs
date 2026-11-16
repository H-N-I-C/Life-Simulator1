public class ScenarioPrisonerProfile : Scenario
{
	private readonly SimOccupationPrisoner _heroPrisonerOccupation;

	private readonly SimPerson _hero;

	private readonly SimPrisoner _fellowPrisoner;

	private string _Narrative;

	private string _prisonPlace;

	public ScenarioPrisonerProfile()
	{
	}

	public ScenarioPrisonerProfile(SimPrisoner thePrisoner)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void VampireFlow(string choice, bool anyRetaliation)
	{
	}

	private void PrisonerTurnHero(bool anyRetaliation)
	{
	}

	private void TurnPrisoner(bool anyRetaliation)
	{
	}

	private void DrainBlood(bool anyRetaliation)
	{
	}

	private void DrinkBlood(bool anyRetaliation)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
