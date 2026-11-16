public class WalkCategory : UnlockTriggerCategory<WalkContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<WalkContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
