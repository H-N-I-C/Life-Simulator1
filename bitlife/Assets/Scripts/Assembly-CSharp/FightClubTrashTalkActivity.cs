public class FightClubTrashTalkActivity : ActivityLazyCsvBase
{
	private enum ScenarioId
	{
		NONE = 0,
		FC_TRASH_TALK_OUTBOUND = 1,
		FC_TRASH_TALK_INBOUND = 2,
		FC_TRASH_TALK_ENRAGED_OUTBOUND = 3,
		FC_TRASH_TALK_ASHAMED_OUTBOUND = 4,
		FC_TRASH_TALK_ENRAGED_INBOUND = 5,
		FC_TRASH_TALK_ASHAMED_INBOUND = 6
	}

	private SimFightClubFighter Fighter__BackingField;

	private SimHero _hero;

	private ScenarioId _outcome;

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

	public FightClubTrashTalkActivity(Life life, SimFightClubFighter fighter)
		: base(null)
	{
	}

	private void SelectRandomOutcome()
	{
	}

	public void ApplyImpacts(string scenarioId)
	{
	}

	public void TryPushbackEndScenarios(string scenarioId)
	{
	}
}
