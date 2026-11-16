using System.Collections.Generic;

public class FightClubSurgeryActivity : ActivityLazyCsvBase
{
	private SimFightClub Club__BackingField;

	private SimFightClubFighterStats HeroStats__BackingField;

	private SimFightClubInjury SelectedInjury__BackingField;

	private readonly SimHero _hero;

	private List<SimFightClubInjury> _surgeryInjuries;

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

	public SimFightClubInjury SelectedInjury
	{
		get
		{
			return SelectedInjury__BackingField;
		}
		set
		{
			SelectedInjury__BackingField = value;
		}
	}

	public FightClubSurgeryActivity(Life life)
		: base(null)
	{
	}

	public bool HasEligibleInjuries()
	{
		return false;
	}

	public bool HasPTInjuries()
	{
		return false;
	}

	public List<SimFightClubInjury> GetSurgeryInjuries()
	{
		return null;
	}

	private void OnChoice(ChoiceSelection selection)
	{
	}
}
