using System;
using System.Collections.Generic;

[Serializable]
public class RandomDeck
{
	private const int PERCENT_STEP = 5;

	private List<bool> _cards;

	private int _index;

	private int _deckSize;

	private int _successCount;

	public int DeckSize => 0;

	public int SuccessCount => 0;

	public RandomDeck(int successCount, int deckSize)
	{
	}

	public bool Draw()
	{
		return false;
	}

	public void SetPercent(float percent)
	{
	}

	public static (int, int) ComputeDeckParameters(float percent)
	{
		return default;
	}

	private static int GCD(int a, int b)
	{
		return 0;
	}

	private void FillAndShuffle(int successCount)
	{
	}

	private void Reshuffle()
	{
	}

	private void ShuffleAll()
	{
	}
}
