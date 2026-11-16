public class ScenarioFightClubMoveBase : ScenarioLazyCsvBase
{
	public const string CSV_PATH = "FightClub/Fight Club Scenarios - Move Unlock";

	private readonly FightingMoveUnlockedActivity Activity__BackingField;

	public FightingMoveUnlockedActivity Activity => Activity__BackingField;

	public ScenarioFightClubMoveBase(FightingMoveUnlockedActivity activity)
		: base(null)
	{
	}

	public ScenarioFightClubMoveBase(FightingMoveUnlockedActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	public ScenarioFightClubMoveBase(FightingMoveUnlockedActivity activity, ChoiceCallback choiceCallback, DismissCallback onDismissSelected)
		: base(null)
	{
	}
}
