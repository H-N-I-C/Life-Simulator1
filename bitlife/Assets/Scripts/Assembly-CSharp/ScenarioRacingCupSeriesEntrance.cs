using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class ScenarioRacingCupSeriesEntrance : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRaceCar, string> _003C_003E9__13_0;

		internal string _003CRefreshScenario_003Eb__13_0(SimRaceCar car)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass14_0
	{
		public ArrayList dropDownStrings;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimRaceCar car)
		{
			return false;
		}
	}

	private const string TRACKS_CATEGORY = "Tracks";

	private const string COMPETITORS_CATEGORY = "Competitors";

	private const string FIRST_PLACE = "1st Place Prize";

	private const string SECOND_PLACE = "2nd Place Prize";

	private const string THIRD_PLACE = "3rd Place Prize";

	private readonly SimRacing _racing;

	private readonly SimRacingCup _racingCup;

	private readonly DataRacingCupSeriesWinning _winnings;

	private readonly List<SimRaceCarDriver> _competitors;

	private BracketParser _bracketParser;

	public ScenarioRacingCupSeriesEntrance(SimRacing racing, SimRacingCup simRacingCup)
	{
	}

	public ScenarioRacingCupSeriesEntrance(RacingCupSeriesRegistration registration)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private ArrayList GetTableOrder()
	{
		return null;
	}

	private Dictionary<string, string> GetCircuitTable()
	{
		return null;
	}

	private string GetCompetitorDescription()
	{
		return null;
	}

	private string GetSponsorDescription()
	{
		return null;
	}
}
