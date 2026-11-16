public abstract class ScenarioFightClubWildEncounterBase : ScenarioLazyCsvBase
{
	protected readonly FightClubWildEncounterActivity _activity;

	protected ScenarioFightClubWildEncounterBase(FightClubWildEncounterActivity activity, string scenarioID, ChoiceCallback onChoice = null, DismissCallback onDismiss = null)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
