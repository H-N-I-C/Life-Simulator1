using System.Collections;
using System.Collections.Generic;

public class ScenarioRacingCupSeriesDemolitionRaceResults : Scenario
{
	private class ScoreSorter : IComparer
	{
		private readonly Dictionary<string, int> _RawTable;

		public ScoreSorter(Dictionary<string, int> scores)
		{
		}

		public int Compare(object x, object y)
		{
			return 0;
		}
	}

	private const string HERO_IDENTIFIER = "You";

	private readonly RacingCupSeriesResults _results;

	private readonly RacingCupSeriesActiveRace _activeRace;

	private readonly SimRacingCup _cup;

	public ScenarioRacingCupSeriesDemolitionRaceResults(RacingCupSeriesResults raceCupResults)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void SortTable(Dictionary<string, int> table, ArrayList tableOrderArray)
	{
	}

	private Dictionary<string, string> CreateTableDictionary()
	{
		return null;
	}

	private ArrayList CreateTableOrder()
	{
		return null;
	}

	private Dictionary<string, int> CreatePointTotalDictionary()
	{
		return null;
	}

	private Dictionary<string, string> CreatePointTotalTable()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
