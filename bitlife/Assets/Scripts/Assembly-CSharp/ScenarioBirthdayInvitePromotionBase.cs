public abstract class ScenarioBirthdayInvitePromotionBase : ScenarioLazyCsvBase
{
	private const string _csvFile = "Birthday Invite/Birthday Invite Scenarios - Promotion";

	public BirthdayInvitePromotionActivity Activity => null;

	public ScenarioBirthdayInvitePromotionBase(BirthdayInvitePromotionActivity activity, ChoiceCallback choiceCallback = null, DismissCallback onDismissSelected = null)
		: base(null)
	{
	}
}
