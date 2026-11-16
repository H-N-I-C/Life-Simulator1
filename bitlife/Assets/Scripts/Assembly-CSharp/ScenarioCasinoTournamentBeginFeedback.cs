using System;

public class ScenarioCasinoTournamentBeginFeedback : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCasinoGambler, float> _003C_003E9__5_0;

		public static Func<SimCasinoGambler, float> _003C_003E9__5_1;

		public static Func<SimCasinoGambler, CASINO_GAMBLING_STYLE> _003C_003E9__5_2;

		public static Func<SimCasinoGambler, int> _003C_003E9__5_3;

		internal float _003CRefreshScenario_003Eb__5_0(SimCasinoGambler t)
		{
			return 0f;
		}

		internal float _003CRefreshScenario_003Eb__5_1(SimCasinoGambler t)
		{
			return 0f;
		}

		internal CASINO_GAMBLING_STYLE _003CRefreshScenario_003Eb__5_2(SimCasinoGambler t)
		{
			return CASINO_GAMBLING_STYLE.NONE;
		}

		internal int _003CRefreshScenario_003Eb__5_3(SimCasinoGambler t)
		{
			return 0;
		}
	}

	private string _narrative;

	private SimCasino _casino;

	private readonly SimCasinoTournament _tournament;

	private SimCasinoGambler _winner;

	public ScenarioCasinoTournamentBeginFeedback(SimCasinoTournament tournament)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
