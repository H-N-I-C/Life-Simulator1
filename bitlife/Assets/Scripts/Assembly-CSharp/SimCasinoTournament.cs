using System;
using System.Collections.Generic;

[Serializable]
public class SimCasinoTournament
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCasinoGambler, string> _003C_003E9__22_0;

		internal string _003C_002Ector_003Eb__22_0(SimCasinoGambler t)
		{
			return null;
		}
	}

	private CASINO_GAME_TYPE GameType__BackingField;

	private int NumberOfPlayers__BackingField;

	private double PrizeMoney__BackingField;

	private double WagerAmount__BackingField;

	private SimCasinoGambler WagerParticipant__BackingField;

	private double _entryFee;

	private List<SimCasinoGambler> _participants;

	public CASINO_GAME_TYPE GameType
	{
		get
		{
			return GameType__BackingField;
		}
		set
		{
			GameType__BackingField = value;
		}
	}

	public int NumberOfPlayers
	{
		get
		{
			return NumberOfPlayers__BackingField;
		}
		set
		{
			NumberOfPlayers__BackingField = value;
		}
	}

	public double PrizeMoney
	{
		get
		{
			return PrizeMoney__BackingField;
		}
		set
		{
			PrizeMoney__BackingField = value;
		}
	}

	public double WagerAmount
	{
		get
		{
			return WagerAmount__BackingField;
		}
		set
		{
			WagerAmount__BackingField = value;
		}
	}

	public SimCasinoGambler WagerParticipant
	{
		get
		{
			return WagerParticipant__BackingField;
		}
		set
		{
			WagerParticipant__BackingField = value;
		}
	}

	public SimCasinoTournament(CASINO_GAME_TYPE gameType, int numberOfPlayers, double entryFee, double prizeMoney)
	{
	}

	public void IncrementAge()
	{
	}

	public List<SimCasinoGambler> GetParticipants()
	{
		return null;
	}
}
