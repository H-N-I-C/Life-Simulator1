public class FightClubInjuryAcquiredActivity : ActivityLazyCsvBase
{
	private SimFightClub Club__BackingField;

	private SimFightClubFighterStats HeroStats__BackingField;

	private SimFightClubInjury Injury__BackingField;

	private SimFightClubFighter Fighter__BackingField;

	private readonly SimHero _hero;

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

	public SimFightClubInjury Injury
	{
		get
		{
			return Injury__BackingField;
		}
		private set
		{
			Injury__BackingField = value;
		}
	}

	public SimFightClubFighter Fighter
	{
		get
		{
			return Fighter__BackingField;
		}
		private set
		{
			Fighter__BackingField = value;
		}
	}

	public FightClubInjuryAcquiredActivity(Life life, SimFightClubFighter fighter)
		: base(null)
	{
	}

	private SimFightClubInjury InflictRandomInjury()
	{
		return null;
	}

	private void ApplyPainImpacts(SimFightClubInjury injury)
	{
	}

	private void OnChoice(ChoiceSelection selection)
	{
	}
}
