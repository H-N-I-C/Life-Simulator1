public class CarPurchaseCategory : UnlockTriggerCategory<CarPurchaseContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<CarPurchaseContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
