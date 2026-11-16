using System.Collections.Generic;

public class FightClubLeaguePositionOrganizer
{
	private readonly SimFightClub _fightClub;

	private readonly object heroPlaceholder;

	private readonly List<object> _currentOrder;

	public FightClubLeaguePositionOrganizer(SimFightClub fightClub)
	{
	}

	public void Refresh()
	{
	}

	public int GetLeaguePosition(SimFightClubLeagueFighter theFighter)
	{
		return 0;
	}

	public void SetLeaguePosition(SimFightClubLeagueFighter theFighter, int proposedLeagueRank, List<SimFightClubLeagueFighter> leagueFighters)
	{
	}
}
