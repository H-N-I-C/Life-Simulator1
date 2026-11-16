using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimFightClubMove
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__27_0;

		internal string _003CGetRandomBodyPartAffected_003Eb__27_0(string p)
		{
			return null;
		}
	}

	private int _useCount;

	private List<string> _moveIDHistory;

	public string MoveID => null;

	public string MoveName => null;

	public DataFightIngMove MoveData => null;

	public SimFightClubMove(DataFightIngMove move)
	{
	}

	public void PerformMove()
	{
	}

	public bool QualifiesForUpgrade()
	{
		return false;
	}

	public int GetUsesRemainingBeforeUpgrade()
	{
		return 0;
	}

	public string GetMoveType()
	{
		return null;
	}

	public FIGHT_CLUB_FIGHTING_STYLE GetFightingStyle()
	{
		return FIGHT_CLUB_FIGHTING_STYLE.NONE;
	}

	public string GetStyleString()
	{
		return null;
	}

	public string GetStrongAgainstString()
	{
		return null;
	}

	public float GetBasePower()
	{
		return 0f;
	}

	public float GetBaseAccuracy()
	{
		return 0f;
	}

	public float GetStaminaCost()
	{
		return 0f;
	}

	public string GetMoveDescription()
	{
		return null;
	}

	public DataFightIngMove GetNextUpgrade()
	{
		return null;
	}

	public string GetUnlockConditions()
	{
		return null;
	}

	public bool HasNextUpgrade()
	{
		return false;
	}

	public bool AlreadyLearned(SimFightClubMove newMove)
	{
		return false;
	}

	public void Upgrade()
	{
	}

	private DataFightIngMove GetByIndex(int index)
	{
		return null;
	}

	public string GetPreviousStageNameOrNull()
	{
		return null;
	}

	public string GetRandomBodyPartAffected(int gender, bool allowBlacklistBodyParts)
	{
		return null;
	}
}
