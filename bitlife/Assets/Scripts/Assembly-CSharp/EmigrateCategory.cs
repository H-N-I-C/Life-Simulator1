public class EmigrateCategory : UnlockTriggerCategory<EmigrateContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<EmigrateContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
