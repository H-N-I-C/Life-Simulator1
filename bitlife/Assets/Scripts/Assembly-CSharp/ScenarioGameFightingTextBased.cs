using System.Collections.Generic;

public class ScenarioGameFightingTextBased : Scenario
{
	private sealed class _003C_003Ec__DisplayClass16_0
	{
		public string moveName;

		internal bool _003CGetHeroLastYearsMoveSetReuseCount_003Eb__0(SimFightClubMove x)
		{
			return false;
		}
	}

	private readonly MiniGameDataFighting _gameData;

	private readonly SimFightClubFighterStats _heroStats;

	private readonly SimFightClubFighterStats _npcStats;

	private readonly List<SimFightClubMove> _heroMoves;

	private readonly List<SimFightClubMove> _npcMoves;

	private readonly List<string> _usedBodyParts;

	private int _heroHp;

	private int _npcHp;

	private SOUND _lastPlayedSound;

	public ScenarioGameFightingTextBased(MiniGameDataFighting gameData)
	{
	}

	private void FirstRoundOfHits()
	{
	}

	private void SecondRoundOfHits()
	{
	}

	private string GetBodyString(List<SimFightClubMove> moves, int receiverGender, int hitIndex)
	{
		return null;
	}

	private void Outcome()
	{
	}

	private void DetermineWinner()
	{
	}

	private int GetScoreForStats(SimFightClubFighterStats stats, SimFightClubFighterStats opponentStats)
	{
		return 0;
	}

	private int GetHeroLastYearsMoveSetReuseCount()
	{
		return 0;
	}

	private void DetermineMoves()
	{
	}

	private static SimFightClubMove GetRandomMove(List<SimFightClubMove> moves, List<SimFightClubMove> blackListMoves)
	{
		return null;
	}
}
