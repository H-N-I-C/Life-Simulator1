using UnityEngine;

public class RouletteBet
{
	private readonly ROULETTE_BET_TYPE Type__BackingField;

	private readonly int Number__BackingField;

	private readonly ROULETTE_COLOR Color__BackingField;

	public ROULETTE_BET_TYPE Type => Type__BackingField;

	public int Number => Number__BackingField;

	public ROULETTE_COLOR Color => Color__BackingField;

	public RouletteBet(int number)
	{
	}

	public RouletteBet(ROULETTE_COLOR color)
	{
	}

	public string Description()
	{
		return null;
	}

	public string FullDescription()
	{
		return null;
	}

	public Color BackgroundColor()
	{
		return default;
	}

	public override bool Equals(object obj)
	{
		return false;
	}

	public override int GetHashCode()
	{
		return 0;
	}
}
