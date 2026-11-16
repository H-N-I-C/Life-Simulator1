public class ScenarioDrugScreening : ScenarioLazyCsvBase
{
	private const string CSVName = "FightClub/Fight Club Scenarios - Drug Screening";

	private readonly LeagueDrugScreeningActivity Acitvity__BackingField;

	protected LeagueDrugScreeningActivity Acitvity => Acitvity__BackingField;

	public ScenarioDrugScreening(LeagueDrugScreeningActivity activity)
		: base(null)
	{
	}

	public ScenarioDrugScreening(LeagueDrugScreeningActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	public ScenarioDrugScreening(LeagueDrugScreeningActivity activity, ChoiceCallback choiceCallback, DismissCallback onDismissSelected)
		: base(null)
	{
	}
}
