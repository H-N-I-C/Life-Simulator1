using System;

[Serializable]
public class SimCasinoGameTexasHoldEm : SimCasinoGame
{
	public SimCasinoGameTexasHoldEm()
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
