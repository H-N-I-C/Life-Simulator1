public class ScenarioFightClubSkillShopBase : ScenarioLazyCsvBase
{
	public const string CSV_PATH = "FightClub/Fight Club Scenarios - Move Unlock";

	private readonly FightingMoveSkillShopUnlockedActivity Activity__BackingField;

	public FightingMoveSkillShopUnlockedActivity Activity => Activity__BackingField;

	public ScenarioFightClubSkillShopBase(FightingMoveSkillShopUnlockedActivity activity)
		: base(null)
	{
	}

	public ScenarioFightClubSkillShopBase(FightingMoveSkillShopUnlockedActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	public ScenarioFightClubSkillShopBase(FightingMoveSkillShopUnlockedActivity activity, ChoiceCallback choiceCallback, DismissCallback onDismissSelected)
		: base(null)
	{
	}
}
