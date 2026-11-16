public class PetPurchaseCategory : UnlockTriggerCategory<PetPurchaseContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<PetPurchaseContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
