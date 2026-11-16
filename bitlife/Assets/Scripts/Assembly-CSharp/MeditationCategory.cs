public class MeditationCategory : UnlockTriggerCategory<MeditationContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<MeditationContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
