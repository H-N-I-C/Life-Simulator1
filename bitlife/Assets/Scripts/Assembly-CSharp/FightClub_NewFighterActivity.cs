using System;
using Game;

public class FightClub_NewFighterActivity : ActivityLazyCsvBase
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataFightIngMove, string> _003C_003E9__8_0;

		internal string _003CGetMoveSet_003Eb__8_0(DataFightIngMove x)
		{
			return null;
		}
	}

	private readonly FIGHT_CLUB_FIGHTING_STYLE _fightingStyle;

	private readonly SimFightClub _fightClub;

	private readonly DataFightIngMove[] _unlockedMoveSets;

	public FightClub_NewFighterActivity(Life life, FIGHT_CLUB_FIGHTING_STYLE fightingStyle)
		: base(null)
	{
	}

	public string GetStyleString()
	{
		return null;
	}

	public SimFightClubCoach GetCoach()
	{
		return null;
	}

	public string GetMoveType()
	{
		return null;
	}

	public string CoachFirstName()
	{
		return null;
	}

	public string[] GetMoveSet()
	{
		return null;
	}

	public string GetStyleColorString()
	{
		return null;
	}

	public string GetEmojiString()
	{
		return null;
	}

	private void FighterSignup(ChoiceSelection choiceSelection)
	{
	}

	private void NewFighterOkClicked(ChoiceSelection choiceSelection)
	{
	}
}
