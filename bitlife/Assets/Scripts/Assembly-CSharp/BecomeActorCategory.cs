public class BecomeActorCategory : UnlockTriggerCategory<JobContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<JobContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private bool ValidateOccupationIsArtist(JobContext jobContext)
	{
		return false;
	}
}
