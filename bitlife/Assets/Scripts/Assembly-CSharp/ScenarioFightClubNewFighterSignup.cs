using System;

public class ScenarioFightClubNewFighterSignup : ScenarioLazyCsvBase
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string, string> _003C_003E9__3_0;

		internal string _003CPrepareParser_003Eb__3_0(string left, string right)
		{
			return null;
		}
	}

	private readonly FightClub_NewFighterActivity _activity;

	public ScenarioFightClubNewFighterSignup(FightClub_NewFighterActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	public override void RefreshScenario()
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
