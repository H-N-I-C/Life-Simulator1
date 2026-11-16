using System;
using System.Collections.Generic;
using Backend;
using BitLife.UI.CandyObjects;
using Game.Seasons;

public class ScenarioSeasonsStreakStatus : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<StuffItem, bool> _003C_003E9__12_1;

		public static Func<StuffItem, int> _003C_003E9__12_2;

		public static Action<List<StuffItem>> _003C_003E9__12_0;

		internal void _003CChoiceSelectedAtIndex_003Eb__12_0(List<StuffItem> items)
		{
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__12_1(StuffItem t)
		{
			return false;
		}

		internal int _003CChoiceSelectedAtIndex_003Eb__12_2(StuffItem t)
		{
			return 0;
		}
	}

	private STREAK_STATUS StreakStatus__BackingField;

	private List<CandyText> _tableEntries;

	private static Dictionary<STREAK_STATUS, string> Titles;

	private static Dictionary<STREAK_STATUS, string> Emojis;

	private static Dictionary<STREAK_STATUS, string> TimeLabels;

	private static Dictionary<STREAK_STATUS, string> BodyTexts;

	private const string BodyTextTitle1 = "<b>Quick, before time runs out!</b>";

	private const string BodyTextTitle2 = "<b>You are about to lose your streak! This is your last chance to keep it going.</b>";

	private const string BodyTextTitle3 = "<b>You can extend again soon!</b>";

	private const string MainBodyText = "Streaks are simple. Come back each day and age up at least 10 times to build it. That's it! The higher your streak, the more rewards you can earn in events (plus bragging rights, of course).";

	private const string BodyText1 = "If you ever miss a day, <b>Streak Savers</b> automatically maintains your Streak!";

	private const string BodyText2 = "If you want to maintain your streak, you will have to use <b>Streak Savers</b>!";

	public STREAK_STATUS StreakStatus
	{
		get
		{
			return StreakStatus__BackingField;
		}
		private set
		{
			StreakStatus__BackingField = value;
		}
	}

	public override string EmojiString()
	{
		return null;
	}

	public ScenarioSeasonsStreakStatus(STREAK_STATUS streakStatus)
	{
	}

	~ScenarioSeasonsStreakStatus()
	{
	}

	public override void Closed()
	{
	}

	private void OnSeasonTimeChanged(SeasonTimeInfo info)
	{
	}

	public override void OnTableDictionaryEntriesCreated(List<CandyText> entries)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
