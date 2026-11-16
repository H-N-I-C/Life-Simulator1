public class BecomePoliticianCategory : UnlockTriggerCategory<JobContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<JobContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
