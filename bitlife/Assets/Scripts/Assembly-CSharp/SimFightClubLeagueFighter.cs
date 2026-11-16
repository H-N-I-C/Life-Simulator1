using System;

[Serializable]
public class SimFightClubLeagueFighter : SimFightClubFighter
{
	private int LastYearsLeaguePosition__BackingField;

	private int HeroAgeAtLastLeagueFightWithHero__BackingField;

	public int LastYearsLeaguePosition
	{
		get
		{
			return LastYearsLeaguePosition__BackingField;
		}
		set
		{
			LastYearsLeaguePosition__BackingField = value;
		}
	}

	public int HeroAgeAtLastLeagueFightWithHero
	{
		get
		{
			return HeroAgeAtLastLeagueFightWithHero__BackingField;
		}
		set
		{
			HeroAgeAtLastLeagueFightWithHero__BackingField = value;
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

	public SimFightClubLeagueFighter(bool initHere, FIGHT_CLUB_FIGHTING_STYLE fightingStyle, int leaguePosition, int gender)
		: base(initHere: false, FIGHT_CLUB_FIGHTING_STYLE.NONE, 0)
	{
	}

	public override void IncrementAge()
	{
	}

	public int GetLeaguePosition()
	{
		return 0;
	}

	public void SetLeaguePosition(int newPosition)
	{
	}

	public override void LevelUp()
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

	public override bool WillAcceptHookup()
	{
		return false;
	}

	public override void Die()
	{
	}

	public override Scenario TryGetPostDeathScenario()
	{
		return null;
	}
}
