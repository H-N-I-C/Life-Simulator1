using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimFightClubFighterStats
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataFightIngMove, SimFightClubMove> _003C_003E9__33_0;

		public static Func<SimFightClubMove, DataFightIngMove> _003C_003E9__76_0;

		public static Func<SimFightClubInjury, int> _003C_003E9__93_0;

		public static Func<SimFightClubInjury, int> _003C_003E9__94_0;

		internal SimFightClubMove _003C_002Ector_003Eb__33_0(DataFightIngMove x)
		{
			return null;
		}

		internal DataFightIngMove _003CGetUndiscoveredMoveDataForCurrentStyle_003Eb__76_0(SimFightClubMove x)
		{
			return null;
		}

		internal int _003CGetCurrentInjuriesRequiringPhysicalTherapy_003Eb__93_0(SimFightClubInjury x)
		{
			return 0;
		}

		internal int _003CGetCurrentInjuriesRequiringSurgery_003Eb__94_0(SimFightClubInjury x)
		{
			return 0;
		}
	}

	private FIGHT_CLUB_FIGHTING_STYLE FightingStyle__BackingField;

	private int Level__BackingField;

	private int NumTimesUsedSteroids__BackingField;

	private int NumTimesUsedSteroidsWithoutBeingCaught__BackingField;

	private List<SimFightClubMove> _currentMoveSet;

	private List<SimFightClubMove> _learnedMoves;

	private int _experiencePoints;

	private float _strength;

	private float _toughness;

	private float _agility;

	private float _technique;

	private float _stamina;

	private SimFightClubBuff _buff;

	private SimFightClubSteroid _steroid;

	private List<string> _lastYearsMoveSet;

	private int _fightsThisYear;

	private int _fightsWon;

	private int _fightsLost;

	private int _championshipBelts;

	private List<SimFightClubInjury> _injuries;

	private bool _isHero;

	public FIGHT_CLUB_FIGHTING_STYLE FightingStyle
	{
		get
		{
			return FightingStyle__BackingField;
		}
		set
		{
			FightingStyle__BackingField = value;
		}
	}

	public int Level
	{
		get
		{
			return Level__BackingField;
		}
		set
		{
			Level__BackingField = value;
		}
	}

	public int NumTimesUsedSteroids
	{
		get
		{
			return NumTimesUsedSteroids__BackingField;
		}
		set
		{
			NumTimesUsedSteroids__BackingField = value;
		}
	}

	public int NumTimesUsedSteroidsWithoutBeingCaught
	{
		get
		{
			return NumTimesUsedSteroidsWithoutBeingCaught__BackingField;
		}
		set
		{
			NumTimesUsedSteroidsWithoutBeingCaught__BackingField = value;
		}
	}

	public SimFightClubFighterStats(FIGHT_CLUB_FIGHTING_STYLE fightingStyle, bool isHero)
	{
	}

	public void IncrementAge()
	{
	}

	public int GetExperiencePoints()
	{
		return 0;
	}

	public void SetExperiencePoints(int experiencePoints)
	{
	}

	public void AdjustExperiencePoints(int amount)
	{
	}

	public float GetBaseStrength()
	{
		return 0f;
	}

	public void SetBaseStrength(float strength)
	{
	}

	public float GetStrength()
	{
		return 0f;
	}

	public void AdjustStrength(float amount)
	{
	}

	public float GetBaseToughness()
	{
		return 0f;
	}

	public void SetBaseToughness(float toughness)
	{
	}

	public float GetToughness()
	{
		return 0f;
	}

	public void AdjustToughness(float amount)
	{
	}

	public float GetBaseAgility()
	{
		return 0f;
	}

	public void SetBaseAgility(float agility)
	{
	}

	public float GetAgility()
	{
		return 0f;
	}

	public void AdjustAgility(float amount)
	{
	}

	public float GetBaseTechnique()
	{
		return 0f;
	}

	public void SetBaseTechnique(float technique)
	{
	}

	public float GetTechnique()
	{
		return 0f;
	}

	public void AdjustTechnique(float amount)
	{
	}

	public float GetBaseStamina()
	{
		return 0f;
	}

	public void SetBaseStamina(float amount)
	{
	}

	public float GetStamina()
	{
		return 0f;
	}

	public void AdjustStamina(float amount)
	{
	}

	public bool IsOnSteroids()
	{
		return false;
	}

	public string GetSteroidName()
	{
		return null;
	}

	public bool CurrentInjuryExistsThatAffectsAttribute(FIGHT_CLUB_ATTRIBUTE attribute)
	{
		return false;
	}

	public void AddSteroid(SimFightClubSteroid steroid)
	{
	}

	public void RemoveSteroid()
	{
	}

	public SimFightClubSteroid GetCurrentSteroid()
	{
		return null;
	}

	public bool AlreadyHasBuff()
	{
		return false;
	}

	public void AddBuff(SimFightClubBuff buff)
	{
	}

	public void RemoveBuff()
	{
	}

	public List<SimFightClubMove> GetCurrentMoveSet()
	{
		return null;
	}

	public List<string> GetLastYearsMoveSet()
	{
		return null;
	}

	public bool MoveAlreadyLearned(SimFightClubMove move)
	{
		return false;
	}

	public void AddLearnedMove(SimFightClubMove move)
	{
	}

	public void AddMoveToCurrentMoveSet(SimFightClubMove move)
	{
	}

	public void ReplaceMoveInCurrentMoveSet(SimFightClubMove newMove, SimFightClubMove oldMove)
	{
	}

	public List<SimFightClubMove> GetLearnedMoves()
	{
		return null;
	}

	public List<SimFightClubMove> GetUnassignedMoves()
	{
		return null;
	}

	public List<SimFightClubMove> GetUndiscoveredMoves()
	{
		return null;
	}

	public List<DataFightIngMove> GetUndiscoveredMoveDataForCurrentStyle()
	{
		return null;
	}

	public List<DataFightIngMove> GetUndiscoveredMoveDataForAllStyles()
	{
		return null;
	}

	public bool QualifiesForLevelUp()
	{
		return false;
	}

	public static int GetStatCapIncrement()
	{
		return 0;
	}

	public int GetStatUpperLimitForLevel(int level)
	{
		return 0;
	}

	public int GetStatLowerLimitForLevel(int level)
	{
		return 0;
	}

	public bool BaseStatAlreadyAtMaxForLevel(FIGHT_CLUB_ATTRIBUTE attribute)
	{
		return false;
	}

	public int GetStatValueBump(FIGHT_CLUB_ATTRIBUTE attribute, bool reducedBump = false)
	{
		return 0;
	}

	public int GetExperiencePointsNeededToLevelUp()
	{
		return 0;
	}

	public int GetExperiencePointsNeededToLevelUp(int level)
	{
		return 0;
	}

	public List<SimFightClubMove> GetMovesThatQualifyForUpgrade()
	{
		return null;
	}

	public int GetFightsWon()
	{
		return 0;
	}

	public int GetFightsLost()
	{
		return 0;
	}

	public int GetNumberOfChampionshipBeltsWon()
	{
		return 0;
	}

	public int GetNumberOfFightsThisYear()
	{
		return 0;
	}

	public List<SimFightClubInjury> GetCurrentInjuries()
	{
		return null;
	}

	public int HealAllCurrentInjuries()
	{
		return 0;
	}

	public List<SimFightClubInjury> GetCurrentInjuriesRequiringPhysicalTherapy()
	{
		return null;
	}

	public List<SimFightClubInjury> GetCurrentInjuriesRequiringSurgery()
	{
		return null;
	}

	public bool HasCurrentInjuryRequiringSurgery()
	{
		return false;
	}

	public bool HasCurrentInjuryRequiringPhysicalTherapy()
	{
		return false;
	}

	public bool AlreadyHasInjury(SimFightClubInjury injury)
	{
		return false;
	}

	public void AddInjury(SimFightClubInjury injury)
	{
	}

	public void UpdateCurrentInjuriesForAgeAtLastFight()
	{
	}

	public void AddFightResult(bool won, bool championshipBeltWon)
	{
	}

	public float GetCumulativeAttributeAverageValue()
	{
		return 0f;
	}

	public void SaveLastYearsMoveSet()
	{
	}

	public string GetBoxingEmoji()
	{
		return null;
	}

	public void ReplaceCurrentMoveSet(List<SimFightClubMove> newMoveSet)
	{
	}

	public string GetFightingStyleString()
	{
		return null;
	}

	public static string GetGameNameForAttribute(FIGHT_CLUB_ATTRIBUTE attribute)
	{
		return null;
	}

	private bool _003CGetUndiscoveredMoveDataForCurrentStyle_003Eb__76_1(DataFightIngMove x)
	{
		return false;
	}
}
