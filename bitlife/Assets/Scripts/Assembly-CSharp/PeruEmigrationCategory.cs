public class PeruEmigrationCategory : UnlockTriggerCategory<EmigrateContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<EmigrateContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private bool ValidateIsPeru(EmigrateContext context)
	{
		return false;
	}

	private static FightClubMoveContext Convert(EmigrateContext context)
	{
		return default;
	}
}
