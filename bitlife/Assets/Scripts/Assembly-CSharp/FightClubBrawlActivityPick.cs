using System.Collections.Generic;

public class FightClubBrawlActivityPick : FightClubBrawlActivityBase
{
	private List<SimFightClubStreetFighter> Fighters__BackingField;

	public List<SimFightClubStreetFighter> Fighters
	{
		get
		{
			return Fighters__BackingField;
		}
		private set
		{
			Fighters__BackingField = value;
		}
	}

	public FightClubBrawlActivityPick(Life life)
		: base(null)
	{
	}

	public void ChooseFighter(ChoiceSelection selection)
	{
	}
}
