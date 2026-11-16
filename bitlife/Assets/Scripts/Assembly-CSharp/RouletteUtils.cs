using UnityEngine;

public static class RouletteUtils
{
	public const int MAX_NUMBER = 12;

	private const int WIN_NUMBER_MULTIPLIER = 13;

	private const int WIN_COLOR_MULTIPLIER = 1;

	public static Color BLACK_COLOR => default;

	public static Color RED_COLOR => default;

	public static Color ZERO_COLOR => default;

	public static ROULETTE_COLOR ColorForNumber(int number)
	{
		return ROULETTE_COLOR.NONE;
	}

	public static int MultiplierForBetType(ROULETTE_BET_TYPE type)
	{
		return 0;
	}

	public static int GetRandomNumber()
	{
		return 0;
	}

	public static bool IsRegularNumber(int number)
	{
		return false;
	}
}
