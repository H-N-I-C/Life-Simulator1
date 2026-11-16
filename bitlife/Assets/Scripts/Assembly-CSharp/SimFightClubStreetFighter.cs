using System;

[Serializable]
public class SimFightClubStreetFighter : SimFightClubFighter
{
	private string Description__BackingField;

	public new string Description
	{
		get
		{
			return Description__BackingField;
		}
		set
		{
			Description__BackingField = value;
		}
	}

	protected override int LevelUpMovesToAddCount()
	{
		return 0;
	}

	protected override bool LevelUpAllowsIllegalMoveAttempts()
	{
		return false;
	}

	protected override int LevelUpStyleAlignedMovePickPercent()
	{
		return 0;
	}

	public SimFightClubStreetFighter(bool initHere, FIGHT_CLUB_FIGHTING_STYLE fightingStyle, int gender, string description)
		: base(initHere: false, FIGHT_CLUB_FIGHTING_STYLE.NONE, 0)
	{
	}

	public override void IncrementAge()
	{
	}

	public override float GetDifficulty()
	{
		return 0f;
	}

	public float GetDifficultyUnclamped()
	{
		return 0f;
	}

	public override void LevelUp()
	{
	}

	public override void Die()
	{
	}

	public override bool WillAcceptHookup()
	{
		return false;
	}
}
