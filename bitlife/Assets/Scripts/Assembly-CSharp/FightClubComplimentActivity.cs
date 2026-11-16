public class FightClubComplimentActivity : ActivityLazyCsvBase
{
	private SimFightClubFighter Fighter__BackingField;

	private SimHero _hero;

	private FightClubInteractionBuff _interactionBuff;

	public SimFightClubFighter Fighter
	{
		get
		{
			return Fighter__BackingField;
		}
		set
		{
			Fighter__BackingField = value;
		}
	}

	public FightClubComplimentActivity(Life life, SimFightClubFighter fighter)
		: base(null)
	{
	}

	private void RefreshOutcome()
	{
	}

	private void ApplyRelationshipImpact()
	{
	}

	public void SetCurrentScenario(Scenario scenario)
	{
	}
}
