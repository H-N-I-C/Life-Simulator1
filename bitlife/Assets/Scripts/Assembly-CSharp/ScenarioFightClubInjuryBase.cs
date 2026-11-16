using System.Collections;

public abstract class ScenarioFightClubInjuryBase : ScenarioLazyCsvBase
{
	protected readonly FightClubInjuryAcquiredActivity _activity;

	protected ScenarioFightClubInjuryBase(FightClubInjuryAcquiredActivity activity, string scenarioID, ChoiceCallback onChoice = null, DismissCallback onDismiss = null)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}

	public static void OverrideInjuryBarColors(ArrayList barGraphArray)
	{
	}
}
