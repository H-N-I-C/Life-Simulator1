using System;

public class FightClubBettingActivity : ActivityLazyCsvBase
{
	public static class Balancing
	{
		public const int MIN_AGE_TO_BET = 18;

		public const int ADMISSION_COST = 100;

		public const float MIN_BET_AMOUNT = 1f;

		public const float MAX_BET_AMOUNT = 1000000f;
	}

	public enum ScenarioID
	{
		FC_BETTING_START = 0,
		FC_BETTING_INSUFFICIENT_FUNDS_FEEDBACK = 1,
		FC_BETTING_PLACE_BET = 2
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimFightClubLeagueFighter, string> _003C_003E9__8_0;

		internal string _003Cget_FighterNames_003Eb__8_0(SimFightClubLeagueFighter x)
		{
			return null;
		}
	}

	private readonly SimHero _hero;

	private readonly SimFightClubBettingMatch _bettingMatch1;

	private readonly SimFightClubBettingMatch _bettingMatch2;

	private SimFightClubBettingMatch _selectedBettingMatch;

	private SimFightClubLeagueFighter _selectedFighter;

	private float _betAmount;

	public string[] FighterNames => null;

	public float MinBet => 0f;

	public float MaxBet => 0f;

	public FightClubBettingActivity(Life life)
		: base(null)
	{
	}

	private void BettingMatchChoiceMade(ChoiceSelection choice)
	{
	}

	private void BettingAmountChoiceMade(ChoiceSelection choice)
	{
	}
}
