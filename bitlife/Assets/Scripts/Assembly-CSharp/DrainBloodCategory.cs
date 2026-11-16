public class DrainBloodCategory : UnlockTriggerCategory<DrainBloodContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<DrainBloodContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private bool ValidateWasDrainedToDeath(DrainBloodContext context)
	{
		return false;
	}

	private MurderContext BloodDrainedToMurder(DrainBloodContext _)
	{
		return default;
	}
}
