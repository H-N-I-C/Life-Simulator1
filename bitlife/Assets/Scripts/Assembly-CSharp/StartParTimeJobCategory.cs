public class StartParTimeJobCategory : UnlockTriggerCategory<PartTimeJobContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<PartTimeJobContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
