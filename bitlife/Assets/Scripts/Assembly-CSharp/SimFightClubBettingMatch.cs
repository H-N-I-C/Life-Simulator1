using System;
using System.Collections.Generic;

[Serializable]
public class SimFightClubBettingMatch
{
	private sealed class _003C_003Ec__DisplayClass18_0
	{
		public SimFightClubLeagueFighter fighter;

		internal bool _003CFighterLastNameInBlackList_003Eb__0(SimFightClubLeagueFighter x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass6_0
	{
		public SimFightClubLeagueFighter fighter2;

		public Func<SimFightClubLeagueFighter, bool> _003C_003E9__0;

		internal bool _003CSetupFighters_003Eb__0(SimFightClubLeagueFighter x)
		{
			return false;
		}
	}

	private List<SimFightClubLeagueFighter> _fighters;

	private SimFightClubLeagueFighter _betFighter;

	private SimFightClubLeagueFighter _winningFighter;

	private List<SimFightClubLeagueFighter> _blackListFighters;

	private double _betAmount;

	public SimFightClubBettingMatch(List<SimFightClubLeagueFighter> blackListFighters = null)
	{
	}

	private void SetupFighters()
	{
	}

	public void SetBetAmount(double amount)
	{
	}

	public double GetBetAmount()
	{
		return 0.0;
	}

	public List<SimFightClubLeagueFighter> GetFighters()
	{
		return null;
	}

	public void SetBetFighter(SimFightClubLeagueFighter fighter)
	{
	}

	public SimFightClubLeagueFighter GetBetFighter()
	{
		return null;
	}

	private void DetermineWinningFighter()
	{
	}

	public void SetWinningFighter(SimFightClubLeagueFighter winningFighter)
	{
	}

	public SimFightClubLeagueFighter GetWinningFighter()
	{
		return null;
	}

	public SimFightClubLeagueFighter GetLosingFighter()
	{
		return null;
	}

	public int GetBettingOddsForFighter(SimFightClubLeagueFighter fighter)
	{
		return 0;
	}

	private float GetScoreForFighter(SimFightClubLeagueFighter fighter)
	{
		return 0f;
	}

	private bool FighterLastNameInBlackList(SimFightClubLeagueFighter fighter)
	{
		return false;
	}

	private float _003CDetermineWinningFighter_003Eb__12_0(SimFightClubLeagueFighter t)
	{
		return 0f;
	}
}
