public abstract class ScenarioFightClubPromotionBase : ScenarioLazyCsvBase
{
	private const string _csvFile = "FightClub/Fight Club Scenarios - Promotion";

	public FightClubPromotionActivity Activity => null;

	public ScenarioFightClubPromotionBase(FightClubPromotionActivity activity, ChoiceCallback choiceCallback = null, DismissCallback onDismissSelected = null)
		: base(null)
	{
	}
}
