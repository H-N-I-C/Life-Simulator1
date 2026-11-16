using System;
using System.Collections.Generic;

public class FightClubViewMoveSetActivity : ActivityLazyCsvBase
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static ScenarioLazyCsvBase.ChoiceCallback _003C_003E9__16_0;

		internal void _003CMoveReplacedWith_003Eb__16_0(ChoiceSelection _)
		{
		}
	}

	private readonly SimFightClubMove Move__BackingField;

	private SimFightClubMove MoveReplaced__BackingField;

	private readonly SimFightClub FightClub__BackingField;

	private readonly Dictionary<string, SimFightClubMove> _moveInfoToMove;

	private readonly List<string> _moveInfo;

	public SimFightClubMove Move => Move__BackingField;

	public SimFightClubMove MoveReplaced
	{
		get
		{
			return MoveReplaced__BackingField;
		}
		private set
		{
			MoveReplaced__BackingField = value;
		}
	}

	public SimFightClub FightClub => FightClub__BackingField;

	public FightClubViewMoveSetActivity(Life life, SimFightClubMove move, bool readOnly)
		: base(null)
	{
	}

	public string GetAccuracyString(string low, string medium, string high)
	{
		return null;
	}

	public string[] GetFormattedMoveDropDownItems()
	{
		return null;
	}

	private void EditMoveSetChoice(ChoiceSelection choice)
	{
	}

	private void MoveReplacedWith(ChoiceSelection choice)
	{
	}

	private void PopulateMoveDictionary()
	{
	}
}
