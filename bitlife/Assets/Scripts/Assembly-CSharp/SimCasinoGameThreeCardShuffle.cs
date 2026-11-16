using System;

[Serializable]
public class SimCasinoGameThreeCardShuffle : SimCasinoGame
{
	private int DifficultyLevel__BackingField;

	public int DifficultyLevel
	{
		get
		{
			return DifficultyLevel__BackingField;
		}
		set
		{
			DifficultyLevel__BackingField = value;
		}
	}

	public SimCasinoGameThreeCardShuffle()
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
