using System;
using System.Collections.Generic;
using Game;

public class ScenarioGameFighting : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__20_0;

		internal string _003CExpandHitTexts_003Eb__20_0(string p)
		{
			return null;
		}
	}

	public static bool SkipMiniGame;

	private readonly MiniGameDataFighting _gameData;

	public ScenarioMiniGameTextDictionary RandomOptions;

	public List<string> replaceEntries;

	public const string HeroMovePrefix = "hero_";

	public const string NpcMovePrefix = "npc_";

	public const string NpcVsMovePrefix = "npc_vs_";

	public const string Domain = "fightclub";

	public ScenarioGameFighting(MiniGameDataFighting gameData)
	{
	}

	public static Dictionary<string, string> GetInitialUnprocessTexts()
	{
		return null;
	}

	public static Dictionary<string, string> GetUnprocessTextAfterLoad(string npcHeShe, string npcHeSheCap, string npcHisHer, string npcHimHer, string coachHeShe, string drugIllegal)
	{
		return null;
	}

	public static void FillMiniGameTextRegistryForHeroPresent(MiniGameDataFighting gameData, MiniGameFightingFighter heroFighter, MiniGameFightingFighter npcFighter, ScenarioMinigameTextRegistry registry, ScenarioMiniGameTextDictionary dictionary)
	{
	}

	private static void TestCachedEntries(ScenarioMinigameTextRegistry registry)
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	public override void MiniGameCompleted()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public static List<string> ExpandTextVariations(string input)
	{
		return null;
	}

	private static void ExpandTextRegistry(Dictionary<string, string> list, ScenarioMinigameTextRegistry registry, ScenarioMiniGameTextDictionary dictionary)
	{
	}

	public static string GetMoveKey(DataFightIngMove moveData, string bodyPart, bool isHero, bool targetIsHero)
	{
		return null;
	}

	public static string GetMovementDomain(DataFightIngMove moveData, bool isHero, bool targetIsHero)
	{
		return null;
	}

	public static void ExpandHitTexts(ScenarioMinigameTextRegistry registry, ScenarioMiniGameTextDictionary dictionary, List<SimFightClubMove> moves, bool isHero, bool targetIsHero, string npcHeSheCap, string npcHisHer)
	{
	}

	private static void AddToRegistry(ScenarioMinigameTextRegistry registry, ScenarioMiniGameTextDictionary dictionary, DataFightIngMove moveData, string bodyPart, bool isHero, bool targetIsHero, string npcHeSheCap, string npcHisHer)
	{
	}

	private static void GenerateCombinations(string remainingString, string currentPrefix, List<string> results)
	{
	}

	private static void PreCacheFighterMoves(SimFightClubFighterStats stats, ScenarioMinigameTextRegistry registry)
	{
	}
}
