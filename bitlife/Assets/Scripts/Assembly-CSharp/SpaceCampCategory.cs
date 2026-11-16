public class SpaceCampCategory : UnlockTriggerCategory<SpaceCampContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<SpaceCampContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
