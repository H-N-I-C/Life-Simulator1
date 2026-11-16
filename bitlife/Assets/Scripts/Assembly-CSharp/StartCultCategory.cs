public class StartCultCategory : UnlockTriggerCategory<CultContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<CultContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
