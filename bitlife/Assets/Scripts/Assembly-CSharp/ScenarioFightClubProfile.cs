using System.Collections;

public class ScenarioFightClubProfile : ScenarioLazyCsvBase
{
	private readonly FightClubProfileActivity _activity;

	public ScenarioFightClubProfile(FightClubProfileActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	public override void RefreshScenario()
	{
	}

	public static void OverrideInjuredStatBarColors(ArrayList barGraphArray, SimFightClubFighterStats stats)
	{
	}

	private static FIGHT_CLUB_ATTRIBUTE? GetAttributeForBarTitle(string title)
	{
		return null;
	}
}
