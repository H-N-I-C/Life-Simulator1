using Game;

public class SkillShopCategory : UnlockTriggerCategory<SkillShopContext>
{
	private sealed class _003C_003Ec__DisplayClass4_0
	{
		public DataFightIngMove move;

		public SkillShopCategory _003C_003E4__this;

		internal bool _003CGetAllContentUnlocked_003Eb__0(SkillShopContext context)
		{
			return false;
		}
	}

	private const string unlockCondition = "Buy from the Skill Shop";

	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<SkillShopContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private bool ValidateSkillUnlock(SkillShopContext unlockContext, string skillName)
	{
		return false;
	}
}
