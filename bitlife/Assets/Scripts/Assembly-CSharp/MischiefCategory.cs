public class MischiefCategory : UnlockTriggerCategory<MischiefContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<MischiefContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
