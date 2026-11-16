using System.Collections;

public class ScenarioFightClubFighterProfile : ScenarioLazyCsvBase
{
	private readonly FightClubFighterProfileActivity _activity;

	public ScenarioFightClubFighterProfile(FightClubFighterProfileActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	public override void RefreshScenario()
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	public static void OverrideBarGraphColors(ArrayList barGraphArray)
	{
	}
}
