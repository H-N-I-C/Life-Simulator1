using System.Collections.Generic;

public struct RacingCupSeriesResults
{
	public int Placement;

	public int Score;

	public Dictionary<SimRaceCarDriver, RacingCupSeriesResults> competitorResults;

	public RacingCupSeriesActiveRace activeRace;
}
