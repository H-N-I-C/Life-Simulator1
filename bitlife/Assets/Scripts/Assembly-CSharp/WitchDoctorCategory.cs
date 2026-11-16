public class WitchDoctorCategory : UnlockTriggerCategory<WitchDoctorContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<WitchDoctorContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
