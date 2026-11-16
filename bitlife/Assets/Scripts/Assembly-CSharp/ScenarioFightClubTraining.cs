public abstract class ScenarioFightClubTraining : ScenarioLazyCsvBase
{
	private const string CSV_NAME = "FightClub/Fight Club Scenarios - Training";

	private readonly FightClubTrainingActivity Activity__BackingField;

	public FightClubTrainingActivity Activity => Activity__BackingField;

	public ScenarioFightClubTraining(FightClubTrainingActivity activity)
		: base(null)
	{
	}

	public ScenarioFightClubTraining(FightClubTrainingActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	public ScenarioFightClubTraining(FightClubTrainingActivity activity, ChoiceCallback choiceCallback, DismissCallback onDismissSelected)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
