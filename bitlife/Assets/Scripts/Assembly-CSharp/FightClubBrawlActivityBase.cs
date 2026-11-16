public abstract class FightClubBrawlActivityBase : ActivityLazyCsvBase
{
	private SimFightClubFighterStats HeroStats__BackingField;

	private SimFightClub Club__BackingField;

	private SimFightClubFighter Fighter__BackingField;

	public SimFightClubFighterStats HeroStats
	{
		get
		{
			return HeroStats__BackingField;
		}
		private set
		{
			HeroStats__BackingField = value;
		}
	}

	public SimFightClub Club
	{
		get
		{
			return Club__BackingField;
		}
		private set
		{
			Club__BackingField = value;
		}
	}

	public SimFightClubFighter Fighter
	{
		get
		{
			return Fighter__BackingField;
		}
		protected set
		{
			Fighter__BackingField = value;
		}
	}

	protected FightClubBrawlActivityBase(Life life)
		: base(null)
	{
	}

	public bool IsEligible()
	{
		return false;
	}

	protected bool WillAcceptBrawl()
	{
		return false;
	}

	protected void LaunchFightingGame()
	{
	}

	protected void AcceptOrRejectBrawl()
	{
	}
}
