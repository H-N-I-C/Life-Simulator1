using System;
using System.Collections.Generic;
using Game;

[Serializable]
public abstract class SimFightClubFighter : SimLover
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimFightClubMove, DataFightIngMove> _003C_003E9__30_0;

		public static Func<SimFightClubMove, int> _003C_003E9__35_0;

		internal DataFightIngMove _003CAddLearnedMovesOnLevelUp_003Eb__30_0(SimFightClubMove m)
		{
			return null;
		}

		internal int _003CRefreshMoveSet_003Eb__35_0(SimFightClubMove x)
		{
			return 0;
		}
	}

	private SimFightClubFighterStats Stats__BackingField;

	private int HeroAgeAtLastInteraction__BackingField;

	private bool HasEverInteractedWithBuffs__BackingField;

	private int HeroAgeAtLastFightClubCompliment__BackingField;

	private int FightClubComplimentCountThisAge__BackingField;

	public SimFightClubFighterStats Stats
	{
		get
		{
			return Stats__BackingField;
		}
		set
		{
			Stats__BackingField = value;
		}
	}

	public int HeroAgeAtLastInteraction
	{
		get
		{
			return HeroAgeAtLastInteraction__BackingField;
		}
		set
		{
			HeroAgeAtLastInteraction__BackingField = value;
		}
	}

	public bool HasEverInteractedWithBuffs
	{
		get
		{
			return HasEverInteractedWithBuffs__BackingField;
		}
		set
		{
			HasEverInteractedWithBuffs__BackingField = value;
		}
	}

	public int HeroAgeAtLastFightClubCompliment
	{
		get
		{
			return HeroAgeAtLastFightClubCompliment__BackingField;
		}
		set
		{
			HeroAgeAtLastFightClubCompliment__BackingField = value;
		}
	}

	public int FightClubComplimentCountThisAge
	{
		get
		{
			return FightClubComplimentCountThisAge__BackingField;
		}
		set
		{
			FightClubComplimentCountThisAge__BackingField = value;
		}
	}

	public SimFightClubFighter(bool initHere, FIGHT_CLUB_FIGHTING_STYLE fightingStyle, int gender)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void Die()
	{
	}

	public SimFightClubMove GetFavoriteMove()
	{
		return null;
	}

	protected virtual int LevelUpMovesToAddCount()
	{
		return 0;
	}

	protected virtual bool LevelUpAllowsIllegalMoveAttempts()
	{
		return false;
	}

	protected virtual int LevelUpStyleAlignedMovePickPercent()
	{
		return 0;
	}

	public virtual void LevelUp()
	{
	}

	public abstract float GetDifficulty();

	protected internal override bool WillHookupWithHero()
	{
		return false;
	}

	private void AddLearnedMovesOnLevelUp(int count, bool allowIllegalMoveAttempts, int styleAlignedPercent)
	{
	}

	private DataFightIngMove PickLevelUpMoveCandidate(List<DataFightIngMove> discovered, bool styleAligned, bool excludeIllegal = false, bool illegalOnly = false)
	{
		return null;
	}

	public new string GenderString()
	{
		return null;
	}

	public override string GetRelationshipString()
	{
		return null;
	}

	public bool WasEverInRelationshipWithHero()
	{
		return false;
	}

	public void RefreshMoveSet()
	{
	}

	public void PostMiniGameFightUpdate()
	{
	}

	public string GetPronoun(PRONOUN_TYPE type)
	{
		return null;
	}
}
