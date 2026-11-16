public class FightClubInteractionBuff
{
	public enum BUFF_OUTCOME_TYPE
	{
		OPPONENT_BUFF = 0,
		OPPONENT_DEBUFF = 1,
		HERO_BUFF = 2,
		HERO_DEBUFF = 3
	}

	private BUFF_OUTCOME_TYPE Outcome__BackingField;

	private readonly SimFightClubFighter _fighter;

	private readonly SimFightClubFighterStats _fighterStats;

	private readonly SimFightClubFighterStats _heroStats;

	private readonly FIGHT_CLUB_BUFF_TYPE _debuffType;

	private readonly FIGHT_CLUB_BUFF_TYPE _buffType;

	private readonly Life _life;

	public BUFF_OUTCOME_TYPE Outcome
	{
		get
		{
			return Outcome__BackingField;
		}
		private set
		{
			Outcome__BackingField = value;
		}
	}

	public FightClubInteractionBuff(SimFightClubFighter fighter, Life life, FIGHT_CLUB_BUFF_TYPE debuffType, FIGHT_CLUB_BUFF_TYPE buffType)
	{
	}

	public void AddRandomBuff()
	{
	}

	private void CalculateOutcome(bool isFirstInteraction, bool fighterAlreadyHasBuff, bool heroAlreadyHasBuff)
	{
	}

	private void ApplyBuffs()
	{
	}
}
