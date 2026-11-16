public class MemoryTestCategory : UnlockTriggerCategory<MemoryTestContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<MemoryTestContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
