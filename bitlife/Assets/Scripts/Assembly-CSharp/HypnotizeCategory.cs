public class HypnotizeCategory : UnlockTriggerCategory<HypnotizeContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<HypnotizeContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
