public class ChiropractorCategory : UnlockTriggerCategory<AlternativeMedicineContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<AlternativeMedicineContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
