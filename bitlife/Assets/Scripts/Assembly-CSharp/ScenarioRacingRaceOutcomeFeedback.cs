using System;
using System.Collections.Generic;

public class ScenarioRacingRaceOutcomeFeedback : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimRaceCarDriver, bool> _003C_003E9__21_0;

		internal bool _003CHeroWinsRace_003Eb__21_0(SimRaceCarDriver t)
		{
			return false;
		}
	}

	private string _narrative;

	private readonly string _winningDriverName;

	private readonly float _prizeMoney;

	private readonly string _raceSponsor;

	private readonly RACING_INCOMPLETE_RACE_REASON _incompleteRaceReason;

	private readonly SimRaceCar _car;

	private readonly SimRacingTrack _track;

	private readonly List<SimRaceCarDriver> _competitors;

	private readonly bool _allowGenericImpacts;

	private readonly TimeSpan _heroTime;

	private readonly MiniGameDataCarRaceResult _heroResult;

	private SimRacing _racing;

	public ScenarioRacingRaceOutcomeFeedback(string winningDriverName, float prizeMoney, string raceSponsor, SimRacingTrack track, List<SimRaceCarDriver> competitors, bool allowGenericImpacts, MiniGameDataCarRaceResult heroResult)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void DidntFinishRace()
	{
	}

	private void CaughtByPoliceOutcome()
	{
	}

	private void StalledOutcome()
	{
	}

	private void MalfunctionOutcome()
	{
	}

	private void CrashedOutcome()
	{
	}

	private void FinishedRace()
	{
	}

	private void HeroLostRace(BracketParser parser, RACING_LICENSE heroLicense)
	{
	}

	private void HeroWinsRace(BracketParser parser, RACING_LICENSE heroLicense)
	{
	}

	private void ImpactCarCondition(int impact)
	{
	}

	public override void DismissSelected()
	{
	}
}
