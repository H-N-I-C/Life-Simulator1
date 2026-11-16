public class SimCasinoSession
{
	private float LastBetAmount__BackingField;

	private float LastWinnings__BackingField;

	private string CasinoName__BackingField;

	private SimPerson Dealer__BackingField;

	private float SessionEarnings__BackingField;

	private int RoundsPlayed__BackingField;

	private SimCasinoGame Game__BackingField;

	private string OriginRV__BackingField;

	public float LastBetAmount
	{
		get
		{
			return LastBetAmount__BackingField;
		}
		set
		{
			LastBetAmount__BackingField = value;
		}
	}

	public float LastWinnings
	{
		get
		{
			return LastWinnings__BackingField;
		}
		set
		{
			LastWinnings__BackingField = value;
		}
	}

	public string CasinoName
	{
		get
		{
			return CasinoName__BackingField;
		}
		set
		{
			CasinoName__BackingField = value;
		}
	}

	public SimPerson Dealer
	{
		get
		{
			return Dealer__BackingField;
		}
		set
		{
			Dealer__BackingField = value;
		}
	}

	public float SessionEarnings
	{
		get
		{
			return SessionEarnings__BackingField;
		}
		set
		{
			SessionEarnings__BackingField = value;
		}
	}

	public int RoundsPlayed
	{
		get
		{
			return RoundsPlayed__BackingField;
		}
		set
		{
			RoundsPlayed__BackingField = value;
		}
	}

	public SimCasinoGame Game
	{
		get
		{
			return Game__BackingField;
		}
		set
		{
			Game__BackingField = value;
		}
	}

	public string OriginRV
	{
		get
		{
			return OriginRV__BackingField;
		}
		set
		{
			OriginRV__BackingField = value;
		}
	}

	public string Description()
	{
		return null;
	}

	public void IncreaseLastBet(float amount)
	{
	}

	public void AdjustHeroBankBalance(float refund)
	{
	}
}
