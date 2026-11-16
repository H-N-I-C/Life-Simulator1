public abstract class ScenarioFightClubBetting : ScenarioLazyCsvBase
{
	public const string CSV_FILE = "FightClub/Fight Club Scenarios - Fight Betting";

	private readonly FightClubBettingActivity Activity__BackingField;

	public FightClubBettingActivity Activity => Activity__BackingField;

	public ScenarioFightClubBetting(FightClubBettingActivity activity)
		: base(null)
	{
	}

	public ScenarioFightClubBetting(FightClubBettingActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	public ScenarioFightClubBetting(FightClubBettingActivity activity, ChoiceCallback choiceCallback, DismissCallback onDismissSelected)
		: base(null)
	{
	}
}
