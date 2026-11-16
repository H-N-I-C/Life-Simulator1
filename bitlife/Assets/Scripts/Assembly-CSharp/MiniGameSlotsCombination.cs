using System;
using System.Collections.Generic;

public class MiniGameSlotsCombination
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<(MiniGameSlotsWinType, float), float> _003C_003E9__19_0;

		public static Func<string, MiniGameSlotsCombination> _003C_003E9__22_0;

		public static Func<string, MiniGameSlotsCombination> _003C_003E9__22_1;

		public static Func<string, MiniGameSlotsCombination> _003C_003E9__22_2;

		public static Func<MiniGameSlotsCombination, string> _003C_003E9__24_0;

		public static Func<MiniGameSlotsCombination, bool> _003C_003E9__30_0;

		internal float _003CGetRandomWinType_003Eb__19_0((MiniGameSlotsWinType, float) w)
		{
			return 0f;
		}

		internal MiniGameSlotsCombination _003CGetAllWinCombinations_003Eb__22_0(string c)
		{
			return null;
		}

		internal MiniGameSlotsCombination _003CGetAllWinCombinations_003Eb__22_1(string c)
		{
			return null;
		}

		internal MiniGameSlotsCombination _003CGetAllWinCombinations_003Eb__22_2(string c)
		{
			return null;
		}

		internal string _003CGetRandomLossCombination_003Eb__24_0(MiniGameSlotsCombination w)
		{
			return null;
		}

		internal bool _003CGetResultCombination_003Eb__30_0(MiniGameSlotsCombination t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass21_0
	{
		public MiniGameSlotsWinType winType;

		internal bool _003CGetRandomWinCombination_003Eb__0(MiniGameSlotsCombination w)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass24_0
	{
		public IEnumerable<string> winCombinations;

		internal bool _003CGetRandomLossCombination_003Eb__1(string c)
		{
			return false;
		}
	}

	private readonly string Symbols__BackingField;

	private readonly float Multiplier__BackingField;

	private readonly MiniGameSlotsWinType WinType__BackingField;

	private const string PossibleSymbols = "ABCDEF";

	private const int SymbolsCount = 3;

	private const bool UseRealWinChance = false;

	private const float WinChance = 0.15f;

	private static List<string> AllCombinations;

	private static List<MiniGameSlotsCombination> WinCombinations;

	private static List<MiniGameSlotsCombination> WinCombinationTypes;

	private static List<string> LossCombinations;

	private static readonly List<(MiniGameSlotsWinType, float)> WeightedWinTypes;

	public string Symbols => Symbols__BackingField;

	public float Multiplier => Multiplier__BackingField;

	public MiniGameSlotsWinType WinType => WinType__BackingField;

	private MiniGameSlotsCombination(string symbols, float multiplier, MiniGameSlotsWinType winType)
	{
	}

	private static MiniGameSlotsWinType GetRandomWinType()
	{
		return MiniGameSlotsWinType.NONE;
	}

	private static MiniGameSlotsCombination GetRandomWinCombination()
	{
		return null;
	}

	private static MiniGameSlotsCombination GetRandomWinCombination(MiniGameSlotsWinType winType)
	{
		return null;
	}

	private static List<MiniGameSlotsCombination> GetAllWinCombinations()
	{
		return null;
	}

	public static List<MiniGameSlotsCombination> GetAllWinCombinationTypes()
	{
		return null;
	}

	private static MiniGameSlotsCombination GetRandomLossCombination()
	{
		return null;
	}

	private static List<string> GenerateTwoOfAKindCombinations(char symbol)
	{
		return null;
	}

	private static List<string> GetAllCombinations()
	{
		return null;
	}

	private static void GenerateAllVariationsWithRepetition(char[] characters, int length, string current, List<string> combinations)
	{
	}

	public static float GetWinChance()
	{
		return 0f;
	}

	private static float GetRealWinChance()
	{
		return 0f;
	}

	public static MiniGameSlotsCombination GetResultCombination()
	{
		return null;
	}
}
