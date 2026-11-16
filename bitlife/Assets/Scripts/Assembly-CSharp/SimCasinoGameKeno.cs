using System;

[Serializable]
public class SimCasinoGameKeno : SimCasinoGame
{
	private string NumbersDrawn__BackingField;

	private string PlayerNumbers__BackingField;

	private string NumbersMatched__BackingField;

	public string NumbersDrawn
	{
		get
		{
			return NumbersDrawn__BackingField;
		}
		set
		{
			NumbersDrawn__BackingField = value;
		}
	}

	public string PlayerNumbers
	{
		get
		{
			return PlayerNumbers__BackingField;
		}
		set
		{
			PlayerNumbers__BackingField = value;
		}
	}

	public string NumbersMatched
	{
		get
		{
			return NumbersMatched__BackingField;
		}
		set
		{
			NumbersMatched__BackingField = value;
		}
	}

	public SimCasinoGameKeno()
		: base(CASINO_GAME_TYPE.NONE)
	{
	}

	public override float GetSkippedGameWinnings(float lastBetAmount)
	{
		return 0f;
	}

	public override float GetSkippedGameLosses(float lastBetAmount)
	{
		return 0f;
	}

	public override bool SkippedGameShouldWin()
	{
		return false;
	}

	public override void AssignSkippedGameSpecialWinType()
	{
	}
}
